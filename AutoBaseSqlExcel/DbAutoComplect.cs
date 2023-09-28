using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс описывающий комплектацию автомобилей
	/// </summary>
	public class DbAutoComplect:Db
	{
		private long			code;
		private long			codeModel;
		private string			codeComplectTxt;
		private string			complectDescription;
		private int				productYearStart;
		private int				productYearEnd;

		// Связь с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 6 собственных полей
			readerLength = 6;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_КОМПЛЕКТАЦИЯ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeComplectTxt", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@complectDescription", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@productYearStart", SqlDbType.Int);
			cmdWrite.Parameters.Add("@productYearEnd", SqlDbType.Int);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_КОМПЛЕКТАЦИЯ", conn);
			cmdSelect.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@productYear", SqlDbType.Int);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Конструкторы
		public DbAutoComplect(DbAutoModel src)
		{
			code					= 0;
			if(src != null)
				codeModel			= src.Code;
			else
				codeModel			= 0;
			codeComplectTxt			= "";
			complectDescription		= "";
			productYearStart		= 0;
			productYearEnd			= 0;

			adding					= true;
		}
		public DbAutoComplect(DbAutoComplect src)
		{
			code					= src.code;
			codeModel				= src.codeModel;
			codeComplectTxt			= src.codeComplectTxt;
			complectDescription		= src.complectDescription;
			productYearStart		= src.productYearStart;
			productYearEnd			= src.productYearEnd;
		}
		public DbAutoComplect(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeModel				= (long)GetValueLong(reader, offset);		offset++;
			codeComplectTxt			= (string)GetValueString(reader, offset);	offset++;
			complectDescription		= (string)GetValueString(reader, offset);	offset++;
			productYearStart		= (int)GetValueInt(reader, offset);			offset++;
			productYearEnd			= (int)GetValueInt(reader, offset);			offset++;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			if((adding == false)&&(changed == false)) return true; // Изменений нет

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@codeModel"].Value				= (long)codeModel;
				cmdWrite.Parameters["@codeComplectTxt"].Value		= (string)codeComplectTxt;
				cmdWrite.Parameters["@complectDescription"].Value	= (string)complectDescription;
				cmdWrite.Parameters["@productYearStart"].Value		= (int)productYearStart;
				cmdWrite.Parameters["@productYearEnd"].Value		= (int)productYearEnd;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		#endregion

		#region Доступ к основным параметрам - Изменение
		public string CodeComplectTxt
		{
			get
			{
				return codeComplectTxt;
			}
			set
			{
				codeComplectTxt = SetStringNotEmptyLength(codeComplectTxt, value, 25, "Код комплектации");
			}
		}
		public string ComplectDescription
		{
			get
			{
				return complectDescription;
			}
			set
			{
				complectDescription = SetStringLength(complectDescription, value, 128, "Описание комплектации");
			}
		}
		public string ProductYearStartTxt
		{
			get
			{
				if(productYearStart == 0) return "";
				return productYearStart.ToString();
			}
			set
			{
				if(value == "")
				{
					if(productYearStart == 0) return;
					productYearStart	= 0;
					changed				= true;
				}
				productYearStart		= SetIntNotMinus(productYearStart, value, "Год начала выпуска");
			}
		}
		public string ProductYearEndTxt
		{
			get
			{
				if(productYearEnd == 0) return "";
				return productYearEnd.ToString();
			}
			set
			{
				if(value == "")
				{
					if(productYearEnd == 0) return;
					productYearEnd	= 0;
					changed				= true;
				}
				productYearEnd		= SetIntNotMinus(productYearEnd, value, "Год окончания выпуска");
			}
		}
		#endregion

		#region Отображение в текст основных параметров
		#endregion

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = codeComplectTxt;
			item.SubItems[1].Text = complectDescription;
			item.SubItems[2].Text = ProductYearStartTxt + " - " + ProductYearEndTxt;
			item.Tag = this;
		}

		public static void FillList(ListView list, DbAutoModel model, int productYear)
		{
			cmdSelect.Parameters["@codeModel"].Value	= model.Code;
			cmdSelect.Parameters["@productYear"].Value	= productYear;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoComplect element = new DbAutoComplect(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array, DbAutoModel model, int productYear)
		{
			cmdSelect.Parameters["@codeModel"].Value	= model.Code;
			cmdSelect.Parameters["@productYear"].Value	= productYear;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbAutoComplect element = new DbAutoComplect(reader, 0);
			array.Add(element);
		}
		#endregion

		#region Доступ к основным параметрам - чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Переопределение виртуальных методов
		public override string DbTitle()
		{
			return this.codeComplectTxt;
		}
		#endregion
	}
}
