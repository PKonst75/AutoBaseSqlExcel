using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCard.
	/// </summary>
	public class DbSqlCard
	{
		// ������ � ����� ������
		public static SqlCommand		select_card;
		public static SqlCommand		select_card_today_time;
		public static SqlCommand		select_card_goin_time;
		public static SqlCommand		select_card_goin_time_pause;
		public static SqlCommand		select_card_workshop;
		public static SqlCommand		select_card_closed;
		public static SqlCommand		select_card_closed_number;
		public static SqlCommand		select_card_closed_number_avtovaz;
		public static SqlCommand		select_card_closed_number_workshop;
		public static SqlCommand		select_card_closed_number_workshop_auto;
		public static SqlCommand		select_card_open_number_workshop;
		public static SqlCommand		select_card_open_number_auto;
		public static SqlCommand		select_card_closed_number_workshop_nal;
		public static SqlCommand		select_card_notclosed_date_number_workshop;
		public static SqlCommand		select_card_open_interval_number_workshop;
		public static SqlCommand		select_warrant_number;	// ������� �����-������ �� ������
		public static SqlCommand		select_number;			// ������� �������� �� ������
		public static SqlCommand		select_number_year;		// ������� �������� �� ������ � ����
		public static SqlCommand		select_auto;			// ������� �������� �� ���� ����������
		public static SqlCommand		select_partner;			// ������� �������� �� ���� ��������� � �������������
		public static SqlCommand		select_card_detail;		// ������� �������� � ������� ���������� ������ �� ������
		public static SqlCommand		select_card_returned;	// ������� ��������� � ������������ ���������
		public static SqlCommand		find;
		public static SqlCommand		find_number_year;		// ����� �� ������ � ����
		public static SqlCommand		find_rate;				// ����� ��� ������ ������
		public static SqlCommand		set_status_control;
		public static SqlCommand		set_master;				// ��������� �������-���������� �� �����-������
		public static SqlCommand		set_discount;			// ��������� ������ �� �����-������
        public static SqlCommand        set_discount_parts;		// ��������� ������ �� �����-������ �� ������
		public static SqlCommand		set_print;				// ������� � ������ ��������
		public static SqlCommand		set_supervisor_guaranty;// ������� �� ��������� ��������
		public static SqlCommand		set_supervisor_payment;	// ������� �� ��������� ������
		public static SqlCommand		set_supervisor_whole;	// ������ ��������� ������
		public static SqlCommand		set_licence_vehicle;	// ��������� ������������� � �����������
		public static SqlCommand		set_licence_plate;		// ��������� �����
		public static SqlCommand		set_return_flag;		// ��������� �������� ���������� ��������
		public static SqlCommand		set_service_manager;		// ��������� ������-������������
		public static SqlCommand		set_service_manager_ever;	// ��������� ������-������������ ������ (��� �������� �����������)
		public static SqlCommand		set_creditcard_flag;		// ��������� �������� ������ �� ��������� �����

		public static SqlCommand		auxiliary_auto_set_null;	// ��������� ���������� � ��������
		public static SqlCommand		auxiliary_partner_replace;	// ������ ��������� ���������� � ��������
		public static SqlCommand		auxiliary_auto_replace;		// ������ ���������� � ��������
		public static SqlCommand		auxiliary_card_insert;		// ��������� �������� ��������
		public static SqlCommand		auxiliary_card_set_date;	// ��������� ���� � ������� �������� ��������
		public static SqlCommand		auxiliary_warrant_set;		// ��������� ����� ������ �����-������
		public static SqlCommand		auxiliary_warrant_open;		// �������� �����-������
		public static SqlCommand		auxiliary_run_set;			// ��������� ����� ������ �� �������
		public static SqlCommand		auxiliary_warrant_close;	// �������� �����-������
		public static SqlCommand		auxiliary_warrant_close_set;// ��������� ���� �������� �����-������

		public static SqlCommand		select_card_auto_run;		// ������� �������� � ����������� �������� �� ����������

		// ����� �����
		public static SqlCommand card_set_agreedPickupTime; // ��������� �������������� ������� ������
		public static SqlCommand card_write;

		private static SqlCommand _spUpdate;
		private static SqlCommand _spFind;
		private static SqlCommand _spInsert;

		public DbSqlCard()
		{
		}

		public static void SetSqlCommandValues(DtCard srcDtCard, SqlCommand srcSqlCommand)
        {
			DbSql.SetParameterValue("number", srcDtCard.Number, srcSqlCommand);
			DbSql.SetParameterValue("year", srcDtCard.Year, srcSqlCommand);
			DbSql.SetParameterValue("date", srcDtCard.Date, srcSqlCommand);
			DbSql.SetParameterValue("codeAuto", srcDtCard.CodeAuto, srcSqlCommand);
			DbSql.SetParameterValue("codePartner", srcDtCard.CodeOwner, srcSqlCommand);
			DbSql.SetParameterValue("codeRepresent", srcDtCard.CodeRepresentative, srcSqlCommand);
			DbSql.SetParameterValue("representDocument", srcDtCard.RepresentativeDocs, srcSqlCommand);
			DbSql.SetParameterValue("codeAutoType", srcDtCard.CodeAutoType, srcSqlCommand);
			DbSql.SetParameterValue("run", srcDtCard.CardRun, srcSqlCommand);
			DbSql.SetParameterValue("return", srcDtCard.Returned, srcSqlCommand);
			DbSql.SetParameterValue("discount_works", srcDtCard.Discount, srcSqlCommand);
			DbSql.SetParameterValue("discount_details", srcDtCard.DiscountDetail, srcSqlCommand);
			DbSql.SetParameterValue("service_manager_code", srcDtCard.ServiceManagerCode, srcSqlCommand);
			DbSql.SetParameterValue("cashless", srcDtCard.Cashless, srcSqlCommand);
			DbSql.SetParameterValue("inner", srcDtCard.Inner, srcSqlCommand);
			DbSql.SetParameterValue("credit_card", srcDtCard.CreditCard, srcSqlCommand);
			DbSql.SetParameterValue("comment", srcDtCard.Comment, srcSqlCommand);
			DbSql.SetParameterValue("codeWorkshop", srcDtCard.CodeWorkshop, srcSqlCommand);
			DbSql.SetParameterValue("licensePlateNumber", srcDtCard.LicensePlate.Number, srcSqlCommand);
			DbSql.SetParameterValue("licensePlateRegion", srcDtCard.LicensePlate.Region, srcSqlCommand);
			DbSql.SetParameterValue("agreedPickupTime", srcDtCard.AgreedPickUpTime, srcSqlCommand);
			DbSql.SetParameterValue("state", srcDtCard.State, srcSqlCommand);
		}
		public static object ReadCard(SqlDataReader reader)
		{
			DtCard dtCard = new DtCard()
			{
				CardCode = DbSql.GetValueLong(reader, "���_��������"),
				Number = DbSql.GetValueLong(reader, "�����_��������"),
				Year = DbSql.GetValueInt(reader, "���_��������"),
				Date = DbSql.GetValueDate(reader, "����"),
				CodeAuto = DbSql.GetValueLong(reader, "��������_������_���_����������"),
				CodeOwner = DbSql.GetValueLong(reader, "��������_������_���_����������"),
				CodeRepresentative = DbSql.GetValueLong(reader, "������_���_�������������"),
				RepresentativeDocs = DbSql.GetValueString(reader, "��������_�������������"),
				CodeAutoType = DbSql.GetValueLong(reader, "��������_������_���_����������_���"),
				CardRun = DbSql.GetValueInt(reader, "������"),
				Returned = DbSql.GetValueBool(reader, "�������"),
				Discount = DbSql.GetValueFloat(reader, "������_������_��������"),
				DiscountDetail = DbSql.GetValueFloat(reader, "������_������_��������"),
				ServiceManagerCode = DbSql.GetValueLong(reader, "������_�����������"),
				Cashless = DbSql.GetValueBool(reader, "������"),
				Inner = DbSql.GetValueBool(reader, "����������_��������"),
				CreditCard = DbSql.GetValueBool(reader, "���������_�����_��������"),
				Comment = DbSql.GetValueString(reader, "����������_��������"),
				CodeWorkshop = DbSql.GetValueLong(reader, "������_���_���"),
				AgreedPickUpTime = DbSql.GetValueDate(reader, "�����_������_�������������"),
				State = (DtCard.CardState)DbSql.GetValueShort(reader, "������_��������"),
				StateControl = DbSql.GetValueShort(reader, "������_��������_��������"),
				WarrantNumber = DbSql.GetValueLong(reader, "�����_�����"),
				MasterCode = DbSql.GetValueLong(reader, "������_���������_��������"),
				IsChg = false, // ��������, ��� ��� �� ���������� �������
				IsNew = false // ��������, ��� ��� �� �����
			};	
			dtCard.LicensePlate.Number = DbSql.GetValueString(reader, "�����_����_�����");
			dtCard.LicensePlate.Region = DbSql.GetValueString(reader, "�����_����_������");
			return dtCard;
		}
		public static bool Insert(DtCard srcCard)
        {
			if (srcCard == null) return false;
			if (!srcCard.IsNew) return false;
			SetSqlCommandValues(srcCard, _spInsert);
			if (DbSql.ExecuteCommandError(_spInsert) == false) return Error.ErroMessageFalse("������ ��� ������ � ��");
			srcCard.Number = (long)DbSql.GetParameterValue("number", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			srcCard.Year = (int)DbSql.GetParameterValue("year", DbSql.SQL_PARAMETER_TYPE.INT, _spInsert);
			srcCard.Date = (DateTime)DbSql.GetParameterValue("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			return true;
        }
		public static bool Update(DtCard srcCard)
		{
			if (srcCard == null) return false;
			if (!srcCard.IsChg) return true;
			SetSqlCommandValues(srcCard, _spUpdate);
			return DbSql.ExecuteCommandError(_spUpdate);
		}
		public static DtCard Find(DtCard srcCard)
		{
			if (srcCard == null) return null;
			SetSqlCommandValues(srcCard, _spFind);
			return (DtCard) DbSql.Find(_spFind, new DbSql.DelegateReadDt(ReadCard));
		}
		public static bool Write(DtCard srcCard)
		{
			// ���� ������ ���������, � ���� ������ �� ����������
			if (srcCard.IsNew)
				return Insert(srcCard);
			if (!srcCard.IsChg) return true;
			return Update(srcCard);
		}

		public static void Init(SqlConnection connection)
		{
			card_write = new SqlCommand("��������_��������", connection);
			card_write.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(card_write);

			_spUpdate = new SqlCommand("��������_UPDATE", connection);
			DbSql.AddParameter("number", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("year", DbSql.SQL_PARAMETER_TYPE.INT, _spUpdate);
			DbSql.AddParameter("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spUpdate);
			DbSql.AddParameter("codeAutoType", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("codePartner", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("codeRepresent", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("representDocument", DbSql.SQL_PARAMETER_TYPE.STRING, _spUpdate);
			DbSql.AddParameter("codeAuto", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("run", DbSql.SQL_PARAMETER_TYPE.INT, _spUpdate);
			DbSql.AddParameter("return", DbSql.SQL_PARAMETER_TYPE.BOOL, _spUpdate);
			DbSql.AddParameter("discount_works", DbSql.SQL_PARAMETER_TYPE.FLOAT, _spUpdate);
			DbSql.AddParameter("discount_details", DbSql.SQL_PARAMETER_TYPE.FLOAT, _spUpdate);
			DbSql.AddParameter("service_manager_code", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("cashless", DbSql.SQL_PARAMETER_TYPE.BOOL, _spUpdate);
			DbSql.AddParameter("inner", DbSql.SQL_PARAMETER_TYPE.BOOL, _spUpdate);
			DbSql.AddParameter("credit_card", DbSql.SQL_PARAMETER_TYPE.BOOL, _spUpdate);
			DbSql.AddParameter("comment", DbSql.SQL_PARAMETER_TYPE.STRING, _spUpdate);
			DbSql.AddParameter("codeWorkshop", DbSql.SQL_PARAMETER_TYPE.LONG, _spUpdate);
			DbSql.AddParameter("licensePlateNumber", DbSql.SQL_PARAMETER_TYPE.STRING, _spUpdate);
			DbSql.AddParameter("licensePlateRegion", DbSql.SQL_PARAMETER_TYPE.STRING, _spUpdate);
			DbSql.AddParameter("agreedPickupTime", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spUpdate);
			DbSql.AddParameter("state", DbSql.SQL_PARAMETER_TYPE.SHORT, _spUpdate);
			DbSql.SetStoredProcedure(_spUpdate);
			DbSql.SetReturnError(_spUpdate);

			_spInsert = new SqlCommand("��������_INSERT", connection);
			DbSql.AddParameter("number", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("year", DbSql.SQL_PARAMETER_TYPE.INT, _spInsert);
			DbSql.AddParameter("date", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			DbSql.AddParameter("codeAutoType", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("codePartner", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("codeRepresent", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("representDocument", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("codeAuto", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("run", DbSql.SQL_PARAMETER_TYPE.INT, _spInsert);
			DbSql.AddParameter("return", DbSql.SQL_PARAMETER_TYPE.BOOL, _spInsert);
			DbSql.AddParameter("discount_works", DbSql.SQL_PARAMETER_TYPE.FLOAT, _spInsert);
			DbSql.AddParameter("discount_details", DbSql.SQL_PARAMETER_TYPE.FLOAT, _spInsert);
			DbSql.AddParameter("service_manager_code", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("cashless", DbSql.SQL_PARAMETER_TYPE.BOOL, _spInsert);
			DbSql.AddParameter("inner", DbSql.SQL_PARAMETER_TYPE.BOOL, _spInsert);
			DbSql.AddParameter("credit_card", DbSql.SQL_PARAMETER_TYPE.BOOL, _spInsert);
			DbSql.AddParameter("comment", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("codeWorkshop", DbSql.SQL_PARAMETER_TYPE.LONG, _spInsert);
			DbSql.AddParameter("licensePlateNumber", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("licensePlateRegion", DbSql.SQL_PARAMETER_TYPE.STRING, _spInsert);
			DbSql.AddParameter("agreedPickupTime", DbSql.SQL_PARAMETER_TYPE.DATETIME, _spInsert);
			DbSql.AddParameter("state", DbSql.SQL_PARAMETER_TYPE.SHORT, _spInsert);
			DbSql.SetStoredProcedure(_spInsert);
			DbSql.SetReturnError(_spInsert);
			DbSql.SetParameterOutput("number", _spInsert);
			DbSql.SetParameterOutput("year", _spInsert);
			DbSql.SetParameterOutput("date", _spInsert);

			_spFind = new SqlCommand("��������_FIND", connection);
			DbSql.AddParameter("number", DbSql.SQL_PARAMETER_TYPE.LONG, _spFind);
			DbSql.AddParameter("year", DbSql.SQL_PARAMETER_TYPE.INT, _spFind);
			DbSql.SetStoredProcedure(_spFind);

			// ����� �����
			card_set_agreedPickupTime = new SqlCommand("��������_���������_�����_������_�������������", connection);
			card_set_agreedPickupTime.Parameters.Add("@number", SqlDbType.BigInt);
			card_set_agreedPickupTime.Parameters.Add("@year", SqlDbType.Int);
			card_set_agreedPickupTime.Parameters.Add("@date", SqlDbType.DateTime);
			card_set_agreedPickupTime.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(card_set_agreedPickupTime);


			select_card = new SqlCommand("��������_�������", connection);
			select_card.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card.Parameters.Add("@no_date", SqlDbType.Bit);
			select_card.Parameters.Add("@owner_mask", SqlDbType.VarChar);
			select_card.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			select_card.Parameters.Add("@sign_mask", SqlDbType.VarChar);
			select_card.Parameters.Add("@show_cancel", SqlDbType.Bit);
			select_card.CommandType = CommandType.StoredProcedure;

			select_card_today_time = new SqlCommand("��������_�������_�������_�����", connection);
			select_card_today_time.Parameters.Add("@date", SqlDbType.DateTime);
			select_card_today_time.CommandType = CommandType.StoredProcedure;

			select_card_goin_time = new SqlCommand("��������_�������_�����_�����", connection);
			select_card_goin_time.CommandType = CommandType.StoredProcedure;

			select_card_goin_time_pause = new SqlCommand("��������_�������_�����_�����_�����", connection);
			select_card_goin_time_pause.CommandType = CommandType.StoredProcedure;

			select_card_workshop = new SqlCommand("��������_�������_�������������", connection);
			select_card_workshop.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_workshop.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_workshop.Parameters.Add("@no_date", SqlDbType.Bit);
			select_card_workshop.Parameters.Add("@owner_mask", SqlDbType.VarChar);
			select_card_workshop.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			select_card_workshop.Parameters.Add("@sign_mask", SqlDbType.VarChar);
			select_card_workshop.Parameters.Add("@show_cancel", SqlDbType.Bit);
			select_card_workshop.Parameters.Add("@code_workshop", SqlDbType.BigInt);
			select_card_workshop.CommandType = CommandType.StoredProcedure;

			select_card_closed = new SqlCommand("��������_�������_��������", connection);
			select_card_closed.Parameters.Add("@date", SqlDbType.DateTime);
			select_card_closed.CommandType = CommandType.StoredProcedure;
			select_card_closed.CommandTimeout = 360;

			select_card_closed_number = new SqlCommand("��������_�������_��������_������", connection);
			select_card_closed_number.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_closed_number.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_closed_number.CommandType = CommandType.StoredProcedure;
			select_card_closed_number.CommandTimeout = 360;

			select_card_closed_number_avtovaz = new SqlCommand("��������_�������_��������_������_�������", connection);
			select_card_closed_number_avtovaz.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_closed_number_avtovaz.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_closed_number_avtovaz.CommandType = CommandType.StoredProcedure;
			select_card_closed_number_avtovaz.CommandTimeout = 360;


			select_card_closed_number_workshop = new SqlCommand("��������_�������_��������_������_�������������", connection);
			select_card_closed_number_workshop.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_closed_number_workshop.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_closed_number_workshop.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_closed_number_workshop.CommandType = CommandType.StoredProcedure;
			select_card_closed_number_workshop.CommandTimeout = 360;

			select_card_closed_number_workshop_auto = new SqlCommand("��������_�������_��������_������_�������������_����������", connection);
			select_card_closed_number_workshop_auto.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_closed_number_workshop_auto.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_closed_number_workshop_auto.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_closed_number_workshop_auto.Parameters.Add("@auto", SqlDbType.BigInt);
			select_card_closed_number_workshop_auto.CommandType = CommandType.StoredProcedure;
			select_card_closed_number_workshop_auto.CommandTimeout = 360;

			select_card_open_number_workshop = new SqlCommand("��������_�������_��������_������_�������������", connection);
			select_card_open_number_workshop.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_open_number_workshop.CommandType = CommandType.StoredProcedure;
			select_card_open_number_workshop.CommandTimeout = 360;

			select_card_open_number_auto = new SqlCommand("��������_�������_��������_������_����������", connection);
			select_card_open_number_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_card_open_number_auto.CommandType = CommandType.StoredProcedure;
			select_card_open_number_auto.CommandTimeout = 360;

			select_card_closed_number_workshop_nal = new SqlCommand("��������_�������_��������_������_�������������_���", connection);
			select_card_closed_number_workshop_nal.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_closed_number_workshop_nal.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_closed_number_workshop_nal.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_closed_number_workshop_nal.CommandType = CommandType.StoredProcedure;
			select_card_closed_number_workshop_nal.CommandTimeout = 360;

			select_card_notclosed_date_number_workshop = new SqlCommand("��������_�������_����������_������_������_�������������", connection);
			select_card_notclosed_date_number_workshop.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_notclosed_date_number_workshop.Parameters.Add("@date", SqlDbType.DateTime);
			select_card_notclosed_date_number_workshop.CommandType = CommandType.StoredProcedure;
			select_card_notclosed_date_number_workshop.CommandTimeout = 360;

			select_card_open_interval_number_workshop = new SqlCommand("��������_�������_��������_����������_������_�������������", connection);
			select_card_open_interval_number_workshop.Parameters.Add("@workshop", SqlDbType.BigInt);
			select_card_open_interval_number_workshop.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_open_interval_number_workshop.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_open_interval_number_workshop.CommandType = CommandType.StoredProcedure;
			select_card_open_interval_number_workshop.CommandTimeout = 360;

			select_card_auto_run = new SqlCommand("��������_�������_����������_������", connection);
			select_card_auto_run.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_card_auto_run.CommandType = CommandType.StoredProcedure;

			select_auto = new SqlCommand("��������_�������_����������", connection);
			select_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_auto.CommandType = CommandType.StoredProcedure;

			select_partner = new SqlCommand("��������_�������_��������", connection);
			select_partner.Parameters.Add("@code_partner", SqlDbType.BigInt);
			select_partner.CommandType = CommandType.StoredProcedure;

			select_card_detail = new SqlCommand("��������_�������_������", connection);
			select_card_detail.Parameters.Add("@code_storage", SqlDbType.BigInt);
			select_card_detail.CommandType = CommandType.StoredProcedure;

			select_card_returned = new SqlCommand("��������_�������_�������", connection);
			select_card_returned.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_card_returned.Parameters.Add("@date_end", SqlDbType.DateTime);
			select_card_returned.CommandType = CommandType.StoredProcedure;


			find = new SqlCommand("��������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_number_year = new SqlCommand("��������_�����_�����_���", connection);
			find_number_year.Parameters.Add("@number", SqlDbType.BigInt);
			find_number_year.Parameters.Add("@year", SqlDbType.Int);
			find_number_year.CommandType = CommandType.StoredProcedure;

			find_rate = new SqlCommand("��������_�����_���_������", connection);
			find_rate.Parameters.Add("@number", SqlDbType.BigInt);
			find_rate.Parameters.Add("@year", SqlDbType.Int);
			find_rate.CommandType = CommandType.StoredProcedure;

			select_number_year = new SqlCommand("��������_�������_�����_���", connection);
			select_number_year.Parameters.Add("@number", SqlDbType.BigInt);
			select_number_year.Parameters.Add("@year", SqlDbType.Int);
			select_number_year.CommandType = CommandType.StoredProcedure;

			select_warrant_number = new SqlCommand("��������_�������_�����_�����", connection);
			select_warrant_number.Parameters.Add("@warrant_number", SqlDbType.BigInt);
			select_warrant_number.CommandType = CommandType.StoredProcedure;

			select_number = new SqlCommand("��������_�������_�����", connection);
			select_number.Parameters.Add("@number", SqlDbType.BigInt);
			select_number.CommandType = CommandType.StoredProcedure;

			set_status_control = new SqlCommand("��������_��������_������_��������", connection);
			set_status_control.Parameters.Add("@code", SqlDbType.BigInt);
			set_status_control.Parameters.Add("@status_control", SqlDbType.SmallInt);
			set_status_control.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_status_control);

			set_master = new SqlCommand("��������_��������_������_���������", connection);
			set_master.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_master.Parameters.Add("@card_year", SqlDbType.Int);
			set_master.Parameters.Add("@card_code", SqlDbType.BigInt);
			set_master.Parameters.Add("@master_code", SqlDbType.BigInt);
			set_master.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_master);

			set_supervisor_guaranty = new SqlCommand("��������_��������_��������", connection);
			set_supervisor_guaranty.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_supervisor_guaranty.Parameters.Add("@card_year", SqlDbType.Int);
			set_supervisor_guaranty.Parameters.Add("@supervisor_guaranty", SqlDbType.BigInt);
			set_supervisor_guaranty.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_supervisor_guaranty);

			set_supervisor_payment = new SqlCommand("��������_��������_������", connection);
			set_supervisor_payment.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_supervisor_payment.Parameters.Add("@card_year", SqlDbType.Int);
			set_supervisor_payment.Parameters.Add("@supervisor_payment", SqlDbType.BigInt);
			set_supervisor_payment.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_supervisor_payment);

			set_supervisor_whole = new SqlCommand("��������_��������_���������", connection);
			set_supervisor_whole.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_supervisor_whole.Parameters.Add("@card_year", SqlDbType.Int);
			set_supervisor_whole.Parameters.Add("@supervisor_whole", SqlDbType.BigInt);
			set_supervisor_whole.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_supervisor_whole);

			set_discount = new SqlCommand("��������_��������_������", connection);
			set_discount.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_discount.Parameters.Add("@card_year", SqlDbType.Int);
			set_discount.Parameters.Add("@card_code", SqlDbType.BigInt);
			set_discount.Parameters.Add("@code_discount", SqlDbType.BigInt);
			set_discount.Parameters.Add("@discount", SqlDbType.Real);
			set_discount.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_discount);

            set_discount_parts = new SqlCommand("��������_��������_������_������", connection);
            set_discount_parts.Parameters.Add("@card_number", SqlDbType.BigInt);
            set_discount_parts.Parameters.Add("@card_year", SqlDbType.Int);
            set_discount_parts.Parameters.Add("@discount", SqlDbType.Real);
            set_discount_parts.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(set_discount_parts);

			set_licence_vehicle = new SqlCommand("��������_�������_�������������_��", connection);
			set_licence_vehicle.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_licence_vehicle.Parameters.Add("@card_year", SqlDbType.Int);
			set_licence_vehicle.Parameters.Add("@code_licence", SqlDbType.BigInt);
			set_licence_vehicle.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_licence_vehicle);

			set_licence_plate = new SqlCommand("��������_���������_�����_����", connection);
			set_licence_plate.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_licence_plate.Parameters.Add("@card_year", SqlDbType.Int);
			set_licence_plate.Parameters.Add("@licence_plate_number", SqlDbType.VarChar);
			set_licence_plate.Parameters.Add("@licence_plate_region", SqlDbType.VarChar);
			set_licence_plate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_licence_plate);

			set_return_flag = new SqlCommand("��������_���������_�������", connection);
			set_return_flag.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_return_flag.Parameters.Add("@card_year", SqlDbType.Int);
			set_return_flag.Parameters.Add("@returned", SqlDbType.Bit);
			set_return_flag.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_return_flag);

			set_print = new SqlCommand("��������_������_��������", connection);
			set_print.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_print.Parameters.Add("@card_year", SqlDbType.Int);
			set_print.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_print);

			
			set_service_manager = new SqlCommand("��������_����������_������_�����������", connection);
			set_service_manager.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_service_manager.Parameters.Add("@card_year", SqlDbType.Int);
			set_service_manager.Parameters.Add("@service_manager_code", SqlDbType.BigInt);
			set_service_manager.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_service_manager);

			set_service_manager_ever = new SqlCommand("��������_����������_������_�����������_������", connection);
			set_service_manager_ever.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_service_manager_ever.Parameters.Add("@card_year", SqlDbType.Int);
			set_service_manager_ever.Parameters.Add("@service_manager_code", SqlDbType.BigInt);
			set_service_manager_ever.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_service_manager_ever);

			set_creditcard_flag = new SqlCommand("��������_���������_���������_�����", connection);
			set_creditcard_flag.Parameters.Add("@card_number", SqlDbType.BigInt);
			set_creditcard_flag.Parameters.Add("@card_year", SqlDbType.Int);
			set_creditcard_flag.Parameters.Add("@creditcard", SqlDbType.Bit);
			set_creditcard_flag.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_creditcard_flag);


			// ������� ���������
			auxiliary_auto_set_null = new SqlCommand("��������_��������_����������_���������", connection);
			auxiliary_auto_set_null.Parameters.Add("@card_number", SqlDbType.BigInt);
			auxiliary_auto_set_null.Parameters.Add("@card_year", SqlDbType.Int);
			auxiliary_auto_set_null.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_auto_set_null);

			auxiliary_partner_replace = new SqlCommand("��������_��������_��������_���������", connection);
			auxiliary_partner_replace.Parameters.Add("@card_number", SqlDbType.BigInt);
			auxiliary_partner_replace.Parameters.Add("@card_year", SqlDbType.Int);
			auxiliary_partner_replace.Parameters.Add("@code_partner", SqlDbType.BigInt);
			auxiliary_partner_replace.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_partner_replace);

			auxiliary_auto_replace = new SqlCommand("��������_��������_����������_���������", connection);
			auxiliary_auto_replace.Parameters.Add("@card_number", SqlDbType.BigInt);
			auxiliary_auto_replace.Parameters.Add("@card_year", SqlDbType.Int);
			auxiliary_auto_replace.Parameters.Add("@code_auto", SqlDbType.BigInt);
			auxiliary_auto_replace.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_auto_replace);

			auxiliary_card_insert = new SqlCommand("��������_����������_���������", connection);
			auxiliary_card_insert.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_card_insert.Parameters.Add("@date", SqlDbType.DateTime);
			auxiliary_card_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_card_insert);

			auxiliary_card_set_date = new SqlCommand("��������_���������_����_���������", connection);
			auxiliary_card_set_date.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_card_set_date.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_card_set_date.Parameters.Add("@date", SqlDbType.DateTime);
			auxiliary_card_set_date.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_card_set_date);

			auxiliary_warrant_set = new SqlCommand("��������_���������_�����_���������", connection);
			auxiliary_warrant_set.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_warrant_set.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_warrant_set.Parameters.Add("@warrant_date", SqlDbType.DateTime);
			auxiliary_warrant_set.Parameters.Add("@warrant_number", SqlDbType.BigInt);
			auxiliary_warrant_set.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_warrant_set);

			auxiliary_warrant_open = new SqlCommand("��������_��������_�����_���������", connection);
			auxiliary_warrant_open.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_warrant_open.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_warrant_open.Parameters.Add("@warrant_date", SqlDbType.DateTime);
			auxiliary_warrant_open.Parameters.Add("@warrant_number", SqlDbType.BigInt);
			auxiliary_warrant_open.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_warrant_open);

			auxiliary_run_set = new SqlCommand("��������_���������_������_���������", connection);
			auxiliary_run_set.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_run_set.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_run_set.Parameters.Add("@run", SqlDbType.Int);
			auxiliary_run_set.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_run_set);

			auxiliary_warrant_close = new SqlCommand("��������_��������_�����_���������", connection);
			auxiliary_warrant_close.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_warrant_close.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_warrant_close.Parameters.Add("@close_date", SqlDbType.DateTime);
			auxiliary_warrant_close.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_warrant_close);

			auxiliary_warrant_close_set = new SqlCommand("��������_���������_�����_��������_���������", connection);
			auxiliary_warrant_close_set.Parameters.Add("@number", SqlDbType.BigInt);
			auxiliary_warrant_close_set.Parameters.Add("@year", SqlDbType.Int);
			auxiliary_warrant_close_set.Parameters.Add("@close_date", SqlDbType.DateTime);
			auxiliary_warrant_close_set.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_warrant_close_set);
		}

		public static void PrepareSelectCard(SearchCard search)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card.Parameters["@date_start"].Value = (DateTime)search.DateStart;
			select_card.Parameters["@date_end"].Value = (DateTime)search.DateEnd;
			select_card.Parameters["@no_date"].Value = (bool)search.NoDate;
			select_card.Parameters["@show_cancel"].Value = (bool)search.ShowCancel;
			select_card.Parameters["@owner_mask"].Value = (string)search.OwnerMask;
			select_card.Parameters["@vin_mask"].Value = (string)search.VinMask;
			select_card.Parameters["@sign_mask"].Value = (string)search.SignMask;

			select_card_workshop.Parameters["@date_start"].Value = (DateTime)search.DateStart;
			select_card_workshop.Parameters["@date_end"].Value = (DateTime)search.DateEnd;
			select_card_workshop.Parameters["@no_date"].Value = (bool)search.NoDate;
			select_card_workshop.Parameters["@show_cancel"].Value = (bool)search.ShowCancel;
			select_card_workshop.Parameters["@owner_mask"].Value = (string)search.OwnerMask;
			select_card_workshop.Parameters["@vin_mask"].Value = (string)search.VinMask;
			select_card_workshop.Parameters["@sign_mask"].Value = (string)search.SignMask;
			select_card_workshop.Parameters["@code_workshop"].Value = (long)search.Workshop;
		}

		public static void SelectCardClosed(ListView list, DateTime date)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed.Parameters["@date"].Value = (DateTime)date;
			DbSql.FillList(list, select_card_closed, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectCardClosedNumber(ArrayList array, DateTime date_start, DateTime date_end)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed_number.Parameters["@date_start"].Value = (DateTime)date_start;
			select_card_closed_number.Parameters["@date_end"].Value = (DateTime)date_end;
			DbSql.FillArray(array, select_card_closed_number, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardClosedNumberAvtovaz(ArrayList array, DateTime date_start, DateTime date_end)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed_number_avtovaz.Parameters["@date_start"].Value = (DateTime)date_start;
			select_card_closed_number_avtovaz.Parameters["@date_end"].Value = (DateTime)date_end;
			DbSql.FillArray(array, select_card_closed_number_avtovaz, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardClosedNumberWorkshop(ArrayList array, DateTime date_start, DateTime date_end, long workshop)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed_number_workshop.Parameters["@date_start"].Value = (DateTime)date_start;
			select_card_closed_number_workshop.Parameters["@date_end"].Value = (DateTime)date_end;
			select_card_closed_number_workshop.Parameters["@workshop"].Value = (long)workshop;
			DbSql.FillArray(array, select_card_closed_number_workshop, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardClosedNumberWorkshopAuto(ArrayList array, DateTime date_start, DateTime date_end, long workshop, long auto)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed_number_workshop_auto.Parameters["@date_start"].Value = (DateTime)date_start;
			select_card_closed_number_workshop_auto.Parameters["@date_end"].Value = (DateTime)date_end;
			select_card_closed_number_workshop_auto.Parameters["@workshop"].Value = (long)workshop;
			select_card_closed_number_workshop_auto.Parameters["@auto"].Value = (long)auto;
			DbSql.FillArray(array, select_card_closed_number_workshop_auto, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardOpenNumberWorkshop(ArrayList array, long workshop)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_open_number_workshop.Parameters["@workshop"].Value = (long)workshop;
			DbSql.FillArray(array, select_card_open_number_workshop, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardOpenNumberAuto(ArrayList array, long code_auto)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_open_number_auto.Parameters["@code_auto"].Value = (long)code_auto;
			DbSql.FillArray(array, select_card_open_number_auto, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardClosedNumberWorkshopNal(ArrayList array, DateTime date_start, DateTime date_end, long workshop)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_closed_number_workshop_nal.Parameters["@date_start"].Value = (DateTime)date_start;
			select_card_closed_number_workshop_nal.Parameters["@date_end"].Value = (DateTime)date_end;
			select_card_closed_number_workshop_nal.Parameters["@workshop"].Value = (long)workshop;
			DbSql.FillArray(array, select_card_closed_number_workshop_nal, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardNotClosedDateNumberWorkshop(ArrayList array, long workshop, DateTime date)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_notclosed_date_number_workshop.Parameters["@workshop"].Value	= (long)workshop;
			select_card_notclosed_date_number_workshop.Parameters["@date"].Value		= (DateTime)date;
			DbSql.FillArray(array, select_card_notclosed_date_number_workshop, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardOpenIntervalNumberWorkshop(ArrayList array, long workshop, DateTime date_start, DateTime date_end)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_open_interval_number_workshop.Parameters["@workshop"].Value		= (long)workshop;
			select_card_open_interval_number_workshop.Parameters["@date_start"].Value	= (DateTime)date_start;
			select_card_open_interval_number_workshop.Parameters["@date_end"].Value		= (DateTime)date_end;
			DbSql.FillArray(array, select_card_open_interval_number_workshop, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}


		public static void SelectWarrantNumber(ListView list, long warrant_number)
		{
			// ���������� ������� ������ �� ��������� ������
			select_warrant_number.Parameters["@warrant_number"].Value = (long)warrant_number;
			DbSql.FillList(list, select_warrant_number, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectWarrantNumber(ArrayList array, long warrant_number)
		{
			// ���������� ������� ������ �� ��������� ������
			select_warrant_number.Parameters["@warrant_number"].Value = (long)warrant_number;
			DbSql.FillArray(array, select_warrant_number, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectCardTodayTime(ArrayList array, DateTime today)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_today_time.Parameters["@date"].Value = (DateTime)today;
			DbSql.FillArray(array, select_card_today_time, new DbSql.DelegateMakeElement(DbSqlCard.MakeElementList2));
		}

		public static void SelectCardGoinTime(ArrayList array)
		{
			// ���������� ������� ������ �� ��������� ������
			DbSql.FillArray(array, select_card_goin_time, new DbSql.DelegateMakeElement(DbSqlCard.MakeElementList2));
		}

		public static void SelectCardGoinTimePause(ArrayList array)
		{
			// ���������� ������� ������ �� ��������� ������
			DbSql.FillArray(array, select_card_goin_time_pause, new DbSql.DelegateMakeElement(DbSqlCard.MakeElementList2));
		}

		public static void SelectNumber(ListView list, long number)
		{
			// ���������� ������� ������ �� ��������� ������
			select_number.Parameters["@number"].Value = (long)number;
			DbSql.FillList(list, select_number, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectAuto(ListView list, DtAuto auto)
		{
			// ���������� ������� ������ �� ��������� ������
			select_auto.Parameters["@code_auto"].Value = (long)auto.GetData("���_����������");
			DbSql.FillList(list, select_auto, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static DtCard FindCardAutoRun(DtAuto auto)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_auto_run.Parameters["@code_auto"].Value = (long)auto.GetData("���_����������");
			return (DtCard)DbSql.Find(select_card_auto_run, new DbSql.DelegateMakeElement(DbSqlCard.MakeElement));
		}

		public static void SelectAuto(ListView list, long code_auto)
		{
			// ���������� ������� ������ �� ��������� ������
			select_auto.Parameters["@code_auto"].Value = (long)code_auto;
			DbSql.FillList(list, select_auto, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectPartner(ListView list, long code_partner)
		{
			// ���������� ������� ������ �� ��������� ������
			select_partner.Parameters["@code_partner"].Value = (long)code_partner;
			DbSql.FillList(list, select_partner, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectCardDetail(ListView list, long code_storage)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_detail.Parameters["@code_storage"].Value = (long)code_storage;
			DbSql.FillList(list, select_card_detail, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}

		public static void SelectInListCardReturned(ListView list, DateTime start, DateTime stop)
		{
			// ���������� ������� ������ �� ��������� ������
			select_card_returned.Parameters["@date_start"].Value = start;
			select_card_returned.Parameters["@date_end"].Value = stop;
			DbSql.FillList(list, select_card_returned, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
		}
		public static object MakeElement(SqlDataReader reader)
		{
			DtCard element = new DtCard();
			element.SetData("���_��������", DbSql.GetValueLong(reader, "���_��������"));
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("��������_��������", DbSql.GetValueLong(reader, "��������_��������"));
			element.SetData("��������_��������", DbSql.GetValueLong(reader, "��������_��������"));
			element.SetData("�������������_��������", DbSql.GetValueLong(reader, "�������������_��������"));
			element.CodeAuto =  DbSql.GetValueLong(reader, "����������_��������");
			element.SetData("��������_�������������_��������", DbSql.GetValueString(reader, "��������_�������������_��������"));
			element.SetData("������_��������", DbSql.GetValueInt(reader, "������_��������"));
			element.SetData("������_��������", DbSql.GetValueShort(reader, "������_��������"));
			element.SetData("�����_�����_��������", DbSql.GetValueLong(reader, "�����_�����_��������"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("����_�����_������_��������", DbSql.GetValueDate(reader, "����_�����_������_��������"));
			element.SetData("����_�����_������_��������", DbSql.GetValueDate(reader, "����_�����_������_��������"));
			element.SetData("�������������_��������", DbSql.GetValueLong(reader, "�������������_��������"));
			element.SetData("���_��������_��������", DbSql.GetValueLong(reader, "���_��������_��������"));
			element.SetData("���_������������_��������", DbSql.GetValueLong(reader, "���_������������_��������"));
			element.SetData("�����������_��������", DbSql.GetValueBool(reader, "�����������_��������"));
			element.SetData("����������_��������", DbSql.GetValueBool(reader, "����������_��������"));
			element.SetData("������_���������_��������", DbSql.GetValueLong(reader, "������_���������_��������"));
			element.SetData("����������_��������", DbSql.GetValueString(reader, "����������_��������"));
			element.SetData("������_������_��������", DbSql.GetValueFloat(reader, "������_������_��������"));
            element.SetData("������_������_��������", DbSql.GetValueFloat(reader, "������_������_��������"));
			element.SetData("���������_��������", DbSql.GetValueLong(reader, "���������_��������"));
			element.SetData("���������_������", DbSql.GetValueLong(reader, "���������_������"));
			element.SetData("���������_������", DbSql.GetValueLong(reader, "���������_������"));
			element.SetData("�������������_��", DbSql.GetValueLong(reader, "�������������_��"));
			element.SetData("������_�����", DbSql.GetValueString(reader, "������_�����"));
			element.SetData("�������", DbSql.GetValueBool(reader, "�������"));
			element.SetData("������_�����������", DbSql.GetValueLong(reader, "������_�����������"));
			element.SetData("���������_�����_��������", DbSql.GetValueBool(reader, "���������_�����_��������"));
			//	element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			//	element.SetData("����������_������", DbSql.GetValueString(reader, "����������_������"));
			//	element.SetData("����������_VIN", DbSql.GetValueString(reader, "����������_VIN"));
			//	element.SetData("����������_���������������_����", DbSql.GetValueString(reader, "����������_���������������_����"));
			if (DbSql.IsValueNULL(reader, "�����_������_�������������") == false)
            {
				element.SetData("�����_������_�������������", DbSql.GetValueDate(reader, "�����_������_�������������"));
				element.SetData("����_�����_������_�������������", true);
			}		
			return (object)(DtCard)element;
		}

		public static object MakeElement�ardRate(SqlDataReader reader)
		{
			DtCard element = new DtCard();
			element.SetData("���_��������", DbSql.GetValueLong(reader, "���_��������"));
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("��������_��������", DbSql.GetValueLong(reader, "��������_��������"));
			element.SetData("��������_��������", DbSql.GetValueLong(reader, "��������_��������"));
			element.SetData("�������������_��������", DbSql.GetValueLong(reader, "�������������_��������"));
			element.CodeAuto = DbSql.GetValueLong(reader, "����������_��������");
			element.SetData("��������_�������������_��������", DbSql.GetValueString(reader, "��������_�������������_��������"));
			element.SetData("������_��������", DbSql.GetValueInt(reader, "������_��������"));
					element.SetData("������_��������", DbSql.GetValueShort(reader, "������_��������"));
			element.SetData("�����_�����_��������", DbSql.GetValueLong(reader, "�����_�����_��������"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("����_�����_������_��������", DbSql.GetValueDate(reader, "����_�����_������_��������"));
			element.SetData("����_�����_������_��������", DbSql.GetValueDate(reader, "����_�����_������_��������"));
			element.SetData("�������������_��������", DbSql.GetValueLong(reader, "�������������_��������"));
			element.SetData("���_��������_��������", DbSql.GetValueLong(reader, "���_��������_��������"));
			element.SetData("���_������������_��������", DbSql.GetValueLong(reader, "���_������������_��������"));
			element.SetData("�����������_��������", DbSql.GetValueBool(reader, "�����������_��������"));
			element.SetData("����������_��������", DbSql.GetValueBool(reader, "����������_��������"));
			element.SetData("������_���������_��������", DbSql.GetValueLong(reader, "������_���������_��������"));
			element.SetData("����������_��������", DbSql.GetValueString(reader, "����������_��������"));
			element.SetData("������_������_��������", DbSql.GetValueFloat(reader, "������_������_��������"));
            element.SetData("������_������_��������", DbSql.GetValueFloat(reader, "������_������_��������"));
			element.SetData("���������_��������", DbSql.GetValueLong(reader, "���������_��������"));
			element.SetData("���������_������", DbSql.GetValueLong(reader, "���������_������"));
			element.SetData("���������_������", DbSql.GetValueLong(reader, "���������_������"));
			element.SetData("�������������_��", DbSql.GetValueLong(reader, "�������������_��"));
			element.SetData("������_�����", DbSql.GetValueString(reader, "������_�����"));
			element.SetData("�������", DbSql.GetValueBool(reader, "�������"));
			element.SetData("������_�����������", DbSql.GetValueLong(reader, "������_�����������"));
			element.SetData("��������_������", DbSql.GetValueShort(reader, "��������_������"));
			element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			element.SetData("����������_������", DbSql.GetValueString(reader, "����������_������"));
			element.SetData("����������_VIN", DbSql.GetValueString(reader, "����������_VIN"));
			element.SetData("����������_���������������_����", DbSql.GetValueString(reader, "����������_���������������_����"));
			element.SetData("������_�����������_�������", DbSql.GetValueString(reader, "������_�����������_�������"));
			element.SetData("������������", DbSql.GetValueShort(reader, "������������"));
			element.SetData("������������_��������", DbSql.GetValueBool(reader, "������������_��������"));
			element.SetData("�����_������", DbSql.GetValueShort(reader, "�����_������"));
			element.SetData("�������������_���������", DbSql.GetValueBool(reader, "�������������_���������"));
			
			return (object)(DtCard)element;
		}

		public static ListViewItem MakeLV_List(SqlDataReader reader)
		{
			ListViewItem item = new ListViewItem();
			item.Tag			= DbSql.GetValueLong(reader, "���_��������");
			item.Text			= DbSql.GetValueLongTxt(reader, "�����_��������");
			item.SubItems.Add(DbSql.GetValueDateTxt(reader, "����"));
			item.SubItems.Add(DbSql.GetValueLongTxt(reader, "�����_�����"));
			item.SubItems.Add(DbSql.GetValueString(reader, "��������"));
			item.SubItems.Add(DbSql.GetValueString(reader, "����������_������"));
			item.SubItems.Add(DbSql.GetValueString(reader, "����������_VIN"));
			item.SubItems.Add(DbSql.GetValueString(reader, "����������_���������������_����"));
			item.SubItems.Add(DbSql.GetValueInt(reader, "������_��������").ToString());
			item.SubItems.Add(DbSql.GetValueString(reader, "������_�����������_�������") + "/" + DbSql.GetValueString(reader, "������_�����"));
			item.SubItems.Add(DbSql.GetValueString(reader, "����������_��������"));
			switch(DbSql.GetValueSmallInt(reader, "������_��������"))
			{
				case 1:
					item.BackColor = Color.Yellow;
					break;
				case 2:
					item.BackColor = Color.LightGreen;
					break;
				case 3:
					item.BackColor = Color.Red;
					break;
				case 4:
					item.BackColor = Color.Yellow;
					break;
				case 5:
					item.BackColor = Color.Gray;
					break;
				default:
					break;
			}
			// ��������� ���������/������������ �������
			switch(DbSql.GetValueShort(reader, "������_��������_��������"))
			{
				case 0:
					item.StateImageIndex = 0;
					break;
				case 1:
					item.StateImageIndex = 1;
					break;
				case 2:
					item.StateImageIndex = 2;
					break;
				default:
					item.StateImageIndex = 0;
					break;
			}
			return item;
		}

		

		public static DtCard Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			DtCard element = (DtCard)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}
		public static DtCard Find(long number, int year)
		{
			// ���������� ������� ������ �� �����
			find_number_year.Parameters["@number"].Value = (long)number;
			find_number_year.Parameters["@year"].Value = (int)year;
			DtCard element = (DtCard)DbSql.Find(find_number_year, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static DtCard FindRate(long number, int year)
		{
			// ���������� ������� ������ �� �����
			find_rate.Parameters["@number"].Value	= (long)number;
			find_rate.Parameters["@year"].Value		= (int)year;
			DtCard element = (DtCard)DbSql.Find(find_rate, new DbSql.DelegateMakeElement(MakeElement�ardRate));
			return element;
		}

		public static object MakeElementList(SqlDataReader reader)
		{
			DtCard element = new DtCard();
			element.SetData("���_��������", DbSql.GetValueLong(reader, "���_��������"));
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("������_��������", DbSql.GetValueShort(reader, "������_��������"));
			element.SetData("�����_�����_��������", DbSql.GetValueLong(reader, "�����_�����"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("������_��������_��������", DbSql.GetValueShort(reader, "������_��������_��������"));
			element.SetData("����������_��������", DbSql.GetValueString(reader, "����������_��������"));
			element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			element.SetData("����������_������", DbSql.GetValueString(reader, "����������_������"));
			element.SetData("����������_VIN", DbSql.GetValueString(reader, "����������_VIN"));
			element.SetData("����������_���������������_����", DbSql.GetValueString(reader, "����������_���������������_����"));
			element.SetData("������_��������", DbSql.GetValueInt(reader, "������_��������"));
			element.SetData("������_�����", DbSql.GetValueString(reader, "������_�����"));
			element.SetData("������_�����������_�������", DbSql.GetValueString(reader, "������_�����������_�������"));
			return (object)(DtCard)element;
		}

		public static object MakeElementList2(SqlDataReader reader)
		{
			DtCard element = new DtCard();
			element.SetData("���_��������", DbSql.GetValueLong(reader, "���_��������"));
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("������_��������", DbSql.GetValueShort(reader, "������_��������"));
			element.SetData("�����_�����_��������", DbSql.GetValueLong(reader, "�����_�����"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("������_��������_��������", DbSql.GetValueShort(reader, "������_��������_��������"));
			element.SetData("����������_��������", DbSql.GetValueString(reader, "����������_��������"));
			element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			element.SetData("����������_������", DbSql.GetValueString(reader, "����������_������"));
			element.SetData("����������_VIN", DbSql.GetValueString(reader, "����������_VIN"));
			element.SetData("����������_���������������_����", DbSql.GetValueString(reader, "����������_���������������_����"));
			element.SetData("������_��������", DbSql.GetValueInt(reader, "������_��������"));
			element.SetData("������_�����", DbSql.GetValueString(reader, "������_�����"));
			return (object)(DtCard)element;
		}

		public static DtCard FindList(DbCard card)
		{
			// ���������� ������� ������ �� �����
			select_number_year.Parameters["@number"].Value = (long)card.Number;
			select_number_year.Parameters["@year"].Value = (int)card.Year;
			DtCard element = (DtCard)DbSql.Find(select_number_year, new DbSql.DelegateMakeElement(MakeElementList));
			return element;
		}

		public static DtCard FindList(DtCard card)
		{
			// ���������� ������� ������ �� �����
			select_number_year.Parameters["@number"].Value = (long)card.GetData("�����_��������");
			select_number_year.Parameters["@year"].Value = (int)card.GetData("���_��������");
			DtCard element = (DtCard)DbSql.Find(select_number_year, new DbSql.DelegateMakeElement(MakeElementList));
			return element;
		}


		public static bool SetStatusControl(long code, short status_control)
		{
			// ��������� ������������ ������� ��������
			set_status_control.Parameters["@code"].Value = (long)code;
			set_status_control.Parameters["@status_control"].Value = (short)status_control;
			return DbSql.ExecuteCommandError(set_status_control);
		}

		public static bool SetMaster(DbCard card, DbStaff master)
		{
			// ��������� ������������ ������� ��������
			set_master.Parameters["@card_number"].Value = (long)card.Number;
			set_master.Parameters["@card_year"].Value = (int)card.Year;
			set_master.Parameters["@card_code"].Value = (long)0;
			set_master.Parameters["@master_code"].Value = (long)master.Code;
			return DbSql.ExecuteCommandError(set_master);
		}

		public static bool SetLicenceVehicle(DbCard card, long code)
		{
			// ��������� ������������ ������� ��������
			set_licence_vehicle.Parameters["@card_number"].Value	= (long)card.Number;
			set_licence_vehicle.Parameters["@card_year"].Value		= (int)card.Year;
			set_licence_vehicle.Parameters["@code_licence"].Value	= (long)code;
			return DbSql.ExecuteCommandError(set_licence_vehicle);
		}

		public static bool SetLicencePlate(DbCard card, string plate_number, string plate_region)
		{
			// ��������� ������������ ������� ��������
			set_licence_plate.Parameters["@card_number"].Value			= (long)card.Number;
			set_licence_plate.Parameters["@card_year"].Value			= (int)card.Year;
			set_licence_plate.Parameters["@licence_plate_number"].Value	= (string)plate_number;
			set_licence_plate.Parameters["@licence_plate_region"].Value	= (string)plate_region;
			return DbSql.ExecuteCommandError(set_licence_plate);
		}

		public static bool SetReturnFlag(DbCard card, bool return_flag)
		{
			// ��������� ������������ ������� ��������
			set_return_flag.Parameters["@card_number"].Value			= (long)card.Number;
			set_return_flag.Parameters["@card_year"].Value			= (int)card.Year;
			set_return_flag.Parameters["@returned"].Value				= (bool)return_flag;
			return DbSql.ExecuteCommandError(set_return_flag);
		}

		public static bool SetMaster(DbCard card, DtStaff master)
		{
			// ��������� ������������ ������� ��������
			set_master.Parameters["@card_number"].Value = (long)card.Number;
			set_master.Parameters["@card_year"].Value = (int)card.Year;
			set_master.Parameters["@card_code"].Value = (long)0;
			set_master.Parameters["@master_code"].Value = (long)master.GetData("���_��������");
			return DbSql.ExecuteCommandError(set_master);
		}

		public static bool SetMaster(long card_number, int card_year, DtStaff master)
		{
			// ��������� ������������ ������� ��������
			set_master.Parameters["@card_number"].Value = (long)card_number;
			set_master.Parameters["@card_year"].Value = (int)card_year;
			set_master.Parameters["@card_code"].Value = (long)0;
			set_master.Parameters["@master_code"].Value = (long)master.GetData("���_��������");
			return DbSql.ExecuteCommandError(set_master);
		}

		public static bool SetSupervisorGuaranty(long card_number, int card_year, long supervisor_guaranty)
		{
			// ��������� ������������ ������� ��������
			set_supervisor_guaranty.Parameters["@card_number"].Value		= (long)card_number;
			set_supervisor_guaranty.Parameters["@card_year"].Value			= (int)card_year;
			set_supervisor_guaranty.Parameters["@supervisor_guaranty"].Value= (long)supervisor_guaranty;
			return DbSql.ExecuteCommandError(set_supervisor_guaranty);
		}

		public static bool SetSupervisorWhole(long card_number, int card_year, long supervisor_whole)
		{
			// ��������� ������������ ������� ��������
			set_supervisor_whole.Parameters["@card_number"].Value		= (long)card_number;
			set_supervisor_whole.Parameters["@card_year"].Value			= (int)card_year;
			set_supervisor_whole.Parameters["@supervisor_whole"].Value	= (long)supervisor_whole;
			return DbSql.ExecuteCommandError(set_supervisor_whole);
		}

		public static bool SetSupervisorPayment(long card_number, int card_year, long supervisor_payment)
		{
			// ��������� ������������ ������� ��������
			set_supervisor_payment.Parameters["@card_number"].Value		= (long)card_number;
			set_supervisor_payment.Parameters["@card_year"].Value			= (int)card_year;
			set_supervisor_payment.Parameters["@supervisor_payment"].Value= (long)supervisor_payment;
			return DbSql.ExecuteCommandError(set_supervisor_payment);
		}

		public static bool SetDiscount(DbCard card, float discount, long code_discount)
		{
			// ��������� ������������ ������� ��������
			set_discount.Parameters["@card_number"].Value	= (long)card.Number;
			set_discount.Parameters["@card_year"].Value		= (int)card.Year;
			set_discount.Parameters["@card_code"].Value		= (long)0;
			set_discount.Parameters["@code_discount"].Value	= (long)code_discount;
			set_discount.Parameters["@discount"].Value		= (float)discount;
			return DbSql.ExecuteCommandError(set_discount);
		}

        public static bool SetDiscountParts(DbCard card, float discount)
        {
            // ��������� ������������ ������� ��������
            set_discount_parts.Parameters["@card_number"].Value = (long)card.Number;
            set_discount_parts.Parameters["@card_year"].Value = (int)card.Year;
            set_discount_parts.Parameters["@discount"].Value = (float)discount;
            return DbSql.ExecuteCommandError(set_discount_parts);
        }

		public static bool SetDiscount(long card_number, int card_year, float discount, long code_discount)
		{
			// ��������� ������������ ������� ��������
			set_discount.Parameters["@card_number"].Value	= (long)card_number;
			set_discount.Parameters["@card_year"].Value		= (int)card_year;
			set_discount.Parameters["@card_code"].Value		= (long)0;
			set_discount.Parameters["@code_discount"].Value	= (long)code_discount;
			set_discount.Parameters["@discount"].Value		= (float)discount;
			return DbSql.ExecuteCommandError(set_discount);
		}

		public static bool SetPrint(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			set_print.Parameters["@card_number"].Value	= (long)card_number;
			set_print.Parameters["@card_year"].Value	= (int)card_year;
			return DbSql.ExecuteCommandError(set_print);
		}

		public static bool SetServiceManager(DtCard card, DtStaff service_manager)
		{
			// ��������� ������������ ������� ��������
			set_service_manager.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			set_service_manager.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			set_service_manager.Parameters["@service_manager_code"].Value = (long)service_manager.GetData("���_��������");
			return DbSql.ExecuteCommandError(set_service_manager);
		}
		public static bool SetServiceManagerEver(DtCard card, DtStaff service_manager)
		{
			// ��������� ������������ ������� ��������
			set_service_manager_ever.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			set_service_manager_ever.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			set_service_manager_ever.Parameters["@service_manager_code"].Value = (long)service_manager.GetData("���_��������");
			return DbSql.ExecuteCommandError(set_service_manager_ever);
		}

		public static bool SetCreditcardFlag(DbCard card, bool creditcard_flag)
		{
			// ��������� ������������ ������� ��������
			set_creditcard_flag.Parameters["@card_number"].Value		= (long)card.Number;
			set_creditcard_flag.Parameters["@card_year"].Value			= (int)card.Year;
			set_creditcard_flag.Parameters["@creditcard"].Value			= (bool)creditcard_flag;
			return DbSql.ExecuteCommandError(set_creditcard_flag);
		}

		public static bool CardSetAgreedPickupTime(long card_number, int card_year, DateTime pickupTime)
		{
			// ��������� ������������ ������� ��������
			card_set_agreedPickupTime.Parameters["@number"].Value = (long)card_number;
			card_set_agreedPickupTime.Parameters["@year"].Value = (int)card_year;
			card_set_agreedPickupTime.Parameters["@date"].Value = (DateTime)pickupTime;
			return DbSql.ExecuteCommandError(card_set_agreedPickupTime);
		}

		#region ������� ���������
		public static bool AuxiliaryAutoSetNull(long card_number, int card_year)
		{
			// ��������� ������������ ������� ��������
			auxiliary_auto_set_null.Parameters["@card_number"].Value	= (long)card_number;
			auxiliary_auto_set_null.Parameters["@card_year"].Value		= (int)card_year;
			return DbSql.ExecuteCommandError(auxiliary_auto_set_null);
		}
		public static bool AuxiliaryAutoSetNull(DtCard card)
		{
			// ��������� ������������ ������� ��������
			auxiliary_auto_set_null.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			auxiliary_auto_set_null.Parameters["@card_year"].Value		= (int)card.GetData("���_��������");
			return DbSql.ExecuteCommandError(auxiliary_auto_set_null);
		}
		public static bool AuxiliaryPartnerReplace(DtCard card, DbPartner partner)
		{
			// ��������� ������������ ������� ��������
			auxiliary_partner_replace.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			auxiliary_partner_replace.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			auxiliary_partner_replace.Parameters["@code_partner"].Value	= (long)partner.Code;
			return DbSql.ExecuteCommandError(auxiliary_partner_replace);
		}
		public static bool AuxiliaryAutoReplace(DtCard card, DtAuto auto)
		{
			// ��������� ������������ ������� ��������
			auxiliary_auto_replace.Parameters["@card_number"].Value	= (long)card.GetData("�����_��������");
			auxiliary_auto_replace.Parameters["@card_year"].Value	= (int)card.GetData("���_��������");
			auxiliary_auto_replace.Parameters["@code_auto"].Value	= (long)auto.GetData("���_����������");
			return DbSql.ExecuteCommandError(auxiliary_auto_replace);
		}
		public static bool AuxiliaryCardInsert(long card_number, DateTime card_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_card_insert.Parameters["@number"].Value	= (long)card_number;
			auxiliary_card_insert.Parameters["@date"].Value	= (DateTime)card_date;
			return DbSql.ExecuteCommandError(auxiliary_card_insert);
		}

		public static bool AuxiliaryCardSetDate(long card_number, int card_year, DateTime card_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_card_set_date.Parameters["@number"].Value	= (long)card_number;
			auxiliary_card_set_date.Parameters["@year"].Value	= (int)card_year;
			auxiliary_card_set_date.Parameters["@date"].Value	= (DateTime)card_date;
			return DbSql.ExecuteCommandError(auxiliary_card_set_date);
		}
		public static bool AuxiliaryWarrantSet(long card_number, int card_year, long warrant_number, DateTime warrant_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_warrant_set.Parameters["@number"].Value			= (long)card_number;
			auxiliary_warrant_set.Parameters["@year"].Value				= (int)card_year;
			auxiliary_warrant_set.Parameters["@warrant_date"].Value		= (DateTime)warrant_date;
			auxiliary_warrant_set.Parameters["@warrant_number"].Value	= (long)warrant_number;
			return DbSql.ExecuteCommandError(auxiliary_warrant_set);
		}
		public static bool AuxiliaryWarrantOpen(long card_number, int card_year, long warrant_number, DateTime warrant_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_warrant_open.Parameters["@number"].Value			= (long)card_number;
			auxiliary_warrant_open.Parameters["@year"].Value			= (int)card_year;
			auxiliary_warrant_open.Parameters["@warrant_date"].Value	= (DateTime)warrant_date;
			auxiliary_warrant_open.Parameters["@warrant_number"].Value	= (long)warrant_number;
			return DbSql.ExecuteCommandError(auxiliary_warrant_open);
		}
		public static bool AuxiliaryRunSet(long card_number, int card_year, int run)
		{
			// ��������� ������������ ������� ��������
			auxiliary_run_set.Parameters["@number"].Value			= (long)card_number;
			auxiliary_run_set.Parameters["@year"].Value				= (int)card_year;
			auxiliary_run_set.Parameters["@run"].Value		= (int)run;
			return DbSql.ExecuteCommandError(auxiliary_run_set);
		}
		public static bool AuxiliaryWarrantClose(long card_number, int card_year, DateTime close_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_warrant_close.Parameters["@number"].Value			= (long)card_number;
			auxiliary_warrant_close.Parameters["@year"].Value			= (int)card_year;
			auxiliary_warrant_close.Parameters["@close_date"].Value		= (DateTime)close_date;
			return DbSql.ExecuteCommandError(auxiliary_warrant_close);
		}
		public static bool AuxiliaryWarrantCloseSet(long card_number, int card_year, DateTime close_date)
		{
			// ��������� ������������ ������� ��������
			auxiliary_warrant_close_set.Parameters["@number"].Value			= (long)card_number;
			auxiliary_warrant_close_set.Parameters["@year"].Value			= (int)card_year;
			auxiliary_warrant_close_set.Parameters["@close_date"].Value		= (DateTime)close_date;
			return DbSql.ExecuteCommandError(auxiliary_warrant_close_set);
		}
		#endregion
	}

	// ��������� ��� ������ � ����
	public class SearchCard
	{
		private DateTime	date_start;
		private DateTime	date_end;
		private bool		no_date;
		private bool		show_cancel;
		private string		owner_mask;
		private string		vin_mask;
		private string		sign_mask;
		private long		code_workshop;


		public SearchCard()
		{
			ClearTemp();
		}
		public void ClearTemp()
		{
			owner_mask = "";
			vin_mask = "";
			sign_mask = "";
		}
		public void SetDates(DateTime start, DateTime end)
		{
			date_start = start;
			date_end = end;
		}
		public void SetNoDate(bool data)
		{
			no_date = data;
		}
		public void SetShowCancel(bool data)
		{
			show_cancel = data;
		}
		public void SetOwnerMask(string data)
		{
			owner_mask = data;
		}
		public void SetVinMask(string data)
		{
			vin_mask = data;
		}
		public void SetSignMask(string data)
		{
			sign_mask = data;
		}
		public void SetWorkshop(long data)
		{
			code_workshop = data;
		}

		public DateTime DateStart
		{
			get{ return date_start; }
		}
		public DateTime DateEnd
		{
			get{ return date_end; }
		}
		public bool NoDate
		{
			get{ return no_date; }
		}
		public bool ShowCancel
		{
			get{ return show_cancel; }
		}
		public string OwnerMask
		{
			get{ return owner_mask; }
		}
		public string VinMask
		{
			get{ return vin_mask; }
		}
		public string SignMask
		{
			get{ return sign_mask; }
		}
		public long Workshop
		{
			get{ return code_workshop; }
		}
	}
}
