using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoSellServ.
	/// </summary>
	public class DbSqlAutoSellServ
	{
		public static SqlCommand	insert;
		public static SqlCommand	find;

		public DbSqlAutoSellServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			find = new SqlCommand("����������_�������_���������_�����", connection);
			find.Parameters.Add("@code_sell", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_�������_���������_����������", connection);
			insert.Parameters.Add("@code_sell", SqlDbType.BigInt);
			insert.Parameters.Add("@code_manager", SqlDbType.BigInt);
			insert.Parameters.Add("@flag_music", SqlDbType.Bit);
			insert.Parameters.Add("@flag_alarm", SqlDbType.Bit);
			insert.Parameters.Add("@flag_tune", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti1", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti2", SqlDbType.Bit);
			insert.Parameters.Add("@flag_other", SqlDbType.Bit);
			insert.Parameters.Add("@flag_gibdd", SqlDbType.Bit);
			insert.Parameters.Add("@flag_sprav", SqlDbType.Bit);
			insert.Parameters.Add("@flag_kasko", SqlDbType.Bit);
			insert.Parameters.Add("@flag_osago", SqlDbType.Bit);
			insert.Parameters.Add("@summ_whole", SqlDbType.Float);
			insert.Parameters.Add("@summ_anti", SqlDbType.Float);
			insert.Parameters.Add("@summ_sprav", SqlDbType.Float);
			insert.Parameters.Add("@auto_summ", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_money", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_other", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_anti", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_tunemus", SqlDbType.Float);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);
		}

		public static bool Insert(DtAutoSellServ sellserv)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@code_sell"].Value		= (long)sellserv.GetData("���_�������");
			insert.Parameters["@code_manager"].Value	= (long)sellserv.GetData("��������");
			insert.Parameters["@flag_music"].Value		= (bool)sellserv.GetData("������");
			insert.Parameters["@flag_alarm"].Value		= (bool)sellserv.GetData("������������");
			insert.Parameters["@flag_tune"].Value		= (bool)sellserv.GetData("������");
			insert.Parameters["@flag_anti"].Value		= (bool)sellserv.GetData("�������");
			insert.Parameters["@flag_anti1"].Value		= (bool)sellserv.GetData("���������");
			insert.Parameters["@flag_anti2"].Value		= (bool)sellserv.GetData("������");
			insert.Parameters["@flag_other"].Value		= (bool)sellserv.GetData("����������");
			insert.Parameters["@flag_gibdd"].Value		= (bool)sellserv.GetData("�����");
			insert.Parameters["@flag_sprav"].Value		= (bool)sellserv.GetData("�����������");
			insert.Parameters["@flag_kasko"].Value		= (bool)sellserv.GetData("�����");
			insert.Parameters["@flag_osago"].Value		= (bool)sellserv.GetData("�����");

			insert.Parameters["@summ_whole"].Value			= (float)sellserv.GetData("����_�����");
			insert.Parameters["@summ_anti"].Value			= (float)sellserv.GetData("�������_�����");
			insert.Parameters["@summ_sprav"].Value			= (float)sellserv.GetData("�����������_�����");
			insert.Parameters["@auto_summ"].Value			= (float)sellserv.GetData("����_���������");
			insert.Parameters["@auto_discount_money"].Value	= (float)sellserv.GetData("����_������_������");
			insert.Parameters["@auto_discount_other"].Value	= (float)sellserv.GetData("����_������_�������");
			insert.Parameters["@auto_discount_anti"].Value	= (float)sellserv.GetData("����_������_�������");
			insert.Parameters["@auto_discount_tunemus"].Value	= (float)sellserv.GetData("����_������_����");
			
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			return true;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoSellServ element			= new DtAutoSellServ();
			element.SetData("���_�������", DbSql.GetValueLong(reader, "���_�������"));
			element.SetData("��������", DbSql.GetValueLong(reader, "��������"));

			element.SetData("������", DbSql.GetValueBool(reader, "������"));
			element.SetData("������������", DbSql.GetValueBool(reader, "������������"));
			element.SetData("������", DbSql.GetValueBool(reader, "������"));
			element.SetData("�������", DbSql.GetValueBool(reader, "�������"));
			element.SetData("���������", DbSql.GetValueBool(reader, "���������"));
			element.SetData("������", DbSql.GetValueBool(reader, "������"));
			element.SetData("����������", DbSql.GetValueBool(reader, "����������"));
			element.SetData("�����", DbSql.GetValueBool(reader, "�����"));
			element.SetData("�����������", DbSql.GetValueBool(reader, "�����������"));
			element.SetData("�����", DbSql.GetValueBool(reader, "�����"));
			element.SetData("�����", DbSql.GetValueBool(reader, "�����"));

			element.SetData("����_�����", DbSql.GetValueFloat(reader, "����_�����"));
			element.SetData("�������_�����", DbSql.GetValueFloat(reader, "�������_�����"));
			element.SetData("�����������_�����", DbSql.GetValueFloat(reader, "�����������_�����"));
			element.SetData("����_���������", DbSql.GetValueFloat(reader, "����_���������"));
			element.SetData("����_������_������", DbSql.GetValueFloat(reader, "����_������_������"));
			element.SetData("����_������_�������", DbSql.GetValueFloat(reader, "����_������_�������"));
			element.SetData("����_������_�������", DbSql.GetValueFloat(reader, "����_������_�������"));
			element.SetData("����_������_����", DbSql.GetValueFloat(reader, "����_������_����"));

			return (object)element;
		}

		public static DtAutoSellServ Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code_sell"].Value	= (long)code;
			return (DtAutoSellServ)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
