using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoReceive.
	/// </summary>
	public class DbSqlAutoReceive
	{
		public static SqlCommand select;
		public static SqlCommand find;
		public static SqlCommand find_auto;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;
		public static SqlCommand transact;

		public static SqlCommand receive;			// оНКСВЕМХЕ ЮБРНЛНАХКЪ
		public static SqlCommand receive_delete;	// нРЛЕМЮ ОНКСВЕМХЪ ЮБРНЛНАХКЪ
		public static SqlCommand update_comment;	// хГЛЕМЕМХЕ ЙНЛЛЕМРЮПХЪ

		public DbSqlAutoReceive()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_бшанпйю", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_онхяй", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_auto = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_онхяй_юбрнлнахкэ", connection);
			find_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);
			find_auto.CommandType = CommandType.StoredProcedure;


			insert = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_днаюбкемхе", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@code_receiver", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_хглемемхе", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@date", SqlDbType.DateTime);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.Parameters.Add("@code_receiver", SqlDbType.BigInt);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_сдюкемхе", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			transact = new SqlCommand("юбрнлнахкэ_онксвемхе_днйслемр_опнбедемхе", connection);
			transact.Parameters.Add("@code", SqlDbType.BigInt);
			transact.Parameters.Add("@transaction", SqlDbType.Bit);
			transact.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(transact);

			receive = new SqlCommand("юбрнлнахкэ_онксвемхе_днаюбкемхе", connection);
			receive.Parameters.Add("@code", SqlDbType.BigInt);
			receive.Parameters.Add("@code_auto", SqlDbType.BigInt);
			receive.Parameters.Add("@code_document", SqlDbType.BigInt);
			receive.Parameters.Add("@comment", SqlDbType.VarChar);
			receive.CommandType = CommandType.StoredProcedure;
			receive.Parameters["@code"].Direction	= ParameterDirection.Output;
			DbSql.SetReturnError(receive);

			receive_delete = new SqlCommand("юбрнлнахкэ_онксвемхе_сдюкемхе", connection);
			receive_delete.Parameters.Add("@code_auto", SqlDbType.BigInt);
			receive_delete.Parameters.Add("@code_document", SqlDbType.BigInt);
			receive_delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(receive_delete);

			update_comment = new SqlCommand("юбрнлнахкэ_онксвемхе_опхлевюмхе", connection);
			update_comment.Parameters.Add("@code_auto", SqlDbType.BigInt);
			update_comment.Parameters.Add("@code_document", SqlDbType.BigInt);
			update_comment.Parameters.Add("@comment", SqlDbType.VarChar);
			update_comment.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_comment);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoReceive element			= new DtAutoReceive();
			element.SetData("йнд_юбрнлнахкэ_онксвемхе_днйслемр", DbSql.GetValueLong(reader, "йнд_юбрнлнахкэ_онксвемхе_днйслемр"));
			element.SetData("дюрю_днйслемр", DbSql.GetValueDate(reader, "дюрю_днйслемр"));
			element.SetData("опхлевюмхе_днйслемр", DbSql.GetValueString(reader, "опхлевюмхе_днйслемр"));
			element.SetData("йнд_онксвхк_юбрнлнахкх", DbSql.GetValueLong(reader, "йнд_онксвхк_юбрнлнахкх"));
			element.SetData("опнбедем_днйслемр", DbSql.GetValueBool(reader, "опнбедем_днйслемр"));
			element.SetData("онксвюрекэ", DbSql.GetValueString(reader, "онксвюрекэ"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtAutoReceive element = (DtAutoReceive)MakeElement(reader);
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

		public static void SelectInList(ListView list)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtAutoReceive Find(long code)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find.Parameters["@code"].Value = (long)code;
			return (DtAutoReceive)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtAutoReceive FindAuto(long code_auto)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find_auto.Parameters["@code_auto"].Value = (long)code_auto;
			return (DtAutoReceive)DbSql.Find(find_auto, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtAutoReceive Insert(DtAutoReceive element)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			insert.Parameters["@date"].Value			= (DateTime)element.GetData("дюрю_днйслемр");
			insert.Parameters["@comment"].Value			= (string)element.GetData("опхлевюмхе_днйслемр");
			insert.Parameters["@code_receiver"].Value	= (long)element.GetData("йнд_онксвхк_юбрнлнахкх");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("йнд_юбрнлнахкэ_онксвемхе_днйслемр",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtAutoReceive element)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			update.Parameters["@code"].Value			= (long)element.GetData("йнд_юбрнлнахкэ_онксвемхе_днйслемр");
			update.Parameters["@date"].Value			= (DateTime)element.GetData("дюрю_днйслемр");
			update.Parameters["@comment"].Value			= (string)element.GetData("опхлевюмхе_днйслемр");
			update.Parameters["@code_receiver"].Value	= (long)element.GetData("йнд_онксвхк_юбрнлнахкх");
			if(DbSql.ExecuteCommandError(update)== false) return false;
			return true;
		}

		public static bool Delete(long code)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			delete.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}

		public static bool Receive(DtAuto auto, DtAutoReceive document, string comment)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			receive.Parameters["@code_auto"].Value			= (long)auto.GetData("йнд_юбрнлнахкэ");
			receive.Parameters["@code_document"].Value		= (long)document.GetData("йнд_юбрнлнахкэ_онксвемхе_днйслемр");
			receive.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(receive)== false) return false;
			return true;
		}

		public static bool ReceiveDelete(long code_auto, long code_document)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			receive_delete.Parameters["@code_auto"].Value		= (long)code_auto;
			receive_delete.Parameters["@code_document"].Value	= (long)code_document;
			if(DbSql.ExecuteCommandError(receive_delete)== false) return false;
			return true;
		}

		public static bool UpdateComment(long code_auto, long code_document, string comment)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			update_comment.Parameters["@code_auto"].Value		= (long)code_auto;
			update_comment.Parameters["@code_document"].Value	= (long)code_document;
			update_comment.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(update_comment)== false) return false;
			return true;
		}
	}
}
