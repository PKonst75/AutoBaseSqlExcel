using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������/������ � ���� ������.
	/// </summary>
	public class DbSqlCatalogueParts
	{
		public static SqlCommand		insert;
		public static SqlCommand		update;
		public static SqlCommand		find;
		public static SqlCommand		select_name;
		public static SqlCommand		select;
		public static SqlCommand		select_group;
		public static SqlCommand		select_group_detail;

		public DbSqlCatalogueParts()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("�������_������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_group", SqlDbType.BigInt);
			insert.Parameters.Add("@flag_group", SqlDbType.Bit);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("�������_������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@code_group", SqlDbType.BigInt);
			update.Parameters.Add("@name", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			find = new SqlCommand("�������_������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			select_name = new SqlCommand("�������_������_�����_������������", connection);
			select_name.Parameters.Add("@pattern", SqlDbType.VarChar);
			select_name.CommandType = CommandType.StoredProcedure;

			select = new SqlCommand("�������_������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_group = new SqlCommand("�������_������_�����_������", connection);
			select_group.Parameters.Add("@code_group", SqlDbType.BigInt);
			select_group.CommandType = CommandType.StoredProcedure;

			select_group_detail = new SqlCommand("�������_������_�����_������_������", connection);
			select_group_detail.Parameters.Add("@code_group", SqlDbType.BigInt);
			select_group_detail.CommandType = CommandType.StoredProcedure;
		}

		public static DtCatalogueParts Insert(DtCatalogueParts element)
		{
			// ���������� ������ ��������
			insert.Parameters["@code_group"].Value	= (long)element.GetData("���_������");
			insert.Parameters["@flag_group"].Value	= (bool)element.GetData("����_������");
			insert.Parameters["@name"].Value		= (string)element.GetData("������������");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���_�������_������", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtCatalogueParts element)
		{
			// ���������� ������ ��������
			update.Parameters["@code"].Value		= (long)element.GetData("���_�������_������");
			update.Parameters["@code_group"].Value	= (long)element.GetData("���_������");
			update.Parameters["@name"].Value		= (string)element.GetData("������������");
			return DbSql.ExecuteCommandError(update);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCatalogueParts element			= new DtCatalogueParts();
			element.SetData("���_�������_������", DbSql.GetValueLong(reader, "���_�������_������"));
			element.SetData("���_������", DbSql.GetValueLong(reader, "���_������"));
			element.SetData("����_������", DbSql.GetValueBool(reader, "����_������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCatalogueParts element = (DtCatalogueParts)MakeElement(reader);
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

		public static TreeNode MakeTNode(SqlDataReader reader)
		{
			DtCatalogueParts element = (DtCatalogueParts)MakeElement(reader);
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

		public static void SelectInTree(TreeNode node, long code_group)
		{
			// ���������� ������� ������ ������� � ������
			select_group.Parameters["@code_group"].Value = (long)code_group;
			DbSql.FillTreeNode(node, select_group, new DbSql.DelegateMakeTNode(MakeTNode));

			// �������� ��������
			foreach(TreeNode element in node.Nodes)
			{
				long element_code = (long) element.Tag;
				SelectInTree(element, element_code);
			}
		}

		public static void SelectInTree(TreeView tree)
		{
			// ���������� ������� ������ ������� � ������
			select_group.Parameters["@code_group"].Value = (long)0;
			DbSql.FillTree(tree, select_group, new DbSql.DelegateMakeTNode(MakeTNode));
			// �������� ��������
			foreach(TreeNode element in tree.Nodes)
			{
				long element_code = (long) element.Tag;
				SelectInTree(element, element_code);
			}
		}

		public static DtCatalogueParts Find(long code)
		{
			// ���������� ������� ������ ������� � ������
			find.Parameters["@code"].Value = (long)code;
			return (DtCatalogueParts)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInList(ListView list, long code_group)
		{
			// ���������� ������� ������ ������� � ������
			select_group_detail.Parameters["@code_group"].Value = (long)code_group;
			DbSql.FillList(list, select_group_detail, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list, string name)
		{
			// ���������� ������� ������ ������� � ������
			select_name.Parameters["@pattern"].Value = (string)name;
			DbSql.FillList(list, select_name, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInList(ListView list)
		{
			// ���������� ������� ������ ������� � ������
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
	}
}
