using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AutoBaseSql
{
	/*
	/// <summary>
	/// Summary description for DbSqlCardWorkend.
	/// </summary>
	public class DbSqlCardWorkend
	{
		public static SqlCommand insert;
		public static SqlCommand select_interval;
		public static SqlCommand find;

		public DbSqlCardWorkend(SqlConnection connection)
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("��������_���������_�������_����������", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			select_interval = new SqlCommand("��������_���������_�������_�����_��������", connection);
			select_interval.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("��������_���������_�������_�����", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

		}

		public static DtCardWorkend Insert(DtCardWorkend element)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@card_number"].Value			= (long)element.GetData("������_��������_�����");
			insert.Parameters["@card_year"].Value			= (int)element.GetData("������_��������_���");
			insert.Parameters["@date"].Value				= (DateTime)element.GetData("����_���������_�������");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardWorkend element			= new DtCardWorkend();
			element.SetData("������_��������_�����", DbSql.GetValueLong(reader, "������_��������_�����"));
			element.SetData("������_��������_���", DbSql.GetValueInt(reader, "������_��������_���"));
			element.SetData("����_���������_�������", DbSql.GetValueDate(reader, "����_���������_�������"));
			return (object)element;
		}

		public static void SelectArrayInterval(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select_interval, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardWorkend Find(long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value			= (long)card_number;
			find.Parameters["@card_year"].Value			= (int)card_year;
			return (DtCardWorkend) DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			
		}

	}
	*/
}
