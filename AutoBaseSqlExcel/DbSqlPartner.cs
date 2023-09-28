using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartner.
	/// </summary>
	public class DbSqlPartner
	{
		public static SqlCommand select;
		public static SqlCommand select_title;
		public static SqlCommand select_birthday;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand delete;
		public static SqlCommand update;

		public DbSqlPartner()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_title = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÂÛÁÎĞÊÀ_ÍÀÈÌÅÍÎÂÀÍÈÅ", connection);
			select_title.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_title.CommandType = CommandType.StoredProcedure;

			select_birthday = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÂÛÁÎĞÊÀ_ÄÍÈ_ĞÎÆÄÅÍÈß", connection);
			select_birthday.Parameters.Add("@date", SqlDbType.DateTime);
			select_birthday.CommandType = CommandType.StoredProcedure;


			find = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÈÇÌÅÍÅÍÈÅ", connection);
			// Îáùàÿ ÷àñòü
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@title", SqlDbType.VarChar, 64);
			update.Parameters.Add("@comment", SqlDbType.VarChar, 60);
			update.Parameters.Add("@inn", SqlDbType.VarChar, 25);
			// ×àñòü êàñàşùàÿñÿ ôèçè÷åñêîãî ëèöà
			update.Parameters.Add("@surname", SqlDbType.VarChar, 25);
			update.Parameters.Add("@name", SqlDbType.VarChar, 25);
			update.Parameters.Add("@patronymic", SqlDbType.VarChar, 25);
			update.Parameters.Add("@registration", SqlDbType.VarChar, 255);
			update.Parameters.Add("@birthday", SqlDbType.DateTime);
			update.Parameters.Add("@is_birthday", SqlDbType.Bit);
			update.Parameters.Add("@address_living", SqlDbType.VarChar, 255);
			// ×àñòü êàñàşùàÿñÿ şğèäè÷åñêîãî ëèöà
			update.Parameters.Add("@name_juridical", SqlDbType.VarChar, 1024);
			update.Parameters.Add("@address_juridical", SqlDbType.VarChar, 255);
			update.Parameters.Add("@address_fact", SqlDbType.VarChar, 255);
			update.Parameters.Add("@contact", SqlDbType.VarChar, 256);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			insert = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÄÎÁÀÂËÅÍÈÅ", connection);
			// Îáùàÿ ÷àñòü
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@juridical", SqlDbType.Bit);
			insert.Parameters.Add("@title", SqlDbType.VarChar);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@inn", SqlDbType.VarChar);
			// ×àñòü êàñàşùàÿñÿ ôèçè÷åñêîãî ëèöà
			insert.Parameters.Add("@surname", SqlDbType.VarChar);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@patronymic", SqlDbType.VarChar);
			insert.Parameters.Add("@registration", SqlDbType.VarChar);
			insert.Parameters.Add("@birthday", SqlDbType.DateTime);
			insert.Parameters.Add("@is_birthday", SqlDbType.Bit);
			insert.Parameters.Add("@address_living", SqlDbType.VarChar);
			// ×àñòü êàñàşùàÿñÿ şğèäè÷åñêîãî ëèöà
			insert.Parameters.Add("@name_juridical", SqlDbType.VarChar);
			insert.Parameters.Add("@address_juridical", SqlDbType.VarChar);
			insert.Parameters.Add("@address_fact", SqlDbType.VarChar);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			delete = new SqlCommand("ÊÎÍÒĞÀÃÅÍÒ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartner element			= new DtPartner();
			element.SetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ"));
			element.SetData("ÊÎÌÅÍÒÀĞÈÉ", DbSql.GetValueString(reader, "ÊÎÌÅÍÒÀĞÈÉ"));
			element.SetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ", DbSql.GetValueBool(reader, "ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ"));
			element.SetData("ÈÍÍ", DbSql.GetValueString(reader, "ÈÍÍ"));
			if((bool)element.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ") == false)
			{
				element.SetData("ÔÈÇÈ×ÅÑÊÎÅ", DbSqlPartnerPerson.MakeElement(reader));
			}
			else
			{
				element.SetData("ŞĞÈÄÈ×ÅÑÊÎÅ", DbSqlPartnerJuridical.MakeElement(reader));
			}
			// Áóäåì îòêàçûâàòüñÿ
			element.SetData("ÒÅËÅÔÎÍ", DbSql.GetValueString(reader, "ÒÅËÅÔÎÍ"));
			element.SetData("ÊÎÍÒÀÊÒ_ÒÅËÅÔÎÍ", DbSql.GetValueString(reader, "ÊÎÍÒÀÊÒ_ÒÅËÅÔÎÍ"));
			return (object)element;
		}

		public static object MakeElement2(SqlDataReader reader, Dt srcPartner = null)
		{
			DtPartner element;
			if (srcPartner == null)
				element = new DtPartner();
			else
				element = (DtPartner)srcPartner;
			element.SetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", DbSql.GetValueLong(reader, "ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ"));
			element.SetData("ÊÎÌÅÍÒÀĞÈÉ", DbSql.GetValueString(reader, "ÊÎÌÅÍÒÀĞÈÉ"));
			element.SetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ", DbSql.GetValueBool(reader, "ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ"));
			element.SetData("ÈÍÍ", DbSql.GetValueString(reader, "ÈÍÍ"));
			if ((bool)element.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ") == false)
			{
				element.SetData("ÔÈÇÈ×ÅÑÊÎÅ", DbSqlPartnerPerson.MakeElement(reader));
			}
			else
			{
				element.SetData("ŞĞÈÄÈ×ÅÑÊÎÅ", DbSqlPartnerJuridical.MakeElement(reader));
			}
			// Áóäåì îòêàçûâàòüñÿ
			element.SetData("ÒÅËÅÔÎÍ", DbSql.GetValueString(reader, "ÒÅËÅÔÎÍ"));
			element.SetData("ÊÎÍÒÀÊÒ_ÒÅËÅÔÎÍ", DbSql.GetValueString(reader, "ÊÎÍÒÀÊÒ_ÒÅËÅÔÎÍ"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtPartner element = (DtPartner)MakeElement(reader);
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

		public static void SelectInList(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			select_title.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_title, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			select_title.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillArray(array, select_title, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array, DateTime date)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			select_birthday.Parameters["@date"].Value = date;
			DbSql.FillArray(array, select_birthday, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInList(ListView list, DateTime date)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			select_birthday.Parameters["@date"].Value = date;
			DbSql.FillList(list, select_birthday, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}


		public static DtPartner Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			find.Parameters["@code"].Value = (long)code;
			return (DtPartner) DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void LoadFromDatabase(DtPartner srcPartner)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			if (srcPartner == null) return;
			find.Parameters["@code"].Value = (long)srcPartner.Code;
			DbSql.LoadFromDatabase(find, new DbSql.DelegateMakeElement2(MakeElement2), srcPartner);
		}

		public static bool Update(DtPartner partner)
		{
			// Ïîêà âñå îáùåå
			update.Parameters["@code"].Value		= (long)partner.GetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ");
			update.Parameters["@title"].Value		= (string)partner.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ");
			update.Parameters["@comment"].Value		= (string)partner.GetData("ÊÎÌÅÍÒÀĞÈÉ");
			update.Parameters["@inn"].Value			= (string)partner.GetData("ÈÍÍ");
			// Òî ÷òî êàñàåòüñÿ ôèçè÷åñêîãî ëèöà
			if((bool)partner.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ") == false)
			{
				DbSqlPartnerPerson.Update((DtPartnerPerson)partner.GetData("ÔÈÇÈ×ÅÑÊÎÅ"), update);
				DbSqlPartnerJuridical.Update(new DtPartnerJuridical(), update);
			}
			else
			{
				DbSqlPartnerPerson.Update(new DtPartnerPerson(), update);
				DbSqlPartnerJuridical.Update((DtPartnerJuridical)partner.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ"), update);
			}
			return DbSql.ExecuteCommandError(update);
		}

		public static DtPartner Insert(DtPartner partner)
		{
			// Ïîêà âñå îáùåå
			insert.Parameters["@code"].Value		= (long)partner.GetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ");
			insert.Parameters["@title"].Value		= (string)partner.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ");
			insert.Parameters["@comment"].Value		= (string)partner.GetData("ÊÎÌÅÍÒÀĞÈÉ");
			insert.Parameters["@inn"].Value			= (string)partner.GetData("ÈÍÍ");
			// Òî ÷òî êàñàåòüñÿ ôèçè÷åñêîãî ëèöà
			if((bool)partner.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ") == false)
			{
				insert.Parameters["@juridical"].Value		= false;
				DbSqlPartnerPerson.Insert((DtPartnerPerson)partner.GetData("ÔÈÇÈ×ÅÑÊÎÅ"), insert);
				DbSqlPartnerJuridical.Insert(new DtPartnerJuridical(), insert);
			}
			else
			{
				insert.Parameters["@juridical"].Value		= true;
				DbSqlPartnerPerson.Insert(new DtPartnerPerson(), insert);
				DbSqlPartnerJuridical.Insert((DtPartnerJuridical)partner.GetData("ŞĞÈÄÈ×ÅÑÊÎÅ"), insert);
			}
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			partner.SetData("ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", (long)insert.Parameters["@code"].Value);
			return partner;
		}

		public static bool Delete(long code)
		{
			// Ïîäãîòîâêà êîìàíäû âûáîğà
			delete.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete);
		}

	}
}
