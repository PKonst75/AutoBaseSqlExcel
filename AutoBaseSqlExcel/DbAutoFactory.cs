using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAutoFactory.
	/// </summary>
	public class DbAutoFactory:Db
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

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 2 собственных поля
			readerLength = 2;

			conn = connection;

			cmdExecWrite = new SqlCommand("WRITE_AUTO_FACTORY", conn);
			cmdExecWrite.CommandType = CommandType.StoredProcedure;
			cmdExecWrite.Parameters.Add("@code", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdExecWrite);

			cmdExecSelect = new SqlCommand("SELECT_AUTO_FACTORY", conn);
			cmdExecSelect.CommandType = CommandType.StoredProcedure;
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdExecWrite.Transaction = trans;
		}

		public DbAutoFactory()
		{
			code = "";
			name = "";

			adding = true;
		}

		public DbAutoFactory(SqlDataReader reader, int offset)
		{
			code =		(string)GetValueString(reader, offset);		offset++;
			name =		(string)GetValueString(reader, offset);		offset++;
		}

		public DbAutoFactory(DbAutoFactory source)
		{
			code = source.code;
			name = source.name;
		}

		public string Name
		{
			set
			{
				name = this.SetStringNotEmptyLength(name, value, 50, "НАИМЕНОВАНИЕ");
			}
			get
			{
				return name;
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
			item.Text = this.name;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdExecSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoFactory element = new DbAutoFactory(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdExecWrite.Parameters["@adding"].Value = (bool)adding;
				cmdExecWrite.Parameters["@code"].Value = (string)code;
				cmdExecWrite.Parameters["@name"].Value = (string)name;
				cmdExecWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdExecWrite);
				cmdExecWrite.Parameters["@code"].Value = (string)code;
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
			MessageBox.Show("Производитель автомобилей добавлен/изменен");
			return true;
		}
	}
}
