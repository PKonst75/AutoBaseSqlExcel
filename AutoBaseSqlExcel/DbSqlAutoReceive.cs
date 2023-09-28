using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoReceive.
	/// </summary>
	public class DbSqlAutoReceive
	{
		public static SqlCommand select;
		public static SqlCommand find;
		public static SqlCommand find_auto;
		public static SqlCommand insert;
		public static SqlCommand update;
		public static SqlCommand delete;
		public static SqlCommand transact;

		public static SqlCommand receive;			// ��������� ����������
		public static SqlCommand receive_delete;	// ������ ��������� ����������
		public static SqlCommand update_comment;	// ��������� �����������

		public DbSqlAutoReceive()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����������_���������_��������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("����������_���������_��������_�����", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_auto = new SqlCommand("����������_���������_��������_�����_����������", connection);
			find_auto.Parameters.Add("@code_auto", SqlDbType.BigInt);
			find_auto.CommandType = CommandType.StoredProcedure;


			insert = new SqlCommand("����������_���������_��������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.Parameters.Add("@code_receiver", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("����������_���������_��������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@date", SqlDbType.DateTime);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.Parameters.Add("@code_receiver", SqlDbType.BigInt);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			delete = new SqlCommand("����������_���������_��������_��������", connection);
			delete.Parameters.Add("@code", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			transact = new SqlCommand("����������_���������_��������_����������", connection);
			transact.Parameters.Add("@code", SqlDbType.BigInt);
			transact.Parameters.Add("@transaction", SqlDbType.Bit);
			transact.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(transact);

			receive = new SqlCommand("����������_���������_����������", connection);
			receive.Parameters.Add("@code", SqlDbType.BigInt);
			receive.Parameters.Add("@code_auto", SqlDbType.BigInt);
			receive.Parameters.Add("@code_document", SqlDbType.BigInt);
			receive.Parameters.Add("@comment", SqlDbType.VarChar);
			receive.CommandType = CommandType.StoredProcedure;
			receive.Parameters["@code"].Direction	= ParameterDirection.Output;
			DbSql.SetReturnError(receive);

			receive_delete = new SqlCommand("����������_���������_��������", connection);
			receive_delete.Parameters.Add("@code_auto", SqlDbType.BigInt);
			receive_delete.Parameters.Add("@code_document", SqlDbType.BigInt);
			receive_delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(receive_delete);

			update_comment = new SqlCommand("����������_���������_����������", connection);
			update_comment.Parameters.Add("@code_auto", SqlDbType.BigInt);
			update_comment.Parameters.Add("@code_document", SqlDbType.BigInt);
			update_comment.Parameters.Add("@comment", SqlDbType.VarChar);
			update_comment.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update_comment);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoReceive element			= new DtAutoReceive();
			element.SetData("���_����������_���������_��������", DbSql.GetValueLong(reader, "���_����������_���������_��������"));
			element.SetData("����_��������", DbSql.GetValueDate(reader, "����_��������"));
			element.SetData("����������_��������", DbSql.GetValueString(reader, "����������_��������"));
			element.SetData("���_�������_����������", DbSql.GetValueLong(reader, "���_�������_����������"));
			element.SetData("��������_��������", DbSql.GetValueBool(reader, "��������_��������"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtAutoReceive element = (DtAutoReceive)MakeElement(reader);
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

		public static DtAutoReceive Find(long code)
		{
			// ���������� ������� ������ �� �����
			find.Parameters["@code"].Value = (long)code;
			return (DtAutoReceive)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtAutoReceive FindAuto(long code_auto)
		{
			// ���������� ������� ������ �� �����
			find_auto.Parameters["@code_auto"].Value = (long)code_auto;
			return (DtAutoReceive)DbSql.Find(find_auto, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtAutoReceive Insert(DtAutoReceive element)
		{
			// ���������� ������� ������ �� �����
			insert.Parameters["@date"].Value			= (DateTime)element.GetData("����_��������");
			insert.Parameters["@comment"].Value			= (string)element.GetData("����������_��������");
			insert.Parameters["@code_receiver"].Value	= (long)element.GetData("���_�������_����������");
			if(DbSql.ExecuteCommandError(insert)== false) return null;
			element.SetData("���_����������_���������_��������",(long)insert.Parameters["@code"].Value);
			return element;
		}

		public static bool Update(DtAutoReceive element)
		{
			// ���������� ������� ������ �� �����
			update.Parameters["@code"].Value			= (long)element.GetData("���_����������_���������_��������");
			update.Parameters["@date"].Value			= (DateTime)element.GetData("����_��������");
			update.Parameters["@comment"].Value			= (string)element.GetData("����������_��������");
			update.Parameters["@code_receiver"].Value	= (long)element.GetData("���_�������_����������");
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

		public static bool Receive(DtAuto auto, DtAutoReceive document, string comment)
		{
			// ���������� ������� ������ �� �����
			receive.Parameters["@code_auto"].Value			= (long)auto.GetData("���_����������");
			receive.Parameters["@code_document"].Value		= (long)document.GetData("���_����������_���������_��������");
			receive.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(receive)== false) return false;
			return true;
		}

		public static bool ReceiveDelete(long code_auto, long code_document)
		{
			// ���������� ������� ������ �� �����
			receive_delete.Parameters["@code_auto"].Value		= (long)code_auto;
			receive_delete.Parameters["@code_document"].Value	= (long)code_document;
			if(DbSql.ExecuteCommandError(receive_delete)== false) return false;
			return true;
		}

		public static bool UpdateComment(long code_auto, long code_document, string comment)
		{
			// ���������� ������� ������ �� �����
			update_comment.Parameters["@code_auto"].Value		= (long)code_auto;
			update_comment.Parameters["@code_document"].Value	= (long)code_document;
			update_comment.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(update_comment)== false) return false;
			return true;
		}
	}
}
