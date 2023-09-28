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
		public static SqlCommand		select_auto_add;			// Ñóììà äîïîëíèòåëüíîãî îáîğóäîâàíèÿ
		public static SqlCommand		select_directions;			// Ñïèñîê ïğåäïèñàíèé ïî âèíó àâòîìîáèëÿ
        public static SqlCommand        select_directions_vin;		// Ñïèñîê ïğåäïèñàíèé ïî âèíó àâòîìîáèëÿ - ÷èñòî ïî VIN

		// Ğàáîòà ñ ğåçåğâàìè
		public static SqlCommand		reserve_insert;
		public static SqlCommand		reserve_remove;

		// Ğàáîòà ñ ïğàéñ-ëèñòîì
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
			find = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÎÈÑÊ", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_vin = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÎÈÑÊ_VIN", connection);
			find_vin.Parameters.Add("@vin", SqlDbType.VarChar);
			find_vin.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_storage_v1 = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_Â1", connection);
			select_storage_v1.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_ÍÀËÈ×ÈÅ", connection);
			select_storage_avaliable.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable_mask = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_ÍÀËÈ×ÈÅ_ÌÀÑÊÀ", connection);
			select_storage_avaliable_mask.Parameters.Add("@model_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@variant_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@color_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			select_storage_avaliable_mask.Parameters.Add("@year_mask", SqlDbType.Int);
			select_storage_avaliable_mask.CommandType = CommandType.StoredProcedure;

			select_storage_avaliable_mask_options = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_ÍÀËÈ×ÈÅ_ÌÀÑÊÀ_ÊÎÌÏËÅÊÒÀÖÈß", connection);
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

			select_storage_avaliable_find = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_ÍÀËÈ×ÈÅ_ÏÎÈÑÊ", connection);
			select_storage_avaliable_find.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_storage_avaliable_find.CommandType = CommandType.StoredProcedure;

			select_storage_v1_vin = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_Â1_VIN", connection);
			select_storage_v1_vin.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_storage_v1_vin.CommandType = CommandType.StoredProcedure;

			select_storage_v1_receivecomment = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_Â1_ÏÎËÓ×ÅÍÈÅ_ÏĞÈÌÅ×ÀÍÈÅ", connection);
			select_storage_v1_receivecomment.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_storage_v1_receivecomment.CommandType = CommandType.StoredProcedure;

			select_storage_v1_noppp = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÊËÀÄ_Â1_ÍÅÒ_ÏÏÏ", connection);
			select_storage_v1_noppp.CommandType = CommandType.StoredProcedure;
			select_storage_v1_noppp.CommandTimeout = 600;

			select_double = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÄÓÁËÜ", connection);
			select_double.CommandType = CommandType.StoredProcedure;
			select_double.CommandTimeout = 300;

			select_vin = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_VIN", connection);
			select_vin.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_vin.CommandType = CommandType.StoredProcedure;

			select_body = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÊÓÇÎÂ", connection);
			select_body.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_body.CommandType = CommandType.StoredProcedure;

			select_model = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÌÎÄÅËÜ", connection);
			select_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_model.CommandType = CommandType.StoredProcedure;

			select_directions = new SqlCommand("ÏĞÅÄÏÈÑÀÍÈÅ_ØÀÑÑÈ_ÂÛÁÎĞÊÀ", connection);
			select_directions.Parameters.Add("@vin", SqlDbType.VarChar);
			select_directions.CommandType = CommandType.StoredProcedure;

            select_directions_vin = new SqlCommand("ÏĞÅÄÏÈÑÀÍÈÅ_VIN_ÂÛÁÎĞÊÀ", connection);
            select_directions_vin.Parameters.Add("@vin", SqlDbType.VarChar);
            select_directions_vin.CommandType = CommandType.StoredProcedure;

			delete = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÄÎÁÀÂËÅÍÈÅ", connection);
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

			update = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÇÌÅÍÅÍÈÅ", connection);
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

			update_licence_plate = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÇÌÅÍÅÍÈÅ_ÍÎÌÅĞ_ÇÍÀÊ", connection);
			update_licence_plate.Parameters.Add("@code", SqlDbType.BigInt);
			update_licence_plate.Parameters.Add("@licence_plate_number", SqlDbType.VarChar);
			update_licence_plate.Parameters.Add("@licence_plate_region", SqlDbType.VarChar);
			update_licence_plate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_licence_plate);

			update_storage_avaliable = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÎÒÏÈÑÀÒÜ_ÑÎ_ÑÊËÀÄÀ", connection);
			update_storage_avaliable.Parameters.Add("@code", SqlDbType.BigInt);
			update_storage_avaliable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_storage_avaliable);

			select_receive_document = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÎËÓ×ÅÍÈÅ_ÂÛÁÎĞÊÀ_ÄÎÊÓÌÅÍÒ", connection);
			select_receive_document.Parameters.Add("@code_document", SqlDbType.BigInt);
			select_receive_document.CommandType = CommandType.StoredProcedure;

			auxiliary_auto_replace = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÇÀÌÅÍÀ_ÑËÓÆÅÁÍÎÅ", connection);
			auxiliary_auto_replace.Parameters.Add("@code_old", SqlDbType.BigInt);
			auxiliary_auto_replace.Parameters.Add("@code_new", SqlDbType.BigInt);
			auxiliary_auto_replace.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_auto_replace);

			auxiliary_set_selldate = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÇÌÅÍÅÍÈÅ_ÄÀÒÀ_ÏĞÎÄÀÆÀ", connection);
			auxiliary_set_selldate.Parameters.Add("@code", SqlDbType.BigInt);
			auxiliary_set_selldate.Parameters.Add("@selldate", SqlDbType.DateTime);
			auxiliary_set_selldate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(auxiliary_set_selldate);

			// Ğàáîòà ñ ğåçåğâàìè
			reserve_insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ĞÅÇÅĞÂÈĞÎÂÀÍÈÅ", connection);
			reserve_insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			reserve_insert.Parameters.Add("@date_end", SqlDbType.DateTime);
			reserve_insert.Parameters.Add("@comment", SqlDbType.VarChar);
			reserve_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reserve_insert);

			reserve_remove = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ĞÅÇÅĞÂÈĞÎÂÀÍÈÅ_ÎÒÌÅÍÀ", connection);
			reserve_remove.Parameters.Add("@code_auto", SqlDbType.BigInt);
			reserve_remove.Parameters.Add("@comment", SqlDbType.VarChar);
			reserve_remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(reserve_remove);

			select_auto_add = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÂÛÁÎĞÊÀ_ÑÓÌÌÀ_ÄÎÏÎÂ", connection);
			select_auto_add.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_auto_add.Parameters.Add("@summ", SqlDbType.Real);
			select_auto_add.CommandType = CommandType.StoredProcedure;
			select_auto_add.Parameters["@summ"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(select_auto_add);

			// Ğàáîòà ñ ïğàéñëèñòîì
			price_insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏĞÀÉÑ_ÄÎÁÀÂËÅÍÈÅ", connection);
			price_insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			price_insert.Parameters.Add("@code_variant", SqlDbType.BigInt);
			price_insert.Parameters.Add("@year", SqlDbType.Int);
			price_insert.Parameters.Add("@price", SqlDbType.Float);
			price_insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(price_insert);

			price_update = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏĞÀÉÑ_ÈÇÌÅÍÅÍÈÅ", connection);
			price_update.Parameters.Add("@code_model", SqlDbType.BigInt);
			price_update.Parameters.Add("@code_variant", SqlDbType.BigInt);
			price_update.Parameters.Add("@year", SqlDbType.Int);
			price_update.Parameters.Add("@price", SqlDbType.Float);
			price_update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(price_update);

            color_price_update = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÈÇÌÅÍÅÍÈÅ_ÄÎÏËÀÒÀ_ÖÂÅÒ", connection);
            color_price_update.Parameters.Add("@code", SqlDbType.BigInt);
            color_price_update.Parameters.Add("@price", SqlDbType.Float);
            color_price_update.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(color_price_update);

            update_pts = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÓÑÒÀÍÎÂÊÀ_ÏÒÑ", connection);
            update_pts.Parameters.Add("@code", SqlDbType.BigInt);
            update_pts.Parameters.Add("@pts", SqlDbType.Bit);
            update_pts.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_pts);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("ÍÎÌÅĞ_ÊÓÇÎÂ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÊÓÇÎÂ"));
			element.SetData("ÏĞÈÌÅ×ÀÍÅ", DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ"));

			element.SetData("ÌÎÄÅËÜ", DbSql.GetValueString(reader, "ÌÎÄÅËÜ"));
			element.SetData("ÏĞÎÈÇÂÎÄÈÒÅËÜ", DbSql.GetValueString(reader, "ÏĞÎÈÇÂÎÄÈÒÅËÜ"));
			return (object)element;
		}

		public static object MakeElementFind(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("VIN_ÎÒÑÓÒÑÒÂÓÅÒ", DbSql.GetValueBool(reader, "VIN_ÎÒÑÓÒÑÒÂÓÅÒ"));
			element.SetData("VIN_ÏĞÎÈÇÂÎÄÈÒÅËÜ", DbSql.GetValueString(reader, "VIN_ÏĞÎÈÇÂÎÄÈÒÅËÜ"));
			element.SetData("ÍÎÌÅĞ_ÊÓÇÎÂ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÊÓÇÎÂ"));
			element.SetData("ÍÎÌÅĞ_ØÀÑÑÈ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ØÀÑÑÈ"));
			element.SetData("ÍÎÌÅĞ_ÄÂÈÃÀÒÅËÜ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÄÂÈÃÀÒÅËÜ"));
			element.SetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÎÒÑÓÒÑÒÂÓÅÒ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueBool(reader, "ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÎÒÑÓÒÑÒÂÓÅÒ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÃÎÄ_ÂÛÏÓÑÊ", DbSql.GetValueInt(reader, "ÃÎÄ_ÂÛÏÓÑÊ"));
            element.SetData("ÊÎÄ_ÏĞÎÈÇÂÎÄÈÒÅËÜ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÏĞÎÈÇÂÎÄÈÒÅËÜ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ"));
			element.SetData("ÍÎÌÅĞ_ÇÍÀÊ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÇÍÀÊ"));
			element.SetData("ÏĞÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ"));
			element.SetData("ÍÎÌÅĞ_ÇÍÀÊ_ÍÎÌÅĞ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÇÍÀÊ_ÍÎÌÅĞ"));
			element.SetData("ÍÎÌÅĞ_ÇÍÀÊ_ĞÅÃÈÎÍ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÇÍÀÊ_ĞÅÃÈÎÍ"));

			element.SetData("ÌÎÄÅËÜ", DbSql.GetValueString(reader, "ÌÎÄÅËÜ"));
			element.SetData("ÏĞÎÈÇÂÎÄÈÒÅËÜ", DbSql.GetValueString(reader, "ÏĞÎÈÇÂÎÄÈÒÅËÜ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ"));

			// Ñòàğàÿ âåğñèÿ äàòû ïğîäàæè
			element.SetData("ÏĞÎÄÀÆÀ_ÄÀÒÀ", DbSql.GetValueDate(reader, "ÏĞÎÄÀÆÀ_ÄÀÒÀ"));
			if(DbSql.IsValueNULL(reader, "ÏĞÎÄÀÆÀ_ÄÀÒÀ") == false)
				element.SetData("ÅÑÒÜ_ÏĞÎÄÀÆÀ_ÄÀÒÀ", true);
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
				item.Text			= "Îøèáêà";
			}
			return item;
		}

		public static void SelectInList(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListDouble(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select_double, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListVin(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_vin.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_vin, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArrayDirection(ArrayList array, string vin)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_directions.Parameters["@vin"].Value = (string)vin;
			DbSql.FillArray(array, select_directions, new DbSql.DelegateMakeElement(MakeElementDirection));
		}

        public static void SelectInArrayDirectionVIN(ArrayList array, string vin)
        {
            // Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
            select_directions_vin.Parameters["@vin"].Value = (string)vin;
            DbSql.FillArray(array, select_directions_vin, new DbSql.DelegateMakeElement(MakeElementDirection));
        }

		public static void SelectInListBody(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_body.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_body, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListModel(ListView list, long code_model)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_model.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select_model, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static bool Delete(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			delete.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete);	
		}

		public static DtAuto Find(long code)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find.Parameters["@code"].Value = (long)code;
			return (DtAuto)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtAuto Find(string vin)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			find_vin.Parameters["@vin"].Value = (string)vin;
			return (DtAuto)DbSql.Find(find_vin, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static DtAuto Insert(DtAuto auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			insert.Parameters["@code_model"].Value		= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			insert.Parameters["@id_vin"].Value			= (string)auto.GetData("VIN");
			insert.Parameters["@id_vin_origin"].Value	= (string)auto.GetData("VIN_ÏĞÎÈÇÂÎÄÈÒÅËÜ");
			insert.Parameters["@is_id_vin"].Value		= (bool)auto.GetData("VIN_ÎÒÑÓÒÑÒÂÓÅÒ");
			insert.Parameters["@id_body"].Value			= (string)auto.GetData("ÍÎÌÅĞ_ÊÓÇÎÂ");
			insert.Parameters["@id_frame"].Value		= (string)auto.GetData("ÍÎÌÅĞ_ØÀÑÑÈ");
			insert.Parameters["@id_engine"].Value		= (string)auto.GetData("ÍÎÌÅĞ_ÄÂÈÃÀÒÅËÜ");
			insert.Parameters["@id_parts"].Value		= (long)auto.GetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÀÂÒÎÌÎÁÈËÜ");
			insert.Parameters["@is_id_parts"].Value		= (bool)auto.GetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÎÒÑÓÒÑÒÂÓÅÒ_ÀÂÒÎÌÎÁÈËÜ");
			insert.Parameters["@year"].Value			= (int)auto.GetData("ÃÎÄ_ÂÛÏÓÑÊ");
			insert.Parameters["@code_producer"].Value	= (long)auto.GetData("ÊÎÄ_ÏĞÎÈÇÂÎÄÈÒÅËÜ_ÀÂÒÎÌÎÁÈËÜ");
			insert.Parameters["@code_color"].Value		= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ");
			insert.Parameters["@code_variant"].Value	= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ");
			insert.Parameters["@id_sign"].Value			= (string)auto.GetData("ÍÎÌÅĞ_ÇÍÀÊ");
			insert.Parameters["@comment"].Value			= (string)auto.GetData("ÏĞÈÌÅ×ÀÍÈÅ");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			auto.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ",(long)insert.Parameters["@code"].Value);
			return auto;
		}

		public static bool Update(DtAuto auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update.Parameters["@code"].Value			= (long)auto.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ");
			update.Parameters["@code_model"].Value		= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ");
			update.Parameters["@id_vin"].Value			= (string)auto.GetData("VIN");
			update.Parameters["@id_vin_origin"].Value	= (string)auto.GetData("VIN_ÏĞÎÈÇÂÎÄÈÒÅËÜ");
			update.Parameters["@is_id_vin"].Value		= (bool)auto.GetData("VIN_ÎÒÑÓÒÑÒÂÓÅÒ");
			update.Parameters["@id_body"].Value			= (string)auto.GetData("ÍÎÌÅĞ_ÊÓÇÎÂ");
			update.Parameters["@id_frame"].Value		= (string)auto.GetData("ÍÎÌÅĞ_ØÀÑÑÈ");
			update.Parameters["@id_engine"].Value		= (string)auto.GetData("ÍÎÌÅĞ_ÄÂÈÃÀÒÅËÜ");
			update.Parameters["@id_parts"].Value		= (long)auto.GetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÀÂÒÎÌÎÁÈËÜ");
			update.Parameters["@is_id_parts"].Value		= (bool)auto.GetData("ÍÎÌÅĞ_ÇÀÏ×ÀÑÒÅÉ_ÎÒÑÓÒÑÒÂÓÅÒ_ÀÂÒÎÌÎÁÈËÜ");
			update.Parameters["@year"].Value			= (int)auto.GetData("ÃÎÄ_ÂÛÏÓÑÊ");
			update.Parameters["@code_producer"].Value	= (long)auto.GetData("ÊÎÄ_ÏĞÎÈÇÂÎÄÈÒÅËÜ_ÀÂÒÎÌÎÁÈËÜ");
			update.Parameters["@code_color"].Value		= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ");
			update.Parameters["@code_variant"].Value	= (long)auto.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ");
			update.Parameters["@id_sign"].Value			= (string)auto.GetData("ÍÎÌÅĞ_ÇÍÀÊ");
			update.Parameters["@comment"].Value			= (string)auto.GetData("ÏĞÈÌÅ×ÀÍÈÅ");
			return DbSql.ExecuteCommandError(update);
		}

		public static bool UpdateLicencePlate(long code_auto, string licence_plate_number, string licence_plate_region)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update_licence_plate.Parameters["@code"].Value					= (long)code_auto;
			update_licence_plate.Parameters["@licence_plate_number"].Value	= (string)licence_plate_number;
			update_licence_plate.Parameters["@licence_plate_region"].Value	= (string)licence_plate_region;
			return DbSql.ExecuteCommandError(update_licence_plate);
		}

        public static bool UpdateColorPrice(long code_auto, float price)
        {
            // Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
            color_price_update.Parameters["@code"].Value = (long)code_auto;
            color_price_update.Parameters["@price"].Value = (float)price;
            return DbSql.ExecuteCommandError(color_price_update);
        }

        public static bool UpdatePts(long code_auto, bool pts)
        {
            // Óñòàíîâêà íàëè÷èÿ ÏÒÑ äà/íåò
            update_pts.Parameters["@code"].Value = (long)code_auto;
            update_pts.Parameters["@pts"].Value = (bool)pts;
            return DbSql.ExecuteCommandError(update_pts);
        }

		public static bool UpdateStorageAvaliable(long code_auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			update_storage_avaliable.Parameters["@code"].Value				= (long)code_auto;
			return DbSql.ExecuteCommandError(update_storage_avaliable);
		}

		public static bool AuxiliaryAutoReplace(long code_old, long code_new)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			auxiliary_auto_replace.Parameters["@code_old"].Value = (long)code_old;
			auxiliary_auto_replace.Parameters["@code_new"].Value = (long)code_new;
			return DbSql.ExecuteCommandError(auxiliary_auto_replace);
		}

		public static bool AuxiliarySetSellDate(long code_auto, DateTime sell_date)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			auxiliary_set_selldate.Parameters["@code"].Value = (long)code_auto;
			auxiliary_set_selldate.Parameters["@selldate"].Value = (DateTime)sell_date;
			return DbSql.ExecuteCommandError(auxiliary_set_selldate);
		}

		public static object MakeElementDirection(SqlDataReader reader)
		{
			string element = "";
			element = DbSql.GetValueString(reader, "ÏĞÅÄÏÈÑÀÍÈÅ");
			return (object)element;
		}

		#region Äëÿ ñïèñêà ïîëó÷åííûõ àâòîìîáèëåé
		public static object MakeElementReceive(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("ÏÎËÓ×ÅÍÈÅ_ÏĞÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏÎËÓ×ÅÍÈÅ_ÏĞÈÌÅ×ÀÍÈÅ"));
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("ÍÎÌÅĞ_ÊÓÇÎÂ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÊÓÇÎÂ"));
			element.SetData("ÃÎÄ_ÂÛÏÓÑÊ", DbSql.GetValueInt(reader, "ÃÎÄ_ÂÛÏÓÑÊ"));

			element.SetData("ÌÎÄÅËÜ", DbSql.GetValueString(reader, "ÌÎÄÅËÜ"));
			element.SetData("ÏĞÎÈÇÂÎÄÈÒÅËÜ", DbSql.GetValueString(reader, "ÏĞÎÈÇÂÎÄÈÒÅËÜ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ"));
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
				item.Text			= "Îøèáêà";
			}
			return item;
		}

		public static void SelectInListReceive(ListView list, long code_document)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_receive_document.Parameters["@code_document"].Value = (long)code_document;
			DbSql.FillListNumerator(list, select_receive_document, new DbSql.DelegateMakeLVItem(MakeLVItemReceive));
		}
		public static void SelectInArrayReceive(ArrayList array, long code_document)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_receive_document.Parameters["@code_document"].Value = (long)code_document;
			DbSql.FillArray(array, select_receive_document, new DbSql.DelegateMakeElement(MakeElementReceive));
		}
		#endregion

		#region Äëÿ àâòîìîáèëüíîãî ñêëàäà
		public static object MakeElementStorageV1(SqlDataReader reader)
		{
			DtAuto element			= new DtAuto();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÌÎÄÅËÜ", DbSql.GetValueString(reader, "ÌÎÄÅËÜ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ"));
			element.SetData("ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ", DbSql.GetValueString(reader, "ÀÂÒÎÌÎÁÈËÜ_ÖÂÅÒ"));
			element.SetData("VIN", DbSql.GetValueString(reader, "VIN"));
			element.SetData("ÍÎÌÅĞ_ÊÓÇÎÂ", DbSql.GetValueString(reader, "ÍÎÌÅĞ_ÊÓÇÎÂ"));
			element.SetData("ÃÎÄ_ÂÛÏÓÑÊ", DbSql.GetValueInt(reader, "ÃÎÄ_ÂÛÏÓÑÊ"));
			element.SetData("ÏÎËÓ×ÅÍÈÅ_ÄÀÒÀ", DbSql.GetValueDate(reader, "ÏÎËÓ×ÅÍÈÅ_ÄÀÒÀ"));
			if(DbSql.IsValueNULL(reader, "ÏÎËÓ×ÅÍÈÅ_ÄÀÒÀ") == false)
				element.SetData("ÅÑÒÜ_ÏÎËÓ×ÅÍÈÅ_ÄÀÒÀ", true);
			element.SetData("ÏÎËÓ×ÅÍÈÅ_ÏĞÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏÎËÓ×ÅÍÈÅ_ÏĞÈÌÅ×ÀÍÈÅ"));
			element.SetData("ÏĞÎÄÀÆÀ_ÄÀÒÀ", DbSql.GetValueDate(reader, "ÏĞÎÄÀÆÀ_ÄÀÒÀ"));
			if(DbSql.IsValueNULL(reader, "ÏĞÎÄÀÆÀ_ÄÀÒÀ") == false)
				element.SetData("ÅÑÒÜ_ÏĞÎÄÀÆÀ_ÄÀÒÀ", true);
			element.SetData("ÏĞÎÄÀÆÀ_ÏÎÊÓÏÀÒÅËÜ", DbSql.GetValueString(reader, "ÏĞÎÄÀÆÀ_ÏÎÊÓÏÀÒÅËÜ"));
			element.SetData("ÏĞÎÄÀÆÀ_ÏĞÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏĞÎÄÀÆÀ_ÏĞÈÌÅ×ÀÍÈÅ"));
			element.SetData("ĞÅÇÅĞÂ", DbSql.GetValueString(reader, "ĞÅÇÅĞÂ"));
			element.SetData("ÖÅÍÀ_ÏĞÀÉÑ", DbSql.GetValueFloat(reader, "ÖÅÍÀ_ÏĞÀÉÑ"));
            element.SetData("ÏÒÑ", DbSql.GetValueBool(reader, "ÏÒÑ"));
            element.SetData("ÄÎÏËÀÒÀ_ÖÂÅÒ", DbSql.GetValueFloat(reader, "ÄÎÏËÀÒÀ_ÖÂÅÒ"));
			
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
				item.Text			= "Îøèáêà";
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
				item.Text			= "Îøèáêà";
			}
			return item;
		}

		public static void SelectInListStorageV1(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select_storage_v1, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}

		public static void SelectInListStorageAvaliable(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select_storage_avaliable, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV2));
		}

		public static void SelectInListStorageV1_Vin(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_storage_v1_vin.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_storage_v1_vin, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInListStorageV1_ReceiveComment(ListView list, string pattern)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_storage_v1_receivecomment.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_storage_v1_receivecomment, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInListStorageV1_Noppp(ListView list)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillList(list, select_storage_v1_noppp, new DbSql.DelegateMakeLVItem(MakeLVItemStorageV1));
		}
		public static void SelectInArrayStorageV1(ArrayList array)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillArray(array, select_storage_v1, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliable(ArrayList array)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			DbSql.FillArray(array, select_storage_avaliable, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliableMask(ArrayList array, string model_mask, string variant_mask, string color_mask, string vin_mask, int year_mask)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_storage_avaliable_mask.Parameters["@model_mask"].Value = (string)model_mask;
			select_storage_avaliable_mask.Parameters["@variant_mask"].Value = (string)variant_mask;
			select_storage_avaliable_mask.Parameters["@color_mask"].Value = (string)color_mask;
			select_storage_avaliable_mask.Parameters["@vin_mask"].Value = (string)vin_mask;
			select_storage_avaliable_mask.Parameters["@year_mask"].Value = (int)year_mask;
			DbSql.FillArray(array, select_storage_avaliable_mask, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static void SelectInArrayStorageAvaliableMaskOptions(ArrayList array, string model_mask, string variant_mask, string color_mask, string vin_mask, int year_mask, long option1, long option2, long option3, long option4, long option5)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
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
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_storage_avaliable_find.Parameters["@code_auto"].Value = (long)code;
			return (DtAuto)DbSql.Find(select_storage_avaliable_find, new DbSql.DelegateMakeElement(MakeElementStorageV1));
		}

		public static float SelectAutoAdd(long code_auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_auto_add.Parameters["@code_auto"].Value		= code_auto;
			if(DbSql.ExecuteCommandError(select_auto_add) == false) return -1.0F;
			if(select_auto_add.Parameters["@summ"].Value == null) return 0.0F;
			return (float)select_auto_add.Parameters["@summ"].Value;
		}
		#endregion

		#region Ğàáîòà ñ ğåçåğâàìè
		public static bool ReserveInsert(long code_auto, DateTime end_date, string comment)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			reserve_insert.Parameters["@code_auto"].Value		= code_auto;
			reserve_insert.Parameters["@date_end"].Value	= end_date;
			reserve_insert.Parameters["@comment"].Value		= comment;
			
			if(DbSql.ExecuteCommandError(reserve_insert) == false) return false;
			return true;
		}
		public static bool ReserveRemove(long code_auto, string comment)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			reserve_remove.Parameters["@code_auto"].Value	= code_auto;
			reserve_remove.Parameters["@comment"].Value		= comment;
			if(DbSql.ExecuteCommandError(reserve_remove) == false) return false;
			return true;
		}
		#endregion

		#region Ïğàéñ ëèñò
		public static bool PriceInsert(long code_model, long code_variant, int year, float price)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			price_insert.Parameters["@code_model"].Value	= code_model;
			price_insert.Parameters["@code_variant"].Value	= code_variant;
			price_insert.Parameters["@year"].Value			= year;
			price_insert.Parameters["@price"].Value			= price;
			
			if(DbSql.ExecuteCommandError(price_insert) == false) return false;
			return true;
		}
		public static bool PriceUpdate(long code_model, long code_variant, int year, float price)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
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
