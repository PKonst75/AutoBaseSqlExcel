using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerJuridical.
	/// </summary>
	public class DbSqlPartnerJuridical
	{
		public DbSqlPartnerJuridical()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartnerJuridical element			= new DtPartnerJuridical();
			element.SetData("������������_�����������", DbSql.GetValueString(reader, "������������_�����������"));
			element.SetData("�����_�����������", DbSql.GetValueString(reader, "�����_�����������"));
			element.SetData("�����_�����������", DbSql.GetValueString(reader, "�����_�����������"));
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			
			return (object)element;
		}

		public static void Update(DtPartnerJuridical element, SqlCommand command)
		{
			command.Parameters["@name_juridical"].Value		= (string)element.GetData("������������_�����������");
			command.Parameters["@address_juridical"].Value	= (string)element.GetData("�����_�����������");
			command.Parameters["@address_fact"].Value		= (string)element.GetData("�����_�����������");
			command.Parameters["@contact"].Value			= (string)element.GetData("�������");
		}

		public static void Insert(DtPartnerJuridical element, SqlCommand command)
		{
			command.Parameters["@name_juridical"].Value		= (string)element.GetData("������������_�����������");
			command.Parameters["@address_juridical"].Value	= (string)element.GetData("�����_�����������");
			command.Parameters["@address_fact"].Value		= (string)element.GetData("�����_�����������");
			command.Parameters["@contact"].Value			= (string)element.GetData("�������");
		}
	}
}
