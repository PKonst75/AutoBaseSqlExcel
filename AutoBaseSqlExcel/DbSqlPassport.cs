using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
    class DbSqlPassport:DbSql
    {
        public static SqlCommand		update;
		public static SqlCommand		insert;
        public static SqlCommand        find;

		
		public DbSqlPassport()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("КОНТРАГЕНТ_ПАСПОРТ_ДОБАВЛЕНИЕ", connection);
            insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_partner", SqlDbType.BigInt);
            insert.Parameters.Add("@active", SqlDbType.Bit);
			insert.Parameters.Add("@series", SqlDbType.VarChar);
            insert.Parameters.Add("@number", SqlDbType.VarChar);
            insert.Parameters.Add("@date", SqlDbType.DateTime);
            insert.Parameters.Add("@place", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
            insert.Parameters["@code"].Direction = ParameterDirection.Output;
            insert.Parameters["@active"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

            find = new SqlCommand("КОНТРАГЕНТ_ПАСПОРТ_ПОИСК_КОНТРАГЕНТ", connection);
            find.Parameters.Add("@code_partner", SqlDbType.BigInt);
            find.CommandType = CommandType.StoredProcedure;
        }

        public static object MakeElement(SqlDataReader reader)
        {
            DtPassport element = new DtPassport();
            element.SetData("КОД_ПАСПОРТ", DbSql.GetValueLong(reader, "КОД_ПАСПОРТ"));
            element.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ", DbSql.GetValueLong(reader, "ССЫЛКА_КОД_КОНТАРГЕНТ"));
            element.SetData("АКТИВЕН", DbSql.GetValueBool(reader, "АКТИВЕН"));
            element.SetData("СЕРИЯ", DbSql.GetValueString(reader, "СЕРИЯ"));
            element.SetData("НОМЕР", DbSql.GetValueString(reader, "НОМЕР"));
            element.SetData("ВЫДАН_КОГДА", DbSql.GetValueDate(reader, "ВЫДАН_КОГДА"));
            element.SetData("ВЫДАН_КЕМ", DbSql.GetValueString(reader, "ВЫДАН_КЕМ"));
            
            return (object)element;
        }

        public static DtPassport Find(long code_partner)
        {
            // Подготовка команды поиска по маске
            find.Parameters["@code_partner"].Value = (long)code_partner;
            return (DtPassport)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
        }

        public static DtPassport Insert(DtPassport element)
        {
            // Подготовка команды поиска по бренду
            insert.Parameters["@series"].Value = (string)element.GetData("СЕРИЯ");
            insert.Parameters["@number"].Value = (string)element.GetData("НОМЕР");
            insert.Parameters["@code_partner"].Value = (long)element.GetData("ССЫЛКА_КОД_КОНТРАГЕНТ");
            insert.Parameters["@date"].Value = (DateTime)element.GetData("ВЫДАН_КОГДА");
            insert.Parameters["@place"].Value = (string)element.GetData("ВЫДАН_КЕМ");
            if (DbSql.ExecuteCommandError(insert) == false) return null;
            element.SetData("КОД_ПАСПОРТ", (object)(long)insert.Parameters["@code"].Value);
            element.SetData("АКТИВЕН", (object)(bool)insert.Parameters["@active"].Value);
            return element;
        }
    }
}
