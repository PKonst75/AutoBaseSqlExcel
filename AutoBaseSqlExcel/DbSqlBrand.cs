using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlBrand.
	/// </summary>
	public class DbSqlBrand
	{
		public static SqlCommand select;
		public static SqlCommand insert;
		public static SqlCommand delete;
		public static SqlCommand update;
		public static SqlCommand find_model;

		public DbSqlBrand()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("юбрнлнахкэ_апемд_бшанпйю", connection);
			select.CommandType = CommandType.StoredProcedure;

			find_model = new SqlCommand("юбрнлнахкэ_апемд_онхяй_лндекэ", connection);
			find_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			find_model.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("юбрнлнахкэ_апемд_днаюбкемхе", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("юбрнлнахкэ_апемд_хглемемхе", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("юбрнлнахкэ_апемд_сдюкемхе", connection);
			delete.Parameters.Add("@code_brand", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtBrand element			= new DtBrand();
			element.SetData("йнд_юбрнлнахкэ_апемд", DbSql.GetValueLong(reader, "йнд_юбрнлнахкэ_апемд"));
			element.SetData("мюхлемнбюмхе_юбрнлнахкэ_апемд", DbSql.GetValueString(reader, "мюхлемнбюмхе_юбрнлнахкэ_апемд"));
			
			return (object)element;
		}

		public static TreeNode MakeTNode(SqlDataReader reader)
		{
			DtBrand element = (DtBrand)MakeElement(reader);
			TreeNode node = new TreeNode();
			if(element != null)
			{
				element.SetTNode(node);
			}
			else
			{
				node.Tag			= 0;
				node.Text			= "нЬХАЙЮ";
			}
			return node;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtBrand element = (DtBrand)MakeElement(reader);
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

		public static void SelectInTree(TreeView tree)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			DbSql.FillTree(tree, select, new DbSql.DelegateMakeTNode(MakeTNode));
		}

		public static DtBrand FindModel(long code_model)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН ЛЮЯЙЕ
			find_model.Parameters["@code_model"].Value	= (long)code_model;
			return (DtBrand)DbSql.Find(find_model, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtBrand Insert(DtBrand element)
		{
			insert.Parameters["@name"].Value = (string)element.GetData("мюхлемнбюмхе_юбрнлнахкэ_апемд");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("йнд_юбрнлнахкэ_апемд", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtBrand element)
		{
			update.Parameters["@code"].Value = (long)element.GetData("йнд_юбрнлнахкэ_апемд");
			update.Parameters["@name"].Value = (string)element.GetData("мюхлемнбюмхе_юбрнлнахкэ_апемд");
			if(DbSql.ExecuteCommandError(update) != true) return false;
			return true;
		}

		public static bool Delete(long code_brand)
		{
			delete.Parameters["@code_brand"].Value = (long)code_brand;
			if(DbSql.ExecuteCommandError(delete) != true) return false;
			return true;
		}
	}
}
