using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ � ��������� �� ������
	/// </summary>
	public class DbSqlStorageDetail
	{

		public static SqlCommand report_exchange;
		public static SqlCommand select_number;
		public static SqlCommand select_name;
		public static SqlCommand select_1c;
		public static SqlCommand select_name_number;
		public static SqlCommand find_1c;
		public static SqlCommand calculate_storage;
		public static SqlCommand find;

		public DbSqlStorageDetail()
		{
		}

		public static void Init(SqlConnection connection)
		{
			report_exchange = new SqlCommand("�����_������_������_��������", connection);
			report_exchange.Parameters.Add("@date_start", SqlDbType.DateTime);
			report_exchange.Parameters.Add("@date_end", SqlDbType.DateTime);
			report_exchange.CommandType = CommandType.StoredProcedure;
			report_exchange.CommandTimeout = 300;

			select_number = new SqlCommand("�����_������_�������_�����", connection);
			select_number.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_number.Parameters.Add("@show_null", SqlDbType.Bit);
			select_number.CommandType = CommandType.StoredProcedure;

			select_name = new SqlCommand("�����_������_�������_������������", connection);
			select_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_name.Parameters.Add("@show_null", SqlDbType.Bit);
			select_name.CommandType = CommandType.StoredProcedure;

			select_name_number = new SqlCommand("�����_������_�������_������������_�����", connection);
			select_name_number.Parameters.Add("@number", SqlDbType.VarChar);
			select_name_number.Parameters.Add("@show_null", SqlDbType.Bit);
			select_name_number.CommandType = CommandType.StoredProcedure;

			select_1c = new SqlCommand("�����_������_�������_1�", connection);
			select_1c.CommandType = CommandType.StoredProcedure;

			find_1c = new SqlCommand("�����_������_�����_1�", connection);
			find_1c.Parameters.Add("@code_1c", SqlDbType.BigInt);
			find_1c.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("�����_������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			calculate_storage = new SqlCommand("�����_������_��������_��������", connection);
			calculate_storage.Parameters.Add("@code", SqlDbType.BigInt);
			calculate_storage.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(calculate_storage);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageDetail element			= new DtStorageDetail();
			element.SetData("���_�����_������", DbSql.GetValueLong(reader, "���_�����_������"));
			element.SetData("������������_�����_������", DbSql.GetValueString(reader, "������������_�����_������"));
			element.SetData("�����_�����_������", DbSql.GetValueString(reader, "�����_�����_������"));
			element.SetData("����������_�����_������", DbSql.GetValueFloat(reader, "����������_�����_������"));
			element.SetData("����_�����_������", DbSql.GetValueFloat(reader, "����_�����_������"));
			element.SetData("����_�����_������", DbSql.GetValueFloat(reader, "����_�����_������"));
			element.SetData("�������_���������", DbSql.GetValueString(reader, "�������_���������"));
			element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			element.SetData("���_1�_�����_������", DbSql.GetValueLong(reader, "���_1�_�����_������"));
			element.SetData("������", DbSql.GetValueFloat(reader, "������"));
			element.SetData("�������", DbSql.GetValueFloat(reader, "�������"));
			element.SetData("����������", DbSql.GetValueFloat(reader, "����������"));
			element.Liquid = DbSql.GetValueBool(reader, "��������_�����_������");
			
			return (object)element;
		}

		public static ListViewItem MakeLVItemBalance(SqlDataReader reader)
		{
			DtStorageDetail element = (DtStorageDetail)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemBalance(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static void SelectInListBalance(ListView list, DateTime start, DateTime end)
		{
			// ���������� ������� ������ �� �����
			report_exchange.Parameters["@date_start"].Value		= (DateTime)start;
			report_exchange.Parameters["@date_end"].Value		= (DateTime)end;
			DbSql.FillList(list, report_exchange, new DbSql.DelegateMakeLVItem(MakeLVItemBalance));
		}


		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageDetail element = (DtStorageDetail)MakeElement(reader);
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
		public static void SelectInListNumber(ListView list, string pattern, bool show_null)
		{
			// ���������� ������� ������ �� �����
			select_number.Parameters["@pattern"].Value			= (string)pattern;
			select_number.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_number, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListName(ListView list, string pattern, bool show_null)
		{
			// ���������� ������� ������ �� �����
			select_name.Parameters["@pattern"].Value		= (string)pattern;
			select_name.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListNameNumber(ListView list, string number, bool show_null)
		{
			// ���������� ������� ������ �� �����
			select_name_number.Parameters["@number"].Value			= (string)number;
			select_name_number.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_name_number, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtStorageDetail Find1C(long code_1c)
		{
			// ���������� ������� ������ �� �����
			find_1c.Parameters["@code_1c"].Value = (long)code_1c;
			return (DtStorageDetail)DbSql.Find(find_1c, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtStorageDetail Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			return (DtStorageDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray1C(ArrayList array)
		{
			DbSql.FillArray(array, select_1c, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool CalculateStorage(long code)
		{
			// ���������� ������� ������ �� �����
			calculate_storage.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(calculate_storage);
		}
	}
}
