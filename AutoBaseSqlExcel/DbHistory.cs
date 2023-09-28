using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbHistory.
	/// </summary>
	public class DbHistory:Db
	{
		public enum TableCode{DbWork=1};

		private DateTime	time;
		private string		table;
		private string		action;
		private long		code_object;
		private long		code_high_object;
		private long		additional_code;
		private long		additional_code_1;
		private int			additional_code_2;
		private string		name;
		private string		description_1;
		private string		description_2;
		private string		user;

		// Связь с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 7 собственных полей
			readerLength = 9;

			conn = connection;

			cmdSelect = new SqlCommand("SELECT_ИСТОРИЯ_2", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@table", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@date_start", SqlDbType.DateTime);
			cmdSelect.Parameters.Add("@date_end", SqlDbType.DateTime);
			cmdSelect.Parameters.Add("@user", SqlDbType.VarChar);
		}


		public DbHistory(SqlDataReader reader, int offset)
		{
			time					= (DateTime)GetValueDate(reader, offset);		offset++;
			table					= (string)GetValueString(reader, offset);		offset++;
			action					= (string)GetValueString(reader, offset);		offset++;
			code_object				= (long)GetValueLong(reader, offset);			offset++;
			code_high_object		= (long)GetValueLong(reader, offset);			offset++;
			additional_code			= (long)GetValueLong(reader, offset);			offset++;
			additional_code_1		= (long)GetValueLong(reader, offset);			offset++;
			additional_code_2		= (int)GetValueInt(reader, offset);				offset++;
			name					= (string)GetValueString(reader, offset);		offset++;
			description_1			= (string)GetValueString(reader, offset);		offset++;
			description_2			= (string)GetValueString(reader, offset);		offset++;
			user					= (string)GetValueString(reader, offset);		offset++;
		}

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = time.ToString();
			item.SubItems[1].Text = table;
			item.SubItems[2].Text = action;
			item.SubItems[3].Text = name;
			item.SubItems[4].Text = description_1;
			item.SubItems[5].Text = user;

			item.Tag = this;
		}
		public static void FillList(ListView list, string table, DateTime date_start, DateTime date_end, string user)
		{
			cmdSelect.Parameters["@table"].Value = table;
			cmdSelect.Parameters["@date_start"].Value = date_start;
			cmdSelect.Parameters["@date_end"].Value = date_end;
			cmdSelect.Parameters["@user"].Value = user;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbHistory element = new DbHistory(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

	}
}
