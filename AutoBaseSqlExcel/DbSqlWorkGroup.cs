using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkGroup.
	/// </summary>
	public class DbSqlWorkGroup
	{
		public static SqlCommand select;
		public DbSqlWorkGroup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÒÈÏ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkGroup element			= new DtWorkGroup();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÒÈÏ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÒÈÏ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkGroup element = (DtWorkGroup)MakeElement(reader);
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

	}
}
