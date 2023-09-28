using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlStorageIncom.
	/// </summary>
	public class DbSqlStorageIncom
	{
		public static SqlCommand		insert;
		public static SqlCommand		select_doc;
		public static SqlCommand		select_detail;

		public DbSqlStorageIncom()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("яйкюд_дерюкэ_опхунд_днаюбкемхе", connection);
			insert.Parameters.Add("@code_doc", SqlDbType.BigInt);
			insert.Parameters.Add("@position", SqlDbType.BigInt);
			insert.Parameters.Add("@code_detail", SqlDbType.BigInt);
			insert.Parameters.Add("@quontity", SqlDbType.Float);
			insert.Parameters.Add("@price", SqlDbType.Float);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			select_doc = new SqlCommand("яйкюд_дерюкэ_опхунд_бшанпйю_днйслемр", connection);
			select_doc.Parameters.Add("@code_doc", SqlDbType.BigInt);
			select_doc.CommandType = CommandType.StoredProcedure;

			select_detail = new SqlCommand("яйкюд_дерюкэ_опхунд_бшанпйю_дерюкэ", connection);
			select_detail.Parameters.Add("@code_detail", SqlDbType.BigInt);
			select_detail.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageIncom element			= new DtStorageIncom();
			element.SetData("йнд_днйслемр", DbSql.GetValueLong(reader, "йнд_днйслемр"));
			element.SetData("онгхжхъ", DbSql.GetValueLong(reader, "онгхжхъ"));
			element.SetData("йнд_яйкюд_дерюкэ", DbSql.GetValueLong(reader, "йнд_яйкюд_дерюкэ"));
			element.SetData("йнкхвеярбн", DbSql.GetValueFloat(reader, "йнкхвеярбн"));
			element.SetData("жемю", DbSql.GetValueFloat(reader, "жемю"));
			element.SetData("мюхлемнбюмхе", DbSql.GetValueString(reader, "мюхлемнбюмхе"));
			element.SetData("юпрхйск", DbSql.GetValueString(reader, "юпрхйск"));
			element.SetData("мнлеп", DbSql.GetValueString(reader, "мнлеп"));
			element.SetData("дюрю", DbSql.GetValueDate(reader, "дюрю"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageIncom element = (DtStorageIncom)MakeElement(reader);
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

		public static ListViewItem MakeLVItemMove(SqlDataReader reader)
		{
			DtStorageIncom element = (DtStorageIncom)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemMove(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "нЬХАЙЮ";
			}
			return item;
		}

		public static DtStorageIncom Insert(DtStorageIncom element)
		{
			// оНДЦНРНБЙЮ ЙНЛЮМДШ ОНХЯЙЮ ОН АПЕМДС
			insert.Parameters["@code_doc"].Value	= (long)element.GetData("йнд_днйслемр");
			insert.Parameters["@position"].Value	= (long)element.GetData("онгхжхъ");
			insert.Parameters["@code_detail"].Value = (long)element.GetData("йнд_яйкюд_дерюкэ");
			insert.Parameters["@quontity"].Value	= (float)element.GetData("йнкхвеярбн");
			insert.Parameters["@price"].Value		= (float)element.GetData("жемю");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			return element;
		}

		public static void SelectInListDoc(ListView list, long code_doc)
		{
			select_doc.Parameters["@code_doc"].Value	= (long)code_doc;
			DbSql.FillList(list, select_doc, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListDoc(ListView list, DtStorageIncomDoc doc)
		{
			select_doc.Parameters["@code_doc"].Value	= (long)doc.GetData("йнд");
			DbSql.FillList(list, select_doc, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListDetail(ListView list, long code_detail)
		{
			select_detail.Parameters["@code_detail"].Value	= (long)code_detail;
			DbSql.FillList(list, select_detail, new DbSql.DelegateMakeLVItem(MakeLVItemMove));
		}
	}
}
