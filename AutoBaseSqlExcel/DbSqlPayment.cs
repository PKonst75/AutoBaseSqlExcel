using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPayment.
	/// </summary>
	public class DbSqlPayment:DbSql
	{
		public static SqlCommand		insert;
		public static SqlCommand		select;
		public static SqlCommand		delete;
		public static SqlCommand		select_workshop;
		public static SqlCommand		select_card_department;
		public static SqlCommand		select_card_workshop_department;
		public static SqlCommand		select_auto;
		public static SqlCommand		find;
		public static SqlCommand		set_card;

		public DbSqlPayment()
		{
		}
		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÎÏËÀÒÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@code_department", SqlDbType.BigInt);
			insert.Parameters.Add("@code_workshop", SqlDbType.BigInt);
			insert.Parameters.Add("@summ", SqlDbType.Real);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.Parameters["@year"].Direction = ParameterDirection.Output;
			insert.Parameters["@date"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			delete = new SqlCommand("ÎÏËÀÒÀ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.Parameters.Add("@year", SqlDbType.Int);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			set_card = new SqlCommand("ÎÏËÀÒÀ_ÓÑÒÀÍÎÂÒÜ_ÊÀĞÒÎ×ÊÀ", connection);
			set_card.Parameters.Add("@code", SqlDbType.BigInt);
			set_card.Parameters.Add("@year", SqlDbType.Int);
			set_card.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_card.Parameters.Add("@card_year", SqlDbType.Int);
			set_card.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_card);

			select = new SqlCommand("ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;
			select.Parameters.Add("@date", SqlDbType.DateTime);

			select_workshop = new SqlCommand("ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ_ÏÎÄĞÀÇÄÅËÅÍÈÅ", connection);
			select_workshop.CommandType = CommandType.StoredProcedure;
			select_workshop.Parameters.Add("@date", SqlDbType.DateTime);
			select_workshop.Parameters.Add("@workshop", SqlDbType.BigInt);

			select_card_department = new SqlCommand("ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ_ÊÀĞÒÎ×ÊÀ_ÊÀÑÑÀ", connection);
			select_card_department.CommandType = CommandType.StoredProcedure;
			select_card_department.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_card_department.Parameters.Add("@card_year", SqlDbType.Int);
			select_card_department.Parameters.Add("@department", SqlDbType.BigInt);

			select_card_workshop_department = new SqlCommand("ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ_ÊÀĞÒÎ×ÊÀ_ÏÎÄĞÀÇÄÅËÅÍÈÅ_ÊÀÑÑÀ", connection);
			select_card_workshop_department.CommandType = CommandType.StoredProcedure;
			select_card_workshop_department.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_card_workshop_department.Parameters.Add("@card_year", SqlDbType.Int);
			select_card_workshop_department.Parameters.Add("@department", SqlDbType.BigInt);
			select_card_workshop_department.Parameters.Add("@workshop", SqlDbType.BigInt);

			select_auto = new SqlCommand("ÎÏËÀÒÀ_ÂÛÁÎĞÊÀ_ÀÂÒÎÌÎÁÈËÜ", connection);
			select_auto.CommandType = CommandType.StoredProcedure;
			select_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);

			find = new SqlCommand("ÎÏËÀÒÀ_ÏÎÈÑÊ", connection);
			find.CommandType = CommandType.StoredProcedure;
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.Parameters.Add("@year", SqlDbType.Int);
		}

		public static CS_Payment Insert(CS_Payment element)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			insert.Parameters["@code_department"].Value = element.code_department;
			insert.Parameters["@code_workshop"].Value	= element.code_workshop;
			insert.Parameters["@summ"].Value			= element.summ;
			insert.Parameters["@comment"].Value			= element.comment;
			insert.Parameters["@card_number"].Value		= element.card_number;
			insert.Parameters["@card_year"].Value		= element.card_year;
			insert.Parameters["@code_auto"].Value		= element.code_auto;
			insert.Parameters["@code_partner"].Value	= element.code_partner;
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.code	= (long)insert.Parameters["@code"].Value;
			element.year	= (int)insert.Parameters["@year"].Value;
			element.date	= (DateTime)insert.Parameters["@date"].Value;
			return element;
		}

		public static bool Delete(long code, int year)
		{
			delete.Parameters["@code"].Value	= (long)code;
			delete.Parameters["@year"].Value	= (long)year;
			return DbSql.ExecuteCommandError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			CS_Payment element			= new CS_Payment();
			element.code				= DbSql.GetValueLong(reader, "ÊÎÄ");
			element.year				= DbSql.GetValueInt(reader, "ÃÎÄ");
			element.date				= DbSql.GetValueDate(reader, "ÄÀÒÀ");
			element.code_department		= DbSql.GetValueLong(reader, "ÎÒÄÅË_ÊÀÑÑÀ");
			element.code_workshop		= DbSql.GetValueLong(reader, "ÏÎÄĞÀÇÄÅËÅÍÈÅ");
			element.summ				= DbSql.GetValueFloat(reader, "ÑÓÌÌÀ");
			element.comment				= DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ");
			element.card_number			= DbSql.GetValueLong(reader, "ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ");
			element.card_year			= DbSql.GetValueInt(reader, "ÊÀĞÒÎ×ÊÀ_ÃÎÄ");
			element.code_auto			= DbSql.GetValueLong(reader, "ÀÂÒÎÌÎÁÈËÜ");
			element.code_partner		= DbSql.GetValueLong(reader, "ÊÎÍÒĞÀÃÅÍÒ");
			element.supervisor_check	= DbSql.GetValueLong(reader, "ÏĞÎÂÅÄÅÍ");

			return (object)element;
		}

		public static void SelectInArray(ArrayList array, DateTime date)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@date"].Value = (DateTime)date;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayWorkshop(ArrayList array, DateTime date, long workshop)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_workshop.Parameters["@date"].Value = (DateTime)date;
			select_workshop.Parameters["@workshop"].Value = (long)workshop;
			DbSql.FillArray(array, select_workshop, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayCardDepartment(ArrayList array, long card_number, int card_year, long department)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_card_department.Parameters["@card_number"].Value = (long)card_number;
			select_card_department.Parameters["@card_year"].Value = (int)card_year;
			select_card_department.Parameters["@department"].Value = (long)department;
			DbSql.FillArray(array, select_card_department, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayCardWorkshopDepartment(ArrayList array, long card_number, int card_year, long workshop, long department)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_card_workshop_department.Parameters["@card_number"].Value = (long)card_number;
			select_card_workshop_department.Parameters["@card_year"].Value = (int)card_year;
			select_card_workshop_department.Parameters["@department"].Value = (long)department;
			select_card_workshop_department.Parameters["@workshop"].Value = (long)workshop;
			DbSql.FillArray(array, select_card_workshop_department, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayAuto(ArrayList array, long code_auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_auto.Parameters["@code_auto"].Value = (long)code_auto;
			DbSql.FillArray(array, select_auto, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static CS_Payment Find(long code, int year)
		{
			find.Parameters["@code"].Value	= (long)code;
			find.Parameters["@year"].Value	= (long)year;
			return (CS_Payment)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool SetPaymentCard(long card_number, long card_year, CS_Payment pay)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áğåíäó
			set_card.Parameters["@code"].Value			= pay.code;
			set_card.Parameters["@year"].Value			= pay.year;
			set_card.Parameters["@card_number"].Value	= card_number;
			set_card.Parameters["@card_year"].Value		= card_year;
			return DbSql.ExecuteCommandError(set_card);
		}
	}
}
