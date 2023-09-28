using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for V1_DbSqlCardRate.
	/// </summary>
	public class V1_DbSqlCardRate
	{
		public static SqlCommand		insert;
		public static SqlCommand		insert_call;

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("КАРТОЧКА_ОЦЕНКА_ДОБАВЛЕНИЕ", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@rate", SqlDbType.SmallInt);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@rate_change", SqlDbType.Bit);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			insert_call = new SqlCommand("КАРТОЧКА_ОЦЕНКА_ОБЗВОН_ДОБАВЛЕНИЕ", connection);
			insert_call.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert_call.Parameters.Add("@card_year", SqlDbType.Int);
			insert_call.Parameters.Add("@call_date", SqlDbType.DateTime);
			insert_call.Parameters.Add("@rate", SqlDbType.SmallInt);
			insert_call.Parameters.Add("@comment", SqlDbType.VarChar);
			insert_call.Parameters.Add("@work_done", SqlDbType.Bit);
			insert_call.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_call);
		}

		public V1_DbSqlCardRate()
		{
		}

		public static bool Insert(V1_DtCardRate element)
		{
			// Добавляем в базу данные выходной анкеты
			insert.Parameters["@card_number"].Value			= (long)element.card_number;
			insert.Parameters["@card_year"].Value			= (int)element.card_year;
			insert.Parameters["@comment"].Value				= (string)element.comment;
			insert.Parameters["@rate"].Value				= (short)element.rate;
			insert.Parameters["@rate_change"].Value			= (bool)element.rate_change;
			
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			return true;
		}

		public static bool InsertCall(V1_DtCardRateCall element)
		{
			// Добавляем в базу данные выходной анкеты
			insert_call.Parameters["@card_number"].Value		= (long)element.card_number;
			insert_call.Parameters["@card_year"].Value			= (int)element.card_year;
			insert_call.Parameters["@comment"].Value			= (string)element.comment;
			insert_call.Parameters["@rate"].Value				= (short)element.rate;
			insert_call.Parameters["@work_done"].Value			= (bool)element.work_done;
			insert_call.Parameters["@call_date"].Value			= (DateTime)element.call_date;
			
			if(DbSql.ExecuteCommandError(insert_call) == false) return false;
			return true;
		}
	}
}
