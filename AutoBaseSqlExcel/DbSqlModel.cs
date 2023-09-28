using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlModel.
	/// </summary>
	public class DbSqlModel
	{
		public static SqlCommand		select_name;
		public static SqlCommand		select_brand;
		public static SqlCommand		select;
		public static SqlCommand		select_in_sell;
		public static SqlCommand		insert_brand_model;
		public static SqlCommand		delete_brand_model;
		public static SqlCommand		delete;
		public static SqlCommand		find;
        public static SqlCommand        find_name;
		public static SqlCommand		update;
		public static SqlCommand		insert;

		public static SqlCommand		update_set_sell;
		public static SqlCommand		update_remove_sell;

        public static SqlCommand        update_ex;

		public DbSqlModel()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			select_name = new SqlCommand("����������_������_�������_������������", connection);
			select_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_name.CommandType = CommandType.StoredProcedure;

			select_brand = new SqlCommand("����������_������_�������_�����", connection);
			select_brand.Parameters.Add("@code_brand", SqlDbType.BigInt);
			select_brand.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("����������_������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_in_sell = new SqlCommand("����������_������_�������_�_�������", connection);
			select_in_sell.CommandType = CommandType.StoredProcedure;

			insert_brand_model = new SqlCommand("����������_������_����������_�����", connection);
			insert_brand_model.Parameters.Add("@code_brand", SqlDbType.BigInt);
			insert_brand_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert_brand_model.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_brand_model);

			delete_brand_model = new SqlCommand("����������_������_��������_�����", connection);
			delete_brand_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			delete_brand_model.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete_brand_model);

			delete = new SqlCommand("����������_������_��������", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			find = new SqlCommand("����������_������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			update = new SqlCommand("����������_������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.Parameters.Add("@code_workgroup", SqlDbType.BigInt);
			update.Parameters.Add("@code_guarantytype", SqlDbType.BigInt);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

            update_ex = new SqlCommand("����������_������_���������_EX", connection);
            update_ex.Parameters.Add("@code", SqlDbType.BigInt);
            update_ex.Parameters.Add("@engine", SqlDbType.VarChar);
            update_ex.Parameters.Add("@trans", SqlDbType.VarChar);
            update_ex.Parameters.Add("@markmodel", SqlDbType.VarChar);
            update_ex.Parameters.Add("@type", SqlDbType.VarChar);
            update_ex.CommandType = CommandType.StoredProcedure;
            DbSql.SetReturnError(update_ex);

			insert = new SqlCommand("����������_������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters.Add("@code_workgroup", SqlDbType.BigInt);
			insert.Parameters.Add("@code_guarantytype", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update_set_sell = new SqlCommand("����������_������_�_�������", connection);
			update_set_sell.Parameters.Add("@code", SqlDbType.BigInt);
			update_set_sell.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_set_sell);

			update_remove_sell = new SqlCommand("����������_������_��_�������", connection);
			update_remove_sell.Parameters.Add("@code", SqlDbType.BigInt);
			update_remove_sell.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_remove_sell);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtModel element			= new DtModel();
			element.SetData("���_����������_������", DbSql.GetValueLong(reader, "���_����������_������"));
			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("�_�������", DbSql.GetValueBool(reader, "�_�������"));

			return (object)element;
		}

		public static object MakeElementWide(SqlDataReader reader)
		{
			DtModel element			= new DtModel();
			element.SetData("���_����������_������", DbSql.GetValueLong(reader, "���_����������_������"));
			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("������_���_����������_���", DbSql.GetValueLong(reader, "������_���_����������_���"));
			element.SetData("������_���_��������", DbSql.GetValueLong(reader, "������_���_��������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			element.SetData("��������_��������", DbSql.GetValueString(reader, "��������_��������"));
			element.SetData("�_�������", DbSql.GetValueBool(reader, "�_�������"));

			return (object)element;
		}

		public static object MakeElementFind(SqlDataReader reader)
		{
			DtModel element			= new DtModel();
			element.SetData("���_����������_������", DbSql.GetValueLong(reader, "���_����������_������"));
			element.SetData("������", DbSql.GetValueString(reader, "������"));
			element.SetData("������_���_����������_���", DbSql.GetValueLong(reader, "������_���_����������_���"));
			element.SetData("������_���_��������", DbSql.GetValueLong(reader, "������_���_��������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			element.SetData("��������_��������", DbSql.GetValueString(reader, "��������_��������"));
            element.SetData("���������", DbSql.GetValueString(reader, "���������"));
            element.SetData("�����_������_���", DbSql.GetValueString(reader, "�����_������_���"));
            element.SetData("�����������", DbSql.GetValueString(reader, "�����������"));
            element.SetData("���_��", DbSql.GetValueString(reader, "���_��"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtModel element = (DtModel)MakeElement(reader);
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
		public static ListViewItem MakeLVItemWide(SqlDataReader reader)
		{
			DtModel element = (DtModel)MakeElementWide(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemWide(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "������";
			}
			return item;
		}

		public static TreeNode MakeTNode(SqlDataReader reader)
		{
			DtModel element = (DtModel)MakeElement(reader);
			TreeNode node = new TreeNode();
			if(element != null)
			{
				element.SetTNode(node);
			}
			else
			{
				node.Tag			= 0;
				node.Text			= "������";
			}
			return node;
		}

		public static void SelectInList(ListView list, string pattern)
		{
			// ���������� ������� ������ �� �����
			select_name.Parameters["@pattern"].Value = (string)pattern;
			DbSql.FillList(list, select_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
        public static void SelectInArray(ArrayList array, string pattern)
        {
            // ���������� ������� ������ �� ����� c ������� � ������
            select_name.Parameters["@pattern"].Value = (string)pattern;
            DbSql.FillArray(array, select_name, new DbSql.DelegateMakeElement(MakeElement));
        }
		public static void SelectInArray(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}
		public static void SelectInArrayInSell(ArrayList array)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillArray(array, select_in_sell, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInTree(TreeNode node, long code_brand)
		{
			// ���������� ������� ������ �� ������
			select_brand.Parameters["@code_brand"].Value = (long)code_brand;
			DbSql.FillTreeNode(node, select_brand, new DbSql.DelegateMakeTNode(MakeTNode));
		}

		public static void SelectInList(ListView list, long code_brand)
		{
			// ���������� ������� ������ �� ������
			select_brand.Parameters["@code_brand"].Value = (long)code_brand;
			DbSql.FillList(list, select_brand, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInListWide(ListView list)
		{
			// ���������� ������� ������ �� ������
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItemWide));
		}

		public static void SelectInListAll(ListView list)
		{
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static bool InsertBrandModel(long code_brand, long code_model)
		{
			// ���������� ������� ������ �� ������
			insert_brand_model.Parameters["@code_brand"].Value = (long)code_brand;
			insert_brand_model.Parameters["@code_model"].Value = (long)code_model;
			return DbSql.ExecuteCommandError(insert_brand_model);
		}

		public static bool DeleteBrandModel(long code_model)
		{
			// ���������� ������� ������ �� ������
			delete_brand_model.Parameters["@code_model"].Value = (long)code_model;
			return DbSql.ExecuteCommandError(delete_brand_model);
		}

		public static bool Delete(long code)
		{
			// ���������� ������� ������ �� ������
			delete.Parameters["@code"].Value = (long)code;
			return DbSql.ExecuteCommandError(delete);
		}

		public static DtModel Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			return (DtModel)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElementFind));
		}

		public static bool Update(DtModel element)
		{
			// ���������� ������� ������ �� ������
			update.Parameters["@code"].Value = (long)element.GetData("���_����������_������");
			update.Parameters["@name"].Value = (string)element.GetData("������");
			update.Parameters["@code_workgroup"].Value = (long)element.GetData("������_���_����������_���");
			update.Parameters["@code_guarantytype"].Value = (long)element.GetData("������_���_��������");
			return DbSql.ExecuteCommandError(update);
		}

        public static bool UpdateEx(DtModel element)
        {
            // ���������� ������� ������ �� ������
            update_ex.Parameters["@code"].Value = (long)element.GetData("���_����������_������");
            update_ex.Parameters["@markmodel"].Value = (string)element.GetData("�����_������_���");
            update_ex.Parameters["@engine"].Value = (string)element.GetData("���������");
            update_ex.Parameters["@type"].Value = (string)element.GetData("���_��");
            update_ex.Parameters["@trans"].Value = (string)element.GetData("�����������");
            return DbSql.ExecuteCommandError(update_ex);
        }

		public static DtModel Insert(DtModel element)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@name"].Value = (string)element.GetData("������");
			insert.Parameters["@code_workgroup"].Value = (long)element.GetData("������_���_����������_���");
			insert.Parameters["@code_guarantytype"].Value = (long)element.GetData("������_���_��������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���_����������_������", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool UpdateSetSell(long code_model)
		{
			// ���������� ������� ������ �� ������
			update_set_sell.Parameters["@code"].Value = code_model;
			return DbSql.ExecuteCommandError(update_set_sell);
		}

		public static bool UpdateRemoveSell(long code_model)
		{
			// ���������� ������� ������ �� ������
			update_remove_sell.Parameters["@code"].Value = code_model;
			return DbSql.ExecuteCommandError(update_remove_sell);
		}
	}
}
