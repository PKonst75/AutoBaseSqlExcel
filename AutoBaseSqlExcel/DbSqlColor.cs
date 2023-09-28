using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlColor.
	/// </summary>
	public class DbSqlColor
	{

		public static SqlCommand select;
		public static SqlCommand select_all;
        public static SqlCommand select_all_mask;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;

		public static SqlCommand cancel;

		public DbSqlColor()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@code_model", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			select_all = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÂÛÁÎĞÊÀ_ÂÑÅÕ", connection);
			select_all.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_all.CommandType = CommandType.StoredProcedure;

            select_all_mask = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÂÛÁÎĞÊÀ_ÂÑÅÕ_ÌÀÑÊÀ", connection);
            select_all_mask.Parameters.Add("@code_model", SqlDbType.BigInt);
            select_all_mask.Parameters.Add("@pattern", SqlDbType.VarChar);
            select_all_mask.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert.Parameters.Add("@color_code", SqlDbType.VarChar);
			insert.Parameters.Add("@color_name", SqlDbType.VarChar);
			insert.Parameters.Add("@color_description", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_model", SqlDbType.BigInt);
			update.Parameters.Add("@color_code", SqlDbType.VarChar);
			update.Parameters.Add("@color_name", SqlDbType.VarChar);
			update.Parameters.Add("@color_description", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			cancel = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ_ÎÒÌÅÍÀ", connection);
			cancel.Parameters.Add("@code", SqlDbType.BigInt);
			cancel.Parameters.Add("@cancel", SqlDbType.Bit);
			cancel.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(cancel);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtColor element			= new DtColor();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ"));
			element.SetData("ÖÂÅÒ_ÊÎÄ", DbSql.GetValueString(reader, "ÖÂÅÒ_ÊÎÄ"));
			element.SetData("ÖÂÅÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÖÂÅÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			element.SetData("ÖÂÅÒ_ÎÏÈÑÀÍÈÅ", DbSql.GetValueString(reader, "ÖÂÅÒ_ÎÏÈÑÀÍÈÅ"));
			element.SetData("ÖÂÅÒ_ÎÒÌÅÍÅÍ", DbSql.GetValueBool(reader, "ÖÂÅÒ_ÎÒÌÅÍÅÍ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtColor element = (DtColor)MakeElement(reader);
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


		public static DtColor Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code"].Value = (long)code;
			return (DtColor)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtColor Insert(DtColor element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_model"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			insert.Parameters["@color_code"].Value			= (string)element.GetData("ÖÂÅÒ_ÊÎÄ");
			insert.Parameters["@color_name"].Value			= (string)element.GetData("ÖÂÅÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			insert.Parameters["@color_description"].Value	= (string)element.GetData("ÖÂÅÒ_ÎÏÈÑÀÍÈÅ");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtColor element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update.Parameters["@code"].Value				= (long)element.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ");
			update.Parameters["@code_model"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			update.Parameters["@color_code"].Value			= (string)element.GetData("ÖÂÅÒ_ÊÎÄ");
			update.Parameters["@color_name"].Value			= (string)element.GetData("ÖÂÅÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			update.Parameters["@color_description"].Value	= (string)element.GetData("ÖÂÅÒ_ÎÏÈÑÀÍÈÅ");
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
