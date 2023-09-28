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
			insert = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÎÊÎÍ×ÀÍÈÅ_ĞÅÌÎÍÒÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			select_interval = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÎÊÎÍ×ÀÍÈÅ_ĞÅÌÎÍÒÀ_ÂÛÁÎĞ_ÈÍÒÅĞÂÀË", connection);
			select_interval.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÎÊÎÍ×ÀÍÈÅ_ĞÅÌÎÍÒÀ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

		}

		public static DtCardWorkend Insert(DtCardWorkend element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@card_number"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ");
			insert.Parameters["@card_year"].Value			= (int)element.GetData("ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÃÎÄ");
			insert.Parameters["@date"].Value				= (DateTime)element.GetData("ÄÀÒÀ_ÎÊÎÍ×ÀÍÈß_ĞÅÌÎÍÒÀ");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardWorkend element			= new DtCardWorkend();
			element.SetData("ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ"));
			element.SetData("ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÃÎÄ", DbSql.GetValueInt(reader, "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÃÎÄ"));
			element.SetData("ÄÀÒÀ_ÎÊÎÍ×ÀÍÈß_ĞÅÌÎÍÒÀ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÎÊÎÍ×ÀÍÈß_ĞÅÌÎÍÒÀ"));
			return (object)element;
		}

		public static void SelectArrayInterval(ArrayList array)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillArray(array, select_interval, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardWorkend Find(long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@card_number"].Value			= (long)card_number;
			find.Parameters["@card_year"].Value			= (int)card_year;
			return (DtCardWorkend) DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			
		}

	}
	*/
}
