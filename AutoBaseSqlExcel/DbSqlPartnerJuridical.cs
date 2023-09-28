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
			element.SetData("мюхлемнбюмхе_чпхдхвеяйне", DbSql.GetValueString(reader, "мюхлемнбюмхе_чпхдхвеяйне"));
			element.SetData("юдпея_чпхдхвеяйхи", DbSql.GetValueString(reader, "юдпея_чпхдхвеяйхи"));
			element.SetData("юдпея_тюйрхвеяйхи", DbSql.GetValueString(reader, "юдпея_тюйрхвеяйхи"));
			element.SetData("йнмрюйр", DbSql.GetValueString(reader, "йнмрюйр"));
			
			return (object)element;
		}

		public static void Update(DtPartnerJuridical element, SqlCommand command)
		{
			command.Parameters["@name_juridical"].Value		= (string)element.GetData("мюхлемнбюмхе_чпхдхвеяйне");
			command.Parameters["@address_juridical"].Value	= (string)element.GetData("юдпея_чпхдхвеяйхи");
			command.Parameters["@address_fact"].Value		= (string)element.GetData("юдпея_тюйрхвеяйхи");
			command.Parameters["@contact"].Value			= (string)element.GetData("йнмрюйр");
		}

		public static void Insert(DtPartnerJuridical element, SqlCommand command)
		{
			command.Parameters["@name_juridical"].Value		= (string)element.GetData("мюхлемнбюмхе_чпхдхвеяйне");
			command.Parameters["@address_juridical"].Value	= (string)element.GetData("юдпея_чпхдхвеяйхи");
			command.Parameters["@address_fact"].Value		= (string)element.GetData("юдпея_тюйрхвеяйхи");
			command.Parameters["@contact"].Value			= (string)element.GetData("йнмрюйр");
		}
	}
}
