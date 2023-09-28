using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Список цехов (подразделений) предприятия
	/// </summary>
	public class DbWorkshop:Db
	{
		private long		code;
		private string		name;
		private string		implement;

		// Связь с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectFind;

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
			// 3 собственных полей
			readerLength = 3;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ЦЕХ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@implement", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_ЦЕХ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectFind = new SqlCommand("SELECT_ЦЕХ_НАЙТИ", conn);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
		}
		#endregion

		#region Конструкторы
		public DbWorkshop()
		{
			code					= 0;
			name					= "";
			implement				= "";

			adding					= true;
		}
		public DbWorkshop(DbWorkshop src)
		{
			code					= src.code;
			name					= src.name;
			implement				= src.implement;
		}
		public DbWorkshop(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			name					= (string)GetValueString(reader, offset);	offset++;
			implement					= (string)GetValueString(reader, offset);	offset++;
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
				
				cmdWrite.Parameters["@adding"].Value	= (bool)adding;
				cmdWrite.Parameters["@code"].Value		= (long)code;
				cmdWrite.Parameters["@name"].Value		= (string)name;
				cmdWrite.Parameters["@implement"].Value	= (string)implement;
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

		public static DbWorkshop Find(long code)
		{
			SqlDataReader reader = null;
			DbWorkshop workshop = null;
			try
			{

				cmdSelectFind.Parameters["@code"].Value = code;
				reader = cmdSelectFind.ExecuteReader();
				if(reader.Read())
					workshop = new DbWorkshop(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return workshop;
		}
		#endregion

		#region Доступ к основным параметрам - Изменение
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = SetStringNotEmptyLength(name, value, 64, "Наименование подразделения");
			}
		}
		public string Implement
		{
			get
			{
				return implement;
			}
			set
			{
				implement = SetStringNotEmptyLength(name, value, 256, "Применение подразделения");
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = name;
			item.SubItems[1].Text = implement;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbWorkshop element = new DbWorkshop(reader, 0);
			ListViewItem	item = list.Items.Add(element.LVItem);
		}

		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbWorkshop element = new DbWorkshop(reader, 0);
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
			return this.name;
		}
		#endregion
	}
}
