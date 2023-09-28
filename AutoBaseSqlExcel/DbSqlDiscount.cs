using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlDiscount.
	/// </summary>
	public class DbSqlDiscount
	{
		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		insert;
		public static SqlCommand		update;
		public static SqlCommand		give;
		public static SqlCommand		find_partner;

		public DbSqlDiscount()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÄÈÑÊÎÍÒ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@code_from", SqlDbType.BigInt);
			select.Parameters.Add("@code_to", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÄÈÑÊÎÍÒ_ÏÎÈÑÊ_ÊÎÄ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_partner = new SqlCommand("ÄÈÑÊÎÍÒ_ÏÎÈÑÊ_ÊÎÍÒĞÀÃÅÍÒ", connection);
			find_partner.Parameters.Add("@code_partner", SqlDbType.BigInt);
			find_partner.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÄÈÑÊÎÍÒ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@discount_service_work", SqlDbType.Real);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("ÄÈÑÊÎÍÒ_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@discount_service_work", SqlDbType.Real);
			update.Parameters.Add("@code_partner", SqlDbType.BigInt);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			give = new SqlCommand("ÄÈÑÊÎÍÒ_ÂÛÄÀÒÜ", connection);
			give.Parameters.Add("@code", SqlDbType.BigInt);
			give.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(give);
		}

		public static bool Insert(float discount)
		{
			insert.Parameters["@discount_service_work"].Value			= (float)discount;
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			return true;
		}

		public static bool Give(long code)
		{
			give.Parameters["@code"].Value	= (long)code;
			if(DbSql.ExecuteCommandError(give) == false) return false;
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtDiscount element = (DtDiscount)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Îøèáêà";
			}
			return item;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtDiscount element			= new DtDiscount();
			element.SetData("ÊÎÄ_ÄÈÑÊÎÍÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÄÈÑÊÎÍÒ"));
			element.SetData("ÑÊÈÄÊÀ_ÑÅĞÂÈÑ_ĞÀÁÎÒÀ_ÄÈÑÊÎÍÒ", DbSql.GetValueFloat(reader, "ÑÊÈÄÊÀ_ÑÅĞÂÈÑ_ĞÀÁÎÒÀ_ÄÈÑÊÎÍÒ"));
			element.SetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÄÈÑÊÎÍÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÄÈÑÊÎÍÒ"));
			element.SetData("ÔËÀÃ_ÂÛÄÀÍÎ_ÄÈÑÊÎÍÒ", DbSql.GetValueBool(reader, "ÔËÀÃ_ÂÛÄÀÍÎ_ÄÈÑÊÎÍÒ"));
			element.SetData("ÏĞÈÌÅ×ÀÍÈÅ_ÄÈÑÊÎÍÒ", DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ_ÄÈÑÊÎÍÒ"));
			element.SetData("ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			return (object)element;
		}

		public static void PrepareSelect(long code_from, long code_to)
		{
			select.Parameters["@code_from"].Value = (long)code_from;
			select.Parameters["@code_to"].Value = (long)code_to;
		}

		public static DtDiscount Find(long code)
		{
			find.Parameters["@code"].Value		= (long)code;
			return(DtDiscount)DbSql.Find(find, new DbSql.DelegateMakeElement(DbSqlDiscount.MakeElement));
		}

		public static DtDiscount FindPartner(long code)
		{
			find_partner.Parameters["@code_partner"].Value		= (long)code;
			return(DtDiscount)DbSql.Find(find_partner, new DbSql.DelegateMakeElement(DbSqlDiscount.MakeElement));
		}

		public static bool Update(DtDiscount element)
		{
			update.Parameters["@code"].Value					= (long)element.GetData("ÊÎÄ_ÄÈÑÊÎÍÒ");
			update.Parameters["@discount_service_work"].Value	= (float)element.GetData("ÑÊÈÄÊÀ_ÑÅĞÂÈÑ_ĞÀÁÎÒÀ_ÄÈÑÊÎÍÒ");
			update.Parameters["@code_partner"].Value			= (long)element.GetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÄÈÑÊÎÍÒ");
			update.Parameters["@comment"].Value					= (string)element.GetData("ÏĞÈÌÅ×ÀÍÈÅ_ÄÈÑÊÎÍÒ");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			return true;
		}

	}
}
