using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoSellServ.
	/// </summary>
	public class DbSqlAutoSellServ
	{
		public static SqlCommand	insert;
		public static SqlCommand	find;

		public DbSqlAutoSellServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			find = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏĞÎÄÀÆÀ_ÑËÓÆÅÁÍÎÅ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code_sell", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏĞÎÄÀÆÀ_ÑËÓÆÅÁÍÎÅ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code_sell", SqlDbType.BigInt);
			insert.Parameters.Add("@code_manager", SqlDbType.BigInt);
			insert.Parameters.Add("@flag_music", SqlDbType.Bit);
			insert.Parameters.Add("@flag_alarm", SqlDbType.Bit);
			insert.Parameters.Add("@flag_tune", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti1", SqlDbType.Bit);
			insert.Parameters.Add("@flag_anti2", SqlDbType.Bit);
			insert.Parameters.Add("@flag_other", SqlDbType.Bit);
			insert.Parameters.Add("@flag_gibdd", SqlDbType.Bit);
			insert.Parameters.Add("@flag_sprav", SqlDbType.Bit);
			insert.Parameters.Add("@flag_kasko", SqlDbType.Bit);
			insert.Parameters.Add("@flag_osago", SqlDbType.Bit);
			insert.Parameters.Add("@summ_whole", SqlDbType.Float);
			insert.Parameters.Add("@summ_anti", SqlDbType.Float);
			insert.Parameters.Add("@summ_sprav", SqlDbType.Float);
			insert.Parameters.Add("@auto_summ", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_money", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_other", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_anti", SqlDbType.Float);
			insert.Parameters.Add("@auto_discount_tunemus", SqlDbType.Float);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);
		}

		public static bool Insert(DtAutoSellServ sellserv)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_sell"].Value		= (long)sellserv.GetData("ÊÎÄ_ÏĞÎÄÀÆÀ");
			insert.Parameters["@code_manager"].Value	= (long)sellserv.GetData("ÌÅÍÅÄÆÅĞ");
			insert.Parameters["@flag_music"].Value		= (bool)sellserv.GetData("ÌÓÇÛÊÀ");
			insert.Parameters["@flag_alarm"].Value		= (bool)sellserv.GetData("ÑÈÃÍÀËÈÇÀÖÈß");
			insert.Parameters["@flag_tune"].Value		= (bool)sellserv.GetData("ÒŞÍÈÍÃ");
			insert.Parameters["@flag_anti"].Value		= (bool)sellserv.GetData("ÀÍÒÈÊÎĞ");
			insert.Parameters["@flag_anti1"].Value		= (bool)sellserv.GetData("ÏÎÄÊĞÛËÊÈ");
			insert.Parameters["@flag_anti2"].Value		= (bool)sellserv.GetData("ÇÀÙÈÒÀ");
			insert.Parameters["@flag_other"].Value		= (bool)sellserv.GetData("ÀÊÑÅÑÑÓÀĞÛ");
			insert.Parameters["@flag_gibdd"].Value		= (bool)sellserv.GetData("ÃÈÁÄÄ");
			insert.Parameters["@flag_sprav"].Value		= (bool)sellserv.GetData("ÑÏĞÀÂÊÀÑ×ÅÒ");
			insert.Parameters["@flag_kasko"].Value		= (bool)sellserv.GetData("ÊÀÑÊÎ");
			insert.Parameters["@flag_osago"].Value		= (bool)sellserv.GetData("ÎÑÀÃÎ");

			insert.Parameters["@summ_whole"].Value			= (float)sellserv.GetData("ÄÎÏÛ_ÑÓÌÌÀ");
			insert.Parameters["@summ_anti"].Value			= (float)sellserv.GetData("ÀÍÒÈÊÎĞ_ÑÓÌÌÀ");
			insert.Parameters["@summ_sprav"].Value			= (float)sellserv.GetData("ÑÏĞÀÂÊÀÑ×ÅÒ_ÑÓÌÌÀ");
			insert.Parameters["@auto_summ"].Value			= (float)sellserv.GetData("ÀÂÒÎ_ÑÒÎÈÌÎÑÒÜ");
			insert.Parameters["@auto_discount_money"].Value	= (float)sellserv.GetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÅÍÜÃÈ");
			insert.Parameters["@auto_discount_other"].Value	= (float)sellserv.GetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÏÎÄÀĞÎÊ");
			insert.Parameters["@auto_discount_anti"].Value	= (float)sellserv.GetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÀÍÒÈÊÎĞ");
			insert.Parameters["@auto_discount_tunemus"].Value	= (float)sellserv.GetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÎÏÛ");
			
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			return true;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoSellServ element			= new DtAutoSellServ();
			element.SetData("ÊÎÄ_ÏĞÎÄÀÆÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏĞÎÄÀÆÀ"));
			element.SetData("ÌÅÍÅÄÆÅĞ", DbSql.GetValueLong(reader, "ÌÅÍÅÄÆÅĞ"));

			element.SetData("ÌÓÇÛÊÀ", DbSql.GetValueBool(reader, "ÌÓÇÛÊÀ"));
			element.SetData("ÑÈÃÍÀËÈÇÀÖÈß", DbSql.GetValueBool(reader, "ÑÈÃÍÀËÈÇÀÖÈß"));
			element.SetData("ÒŞÍÈÍÃ", DbSql.GetValueBool(reader, "ÒŞÍÈÍÃ"));
			element.SetData("ÀÍÒÈÊÎĞ", DbSql.GetValueBool(reader, "ÀÍÒÈÊÎĞ"));
			element.SetData("ÏÎÄÊĞÛËÊÈ", DbSql.GetValueBool(reader, "ÏÎÄÊĞÛËÊÈ"));
			element.SetData("ÇÀÙÈÒÀ", DbSql.GetValueBool(reader, "ÇÀÙÈÒÀ"));
			element.SetData("ÀÊÑÅÑÑÓÀĞÛ", DbSql.GetValueBool(reader, "ÀÊÑÅÑÑÓÀĞÛ"));
			element.SetData("ÃÈÁÄÄ", DbSql.GetValueBool(reader, "ÃÈÁÄÄ"));
			element.SetData("ÑÏĞÀÂÊÀÑ×ÅÒ", DbSql.GetValueBool(reader, "ÑÏĞÀÂÊÀÑ×ÅÒ"));
			element.SetData("ÊÀÑÊÎ", DbSql.GetValueBool(reader, "ÊÀÑÊÎ"));
			element.SetData("ÎÑÀÃÎ", DbSql.GetValueBool(reader, "ÎÑÀÃÎ"));

			element.SetData("ÄÎÏÛ_ÑÓÌÌÀ", DbSql.GetValueFloat(reader, "ÄÎÏÛ_ÑÓÌÌÀ"));
			element.SetData("ÀÍÒÈÊÎĞ_ÑÓÌÌÀ", DbSql.GetValueFloat(reader, "ÀÍÒÈÊÎĞ_ÑÓÌÌÀ"));
			element.SetData("ÑÏĞÀÂÊÀÑ×ÅÒ_ÑÓÌÌÀ", DbSql.GetValueFloat(reader, "ÑÏĞÀÂÊÀÑ×ÅÒ_ÑÓÌÌÀ"));
			element.SetData("ÀÂÒÎ_ÑÒÎÈÌÎÑÒÜ", DbSql.GetValueFloat(reader, "ÀÂÒÎ_ÑÒÎÈÌÎÑÒÜ"));
			element.SetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÅÍÜÃÈ", DbSql.GetValueFloat(reader, "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÅÍÜÃÈ"));
			element.SetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÏÎÄÀĞÎÊ", DbSql.GetValueFloat(reader, "ÀÂÒÎ_ÑÊÈÄÊÀ_ÏÎÄÀĞÎÊ"));
			element.SetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÀÍÒÈÊÎĞ", DbSql.GetValueFloat(reader, "ÀÂÒÎ_ÑÊÈÄÊÀ_ÀÍÒÈÊÎĞ"));
			element.SetData("ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÎÏÛ", DbSql.GetValueFloat(reader, "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÎÏÛ"));

			return (object)element;
		}

		public static DtAutoSellServ Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code_sell"].Value	= (long)code;
			return (DtAutoSellServ)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
