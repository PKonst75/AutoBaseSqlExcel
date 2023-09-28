using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// � ����� ������
	/// </summary>
	public class DbSqlCardDetailOrder
	{
		public static SqlCommand insert;
		public static SqlCommand select;

		public DbSqlCardDetailOrder()
		{
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("��������_������_������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_catalogue_detail", SqlDbType.BigInt);
			insert.Parameters.Add("@code_storage_detail", SqlDbType.BigInt);
			insert.Parameters.Add("@guaranty_flag", SqlDbType.Bit);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("��������_������_������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static DtCardDetailOrder Insert(DtCardDetailOrder element)
		{
			insert.Parameters["@card_number"].Value				= (long)element.GetData("�����_��������");
			insert.Parameters["@card_year"].Value				= (int)element.GetData("���_��������");
			insert.Parameters["@code_catalogue_detail"].Value	= (long)element.GetData("���_�������_������");
			insert.Parameters["@code_storage_detail"].Value		= (long)element.GetData("���_�����_������");
			insert.Parameters["@guaranty_flag"].Value			= (bool)element.GetData("��������");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("���_��������_������_������", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardDetailOrder element			= new DtCardDetailOrder();
			element.SetData("���_��������_������_������", DbSql.GetValueLong(reader, "���_��������_������_������"));
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("���_�������_������", DbSql.GetValueLong(reader, "���_�������_������"));
			element.SetData("���_�����_������", DbSql.GetValueLong(reader, "���_�����_������"));
			element.SetData("��������", DbSql.GetValueBool(reader, "��������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardDetailOrder element = (DtCardDetailOrder)MakeElement(reader);
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

		public static void SelectInList(ListView list, long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

	}
}
