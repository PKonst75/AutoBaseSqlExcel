using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlIncomingCall.
	/// </summary>
	public class DbSqlIncomingCall
	{

		public static SqlCommand		select;
		public static SqlCommand		insert;

		public DbSqlIncomingCall()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("����_��������_��������_�������", connection);
			select.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("����_��������_��������_����������", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@year", SqlDbType.Int);
			insert.Parameters.Add("@date", SqlDbType.DateTime);
			insert.Parameters.Add("@interest", SqlDbType.SmallInt);
			insert.Parameters.Add("@contact_type", SqlDbType.SmallInt);
			insert.Parameters.Add("@fio", SqlDbType.VarChar);
			insert.Parameters.Add("@contact", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.Parameters["@year"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtIncomingCall element			= new DtIncomingCall();
			element.SetData("���", DbSql.GetValueLong(reader, "���"));
			element.SetData("���", DbSql.GetValueInt(reader, "���"));
			element.SetData("����", DbSql.GetValueDate(reader, "����"));
			element.SetData("�������", DbSql.GetValueShort(reader, "�������"));
			element.SetData("���", DbSql.GetValueShort(reader, "���"));
			element.SetData("���", DbSql.GetValueString(reader, "���"));
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtIncomingCall element = (DtIncomingCall)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= null;
				item.Text			= "������";
			}
			return item;
		}

		public static DtIncomingCall InsertIncomingCall(DtIncomingCall incomingCall)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@date"].Value = incomingCall.date;
			insert.Parameters["@interest"].Value = incomingCall.interest;
			insert.Parameters["@contact_type"].Value = incomingCall.contact_type;
			insert.Parameters["@fio"].Value = incomingCall.fio;
			insert.Parameters["@contact"].Value = incomingCall.contact;
			if (DbSql.ExecuteCommandError(insert) == false) return null;
			incomingCall.code = (long)insert.Parameters["@code"].Value;
			incomingCall.year = (int)insert.Parameters["@year"].Value;
			return incomingCall;
		}

		public static void SelectInList(ListView list)
		{
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}
	}
}
