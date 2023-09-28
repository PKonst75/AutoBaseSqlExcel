using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWork.
	/// </summary>
	public class DbSqlWork
	{
		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		set_collection;
		public static SqlCommand		clear_collection;
		public static SqlCommand		set_nv;
		public static SqlCommand		set_guaranty_price;
		
		public DbSqlWork()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@code_work_group", SqlDbType.BigInt);
			select.Parameters.Add("@search_pattern", SqlDbType.VarChar);
			select.Parameters.Add("@search_type", SqlDbType.BigInt);
			select.Parameters.Add("@show_common", SqlDbType.Bit);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			set_collection = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÓÑÒÀÍÎÂÊÀ_ÊÎËËÅÊÖÈß", connection);
			set_collection.Parameters.Add("@code", SqlDbType.BigInt);
			set_collection.Parameters.Add("@code_collection", SqlDbType.BigInt);
			DbSql.SetReturnError(set_collection);
			set_collection.CommandType = CommandType.StoredProcedure;

			set_nv = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÓÑÒÀÍÎÂÊÀ_ÍÂ", connection);
			set_nv.Parameters.Add("@code", SqlDbType.BigInt);
			set_nv.Parameters.Add("@nv", SqlDbType.Float);
			DbSql.SetReturnError(set_nv);
			set_nv.CommandType = CommandType.StoredProcedure;

			set_guaranty_price = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÓÑÒÀÍÎÂÊÀ_ÃÀĞÀÍÒÈÉÍÛÉ_ÍÎĞÌÀ×ÀÑ", connection);
			set_guaranty_price.Parameters.Add("@auto_type", SqlDbType.BigInt);
			set_guaranty_price.Parameters.Add("@price", SqlDbType.Float);
			DbSql.SetReturnError(set_guaranty_price);
			set_guaranty_price.CommandType = CommandType.StoredProcedure;

			clear_collection = new SqlCommand("ÒĞÓÄÎÅÌÊÎÑÒÜ_ÓÄÀËÅÍÈÅ_ÊÎËËÅÊÖÈß", connection);
			clear_collection.Parameters.Add("@code", SqlDbType.BigInt);
			DbSql.SetReturnError(clear_collection);
			clear_collection.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWork element			= new DtWork();
			element.SetData("ÊÎÄ_ÒĞÓÄÎÅÌÊÎÑÒÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÒĞÓÄÎÅÌÊÎÑÒÜ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÒÈÏ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÒÈÏ"));
			element.SetData("ÍÎÌÅĞ_ÏÎÇÈÖÈß", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÏÎÇÈÖÈß"));
			element.SetData("ÊÎÄ_ÄÅÒÀËÜ_ÒÅÊÑÒ", DbSql.GetValueString(reader, "ÊÎÄ_ÄÅÒÀËÜ_ÒÅÊÑÒ"));
			element.SetData("ÊÎÄ_ĞÀÁÎÒÀ_ÒÅÊÑÒ", DbSql.GetValueString(reader, "ÊÎÄ_ĞÀÁÎÒÀ_ÒÅÊÑÒ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			element.SetData("ÎÏÈÑÀÍÈÅ", DbSql.GetValueString(reader, "ÎÏÈÑÀÍÈÅ"));
			element.SetData("ÒĞÓÄÎÅÌÊÎÑÒÜ", DbSql.GetValueFloat(reader, "ÒĞÓÄÎÅÌÊÎÑÒÜ"));
			element.SetData("ÍÎĞÌÀ×ÀÑ", DbSql.GetValueFloat(reader, "ÍÎĞÌÀ×ÀÑ"));
			element.SetData("ÍÎĞÌÀ×ÀÑ_ÃÀĞÀÍÒÈß", DbSql.GetValueFloat(reader, "ÍÎĞÌÀ×ÀÑ_ÃÀĞÀÍÒÈß"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÑÏĞÀÂÎ×ÍÈÊ_ÒĞÓÄÎÅÌÊÎÑÒÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÑÏĞÀÂÎ×ÍÈÊ_ÒĞÓÄÎÅÌÊÎÑÒÜ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎËËÅÊÖÈß", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÊÎËËÅÊÖÈß"));
			element.SetData("ÍÂ", DbSql.GetValueFloat(reader, "ÍÂ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWork element = (DtWork)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_work_group, bool show_common, long search_type, string search_pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_work_group"].Value = (long)code_work_group;
			select.Parameters["@show_common"].Value = (bool)show_common;
			select.Parameters["@search_type"].Value = (long)search_type;
			select.Parameters["@search_pattern"].Value = (string)search_pattern;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtWork Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code"].Value = (long)code;
			return (DtWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool SetCollection(long code, long code_collection)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			set_collection.Parameters["@code"].Value = (long)code;
			set_collection.Parameters["@code_collection"].Value = (long)code_collection;
			return DbSql.ExecuteCommandError(set_collection);
		}

		public static bool SetNv(long code, float nv)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			set_nv.Parameters["@code"].Value	= (long)code;
			set_nv.Parameters["@nv"].Value		= (float)nv;
			return DbSql.ExecuteCommandError(set_nv);
		}

		public static bool SetGuarantyPrice(long auto_type, float price)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			set_guaranty_price.Parameters["@auto_type"].Value	= (long)auto_type;
			set_guaranty_price.Parameters["@price"].Value		= (float)price;
			return DbSql.ExecuteCommandError(set_guaranty_price);
		}

		public static bool ClearCollection(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			clear_collection.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(clear_collection);
		}
	}
}
