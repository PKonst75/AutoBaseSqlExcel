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
			element.SetData("ÔÀÌÈËÈß", DbSql.GetValueString(reader, "ÔÀÌÈËÈß"));
			element.SetData("ÈÌß", DbSql.GetValueString(reader, "ÈÌß"));
			element.SetData("ÎÒ×ÅÑÒÂÎ", DbSql.GetValueString(reader, "ÎÒ×ÅÑÒÂÎ"));
			element.SetData("ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ", DbSql.GetValueString(reader, "ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ"));
			element.SetData("ÄÀÒÀ_ĞÎÆÄÅÍÈß", DbSql.GetValueDate(reader, "ÄÀÒÀ_ĞÎÆÄÅÍÈß"));
			element.SetData("ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß", DbSql.GetValueBool(reader, "ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß"));
			element.SetData("ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ", DbSql.GetValueString(reader, "ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ"));
			
			return (object)element;
		}

		public static void Update(DtPartnerPerson element, SqlCommand command)
		{
			command.Parameters["@surname"].Value		= (string)element.GetData("ÔÀÌÈËÈß");
			command.Parameters["@name"].Value			= (string)element.GetData("ÈÌß");
			command.Parameters["@patronymic"].Value		= (string)element.GetData("ÎÒ×ÅÑÒÂÎ");
			command.Parameters["@registration"].Value	= (string)element.GetData("ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ");
			command.Parameters["@birthday"].Value		= (DateTime)element.GetData("ÄÀÒÀ_ĞÎÆÄÅÍÈß");
			command.Parameters["@is_birthday"].Value	= (bool)element.GetData("ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß");
			command.Parameters["@address_living"].Value	= (string)element.GetData("ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ");
		}

		public static void Insert(DtPartnerPerson element, SqlCommand command)
		{
			command.Parameters["@surname"].Value		= (string)element.GetData("ÔÀÌÈËÈß");
			command.Parameters["@name"].Value			= (string)element.GetData("ÈÌß");
			command.Parameters["@patronymic"].Value		= (string)element.GetData("ÎÒ×ÅÑÒÂÎ");
			command.Parameters["@registration"].Value	= (string)element.GetData("ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ");
			command.Parameters["@birthday"].Value		= (DateTime)element.GetData("ÄÀÒÀ_ĞÎÆÄÅÍÈß");
			command.Parameters["@is_birthday"].Value	= (bool)element.GetData("ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß");
			command.Parameters["@address_living"].Value	= (string)element.GetData("ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ");
		}
	}
}
