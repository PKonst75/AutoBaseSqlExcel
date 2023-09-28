using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlFactory.
	/// </summary>
	public class DbSqlFactory
	{
		public static SqlCommand	select;
		public static SqlCommand	find;
		public static SqlCommand	find_prefix;
		public static SqlCommand	insert;
		public static SqlCommand	update;
		public static SqlCommand	delete;

		public DbSqlFactory()
		{
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_�������������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("����������_�������������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_prefix = new SqlCommand("����������_�������������_�����_�������", connection);
			find_prefix.Parameters.Add("@prefix", SqlDbType.VarChar);
			find_prefix.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_�������������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@prefix", SqlDbType.VarChar);
			DbSql.SetReturnError(insert);
			insert.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("����������_�������������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.Parameters.Add("@prefix", SqlDbType.VarChar);
			DbSql.SetReturnError(update);
			update.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtFactory element			= new DtFactory();
			element.SetData("���_����������_�������������", DbSql.GetValueLong(reader, "���_����������_�������������"));
			element.SetData("������������_����������_�������������", DbSql.GetValueString(reader, "������������_����������_�������������"));
			element.SetData("�������_����������_�������������", DbSql.GetValueString(reader, "�������_����������_�������������"));
			return (object)element;
		}

		public static void SelectInList(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtFactory Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			DtFactory element = (DtFactory)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}
		public static DtFactory FindPrefix(string prefix)
		{
			// ���������� ������� ������ �� �����
			find_prefix.Parameters["@prefix"].Value = (string)prefix;
			DtFactory element = (DtFactory)DbSql.Find(find_prefix, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool Insert(DtFactory data)
		{
			insert.Parameters["@code"].Value		= (long)data.GetData("���_����������_�������������");
			insert.Parameters["@name"].Value		= (string)data.GetData("������������_����������_�������������");
			insert.Parameters["@prefix"].Value		= (string)data.GetData("�������_����������_�������������");
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			data.SetData("���_����������_�������������", (long)insert.Parameters["@code"].Value);
			MessageBox.Show("�����-������������ ��������");
			return true;
		}
		public static bool Update(DtFactory data)
		{
			update.Parameters["@code"].Value		= (long)data.GetData("���_����������_�������������");
			update.Parameters["@name"].Value		= (string)data.GetData("������������_����������_�������������");
			update.Parameters["@prefix"].Value		= (string)data.GetData("�������_����������_�������������");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			MessageBox.Show("�����-������������ �������");
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtFactory element = (DtFactory)MakeElement(reader);
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
	}
}
