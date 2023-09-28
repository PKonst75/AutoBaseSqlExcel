using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlDirection.
	/// </summary>
	public class DbSqlDirection
	{
		public static SqlCommand	select;
		public static SqlCommand	select_list;
		public static SqlCommand	find;
		public static SqlCommand	insert;
		public static SqlCommand	update;
		public static SqlCommand	delete;

		public static SqlCommand	insert_done;


		public DbSqlDirection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("�����������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_list = new SqlCommand("�����������_������", connection);
			select_list.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_list.Parameters.Add("@code_factory", SqlDbType.BigInt);
			select_list.Parameters.Add("@model_name", SqlDbType.VarChar);
			select_list.Parameters.Add("@search_term", SqlDbType.BigInt);
			select_list.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("�����������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("�����������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			insert.Parameters.Add("@code_factory", SqlDbType.BigInt);
			insert.Parameters.Add("@interval_start", SqlDbType.BigInt);
			insert.Parameters.Add("@interval_end", SqlDbType.BigInt);
			insert.Parameters.Add("@number", SqlDbType.VarChar);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@model", SqlDbType.VarChar);
			insert.Parameters.Add("@description", SqlDbType.VarChar);
			insert.Parameters.Add("@search_type", SqlDbType.BigInt);
			DbSql.SetReturnError(insert);
			insert.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("�����������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_factory", SqlDbType.BigInt);
			update.Parameters.Add("@interval_start", SqlDbType.BigInt);
			update.Parameters.Add("@interval_end", SqlDbType.BigInt);
			update.Parameters.Add("@number", SqlDbType.VarChar);
			update.Parameters.Add("@date", SqlDbType.DateTime);
			update.Parameters.Add("@model", SqlDbType.VarChar);
			update.Parameters.Add("@description", SqlDbType.VarChar);
			update.Parameters.Add("@search_type", SqlDbType.BigInt);
			DbSql.SetReturnError(update);
			update.CommandType = CommandType.StoredProcedure;


			insert_done = new SqlCommand("�����������_����������_����������", connection);
			insert_done.Parameters.Add("@code_direction", SqlDbType.BigInt);
			insert_done.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_code", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert_done.Parameters.Add("@card_year", SqlDbType.BigInt);
			insert_done.Parameters.Add("@other_diler", SqlDbType.Bit);
			insert_done.Parameters.Add("@exception", SqlDbType.Bit);
			DbSql.SetReturnError(insert_done);
			insert_done.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtDirection element			= new DtDirection();
			element.SetData("���_�����������", DbSql.GetValueLong(reader, "���_�����������"));
			element.SetData("�������������_�����������", DbSql.GetValueLong(reader, "�������������_�����������"));
			element.SetData("������_�����������", DbSql.GetValueString(reader, "������_�����������"));
			element.SetData("������_��������_�����������", DbSql.GetValueLong(reader, "������_��������_�����������"));
			element.SetData("���������_��������_�����������", DbSql.GetValueLong(reader, "���������_��������_�����������"));
			element.SetData("�����_�����������", DbSql.GetValueString(reader, "�����_�����������"));
			element.SetData("��������_�����������", DbSql.GetValueString(reader, "��������_�����������"));
			element.SetData("���_������_�����������", DbSql.GetValueLong(reader, "���_������_�����������"));
			element.SetData("����_�����������", DbSql.GetValueDate(reader, "����_�����������"));
			element.SetData("������������_�������������_�����������", DbSql.GetValueString(reader, "������������_�������������_�����������"));
			return (object)element;
		}

		public static DtDirection Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			DtDirection element = (DtDirection)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
			return element;
		}

		public static bool Insert(DtDirection data)
		{
			insert.Parameters["@code"].Value			= (long)data.GetData("���_�����������");
			insert.Parameters["@code_factory"].Value	= (long)data.GetData("�������������_�����������");
			insert.Parameters["@model"].Value			= (string)data.GetData("������_�����������");
			insert.Parameters["@interval_start"].Value	= (long)data.GetData("������_��������_�����������");
			insert.Parameters["@interval_end"].Value	= (long)data.GetData("���������_��������_�����������");
			insert.Parameters["@number"].Value			= (string)data.GetData("�����_�����������");
			insert.Parameters["@description"].Value		= (string)data.GetData("��������_�����������");
			insert.Parameters["@search_type"].Value		= (long)data.GetData("���_������_�����������");
			insert.Parameters["@date"].Value			= (DateTime)data.GetData("����_�����������");
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			data.SetData("���_�����������", (long)insert.Parameters["@code"].Value);
			MessageBox.Show("�����-������������ ��������");
			return true;
		}
		public static bool Update(DtDirection data)
		{
			update.Parameters["@code"].Value			= (long)data.GetData("���_�����������");
			update.Parameters["@code_factory"].Value	= (long)data.GetData("�������������_�����������");
			update.Parameters["@model"].Value			= (string)data.GetData("������_�����������");
			update.Parameters["@interval_start"].Value	= (long)data.GetData("������_��������_�����������");
			update.Parameters["@interval_end"].Value	= (long)data.GetData("���������_��������_�����������");
			update.Parameters["@number"].Value			= (string)data.GetData("�����_�����������");
			update.Parameters["@description"].Value		= (string)data.GetData("��������_�����������");
			update.Parameters["@search_type"].Value		= (long)data.GetData("���_������_�����������");
			update.Parameters["@date"].Value			= (DateTime)data.GetData("����_�����������");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			MessageBox.Show("�����-������������ �������");
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtDirection element = (DtDirection)MakeElement(reader);
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

		public static void PrepareSelectList(DbAuto auto)
		{
			select_list.Parameters["@code_auto"].Value = (long)auto.Code;
			select_list.Parameters["@code_factory"].Value = (long)auto.CodeFactory;
			select_list.Parameters["@model_name"].Value = (string)auto.ModelTxt;
			select_list.Parameters["@search_term"].Value = (long)auto.SparePartNumber;
		}

		public static bool InsertDone(DbAuto auto, DbCard card, DtDirection direction, bool exception)
		{
			insert_done.Parameters["@code_direction"].Value = (long)direction.GetData("���_�����������");
			insert_done.Parameters["@code_auto"].Value = (long)auto.Code;
			if(card != null)
			{
				insert_done.Parameters["@card_number"].Value = (long)card.Number;
				insert_done.Parameters["@card_year"].Value = (long)card.Year;
				insert_done.Parameters["@card_code"].Value = (long)0;
				insert_done.Parameters["@other_diler"].Value = (bool)false;
				insert_done.Parameters["@exception"].Value = (bool)false;
			}
			else
			{
				insert_done.Parameters["@card_number"].Value = (long)0;
				insert_done.Parameters["@card_year"].Value = (long)0;
				insert_done.Parameters["@card_code"].Value = (long)0;
				if(exception == false)
				{
					insert_done.Parameters["@other_diler"].Value = (bool)true;
					insert_done.Parameters["@exception"].Value = (bool)false;
				}
				else
				{
					insert_done.Parameters["@other_diler"].Value = (bool)false;
					insert_done.Parameters["@exception"].Value = (bool)exception;
				}
			}
			if(DbSql.ExecuteCommandError(insert_done) == false) return false;
			MessageBox.Show("����������� ���������");
			return true;
		}
	}
}
