using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlStaff.
	/// </summary>
	public class DbSqlStaff
	{
		private static SqlCommand select;
		private static SqlCommand select_executor;
		private static SqlCommand select_job;
		private static SqlCommand select_job_workshop;
		private static SqlCommand select_workshop;
		private static SqlCommand find;
		private static SqlCommand find_sign;
		private static SqlCommand select_job_staff;
		private static SqlCommand select_auto_restriction;

		private static SqlCommand _spSelectExecutor; // Список исполнителей заданный работы в карточке

		public DbSqlStaff()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Новая схема
		public static void SetSqlCommandValues(DtCardWork srcDtCardWork, SqlCommand srcSqlCommand)
		{
			DbSql.SetParameterValue("cardNumber", srcDtCardWork.CardNumber, srcSqlCommand);
			DbSql.SetParameterValue("card_number", srcDtCardWork.CardNumber, srcSqlCommand);
			DbSql.SetParameterValue("cardYear", srcDtCardWork.CardYear, srcSqlCommand);
			DbSql.SetParameterValue("card_year", srcDtCardWork.CardYear, srcSqlCommand);
			DbSql.SetParameterValue("position", srcDtCardWork.Position, srcSqlCommand);
			DbSql.SetParameterValue("number", srcDtCardWork.Position, srcSqlCommand);
		}
		public static object ReadStaff(SqlDataReader reader)
		{
			DtStaff dtStaff = new DtStaff();
			dtStaff.Code = DbSql.GetValueLong(reader, "КОД_ПЕРСОНАЛ");
			dtStaff.Surname = DbSql.GetValueString(reader, "ФАМИЛИЯ_ПЕРСОНАЛ");
			dtStaff.Name = DbSql.GetValueString(reader, "ИМЯ_ПЕРСОНАЛ");
			dtStaff.Patronymic = DbSql.GetValueString(reader, "ОТЧЕСТВО_ПЕРСОНАЛ");
			dtStaff.Login = DbSql.GetValueString(reader, "ЛОГИН");
			dtStaff.Avaliable = DbSql.GetValueBool(reader, "РАБОТАЕТ");
			dtStaff.Sign = DbSql.GetValueLong(reader, "ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ");
			dtStaff.CodeJob = DbSql.GetValueLong(reader, "ССЫЛКА_КОД_ДОЛЖНОСТЬ");
			dtStaff.CodeWorksjop = DbSql.GetValueLong(reader, "ССЫЛКА_КОД_ПОДРАЗДЕЛЕНИЕ");
			dtStaff.CoefSelary = DbSql.GetValueFloat(reader, "РАЗРЯД_КОЭФФИЦИЕНТ");
			dtStaff.Salary = DbSql.GetValueFloat(reader, "ОКЛАД");
			dtStaff.IsChg = false; // Отмечаем, что это не измененный элемент
			dtStaff.IsNew = false; // Отмечаем, что это не новый
			return dtStaff;
		}

		public static ArrayList SelectExecutors(DtCardWork srcCardWork) // Выбираем список исполнителей работы
		{
			if (srcCardWork == null) return null;
			SetSqlCommandValues(srcCardWork, _spSelectExecutor);
			ArrayList elements = DbSql.MakeArrayFromDatabase(_spSelectExecutor, new DbSql.DelegateReadDt(ReadStaff));
			return elements;
		}
		#endregion

		public static void Init(SqlConnection connection)
		{

			_spSelectExecutor = new SqlCommand("ПЕРСОНАЛ_FIND_ИСПОЛНИТЕЛЬ_РАБОТА", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spSelectExecutor);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spSelectExecutor);
			DbSql.AddParameter("position", DbSql.SQL_PARAMETER_TYPE.INT, _spSelectExecutor);
			DbSql.SetStoredProcedure(_spSelectExecutor);


			select = new SqlCommand("ПЕРСОНАЛ_ВЫБОРКА", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_executor = new SqlCommand("ПЕРСОНАЛ_ВЫБОРКА_ИСПОЛНИТЕЛЬ_РАБОТА", connection);
			select_executor.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_executor.Parameters.Add("@card_year", SqlDbType.Int);
			select_executor.Parameters.Add("@number", SqlDbType.Int);
			select_executor.CommandType = CommandType.StoredProcedure;

			select_job = new SqlCommand("ПЕРСОНАЛ_ВЫБОРКА_ДОЛЖНОСТЬ", connection);
			select_job.Parameters.Add("@code_job", SqlDbType.BigInt);
			select_job.CommandType = CommandType.StoredProcedure;

			select_job_workshop = new SqlCommand("ПЕРСОНАЛ_ВЫБОРКА_ДОЛЖНОСТЬ_ПОДРАЗДЕЛЕНИЕ", connection);
			select_job_workshop.Parameters.Add("@code_job", SqlDbType.BigInt);
			select_job_workshop.Parameters.Add("@code_workshop", SqlDbType.BigInt);
			select_job_workshop.CommandType = CommandType.StoredProcedure;

			select_workshop = new SqlCommand("ПЕРСОНАЛ_ВЫБОРКА_ПОДРАЗДЕЛЕНИЕ", connection);
			select_workshop.Parameters.Add("@code_workshop", SqlDbType.BigInt);
			select_workshop.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ПЕРСОНАЛ_ПОИСК", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;	

			find_sign = new SqlCommand("ПЕРСОНАЛ_ПОИСК_ЭЛЕКТРОННАЯ_ПОДПИСЬ", connection);
			find_sign.Parameters.Add("@sign", SqlDbType.BigInt);
			find_sign.CommandType = CommandType.StoredProcedure;
	
			select_auto_restriction = new SqlCommand("АВТОМОБИЛЬ_СПЕЦРАЗРЕШЕНИЕ_ВЫБОРКА_АВТОМОБИЛЬ", connection);
			select_auto_restriction.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_auto_restriction.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtStaff element = new DtStaff();
			element.SetData("КОД_ПЕРСОНАЛ", DbSql.GetValueLong(reader, "КОД_ПЕРСОНАЛ"));
			element.SetData("ФАМИЛИЯ_ПЕРСОНАЛ", DbSql.GetValueString(reader, "ФАМИЛИЯ_ПЕРСОНАЛ"));
			element.SetData("ИМЯ_ПЕРСОНАЛ", DbSql.GetValueString(reader, "ИМЯ_ПЕРСОНАЛ"));
			element.SetData("ОТЧЕСТВО_ПЕРСОНАЛ", DbSql.GetValueString(reader, "ОТЧЕСТВО_ПЕРСОНАЛ"));
			element.SetData("ЛОГИН", DbSql.GetValueString(reader, "ЛОГИН"));
			element.SetData("РАБОТАЕТ", DbSql.GetValueBool(reader, "РАБОТАЕТ"));
			element.SetData("ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ", DbSql.GetValueLong(reader, "ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ"));
			element.SetData("ССЫЛКА_КОД_ДОЛЖНОСТЬ", DbSql.GetValueLong(reader, "ССЫЛКА_КОД_ДОЛЖНОСТЬ"));
			element.SetData("ССЫЛКА_КОД_ПОДРАЗДЕЛЕНИЕ", DbSql.GetValueLong(reader, "ССЫЛКА_КОД_ПОДРАЗДЕЛЕНИЕ"));
			element.SetData("РАЗРЯД_КОЭФФИЦИЕНТ", DbSql.GetValueFloat(reader, "РАЗРЯД_КОЭФФИЦИЕНТ"));
			element.SetData("ОКЛАД", DbSql.GetValueFloat(reader, "ОКЛАД"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStaff element = (DtStaff)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Ошибка";
			}
			return item;
		}

		public static void SelectInArray(ArrayList array, long code_job)
		{
			// Подготовка команды поиска по маске
			select_job.Parameters["@code_job"].Value = (long)code_job;
			DbSql.FillArray(array, select_job, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(ArrayList array)
		{
			// Подготовка команды поиска по маске
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayExecutor(ArrayList array, long card_number, int card_year, long number)
		{
			// Подготовка команды поиска по маске
			select_executor.Parameters["@card_number"].Value	= (long)card_number;
			select_executor.Parameters["@card_year"].Value		= (int)card_year;
			select_executor.Parameters["@number"].Value			= (long)number;
			DbSql.FillArray(array, select_executor, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInList(ListView list)
		{
			// Подготовка команды поиска по маске
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
		public static void SelectInListJobWorkshop(ListView list, long code_job, long code_workshop)
		{
			// Подготовка команды поиска по маске
			select_job_workshop.Parameters["@code_job"].Value = (long)code_job;
			select_job_workshop.Parameters["@code_workshop"].Value = (long)code_workshop;
			DbSql.FillList(list, select_job_workshop, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
		public static void SelectInListWorkshop(ListView list, long code_workshop)
		{
			// Подготовка команды поиска по маске
			select_workshop.Parameters["@code_workshop"].Value = (long)code_workshop;
			DbSql.FillList(list, select_workshop, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
		public static void SelectInListJob(ListView list, long code_job)
		{
			// Подготовка команды поиска по маске
			select_job.Parameters["@code_job"].Value = (long)code_job;
			DbSql.FillList(list, select_job, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArrayAutoRestriction(ArrayList array, long code_auto)
		{
			// Подготовка команды поиска по маске
			select_auto_restriction.Parameters["@code_auto"].Value = (long)code_auto;
			DbSql.FillArray(array, select_auto_restriction, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtStaff Find(long code)
		{
			// Подготовка команды поиска по маске
			find.Parameters["@code"].Value = (long)code;
			return (DtStaff)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
		public static DtStaff FindSign(long sign)
		{
			// Подготовка команды поиска по маске
			find_sign.Parameters["@sign"].Value = (long)sign;
			return (DtStaff)DbSql.Find(find_sign, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
