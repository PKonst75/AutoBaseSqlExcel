using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardRecomendation.
	/// </summary>
	public class DbSqlCardRecomendation
	{
		public static SqlCommand select;

		public DbSqlCardRecomendation()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("��������_������������_�������", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardRecomendation element			= new DtCardRecomendation();
			element.SetData("������_�����_��������", DbSql.GetValueLong(reader, "������_�����_��������"));
			element.SetData("������_���_��������", DbSql.GetValueInt(reader, "������_���_��������"));
			element.SetData("�����_������������", DbSql.GetValueLong(reader, "�����_������������"));
			element.SetData("������������", DbSql.GetValueString(reader, "������������"));
			return (object)element;
		}

		public static void SelectInArray(ArrayList array, long card_number, int card_year)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
