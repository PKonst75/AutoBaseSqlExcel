using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAuto.
	/// </summary>
	public class DbSqlAuto
	{
		public static SqlCommand		find;
		public static SqlCommand		find_vin;
		public static SqlCommand		select;
		public static SqlCommand		select_double;
		public static SqlCommand		select_vin;
		public static SqlCommand		select_body;
		public static SqlCommand		select_model;
		public static SqlCommand		delete;
		public static SqlCommand		insert;
		public static SqlCommand		update;
		public static SqlCommand		update_licence_plate;
		public static SqlCommand		update_storage_avaliable;
        public static SqlCommand        update_pts;

		public static SqlCommand		select_receive_document;
		public static SqlCommand		select_storage_v1;
		public static SqlCommand		select_storage_v1_vin;
		public static SqlCommand		select_storage_v1_receivecomment;
		public static SqlCommand		select_storage_v1_noppp;

		public static SqlCommand		auxiliary_auto_replace;
		public static SqlCommand		auxiliary_set_selldate;

		public static SqlCommand		select_storage_avaliable;
		public static SqlCommand		select_storage_avaliable_mask;
		public static SqlCommand		select_storage_avaliable_mask_options;
		public static SqlCommand		select_storage_avaliable_find;
		public static SqlCommand		select_auto_add;			// ����� ��������������� ������������
		public static SqlCommand		select_directions;			// ������ ����������� �� ���� ����������
        public static SqlCommand        select_directions_vin;		// ������ ����������� �� ���� ���������� - ����� �� VIN

		// ������ � ���������
		public static SqlCommand		reserve_insert;
		public static SqlCommand		reserve_remove;

		// ������ � �����-������
		public static SqlCommand		price_insert;
		public static SqlCommand		price_update;
        public static SqlCommand        color_price_update;
		

		public DbSqlAuto()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			find = new SqlCommand("����������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_vin = new SqlCommand("����������_�����_VIN", connection);
			find_vin.Parameters.Add("@vin", SqlDbType.VarChar);
			find_vin.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("����������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_storage_v1 = new SqlCommand("����������_�������_�����_�1", connection);
			select_storage_v1.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable = new SqlCommand("����������_�������_�����_�������", connection);
			select_storage_avaliable.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable_mask = new SqlCommand("����������_�������_�����_�������_�����", connection);
			select_storage_avaliable_mask.Parameters.Add("@model_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@variant_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@color_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@year_mask", SqlDbType.Int);
			select_storage_avaliable_mask.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable_mask_options = new SqlCommand("����������_�������_�����_�������_�����_������������", connection);
			select_storage_avaliable_mask_options.Parameters.Add("@model_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask_options.Parameters.Add("@variant_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask_options.Parameters.Add("@color_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask_options.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask_options.Parameters.Add("@year_mask", SqlDbType.Int);
			select_storage_avaliable_mask_options.Parameters.Add("@option1", SqlDbType.BigInt);
			select_storage_avaliable_mask_options.Parameters.Add("@option2", SqlDbType.BigInt);
			select_storage_avaliable_mask_options.Parameters.Add("@option3", SqlDbType.BigInt);
			select_storage_avaliable_mask_options.Parameters.Add("@option4", SqlDbType.BigInt);
			select_storage_avaliable_mask_options.Parameters.Add("@option5", SqlDbType.BigInt);
			select_storage_avaliable_mask_options.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable_find = new SqlCommand("����������_�������_�����_�������_�����", connection);
			select_storage_avaliable_find.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_storage_avaliable_find.CommandType = CommandType.StoredProcedure;

			select_storage_v1_vin = new SqlCommand("����������_�������_�����_�1_VIN", connection);
			select_storage_v1_vin.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_storage_v1_vin.CommandType = CommandType.StoredProcedure;

			select_storage_v1_receivecomment = new SqlCommand("����������_�������_�����_�1_���������_����������", connection);
			select_storage_v1_receivecomment.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_storage_v1_receivecomment.CommandType = CommandType.StoredProcedure;

			select_storage_v1_noppp = new SqlCommand("����������_�������_�����_�1_���_���", connection);
			select_storage_v1_noppp.CommandType = CommandType.StoredProcedure;
			select_storage_v1_noppp.CommandTimeout = 600;

			select_double = new SqlCommand("����������_�������_�����", connection);
			select_double.CommandType = CommandType.StoredProcedure;
			select_double.CommandTimeout = 300;

			select_vin = new SqlCommand("����������_�������_VIN", connection);
			select_vin.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_vin.CommandType = CommandType.StoredProcedure;

			select_body = new SqlCommand("����������_�������_�����", connection);
			select_body.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_body.CommandType = CommandType.StoredProcedure;

			select_model = new SqlCommand("����������_�������_������", connection);
			select_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_model.CommandType = CommandType.StoredProcedure;

			select_directions = new SqlCommand("�����������_�����_�������", connection);
			select_directions.Parameters.Add("@vin", SqlDbType.VarChar);
			select_directions.CommandType = CommandType.StoredProcedure;

            select_directions_vin = new SqlCommand("�����������_VIN_�������", connection);
            select_directions_vin.Parameters.Add("@vin", SqlDbType.VarChar);
            select_directions_vin.CommandType = CommandType.StoredProcedure;

			delete = new SqlCommand("����������_��������", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			insert = new SqlCommand("����������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert.Parameters.Add("@id_vin", SqlDbType.VarChar);
			insert.Parameters.Add("@is_id_vin", SqlDbType.Bit);
			insert.Parameters.Add("@id_vin_origin", SqlDbType.VarChar);
			insert.Parameters.Add("@id_body", SqlDbType.VarChar);
			insert.Parameters.Add("@id_engine", SqlDbType.VarChar);
			insert.Parameters.Add("@id_frame", SqlDbType.VarChar);
			insert.Parameters.Add("@id_parts", SqlDbType.BigInt);
			insert.Parameters.Add("@is_id_parts", SqlDbType.Bit);
			insert.Parameters.Add("@code_producer", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@code_color", SqlDbType.BigInt);
			insert.Parameters.Add("@code_variant", SqlDbType.BigInt);
			insert.Parameters.Add("@id_sign", SqlDbType.VarChar);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("����������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_model", SqlDbType.BigInt);
			update.Parameters.Add("@id_vin", SqlDbType.VarChar);
			update.Parameters.Add("@is_id_vin", SqlDbType.Bit);
			update.Parameters.Add("@id_vin_origin", SqlDbType.VarChar);
			update.Parameters.Add("@id_body", SqlDbType.VarChar);
			update.Parameters.Add("@id_engine", SqlDbType.VarChar);
			update.Parameters.Add("@id_frame", SqlDbType.VarChar);
			update.Parameters.Add("@id_parts", SqlDbType.BigInt);
			update.Parameters.Add("@is_id_parts", SqlDbType.Bit);
			update.Parameters.Add("@code_producer", SqlDbType.BigInt);
			update.Parameters.Add("@year", SqlDbType.Int);
			update.Parameters.Add("@code_color", SqlDbType.BigInt);
			update.Parameters.Add("@code_variant", SqlDbType.BigInt);
			update.Parameters.Add("@id_sign", SqlDbType.VarChar);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			update_licence_plate = new SqlCommand("����������_���������_�����_����", connection);
			update_licence_plate.Parameters.Add("@code", SqlDbType.BigInt);
			update_licence_plate.Parameters.Add("@licence_plate_number", SqlDbType.VarChar);
			update_licence_plate.Parameters.Add("@licence_plate_region", SqlDbType.VarChar);
			update_licence_plate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_licence_plate);

			update_storage_avaliable = new SqlCommand("����������_��������_��_������", connection);
			update_storage_avaliable.Parameters.Add("@code", SqlDbType.BigInt);
			update_storage_avaliable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_storage_avaliable);

			select_receive_document = new SqlCommand("����������_���������_�������_��������", connection);
			select_receive_document.Parameters.Add("@code_document", SqlDbType.BigInt);
			select_receive_document.CommandType = CommandType.StoredProcedure;

			auxiliary_auto_replace = new SqlCommand("����������_������_���������", connection);
			auxiliary_auto_replace.Parameters.Add("@code_old", SqlDbType.BigInt);
			auxiliary_auto_replace.Parameters.Add("@code_new", SqlDbType.BigInt);
			auxiliary_auto_replace.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_auto_replace);

			auxiliary_set_selldate = new SqlCommand("����������_���������_����_�������", connection);
			auxiliary_set_selldate.Parameters.Add("@code", SqlDbType.BigInt);
			auxiliary_set_selldate.Parameters.Add("@selldate", SqlDbType.DateTime);
			auxiliary_set_selldate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_set_selldate);

			// ������ � ���������
			reserve_insert = new SqlCommand("����������_��������������", connection);
			reserve_insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			reserve_insert.Parameters.Add("@date_end", SqlDbType.DateTime);
			reserve_insert.Parameters.Add("@comment", SqlDbType.VarChar);
			reserve_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reserve_insert);

			reserve_remove = new SqlCommand("����������_��������������_������", connection);
			reserve_remove.Parameters.Add("@code_auto", SqlDbType.BigInt);
			reserve_remove.Parameters.Add("@comment", SqlDbType.VarChar);
			reserve_remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reserve_remove);

			select_auto_add = new SqlCommand("����������_�������_�����_�����", connection);
			select_auto_add.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_auto_add.Parameters.Add("@summ", SqlDbType.Real);
			select_auto_add.CommandType = CommandType.StoredProcedure;
			select_auto_add.Parameters["@summ"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(select_auto_add);

			// ������ � �����������
			price_insert = new SqlCommand("����������_�����_����������", connection);
			price_insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			price_insert.Parameters.Add("@code_variant", SqlDbType.BigInt);
			price_insert.Parameters.Add("@year", SqlDbType.Int);
			price_insert.Parameters.Add("@price", SqlDbType.Float);
			price_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(price_insert);

			price_update = new SqlCommand("����������_�����_���������", connection);
			price_update.Parameters.Add("@code_model", SqlDbType.BigInt);
			price_update.Parameters.Add("@code_variant", SqlDbType.BigInt);
			price_update.Parameters.Add("@year", SqlDbType.Int);
			price_update.Parameters.Add("@price", SqlDbType.Float);
			price_update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(price_update);

            color_price_update = new SqlCommand("����������_���������_�������_����", connection);
            color_price_update.Parameters.Add("@code", SqlDbType.BigInt);
            color_price_update.Parameters.Add("@price", SqlDbType.Float);
            color_price_update.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(color_price_update);

            update_pts = new SqlCommand("����������_���������_���", connection);
            update_pts.Parameters.Add("@code", SqlDbType.BigInt);
            update_pts.Parameters.Add("@pts", SqlDbType.Bit);
            update_pts.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_pts);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("�����_�����", DbSql.GetValueString(reader, "�����_�����"));
			element.SetData("���������", DbSql.GetValueString(reader, "����������"));

			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("�������������", DbSql.GetValueString(reader, "�������������"));
			return (object)element;
		}

		public static object MakeElementFind(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("������_���_����������_������", DbSql.GetValueLong(reader, "������_���_����������_������"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("VIN_�����������", DbSql.GetValueBool(reader, "VIN_�����������"));
			element.SetData("VIN_�������������", DbSql.GetValueString(reader, "VIN_�������������"));
			element.SetData("�����_�����", DbSql.GetValueString(reader, "�����_�����"));
			element.SetData("�����_�����", DbSql.GetValueString(reader, "�����_�����"));
			element.SetData("�����_���������", DbSql.GetValueString(reader, "�����_���������"));
			element.SetData("�����_���������_����������", DbSql.GetValueLong(reader, "�����_���������_����������"));
			element.SetData("�����_���������_�����������_����������", DbSql.GetValueBool(reader, "�����_���������_�����������_����������"));
			element.SetData("���_������", DbSql.GetValueInt(reader, "���_������"));
            element.SetData("���_�������������_����������", DbSql.GetValueLong(reader, "���_�������������_����������"));
			element.SetData("������_���_����������_����", DbSql.GetValueLong(reader, "������_���_����������_����"));
			element.SetData("������_���_����������_����������", DbSql.GetValueLong(reader, "������_���_����������_����������"));
			element.SetData("�����_����", DbSql.GetValueString(reader, "�����_����"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			element.SetData("�����_����_�����", DbSql.GetValueString(reader, "�����_����_�����"));
			element.SetData("�����_����_������", DbSql.GetValueString(reader, "�����_����_������"));

			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("�������������", DbSql.GetValueString(reader, "�������������"));
			element.SetData("����������_����������", DbSql.GetValueString(reader, "����������_����������"));
			element.SetData("����������_����", DbSql.GetValueString(reader, "����������_����"));

			// ������ ������ ���� �������
			element.SetData("�������_����", DbSql.GetValueDate(reader, "�������_����"));
			if(DbSql.IsValueNULL(reader, "�������_����") == false)
				element.SetData("����_�������_����", true);
			return (object)element;
		}


		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtAuto element = (DtAuto)MakeElement(reader);
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

		public static void SelectInList(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListDouble(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select_double, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListVin(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_vin.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_vin, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArrayDirection(ArrayList array, string vin)
		{
			// ���������� ������� ������ �� �����
			select_directions.Parameters["@vin"].Value = (string)vin;
			DbSql.FillArray(array, select_directions, new DbSql.DelegateMakeElement(MakeElementDirection));
		}

        public static void SelectInArrayDirectionVIN(ArrayList array, string vin)
        {
            // ���������� ������� ������ �� �����
            select_directions_vin.Parameters["@vin"].Value = (string)vin;
            DbSql.FillArray(array, select_directions_vin, new DbSql.DelegateMakeElement(MakeElementDirection));
        }

		public static void SelectInListBody(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_body.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_body, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListModel(ListView list, long code_model)
		{
			// ���������� ������� ������ �� �����
			select_model.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select_model, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static bool Delete(long code)
		{
			// ���������� ������� ������ �� �����
			delete.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete);	
		}

		public static DtAuto Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			return (DtAuto)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtAuto Find(string vin)
		{
			// ���������� ������� ������ �� �����
			find_vin.Parameters["@vin"].Value = (string)vin;
			return (DtAuto)DbSql.Find(find_vin, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtAuto Insert(DtAuto auto)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@code_model"].Value		= (long)auto.GetData("������_���_����������_������");
			insert.Parameters["@id_vin"].Value			= (string)auto.GetData("VIN");
			insert.Parameters["@id_vin_origin"].Value	= (string)auto.GetData("VIN_�������������");
			insert.Parameters["@is_id_vin"].Value		= (bool)auto.GetData("VIN_�����������");
			insert.Parameters["@id_body"].Value			= (string)auto.GetData("�����_�����");
			insert.Parameters["@id_frame"].Value		= (string)auto.GetData("�����_�����");
			insert.Parameters["@id_engine"].Value		= (string)auto.GetData("�����_���������");
			insert.Parameters["@id_parts"].Value		= (long)auto.GetData("�����_���������_����������");
			insert.Parameters["@is_id_parts"].Value		= (bool)auto.GetData("�����_���������_�����������_����������");
			insert.Parameters["@year"].Value			= (int)auto.GetData("���_������");
			insert.Parameters["@code_producer"].Value	= (long)auto.GetData("���_�������������_����������");
			insert.Parameters["@code_color"].Value		= (long)auto.GetData("������_���_����������_����");
			insert.Parameters["@code_variant"].Value	= (long)auto.GetData("������_���_����������_����������");
			insert.Parameters["@id_sign"].Value			= (string)auto.GetData("�����_����");
			insert.Parameters["@comment"].Value			= (string)auto.GetData("����������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			auto.SetData("���_����������",(long)insert.Parameters["@code"].Value);
			return auto;
		}

		public static bool Update(DtAuto auto)
		{
			// ���������� ������� ������ �� �����
			update.Parameters["@code"].Value			= (long)auto.GetData("���_����������");
			update.Parameters["@code_model"].Value		= (long)auto.GetData("������_���_����������_������");
			update.Parameters["@id_vin"].Value			= (string)auto.GetData("VIN");
			update.Parameters["@id_vin_origin"].Value	= (string)auto.GetData("VIN_�������������");
			update.Parameters["@is_id_vin"].Value		= (bool)auto.GetData("VIN_�����������");
			update.Parameters["@id_body"].Value			= (string)auto.GetData("�����_�����");
			update.Parameters["@id_frame"].Value		= (string)auto.GetData("�����_�����");
			update.Parameters["@id_engine"].Value		= (string)auto.GetData("�����_���������");
			update.Parameters["@id_parts"].Value		= (long)auto.GetData("�����_���������_����������");
			update.Parameters["@is_id_parts"].Value		= (bool)auto.GetData("�����_���������_�����������_����������");
			update.Parameters["@year"].Value			= (int)auto.GetData("���_������");
			update.Parameters["@code_producer"].Value	= (long)auto.GetData("���_�������������_����������");
			update.Parameters["@code_color"].Value		= (long)auto.GetData("������_���_����������_����");
			update.Parameters["@code_variant"].Value	= (long)auto.GetData("������_���_����������_����������");
			update.Parameters["@id_sign"].Value			= (string)auto.GetData("�����_����");
			update.Parameters["@comment"].Value			= (string)auto.GetData("����������");
			return DbSql.ExecuteCommandError(update);
		}

		public static bool UpdateLicencePlate(long code_auto, string licence_plate_number, string licence_plate_region)
		{
			// ���������� ������� ������ �� �����
			update_licence_plate.Parameters["@code"].Value					= (long)code_auto;
			update_licence_plate.Parameters["@licence_plate_number"].Value	= (string)licence_plate_number;
			update_licence_plate.Parameters["@licence_plate_region"].Value	= (string)licence_plate_region;
			return DbSql.ExecuteCommandError(update_licence_plate);
		}

        public static bool UpdateColorPrice(long code_auto, float price)
        {
            // ���������� ������� ������ �� �����
            color_price_update.Parameters["@code"].Value = (long)code_auto;
            color_price_update.Parameters["@price"].Value = (float)price;
            return DbSql.ExecuteCommandError(color_price_update);
        }

        public static bool UpdatePts(long code_auto, bool pts)
        {
            // ��������� ������� ��� ��/���
            update_pts.Parameters["@code"].Value = (long)code_auto;
            update_pts.Parameters["@pts"].Value = (bool)pts;
            return DbSql.ExecuteCommandError(update_pts);
        }

		public static bool UpdateStorageAvaliable(long code_auto)
		{
			// ���������� ������� ������ �� �����
			update_storage_avaliable.Parameters["@code"].Value				= (long)code_auto;
			return DbSql.ExecuteCommandError(update_storage_avaliable);
		}

		public static bool AuxiliaryAutoReplace(long code_old, long code_new)
		{
			// ���������� ������� ������ �� �����
			auxiliary_auto_replace.Parameters["@code_old"].Value = (long)code_old;
			auxiliary_auto_replace.Parameters["@code_new"].Value = (long)code_new;
			return DbSql.ExecuteCommandError(auxiliary_auto_replace);
		}

		public static bool AuxiliarySetSellDate(long code_auto, DateTime sell_date)
		{
			// ���������� ������� ������ �� �����
			auxiliary_set_selldate.Parameters["@code"].Value = (long)code_auto;
			auxiliary_set_selldate.Parameters["@selldate"].Value = (DateTime)sell_date;
			return DbSql.ExecuteCommandError(auxiliary_set_selldate);
		}

		public static object MakeElementDirection(SqlDataReader reader)
		{
			string element = "";
			element = DbSql.GetValueString(reader, "�����������");
			return (object)element;
		}

		#region ��� ������ ���������� �����������
		public static object MakeElementReceive(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("���������_����������", DbSql.GetValueString(reader, "���������_����������"));
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("�����_�����", DbSql.GetValueString(reader, "�����_�����"));
			element.SetData("���_������", DbSql.GetValueInt(reader, "���_������"));

			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("�������������", DbSql.GetValueString(reader, "�������������"));
			element.SetData("����������_����", DbSql.GetValueString(reader, "����������_����"));
			element.SetData("����������_����������", DbSql.GetValueString(reader, "����������_����������"));
			return (object)element;
		}
		public static ListViewItem MakeLVItemReceive(SqlDataReader reader)
		{
			DtAuto element = (DtAuto)MakeElementReceive(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemReceive(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static void SelectInListReceive(ListView list, long code_document)
		{
			// ���������� ������� ������ �� �����
			select_receive_document.Parameters["@code_document"].Value = (long)code_document;
			DbSql.FillListNumerator(list, select_receive_document, new DbSql.DelegateMakeLVItem(MakeLVItemReceive));
		}
		public static void SelectInArrayReceive(ArrayList array, long code_document)
		{
			// ���������� ������� ������ �� �����
			select_receive_document.Parameters["@code_document"].Value = (long)code_document;
			DbSql.FillArray(array, select_receive_document, new DbSql.DelegateMakeElement(MakeElementReceive));
		}
		#endregion

		#region ��� �������������� ������
		public static object MakeElementStorageV1(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("����������_����������", DbSql.GetValueString(reader, "����������_����������"));
			element.SetData("����������_����", DbSql.GetValueString(reader, "����������_����"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("�����_�����", DbSql.GetValueString(reader, "�����_�����"));
			element.SetData("���_������", DbSql.GetValueInt(reader, "���_������"));
			element.SetData("���������_����", DbSql.GetValueDate(reader, "���������_����"));
			if(DbSql.IsValueNULL(reader, "���������_����") == false)
				element.SetData("����_���������_����", true);
			element.SetData("���������_����������", DbSql.GetValueString(reader, "���������_����������"));
			element.SetData("�������_����", DbSql.GetValueDate(reader, "�������_����"));
			if(DbSql.IsValueNULL(reader, "�������_����") == false)
				element.SetData("����_�������_����", true);
			element.SetData("�������_����������", DbSql.GetValueString(reader, "�������_����������"));
			element.SetData("�������_����������", DbSql.GetValueString(reader, "�������_����������"));
			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("����_�����", DbSql.GetValueFloat(reader, "����_�����"));
            element.SetData("���", DbSql.GetValueBool(reader, "���"));
            element.SetData("�������_����", DbSql.GetValueFloat(reader, "�������_����"));
			
			return (object)element;
		}
		public static ListViewItem MakeLVItemStorageV1(SqlDataReader reader)
		{
			DtAuto element = (DtAuto)MakeElementStorageV1(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemStorageV1(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}
		public static ListViewItem MakeLVItemStorageV2(SqlDataReader reader)
		{
			DtAuto element = (DtAuto)MakeElementStorageV1(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemStorageV2(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static void SelectInListStorageV1(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select_storage_v1, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}

		public static void SelectInListStorageAvaliable(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select_storage_avaliable, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV2));
		}

		public static void SelectInListStorageV1_Vin(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_storage_v1_vin.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_storage_v1_vin, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInListStorageV1_ReceiveComment(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_storage_v1_receivecomment.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_storage_v1_receivecomment, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInListStorageV1_Noppp(ListView list)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillList(list, select_storage_v1_noppp, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInArrayStorageV1(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select_storage_v1, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliable(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select_storage_avaliable, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliableMask(ArrayList array, string model_mask, string variant_mask, string color_mask, string vin_mask, int year_mask)
		{
			// ���������� ������� ������ �� �����
			select_storage_avaliable_mask.Parameters["@model_mask"].Value = (string)model_mask;
			select_storage_avaliable_mask.Parameters["@variant_mask"].Value = (string)variant_mask;
			select_storage_avaliable_mask.Parameters["@color_mask"].Value = (string)color_mask;
			select_storage_avaliable_mask.Parameters["@vin_mask"].Value = (string)vin_mask;
			select_storage_avaliable_mask.Parameters["@year_mask"].Value = (int)year_mask;
			DbSql.FillArray(array, select_storage_avaliable_mask, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliableMaskOptions(ArrayList array, string model_mask, string variant_mask, string color_mask, string vin_mask, int year_mask, long option1, long option2, long option3, long option4, long option5)
		{
			// ���������� ������� ������ �� �����
			select_storage_avaliable_mask_options.Parameters["@model_mask"].Value = (string)model_mask;
			select_storage_avaliable_mask_options.Parameters["@variant_mask"].Value = (string)variant_mask;
			select_storage_avaliable_mask_options.Parameters["@color_mask"].Value = (string)color_mask;
			select_storage_avaliable_mask_options.Parameters["@vin_mask"].Value = (string)vin_mask;
			select_storage_avaliable_mask_options.Parameters["@year_mask"].Value = (int)year_mask;
			select_storage_avaliable_mask_options.Parameters["@option1"].Value = (long)option1;
			select_storage_avaliable_mask_options.Parameters["@option2"].Value = (long)option2;
			select_storage_avaliable_mask_options.Parameters["@option3"].Value = (long)option3;
			select_storage_avaliable_mask_options.Parameters["@option4"].Value = (long)option4;
			select_storage_avaliable_mask_options.Parameters["@option5"].Value = (long)option5;
			DbSql.FillArray(array, select_storage_avaliable_mask_options, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static DtAuto SelectStorageAvaliableFind(long code)
		{
			// ���������� ������� ������ �� �����
			select_storage_avaliable_find.Parameters["@code_auto"].Value = (long)code;
			return (DtAuto)DbSql.Find(select_storage_avaliable_find, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static float SelectAutoAdd(long code_auto)
		{
			// ���������� ������� ������ �� �����
			select_auto_add.Parameters["@code_auto"].Value		= code_auto;
			if(DbSql.ExecuteCommandError(select_auto_add) == false) return -1.0F;
			if(select_auto_add.Parameters["@summ"].Value == null) return 0.0F;
			return (float)select_auto_add.Parameters["@summ"].Value;
		}
		#endregion

		#region ������ � ���������
		public static bool ReserveInsert(long code_auto, DateTime end_date, string comment)
		{
			// ���������� ������� ������ �� �����
			reserve_insert.Parameters["@code_auto"].Value		= code_auto;
			reserve_insert.Parameters["@date_end"].Value	= end_date;
			reserve_insert.Parameters["@comment"].Value		= comment;
			
			if(DbSql.ExecuteCommandError(reserve_insert) == false) return false;
			return true;
		}
		public static bool ReserveRemove(long code_auto, string comment)
		{
			// ���������� ������� ������ �� �����
			reserve_remove.Parameters["@code_auto"].Value	= code_auto;
			reserve_remove.Parameters["@comment"].Value		= comment;
			if(DbSql.ExecuteCommandError(reserve_remove) == false) return false;
			return true;
		}
		#endregion

		#region ����� ����
		public static bool PriceInsert(long code_model, long code_variant, int year, float price)
		{
			// ���������� ������� ������ �� �����
			price_insert.Parameters["@code_model"].Value	= code_model;
			price_insert.Parameters["@code_variant"].Value	= code_variant;
			price_insert.Parameters["@year"].Value			= year;
			price_insert.Parameters["@price"].Value			= price;
			
			if(DbSql.ExecuteCommandError(price_insert) == false) return false;
			return true;
		}
		public static bool PriceUpdate(long code_model, long code_variant, int year, float price)
		{
			// ���������� ������� ������ �� �����
			price_update.Parameters["@code_model"].Value	= code_model;
			price_update.Parameters["@code_variant"].Value	= code_variant;
			price_update.Parameters["@year"].Value			= year;
			price_update.Parameters["@price"].Value			= price;
			
			if(DbSql.ExecuteCommandError(price_update) == false) return false;
			return true;
		}
		#endregion

	}
}
