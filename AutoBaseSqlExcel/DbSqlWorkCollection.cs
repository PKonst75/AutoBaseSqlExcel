using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkCollection.
	/// </summary>
	public class DbSqlWorkCollection
	{
		public static SqlCommand		select;
		public static SqlCommand		update;
		public static SqlCommand		insert;
		public static SqlCommand		remove;

		public DbSqlWorkCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÊÎËËÅÊÖÈß_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÊÎËËÅÊÖÈß_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÊÎËËÅÊÖÈß_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			remove = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÊÎËËÅÊÖÈß_ÓÄÀËÅÍÈÅ", connection);
			remove.Parameters.Add("@code", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkCollection element			= new DtWorkCollection();
			element.SetData("ÊÎÄ_ÊÎËËÅÊÖÈß", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎËËÅÊÖÈß"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÎËËÅÊÖÈß", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÎËËÅÊÖÈß"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkCollection element = (DtWorkCollection)MakeElement(reader);
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

		public static DtWorkCollection Insert(DtWorkCollection element)
		{
			// Äîáàâëåíèå íàáîğà
			insert.Parameters["@name"].Value = (string)element.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÎËËÅÊÖÈß");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("ÊÎÄ_ÊÎËËÅÊÖÈß", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtWorkCollection element)
		{
			// Äîáàâëåíèå íàáîğà
			update.Parameters["@code"].Value = (long)element.GetData("ÊÎÄ_ÊÎËËÅÊÖÈß");
			update.Parameters["@name"].Value = (string)element.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÎËËÅÊÖÈß");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			return true;
		}

		public static bool Remove(long code)
		{
			// Äîáàâëåíèå íàáîğà
			remove.Parameters["@code"].Value = (long)code;
			if(DbSql.ExecuteCommandError(remove) == false) return false;
			return true;
		}

	}
}
