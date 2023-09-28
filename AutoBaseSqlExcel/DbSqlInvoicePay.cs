using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlInvoicePay.
	/// </summary>
	public class DbSqlInvoicePay
	{
		public static SqlCommand		insert;
		public static SqlCommand		select;
		public static SqlCommand		find;
		
		public DbSqlInvoicePay()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÎÏËÀÒÀ_ÁÅÇÍÀËÈ×ÍÛÉ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@summ", SqlDbType.Real);
			insert.Parameters.Add("@type", SqlDbType.Int);
			insert.Parameters.Add("@number_pp", SqlDbType.VarChar);
			insert.Parameters.Add("@invoice_code", SqlDbType.BigInt);
			insert.Parameters.Add("@invoice_year", SqlDbType.Int);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.Parameters["@year"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("ÎÏËÀÒÀ_ÁÅÇÍÀËÈ×ÍÛÉ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÎÏËÀÒÀ_ÁÅÇÍÀËÈ×ÍÛÉ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static DtInvoicePay Insert(DtInvoicePay element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@code_partner"].Value		= (long)element.code_partner;
			insert.Parameters["@comment"].Value				= (string)element.comment;
			insert.Parameters["@date"].Value				= (DateTime)element.date;
			insert.Parameters["@summ"].Value				= (float)element.summ;
			insert.Parameters["@type"].Value				= (int)element.type;
			insert.Parameters["@number_pp"].Value			= (string)element.number_pp;
			insert.Parameters["@invoice_code"].Value		= (long)element.invoice_code;
			insert.Parameters["@invoice_year"].Value		= (int)element.invoice_year;
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.code		=  (long)(insert.Parameters["@code"].Value);
			element.year		=  (int)(insert.Parameters["@year"].Value);
			element.date		=  (DateTime)(insert.Parameters["@date"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtInvoicePay element			= new DtInvoicePay();
			element.code					= DbSql.GetValueLong(reader, "ÊÎÄ");
			element.year					= DbSql.GetValueInt(reader, "ÃÎÄ");
			element.date					= DbSql.GetValueDate(reader, "ÄÀÒÀ");
			element.code_partner			= DbSql.GetValueLong(reader, "ÊÎÍÒĞÀÃÅÍÒ");
			element.comment					= DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ");
			element.summ					= DbSql.GetValueFloat(reader, "ÑÓÌÌÀ");
			element.type					= DbSql.GetValueInt(reader, "ÒÈÏ");
			element.number_pp				= DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÏÏ");
			
			element.tmp_partner_name		= DbSql.GetValueString(reader, "ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			
			return (object)element;
		}

		public static void SelectInArray(ArrayList array)
		{
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtInvoicePay Find(long code, int year)
		{
			find.Parameters["@code"].Value	= (long)code;
			find.Parameters["@year"].Value	= (int)year;
			return (DtInvoicePay)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
