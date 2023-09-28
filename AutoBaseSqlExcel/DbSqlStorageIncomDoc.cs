using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlStorageIncomDoc.
	/// </summary>
	public class DbSqlStorageIncomDoc
	{
		public static SqlCommand		insert;
		public static SqlCommand		find_number;
		public static SqlCommand		find;
		public static SqlCommand		select;

		public DbSqlStorageIncomDoc()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("�����_������_������_��������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@number", SqlDbType.VarChar);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			find_number = new SqlCommand("�����_������_������_��������_�����_�����", connection);
			find_number.Parameters.Add("@number", SqlDbType.VarChar);
			find_number.Parameters.Add("@date", SqlDbType.DateTime);
			find_number.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("�����_������_������_��������_�����", connection);
			find.Parameters.Add("@code_doc", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;
			
			select = new SqlCommand("�����_������_������_��������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtStorageIncomDoc element			= new DtStorageIncomDoc();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("�����", DbSql.GetValueString(reader, "�����"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtStorageIncomDoc element = (DtStorageIncomDoc)MakeElement(reader);
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

		public static DtStorageIncomDoc Insert(DtStorageIncomDoc element)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@number"].Value = (string)element.GetData("�����");
			insert.Parameters["@date"].Value = (DateTime)element.GetData("����");
			if(DbSql.ExecuteCommandError(insert) == false) return null;
			element.SetData("���", (object)(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static DtStorageIncomDoc FindNumber(string number, DateTime date)
		{
			// ���������� ������� ������ �� �����
			find_number.Parameters["@number"].Value = (string)number;
			find_number.Parameters["@date"].Value = (DateTime)date;
			return (DtStorageIncomDoc)DbSql.Find(find_number, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInListAll(ListView list)
		{
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static DtStorageIncomDoc Find(long code_doc)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code_doc"].Value = (long)code_doc;
			return (DtStorageIncomDoc)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
