using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс описания типов коробки передач
	/// </summary>
	public class DbTransmissionType:Db
	{
		private long code;
		private string description;

		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbTransmissionType()
		{
			code			= 0;
			description		= "";

			adding			= true;
		}
		public DbTransmissionType(DbTransmissionType src)
		{
			code			= src.code;
			description		= src.description;
		}
		public DbTransmissionType(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);			offset++;
			description		= (string)GetValueString(reader, offset);		offset++;
		}
		#endregion

		#region Инициализация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 2 собственных поля
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КПП_ТИП", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@description", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction=ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_КПП_ТИП", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
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
				
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@description"].Value	= (string)description;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			if(adding) MessageBox.Show("Новый тип КПП добавлен");
			else
				if(changed) MessageBox.Show("Тип КПП изменен");
			return true;
		}
		#endregion

		#region Отображение
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
			item.Text	= this.description;
			item.Tag	= this;
		}
		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbTransmissionType element = new DbTransmissionType(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region Доступ к основным параметрам - изменение
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = this.SetStringNotEmptyLength(description, value, 64, "Описание типа гарантии");
			}
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
	}
}
