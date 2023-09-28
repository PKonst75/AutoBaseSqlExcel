using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlPartnerPerson.
	/// </summary>
	public class DbSqlPartnerPerson
	{
		public DbSqlPartnerPerson()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtPartnerPerson element			= new DtPartnerPerson();
			element.SetData("�������", DbSql.GetValueString(reader, "�������"));
			element.SetData("���", DbSql.GetValueString(reader, "���"));
			element.SetData("��������", DbSql.GetValueString(reader, "��������"));
			element.SetData("�����_��������", DbSql.GetValueString(reader, "�����_��������"));
			element.SetData("����_��������", DbSql.GetValueDate(reader, "����_��������"));
			element.SetData("����_����_��������", DbSql.GetValueBool(reader, "����_����_��������"));
			element.SetData("�����_����������", DbSql.GetValueString(reader, "�����_����������"));
			
			return (object)element;
		}

		public static void Update(DtPartnerPerson element, SqlCommand command)
		{
			command.Parameters["@surname"].Value		= (string)element.GetData("�������");
			command.Parameters["@name"].Value			= (string)element.GetData("���");
			command.Parameters["@patronymic"].Value		= (string)element.GetData("��������");
			command.Parameters["@registration"].Value	= (string)element.GetData("�����_��������");
			command.Parameters["@birthday"].Value		= (DateTime)element.GetData("����_��������");
			command.Parameters["@is_birthday"].Value	= (bool)element.GetData("����_����_��������");
			command.Parameters["@address_living"].Value	= (string)element.GetData("�����_����������");
		}

		public static void Insert(DtPartnerPerson element, SqlCommand command)
		{
			command.Parameters["@surname"].Value		= (string)element.GetData("�������");
			command.Parameters["@name"].Value			= (string)element.GetData("���");
			command.Parameters["@patronymic"].Value		= (string)element.GetData("��������");
			command.Parameters["@registration"].Value	= (string)element.GetData("�����_��������");
			command.Parameters["@birthday"].Value		= (DateTime)element.GetData("����_��������");
			command.Parameters["@is_birthday"].Value	= (bool)element.GetData("����_����_��������");
			command.Parameters["@address_living"].Value	= (string)element.GetData("�����_����������");
		}
	}
}
