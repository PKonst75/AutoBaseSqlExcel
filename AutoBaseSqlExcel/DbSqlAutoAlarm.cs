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
			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÑÈÃÍÀËÈÇÀÖÈß_ÄÎÁÀÂËÅÍÈÅ", connection);
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

			find_last = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÑÈÃÍÀËÈÇÀÖÈß_ÏÎÈÑÊ_ÏÎÑËÅÄÍÈÉ", connection);
			find_last.Parameters.Add("@code_auto", SqlDbType.BigInt);
			find_last.CommandType = CommandType.StoredProcedure;
		}

		public static bool Insert(DtAutoAlarm element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@code_auto"].Value			= (long)element.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ");
			insert.Parameters["@flag"].Value				= (long)element.GetData("ÔËÀÃ_ÓÑÒÀÍÎÂÊÀ");
			insert.Parameters["@code_alarm"].Value			= (long)element.GetData("ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß");
			insert.Parameters["@code_service"].Value		= (long)element.GetData("ÊÎÄ_ÑÅĞÂÈÑ");
			insert.Parameters["@code_reason"].Value			= (long)element.GetData("ÊÎÄ_ÏĞÈ×ÈÍÀ");
			insert.Parameters["@date"].Value				= (DateTime)element.GetData("ÄÀÒÀ_ÓÑÒÀÍÎÂÊÀ");
			
			if(DbSql.ExecuteCommandError(insert) == false) return false;

			element.SetData("ÂÅĞÑÈß", insert.Parameters["@version"].Value);
			return true;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoAlarm element			= new DtAutoAlarm();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÔËÀÃ_ÓÑÒÀÍÎÂÊÀ", DbSql.GetValueLong(reader, "ÔËÀÃ_ÓÑÒÀÍÎÂÊÀ"));
			element.SetData("ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß"));
			element.SetData("ÊÎÄ_ÑÅĞÂÈÑ", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÅĞÂÈÑ"));
			element.SetData("ÊÎÄ_ÏĞÈ×ÈÍÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏĞÈ×ÈÍÀ"));
			element.SetData("ÄÀÒÀ_ÓÑÒÀÍÎÂÊÀ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÓÑÒÀÍÎÂÊÀ"));
			element.SetData("ÂÅĞÑÈß", DbSql.GetValueLong(reader, "ÂÅĞÑÈß"));
			
			return (object)element;
		}

		public static DtAutoAlarm FindLast(long code_auto)
		{
			find_last.Parameters["@code_auto"].Value	= (long)code_auto;
			return (DtAutoAlarm)DbSql.Find(find_last, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
