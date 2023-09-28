using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlBrand.
	/// </summary>
	public class DbSqlBrand
	{
		public static SqlCommand select;
		public static SqlCommand insert;
		public static SqlCommand delete;
		public static SqlCommand update;
		public static SqlCommand find_model;

		public DbSqlBrand()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_�����_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			find_model = new SqlCommand("����������_�����_�����_������", connection);
			find_model.Parameters.Add("@code_model", SqlDbType.BigInt);
			find_model.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����������_�����_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("����������_�����_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("����������_�����_��������", connection);
			delete.Parameters.Add("@code_brand", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtBrand element			= new DtBrand();
			element.SetData("���_����������_�����", DbSql.GetValueLong(reader, "���_����������_�����"));
			element.SetData("������������_����������_�����", DbSql.GetValueString(reader, "������������_����������_�����"));
			
			return (object)element;
		}

		public static TreeNode MakeTNode(SqlDataReader reader)
		{
			DtBrand element = (DtBrand)MakeElement(reader);
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

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtBrand element = (DtBrand)MakeElement(reader);
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

		public static void SelectInTree(TreeView tree)
		{
			// ���������� ������� ������ �� �����
			DbSql.FillTree(tree, select, new DbSql.DelegateMakeTNode(MakeTNode));
		}

		public static DtBrand FindModel(long code_model)
		{
			// ���������� ������� ������ �� �����
			find_model.Parameters["@code_model"].Value	= (long)code_model;
			return (DtBrand)DbSql.Find(find_model, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtBrand Insert(DtBrand element)
		{
			insert.Parameters["@name"].Value = (string)element.GetData("������������_����������_�����");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("���_����������_�����", (long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtBrand element)
		{
			update.Parameters["@code"].Value = (long)element.GetData("���_����������_�����");
			update.Parameters["@name"].Value = (string)element.GetData("������������_����������_�����");
			if(DbSql.ExecuteCommandError(update) != true) return false;
			return true;
		}

		public static bool Delete(long code_brand)
		{
			delete.Parameters["@code_brand"].Value = (long)code_brand;
			if(DbSql.ExecuteCommandError(delete) != true) return false;
			return true;
		}
	}
}
