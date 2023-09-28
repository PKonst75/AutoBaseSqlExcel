using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardWork.
	/// </summary>
	public class DbSqlCardWork
	{
		private static SqlCommand create;
		private static SqlCommand select;
		private static SqlCommand select_personal;
		private static SqlCommand select_personal_interval;
		private static SqlCommand select_period;
		private static SqlCommand find;
		private static SqlCommand update_guaranty;
		private static SqlCommand update_cost;
		private static SqlCommand update_val;
        private static SqlCommand update_discount;
		private static SqlCommand select_executor;
		private static SqlCommand updateValues;

		public DbSqlCardWork()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			create = new SqlCommand("��������_������������", connection);
			create.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			create.Parameters.Add("@cardYear", SqlDbType.Int);
			create.Parameters.Add("@number", SqlDbType.Int);
			create.Parameters.Add("@codeWork", SqlDbType.BigInt);
			create.Parameters.Add("@val", SqlDbType.Real);
			create.Parameters.Add("@price", SqlDbType.Real);
			create.Parameters.Add("@guaranty", SqlDbType.Bit);
			create.Parameters.Add("@quontity", SqlDbType.Float);
			create.Parameters.Add("@oil", SqlDbType.Bit);
			create.Parameters.Add("@discount", SqlDbType.Real);
			DbSql.SetReturnError(create);
			create.CommandType = CommandType.StoredProcedure;
			create.Parameters["@number"].Direction = ParameterDirection.InputOutput;

			select_executor = new SqlCommand("��������_������_�����������", connection);
			select_executor.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_executor.Parameters.Add("@card_year", SqlDbType.Int);
			select_executor.Parameters.Add("@work_number", SqlDbType.BigInt);
			select_executor.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("��������_������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;

			select_personal = new SqlCommand("��������_������_�������_��������", connection);
			select_personal.Parameters.Add("@code_personal", SqlDbType.BigInt);
			select_personal.Parameters.Add("@year", SqlDbType.Int);
			select_personal.Parameters.Add("@month", SqlDbType.Int);
			select_personal.CommandType = CommandType.StoredProcedure;

			select_personal_interval = new SqlCommand("��������_������_�������_��������_��������", connection);
			select_personal_interval.Parameters.Add("@code_personal", SqlDbType.BigInt);
			select_personal_interval.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_personal_interval.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_personal_interval.CommandType = CommandType.StoredProcedure;

			select_period = new SqlCommand("��������_������_�������_������", connection);
			select_period.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_period.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("��������_������_�����", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.Parameters.Add("@position", SqlDbType.Int);
			find.CommandType = CommandType.StoredProcedure;

			update_guaranty = new SqlCommand("��������_������_��������_��������", connection);
			update_guaranty.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@card_year", SqlDbType.Int);
			update_guaranty.Parameters.Add("@position", SqlDbType.Int);
			update_guaranty.Parameters.Add("@guaranty", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake_initiator", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake", SqlDbType.VarChar);
			update_guaranty.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_guaranty);

			update_cost = new SqlCommand("��������_������_��������_��������", connection);
			update_cost.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_cost.Parameters.Add("@card_year", SqlDbType.Int);
			update_cost.Parameters.Add("@position", SqlDbType.Int);
			update_cost.Parameters.Add("@cost", SqlDbType.Float);
			update_cost.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_cost);

			update_val = new SqlCommand("��������_������_��������_������������", connection);
			update_val.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_val.Parameters.Add("@card_year", SqlDbType.Int);
			update_val.Parameters.Add("@position", SqlDbType.Int);
			update_val.Parameters.Add("@val", SqlDbType.Float);
			update_val.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_val);

            update_discount = new SqlCommand("��������_������_��������_������", connection);
            update_discount.Parameters.Add("@card_number", SqlDbType.BigInt);
            update_discount.Parameters.Add("@card_year", SqlDbType.Int);
            update_discount.Parameters.Add("@position", SqlDbType.Int);
            update_discount.Parameters.Add("@discount", SqlDbType.Float);
            update_discount.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_discount);

			updateValues = new SqlCommand("��������_������_��������_������", connection);
			updateValues.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			updateValues.Parameters.Add("@cardYear", SqlDbType.Int);
			updateValues.Parameters.Add("@number", SqlDbType.Int);
			updateValues.Parameters.Add("@val", SqlDbType.Real);
			updateValues.Parameters.Add("@price", SqlDbType.Real);
			updateValues.Parameters.Add("@guaranty", SqlDbType.Bit);
			updateValues.Parameters.Add("@quontity", SqlDbType.Float);
			updateValues.Parameters.Add("@oil", SqlDbType.Bit);
			updateValues.Parameters.Add("@discount", SqlDbType.Real);
			DbSql.SetReturnError(updateValues);
			updateValues.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardWork element = new DtCardWork();
			element.SetData("�����_��������_��������_������", DbSql.GetValueLong(reader, "�����_��������_��������_������"));
			element.SetData("���_��������_��������_������", DbSql.GetValueInt(reader, "���_��������_��������_������"));
			element.SetData("�������_��������_������", DbSql.GetValueInt(reader, "�������_��������_������"));
			element.SetData("���_������������_��������_������", DbSql.GetValueLong(reader, "���_������������_��������_������"));
			element.SetData("������������_��������_������", DbSql.GetValueString(reader, "������������_��������_������"));
			element.SetData("�����_�������_��������_������", DbSql.GetValueString(reader, "�����_�������_��������_������"));
			element.SetData("����������_��������_������", DbSql.GetValueFloat(reader, "����������_��������_������"));
			element.SetData("������������_��������_������", DbSql.GetValueFloat(reader, "������������_��������_������"));
			element.SetData("��������_��������_������", DbSql.GetValueFloat(reader, "��������_��������_������"));
			element.SetData("��������_��������_������", DbSql.GetValueBool(reader, "��������_��������_������"));
			element.SetData("�����_��������_������", DbSql.GetValueBool(reader, "�����_��������_������"));
			element.SetData("������_��������_������", DbSql.GetValueFloat(reader, "������_��������_������"));
			element.SetData("���������_��������_������", DbSql.GetValueLong(reader, "���������_��������_������"));
			element.SetData("���������_������������_��������_������", DbSql.GetValueString(reader, "���������_������������_��������_������"));
			element.SetData("��������_���_��������_������", DbSql.GetValueLong(reader, "��������_���_��������_������"));
			element.SetData("��������_���_������������_��������_������", DbSql.GetValueString(reader, "��������_���_������������_��������_������"));
			element.SetData("�����_��������_������", DbSql.GetValueString(reader, "�����_��������_������"));
			element.SetData("����������_������������", DbSql.GetValueInt(reader, "����������_������������"));
			element.SetData("������_���_����������_������������", DbSql.GetValueLong(reader, "������_���_����������_������������"));
			element.SetData("������", DbSql.GetValueBool(reader, "������"));
			element.SetData("�����������_���", DbSql.GetValueLong(reader, "�����������_���"));
			element.SetData("����������_��������", DbSql.GetValueBool(reader, "����������_��������"));
			element.SetData("��������_������_���_����������", DbSql.GetValueLong(reader, "��������_������_���_����������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardWork element = (DtCardWork)MakeElement(reader);
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

		public static void SelectInList(DtCard card, ListView list)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			select.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		// ����� ������� ���������� �������
		public static void SelectInArray(SqlCommand cmd, ArrayList array)
		{
			// ���������� ������� �������� ��������
			DbSql.FillArray(array, cmd, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(DtCard card, ArrayList array)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			select.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(long card_number, int card_year, ArrayList array)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value = card_number;
			select.Parameters["@card_year"].Value = card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(long code_personal, int year, int month, ArrayList array)
		{
			// ���������� ������� ������ �� �����
			select_personal.Parameters["@code_personal"].Value	= (long)code_personal;
			select_personal.Parameters["@year"].Value			= (int)year;
			select_personal.Parameters["@month"].Value			= (int)month;
			DbSql.FillArray(array, select_personal, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(long code_personal, DateTime start_date, DateTime end_date, ArrayList array)
		{
			// ���������� ������� ������ �� �����
			select_personal_interval.Parameters["@code_personal"].Value	= (long)code_personal;
			select_personal_interval.Parameters["@start_date"].Value		= (DateTime)start_date;
			select_personal_interval.Parameters["@end_date"].Value			= (DateTime)end_date;
			DbSql.FillArray(array, select_personal_interval, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArray(DateTime start_date, DateTime end_date, long workshop, ArrayList array)
		{
			// ���������� ������� ������ �� �����
			select_period.Parameters["@start_date"].Value		= (DateTime)start_date;
			select_period.Parameters["@end_date"].Value			= (DateTime)end_date;
			select_period.Parameters["@workshop"].Value			= (long)workshop;
			DbSql.FillArray(array, select_period, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardWork Find(DtCard card, int position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			find.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			find.Parameters["@position"].Value = (int)position;
			return (DtCardWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		// ��� ������������� ������
		public static DtCardWork Find(DbCard card, int position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card.Number;
			find.Parameters["@card_year"].Value = (int)card.Year;
			find.Parameters["@position"].Value = (int)position;
			return (DtCardWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardWork Find(DbCardWork work)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)work.CardNumber;
			find.Parameters["@card_year"].Value = (int)work.CardYear;
			find.Parameters["@position"].Value = (int)work.Number;
			return (DtCardWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		// ��� ������������� ������
		public static DtCardWork Find(long cardNumber, int cardYear, int position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)cardNumber;
			find.Parameters["@card_year"].Value = (int)cardYear;
			find.Parameters["@position"].Value = (int)position;
			return (DtCardWork)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool UpdateGuaranty(DtCardWork work, DbGuarantyType guaranty, DbStaff mistake_initiator, string mistake)
		{
			// ���������� ������� ������ �� �����

			update_guaranty.Parameters["@card_number"].Value = (long)work.GetData("�����_��������_��������_������");
			update_guaranty.Parameters["@card_year"].Value = (int)work.GetData("���_��������_��������_������");
			update_guaranty.Parameters["@position"].Value = (int)work.GetData("�������_��������_������");
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

		public static bool UpdateGuaranty(long card_number, int card_year, int position, long code_guaranty, long code_responsible, string mistake)
		{
			// ���������� ������� ������ �� �����

			update_guaranty.Parameters["@card_number"].Value = (long)card_number;
			update_guaranty.Parameters["@card_year"].Value = (int)card_year;
			update_guaranty.Parameters["@position"].Value = (int)position;
			update_guaranty.Parameters["@guaranty"].Value = (long)code_guaranty;
			update_guaranty.Parameters["@mistake_initiator"].Value = (long)code_responsible;
			update_guaranty.Parameters["@mistake"].Value = (string)mistake;
			return DbSql.ExecuteCommandError(update_guaranty);
		}

		public static bool UpdateCost(DtCardWork work, float cost)
		{
			// ���������� ������� ������ �� �����
			update_cost.Parameters["@card_number"].Value = (long)work.GetData("�����_��������_��������_������");
			update_cost.Parameters["@card_year"].Value = (int)work.GetData("���_��������_��������_������");
			update_cost.Parameters["@position"].Value = (int)work.GetData("�������_��������_������");
			update_cost.Parameters["@cost"].Value = (float)cost;
			return DbSql.ExecuteCommandError(update_cost);
		}

		public static bool UpdateVal(DtCardWork work, float val)
		{
			// ���������� ������� ������ �� �����
			update_val.Parameters["@card_number"].Value = (long)work.GetData("�����_��������_��������_������");
			update_val.Parameters["@card_year"].Value = (int)work.GetData("���_��������_��������_������");
			update_val.Parameters["@position"].Value = (int)work.GetData("�������_��������_������");
			update_val.Parameters["@val"].Value = (float)val;
			return DbSql.ExecuteCommandError(update_val);
		}

        public static bool UpdateDiscount(DtCardWork work, float discount)
        {
            // ���������� ������� ������ �� �����
            update_discount.Parameters["@card_number"].Value = (long)work.GetData("�����_��������_��������_������");
            update_discount.Parameters["@card_year"].Value = (int)work.GetData("���_��������_��������_������");
            update_discount.Parameters["@position"].Value = (int)work.GetData("�������_��������_������");
            update_discount.Parameters["@discount"].Value = (float)discount;
            return DbSql.ExecuteCommandError(update_discount);
        }

        public static bool SetDiscount(long card_number, int card_year, int position, float discount)
        {
            // ���������� ������� ������ �� �����
            update_discount.Parameters["@card_number"].Value = (long)card_number;
            update_discount.Parameters["@card_year"].Value = (int)card_year;
            update_discount.Parameters["@position"].Value = (int)position;
            update_discount.Parameters["@discount"].Value = (float)discount;
            return DbSql.ExecuteCommandError(update_discount);
        }

		public static ArrayList Select(DtCard srcCard)
		{
			ArrayList array = new ArrayList();
			SelectInArray(srcCard, array);
			return array;
		}

		public static bool UpdateValues(DtCardWork cardWork)
		{
			// ���������� ������� ������ �� �����

			updateValues.Parameters["@cardNumber"].Value = cardWork.CardNumber;
			updateValues.Parameters["@cardYear"].Value = cardWork.CardYear;
			updateValues.Parameters["@number"].Value = cardWork.Position;
			updateValues.Parameters["@guaranty"].Value = cardWork.GuaranteeFlag();
			updateValues.Parameters["@discount"].Value = cardWork.Discount;
			updateValues.Parameters["@val"].Value = cardWork.LaborTime;
			updateValues.Parameters["@price"].Value = cardWork.OperationCost;
			updateValues.Parameters["@quontity"].Value = cardWork.OperationAmount;
			updateValues.Parameters["@oil"].Value = cardWork.Oil;

			if (DbSql.ExecuteCommandError(updateValues))
			{
				cardWork.IsChg = false;
				return true;
			}
			else
				return false;
		}
		public static bool Create(DtCardWork cardWork)
		{
			// ������ ������ �������� � ���� ������ SQL

			create.Parameters["@cardNumber"].Value = cardWork.CardNumber;
			create.Parameters["@cardYear"].Value = cardWork.CardYear;
			create.Parameters["@codeWork"].Value = cardWork.CodeWork;
			create.Parameters["@guaranty"].Value = cardWork.GuaranteeFlag();
			create.Parameters["@discount"].Value = cardWork.Discount;
			create.Parameters["@val"].Value = cardWork.LaborTime;
			create.Parameters["@price"].Value = cardWork.OperationCost;
			create.Parameters["@quontity"].Value = cardWork.OperationAmount;
			create.Parameters["@oil"].Value = cardWork.Oil;

			if (DbSql.ExecuteCommandError(create))
			{
				cardWork.Position = (int)create.Parameters["@number"].Value;
				cardWork.IsChg = false;
				cardWork.IsNew = false;
				return true;
			}
			else
				return false;
		}
	}
}
