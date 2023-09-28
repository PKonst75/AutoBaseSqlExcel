using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerContact.
	/// </summary>
	public class DbSqlPartnerContact
	{
		public static SqlCommand select;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;

		public DbSqlPartnerContact()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@code_partner", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code_partner", SqlDbType.BigInt);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@type", SqlDbType.VarChar);
			insert.Parameters.Add("@sort", SqlDbType.VarChar);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code_partner", SqlDbType.BigInt);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@sort", SqlDbType.VarChar);
			update.Parameters.Add("@contact", SqlDbType.VarChar);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code_partner", SqlDbType.BigInt);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartnerContact element	= new DtPartnerContact();
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ"));
			element.SetData("ÊÎÄ_ÊÎÍÒÀÊÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎÍÒÀÊÒ"));
			element.SetData("ÒÈÏ_ÊÎÍÒÀÊÒ", DbSql.GetValueString(reader, "ÒÈÏ_ÊÎÍÒÀÊÒ"));
			element.SetData("ÂÈÄ_ÊÎÍÒÀÊÒ", DbSql.GetValueString(reader, "ÂÈÄ_ÊÎÍÒÀÊÒ"));
			element.SetData("ÊÎÍÒÀÊÒ", DbSql.GetValueString(reader, "ÊÎÍÒÀÊÒ"));
			element.SetData("ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ", DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtPartnerContact element = (DtPartnerContact)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_partner)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_partner"].Value = (long)code_partner;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, long code_partner)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_partner"].Value = (long)code_partner;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtPartnerContact Find(long code_partner, long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code_partner"].Value	= (long)code_partner;
			find.Parameters["@code"].Value			= (long)code;
			return (DtPartnerContact)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtPartnerContact Insert(DtPartnerContact element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_partner"].Value	= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ");
			insert.Parameters["@type"].Value			= (string)element.GetData("ÒÈÏ_ÊÎÍÒÀÊÒ");
			insert.Parameters["@sort"].Value			= (string)element.GetData("ÂÈÄ_ÊÎÍÒÀÊÒ");
			insert.Parameters["@contact"].Value			= (string)element.GetData("ÊÎÍÒÀÊÒ");
			insert.Parameters["@comment"].Value			= (string)element.GetData("ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("ÊÎÄ_ÊÎÍÒÀÊÒ",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtPartnerContact element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update.Parameters["@code_partner"].Value	= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ");
			update.Parameters["@code"].Value			= (long)element.GetData("ÊÎÄ_ÊÎÍÒÀÊÒ");
			update.Parameters["@sort"].Value			= (string)element.GetData("ÂÈÄ_ÊÎÍÒÀÊÒ");
			update.Parameters["@contact"].Value			= (string)element.GetData("ÊÎÍÒÀÊÒ");
			update.Parameters["@comment"].Value			= (string)element.GetData("ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ");
			if(DbSql.ExecuteCommandError(update)== false) return false;
			return true;
		}

		public static bool Delete(long code_partner, long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			delete.Parameters["@code_partner"].Value		= (long)code_partner;
			delete.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}
	}
}
