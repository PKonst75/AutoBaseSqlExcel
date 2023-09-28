using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlLicenceVehicle.
	/// </summary>
	public class DbSqlLicenceVehicle
	{
		public static SqlCommand		insert;
		public static SqlCommand		select;
		public static SqlCommand		select_licence_number;
		public static SqlCommand		select_vehicle_number;
		public static SqlCommand		find;

		public DbSqlLicenceVehicle()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("�������������_��_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@version", SqlDbType.BigInt);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@code_owner", SqlDbType.BigInt);
			insert.Parameters.Add("@licence_series", SqlDbType.VarChar);
			insert.Parameters.Add("@licence_number", SqlDbType.VarChar);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@vehicle_number", SqlDbType.VarChar);
			insert.Parameters.Add("@vehicle_region", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);


			select = new SqlCommand("�������������_��_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_licence_number = new SqlCommand("�������������_��_�������_�����", connection);
			select_licence_number.CommandType = CommandType.StoredProcedure;
			select_licence_number.Parameters.Add("@mask", SqlDbType.VarChar);

			select_vehicle_number = new SqlCommand("�������������_��_�������_�������", connection);
			select_vehicle_number.CommandType = CommandType.StoredProcedure;
			select_vehicle_number.Parameters.Add("@mask", SqlDbType.VarChar);

			find = new SqlCommand("�������������_��_�����", connection);
			find.CommandType = CommandType.StoredProcedure;
			find.Parameters.Add("@code", SqlDbType.BigInt);
		}

		public static CS_LicenceVehicle Insert(CS_LicenceVehicle element)
		{
			// ���������� ������� �������
			insert.Parameters["@version"].Value			= element.version;
			insert.Parameters["@code_auto"].Value		= element.code_auto;
			insert.Parameters["@code_owner"].Value		= element.code_owner;
			insert.Parameters["@licence_series"].Value	= element.licence_series;
			insert.Parameters["@licence_number"].Value	= element.licence_number;
			insert.Parameters["@date"].Value			= element.date;
			insert.Parameters["@vehicle_number"].Value	= element.vehicle_number;
			insert.Parameters["@vehicle_region"].Value	= element.vehicle_region;
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.code	= (long)insert.Parameters["@code"].Value;
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			CS_LicenceVehicle element	= new CS_LicenceVehicle();
			element.code				= DbSql.GetValueLong(reader, "���");
			element.version				= DbSql.GetValueLong(reader, "������");
			element.code_auto			= DbSql.GetValueLong(reader, "������_���_����������");
			element.code_owner			= DbSql.GetValueLong(reader, "������_���_��������");
			element.licence_series		= DbSql.GetValueString(reader, "�����_�������������");
			element.licence_number		= DbSql.GetValueString(reader, "�����_�������������");
			element.date				= DbSql.GetValueDate(reader, "����_������");
			element.vehicle_number		= DbSql.GetValueString(reader, "���������������_����");
			element.vehicle_region		= DbSql.GetValueString(reader, "������");

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			CS_LicenceVehicle element = (CS_LicenceVehicle)MakeElement(reader);
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

		public static void SelectInArray(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}


		public static void SelectInArrayLicenceNumber(ArrayList array, string mask)
		{
			// ���������� ������� ������ �� �����
			select_licence_number.Parameters["@mask"].Value = (string)mask;
			DbSql.FillArray(array, select_licence_number, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayVehicleNumber(ArrayList array, string mask)
		{
			// ���������� ������� ������ �� �����
			select_vehicle_number.Parameters["@mask"].Value = (string)mask;
			DbSql.FillArray(array, select_vehicle_number, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static CS_LicenceVehicle Find(long code)
		{
			find.Parameters["@code"].Value	= (long)code;
			return (CS_LicenceVehicle)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
