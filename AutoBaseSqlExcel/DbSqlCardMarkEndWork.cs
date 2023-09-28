using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardMarkEndWork.
	/// </summary>
	public class DbSqlCardMarkEndWork
	{
		public static SqlCommand insert;
		public static SqlCommand find;

		public static SqlCommand auxiliary_insert;
		public static SqlCommand auxiliary_update;

		public DbSqlCardMarkEndWork()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("��������_�������_���������_�������_����������", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@date"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find = new SqlCommand("��������_�������_���������_�������_�����", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

			auxiliary_insert = new SqlCommand("��������_�������_���������_�������_����������_���������", connection);
			auxiliary_insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			auxiliary_insert.Parameters.Add("@card_year", SqlDbType.Int);
			auxiliary_insert.Parameters.Add("@date", SqlDbType.DateTime);
			auxiliary_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_insert);

			auxiliary_update = new SqlCommand("��������_�������_���������_�������_���������_���������", connection);
			auxiliary_update.Parameters.Add("@card_number", SqlDbType.BigInt);
			auxiliary_update.Parameters.Add("@card_year", SqlDbType.Int);
			auxiliary_update.Parameters.Add("@date", SqlDbType.DateTime);
			auxiliary_update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_update);
		}

		public static DtCardMarkEndWork Insert(DtCard card)
		{
			// ���������� ������� ������ �� �����
			DtCardMarkEndWork element = new DtCardMarkEndWork((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
			insert.Parameters["@card_number"].Value			= (long)card.GetData("�����_��������");
			insert.Parameters["@card_year"].Value			= (int)card.GetData("���_��������");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("����", (DateTime)insert.Parameters["@date"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardMarkEndWork element			= new DtCardMarkEndWork();
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			return (object)element;
		}

		public static DtCardMarkEndWork Find(long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card_number;
			find.Parameters["@card_year"].Value = (int)card_year;
			return (DtCardMarkEndWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardMarkEndWork Find(DtCard card)
		{
			// ���������� ������� ������ �� �����
			return Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
		}

		public static bool AuxiliaryInsert(long card_number, int card_year, DateTime date)
		{
			// ���������� ������� ������ �� �����
			auxiliary_insert.Parameters["@card_number"].Value		= (long)card_number;
			auxiliary_insert.Parameters["@card_year"].Value			= (int)card_year;
			auxiliary_insert.Parameters["@date"].Value				= (DateTime)date;
			return DbSql.ExecuteCommandError(auxiliary_insert);
		}
		public static bool AuxiliaryUpdate(long card_number, int card_year, DateTime date)
		{
			// ���������� ������� ������ �� �����
			auxiliary_update.Parameters["@card_number"].Value		= (long)card_number;
			auxiliary_update.Parameters["@card_year"].Value			= (int)card_year;
			auxiliary_update.Parameters["@date"].Value				= (DateTime)date;
			return DbSql.ExecuteCommandError(auxiliary_update);
		}
	}
}
