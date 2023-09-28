using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbWorkPlace.
	/// </summary>
	public class DbWorkPlace:Db
	{
		// Основные параметры
		private long		code;		// Код рабочего места
		private string		name;		// Наименование
		private int			hour1;		// Рабочие
		private int			hour2;		//		часы
		private int			hour3;		//

		// Дополнительные
		private bool[]		hours;		// Развернутое описание рабочих часов

		// Связь с базой
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		// Для отображения на экране
		private static int height1;
		private static int height2;
		private static int width1;
		private static int width2;
		private static int offsetX1;
		private static int offsetX2;
		private static int offsetY1;
		private static int offsetY2;
		private static Brush brushGray;
		private static Brush brushGreen;
		private static Brush brushBlack;
		private static Brush brushRed;
		private static Brush brushLightBlue;
		private static Pen pen;
		private static Font font;

		private static long selectedCodeGroup;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}


		#region Конструкторы
		public DbWorkPlace()
		{
			hours = new bool[96];

			code = 0;
			name = "";
			hour1 = 0;
			hour2 = 0;
			hour3 = 0;

			adding = true;
		}

		public DbWorkPlace(SqlDataReader reader, int offset)
		{
			hours = new bool[96];

			code	= (long)GetValueLong(reader, offset);		offset++;
			name	= (string)GetValueString(reader, offset);	offset++;
			hour1	= (int)GetValueInt(reader, offset);			offset++;
			hour2	= (int)GetValueInt(reader, offset);			offset++;
			hour3	= (int)GetValueInt(reader, offset);			offset++;
		}
		#endregion

		#region Иницаилизация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			height1 = 20;
			width1 = 23;
			offsetX1 = 70;
			offsetY1 = 50;

			selectedCodeGroup = 0;
			brushGray = new SolidBrush(Color.Gray);
			brushGreen = new SolidBrush(Color.LightGreen);
			brushRed = new SolidBrush(Color.Red);
			brushLightBlue = new SolidBrush(Color.LightBlue);
			brushBlack = new SolidBrush(Color.Black);
			pen = new Pen(Color.Black, 1);
			font = new Font("Microsoft Sans Serif", 10);

			// Расчет общей длины использования ридера
			// 5 собственных полей
			readerLength = 5;

			conn = connection;

			cmdWrite= new SqlCommand("WRITE_РАБОЧЕЕ_МЕСТО", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@hour1", SqlDbType.Int);
			cmdWrite.Parameters.Add("@hour2", SqlDbType.Int);
			cmdWrite.Parameters.Add("@hour3", SqlDbType.Int);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_РАБОЧЕЕ_МЕСТО", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@code"].Value = (long)code;
				cmdWrite.Parameters["@name"].Value = (string)name;
				cmdWrite.Parameters["@hour1"].Value = (int)hour1;
				cmdWrite.Parameters["@hour2"].Value = (int)hour2;
				cmdWrite.Parameters["@hour3"].Value = (int)hour3;
				cmdWrite.Parameters["@adding"].Value = (bool)adding;
				cmdWrite.Parameters["@delete"].Value = (bool)deleted;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
				
			}
			catch(Exception E)
			{
				trans.Rollback();
				SetTransaction(null);
				SetException(E);
				ShowFaults();
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("Рабочее место добавлено/изменено/удалено");
			return true;
		}
		#endregion

		public Rectangle GetCellRect(int i, int offsetX, int offsetY)
		{
			if((i < 0) || (i > 95)) return new Rectangle(0, 0, 0, 0);
			//int y = (int)i / (int)24;
			//int x = i - y * 24;
			int x = (int)i / (int) 4;
			int y = i - x * 4;
			x = x * width1 + offsetX;
			y = y * height1 + offsetY;
			Rectangle rect = new Rectangle(x, y, width1, height1);
			return rect;
		}

		#region Отображение на экране
		public void DrawHour(int i, Graphics graph, int offsetX, int offsetY)
		{
			Rectangle rect = GetCellRect(i, offsetX, offsetY);
			if(hours[i] == true)
			{
				graph.FillRectangle(brushGreen, rect);
			}
			else
			{
				graph.FillRectangle(brushGray, rect);
			}
			graph.DrawRectangle(pen, rect);
		}

		public void DrawHour(int i, Graphics graph, object[] outer, int offsetX, int offsetY)
		{
			Rectangle rect = GetCellRect(i, offsetX, offsetY);
			if(hours[i] == true)
			{
				if(outer == null || outer[i]==null)
				{
					graph.FillRectangle(brushGreen, rect);
				}
				else
				{
					graph.FillRectangle(brushRed, rect);
					if(selectedCodeGroup != 0)
					{
						//if(((DbJournal)outer[i]).CodeGroup == selectedCodeGroup)
						//{
						//	graph.FillRectangle(brushLightBlue, rect);
						//}
						if(((DbJournal)outer[i]).ExternCodeGroup == selectedCodeGroup)
						{
							graph.FillRectangle(brushLightBlue, rect);
						}
					}
				}
			}
			else
			{
				graph.FillRectangle(brushGray, rect);
			}
			graph.DrawRectangle(pen, rect);
		}

		public void DrawHours(Graphics graph, int offsetX, int offsetY)
		{
			for(int i = 0; i < 96; i++)
			{
				DrawHour(i, graph, offsetX, offsetY);
			}
		}

		public void DrawHours(Graphics graph, object[] outer, int offsetX, int offsetY)
		{
			for(int i = 0; i < 96; i++)
			{
				DrawHour(i, graph, outer, offsetX, offsetY);
			}
		}

		public void DrawTitle(Graphics graph, int offsetX, int offsetY)
		{
			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;
			int y = -2;
			int x = 0;
			x = x * width1 + offsetX;
			y = y * height1 + offsetY;
			Rectangle rect = new Rectangle(x, y, width1 * 24, height1);
			graph.DrawString(name, font, brushBlack, rect, format);
			for(int i = 0; i < 24; i++)
			{
				y = -1;
				x = i;
				x = x * width1 + offsetX;
				y = y * height1 + offsetY;
				rect = new Rectangle(x, y, width1, height1);
				graph.DrawString(i.ToString(), font, brushBlack, rect, format);
			}

			for(int i = 0; i < 4; i++)
			{
				y = i;
				x = -1;
				string txt = (i * 15).ToString() + " - " + ((i + 1)* 15).ToString();
				x = x * width1 * 3 + offsetX;
				y = y * height1 + offsetY;
				rect = new Rectangle(x, y, width1 * 3, height1);
				graph.DrawString(txt, font, brushBlack, rect, format);
			}
		}
		#endregion

		public void ChangeStatus(int i)
		{
			if((i < 0)||(i > 95)) return;
			if(hours[i] == true)
				hours[i] = false;
			else
				hours[i] = true;
		}

		public int ChangeStatus(Point pnt, int offsetX, int offsetY)
		{
			int i = GetIndex(pnt, offsetX, offsetY);
			if(i < 0) return -1;
			ChangeStatus(i);
			return i;
		}

		public int GetIndex(Point pnt, int offsetX, int offsetY)
		{
			pnt.X = pnt.X - offsetX;
			pnt.Y = pnt.Y - offsetY;
			if((pnt.X < 0) || (pnt.Y < 0)) return -1;
			int x = pnt.X / width1;
			int y = pnt.Y / height1;	
			if(y >= 4) return -1;
			int i = y + x * 4;
			if(i >= 96) return -1;
			return i;
		}

		public void MakeHours()
		{
			// Установление свернутых рабочих часов по развернутым
			int hours1 = 0;
			int hours2 = 0;
			int hours3 = 0;
			for(int j =0; j < 3;  j++)
			{
				for(int i = 0; i < 32; i++)
				{
					if(hours[j*32 + i] == true)
					{
						switch(j)
						{
							case 0:
								hours1 = hours1 + (0x00000001 << i);
								break;
							case 1:
								hours2 = hours2 + (0x00000001 << i);
								break;
							case 2:
								hours3 = hours3 + (0x00000001 << i);
								break;
						}
					}
				}
			}
			hour1 = SetInt(hour1, hours1);
			hour2 = SetInt(hour2, hours2);
			hour3 = SetInt(hour3, hours3);
		}

		public void MakeHour()
		{
			for(int j =0; j < 3;  j++)
			{
				for(int i = 0; i < 32; i++)
				{
					
					switch(j)
					{
						case 0:
							if((hour1 & (0x00000001 << i)) != 0) hours[j*32 + i] = true;
							else hours[j*32 + i] = false;
							break;
						case 1:
							if((hour2 & (0x00000001 << i)) != 0) hours[j*32 + i] = true;
							else hours[j*32 + i] = false;
							break;
						case 2:
							if((hour3 & (0x00000001 << i)) != 0) hours[j*32 + i] = true;
							else hours[j*32 + i] = false;
							break;
					}
					
				}
			}
		}

		public int CountHours()
		{
			int workDays = 0;
			for(int j =0; j < 3;  j++)
			{
				for(int i = 0; i < 32; i++)
				{
					
					switch(j)
					{
						case 0:
							if((hour1 & (0x00000001 << i)) != 0) workDays++;
							break;
						case 1:
							if((hour2 & (0x00000001 << i)) != 0) workDays++;
							break;
						case 2:
							if((hour3 & (0x00000001 << i)) != 0) workDays++;
							break;
					}
					
				}
			}
			return workDays;
		}

		public string Name
		{
			set
			{
				name = SetStringNotEmptyLength(name, value, 120, "НАИМЕНОВАНИЕ");
			}
			get
			{
				return name;
			}
		}

		/* Заполнение листа */
		public static void FillList(ListView list)
		{
			SqlDataReader reader = null;
			try
			{
				reader = cmdSelect.ExecuteReader();
				while(reader.Read())
				{
					DbWorkPlace workPlace = new DbWorkPlace(reader, 0);
					list.Items.Add(workPlace.LVItem);
				}	
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
			}
			if(reader != null) reader.Close();
			ShowFaults();
		}

		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = name;
			item.Tag = this;
		}

		/* Создание листа */
		public static ArrayList MakeList()
		{
			ArrayList list = null;
			SqlDataReader reader = null;
			try
			{
				reader = cmdSelect.ExecuteReader();
				while(reader.Read())
				{
					if(list == null) list = new ArrayList();
					DbWorkPlace workPlace = new DbWorkPlace(reader, 0);
					list.Add((object)workPlace);
				}	
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			ShowFaults();
			return list;
		}

		public bool IsValidIndex(int index)
		{
			if((index < 0) || (index > 95)) return false;
			return true;
		}

		public long Code
		{
			get
			{
				return code;
			}
		}

		public bool IsAvalaiableAt(int codeTime)
		{
			return hours[codeTime];
		}

		public static void SetSelectedGroup(long setCodeGroup)
		{
			selectedCodeGroup = setCodeGroup;
		}
	}
}
