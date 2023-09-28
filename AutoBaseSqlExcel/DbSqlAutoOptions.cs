using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoOptions.
	/// </summary>
	public class DbSqlAutoOptions:DbSql
	{
		public static SqlCommand insert_option;
		public static SqlCommand select_option;
		public static SqlCommand select_option_find;
		public static SqlCommand select_option_group_complect;
		public static SqlCommand set_option_find;
		public static SqlCommand remove_option_find;

		public static SqlCommand insert_option_variant;
		public static SqlCommand select_option_variant;

		public static SqlCommand select_group;

		public static SqlCommand insert_complect;
		public static SqlCommand remove_complect;
		public static SqlCommand select_option_complect;

		public DbSqlAutoOptions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select_option = new SqlCommand("����������_�����_�������", connection);
			select_option.CommandType = CommandType.StoredProcedure;

			select_option_find = new SqlCommand("����������_�����_�������_�����", connection);
			select_option_find.CommandType = CommandType.StoredProcedure;

			select_option_group_complect = new SqlCommand("����������_�����_�������_������������_������", connection);
			select_option_group_complect.Parameters.Add("@code_group", SqlDbType.BigInt);
			select_option_group_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_option_group_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			select_option_group_complect.CommandType = CommandType.StoredProcedure;

			select_group = new SqlCommand("����������_�����_������_�������", connection);
			select_group.CommandType = CommandType.StoredProcedure;

			select_option_variant = new SqlCommand("����������_�����_�������_�������", connection);
			select_option_variant.Parameters.Add("@code_option", SqlDbType.BigInt);
			select_option_variant.CommandType = CommandType.StoredProcedure;

			select_option_complect = new SqlCommand("����������_�����_�������_������������", connection);
			select_option_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_option_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			select_option_complect.CommandType = CommandType.StoredProcedure;

			insert_option = new SqlCommand("����������_�����_����������", connection);
			insert_option.Parameters.Add("@code", SqlDbType.BigInt);
			insert_option.Parameters.Add("@name", SqlDbType.VarChar);
			insert_option.Parameters.Add("@code_group", SqlDbType.BigInt);
			insert_option.CommandType = CommandType.StoredProcedure;
			insert_option.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert_option);

			set_option_find = new SqlCommand("����������_�����_�_�����", connection);
			set_option_find.Parameters.Add("@code", SqlDbType.BigInt);
			set_option_find.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_option_find);

			remove_option_find = new SqlCommand("����������_�����_��_������", connection);
			remove_option_find.Parameters.Add("@code", SqlDbType.BigInt);
			remove_option_find.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove_option_find);

			insert_option_variant = new SqlCommand("����������_�����_�������_����������", connection);
			insert_option_variant.Parameters.Add("@code", SqlDbType.BigInt);
			insert_option_variant.Parameters.Add("@name", SqlDbType.VarChar);
			insert_option_variant.Parameters.Add("@code_option", SqlDbType.BigInt);
			insert_option_variant.CommandType = CommandType.StoredProcedure;
			insert_option_variant.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert_option_variant);

			insert_complect = new SqlCommand("����������_������������_��������_����������", connection);
			insert_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_option", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_option_variant", SqlDbType.BigInt);
			insert_complect.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_complect);

			remove_complect = new SqlCommand("����������_������������_��������_��������", connection);
			remove_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			remove_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			remove_complect.Parameters.Add("@code_option", SqlDbType.BigInt);
			remove_complect.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove_complect);
		}

		#region ������ �����
		public static ListViewItem MakeLVItemOption(SqlDataReader reader)
		{
			DtAutoOption element = (DtAutoOption)MakeElementOption(reader);
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
		public static void SelectInListOption(ListView list)
		{
			// ���������� ������� ������
			DbSql.FillList(list, select_option, new DbSql.DelegateMakeLVItem(MakeLVItemOption));
		}
		public static void SelectInListOptionFind(ListView list)
		{
			// ���������� ������� ������
			DbSql.FillList(list, select_option_find, new DbSql.DelegateMakeLVItem(MakeLVItemOption));
		}
		public static object MakeElementOption(SqlDataReader reader)
		{
			DtAutoOption element			= new DtAutoOption();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("�����_������������", DbSql.GetValueString(reader, "�����_������������"));
			element.SetData("������_���_�����_������", DbSql.GetValueLong(reader, "������_���_�����_������"));
			
			return (object)element;
		}

		public static DtAutoOption InsertOption(DtAutoOption element)
		{
			insert_option.Parameters["@name"].Value = (string)element.GetData("�����_������������");
			insert_option.Parameters["@code_group"].Value = (long)element.GetData("������_���_�����_������");
			if(DbSql.ExecuteCommandError(insert_option) != true) return null;
			element.SetData("���", (long)insert_option.Parameters["@code"].Value);
			return element;
		}

		public static bool SetOptionFind(long code_option)
		{
			set_option_find.Parameters["@code"].Value = (long)code_option;
			if(DbSql.ExecuteCommandError(set_option_find) != true) return false;
			return true;
		}
		public static bool RemoveOptionFind(long code_option)
		{
			remove_option_find.Parameters["@code"].Value = (long)code_option;
			if(DbSql.ExecuteCommandError(remove_option_find) != true) return false;
			return true;
		}
		#endregion

		#region ������ ��������� �����
		public static ListViewItem MakeLVItemOptionVariant(SqlDataReader reader)
		{
			DtAutoOptionVariant element = (DtAutoOptionVariant)MakeElementOptionVariant(reader);
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
		public static void SelectInListVariant(ListView list, long code_option)
		{
			// ���������� ������� ������
			select_option_variant.Parameters["@code_option"].Value = (long)code_option;
			DbSql.FillList(list, select_option_variant, new DbSql.DelegateMakeLVItem(MakeLVItemOptionVariant));
		}
		public static object MakeElementOptionVariant(SqlDataReader reader)
		{
			DtAutoOptionVariant element			= new DtAutoOptionVariant();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("�������_������������", DbSql.GetValueString(reader, "�������_������������"));
			element.SetData("������_���_�����", DbSql.GetValueLong(reader, "������_���_�����"));
			
			return (object)element;
		}

		public static DtAutoOptionVariant InsertOptionVariant(DtAutoOptionVariant element)
		{
			insert_option_variant.Parameters["@name"].Value = (string)element.GetData("�������_������������");
			insert_option_variant.Parameters["@code_option"].Value = (long)element.GetData("������_���_�����");
			if(DbSql.ExecuteCommandError(insert_option_variant) != true) return null;
			element.SetData("���", (long)insert_option_variant.Parameters["@code"].Value);
			return element;
		}
		#endregion

		#region ������ ����� �����
		public static object MakeElementOptionGroup(SqlDataReader reader)
		{
			DtAutoOptionGroup element			= new DtAutoOptionGroup();
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("������������_������", DbSql.GetValueString(reader, "������������_������"));
			
			return (object)element;
		}
		public static void SelectInArrayGroup(ArrayList array)
		{
			// ���������� ������� ������
			DbSql.FillArray(array, select_group, new DbSql.DelegateMakeElement(MakeElementOptionGroup));
		}

		#endregion

		#region ������������
		public static object MakeElementOptionComplect(SqlDataReader reader)
		{
			DtAutoOption element			= new DtAutoOption();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("�����_������������", DbSql.GetValueString(reader, "�����_������������"));
			element.SetData("������_���_�����_������", DbSql.GetValueLong(reader, "������_���_�����_������"));

			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("�������_������������", DbSql.GetValueString(reader, "�������_������������"));

			// ���������� �������������� ������ ��� ��������
			if (element.tmp_code_model > 0) element.tmp_active = true;
			
			return (object)element;
		}
		public static ListViewItem MakeLVItemOptionComplect(SqlDataReader reader)
		{
			DtAutoOption element = (DtAutoOption)MakeElementOptionComplect(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemComplect(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static bool InsertComplect(long code_model, long code_model_variant, long code_option, long code_option_variant)
		{
			insert_complect.Parameters["@code_model"].Value = code_model;
			insert_complect.Parameters["@code_model_variant"].Value = code_model_variant;
			insert_complect.Parameters["@code_option"].Value = code_option;
			insert_complect.Parameters["@code_option_variant"].Value = code_option_variant;
			if(DbSql.ExecuteCommandError(insert_complect) != true) return false;
			return true;
		}
		public static bool RemoveComplect(long code_model, long code_model_variant, long code_option)
		{
			remove_complect.Parameters["@code_model"].Value = code_model;
			remove_complect.Parameters["@code_model_variant"].Value = code_model_variant;
			remove_complect.Parameters["@code_option"].Value = code_option;
			if(DbSql.ExecuteCommandError(remove_complect) != true) return false;
			return true;
		}
		public static void SelectInListOtionComplect(ListView list, long code_model, long code_model_variant)
		{
			// ���������� ������� ������
			select_option_complect.Parameters["@code_model"].Value = (long)code_model;
			select_option_complect.Parameters["@code_model_variant"].Value = (long)code_model_variant;
			DbSql.FillList(list, select_option_complect, new DbSql.DelegateMakeLVItem(MakeLVItemOptionComplect));
		}
		public static void SelectInArrayOptionGroupCpmplect(ArrayList array, long code_group, long code_model, long code_variant)
		{
			// ���������� ������� ������
			select_option_group_complect.Parameters["@code_group"].Value = (long)code_group;
			select_option_group_complect.Parameters["@code_model"].Value = (long)code_model;
			select_option_group_complect.Parameters["@code_model_variant"].Value = (long)code_variant;
			DbSql.FillArray(array, select_option_group_complect, new DbSql.DelegateMakeElement(MakeElementOptionComplect));
		}
		#endregion

	}
}
