using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoSell.
	/// </summary>
	public class DbSqlAutoSell
	{
		public static SqlCommand	insert;
		public static SqlCommand	select;
        public static SqlCommand    find;
        public static SqlCommand    find_auto;
		public static SqlCommand	select_param;
		public static SqlCommand	select_partner;
		public static SqlCommand	select_vinbody;

		public static SqlCommand	select_calltoclient;

		public static SqlCommand	set_selldate;

		public DbSqlAutoSell()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public struct SearchMask
		{
			public bool		timeon;
			public DateTime	date_start;
			public DateTime	date_stop;
			public long		code_model;
			public long		code_variant;
			public long		code_color;
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_�������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_param = new SqlCommand("����������_�������_�������_���������", connection);
			select_param.Parameters.Add("@timeon", SqlDbType.Bit);
			select_param.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_param.Parameters.Add("@date_stop", SqlDbType.DateTime);
			select_param.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_param.Parameters.Add("@code_variant", SqlDbType.BigInt);
			select_param.Parameters.Add("@code_color", SqlDbType.BigInt);
			select_param.CommandType = CommandType.StoredProcedure;

			select_vinbody = new SqlCommand("����������_�������_�������_VIN�����", connection);
			select_vinbody.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_vinbody.CommandType = CommandType.StoredProcedure;

			select_partner = new SqlCommand("����������_�������_�������_����������", connection);
			select_partner.Parameters.Add("@code_partner", SqlDbType.BigInt);
			select_partner.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_�������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@code_customer", SqlDbType.BigInt);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);


			select_calltoclient = new SqlCommand("����������_�������_�������_������", connection);
			select_calltoclient.Parameters.Add("@date_start", SqlDbType.DateTime);
			select_calltoclient.Parameters.Add("@date_stop", SqlDbType.DateTime);
			select_calltoclient.CommandType = CommandType.StoredProcedure;

			set_selldate = new SqlCommand("����������_�������_����_���������", connection);
			set_selldate.Parameters.Add("@code", SqlDbType.BigInt);
			set_selldate.Parameters.Add("@date", SqlDbType.DateTime);			
			set_selldate.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_selldate);

            find = new SqlCommand("����������_�������_�����", connection);
            find.Parameters.Add("@code", SqlDbType.BigInt);
            find.CommandType = CommandType.StoredProcedure;

            find_auto = new SqlCommand("����������_�������_�����_����������", connection);
            find_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);
            find_auto.CommandType = CommandType.StoredProcedure;
		}

		public static DtAutoSell Insert(DtAutoSell sell)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@code_auto"].Value		= (long)sell.GetData("������_���_����������");
			insert.Parameters["@code_customer"].Value	= (long)sell.GetData("������_���_����������");
			insert.Parameters["@date"].Value			= (DateTime)sell.GetData("����_����������_�������");
			insert.Parameters["@comment"].Value			= (string)sell.GetData("����������_����������_�������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			sell.SetData("���_����������_�������",(long)insert.Parameters["@code"].Value);
			return sell;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoSell element			= new DtAutoSell();
			element.SetData("���_����������_�������", DbSql.GetValueLong(reader, "���_����������_�������"));
			element.SetData("������_���_����������", DbSql.GetValueLong(reader, "������_���_����������"));
			element.SetData("������_���_����������", DbSql.GetValueLong(reader, "������_���_����������"));
			element.SetData("����_����������_�������", DbSql.GetValueDate(reader, "����_����������_�������"));
			element.SetData("����������_����������_�������", DbSql.GetValueString(reader, "����������_����������_�������"));

			element.SetData("����������_������������", DbSql.GetValueString(reader, "����������_������������"));
			element.SetData("����������_������", DbSql.GetValueString(reader, "����������_������"));
			element.SetData("����������_����", DbSql.GetValueString(reader, "����������_����"));
			element.SetData("����������_����������", DbSql.GetValueString(reader, "����������_����������"));
			element.SetData("����������_VIN", DbSql.GetValueString(reader, "����������_VIN"));
			return (object)element;
		}

		public static object MakeElementCallToClient(SqlDataReader reader)
		{
			DtCallToClient element			= new DtCallToClient();
			element.code_sell	= DbSql.GetValueLong(reader, "���_����������_�������");
			element.date		= DbSql.GetValueDate(reader, "����_����������_�������");
			
			element.tmp_customer_name	= DbSql.GetValueString(reader, "����������_������������");
			element.tmp_auto_model		= DbSql.GetValueString(reader, "����������_������");
			return (object)element;
		}

		public static ListViewItem MakeLVItemCallToClient(SqlDataReader reader)
		{
			DtCallToClient element = (DtCallToClient)MakeElementCallToClient(reader);
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

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtAutoSell element = (DtAutoSell)MakeElement(reader);
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

		public static void SelectInList(ListView list, SearchMask mask)
		{
			// ���������� ������� ������ �� �����
			select_param.Parameters["@timeon"].Value		= (bool)mask.timeon;
			select_param.Parameters["@date_start"].Value	= (DateTime)mask.date_start;
			select_param.Parameters["@date_stop"].Value		= (DateTime)mask.date_stop;
			select_param.Parameters["@code_model"].Value	= (long)mask.code_model;
			select_param.Parameters["@code_variant"].Value	= (long)mask.code_variant;
			select_param.Parameters["@code_color"].Value	= (long)mask.code_color;

			DbSql.FillList(list, select_param, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array, SearchMask mask)
		{
			// ���������� ������� ������ �� �����
			select_param.Parameters["@timeon"].Value		= (bool)mask.timeon;
			select_param.Parameters["@date_start"].Value	= (DateTime)mask.date_start;
			select_param.Parameters["@date_stop"].Value		= (DateTime)mask.date_stop;
			select_param.Parameters["@code_model"].Value	= (long)mask.code_model;
			select_param.Parameters["@code_variant"].Value	= (long)mask.code_variant;
			select_param.Parameters["@code_color"].Value	= (long)mask.code_color;

			DbSql.FillArray(array, select_param, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInListVinBody(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_vinbody.Parameters["@pattern"].Value		= (string)pattern;	
			DbSql.FillList(list, select_vinbody, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListPartner(ListView list, long code_partner)
		{
			// ���������� ������� ������ �� �����
			select_partner.Parameters["@code_partner"].Value		= (long)code_partner;	
			DbSql.FillList(list, select_partner, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListCallToClient(ListView list, DateTime start, DateTime stop)
		{
			// ���������� ������� ������ �� �����
			
			select_calltoclient.Parameters["@date_start"].Value		= (DateTime)start;
			select_calltoclient.Parameters["@date_stop"].Value		= (DateTime)stop;
			
			DbSql.FillList(list, select_calltoclient, new DbSql.DelegateMakeLVItem(MakeLVItemCallToClient));
		}

		public static bool SetSellDate(long code_sell, DateTime date)
		{
			// ���������� ������� ������ �� �����
			set_selldate.Parameters["@code"].Value		= (long)code_sell;
			set_selldate.Parameters["@date"].Value		= (DateTime)date;
			if(DbSql.ExecuteCommandError(set_selldate) == false) return false;
			return true;
		}

        public static DtAutoSell FindSell(long code)
        {
            // ���������� ������� ������ �� �����
            find.Parameters["@code"].Value = (long)code;
            return (DtAutoSell)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
        }

        public static DtAutoSell FindSellByAuto(long code_auto)
        {
            // ���������� ������� ������ �� �����
            find_auto.Parameters["@code_auto"].Value = (long)code_auto;
            return (DtAutoSell)DbSql.Find(find_auto, new DbSql.DelegateMakeElement(MakeElement));
        }
	}
}
