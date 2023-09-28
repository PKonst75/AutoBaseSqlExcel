using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerConnection.
	/// </summary>
	public class DbSqlPartnerConnection
	{
		public static SqlCommand select;
		public static SqlCommand select_date;
		public static SqlCommand select_contact;
		public static SqlCommand select_partner;
		public static SqlCommand insert;
		public static SqlCommand delete;

		public DbSqlPartnerConnection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_date = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÂÛÁÎĞÊÀ_ÄÀÒÀ", connection);
			select_date.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_date.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_date.CommandType = CommandType.StoredProcedure;

			select_contact = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÂÛÁÎĞÊÀ_ÊÎÍÒÀÊÒ", connection);
			select_contact.Parameters.Add("@contact", SqlDbType.VarChar);
			select_contact.CommandType = CommandType.StoredProcedure;

			select_partner = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÂÛÁÎĞÊÀ_ÊÎÍÒĞÀÃÅÍÒ", connection);
			select_partner.Parameters.Add("@code_partner", SqlDbType.BigInt);
			select_partner.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);
			insert.Parameters["@date"].Direction = ParameterDirection.Output;

			delete = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂßÇÜ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@contact", SqlDbType.VarChar);
			delete.Parameters.Add("@date", SqlDbType.DateTime);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartnerConnection element	= new DtPartnerConnection();
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
			element.SetData("ÄÀÒÀ", DbSql.GetValueDate(reader, "ÄÀÒÀ"));
			element.SetData("ÊÎÍÒÀÊÒ", DbSql.GetValueString(reader, "ÊÎÍÒÀÊÒ"));
			element.SetData("ÖÅËÜ", DbSql.GetValueString(reader, "ÖÅËÜ"));

			element.SetData("ÊÎÍÒĞÀÃÅÍÒ", DbSql.GetValueString(reader, "ÊÎÍÒĞÀÃÅÍÒ"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtPartnerConnection element = (DtPartnerConnection)MakeElement(reader);
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

		public static void SelectInList(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, DateTime start, DateTime end)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_date.Parameters["@start_date"].Value			= (DateTime)start;
			select_date.Parameters["@end_date"].Value			= (DateTime)end;
			DbSql.FillList(list, select_date, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string contact)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_contact.Parameters["@contact"].Value			= (string)contact;
			DbSql.FillList(list, select_contact, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, long code_partner)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_partner.Parameters["@code_partner"].Value			= (long)code_partner;
			DbSql.FillList(list, select_partner, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}


		public static DtPartnerConnection Insert(DtPartnerConnection element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_partner"].Value	= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ");
			insert.Parameters["@contact"].Value			= (string)element.GetData("ÊÎÍÒÀÊÒ");
			insert.Parameters["@comment"].Value			= (string)element.GetData("ÖÅËÜ");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("ÄÀÒÀ", insert.Parameters["@date"].Value);
			return element;
		}

		public static bool Delete(DateTime date, string contact)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			delete.Parameters["@contact"].Value			= (string)contact;
			delete.Parameters["@date"].Value			= (DateTime)date;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}
	}
}
