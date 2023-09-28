using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkCollection.
	/// </summary>
	public class DbSqlWorkCollection
	{
		public static SqlCommand		select;
		public static SqlCommand		update;
		public static SqlCommand		insert;
		public static SqlCommand		remove;

		public DbSqlWorkCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("������������_���������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("������������_���������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("������������_���������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			remove = new SqlCommand("������������_���������_��������", connection);
			remove.Parameters.Add("@code", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkCollection element			= new DtWorkCollection();
			element.SetData("���_���������", DbSql.GetValueLong(reader, "���_���������"));
			element.SetData("������������_���������", DbSql.GetValueString(reader, "������������_���������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkCollection element = (DtWorkCollection)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static void SelectInList(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtWorkCollection Insert(DtWorkCollection element)
		{
			// ���������� ������
			insert.Parameters["@name"].Value = (string)element.GetData("������������_���������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���_���������", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtWorkCollection element)
		{
			// ���������� ������
			update.Parameters["@code"].Value = (long)element.GetData("���_���������");
			update.Parameters["@name"].Value = (string)element.GetData("������������_���������");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			return true;
		}

		public static bool Remove(long code)
		{
			// ���������� ������
			remove.Parameters["@code"].Value = (long)code;
			if(DbSql.ExecuteCommandError(remove) == false) return false;
			return true;
		}

	}
}
