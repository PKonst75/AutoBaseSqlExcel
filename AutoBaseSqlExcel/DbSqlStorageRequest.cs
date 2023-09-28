using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlStorageRequest.
	/// </summary>
	public class DbSqlStorageRequest
	{
		private static SqlCommand insert;
		private static SqlCommand select;
		private static SqlCommand select_partner_name;
		private static SqlCommand find;
		private static SqlCommand give;
		private static SqlCommand execute;
		private static SqlCommand archive;

		public DbSqlStorageRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("�����_������_������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@code_storage", SqlDbType.BigInt);
			insert.Parameters.Add("@quontity", SqlDbType.Real);
			insert.Parameters.Add("@guaranty", SqlDbType.Bit);
			insert.Parameters.Add("@date_perfomance", SqlDbType.DateTime);
			insert.Parameters.Add("@code_requester", SqlDbType.BigInt);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@tmp_date_perfomance_is", SqlDbType.Bit);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters["@year"].Direction = ParameterDirection.InputOutput;
			insert.Parameters["@date"].Direction = ParameterDirection.InputOutput;
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			give = new SqlCommand("�����_������_������_������", connection);
			give.Parameters.Add("@code", SqlDbType.BigInt);
			give.Parameters.Add("@year", SqlDbType.Int);
			give.Parameters.Add("@date_give", SqlDbType.DateTime);
			give.Parameters.Add("@code_giver", SqlDbType.BigInt);
			give.Parameters.Add("@date_supply", SqlDbType.DateTime);
			give.CommandType = CommandType.StoredProcedure;
			give.Parameters["@date_give"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(give);

			execute = new SqlCommand("�����_������_������_����������", connection);
			execute.Parameters.Add("@code", SqlDbType.BigInt);
			execute.Parameters.Add("@year", SqlDbType.Int);
			execute.Parameters.Add("@date_execute", SqlDbType.DateTime);
			execute.Parameters.Add("@code_execute", SqlDbType.BigInt);
			execute.CommandType = CommandType.StoredProcedure;
			execute.Parameters["@date_execute"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(execute);

			archive = new SqlCommand("�����_������_������_���������", connection);
			archive.Parameters.Add("@code", SqlDbType.BigInt);
			archive.Parameters.Add("@year", SqlDbType.Int);
			archive.Parameters.Add("@code_archive", SqlDbType.BigInt);
			archive.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(archive);

			select = new SqlCommand("�����_������_������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_partner_name = new SqlCommand("�����_������_������_�������_����������_������������", connection);
			select_partner_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_partner_name.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("�����_������_������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static DtStorageRequest Insert(DtStorageRequest element)
		{
			insert.Parameters["@code"].Value					= (long)element.GetData("���_������");
			insert.Parameters["@year"].Value					= (int)element.GetData("���_������");
			insert.Parameters["@date"].Value					= (DateTime)element.GetData("����_������");
			insert.Parameters["@code_storage"].Value			= (long)element.GetData("������_���_�����_������");
			insert.Parameters["@quontity"].Value				= (float)element.GetData("����������_�����_������");
			insert.Parameters["@guaranty"].Value				= (bool)element.GetData("��������_������");
			insert.Parameters["@date_perfomance"].Value			= (DateTime)element.GetData("���������_����_����������");
			insert.Parameters["@code_requester"].Value			= (long)element.GetData("���_��������_������");
			insert.Parameters["@card_number"].Value				= (long)element.GetData("������_�����_��������");
			insert.Parameters["@card_year"].Value				= (int)element.GetData("������_���_��������");
			insert.Parameters["@code_partner"].Value			= (long)element.GetData("������_���_����������");
			insert.Parameters["@tmp_date_perfomance_is"].Value	= (bool)element.GetData("����_���������_����_����������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���_������", insert.Parameters["@code"].Value);
			element.SetData("���_������", insert.Parameters["@year"].Value);
			element.SetData("����_������", insert.Parameters["@date"].Value);
			return element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageRequest element = (DtStorageRequest)MakeElement(reader);
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

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageRequest element			= new DtStorageRequest();
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("���_������", DbSql.GetValueInt(reader, "���_������"));
			element.SetData("����_������", DbSql.GetValueDate(reader, "����_������"));
			element.SetData("������_���_�����_������", DbSql.GetValueLong(reader, "������_���_�����_������"));
			element.SetData("����������_�����_������", DbSql.GetValueFloat(reader, "����������_�����_������"));
			element.SetData("��������_������", DbSql.GetValueBool(reader, "��������_������"));

			if(DbSql.IsValueNULL(reader, "���������_����_����������") == false)
				element.SetData("���������_����_����������", DbSql.GetValueDate(reader, "���������_����_����������"));

			element.SetData("���_��������_������", DbSql.GetValueLong(reader, "���_��������_������"));
			if(DbSql.IsValueNULL(reader, "����_������_������") == false)
				element.SetData("����_������_������", DbSql.GetValueDate(reader, "����_������_������"));
			if(DbSql.IsValueNULL(reader, "����_������_����������") == false)
				element.SetData("����_������_����������", DbSql.GetValueDate(reader, "����_������_����������"));
			if(DbSql.IsValueNULL(reader, "����_��������") == false)
				element.SetData("����_��������", DbSql.GetValueDate(reader, "����_��������"));

			element.SetData("������������_�����_������", DbSql.GetValueString(reader, "������������_�����_������"));
			element.SetData("��������_������", DbSql.GetValueString(reader, "��������_������"));
			element.SetData("����������_������������", DbSql.GetValueString(reader, "����������_������������"));
			return (object)element;
		}

		public static object MakeElementFind(SqlDataReader reader)
		{
			DtStorageRequest element			= new DtStorageRequest();
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("���_������", DbSql.GetValueInt(reader, "���_������"));
			element.SetData("����_������", DbSql.GetValueDate(reader, "����_������"));
			element.SetData("������_���_�����_������", DbSql.GetValueLong(reader, "������_���_�����_������"));
			element.SetData("����������_�����_������", DbSql.GetValueFloat(reader, "����������_�����_������"));
			element.SetData("��������_������", DbSql.GetValueBool(reader, "��������_������"));
			if(DbSql.IsValueNULL(reader, "���������_����_����������") == false)
				element.SetData("���������_����_����������", DbSql.GetValueDate(reader, "���������_����_����������"));
			element.SetData("���_��������_������", DbSql.GetValueLong(reader, "���_��������_������"));
			element.SetData("������_�����_��������", DbSql.GetValueLong(reader, "������_�����_��������"));
			element.SetData("������_���_��������", DbSql.GetValueInt(reader, "������_���_��������"));
			element.SetData("������_���_����������", DbSql.GetValueLong(reader, "������_���_����������"));
			if(DbSql.IsValueNULL(reader, "����_������_������") == false)
				element.SetData("����_������_������", DbSql.GetValueDate(reader, "����_������_������"));
			element.SetData("���_��������_������_������", DbSql.GetValueLong(reader, "���_��������_������_������"));
			if(DbSql.IsValueNULL(reader, "����_������_����������") == false)
				element.SetData("����_������_����������", DbSql.GetValueDate(reader, "����_������_����������"));
			element.SetData("���_��������_����������_������", DbSql.GetValueLong(reader, "���_��������_����������_������"));
			if(DbSql.IsValueNULL(reader, "����_��������") == false)
				element.SetData("����_��������", DbSql.GetValueDate(reader, "����_��������"));

			element.SetData("������������_�����_������", DbSql.GetValueString(reader, "������������_�����_������"));
			element.SetData("��������_������", DbSql.GetValueString(reader, "��������_������"));
			return (object)element;
		}

		public static void SelectInList(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_partner_name.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_partner_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtStorageRequest Find(long code, int year)
		{
			// ����� ��������
			find.Parameters["@code"].Value = (long)code;
			find.Parameters["@year"].Value = (int)year;
			return (DtStorageRequest)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtStorageRequest Give(DtStorageRequest element)
		{
			give.Parameters["@code"].Value			= (long)element.GetData("���_������");
			give.Parameters["@year"].Value			= (int)element.GetData("���_������");
			give.Parameters["@code_giver"].Value	= (long)element.GetData("���_��������_������_������");
			give.Parameters["@date_supply"].Value	= (DateTime)element.GetData("����_��������");
			if(DbSql.ExecuteCommandError(give) == false) return null;
			element.SetData("����_������_������", give.Parameters["@date_give"].Value);
			return element;
		}
		public static DtStorageRequest Execute(DtStorageRequest element)
		{
			execute.Parameters["@code"].Value			= (long)element.GetData("���_������");
			execute.Parameters["@year"].Value			= (int)element.GetData("���_������");
			execute.Parameters["@code_execute"].Value	= (long)element.GetData("���_��������_����������_������");
			if(DbSql.ExecuteCommandError(execute) == false) return null;
			element.SetData("����_������_����������", execute.Parameters["@date_execute"].Value);
			return element;
		}
		public static bool Archive(DtStorageRequest element)
		{
			archive.Parameters["@code"].Value			= (long)element.GetData("���_������");
			archive.Parameters["@year"].Value			= (int)element.GetData("���_������");
			archive.Parameters["@code_archive"].Value	= (long)element.GetData("���_��������_���������");
			if(DbSql.ExecuteCommandError(archive) == false) return false;
			return true;
		}
	}
}
