using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardWorkComment.
	/// </summary>
	public class DbSqlCardWorkComment
	{
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand insert_connection;
		public static SqlCommand delete_connection;
		public static SqlCommand show_connection;
		public static SqlCommand hide_connection;
		public static SqlCommand select;
		public static SqlCommand select_connection;
		public static SqlCommand select_connection_visible;
		public static SqlCommand select_connection_invisible;

		public DbSqlCardWorkComment()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("��������_������������_����������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@text", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("��������_������������_����������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@text", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			select = new SqlCommand("��������_������������_����������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			insert_connection = new SqlCommand("��������_������������_����������_�����_����������", connection);
			insert_connection.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert_connection.Parameters.Add("@card_year", SqlDbType.Int);
			insert_connection.Parameters.Add("@position", SqlDbType.BigInt);
			insert_connection.Parameters.Add("@code", SqlDbType.BigInt);
			insert_connection.Parameters.Add("@show_flag", SqlDbType.Bit);
			insert_connection.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_connection);

			delete_connection = new SqlCommand("��������_������������_����������_�����_��������", connection);
			delete_connection.Parameters.Add("@card_number", SqlDbType.BigInt);
			delete_connection.Parameters.Add("@card_year", SqlDbType.Int);
			delete_connection.Parameters.Add("@position", SqlDbType.BigInt);
			delete_connection.Parameters.Add("@code", SqlDbType.BigInt);
			delete_connection.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete_connection);

			show_connection = new SqlCommand("��������_������������_����������_�����_��������", connection);
			show_connection.Parameters.Add("@card_number", SqlDbType.BigInt);
			show_connection.Parameters.Add("@card_year", SqlDbType.Int);
			show_connection.Parameters.Add("@position", SqlDbType.BigInt);
			show_connection.Parameters.Add("@code", SqlDbType.BigInt);
			show_connection.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(show_connection);

			hide_connection = new SqlCommand("��������_������������_����������_�����_��������", connection);
			hide_connection.Parameters.Add("@card_number", SqlDbType.BigInt);
			hide_connection.Parameters.Add("@card_year", SqlDbType.Int);
			hide_connection.Parameters.Add("@position", SqlDbType.BigInt);
			hide_connection.Parameters.Add("@code", SqlDbType.BigInt);
			hide_connection.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(hide_connection);


			select_connection = new SqlCommand("��������_������������_����������_�����_�������", connection);
			select_connection.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_connection.Parameters.Add("@card_year", SqlDbType.Int);
			select_connection.Parameters.Add("@position", SqlDbType.BigInt);
			select_connection.CommandType = CommandType.StoredProcedure;

			select_connection_visible = new SqlCommand("��������_������������_����������_�����_�������_�������", connection);
			select_connection_visible.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_connection_visible.Parameters.Add("@card_year", SqlDbType.Int);
			select_connection_visible.Parameters.Add("@position", SqlDbType.BigInt);
			select_connection_visible.CommandType = CommandType.StoredProcedure;

			select_connection_invisible = new SqlCommand("��������_������������_����������_�����_�������_���������", connection);
			select_connection_invisible.Parameters.Add("@card_number", SqlDbType.BigInt);
			select_connection_invisible.Parameters.Add("@card_year", SqlDbType.Int);
			select_connection_invisible.Parameters.Add("@position", SqlDbType.BigInt);
			select_connection_invisible.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardWorkComment element			= new DtCardWorkComment();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("�����", DbSql.GetValueString(reader, "�����"));
			
			return (object)element;
		}

		public static DtCardWorkComment Insert(DtCardWorkComment element)
		{
			insert.Parameters["@text"].Value = (string)element.GetData("�����");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("���", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(long code, string text)
		{
			update.Parameters["@code"].Value = (long)code;
			update.Parameters["@text"].Value = (string)text;
			if(DbSql.ExecuteCommandError(update) != true) return false;
			return true;
		}

		public static bool InsertConnection(long card_number, int card_year, int position, long code, bool show_flag)
		{
			insert_connection.Parameters["@card_number"].Value = (long)card_number;
			insert_connection.Parameters["@card_year"].Value = (int)card_year;
			insert_connection.Parameters["@position"].Value = (long)position;
			insert_connection.Parameters["@code"].Value = (long)code;
			insert_connection.Parameters["@show_flag"].Value = (bool)show_flag;
			return DbSql.ExecuteCommandError(insert_connection);
		}

		public static bool DeleteConnection(long card_number, int card_year, int position, long code)
		{
			delete_connection.Parameters["@card_number"].Value = (long)card_number;
			delete_connection.Parameters["@card_year"].Value = (int)card_year;
			delete_connection.Parameters["@position"].Value = (long)position;
			delete_connection.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete_connection);
		}

		public static bool ShowConnection(long card_number, int card_year, int position, long code)
		{
			show_connection.Parameters["@card_number"].Value = (long)card_number;
			show_connection.Parameters["@card_year"].Value = (int)card_year;
			show_connection.Parameters["@position"].Value = (long)position;
			show_connection.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(show_connection);
		}

		public static bool HideConnection(long card_number, int card_year, int position, long code)
		{
			hide_connection.Parameters["@card_number"].Value = (long)card_number;
			hide_connection.Parameters["@card_year"].Value = (int)card_year;
			hide_connection.Parameters["@position"].Value = (long)position;
			hide_connection.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(hide_connection);
		}

		public static void SelectInArray(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayConnection(ArrayList array, long card_number, int card_year, int position)
		{
			// ���������� ������� ������ �� �����
			select_connection.Parameters["@card_number"].Value = (long)card_number;
			select_connection.Parameters["@card_year"].Value = (int)card_year;
			select_connection.Parameters["@position"].Value = (long)position;
			DbSql.FillArray(array, select_connection, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayConnectionVisible(ArrayList array, long card_number, int card_year, int position)
		{
			// ���������� ������� ������ �� �����
			select_connection_visible.Parameters["@card_number"].Value = (long)card_number;
			select_connection_visible.Parameters["@card_year"].Value = (int)card_year;
			select_connection_visible.Parameters["@position"].Value = (long)position;
			DbSql.FillArray(array, select_connection_visible, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayConnectionInvisible(ArrayList array, long card_number, int card_year, int position)
		{
			// ���������� ������� ������ �� �����
			select_connection_invisible.Parameters["@card_number"].Value = (long)card_number;
			select_connection_invisible.Parameters["@card_year"].Value = (int)card_year;
			select_connection_invisible.Parameters["@position"].Value = (long)position;
			DbSql.FillArray(array, select_connection_invisible, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
