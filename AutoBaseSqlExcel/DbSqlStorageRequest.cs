using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlStorageRequest.
	/// </summary>
	public class DbSqlStorageRequest
	{
		private static SqlCommand insert;
		private static SqlCommand select;
		private static SqlCommand select_partner_name;
		private static SqlCommand find;
		private static SqlCommand give;
		private static SqlCommand execute;
		private static SqlCommand archive;

		public DbSqlStorageRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@code_storage", SqlDbType.BigInt);
			insert.Parameters.Add("@quontity", SqlDbType.Real);
			insert.Parameters.Add("@guaranty", SqlDbType.Bit);
			insert.Parameters.Add("@date_perfomance", SqlDbType.DateTime);
			insert.Parameters.Add("@code_requester", SqlDbType.BigInt);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@tmp_date_perfomance_is", SqlDbType.Bit);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters["@year"].Direction = ParameterDirection.InputOutput;
			insert.Parameters["@date"].Direction = ParameterDirection.InputOutput;
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			give = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÏÎÄÀ×À", connection);
			give.Parameters.Add("@code", SqlDbType.BigInt);
			give.Parameters.Add("@year", SqlDbType.Int);
			give.Parameters.Add("@date_give", SqlDbType.DateTime);
			give.Parameters.Add("@code_giver", SqlDbType.BigInt);
			give.Parameters.Add("@date_supply", SqlDbType.DateTime);
			give.CommandType = CommandType.StoredProcedure;
			give.Parameters["@date_give"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(give);

			execute = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ", connection);
			execute.Parameters.Add("@code", SqlDbType.BigInt);
			execute.Parameters.Add("@year", SqlDbType.Int);
			execute.Parameters.Add("@date_execute", SqlDbType.DateTime);
			execute.Parameters.Add("@code_execute", SqlDbType.BigInt);
			execute.CommandType = CommandType.StoredProcedure;
			execute.Parameters["@date_execute"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(execute);

			archive = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÀĞÕÈÂÀÖÈß", connection);
			archive.Parameters.Add("@code", SqlDbType.BigInt);
			archive.Parameters.Add("@year", SqlDbType.Int);
			archive.Parameters.Add("@code_archive", SqlDbType.BigInt);
			archive.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(archive);

			select = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_partner_name = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÂÛÁÎĞÊÀ_ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ", connection);
			select_partner_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_partner_name.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÑÊËÀÄ_ÄÅÒÀËÜ_ÇÀßÂÊÀ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static DtStorageRequest Insert(DtStorageRequest element)
		{
			insert.Parameters["@code"].Value					= (long)element.GetData("ÊÎÄ_ÇÀßÂÊÀ");
			insert.Parameters["@year"].Value					= (int)element.GetData("ÃÎÄ_ÇÀßÂÊÀ");
			insert.Parameters["@date"].Value					= (DateTime)element.GetData("ÄÀÒÀ_ÇÀßÂÊÀ");
			insert.Parameters["@code_storage"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ");
			insert.Parameters["@quontity"].Value				= (float)element.GetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ");
			insert.Parameters["@guaranty"].Value				= (bool)element.GetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ");
			insert.Parameters["@date_perfomance"].Value			= (DateTime)element.GetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß");
			insert.Parameters["@code_requester"].Value			= (long)element.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ");
			insert.Parameters["@card_number"].Value				= (long)element.GetData("ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@card_year"].Value				= (int)element.GetData("ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@code_partner"].Value			= (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ");
			insert.Parameters["@tmp_date_perfomance_is"].Value	= (bool)element.GetData("ÅÑÒÜ_ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("ÊÎÄ_ÇÀßÂÊÀ", insert.Parameters["@code"].Value);
			element.SetData("ÃÎÄ_ÇÀßÂÊÀ", insert.Parameters["@year"].Value);
			element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ", insert.Parameters["@date"].Value);
			return element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageRequest element = (DtStorageRequest)MakeElement(reader);
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

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageRequest element			= new DtStorageRequest();
			element.SetData("ÊÎÄ_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÇÀßÂÊÀ"));
			element.SetData("ÃÎÄ_ÇÀßÂÊÀ", DbSql.GetValueInt(reader, "ÃÎÄ_ÇÀßÂÊÀ"));
			element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ", DbSql.GetValueBool(reader, "ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ"));

			if(DbSql.IsValueNULL(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß") == false)
				element.SetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß", DbSql.GetValueDate(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß"));

			element.SetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À") == false)
				element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ") == false)
				element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÏÎÑÒÀÂÊÈ") == false)
				element.SetData("ÄÀÒÀ_ÏÎÑÒÀÂÊÈ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÏÎÑÒÀÂÊÈ"));

			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ", DbSql.GetValueString(reader, "ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ"));
			element.SetData("ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ", DbSql.GetValueString(reader, "ÊÎÍÒĞÀÃÅÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ"));
			return (object)element;
		}

		public static object MakeElementFind(SqlDataReader reader)
		{
			DtStorageRequest element			= new DtStorageRequest();
			element.SetData("ÊÎÄ_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÇÀßÂÊÀ"));
			element.SetData("ÃÎÄ_ÇÀßÂÊÀ", DbSql.GetValueInt(reader, "ÃÎÄ_ÇÀßÂÊÀ"));
			element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ", DbSql.GetValueBool(reader, "ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ"));
			if(DbSql.IsValueNULL(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß") == false)
				element.SetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß", DbSql.GetValueDate(reader, "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß"));
			element.SetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueInt(reader, "ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À") == false)
				element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À"));
			element.SetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ") == false)
				element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ"));
			element.SetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÏÎÑÒÀÂÊÈ") == false)
				element.SetData("ÄÀÒÀ_ÏÎÑÒÀÂÊÈ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÏÎÑÒÀÂÊÈ"));

			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ"));
			element.SetData("ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ", DbSql.GetValueString(reader, "ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ"));
			return (object)element;
		}

		public static void SelectInList(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_partner_name.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_partner_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtStorageRequest Find(long code, int year)
		{
			// Ïîèñê İëåìåíòà
			find.Parameters["@code"].Value = (long)code;
			find.Parameters["@year"].Value = (int)year;
			return (DtStorageRequest)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtStorageRequest Give(DtStorageRequest element)
		{
			give.Parameters["@code"].Value			= (long)element.GetData("ÊÎÄ_ÇÀßÂÊÀ");
			give.Parameters["@year"].Value			= (int)element.GetData("ÃÎÄ_ÇÀßÂÊÀ");
			give.Parameters["@code_giver"].Value	= (long)element.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ");
			give.Parameters["@date_supply"].Value	= (DateTime)element.GetData("ÄÀÒÀ_ÏÎÑÒÀÂÊÈ");
			if(DbSql.ExecuteCommandError(give) == false) return null;
			element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À", give.Parameters["@date_give"].Value);
			return element;
		}
		public static DtStorageRequest Execute(DtStorageRequest element)
		{
			execute.Parameters["@code"].Value			= (long)element.GetData("ÊÎÄ_ÇÀßÂÊÀ");
			execute.Parameters["@year"].Value			= (int)element.GetData("ÃÎÄ_ÇÀßÂÊÀ");
			execute.Parameters["@code_execute"].Value	= (long)element.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ");
			if(DbSql.ExecuteCommandError(execute) == false) return null;
			element.SetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ", execute.Parameters["@date_execute"].Value);
			return element;
		}
		public static bool Archive(DtStorageRequest element)
		{
			archive.Parameters["@code"].Value			= (long)element.GetData("ÊÎÄ_ÇÀßÂÊÀ");
			archive.Parameters["@year"].Value			= (int)element.GetData("ÃÎÄ_ÇÀßÂÊÀ");
			archive.Parameters["@code_archive"].Value	= (long)element.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÀĞÕÈÂÀÖÈß");
			if(DbSql.ExecuteCommandError(archive) == false) return false;
			return true;
		}
	}
}
