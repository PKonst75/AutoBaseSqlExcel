using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlDirection.
	/// </summary>
	public class DbSqlDirection
	{
		public static SqlCommand	select;
		public static SqlCommand	select_list;
		public static SqlCommand	find;
		public static SqlCommand	insert;
		public static SqlCommand	update;
		public static SqlCommand	delete;

		public static SqlCommand	insert_done;


		public DbSqlDirection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("опедохяюмхе_бшанпйю", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_list = new SqlCommand("опедохяюмхе_яохянй", connection);
			select_list.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_list.Parameters.Add("@code_factory", SqlDbType.BigInt);
			select_list.Parameters.Add("@model_name", SqlDbType.VarChar);
			select_list.Parameters.Add("@search_term", SqlDbType.BigInt);
			select_list.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("опедохяюмхе_онхяй", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("опедохяюмхе_днаюбкемхе", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters.Add("@code_factory", SqlDbType.BigInt);
			insert.Parameters.Add("@interval_start", SqlDbType.BigInt);
			insert.Parameters.Add("@interval_end", SqlDbType.BigInt);
			insert.Parameters.Add("@number", SqlDbType.VarChar);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@model", SqlDbType.VarChar);
			insert.Parameters.Add("@description", SqlDbType.VarChar);
			insert.Parameters.Add("@search_type", SqlDbType.BigInt);
			DbSql.SetReturnError(insert);
			insert.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("опедохяюмхе_хглемемхе", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_factory", SqlDbType.BigInt);
			update.Parameters.Add("@interval_start", SqlDbType.BigInt);
			update.Parameters.Add("@interval_end", SqlDbType.BigInt);
			update.Parameters.Add("@number", SqlDbType.VarChar);
			update.Parameters.Add("@date", SqlDbType.DateTime);
			update.Parameters.Add("@model", SqlDbType.VarChar);
			update.Parameters.Add("@description", SqlDbType.VarChar);
			update.Parameters.Add("@search_type", SqlDbType.BigInt);
			DbSql.SetReturnError(update);
			update.CommandType = CommandType.StoredProcedure;


			insert_done = new SqlCommand("опедохяюмхе_бшонкмемхе_днаюбкемхе", connection);
			insert_done.Parameters.Add("@code_direction", SqlDbType.BigInt);
			insert_done.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_code", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_year", SqlDbType.BigInt);
			insert_done.Parameters.Add("@other_diler", SqlDbType.Bit);
			insert_done.Parameters.Add("@exception", SqlDbType.Bit);
			DbSql.SetReturnError(insert_done);
			insert_done.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtDirection element			= new DtDirection();
			element.SetData("йнд_опедохяюмхе", DbSql.GetValueLong(reader, "йнд_опедохяюмхе"));
			element.SetData("опнхгбндхрекэ_опедохяюмхе", DbSql.GetValueLong(reader, "опнхгбндхрекэ_опедохяюмхе"));
			element.SetData("лндекэ_опедохяюмхе", DbSql.GetValueString(reader, "лндекэ_опедохяюмхе"));
			element.SetData("мювюкн_хмрепбюк_опедохяюмхе", DbSql.GetValueLong(reader, "мювюкн_хмрепбюк_опедохяюмхе"));
			element.SetData("нйнмвюмхе_хмрепбюк_опедохяюмхе", DbSql.GetValueLong(reader, "нйнмвюмхе_хмрепбюк_опедохяюмхе"));
			element.SetData("мнлеп_опедохяюмхе", DbSql.GetValueString(reader, "мнлеп_опедохяюмхе"));
			element.SetData("нохяюмхе_опедохяюмхе", DbSql.GetValueString(reader, "нохяюмхе_опедохяюмхе"));
			element.SetData("рхо_онхяйю_опедохяюмхе", DbSql.GetValueLong(reader, "рхо_онхяйю_опедохяюмхе"));
			element.SetData("дюрю_опедохяюмхе", DbSql.GetValueDate(reader, "дюрю_опедохяюмхе"));
			element.SetData("мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе", DbSql.GetValueString(reader, "мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе"));
			return (object)element;
		}

		public static DtDirection Find(long code)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find.Parameters["@code"].Value = (long)code;
			DtDirection element = (DtDirection)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool Insert(DtDirection data)
		{
			insert.Parameters["@code"].Value			= (long)data.GetData("йнд_опедохяюмхе");
			insert.Parameters["@code_factory"].Value	= (long)data.GetData("опнхгбндхрекэ_опедохяюмхе");
			insert.Parameters["@model"].Value			= (string)data.GetData("лндекэ_опедохяюмхе");
			insert.Parameters["@interval_start"].Value	= (long)data.GetData("мювюкн_хмрепбюк_опедохяюмхе");
			insert.Parameters["@interval_end"].Value	= (long)data.GetData("нйнмвюмхе_хмрепбюк_опедохяюмхе");
			insert.Parameters["@number"].Value			= (string)data.GetData("мнлеп_опедохяюмхе");
			insert.Parameters["@description"].Value		= (string)data.GetData("нохяюмхе_опедохяюмхе");
			insert.Parameters["@search_type"].Value		= (long)data.GetData("рхо_онхяйю_опедохяюмхе");
			insert.Parameters["@date"].Value			= (DateTime)data.GetData("дюрю_опедохяюмхе");
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			data.SetData("йнд_опедохяюмхе", (long)insert.Parameters["@code"].Value);
			MessageBox.Show("гЮБНД-ХГЦНРНБХРЕКЭ ДНАЮБКЕМ");
			return true;
		}
		public static bool Update(DtDirection data)
		{
			update.Parameters["@code"].Value			= (long)data.GetData("йнд_опедохяюмхе");
			update.Parameters["@code_factory"].Value	= (long)data.GetData("опнхгбндхрекэ_опедохяюмхе");
			update.Parameters["@model"].Value			= (string)data.GetData("лндекэ_опедохяюмхе");
			update.Parameters["@interval_start"].Value	= (long)data.GetData("мювюкн_хмрепбюк_опедохяюмхе");
			update.Parameters["@interval_end"].Value	= (long)data.GetData("нйнмвюмхе_хмрепбюк_опедохяюмхе");
			update.Parameters["@number"].Value			= (string)data.GetData("мнлеп_опедохяюмхе");
			update.Parameters["@description"].Value		= (string)data.GetData("нохяюмхе_опедохяюмхе");
			update.Parameters["@search_type"].Value		= (long)data.GetData("рхо_онхяйю_опедохяюмхе");
			update.Parameters["@date"].Value			= (DateTime)data.GetData("дюрю_опедохяюмхе");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			MessageBox.Show("гЮБНД-ХГЦНРНБХРЕКЭ ХГЛЕМЕМ");
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtDirection element = (DtDirection)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "нЬХАЙЮ";
			}
			return item;
		}

		public static void PrepareSelectList(DbAuto auto)
		{
			select_list.Parameters["@code_auto"].Value = (long)auto.Code;
			select_list.Parameters["@code_factory"].Value = (long)auto.CodeFactory;
			select_list.Parameters["@model_name"].Value = (string)auto.ModelTxt;
			select_list.Parameters["@search_term"].Value = (long)auto.SparePartNumber;
		}

		public static bool InsertDone(DbAuto auto, DbCard card, DtDirection direction, bool exception)
		{
			insert_done.Parameters["@code_direction"].Value = (long)direction.GetData("йнд_опедохяюмхе");
			insert_done.Parameters["@code_auto"].Value = (long)auto.Code;
			if(card != null)
			{
				insert_done.Parameters["@card_number"].Value = (long)card.Number;
				insert_done.Parameters["@card_year"].Value = (long)card.Year;
				insert_done.Parameters["@card_code"].Value = (long)0;
				insert_done.Parameters["@other_diler"].Value = (bool)false;
				insert_done.Parameters["@exception"].Value = (bool)false;
			}
			else
			{
				insert_done.Parameters["@card_number"].Value = (long)0;
				insert_done.Parameters["@card_year"].Value = (long)0;
				insert_done.Parameters["@card_code"].Value = (long)0;
				if(exception == false)
				{
					insert_done.Parameters["@other_diler"].Value = (bool)true;
					insert_done.Parameters["@exception"].Value = (bool)false;
				}
				else
				{
					insert_done.Parameters["@other_diler"].Value = (bool)false;
					insert_done.Parameters["@exception"].Value = (bool)exception;
				}
			}
			if(DbSql.ExecuteCommandError(insert_done) == false) return false;
			MessageBox.Show("оПЕДОХЯЮМХЕ БШОНКМЕМН");
			return true;
		}
	}
}
