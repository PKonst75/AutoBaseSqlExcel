using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Ğàáîòà ñ îñòàòêàìè íà ñêëàäå
	/// </summary>
	public class DbSqlStorageDetail
	{

		public static SqlCommand report_exchange;
		public static SqlCommand select_number;
		public static SqlCommand select_name;
		public static SqlCommand select_1c;
		public static SqlCommand select_name_number;
		public static SqlCommand find_1c;
		public static SqlCommand calculate_storage;
		public static SqlCommand find;

		public DbSqlStorageDetail()
		{
		}

		public static void Init(SqlConnection connection)
		{
			report_exchange = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÀÍÀËÈÇ_ÄÂÈÆÅÍÈß", connection);
			report_exchange.Parameters.Add("@date_start", SqlDbType.DateTime);
			report_exchange.Parameters.Add("@date_end", SqlDbType.DateTime);
			report_exchange.CommandType = CommandType.StoredProcedure;
			report_exchange.CommandTimeout = 300;

			select_number = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_ÍÎÌÅĞ", connection);
			select_number.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_number.Parameters.Add("@show_null", SqlDbType.Bit);
			select_number.CommandType = CommandType.StoredProcedure;

			select_name = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_ÍÀÈÌÅÍÎÂÀÍÈÅ", connection);
			select_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_name.Parameters.Add("@show_null", SqlDbType.Bit);
			select_name.CommandType = CommandType.StoredProcedure;

			select_name_number = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÍÎÌÅĞ", connection);
			select_name_number.Parameters.Add("@number", SqlDbType.VarChar);
			select_name_number.Parameters.Add("@show_null", SqlDbType.Bit);
			select_name_number.CommandType = CommandType.StoredProcedure;

			select_1c = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_1Ñ", connection);
			select_1c.CommandType = CommandType.StoredProcedure;

			find_1c = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÏÎÈÑÊ_1Ñ", connection);
			find_1c.Parameters.Add("@code_1c", SqlDbType.BigInt);
			find_1c.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			calculate_storage = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÏÅĞÅÑ×ÅÒ_ÎÑÒÀÒÊÎÂ", connection);
			calculate_storage.Parameters.Add("@code", SqlDbType.BigInt);
			calculate_storage.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(calculate_storage);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageDetail element			= new DtStorageDetail();
			element.SetData("ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÍÎÌÅĞ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÖÅÍÀ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÖÅÍÀ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÂÕÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÂÕÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß", DbSql.GetValueString(reader, "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß"));
			element.SetData("ÎÏÈÑÀÍÈÅ", DbSql.GetValueString(reader, "ÎÏÈÑÀÍÈÅ"));
			element.SetData("ÊÎÄ_1Ñ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_1Ñ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ĞÀÑÕÎÄ", DbSql.GetValueFloat(reader, "ĞÀÑÕÎÄ"));
			element.SetData("ÎÑÒÀÒÎÊ", DbSql.GetValueFloat(reader, "ÎÑÒÀÒÎÊ"));
			element.SetData("ÊÎËÈ×ÅÑÒÂÎ", DbSql.GetValueFloat(reader, "ÊÎËÈ×ÅÑÒÂÎ"));
			element.Liquid = DbSql.GetValueBool(reader, "ÆÈÄÊÎÑÒÈ_ÑÊËÀÄ_ÄÅÒÀËÜ");
			
			return (object)element;
		}

		public static ListViewItem MakeLVItemBalance(SqlDataReader reader)
		{
			DtStorageDetail element = (DtStorageDetail)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemBalance(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Îøèáêà";
			}
			return item;
		}

		public static void SelectInListBalance(ListView list, DateTime start, DateTime end)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			report_exchange.Parameters["@date_start"].Value		= (DateTime)start;
			report_exchange.Parameters["@date_end"].Value		= (DateTime)end;
			DbSql.FillList(list, report_exchange, new DbSql.DelegateMakeLVItem(MakeLVItemBalance));
		}


		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageDetail element = (DtStorageDetail)MakeElement(reader);
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
		public static void SelectInListNumber(ListView list, string pattern, bool show_null)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_number.Parameters["@pattern"].Value			= (string)pattern;
			select_number.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_number, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListName(ListView list, string pattern, bool show_null)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_name.Parameters["@pattern"].Value		= (string)pattern;
			select_name.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListNameNumber(ListView list, string number, bool show_null)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_name_number.Parameters["@number"].Value			= (string)number;
			select_name_number.Parameters["@show_null"].Value		= (bool)show_null;
			DbSql.FillList(list, select_name_number, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtStorageDetail Find1C(long code_1c)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find_1c.Parameters["@code_1c"].Value = (long)code_1c;
			return (DtStorageDetail)DbSql.Find(find_1c, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtStorageDetail Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code"].Value = (long)code;
			return (DtStorageDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray1C(ArrayList array)
		{
			DbSql.FillArray(array, select_1c, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool CalculateStorage(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			calculate_storage.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(calculate_storage);
		}
	}
}
