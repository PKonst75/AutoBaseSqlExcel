using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCommonReason.
	/// </summary>
	public class DbSqlCommonReason
	{
		public static SqlCommand		insert;
		public static SqlCommand		find;
		public static SqlCommand		select;

		public DbSqlCommonReason()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÎÁÙÀß_ÏĞÈ×ÈÍÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@description", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find = new SqlCommand("ÎÁÙÀß_ÏĞÈ×ÈÍÀ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("ÎÁÙÀß_ÏĞÈ×ÈÍÀ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static DtCommonReason Insert(DtCommonReason element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@description"].Value		= (string)element.GetData("ÎÏÈÑÀÍÈÅ");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("ÊÎÄ_ÏĞÈ×ÈÍÀ", (object)insert.Parameters["@code"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCommonReason element			= new DtCommonReason();
			element.SetData("ÊÎÄ_ÏĞÈ×ÈÍÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏĞÈ×ÈÍÀ"));
			element.SetData("ÎÏÈÑÀÍÈÅ", DbSql.GetValueString(reader, "ÎÏÈÑÀÍÈÅ"));
			
			return (object)element;
		}

		public static DtCommonReason Find(long code)
		{
			find.Parameters["@code"].Value	= (long)code;
			return (DtCommonReason)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array)
		{
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCommonReason element = (DtCommonReason)MakeElement(reader);
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
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
	}
}
