using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlInvoice.
	/// </summary>
	public class DbSqlInvoice
	{
		public static SqlCommand		insert;
		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		delete;
		public static SqlCommand		select_cards;
		public static SqlCommand		insert_card;
		public static SqlCommand		insert_pay;
		public static SqlCommand		select_pay;

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@number_buhg", SqlDbType.VarChar);
			insert.Parameters.Add("@date_buhg", SqlDbType.DateTime);
			insert.Parameters.Add("@summ", SqlDbType.Real);
			insert.Parameters.Add("@type", SqlDbType.Int);
			insert.Parameters.Add("@date_controll_green", SqlDbType.DateTime);
			insert.Parameters.Add("@date_controll_yellow", SqlDbType.DateTime);
			insert.Parameters.Add("@date_controll_red", SqlDbType.DateTime);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.Parameters["@year"].Direction = ParameterDirection.Output;
			insert.Parameters["@date"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÂÛÁÎĞÊÀ_ÍÅÎÏËÀ×ÅÍÍÛÅ", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

			delete = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.Parameters.Add("@year", SqlDbType.Int);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			select_cards = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÊÀĞÒÎ×ÊÀ_ÂÛÁÎĞÊÀ", connection);
			select_cards.Parameters.Add("@code", SqlDbType.BigInt);
			select_cards.Parameters.Add("@year", SqlDbType.Int);
			select_cards.CommandType = CommandType.StoredProcedure;

			select_pay = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ", connection);
			select_pay.Parameters.Add("@code", SqlDbType.BigInt);
			select_pay.Parameters.Add("@year", SqlDbType.Int);
			select_pay.CommandType = CommandType.StoredProcedure;

			insert_card = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÊÀĞÒÎ×ÊÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert_card.Parameters.Add("@code", SqlDbType.BigInt);
			insert_card.Parameters.Add("@year", SqlDbType.Int);
			insert_card.Parameters.Add("@type", SqlDbType.Int);
			insert_card.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert_card.Parameters.Add("@card_year", SqlDbType.Int);
			insert_card.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_card);

			insert_pay = new SqlCommand("Ñ×ÅÒ_ÁÅÇÍÀËÈ×ÍÛÉ_ÎÏËÀÒÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert_pay.Parameters.Add("@code", SqlDbType.BigInt);
			insert_pay.Parameters.Add("@year", SqlDbType.Int);
			insert_pay.Parameters.Add("@type", SqlDbType.Int);
			insert_pay.Parameters.Add("@date", SqlDbType.DateTime);
			insert_pay.Parameters.Add("@invoice_code", SqlDbType.BigInt);
			insert_pay.Parameters.Add("@invoice_year", SqlDbType.Int);
			insert_pay.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_pay);
		}

		public DbSqlInvoice()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DtInvoice Insert(DtInvoice element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@code_partner"].Value		= (long)element.code_partner;
			insert.Parameters["@comment"].Value				= (string)element.comment;
			insert.Parameters["@number_buhg"].Value			= (string)element.number_buhg;
			insert.Parameters["@date_buhg"].Value			= (DateTime)element.date_buhg;
			insert.Parameters["@summ"].Value				= (float)element.summ;
			insert.Parameters["@type"].Value				= (int)element.type;
			insert.Parameters["@date_controll_green"].Value	= (DateTime)element.date_controll_green;
			insert.Parameters["@date_controll_yellow"].Value= (DateTime)element.date_controll_yellow;
			insert.Parameters["@date_controll_red"].Value	= (DateTime)element.date_controll_red;
			insert.Parameters["@card_number"].Value			= (long)element.card_number;
			insert.Parameters["@card_year"].Value			= (int)element.card_year;
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.code		=  (long)(insert.Parameters["@code"].Value);
			element.year		=  (int)(insert.Parameters["@year"].Value);
			element.date		=  (DateTime)(insert.Parameters["@date"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtInvoice	element				= new DtInvoice();
			element.code					= (long)DbSql.GetValueLong(reader, "ÊÎÄ");
			element.year					= (int)DbSql.GetValueInt(reader, "ÃÎÄ");
			element.date					= (DateTime)DbSql.GetValueDate(reader, "ÄÀÒÀ");
			element.comment					= (string)DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ");
			element.code_partner			= (long)DbSql.GetValueLong(reader, "ÊÎÍÒĞÀÃÅÍÒ");
			element.number_buhg				= (string)DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÁÓÕÃÀËÒÅĞÑÊÈÉ");
			element.date_buhg				= (DateTime)DbSql.GetValueDate(reader, "ÄÀÒÀ_ÁÓÕÃÀËÒÅĞÑÊÀß");
			element.summ					= (float)DbSql.GetValueFloat(reader, "ÑÓÌÌÀ");
			element.type					= (int)DbSql.GetValueInt(reader, "ÒÈÏ");
			element.date_controll_green		= (DateTime)DbSql.GetValueDate(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÎÏËÀÒÛ_ÇÅËÅÍÀß");
			element.date_controll_yellow	= (DateTime)DbSql.GetValueDate(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÎÏËÀÒÛ_ÆÅËÒÀß");
			element.date_controll_red		= (DateTime)DbSql.GetValueDate(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÎÏËÀÒÛ_ÊĞÀÑÍÀß");
			element.pay						= (bool)DbSql.GetValueBool(reader, "ÎÏËÀ×ÅÍ");
			element.date_pay				= (DateTime)DbSql.GetValueDate(reader, "ÄÀÒÀ_ÎÏËÀÒÛ");
			element.comment_unpay			= (string)DbSql.GetValueString(reader, "ÏĞÈ×ÈÍÀ_ÍÅÏËÀÒÅÆÀ");
			element.pay_delay				= (int)DbSql.GetValueInt(reader, "ÇÀÄÅĞÆÊÀ_ÎÏËÀÒÛ");
			element.tmp_partner_name		= (string)DbSql.GetValueString(reader, "ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ");

			return (object)element;
		}

		public static object MakeElementCard(SqlDataReader reader)
		{
			DtCard.Pair	element				= new DtCard.Pair();
			element.number					= (long)DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			element.year					= (int)DbSql.GetValueInt(reader, "ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			
			return (object)element;
		}

		public static void SelectInArray(ArrayList array)
		{
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayCards(long code, int year, ArrayList array)
		{
			select_cards.Parameters["@code"].Value	= (long)code;
			select_cards.Parameters["@year"].Value	= (int)year;
			DbSql.FillArray(array, select_cards, new DbSql.DelegateMakeElement(MakeElementCard));
		}

		public static void SelectInArrayPay(long code, int year, ArrayList array)
		{
			select_pay.Parameters["@code"].Value	= (long)code;
			select_pay.Parameters["@year"].Value	= (int)year;
			DbSql.FillArray(array, select_pay, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtInvoice Find(long code, int year)
		{
			find.Parameters["@code"].Value	= (long)code;
			find.Parameters["@year"].Value	= (int)year;
			return (DtInvoice)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool Delete(long code, int year)
		{
			delete.Parameters["@code"].Value	= (long)code;
			delete.Parameters["@year"].Value	= (int)year;
			return DbSql.ExecuteCommandError(delete);
		}

		public static bool InsertCard(DtInvoice element, long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert_card.Parameters["@code"].Value				= (long)element.code;
			insert_card.Parameters["@year"].Value				= (int)element.year;
			insert_card.Parameters["@type"].Value				= (int)element.type;
			insert_card.Parameters["@card_number"].Value		= (long)card_number;
			insert_card.Parameters["@card_year"].Value			= (int)card_year;
			return DbSql.ExecuteCommandError(insert_card);
		}

		public static bool InsertPay(DtInvoicePay pay, long invoice_code, int invoice_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert_pay.Parameters["@code"].Value				= (long)pay.code;
			insert_pay.Parameters["@year"].Value				= (int)pay.year;
			insert_pay.Parameters["@type"].Value				= (int)pay.type;
			insert_pay.Parameters["@date"].Value				= (DateTime)pay.date;
			insert_pay.Parameters["@invoice_code"].Value		= (long)invoice_code;
			insert_pay.Parameters["@invoice_year"].Value		= (int)invoice_year;
			return DbSql.ExecuteCommandError(insert_pay);
		}
	}
}
