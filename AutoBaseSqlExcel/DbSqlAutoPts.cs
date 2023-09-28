using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
    class DbSqlAutoPts:DbSql
    {
        public static SqlCommand		update;
		public static SqlCommand		insert;
        public static SqlCommand        find;

		
		public DbSqlAutoPts()
		{
			
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("АВТОМОБИЛЬ_ПТС_ДОБАВЛЕНИЕ", connection);
            insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
            insert.Parameters.Add("@active", SqlDbType.Bit);
			insert.Parameters.Add("@series", SqlDbType.VarChar);
            insert.Parameters.Add("@number", SqlDbType.VarChar);
            insert.Parameters.Add("@date", SqlDbType.DateTime);
            insert.Parameters.Add("@place", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
            insert.Parameters["@code"].Direction = ParameterDirection.Output;
            insert.Parameters["@active"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

            find = new SqlCommand("АВТОМОБИЛЬ_ПТС_ПОИСК_АВТОМОБИЛЬ", connection);
            find.Parameters.Add("@code_auto", SqlDbType.BigInt);
            find.CommandType = CommandType.StoredProcedure;
        }

        public static object MakeElement(SqlDataReader reader)
        {
            DtAutoPts element = new DtAutoPts();
            element.SetData("КОД_ПТС", DbSql.GetValueLong(reader, "КОД_ПТС"));
            element.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ", DbSql.GetValueLong(reader, "ССЫЛКА_КОД_АВТОМОБИЛЬ"));
            element.SetData("АКТИВЕН", DbSql.GetValueBool(reader, "АКТИВЕН"));
            element.SetData("СЕРИЯ", DbSql.GetValueString(reader, "СЕРИЯ"));
            element.SetData("НОМЕР", DbSql.GetValueString(reader, "НОМЕР"));
            element.SetData("ВЫДАН_КОГДА", DbSql.GetValueDate(reader, "ВЫДАН_КОГДА"));
            element.SetData("ВЫДАН_КЕМ", DbSql.GetValueString(reader, "ВЫДАН_КЕМ"));
            
            return (object)element;
        }

        public static DtAutoPts Find(long code_auto)
        {
            // Подготовка команды поиска по маске
            find.Parameters["@code_auto"].Value = (long)code_auto;
            return (DtAutoPts)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
        }

        public static DtAutoPts Insert(DtAutoPts element)
        {
            // Подготовка команды поиска по бренду
            insert.Parameters["@series"].Value = (string)element.GetData("СЕРИЯ");
            insert.Parameters["@number"].Value = (string)element.GetData("НОМЕР");
            insert.Parameters["@code_auto"].Value = (long)element.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ");
            insert.Parameters["@date"].Value = (DateTime)element.GetData("ВЫДАН_КОГДА");
            insert.Parameters["@place"].Value = (string)element.GetData("ВЫДАН_КЕМ");
            if (DbSql.ExecuteCommandError(insert) == false) return null;
            element.SetData("КОД_ПТС", (object)(long)insert.Parameters["@code"].Value);
            element.SetData("АКТИВЕН", (object)(bool)insert.Parameters["@active"].Value);
            return element;
        }
    }
}
