using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardClaim.
	/// </summary>
	public class DbSqlCardClaim
	{
		public static SqlCommand insert;
		public static SqlCommand select;
		public static SqlCommand remove;

		public DbSqlCardClaim()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("��������_������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
			
			insert = new SqlCommand("��������_������_����������", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_claim", SqlDbType.BigInt);
			insert.Parameters.Add("@position", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@position"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			remove = new SqlCommand("��������_������_��������", connection);
			remove.Parameters.Add("@card_number", SqlDbType.BigInt);
			remove.Parameters.Add("@card_year", SqlDbType.Int);
			remove.Parameters.Add("@position", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static DtCardClaim Insert(DtCardClaim element)
		{
			insert.Parameters["@card_number"].Value = (long)element.GetData("�����_��������");
			insert.Parameters["@card_year"].Value = (int)element.GetData("���_��������");
			insert.Parameters["@code_claim"].Value = (long)element.GetData("������_���_������");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("�������", (long)insert.Parameters["@position"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardClaim element			= new DtCardClaim();
			element.SetData("�����_��������", DbSql.GetValueLong(reader, "�����_��������"));
			element.SetData("���_��������", DbSql.GetValueInt(reader, "���_��������"));
			element.SetData("�������", DbSql.GetValueLong(reader, "�������"));
			element.SetData("������_���_������", DbSql.GetValueLong(reader, "������_���_������"));
			element.SetData("������", DbSql.GetValueBool(reader, "������"));
			element.SetData("������������", DbSql.GetValueBool(reader, "������������"));
			element.SetData("��������", DbSql.GetValueBool(reader, "��������"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			if(DbSql.IsValueNULL(reader, "����_������������") == true)
			{
				element.SetData("����_����_������������", false);
				element.SetData("����_������������", DateTime.Now);
			}
			else
			{
				element.SetData("����_����_������������", true);
				element.SetData("����_������������", DbSql.GetValueDate(reader, "����_������������"));
			}
			element.SetData("������������_������", DbSql.GetValueString(reader, "������������_������"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardClaim element = (DtCardClaim)MakeElement(reader);
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
		public static ListViewItem MakeLVItemFromElement(object srcElement)
		{
			DtCardClaim element = (DtCardClaim)srcElement;
			ListViewItem item = new ListViewItem();
			if (element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag = 0;
				item.Text = "������";
			}
			return item;
		}
		public static void SelectInList(ListView list, long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			//DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
			ArrayList array = new ArrayList();
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
			DbSql.FillListFromArray(list, array, new DbSql.DelegateMakeLVItemFromElement(MakeLVItemFromElement));

		}

		public static void SelectInArray(ArrayList array, long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool Remove(DtCard card, long position)
		{
			remove.Parameters["@card_number"].Value = (long)card.GetData("�����_��������");
			remove.Parameters["@card_year"].Value = (int)card.GetData("���_��������");
			remove.Parameters["@position"].Value = (long)position;
			return DbSql.ExecuteCommandError(remove);
		}
		public static ArrayList Select(DtCard srcCard)
        {
			ArrayList array = new ArrayList();
			SelectInArray(array, srcCard.Number, srcCard.Year);
			return array;
        }

		public static void SelectInListNoDatabase(ListView list, DtCard srcCard)
		{
			// ���������� ������� ������ �� �����
			ArrayList array = srcCard.Claims.Collection;
			DbSql.FillListFromArray(list, array, new DbSql.DelegateMakeLVItemFromElement(MakeLVItemFromElement));

		}
	}
}
