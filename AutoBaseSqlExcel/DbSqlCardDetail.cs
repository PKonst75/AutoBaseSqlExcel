using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardDetail.
	/// </summary>
	public class DbSqlCardDetail
	{
		private static SqlCommand select;
		private static SqlCommand select_period;
		private static SqlCommand select_period_spec1;
		private static SqlCommand find;
		private static SqlCommand update_input;
		private static SqlCommand update_price;
		private static SqlCommand update_guaranty;
		private static SqlCommand update_present;
        private static SqlCommand update_to;
        private static SqlCommand update_discount;

		public DbSqlCardDetail()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;

			select_period = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_ÏÅĞÈÎÄ", connection);
			select_period.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_period.Parameters.Add("@liquid", SqlDbType.Bit);
			select_period.Parameters.Add("@guaranty", SqlDbType.Bit);
			select_period.Parameters.Add("@cashless", SqlDbType.Bit);
			select_period.Parameters.Add("@inner", SqlDbType.Bit);
			select_period.CommandType = CommandType.StoredProcedure;

			select_period_spec1 = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÂÛÁÎĞÊÀ_ÏÅĞÈÎÄ_ÑÏÅÖ1", connection);
			select_period_spec1.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_period_spec1.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_period_spec1.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.Parameters.Add("@position", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			update_input = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÂÕÎÄ", connection);
			update_input.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_input.Parameters.Add("@card_year", SqlDbType.Int);
			update_input.Parameters.Add("@position", SqlDbType.BigInt);
			update_input.Parameters.Add("@input", SqlDbType.Real);
			update_input.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_input);

			update_price = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÖÅÍÀ", connection);
			update_price.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_price.Parameters.Add("@card_year", SqlDbType.Int);
			update_price.Parameters.Add("@position", SqlDbType.BigInt);
			update_price.Parameters.Add("@price", SqlDbType.Real);
			update_price.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_price);

			update_guaranty = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÃÀĞÀÍÒÈß", connection);
			update_guaranty.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@card_year", SqlDbType.Int);
			update_guaranty.Parameters.Add("@position", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@guaranty", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake_initiator", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake", SqlDbType.VarChar);
			update_guaranty.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_guaranty);

			update_present = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÏÎÄÀĞÎÊ", connection);
			update_present.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_present.Parameters.Add("@card_year", SqlDbType.Int);
			update_present.Parameters.Add("@position", SqlDbType.BigInt);
			update_present.Parameters.Add("@present", SqlDbType.Bit);
			update_present.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_present);

            update_to = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÒÎ", connection);
            update_to.Parameters.Add("@card_number", SqlDbType.BigInt);
            update_to.Parameters.Add("@card_year", SqlDbType.Int);
            update_to.Parameters.Add("@position", SqlDbType.BigInt);
            update_to.Parameters.Add("@to", SqlDbType.Bit);
            update_to.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_to);

            update_discount = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ_ÈÇÌÅÍÈÒÜ_ÑÊÈÄÊÀ", connection);
            update_discount.Parameters.Add("@card_number", SqlDbType.BigInt);
            update_discount.Parameters.Add("@card_year", SqlDbType.Int);
            update_discount.Parameters.Add("@position", SqlDbType.BigInt);
            update_discount.Parameters.Add("@discount", SqlDbType.Real);
            update_discount.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_discount);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardDetail element = new DtCardDetail();
			element.SetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueInt(reader, "ÃÎÄ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÏÎÇÈÖÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÏÎÇÈÖÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎËÈ×ÅÑÒÂÎ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÊÎËÈ×ÅÑÒÂÎ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÖÅÍÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÖÅÍÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÂÕÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueFloat(reader, "ÂÕÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueBool(reader, "ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÂÍÅØÍßß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueBool(reader, "ÂÍÅØÍßß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("×ÅÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueBool(reader, "×ÅÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÏÎËÓ×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÏÎËÓ×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎÄ_ÑÊËÀÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÑÊËÀÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÊÀÒÀËÎÃ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÊÀÒÀËÎÃ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÆÈÄÊÎÑÒÜ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueBool(reader, "ÆÈÄÊÎÑÒÜ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎÄ_1Ñ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_1Ñ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueLong(reader, "ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", DbSql.GetValueString(reader, "ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			element.SetData("ÏÎÄÀĞÎÊ", DbSql.GetValueBool(reader, "ÏÎÄÀĞÎÊ"));
            element.SetData("ÒÎ", DbSql.GetValueBool(reader, "ÒÎ"));
            element.SetData("ÑÊÈÄÊÀ", DbSql.GetValueFloat(reader, "ÑÊÈÄÊÀ"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardDetail element = (DtCardDetail)MakeElement(reader);
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

		public static void SelectInList(DtCard card, ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			select.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(DtCard card, ArrayList array)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			select.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(long card_number, int card_year, ArrayList array)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value = card_number;
			select.Parameters["@card_year"].Value = card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array, DateTime start_date, DateTime end_date, long workshop, bool liquid, bool guaranty, bool cashless, bool inner)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_period.Parameters["@start_date"].Value = (DateTime)start_date;
			select_period.Parameters["@end_date"].Value = (DateTime)end_date;
			select_period.Parameters["@workshop"].Value = (long)workshop;
			select_period.Parameters["@liquid"].Value = (bool)liquid;
			select_period.Parameters["@guaranty"].Value = (bool)guaranty;
			select_period.Parameters["@cashless"].Value = (bool)cashless;
			select_period.Parameters["@inner"].Value = (bool)inner;
			DbSql.FillArray(array, select_period, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArraySpec1(ArrayList array, DateTime start_date, DateTime end_date)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_period_spec1.Parameters["@start_date"].Value = (DateTime)start_date;
			select_period_spec1.Parameters["@end_date"].Value = (DateTime)end_date;
			DbSql.FillArray(array, select_period_spec1, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardDetail Find(DtCard card, long position)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			find.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardDetail Find(long card_number, int card_year, long position)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@card_number"].Value = (long)card_number;
			find.Parameters["@card_year"].Value = (int)card_year;
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		// Äëÿ ñîâìåñòèìîñòè âåğñèé
		public static DtCardDetail Find(DbCard card, long position)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@card_number"].Value = (long)card.Number;
			find.Parameters["@card_year"].Value = (int)card.Year;
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool UpdateInput(DtCard card, long position, float input)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update_input.Parameters["@card_number"].Value	= (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			update_input.Parameters["@card_year"].Value		= (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			update_input.Parameters["@position"].Value		= (long)position;
			update_input.Parameters["@input"].Value		= (float)input;
			return DbSql.ExecuteCommandError(update_input);
		}

		public static bool UpdatePrice(DtCard card, long position, float price)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update_price.Parameters["@card_number"].Value	= (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			update_price.Parameters["@card_year"].Value		= (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			update_price.Parameters["@position"].Value		= (long)position;
			update_price.Parameters["@price"].Value			= (float)price;
			return DbSql.ExecuteCommandError(update_price);
		}

		public static bool UpdateGuaranty(DtCardDetail detail, DbGuarantyType guaranty, DbStaff mistake_initiator, string mistake)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå

			update_guaranty.Parameters["@card_number"].Value = (long)detail.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ");
			update_guaranty.Parameters["@card_year"].Value = (int)detail.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ");
			update_guaranty.Parameters["@position"].Value = (long)detail.GetData("ÏÎÇÈÖÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ");;
			if(guaranty != null)
				update_guaranty.Parameters["@guaranty"].Value = (long)guaranty.Code;
			else
				update_guaranty.Parameters["@guaranty"].Value = (long)0;
			if(mistake_initiator != null)
				update_guaranty.Parameters["@mistake_initiator"].Value = (long)mistake_initiator.Code;
			else
				update_guaranty.Parameters["@mistake_initiator"].Value = (long)0;
			update_guaranty.Parameters["@mistake"].Value = (string)mistake;
			return DbSql.ExecuteCommandError(update_guaranty);
		}

		public static bool UpdateGuaranty(long card_number, int card_year, long position, long code_guaranty, long code_responsible, string mistake)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå

			update_guaranty.Parameters["@card_number"].Value = (long)card_number;
			update_guaranty.Parameters["@card_year"].Value = (int)card_year;
			update_guaranty.Parameters["@position"].Value = (long)position;
			update_guaranty.Parameters["@guaranty"].Value = (long)code_guaranty;
			update_guaranty.Parameters["@mistake_initiator"].Value = (long)code_responsible;
			update_guaranty.Parameters["@mistake"].Value = (string)mistake;
			return DbSql.ExecuteCommandError(update_guaranty);
		}

		public static bool UpdatePresent(DtCard card, long position, bool present)
		{
			// Óñòàíîâêà îòìåòêè î ïîäàğêå
			update_present.Parameters["@card_number"].Value	= (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			update_present.Parameters["@card_year"].Value		= (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			update_present.Parameters["@position"].Value		= (long)position;
			update_present.Parameters["@present"].Value			= (bool)present;
			return DbSql.ExecuteCommandError(update_present);
		}

        public static bool UpdateTo(DtCard card, long position, bool to)
        {
            // Óñòàíîâêà îòìåòêè î ïîäàğêå
            update_to.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
            update_to.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
            update_to.Parameters["@position"].Value = (long)position;
            update_to.Parameters["@to"].Value = (bool)to;
            return DbSql.ExecuteCommandError(update_to);
        }
        public static bool SetTo(DtCard card, long position)
        {
            return UpdateTo(card, position, true);
        }
        public static bool UnsetTo(DtCard card, long position)
        {
            return UpdateTo(card, position, false);
        }

		public static bool SetPresent(DtCard card, long position)
		{
			return UpdatePresent(card, position, true);
		}
		public static bool UnsetPresent(DtCard card, long position)
		{
			return UpdatePresent(card, position, false);
		}
        public static bool SetDetailDiscount(DtCard card, long position, float dsc)
        {
            // Óñòàíîâêà îòìåòêè î ïîäàğêå
            update_discount.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
            update_discount.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
            update_discount.Parameters["@position"].Value = (long)position;
            update_discount.Parameters["@discount"].Value = (float)dsc;
            return DbSql.ExecuteCommandError(update_discount);
        }
	}
}
