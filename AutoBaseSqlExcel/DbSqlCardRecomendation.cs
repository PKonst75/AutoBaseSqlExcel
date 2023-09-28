using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardRecomendation.
	/// </summary>
	public class DbSqlCardRecomendation
	{
		public static SqlCommand select;

		public DbSqlCardRecomendation()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ĞÅÊÎÌÅÍÄÀÖÈß_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardRecomendation element			= new DtCardRecomendation();
			element.SetData("ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueInt(reader, "ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÍÎÌÅĞ_ĞÅÊÎÌÅÍÄÀÖÈß", DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ĞÅÊÎÌÅÍÄÀÖÈß"));
			element.SetData("ĞÅÊÎÌÅÍÄÀÖÈß", DbSql.GetValueString(reader, "ĞÅÊÎÌÅÍÄÀÖÈß"));
			return (object)element;
		}

		public static void SelectInArray(ArrayList array, long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
