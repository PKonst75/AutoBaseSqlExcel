using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlVariant.
	/// </summary>
	public class DbSqlVariant
	{
		public static SqlCommand select;
		public static SqlCommand select_all;
        public static SqlCommand select_all_mask;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;

		public static SqlCommand cancel;

		public DbSqlVariant()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@code_model", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			select_all = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÂÛÁÎĞÊÀ_ÂÑÅÕ", connection);
			select_all.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_all.CommandType = CommandType.StoredProcedure;

            select_all_mask = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÂÛÁÎĞÊÀ_ÂÑÅÕ_ÌÀÑÊÀ", connection);
            select_all_mask.Parameters.Add("@code_model", SqlDbType.BigInt);
            select_all_mask.Parameters.Add("@pattern", SqlDbType.VarChar);
            select_all_mask.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert.Parameters.Add("@variant_name", SqlDbType.VarChar);
			insert.Parameters.Add("@variant_description", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_model", SqlDbType.BigInt);
			update.Parameters.Add("@variant_name", SqlDbType.VarChar);
			update.Parameters.Add("@variant_description", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			cancel = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ_ÎÒÌÅÍÀ", connection);
			cancel.Parameters.Add("@code", SqlDbType.BigInt);
			cancel.Parameters.Add("@cancel", SqlDbType.Bit);
			cancel.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(cancel);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtVariant element			= new DtVariant();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ"));
			element.SetData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			element.SetData("ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ", DbSql.GetValueString(reader, "ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ"));
			element.SetData("ÈÑÏÎËÍÅÍÈÅ_ÎÒÌÅÍÅÍ", DbSql.GetValueBool(reader, "ÈÑÏÎËÍÅÍÈÅ_ÎÒÌÅÍÅÍ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtVariant element = (DtVariant)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_model)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, long code_model)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInListAll(ListView list, long code_model)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_all.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select_all, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

        public static void SelectInArrayAll(ArrayList array, long code_model, string pattern)
        {
            // Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
            select_all_mask.Parameters["@code_model"].Value = (long)code_model;
            select_all_mask.Parameters["@pattern"].Value = (string)pattern;
            DbSql.FillArray(array, select_all_mask, new DbSql.DelegateMakeElement(MakeElement));
        }

		public static DtVariant Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code"].Value = (long)code;
			return (DtVariant)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtVariant Insert(DtVariant element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_model"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			insert.Parameters["@variant_name"].Value			= (string)element.GetData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			insert.Parameters["@variant_description"].Value	= (string)element.GetData("ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtVariant element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update.Parameters["@code"].Value				= (long)element.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ");
			update.Parameters["@code_model"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			update.Parameters["@variant_name"].Value		= (string)element.GetData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			update.Parameters["@variant_description"].Value	= (string)element.GetData("ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ");
			if(DbSql.ExecuteCommandError(update)== false) return false;
			return true;
		}

		public static bool Delete(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			delete.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}

		public static bool Cancel(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			cancel.Parameters["@cancel"].Value				= (bool)true;
			cancel.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(cancel)== false) return false;
			return true;
		}
		public static bool Restore(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			cancel.Parameters["@cancel"].Value				= (bool)false;
			cancel.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(cancel)== false) return false;
			return true;
		}
	}
}
