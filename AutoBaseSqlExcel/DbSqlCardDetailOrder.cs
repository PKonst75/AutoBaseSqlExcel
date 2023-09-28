using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// Ñ áàùîé äàííûõ
	/// </summary>
	public class DbSqlCardDetailOrder
	{
		public static SqlCommand insert;
		public static SqlCommand select;

		public DbSqlCardDetailOrder()
		{
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_catalogue_detail", SqlDbType.BigInt);
			insert.Parameters.Add("@code_storage_detail", SqlDbType.BigInt);
			insert.Parameters.Add("@guaranty_flag", SqlDbType.Bit);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static DtCardDetailOrder Insert(DtCardDetailOrder element)
		{
			insert.Parameters["@card_number"].Value				= (long)element.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@card_year"].Value				= (int)element.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@code_catalogue_detail"].Value	= (long)element.GetData("ÊÎÄ_ÊÀÒÀËÎÃ_ÄÅÒÀËÜ");
			insert.Parameters["@code_storage_detail"].Value		= (long)element.GetData("ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ");
			insert.Parameters["@guaranty_flag"].Value			= (bool)element.GetData("ÃÀĞÀÍÒÈß");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("ÊÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÇÀßÂÊÀ", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardDetailOrder element			= new DtCardDetailOrder();
			element.SetData("ÊÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÇÀßÂÊÀ"));
			element.SetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueInt(reader, "ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÊÎÄ_ÊÀÒÀËÎÃ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÀÒÀËÎÃ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß", DbSql.GetValueBool(reader, "ÃÀĞÀÍÒÈß"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardDetailOrder element = (DtCardDetailOrder)MakeElement(reader);
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

		public static void SelectInList(ListView list, long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

	}
}
