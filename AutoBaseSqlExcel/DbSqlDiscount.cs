using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlDiscount.
	/// </summary>
	public class DbSqlDiscount
	{
		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		insert;
		public static SqlCommand		update;
		public static SqlCommand		give;
		public static SqlCommand		find_partner;

		public DbSqlDiscount()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("�������_�������", connection);
			select.Parameters.Add("@code_from", SqlDbType.BigInt);
			select.Parameters.Add("@code_to", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("�������_�����_���", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_partner = new SqlCommand("�������_�����_����������", connection);
			find_partner.Parameters.Add("@code_partner", SqlDbType.BigInt);
			find_partner.CommandType = CommandType.StoredProcedure;

			insert = new SqlCommand("�������_����������", connection);
			insert.Parameters.Add("@discount_service_work", SqlDbType.Real);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			update = new SqlCommand("�������_���������", connection);
			update.Parameters.Add("@code", SqlDbType.BigInt);
			update.Parameters.Add("@discount_service_work", SqlDbType.Real);
			update.Parameters.Add("@code_partner", SqlDbType.BigInt);
			update.Parameters.Add("@comment", SqlDbType.VarChar);
			update.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(update);

			give = new SqlCommand("�������_������", connection);
			give.Parameters.Add("@code", SqlDbType.BigInt);
			give.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(give);
		}

		public static bool Insert(float discount)
		{
			insert.Parameters["@discount_service_work"].Value			= (float)discount;
			if(DbSql.ExecuteCommandError(insert) == false) return false;
			return true;
		}

		public static bool Give(long code)
		{
			give.Parameters["@code"].Value	= (long)code;
			if(DbSql.ExecuteCommandError(give) == false) return false;
			return true;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtDiscount element = (DtDiscount)MakeElement(reader);
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

		public static object MakeElement(SqlDataReader reader)
		{
			DtDiscount element			= new DtDiscount();
			element.SetData("���_�������", DbSql.GetValueLong(reader, "���_�������"));
			element.SetData("������_������_������_�������", DbSql.GetValueFloat(reader, "������_������_������_�������"));
			element.SetData("���_����������_�������", DbSql.GetValueLong(reader, "���_����������_�������"));
			element.SetData("����_������_�������", DbSql.GetValueBool(reader, "����_������_�������"));
			element.SetData("����������_�������", DbSql.GetValueString(reader, "����������_�������"));
			element.SetData("����������_������������", DbSql.GetValueString(reader, "����������_������������"));
			return (object)element;
		}

		public static void PrepareSelect(long code_from, long code_to)
		{
			select.Parameters["@code_from"].Value = (long)code_from;
			select.Parameters["@code_to"].Value = (long)code_to;
		}

		public static DtDiscount Find(long code)
		{
			find.Parameters["@code"].Value		= (long)code;
			return(DtDiscount)DbSql.Find(find, new DbSql.DelegateMakeElement(DbSqlDiscount.MakeElement));
		}

		public static DtDiscount FindPartner(long code)
		{
			find_partner.Parameters["@code_partner"].Value		= (long)code;
			return(DtDiscount)DbSql.Find(find_partner, new DbSql.DelegateMakeElement(DbSqlDiscount.MakeElement));
		}

		public static bool Update(DtDiscount element)
		{
			update.Parameters["@code"].Value					= (long)element.GetData("���_�������");
			update.Parameters["@discount_service_work"].Value	= (float)element.GetData("������_������_������_�������");
			update.Parameters["@code_partner"].Value			= (long)element.GetData("���_����������_�������");
			update.Parameters["@comment"].Value					= (string)element.GetData("����������_�������");
			if(DbSql.ExecuteCommandError(update) == false) return false;
			return true;
		}

	}
}
