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
		public static SqlCommand		select_last;	// ����� �������� ������������� � ���������
		public static SqlCommand		find_close;
		public static SqlCommand		cancel_close;
		public static SqlCommand		delete_close;
		public static SqlCommand		reclose;

		public static SqlCommand _spSelect; // ����������� ��������� ������� �������� ��� ������������ ��������
		public static SqlCommand _spInsert; // ����������� ��������� ���������� �������� ��� ������������ ��������
		public static SqlCommand _spSelectEndWork; // ����������� ��������� ������ ������� �� ��������� �������

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
			dtCardAction.CardNumber = DbSql.GetValueLong(reader, "������_�����_��������");
			dtCardAction.CardYear = DbSql.GetValueInt(reader, "������_���_��������");
			dtCardAction.Date = DbSql.GetValueDate(reader, "����");
			dtCardAction.ActionCode = (DtCard.CardState)DbSql.GetValueShort(reader, "���");
			dtCardAction.Comment = DbSql.GetValueString(reader, "����������");
			dtCardAction.IsChg = false; // ��������, ��� ��� �� ���������� �������
			dtCardAction.IsNew = false; // ��������, ��� ��� �� �����
			return dtCardAction;
		}
		public static object ReadCardActionWorkend(SqlDataReader reader)
		{
			DtCardAction dtCardAction = new DtCardAction();
			dtCardAction.CardNumber = DbSql.GetValueLong(reader, "�����_��������");
			dtCardAction.CardYear = DbSql.GetValueInt(reader, "���_��������");
			dtCardAction.Date = DbSql.GetValueDate(reader, "����");
			dtCardAction.ActionCode = DtCard.CardState.MARKWORKEND;
			dtCardAction.IsChg = false; // ��������, ��� ��� �� ���������� �������
			dtCardAction.IsNew = false; // ��������, ��� ��� �� �����
			return dtCardAction;
		}

		public static ArrayList Select(DtCard srcCard)
		{
			if (srcCard == null) return null;
			DtCardAction cardAction = new DtCardAction(srcCard);
			SetSqlCommandValues(cardAction, _spSelect);
			SetSqlCommandValues(cardAction, _spSelectEndWork);
			ArrayList elements = DbSql.MakeArrayFromDatabase(_spSelect, new DbSql.DelegateReadDt(ReadCardAction));
			// ��������� ������� �� ��������� �������
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
			_spSelect = new SqlCommand("��������_��������_SELECT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spSelect);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spSelect);
			DbSql.SetStoredProcedure(_spSelect);

			_spSelectEndWork = new SqlCommand("��������_�������_���������_�������_SELECT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spSelectEndWork);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spSelectEndWork);
			DbSql.SetStoredProcedure(_spSelectEndWork);

			_spInsert = new SqlCommand("��������_��������_INSERT", connection);
			DbSql.AddParameter("cardNumber", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("cardYear", DbSql.SQL_PARAMETER_TYPE.INT, _spInsert);
			DbSql.AddParameter("actionCode", DbSql.SQL_PARAMETER_TYPE.SHORT, _spInsert);
			DbSql.AddParameter("comment", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			DbSql.SetReturnError(_spInsert);
			DbSql.SetStoredProcedure(_spInsert);




			select = new SqlCommand("��������_��������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;

			select_last = new SqlCommand("��������_��������_���������", connection);
			select_last.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_last.Parameters.Add("@card_year", SqlDbType.Int);
			select_last.CommandType = CommandType.StoredProcedure;

			find_close = new SqlCommand("��������_��������_�����_��������", connection);
			find_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			find_close.Parameters.Add("@card_year", SqlDbType.Int);
			find_close.CommandType = CommandType.StoredProcedure;

			cancel_close = new SqlCommand("��������_��������_������_��������", connection);
			cancel_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			cancel_close.Parameters.Add("@card_year", SqlDbType.Int);
			cancel_close.Parameters.Add("@comment", SqlDbType.VarChar);
			cancel_close.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(cancel_close);

			delete_close = new SqlCommand("��������_��������_��������_��������", connection);
			delete_close.Parameters.Add("@card_number", SqlDbType.BigInt);
			delete_close.Parameters.Add("@card_year", SqlDbType.Int);
			delete_close.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete_close);

			reclose = new SqlCommand("��������_��������_��������������_��������", connection);
			reclose.Parameters.Add("@card_number", SqlDbType.BigInt);
			reclose.Parameters.Add("@card_year", SqlDbType.Int);
			reclose.Parameters.Add("@comment", SqlDbType.VarChar);
			reclose.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reclose);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardAction element			= new DtCardAction();
			element.SetData("������_�����_��������", DbSql.GetValueLong(reader, "������_�����_��������"));
			element.SetData("������_���_��������", DbSql.GetValueInt(reader, "������_���_��������"));
			element.SetData("���", DbSql.GetValueShort(reader, "���"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
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
				item.Text			= "������";
			}
			return item;
		}

		public static void SelectInList(ListView list, DtCard card)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			select.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtCardAction FindClose(DtCard card)
		{
			// ���������� ������� ������ �� �����
			find_close.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			find_close.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			return (DtCardAction)DbSql.Find(find_close, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardAction SelectLast(DtCard card)
		{
			// ���������� ������� ������ �� �����
			select_last.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			select_last.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			return (DtCardAction)DbSql.Find(select_last, new DbSql.DelegateMakeElement(MakeElement));
		}
		public static DtCardAction SelectLast(long number, int year)
		{
			// ���������� ������� ������ �� �����
			select_last.Parameters["@card_number"].Value = (long)number;
			select_last.Parameters["@card_year"].Value	= (int)year;
			return (DtCardAction)DbSql.Find(select_last, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool CancelClose(DtCard card, string comment)
		{
			// ���������� ������� ������ �� �����
			cancel_close.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			cancel_close.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			cancel_close.Parameters["@comment"].Value		= (string)comment;
			return DbSql.ExecuteCommandError(cancel_close);
		}
		public static bool DeleteClose(DtCard card)
		{
			// ���������� ������� ������ �� �����
			delete_close.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			delete_close.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			return DbSql.ExecuteCommandError(delete_close);
		}

		public static bool ReClose(DtCard card, string comment)
		{
			// ���������� ������� ������ �� �����
			reclose.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			reclose.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			reclose.Parameters["@comment"].Value		= (string)comment;
			return DbSql.ExecuteCommandError(reclose);
		}
	}
}
