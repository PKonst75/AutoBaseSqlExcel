using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlListAlarm.
	/// </summary>
	public class DbSqlListAlarm
	{
		public static SqlCommand		insert;
		public static SqlCommand		find;
		public static SqlCommand		select;

		public DbSqlListAlarm()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÑÏÈÑÎÊ_ÑÈÃÍÀËÈÇÀÖÈß_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find = new SqlCommand("ÑÏÈÑÎÊ_ÑÈÃÍÀËÈÇÀÖÈß_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("ÑÏÈÑÎÊ_ÑÈÃÍÀËÈÇÀÖÈß_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static DtListAlarm Insert(DtListAlarm element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@name"].Value		= (string)element.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß", (long)(insert.Parameters["@code"].Value));
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtListAlarm element			= new DtListAlarm();
			element.SetData("ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtListAlarm element = (DtListAlarm)MakeElement(reader);
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

		public static DtListAlarm Find(long code)
		{
			find.Parameters["@code"].Value	= (long)code;
			return (DtListAlarm)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array)
		{
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInList(ListView list)
		{
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
	}
}
