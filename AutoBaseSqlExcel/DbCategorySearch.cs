using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Категории поиска в справочнике трудоемкостей
	/// </summary>
	public class DbCategorySearch:Db
	{
		private long		code;
		private string		name;

		private static SqlConnection	conn;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdSelect;
		private static SqlCommand		cmdSelectFind;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbCategorySearch()
		{
			code		= 0;
			name		= "";

			adding		= true;
		}
		public DbCategorySearch(DbCategorySearch src)
		{
			code		= src.code;
			name		= src.name;

			adding		= false;
		}
		public DbCategorySearch(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			name		= (string)GetValueString(reader, offset);	offset++;

			adding		= false;
		}
		#endregion

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 2 собственных поля и остальное
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КАТЕГОРИЯ_ПОИСК", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_КАТЕГОРИЯ_ПОИСК", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectFind = new SqlCommand("SELECT_КАТЕГОРИЯ_ПОИСК_НАЙТИ", conn);
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
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
			item.Text = name;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCategorySearch element = new DbCategorySearch(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCategorySearch element = new DbCategorySearch(reader, 0);
			array.Add(element);
		}
		public static DbCategorySearch Find(long code)
		{
			ArrayList array = new ArrayList();
			cmdSelectFind.Parameters["@code"].Value = (long)code;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
			if(array.Count == 0) return null;
			return (DbCategorySearch)array[0];
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Доступ к основным параметрам
		public string Name
		{
			get
			{
				return name;
			}
		}
		#endregion

		#region Переопределенные виртуальные методы
		override public string DbTitle()
		{
			return this.Name;
		}
		#endregion
	}
}
