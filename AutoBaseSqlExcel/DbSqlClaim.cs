using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlClaim.
	/// </summary>
	public class DbSqlClaim
	{
		public static SqlCommand select;
		public static SqlCommand find;
		public static SqlCommand remove;
		public static SqlCommand select_name;
		public static SqlCommand insert;

		public DbSqlClaim()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			select_name = new SqlCommand("������_�������_������������", connection);
			select_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_name.CommandType = CommandType.StoredProcedure;


			insert = new SqlCommand("������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			remove = new SqlCommand("������_��������", connection);
			remove.Parameters.Add("@code", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtClaim element			= new DtClaim();
			element.Code = DbSql.GetValueLong(reader, "���_������");
			element.Name = DbSql.GetValueString(reader, "������������_������");
			//element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			//element.SetData("������������_������", DbSql.GetValueString(reader, "������������_������"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtClaim element = (DtClaim)MakeElement(reader);
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

		public static void SelectInList(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_name.Parameters["@pattern"].Value	= (string)pattern;
			DbSql.FillList(list, select_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtClaim Insert(DtClaim element)
		{
			insert.Parameters["@name"].Value = element.Name;
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.Code = (long)insert.Parameters["@code"].Value;
			//element.SetData("���_������", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static DtClaim Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value	= (long)code;
			return (DtClaim)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool Remove(long code)
		{
			// ���������� ������� ������ �� �����
			remove.Parameters["@code"].Value	= (long)code;
			return DbSql.ExecuteCommandError(remove);
		}
	}
}
