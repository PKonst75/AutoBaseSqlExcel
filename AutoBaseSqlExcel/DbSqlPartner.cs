using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartner.
	/// </summary>
	public class DbSqlPartner
	{
		public static SqlCommand select;
		public static SqlCommand select_title;
		public static SqlCommand select_birthday;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand delete;
		public static SqlCommand update;

		public DbSqlPartner()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_title = new SqlCommand("����������_�������_������������", connection);
			select_title.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_title.CommandType = CommandType.StoredProcedure;

			select_birthday = new SqlCommand("����������_�������_���_��������", connection);
			select_birthday.Parameters.Add("@date", SqlDbType.DateTime);
			select_birthday.CommandType = CommandType.StoredProcedure;


			find = new SqlCommand("����������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("����������_���������", connection);
			// ����� �����
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@title", SqlDbType.VarChar, 64);
			update.Parameters.Add("@comment", SqlDbType.VarChar, 60);
			update.Parameters.Add("@inn", SqlDbType.VarChar, 25);
			// ����� ���������� ����������� ����
			update.Parameters.Add("@surname", SqlDbType.VarChar, 25);
			update.Parameters.Add("@name", SqlDbType.VarChar, 25);
			update.Parameters.Add("@patronymic", SqlDbType.VarChar, 25);
			update.Parameters.Add("@registration", SqlDbType.VarChar, 255);
			update.Parameters.Add("@birthday", SqlDbType.DateTime);
			update.Parameters.Add("@is_birthday", SqlDbType.Bit);
			update.Parameters.Add("@address_living", SqlDbType.VarChar, 255);
			// ����� ���������� ������������ ����
			update.Parameters.Add("@name_juridical", SqlDbType.VarChar, 1024);
			update.Parameters.Add("@address_juridical", SqlDbType.VarChar, 255);
			update.Parameters.Add("@address_fact", SqlDbType.VarChar, 255);
			update.Parameters.Add("@contact", SqlDbType.VarChar, 256);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			insert = new SqlCommand("����������_����������", connection);
			// ����� �����
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@juridical", SqlDbType.Bit);
			insert.Parameters.Add("@title", SqlDbType.VarChar);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@inn", SqlDbType.VarChar);
			// ����� ���������� ����������� ����
			insert.Parameters.Add("@surname", SqlDbType.VarChar);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@patronymic", SqlDbType.VarChar);
			insert.Parameters.Add("@registration", SqlDbType.VarChar);
			insert.Parameters.Add("@birthday", SqlDbType.DateTime);
			insert.Parameters.Add("@is_birthday", SqlDbType.Bit);
			insert.Parameters.Add("@address_living", SqlDbType.VarChar);
			// ����� ���������� ������������ ����
			insert.Parameters.Add("@name_juridical", SqlDbType.VarChar);
			insert.Parameters.Add("@address_juridical", SqlDbType.VarChar);
			insert.Parameters.Add("@address_fact", SqlDbType.VarChar);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			delete = new SqlCommand("����������_��������", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartner element			= new DtPartner();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("������������_�������", DbSql.GetValueString(reader, "������������_�������"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			element.SetData("�����������_����", DbSql.GetValueBool(reader, "�����������_����"));
			element.SetData("���", DbSql.GetValueString(reader, "���"));
			if((bool)element.GetData("�����������_����") == false)
			{
				element.SetData("����������", DbSqlPartnerPerson.MakeElement(reader));
			}
			else
			{
				element.SetData("�����������", DbSqlPartnerJuridical.MakeElement(reader));
			}
			// ����� ������������
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			element.SetData("�������_�������", DbSql.GetValueString(reader, "�������_�������"));
			return (object)element;
		}

		public static object MakeElement2(SqlDataReader reader, Dt srcPartner = null)
		{
			DtPartner element;
			if (srcPartner == null)
				element = new DtPartner();
			else
				element = (DtPartner)srcPartner;
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("������������_�������", DbSql.GetValueString(reader, "������������_�������"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			element.SetData("�����������_����", DbSql.GetValueBool(reader, "�����������_����"));
			element.SetData("���", DbSql.GetValueString(reader, "���"));
			if ((bool)element.GetData("�����������_����") == false)
			{
				element.SetData("����������", DbSqlPartnerPerson.MakeElement(reader));
			}
			else
			{
				element.SetData("�����������", DbSqlPartnerJuridical.MakeElement(reader));
			}
			// ����� ������������
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			element.SetData("�������_�������", DbSql.GetValueString(reader, "�������_�������"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtPartner element = (DtPartner)MakeElement(reader);
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
			// ���������� ������� ������
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string pattern)
		{
			// ���������� ������� ������
			select_title.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_title, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, string pattern)
		{
			// ���������� ������� ������
			select_title.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillArray(array, select_title, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array, DateTime date)
		{
			// ���������� ������� ������
			select_birthday.Parameters["@date"].Value = date;
			DbSql.FillArray(array, select_birthday, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInList(ListView list, DateTime date)
		{
			// ���������� ������� ������
			select_birthday.Parameters["@date"].Value = date;
			DbSql.FillList(list, select_birthday, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}


		public static DtPartner Find(long code)
		{
			// ���������� ������� ������
			find.Parameters["@code"].Value = (long)code;
			return (DtPartner) DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void LoadFromDatabase(DtPartner srcPartner)
		{
			// ���������� ������� ������
			if (srcPartner == null) return;
			find.Parameters["@code"].Value = (long)srcPartner.Code;
			DbSql.LoadFromDatabase(find, new DbSql.DelegateMakeElement2(MakeElement2), srcPartner);
		}

		public static bool Update(DtPartner partner)
		{
			// ���� ��� �����
			update.Parameters["@code"].Value		= (long)partner.GetData("���_����������");
			update.Parameters["@title"].Value		= (string)partner.GetData("������������_�������");
			update.Parameters["@comment"].Value		= (string)partner.GetData("����������");
			update.Parameters["@inn"].Value			= (string)partner.GetData("���");
			// �� ��� ��������� ����������� ����
			if((bool)partner.GetData("�����������_����") == false)
			{
				DbSqlPartnerPerson.Update((DtPartnerPerson)partner.GetData("����������"), update);
				DbSqlPartnerJuridical.Update(new DtPartnerJuridical(), update);
			}
			else
			{
				DbSqlPartnerPerson.Update(new DtPartnerPerson(), update);
				DbSqlPartnerJuridical.Update((DtPartnerJuridical)partner.GetData("�����������"), update);
			}
			return DbSql.ExecuteCommandError(update);
		}

		public static DtPartner Insert(DtPartner partner)
		{
			// ���� ��� �����
			insert.Parameters["@code"].Value		= (long)partner.GetData("���_����������");
			insert.Parameters["@title"].Value		= (string)partner.GetData("������������_�������");
			insert.Parameters["@comment"].Value		= (string)partner.GetData("����������");
			insert.Parameters["@inn"].Value			= (string)partner.GetData("���");
			// �� ��� ��������� ����������� ����
			if((bool)partner.GetData("�����������_����") == false)
			{
				insert.Parameters["@juridical"].Value		= false;
				DbSqlPartnerPerson.Insert((DtPartnerPerson)partner.GetData("����������"), insert);
				DbSqlPartnerJuridical.Insert(new DtPartnerJuridical(), insert);
			}
			else
			{
				insert.Parameters["@juridical"].Value		= true;
				DbSqlPartnerPerson.Insert(new DtPartnerPerson(), insert);
				DbSqlPartnerJuridical.Insert((DtPartnerJuridical)partner.GetData("�����������"), insert);
			}
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			partner.SetData("���_����������", (long)insert.Parameters["@code"].Value);
			return partner;
		}

		public static bool Delete(long code)
		{
			// ���������� ������� ������
			delete.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete);
		}

	}
}
