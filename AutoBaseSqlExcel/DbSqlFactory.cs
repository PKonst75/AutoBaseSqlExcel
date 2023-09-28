using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlFactory.
	/// </summary>
	public class DbSqlFactory
	{
		public static SqlCommand	select;
		public static SqlCommand	find;
		public static SqlCommand	find_prefix;
		public static SqlCommand	insert;
		public static SqlCommand	update;
		public static SqlCommand	delete;

		public DbSqlFactory()
		{
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("юбрнлнахкэ_опнхгбндхрекэ_бшанпйю", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("юбрнлнахкэ_опнхгбндхрекэ_онхяй", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_prefix = new SqlCommand("юбрнлнахкэ_опнхгбндхрекэ_онхяй_опетхйя", connection);
			find_prefix.Parameters.Add("@prefix", SqlDbType.VarChar);
			find_prefix.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("юбрнлнахкэ_опнхгбндхрекэ_днаюбкемхе", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@prefix", SqlDbType.VarChar);
			DbSql.SetReturnError(insert);
			insert.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("юбрнлнахкэ_опнхгбндхрекэ_хглемемхе", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.Parameters.Add("@prefix", SqlDbType.VarChar);
			DbSql.SetReturnError(update);
			update.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtFactory element			= new DtFactory();
			element.SetData("йнд_юбрнлнахкэ_опнхгбндхрекэ", DbSql.GetValueLong(reader, "йнд_юбрнлнахкэ_опнхгбндхрекэ"));
			element.SetData("мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ", DbSql.GetValueString(reader, "мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ"));
			element.SetData("опетхйя_юбрнлнахкэ_опнхгбндхрекэ", DbSql.GetValueString(reader, "опетхйя_юбрнлнахкэ_опнхгбндхрекэ"));
			return (object)element;
		}

		public static void SelectInList(ListView list)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtFactory Find(long code)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find.Parameters["@code"].Value = (long)code;
			DtFactory element = (DtFactory)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}
		public static DtFactory FindPrefix(string prefix)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find_prefix.Parameters["@prefix"].Value = (string)prefix;
			DtFactory element = (DtFactory)DbSql.Find(find_prefix, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool Insert(DtFactory data)
		{
			insert.Parameters["@code"].Value		= (long)data.GetData("йнд_юбрнлнахкэ_опнхгбндхрекэ");
			insert.Parameters["@name"].Value		= (string)data.GetData("мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ");
			insert.Parameters["@prefix"].Value		= (string)data.GetData("опетхйя_юбрнлнахкэ_опнхгбндхрекэ");
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			data.SetData("йнд_юбрнлнахкэ_опнхгбндхрекэ", (long)insert.Parameters["@code"].Value);
			MessageBox.Show("гЮБНД-ХГЦНРНБХРЕКЭ ДНАЮБКЕМ");
			return true;
		}
		public static bool Update(DtFactory data)
		{
			update.Parameters["@code"].Value		= (long)data.GetData("йнд_юбрнлнахкэ_опнхгбндхрекэ");
			update.Parameters["@name"].Value		= (string)data.GetData("мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ");
			update.Parameters["@prefix"].Value		= (string)data.GetData("опетхйя_юбрнлнахкэ_опнхгбндхрекэ");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			MessageBox.Show("гЮБНД-ХГЦНРНБХРЕКЭ ХГЛЕМЕМ");
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtFactory element = (DtFactory)MakeElement(reader);
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
	}
}
