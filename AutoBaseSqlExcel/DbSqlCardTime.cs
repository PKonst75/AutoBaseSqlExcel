using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardTime.
	/// </summary>
	public class DbSqlCardTime
	{
		public static SqlCommand		set_goin_time;
		public static SqlCommand		set_begin_time;
		public static SqlCommand		set_end_time;
		public static SqlCommand		set_goout_time;
		public static SqlCommand		set_notime;
		public static SqlCommand		find;

		public static SqlCommand		pause_set_begin_time;
		public static SqlCommand		pause_set_end_time;

		public DbSqlCardTime()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			find = new SqlCommand("��������_�����_�����", connection);
			find.Parameters.Add("@number", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

			set_notime = new SqlCommand("��������_�����_���������_�������", connection);
			set_notime.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_notime.Parameters.Add("@card_year", SqlDbType.Int);
			set_notime.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_notime);

			set_goin_time = new SqlCommand("��������_�����_���������_�����", connection);
			set_goin_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_goin_time.Parameters.Add("@card_year", SqlDbType.Int);
			set_goin_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_goin_time);

			set_begin_time = new SqlCommand("��������_�����_���������_������", connection);
			set_begin_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_begin_time.Parameters.Add("@card_year", SqlDbType.Int);
			set_begin_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_begin_time);

			set_end_time = new SqlCommand("��������_�����_���������_���������", connection);
			set_end_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_end_time.Parameters.Add("@card_year", SqlDbType.Int);
			set_end_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_end_time);

			set_goout_time = new SqlCommand("��������_�����_���������_�����", connection);
			set_goout_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_goout_time.Parameters.Add("@card_year", SqlDbType.Int);
			set_goout_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_goout_time);

			pause_set_begin_time = new SqlCommand("��������_�����_�����_���������_������", connection);
			pause_set_begin_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			pause_set_begin_time.Parameters.Add("@card_year", SqlDbType.Int);
			pause_set_begin_time.Parameters.Add("@reason", SqlDbType.BigInt);
			pause_set_begin_time.Parameters.Add("@goout", SqlDbType.Bit);
			pause_set_begin_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(pause_set_begin_time);

			pause_set_end_time = new SqlCommand("��������_�����_�����_���������_���������", connection);
			pause_set_end_time.Parameters.Add("@card_number", SqlDbType.BigInt);
			pause_set_end_time.Parameters.Add("@card_year", SqlDbType.Int);
			pause_set_end_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(pause_set_end_time);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardTime element = new DtCardTime();
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));	
			if(DbSql.IsValueNULL(reader, "�����_�����") == false)
			{
				element.SetData("�����_�����", DbSql.GetValueDate(reader, "�����_�����"));
				element.SetData("����_�����_�����", (bool)true);
			}
			if(DbSql.IsValueNULL(reader, "�����_������") == false)
			{
				element.SetData("�����_������", DbSql.GetValueDate(reader, "�����_������"));
				element.SetData("����_�����_������", (bool)true);
			}
			if(DbSql.IsValueNULL(reader, "�����_���������") == false)
			{
				element.SetData("�����_���������", DbSql.GetValueDate(reader, "�����_���������"));
				element.SetData("����_�����_���������", (bool)true);
			}
			if(DbSql.IsValueNULL(reader, "�����_�����") == false)
			{
				element.SetData("�����_�����", DbSql.GetValueDate(reader, "�����_�����"));
				element.SetData("����_�����_�����", (bool)true);
			}
			element.SetData("�������", DbSql.GetValueBool(reader, "�������"));
			element.SetData("�����", DbSql.GetValueBool(reader, "�����"));
			
			return (object)(DtCardTime)element;
		}

		public static DtCardTime Find(long number, int year)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@number"].Value = (long)number;
			find.Parameters["@year"].Value = (int)year;
			DtCardTime element = (DtCardTime)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool SetNoTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_notime.Parameters["@card_number"].Value = (long)card_number;
			set_notime.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(set_notime);
		}

		public static bool SetGoinTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_goin_time.Parameters["@card_number"].Value = (long)card_number;
			set_goin_time.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(set_goin_time);
		}

		public static bool SetBeginTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_begin_time.Parameters["@card_number"].Value = (long)card_number;
			set_begin_time.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(set_begin_time);
		}

		public static bool SetEndTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_end_time.Parameters["@card_number"].Value = (long)card_number;
			set_end_time.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(set_end_time);
		}

		public static bool SetGooutTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_goout_time.Parameters["@card_number"].Value = (long)card_number;
			set_goout_time.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(set_goout_time);
		}

		public static bool PauseSetBeginTime(long card_number, int card_year, long reason, bool goout)
		{
			// ��������� ������������ ������� ��������
			pause_set_begin_time.Parameters["@card_number"].Value = (long)card_number;
			pause_set_begin_time.Parameters["@card_year"].Value = (int)card_year;
			pause_set_begin_time.Parameters["@reason"].Value = (long)reason;
			pause_set_begin_time.Parameters["@goout"].Value = (bool)goout;
			return DbSql.ExecuteCommandError(pause_set_begin_time);
		}

		public static bool PauseSetEndTime(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			pause_set_end_time.Parameters["@card_number"].Value = (long)card_number;
			pause_set_end_time.Parameters["@card_year"].Value = (int)card_year;
			return DbSql.ExecuteCommandError(pause_set_end_time);
		}
	}
}
