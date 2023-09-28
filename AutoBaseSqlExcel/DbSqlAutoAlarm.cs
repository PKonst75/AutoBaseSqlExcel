using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoAlarm.
	/// </summary>
	public class DbSqlAutoAlarm
	{
		public static SqlCommand		insert;
		public static SqlCommand		find_last;
	
		public DbSqlAutoAlarm()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("����������_������������_����������", connection);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@flag", SqlDbType.BigInt);
			insert.Parameters.Add("@code_alarm", SqlDbType.BigInt);
			insert.Parameters.Add("@code_service", SqlDbType.BigInt);
			insert.Parameters.Add("@code_reason", SqlDbType.BigInt);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@version", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@version"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find_last = new SqlCommand("����������_������������_�����_���������", connection);
			find_last.Parameters.Add("@code_auto", SqlDbType.BigInt);
			find_last.CommandType = CommandType.StoredProcedure;
		}

		public static bool Insert(DtAutoAlarm element)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@code_auto"].Value			= (long)element.GetData("���_����������");
			insert.Parameters["@flag"].Value				= (long)element.GetData("����_���������");
			insert.Parameters["@code_alarm"].Value			= (long)element.GetData("���_������������");
			insert.Parameters["@code_service"].Value		= (long)element.GetData("���_������");
			insert.Parameters["@code_reason"].Value			= (long)element.GetData("���_�������");
			insert.Parameters["@date"].Value				= (DateTime)element.GetData("����_���������");
			
			if(DbSql.ExecuteCommandError(insert) == false) return false;

			element.SetData("������", insert.Parameters["@version"].Value);
			return true;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoAlarm element			= new DtAutoAlarm();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("����_���������", DbSql.GetValueLong(reader, "����_���������"));
			element.SetData("���_������������", DbSql.GetValueLong(reader, "���_������������"));
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("���_�������", DbSql.GetValueLong(reader, "���_�������"));
			element.SetData("����_���������", DbSql.GetValueDate(reader, "����_���������"));
			element.SetData("������", DbSql.GetValueLong(reader, "������"));
			
			return (object)element;
		}

		public static DtAutoAlarm FindLast(long code_auto)
		{
			find_last.Parameters["@code_auto"].Value	= (long)code_auto;
			return (DtAutoAlarm)DbSql.Find(find_last, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
