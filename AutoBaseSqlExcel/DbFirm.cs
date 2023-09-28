using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbFirm.
	/// </summary>
	public class DbFirm:Db
	{
		private string code;
		private string name;

		private static SqlConnection conn;
		private static SqlCommand cmdExecWrite;
		private static SqlCommand cmdExecSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public DbFirm()
		{
			code = "";
			name = "";
			adding = true;
		}

		public DbFirm(DbFirm sourceFirm)
		{
			code = sourceFirm.code;
			name = sourceFirm.name;
		}

		public DbFirm(SqlDataReader reader, int offset)
		{
			code =		(string)GetValueString(reader, offset);		offset++;
			name =		(string)GetValueString(reader, offset);		offset++;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 2 собственных поля
			readerLength = 2;

			conn = connection;

			cmdExecWrite = new SqlCommand("WRITE_FIRM", conn);
			cmdExecWrite.CommandType = CommandType.StoredProcedure;
			cmdExecWrite.Parameters.Add("@code", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdExecWrite);
			cmdExecWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdExecSelect = new SqlCommand("SELECT_FIRM", conn);
			cmdExecSelect.CommandType = CommandType.StoredProcedure;
		}

		public string Code
		{
			get
			{
				return code;
			}
			set
			{
				code = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = this.SetStringNotEmptyLength(name, value, 120, "НАИМЕНОВАНИЕ ПРОИЗВОДИТЕЛЯ");
			}
		}

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
			Db.DbFillList(list, cmdExecSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbFirm element = new DbFirm(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		public bool Write()
		{
			try
			{
				cmdExecWrite.Parameters["@adding"].Value = (bool)adding;
				cmdExecWrite.Parameters["@code"].Value = (string)code;
				cmdExecWrite.Parameters["@name"].Value = (string)name;
				cmdExecWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdExecWrite);
				code = (string)cmdExecWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			MessageBox.Show("Производитель добавлен/изменен");
			return true;
		}
	}
}
