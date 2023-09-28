using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardAction.
	/// </summary>
	public class DbSqlCardAction
	{
		public static SqlCommand		select;
		public static SqlCommand		select_last;	// Выбор действий произведенных с карточкой
		public static SqlCommand		find_close;
		public static SqlCommand		cancel_close;
		public static SqlCommand		delete_close;
		public static SqlCommand		reclose;

		public static SqlCommand _spSelect; // Сохраненная процедура выборки действий для определенной карточки
		public static SqlCommand _spInsert; // Сохраненная процедура добавления действия для определенной карточки
		public static SqlCommand _spSelectEndWork; // Сохраненная процедура поиска отметки об окончании ремонта

		public DbSqlCardAction()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void SetSqlCommandValues(DtCardAction srcDtCardAction, SqlCommand srcSqlCommand)
		{
			DbSql.SetParameterValue("cardNumber", srcDtCardAction.CardNumber, srcSqlCommand);
			DbSql.SetParameterValue("cardYear", srcDtCardAction.CardYear, srcSqlCommand);
			DbSql.SetParameterValue("date", srcDtCardAction.Date, srcSqlCommand);
			DbSql.SetParameterValue("actionCode", srcDtCardAction.ActionCode, srcSqlCommand);
			DbSql.SetParameterValue("comment", srcDtCardAction.Comment, srcSqlCommand);
		}
		public static object ReadCardAction(SqlDataReader reader)
		{
			DtCardAction dtCardAction = new DtCardAction();
			dtCardAction.CardNumber = DbSql.GetValueLong(reader, "ССЫЛКА_НОМЕР_КАРТОЧКА");
			dtCardAction.CardYear = DbSql.GetValueInt(reader, "ССЫЛКА_ГОД_КАРТОЧКА");
			dtCardAction.Date = DbSql.GetValueDate(reader, "ДАТА");
			dtCardAction.ActionCode = (DtCard.CardState)DbSql.GetValueShort(reader, "КОД");
			dtCardAction.Comment = DbSql.GetValueString(reader, "ПРИМЕЧАНИЕ");
			dtCardAction.IsChg = false; // Отмечаем, что это не измененный элемент
			dtCardAction.IsNew = false; // Отмечаем, что это не новый
			return dtCardAction;
		}
		public static object ReadCardActionWorkend(SqlDataReader reader)
		{
			DtCardAction dtCardAction = new DtCardAction();
			dtCardAction.CardNumber = DbSql.GetValueLong(reader, "НОМЕР_КАРТОЧКА");
			dtCardAction.CardYear = DbSql.GetValueInt(reader, "ГОД_КАРТОЧКА");
			dtCardAction.Date = DbSql.GetValueDate(reader, "ДАТА");
			dtCardAction.ActionCode = DtCard.CardState.MARKWORKEND;
			dtCardAction.IsChg = false; // Отмечаем, что это не измененный элемент
			dtCardAction.IsNew = false; // Отмечаем, что это не новый
			return dtCardAction;
		}

		public static ArrayList Select(DtCard srcCard)
		{
			if (srcCard == null) return null;
			DtCardAction cardAction = new DtCardAction(srcCard);
			SetSqlCommandValues(cardAction, _spSelect);
			SetSqlCommandValues(cardAction, _spSelectEndWork);
			ArrayList elements = DbSql.MakeArrayFromDatabase(_spSelect, new DbSql.DelegateReadDt(ReadCardAction));
			// Догружаем отметку об окончании ремонта
			ArrayList elemenst1 = DbSql.MakeArrayFromDatabase(_spSelectEndWork, new DbSql.DelegateReadDt(ReadCardActionWorkend));
			elements.AddRange(elemenst1);
			return elements;
		}
		public static DtCardAction Insert(DtCardAction srcCardAction)
        {
			if (srcCardAction == null) return null;
			SetSqlCommandValues(srcCardAction, _spInsert);
			DbSql.SetParameterOutput("date", _spInsert);
			if (DbSql.ExecuteCommandError(_spInsert))
				srcCardAction.Date = (DateTime)DbSql.GetParameterValue("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			else
				srcCardAction = null;
			return srcCardAction;
		}
		public static void Init(SqlConnection connection)
		{
			_spSelect = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_SELECT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spSelect);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spSelect);
			DbSql.SetStoredProcedure(_spSelect);

			_spSelectEndWork = new SqlCommand("КАРТОЧКА_ОТМЕТКА_ОКОНЧАНИЕ_РЕМОНТА_SELECT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spSelectEndWork);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spSelectEndWork);
			DbSql.SetStoredProcedure(_spSelectEndWork);

			_spInsert = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_INSERT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spInsert);
			DbSql.AddParameter("actionCode", DbSql.SQL_PARAMETER_TYPE.SHORT, _spInsert);
			DbSql.AddParameter("comment", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			DbSql.SetReturnError(_spInsert);
			DbSql.SetStoredProcedure(_spInsert);




			select = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_ВЫБОРКА", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;

			select_last = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_ПОСЛЕДНЕЕ", connection);
			select_last.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_last.Parameters.Add("@card_year", SqlDbType.Int);
			select_last.CommandType = CommandType.StoredProcedure;

			find_close = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_ПОИСК_ЗАКРЫТИЕ", connection);
			find_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			find_close.Parameters.Add("@card_year", SqlDbType.Int);
			find_close.CommandType = CommandType.StoredProcedure;

			cancel_close = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_ОТМЕНА_ЗАКРЫТИЕ", connection);
			cancel_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			cancel_close.Parameters.Add("@card_year", SqlDbType.Int);
			cancel_close.Parameters.Add("@comment", SqlDbType.VarChar);
			cancel_close.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(cancel_close);

			delete_close = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_УДАЛЕНИЕ_ЗАКРЫТИЕ", connection);
			delete_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			delete_close.Parameters.Add("@card_year", SqlDbType.Int);
			delete_close.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete_close);

			reclose = new SqlCommand("КАРТОЧКА_ДЕЙСТВИЕ_ВОССТАНОВЛЕНИЕ_ЗАКРЫТИЕ", connection);
			reclose.Parameters.Add("@card_number", SqlDbType.BigInt);
			reclose.Parameters.Add("@card_year", SqlDbType.Int);
			reclose.Parameters.Add("@comment", SqlDbType.VarChar);
			reclose.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reclose);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardAction element			= new DtCardAction();
			element.SetData("ССЫЛКА_НОМЕР_КАРТОЧКА", DbSql.GetValueLong(reader, "ССЫЛКА_НОМЕР_КАРТОЧКА"));
			element.SetData("ССЫЛКА_ГОД_КАРТОЧКА", DbSql.GetValueInt(reader, "ССЫЛКА_ГОД_КАРТОЧКА"));
			element.SetData("КОД", DbSql.GetValueShort(reader, "КОД"));
			element.SetData("ДАТА", DbSql.GetValueDate(reader, "ДАТА"));
			element.SetData("ПРИМЕЧАНИЕ", DbSql.GetValueString(reader, "ПРИМЕЧАНИЕ"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardAction element = (DtCardAction)MakeElement(reader);
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

		public static void SelectInList(ListView list, DtCard card)
		{
			// Подготовка команды поиска по маске
			select.Parameters["@card_number"].Value = (long)card.GetData("НОМЕР_КАРТОЧКА");
			select.Parameters["@card_year"].Value	= (int)card.GetData("ГОД_КАРТОЧКА");
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtCardAction FindClose(DtCard card)
		{
			// Подготовка команды поиска по маске
			find_close.Parameters["@card_number"].Value = (long)card.GetData("НОМЕР_КАРТОЧКА");
			find_close.Parameters["@card_year"].Value	= (int)card.GetData("ГОД_КАРТОЧКА");
			return (DtCardAction)DbSql.Find(find_close, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardAction SelectLast(DtCard card)
		{
			// Подготовка команды поиска по маске
			select_last.Parameters["@card_number"].Value = (long)card.GetData("НОМЕР_КАРТОЧКА");
			select_last.Parameters["@card_year"].Value	= (int)card.GetData("ГОД_КАРТОЧКА");
			return (DtCardAction)DbSql.Find(select_last, new DbSql.DelegateMakeElement(MakeElement));
		}
		public static DtCardAction SelectLast(long number, int year)
		{
			// Подготовка команды поиска по маске
			select_last.Parameters["@card_number"].Value = (long)number;
			select_last.Parameters["@card_year"].Value	= (int)year;
			return (DtCardAction)DbSql.Find(select_last, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool CancelClose(DtCard card, string comment)
		{
			// Подготовка команды поиска по маске
			cancel_close.Parameters["@card_number"].Value	= (long)card.GetData("НОМЕР_КАРТОЧКА");
			cancel_close.Parameters["@card_year"].Value		= (int)card.GetData("ГОД_КАРТОЧКА");
			cancel_close.Parameters["@comment"].Value		= (string)comment;
			return DbSql.ExecuteCommandError(cancel_close);
		}
		public static bool DeleteClose(DtCard card)
		{
			// Подготовка команды поиска по маске
			delete_close.Parameters["@card_number"].Value	= (long)card.GetData("НОМЕР_КАРТОЧКА");
			delete_close.Parameters["@card_year"].Value		= (int)card.GetData("ГОД_КАРТОЧКА");
			return DbSql.ExecuteCommandError(delete_close);
		}

		public static bool ReClose(DtCard card, string comment)
		{
			// Подготовка команды поиска по маске
			reclose.Parameters["@card_number"].Value = (long)card.GetData("НОМЕР_КАРТОЧКА");
			reclose.Parameters["@card_year"].Value	= (int)card.GetData("ГОД_КАРТОЧКА");
			reclose.Parameters["@comment"].Value		= (string)comment;
			return DbSql.ExecuteCommandError(reclose);
		}
	}
}
