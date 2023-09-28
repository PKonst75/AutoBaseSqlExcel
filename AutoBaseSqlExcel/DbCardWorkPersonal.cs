using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardWorkPersonal.
	/// </summary>
	public class DbCardWorkPersonal : Db
	{
		
		// Основные параметры
		private long		cardNumber;
		private int			cardYear;
		private int			number;
		private long		donePersonal;
		
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
			// 4 собственных поля
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КАРТОЧКА_ТРУДОЕМКОСТЬ_ИCПОЛНИТЕЛЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@number", SqlDbType.Int);
			cmdWrite.Parameters.Add("@donePersonal", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@deleting", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);

			cmdSelect = new SqlCommand("SELECT_КАРТОЧКА_ИСПОЛНИТЕЛЬ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdSelect.Parameters.Add("@number", SqlDbType.Int);
		}
		#endregion

		#region Конструкторы
		public DbCardWorkPersonal(DbCardWork cardWork, DbStaff staff)
		{
			cardNumber		= cardWork.CardNumber;
			cardYear		= cardWork.CardYear;
			number			= cardWork.Number;
			if(staff != null)
			{
				donePersonal	= staff.Code;
				adding			= true;
				deleted			= false;
			}
			else
			{
				donePersonal	= 0;
				adding			= false;
				deleted			= true;
			}
		}

		public DbCardWorkPersonal(SqlDataReader reader, int offset)
		{
			cardNumber		= (long)GetValueLong(reader, offset);	offset++;
			cardYear		= (int)GetValueInt(reader, offset);		offset++;
			number			= (int)GetValueInt(reader, offset);		offset++;
			donePersonal	= (long)GetValueLong(reader, offset);	offset++;
			
			adding			= false;
			deleted			= false;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			if((adding == false)&&(deleted == false)) return true; // Изменений нет

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@deleting"].Value		= (bool)deleted;
				cmdWrite.Parameters["@cardNumber"].Value	= (long)cardNumber;
				cmdWrite.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdWrite.Parameters["@number"].Value		= (int)number;
				cmdWrite.Parameters["@donePersonal"].Value	= (long)donePersonal;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
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

		public static bool WriteListSelection(ListView list, DbCardWork cardWork)
		{
			DbCardWorkPersonal cardWorkPersonal;
			string message = "Не удалось записать выполнение работы";
			bool flag = false;

			foreach(ListViewItem item in list.SelectedItems)
			{
				DbStaff element = (DbStaff)item.Tag;
				if(element != null)
				{
					cardWorkPersonal = new DbCardWorkPersonal(cardWork, element);
					if(cardWorkPersonal.Write())
					{
						if(flag == false)
						{
							flag = true;
							cardWork.DonePersonal = element;
							message = "Записано выполнение работ на следующий персонал: \n";
						}
						message += element.Title + "\n";
					}
				}
			}
			//MessageBox.Show((IWin32Window)null, message);
			ShowFaults();
			return true;
		}

		public static bool WriteListSelectionCode(ListView list, DbCardWork cardWork)
		{
			DbCardWorkPersonal cardWorkPersonal;
			string message = "Не удалось записать выполнение работы";
			bool flag = false;

			foreach(ListViewItem item in list.SelectedItems)
			{
				long staff_code = (long)item.Tag;
				DbStaff element = (DbStaff)DbStaff.Find(staff_code);
				if(element != null)
				{
					cardWorkPersonal = new DbCardWorkPersonal(cardWork, element);
					if(cardWorkPersonal.Write())
					{
						if(flag == false)
						{
							flag = true;
							cardWork.DonePersonal = element;
							message = "Записано выполнение работ на следующий персонал: \n";
						}
						message += element.Title + "\n";
					}
				}
			}
			//MessageBox.Show((IWin32Window)null, message);
			ShowFaults();
			return true;
		}
		#endregion
	}
}
