using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerProperty.
	/// </summary>
	public class DbSqlPartnerProperty
	{

		public static SqlCommand	select;
		public static SqlCommand	find;
		public static SqlCommand	insert;
		public static SqlCommand	update;
		public static SqlCommand	delete;

		public DbSqlPartnerProperty()
		{
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@name_mask", SqlDbType.VarChar);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code_partner", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.Parameters.Add("@cashless", SqlDbType.Bit);
			insert.Parameters.Add("@discount", SqlDbType.Real);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			DbSql.SetReturnError(insert);
			insert.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ_ÈÇÌÅÍÅÍÈÅ", connection);
			update.Parameters.Add("@code_partner", SqlDbType.BigInt);
			update.Parameters.Add("@cashless", SqlDbType.Bit);
			update.Parameters.Add("@discount", SqlDbType.Real);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.Parameters.Add("@card_number", SqlDbType.BigInt);
			DbSql.SetReturnError(update);
			update.CommandType = CommandType.StoredProcedure;

			delete = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code_partner", SqlDbType.BigInt);
			DbSql.SetReturnError(delete);
			delete.CommandType = CommandType.StoredProcedure;
		}

		public static void PrepareSelect(string name_mask)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@name_mask"].Value = (string)name_mask;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			ListViewItem item = new ListViewItem();
			DtPartnerProperty element = (DtPartnerProperty)MakeElement(reader);
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
			DtPartnerProperty element	= new DtPartnerProperty();
			element.CodePartner			= DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ");
			element.PartnerName			= DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ");
			element.Cashless			= DbSql.GetValueBool(reader, "ÁÅÇÍÀË_ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ");
			element.Discount			= DbSql.GetValueFloat(reader, "ÑÊÈÄÊÀ_ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ");
			element.Comment				= DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ");
			element.CardNumber			= DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÊÀĞÒÀ_ÊÎÍÒĞÀÃÅÍÒ_ÑÂÎÉÑÒÂÎ");
			return (object)element;
		}

		public static DtPartnerProperty Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code_partner"].Value = (long)code;
			DtPartnerProperty element = (DtPartnerProperty)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool Insert(DtPartnerProperty data)
		{
			insert.Parameters["@code_partner"].Value	= data.CodePartner;
			insert.Parameters["@cashless"].Value		= data.Cashless;
			insert.Parameters["@discount"].Value		= data.Discount;
			insert.Parameters["@comment"].Value			= data.Comment;
			insert.Parameters["@card_number"].Value		= data.CardNumber;
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			MessageBox.Show("Ñâîéñòâî êîíòğàãåíòà äîáàâëåíî");
			return true;
		}
		public static bool Update(DtPartnerProperty data)
		{
			update.Parameters["@code_partner"].Value	= data.CodePartner;
			update.Parameters["@cashless"].Value		= data.Cashless;
			update.Parameters["@discount"].Value		= data.Discount;
			update.Parameters["@comment"].Value			= data.Comment;
			update.Parameters["@card_number"].Value		= data.CardNumber;
			if(DbSql.ExecuteCommandError(update) == false) return false;
			MessageBox.Show("Ñâîéñòâî êîíòğàãåíòà èçìåíåíî");
			return true;
		}
		public static bool Delete(long data)
		{
			delete.Parameters["@code_partner"].Value	= (long)data;
			if(DbSql.ExecuteCommandError(delete) == false) return false;
			MessageBox.Show("Ñâîéñòâî êîíòğàãåíòà óäàëåíî");
			return true;
		}
	}
}
