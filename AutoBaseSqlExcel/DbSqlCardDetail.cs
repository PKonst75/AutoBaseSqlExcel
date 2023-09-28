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
			select = new SqlCommand("��������_������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;

			select_period = new SqlCommand("��������_������_�������_������", connection);
			select_period.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_period.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_period.Parameters.Add("@liquid", SqlDbType.Bit);
			select_period.Parameters.Add("@guaranty", SqlDbType.Bit);
			select_period.Parameters.Add("@cashless", SqlDbType.Bit);
			select_period.Parameters.Add("@inner", SqlDbType.Bit);
			select_period.CommandType = CommandType.StoredProcedure;

			select_period_spec1 = new SqlCommand("��������_������_�������_������_����1", connection);
			select_period_spec1.Parameters.Add("@start_date", SqlDbType.DateTime);
			select_period_spec1.Parameters.Add("@end_date", SqlDbType.DateTime);
			select_period_spec1.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("��������_������_�����", connection);
			find.Parameters.Add("@card_number", SqlDbType.BigInt);
			find.Parameters.Add("@card_year", SqlDbType.Int);
			find.Parameters.Add("@position", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			update_input = new SqlCommand("��������_������_��������_����", connection);
			update_input.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_input.Parameters.Add("@card_year", SqlDbType.Int);
			update_input.Parameters.Add("@position", SqlDbType.BigInt);
			update_input.Parameters.Add("@input", SqlDbType.Real);
			update_input.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_input);

			update_price = new SqlCommand("��������_������_��������_����", connection);
			update_price.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_price.Parameters.Add("@card_year", SqlDbType.Int);
			update_price.Parameters.Add("@position", SqlDbType.BigInt);
			update_price.Parameters.Add("@price", SqlDbType.Real);
			update_price.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_price);

			update_guaranty = new SqlCommand("��������_������_��������_��������", connection);
			update_guaranty.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@card_year", SqlDbType.Int);
			update_guaranty.Parameters.Add("@position", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@guaranty", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake_initiator", SqlDbType.BigInt);
			update_guaranty.Parameters.Add("@mistake", SqlDbType.VarChar);
			update_guaranty.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_guaranty);

			update_present = new SqlCommand("��������_������_��������_�������", connection);
			update_present.Parameters.Add("@card_number", SqlDbType.BigInt);
			update_present.Parameters.Add("@card_year", SqlDbType.Int);
			update_present.Parameters.Add("@position", SqlDbType.BigInt);
			update_present.Parameters.Add("@present", SqlDbType.Bit);
			update_present.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_present);

            update_to = new SqlCommand("��������_������_��������_��", connection);
            update_to.Parameters.Add("@card_number", SqlDbType.BigInt);
            update_to.Parameters.Add("@card_year", SqlDbType.Int);
            update_to.Parameters.Add("@position", SqlDbType.BigInt);
            update_to.Parameters.Add("@to", SqlDbType.Bit);
            update_to.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_to);

            update_discount = new SqlCommand("��������_������_��������_������", connection);
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
			element.SetData("�����_��������_��������_������", DbSql.GetValueLong(reader, "�����_��������_��������_������"));
			element.SetData("���_��������_��������_������", DbSql.GetValueInt(reader, "���_��������_��������_������"));
			element.SetData("�������_��������_������", DbSql.GetValueLong(reader, "�������_��������_������"));
			element.SetData("����������_��������_������", DbSql.GetValueFloat(reader, "����������_��������_������"));
			element.SetData("����_��������_������", DbSql.GetValueFloat(reader, "����_��������_������"));
			element.SetData("����_��������_������", DbSql.GetValueFloat(reader, "����_��������_������"));
			element.SetData("��������_��������_������", DbSql.GetValueBool(reader, "��������_��������_������"));
			element.SetData("�������_��������_������", DbSql.GetValueBool(reader, "�������_��������_������"));
			element.SetData("���_��������_������", DbSql.GetValueBool(reader, "���_��������_������"));
			element.SetData("�������_��������_������", DbSql.GetValueLong(reader, "�������_��������_������"));
			element.SetData("���_�����_��������_������", DbSql.GetValueLong(reader, "���_�����_��������_������"));
			element.SetData("������������_��������_������", DbSql.GetValueString(reader, "������������_��������_������"));
			element.SetData("�������_���������_��������_������", DbSql.GetValueString(reader, "�������_���������_��������_������"));
			element.SetData("�������_�����_��������_������", DbSql.GetValueString(reader, "�������_�����_��������_������"));
			element.SetData("��������_��������_������", DbSql.GetValueBool(reader, "��������_��������_������"));
			element.SetData("���_1�_��������_������", DbSql.GetValueLong(reader, "���_1�_��������_������"));
			element.SetData("��������_���_��������_������", DbSql.GetValueLong(reader, "��������_���_��������_������"));
			element.SetData("��������_���_������������_��������_������", DbSql.GetValueString(reader, "��������_���_������������_��������_������"));
			element.SetData("���������_������������_��������_������", DbSql.GetValueString(reader, "���������_������������_��������_������"));
			element.SetData("���������_��������_������", DbSql.GetValueLong(reader, "���������_��������_������"));
			element.SetData("�����_��������_������", DbSql.GetValueString(reader, "�����_��������_������"));
			element.SetData("�������", DbSql.GetValueBool(reader, "�������"));
            element.SetData("��", DbSql.GetValueBool(reader, "��"));
            element.SetData("������", DbSql.GetValueFloat(reader, "������"));
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

		public static void SelectInArray(ArrayList array, DateTime start_date, DateTime end_date, long workshop, bool liquid, bool guaranty, bool cashless, bool inner)
		{
			// ���������� ������� ������ �� �����
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
			// ���������� ������� ������ �� �����
			select_period_spec1.Parameters["@start_date"].Value = (DateTime)start_date;
			select_period_spec1.Parameters["@end_date"].Value = (DateTime)end_date;
			DbSql.FillArray(array, select_period_spec1, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardDetail Find(DtCard card, long position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			find.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtCardDetail Find(long card_number, int card_year, long position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card_number;
			find.Parameters["@card_year"].Value = (int)card_year;
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		// ��� ������������� ������
		public static DtCardDetail Find(DbCard card, long position)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@card_number"].Value = (long)card.Number;
			find.Parameters["@card_year"].Value = (int)card.Year;
			find.Parameters["@position"].Value = (long)position;
			return (DtCardDetail)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool UpdateInput(DtCard card, long position, float input)
		{
			// ���������� ������� ������ �� �����
			update_input.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			update_input.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			update_input.Parameters["@position"].Value		= (long)position;
			update_input.Parameters["@input"].Value		= (float)input;
			return DbSql.ExecuteCommandError(update_input);
		}

		public static bool UpdatePrice(DtCard card, long position, float price)
		{
			// ���������� ������� ������ �� �����
			update_price.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			update_price.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			update_price.Parameters["@position"].Value		= (long)position;
			update_price.Parameters["@price"].Value			= (float)price;
			return DbSql.ExecuteCommandError(update_price);
		}

		public static bool UpdateGuaranty(DtCardDetail detail, DbGuarantyType guaranty, DbStaff mistake_initiator, string mistake)
		{
			// ���������� ������� ������ �� �����

			update_guaranty.Parameters["@card_number"].Value = (long)detail.GetData("�����_��������_��������_������");
			update_guaranty.Parameters["@card_year"].Value = (int)detail.GetData("���_��������_��������_������");
			update_guaranty.Parameters["@position"].Value = (long)detail.GetData("�������_��������_������");;
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
			// ���������� ������� ������ �� �����

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
			// ��������� ������� � �������
			update_present.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			update_present.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			update_present.Parameters["@position"].Value		= (long)position;
			update_present.Parameters["@present"].Value			= (bool)present;
			return DbSql.ExecuteCommandError(update_present);
		}

        public static bool UpdateTo(DtCard card, long position, bool to)
        {
            // ��������� ������� � �������
            update_to.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
            update_to.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
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
            // ��������� ������� � �������
            update_discount.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
            update_discount.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
            update_discount.Parameters["@position"].Value = (long)position;
            update_discount.Parameters["@discount"].Value = (float)dsc;
            return DbSql.ExecuteCommandError(update_discount);
        }
	}
}
