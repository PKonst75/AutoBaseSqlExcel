using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlGuarantyType.
	/// </summary>
	public class DbSqlGuarantyType
	{

		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		find_default;
		public static SqlCommand		select_child;
		public static SqlCommand		select_root;
		public DbSqlGuarantyType()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("��������_���_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_root = new SqlCommand("��������_���_�������_�������", connection);
			select_root.CommandType = CommandType.StoredProcedure;

			select_child = new SqlCommand("��������_���_�������_��������", connection);
			select_child.Parameters.Add("@code_parent", SqlDbType.BigInt);
			select_child.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("��������_���_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_default = new SqlCommand("��������_���_�����_���������", connection);
			find_default.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtGuarantyType element			= new DtGuarantyType();
			element.SetData("���_��������", DbSql.GetValueLong(reader, "���_��������"));
			element.SetData("��������_��������", DbSql.GetValueString(reader, "��������_��������"));
			element.SetData("�������������", DbSql.GetValueBool(reader, "�������������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtGuarantyType element = (DtGuarantyType)MakeElement(reader);
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

		public static DtGuarantyType Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value	= (long)code;
			return (DtGuarantyType)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtGuarantyType FindDefault()
		{
			// ���������� ������� ������ �� ���������
			return (DtGuarantyType)DbSql.Find(find_default, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayRoot(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select_root, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayChild(ArrayList array, long code_parent)
		{
			// ���������� ������� ������ �� �����
			select_child.Parameters["@code_parent"].Value	= (long)code_parent;
			DbSql.FillArray(array, select_child, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
