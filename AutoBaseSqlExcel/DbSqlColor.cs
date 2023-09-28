using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlColor.
	/// </summary>
	public class DbSqlColor
	{

		public static SqlCommand select;
		public static SqlCommand select_all;
        public static SqlCommand select_all_mask;
		public static SqlCommand find;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;

		public static SqlCommand cancel;

		public DbSqlColor()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_����_�������", connection);
			select.Parameters.Add("@code_model", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			select_all = new SqlCommand("����������_����_�������_����", connection);
			select_all.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_all.CommandType = CommandType.StoredProcedure;

            select_all_mask = new SqlCommand("����������_����_�������_����_�����", connection);
            select_all_mask.Parameters.Add("@code_model", SqlDbType.BigInt);
            select_all_mask.Parameters.Add("@pattern", SqlDbType.VarChar);
            select_all_mask.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("����������_����_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_����_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert.Parameters.Add("@color_code", SqlDbType.VarChar);
			insert.Parameters.Add("@color_name", SqlDbType.VarChar);
			insert.Parameters.Add("@color_description", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("����������_����_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_model", SqlDbType.BigInt);
			update.Parameters.Add("@color_code", SqlDbType.VarChar);
			update.Parameters.Add("@color_name", SqlDbType.VarChar);
			update.Parameters.Add("@color_description", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("����������_����_��������", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			cancel = new SqlCommand("����������_����_������", connection);
			cancel.Parameters.Add("@code", SqlDbType.BigInt);
			cancel.Parameters.Add("@cancel", SqlDbType.Bit);
			cancel.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(cancel);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtColor element			= new DtColor();
			element.SetData("���_����������_����", DbSql.GetValueLong(reader, "���_����������_����"));
			element.SetData("������_���_����������_������", DbSql.GetValueLong(reader, "������_���_����������_������"));
			element.SetData("����_���", DbSql.GetValueString(reader, "����_���"));
			element.SetData("����_������������", DbSql.GetValueString(reader, "����_������������"));
			element.SetData("����_��������", DbSql.GetValueString(reader, "����_��������"));
			element.SetData("����_�������", DbSql.GetValueBool(reader, "����_�������"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtColor element = (DtColor)MakeElement(reader);
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

		public static void SelectInList(ListView list, long code_model)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListAll(ListView list, long code_model)
		{
			// ���������� ������� ������ �� �����
			select_all.Parameters["@code_model"].Value = (long)code_model;
			DbSql.FillList(list, select_all, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

        public static void SelectInArrayAll(ArrayList array, long code_model, string pattern)
        {
            // ���������� ������� ������ �� �����
            select_all_mask.Parameters["@code_model"].Value = (long)code_model;
            select_all_mask.Parameters["@pattern"].Value = (string)pattern;
            DbSql.FillArray(array, select_all_mask, new DbSql.DelegateMakeElement(MakeElement));
        }


		public static DtColor Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			return (DtColor)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtColor Insert(DtColor element)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@code_model"].Value			= (long)element.GetData("������_���_����������_������");
			insert.Parameters["@color_code"].Value			= (string)element.GetData("����_���");
			insert.Parameters["@color_name"].Value			= (string)element.GetData("����_������������");
			insert.Parameters["@color_description"].Value	= (string)element.GetData("����_��������");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("���_����������_����",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtColor element)
		{
			// ���������� ������� ������ �� �����
			update.Parameters["@code"].Value				= (long)element.GetData("���_����������_����");
			update.Parameters["@code_model"].Value			= (long)element.GetData("������_���_����������_������");
			update.Parameters["@color_code"].Value			= (string)element.GetData("����_���");
			update.Parameters["@color_name"].Value			= (string)element.GetData("����_������������");
			update.Parameters["@color_description"].Value	= (string)element.GetData("����_��������");
			if(DbSql.ExecuteCommandError(update)== false) return false;
			return true;
		}

		public static bool Delete(long code)
		{
			// ���������� ������� ������ �� �����
			delete.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(delete)== false) return false;
			return true;
		}

		public static bool Cancel(long code)
		{
			// ���������� ������� ������ �� �����
			cancel.Parameters["@cancel"].Value				= (bool)true;
			cancel.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(cancel)== false) return false;
			return true;
		}
		public static bool Restore(long code)
		{
			// ���������� ������� ������ �� �����
			cancel.Parameters["@cancel"].Value				= (bool)false;
			cancel.Parameters["@code"].Value				= (long)code;
			if(DbSql.ExecuteCommandError(cancel)== false) return false;
			return true;
		}
	}
}
