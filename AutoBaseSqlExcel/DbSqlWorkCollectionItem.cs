using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkCollectionItem.
	/// </summary>
	public class DbSqlWorkCollectionItem
	{
		public static SqlCommand		select;
		public static SqlCommand		update;
		public static SqlCommand		update_time;
		public static SqlCommand		update_number;
		public static SqlCommand		update_number_group;
		public static SqlCommand		find;
		public static SqlCommand		insert;
		public static SqlCommand		remove;

		public DbSqlWorkCollectionItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_бшанпйю", connection);
			select.Parameters.Add("@code_collection", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_онхяй", connection);
			find.Parameters.Add("@code_collection", SqlDbType.BigInt);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_днаюбкемхе", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_collection", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_хглемемхе", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_collection", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);


			update_time = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_хглемемхе_рпсднелйнярэ", connection);
			update_time.Parameters.Add("@code", SqlDbType.BigInt);
			update_time.Parameters.Add("@code_collection", SqlDbType.BigInt);
			update_time.Parameters.Add("@time", SqlDbType.Real);
			update_time.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_time);

			update_number = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_хглемемхе_мнлеп", connection);
			update_number.Parameters.Add("@code", SqlDbType.BigInt);
			update_number.Parameters.Add("@code_collection", SqlDbType.BigInt);
			update_number.Parameters.Add("@number", SqlDbType.Int);
			update_number.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_number);

			update_number_group = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_хглемемхе_мнлеп_цпсоою", connection);
			update_number_group.Parameters.Add("@code", SqlDbType.BigInt);
			update_number_group.Parameters.Add("@code_collection", SqlDbType.BigInt);
			update_number_group.Parameters.Add("@number_group", SqlDbType.Int);
			update_number_group.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_number_group);


			remove = new SqlCommand("рпсднелйнярэ_йнккейжхъ_щкелемр_сдюкемхе", connection);
			remove.Parameters.Add("@code", SqlDbType.BigInt);
			remove.Parameters.Add("@code_collection", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkCollectionItem element			= new DtWorkCollectionItem();
			element.SetData("йнд_йнккейжхъ_щкелемр", DbSql.GetValueLong(reader, "йнд_йнккейжхъ_щкелемр"));
			element.SetData("яяшкйю_йнд_йнккейжхъ", DbSql.GetValueLong(reader, "яяшкйю_йнд_йнккейжхъ"));
			element.SetData("мнлеп_йнккейжхъ_щкелемр", DbSql.GetValueInt(reader, "мнлеп_йнккейжхъ_щкелемр"));
			element.SetData("мнлеп_цпсоою_йнккейжхъ_щкелемр", DbSql.GetValueInt(reader, "мнлеп_цпсоою_йнккейжхъ_щкелемр"));
			element.SetData("мюхлемнбюмхе_йнккейжхъ_щкелемр", DbSql.GetValueString(reader, "мюхлемнбюмхе_йнккейжхъ_щкелемр"));
			element.SetData("рпсднелйнярэ_йнккейжхъ_щкелемр", DbSql.GetValueFloat(reader, "рпсднелйнярэ_йнккейжхъ_щкелемр"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkCollectionItem element = (DtWorkCollectionItem)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_collection)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			select.Parameters["@code_collection"].Value = (long)code_collection;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, long code_collection)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			select.Parameters["@code_collection"].Value = (long)code_collection;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtWorkCollectionItem Find(long code_collection, long code)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find.Parameters["@code_collection"].Value = (long)code_collection;
			find.Parameters["@code"].Value = (long)code;
			return (DtWorkCollectionItem)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtWorkCollectionItem Insert(DtWorkCollectionItem element)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			insert.Parameters["@code_collection"].Value = (long)element.GetData("яяшкйю_йнд_йнккейжхъ");
			insert.Parameters["@name"].Value = (string)element.GetData("мюхлемнбюмхе_йнккейжхъ_щкелемр");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("йнд_йнккейжхъ_щкелемр", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtWorkCollectionItem element)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			update.Parameters["@code"].Value = (long)element.GetData("йнд_йнккейжхъ_щкелемр");
			update.Parameters["@code_collection"].Value = (long)element.GetData("яяшкйю_йнд_йнккейжхъ");
			update.Parameters["@name"].Value = (string)element.GetData("мюхлемнбюмхе_йнккейжхъ_щкелемр");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			return true;
		}

		public static bool UpdateTime(float time, long code_collection, long code)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			update_time.Parameters["@code"].Value				= (long)code;
			update_time.Parameters["@code_collection"].Value	= (long)code_collection;
			update_time.Parameters["@time"].Value				= (float)time;
			if(DbSql.ExecuteCommandError(update_time) == false) return false;
			return true;
		}

		public static bool UpdateNumber(int number, long code_collection, long code)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			update_number.Parameters["@code"].Value				= (long)code;
			update_number.Parameters["@code_collection"].Value	= (long)code_collection;
			update_number.Parameters["@number"].Value				= (int)number;
			if(DbSql.ExecuteCommandError(update_number) == false) return false;
			return true;
		}
		public static bool UpdateNumberGroup(int number_group, long code_collection, long code)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			update_number_group.Parameters["@code"].Value				= (long)code;
			update_number_group.Parameters["@code_collection"].Value	= (long)code_collection;
			update_number_group.Parameters["@number_group"].Value		= (int)number_group;
			if(DbSql.ExecuteCommandError(update_number_group) == false) return false;
			return true;
		}


		public static bool Remove(long code_collection, long code)
		{
			// дНАЮБКЕМХЕ МЮАНПЮ
			remove.Parameters["@code"].Value			= (long)code;
			remove.Parameters["@code_collection"].Value = (long)code_collection;
			if(DbSql.ExecuteCommandError(remove) == false) return false;
			return true;
		}
	}
}
