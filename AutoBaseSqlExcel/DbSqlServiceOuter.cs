using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlServiceOuter.
	/// </summary>
	public class DbSqlServiceOuter
	{
		public static SqlCommand		insert;
		public static SqlCommand		find;
		public static SqlCommand		select;

		public DbSqlServiceOuter()
		{
		
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("������_���������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@address", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find = new SqlCommand("������_���������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("������_���������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static DtServiceOuter Insert(DtServiceOuter element)
		{
			// ���������� ������� ������ �� ������

			insert.Parameters["@name"].Value			= (string)element.GetData("������������");
			insert.Parameters["@address"].Value			= (string)element.GetData("�����");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���_������", (object)insert.Parameters["@code"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtServiceOuter element			= new DtServiceOuter();
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			element.SetData("�����", DbSql.GetValueString(reader, "�����"));
			
			return (object)element;
		}

		public static DtServiceOuter Find(long code)
		{
			find.Parameters["@code"].Value	= (long)code;
			return (DtServiceOuter)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array)
		{
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtServiceOuter element = (DtServiceOuter)MakeElement(reader);
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
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
	}
}
