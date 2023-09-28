using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerContact.
	/// </summary>
	public class DbSqlPartnerContact
	{
		public static SqlCommand select;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;

		public DbSqlPartnerContact()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_�������_�������", connection);
			select.Parameters.Add("@code_partner", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("����������_�������_�����", connection);
			find.Parameters.Add("@code_partner", SqlDbType.BigInt);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_�������_����������", connection);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@type", SqlDbType.VarChar);
			insert.Parameters.Add("@sort", SqlDbType.VarChar);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("����������_�������_���������", connection);
			update.Parameters.Add("@code_partner", SqlDbType.BigInt);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@sort", SqlDbType.VarChar);
			update.Parameters.Add("@contact", SqlDbType.VarChar);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("����������_�������_��������", connection);
			delete.Parameters.Add("@code_partner", SqlDbType.BigInt);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartnerContact element	= new DtPartnerContact();
			element.SetData("������_���_����������_�������", DbSql.GetValueLong(reader, "������_���_����������_�������"));
			element.SetData("���_�������", DbSql.GetValueLong(reader, "���_�������"));
			element.SetData("���_�������", DbSql.GetValueString(reader, "���_�������"));
			element.SetData("���_�������", DbSql.GetValueString(reader, "���_�������"));
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			element.SetData("����������_�������", DbSql.GetValueString(reader, "����������_�������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtPartnerContact element = (DtPartnerContact)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_partner)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@code_partner"].Value = (long)code_partner;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, long code_partner)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@code_partner"].Value = (long)code_partner;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtPartnerContact Find(long code_partner, long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code_partner"].Value	= (long)code_partner;
			find.Parameters["@code"].Value			= (long)code;
			return (DtPartnerContact)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtPartnerContact Insert(DtPartnerContact element)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@code_partner"].Value	= (long)element.GetData("������_���_����������_�������");
			insert.Parameters["@type"].Value			= (string)element.GetData("���_�������");
			insert.Parameters["@sort"].Value			= (string)element.GetData("���_�������");
			insert.Parameters["@contact"].Value			= (string)element.GetData("�������");
			insert.Parameters["@comment"].Value			= (string)element.GetData("����������_�������");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("���_�������",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtPartnerContact element)
		{
			// ���������� ������� ������ �� �����
			update.Parameters["@code_partner"].Value	= (long)element.GetData("������_���_����������_�������");
			update.Parameters["@code"].Value			= (long)element.GetData("���_�������");
			update.Parameters["@sort"].Value			= (string)element.GetData("���_�������");
			update.Parameters["@contact"].Value			= (string)element.GetData("�������");
			update.Parameters["@comment"].Value			= (string)element.GetData("����������_�������");
			if(DbSql.ExecuteCommandError(update)== false) return false;
			return true;
		}

		public static bool Delete(long code_partner, long code)
		{
			// ���������� ������� ������ �� �����
			delete.Parameters["@code_partner"].Value		= (long)code_partner;
			delete.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}
	}
}
