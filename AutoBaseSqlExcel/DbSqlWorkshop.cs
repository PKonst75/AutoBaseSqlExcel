using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkshop.
	/// </summary>
	public class DbSqlWorkshop
	{
		public static SqlCommand		select;
		public static SqlCommand		find;

		public DbSqlWorkshop()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("���_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("���_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkshop element			= new DtWorkshop();
			element.SetData("���_���", DbSql.GetValueLong(reader, "���_���"));
			element.SetData("������������_���", DbSql.GetValueString(reader, "������������_���"));
			element.SetData("����������_���", DbSql.GetValueString(reader, "����������_���"));
			element.SetData("�������_����������", DbSql.GetValueString(reader, "�������_����������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkshop element = (DtWorkshop)MakeElement(reader);
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

		public static void SelectInArray(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtWorkshop Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value	= (long)code;
			return (DtWorkshop)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

	}
}
