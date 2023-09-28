using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс отвечающий за запись/чтение CS_PTS в SQL базе данных.
	/// </summary>
	public class Data_SQL_PTS
	{
		
		DT_Struct data;

		public static void Init(SqlConnection connection)
		{
			SqlCommand insert	= new SqlCommand("ДОКУМЕНТ_ПТС_ДОБАВЛЕНИЕ", connection);
			insert.Parameters.Add("@code", SqlDbType.BigInt);
			insert.Parameters.Add("@series", SqlDbType.VarChar);
			insert.Parameters.Add("@name", SqlDbType.VarChar);
			insert.Parameters["@code"].Direction = ParameterDirection.Output;
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);
		}

		public Data_SQL_PTS(DT_Struct source)
		{
			data = source;
		}

		public void Test()
		{
			data.ChangeLong1("КОД", 9);
		}

		public DT_Struct Return()
		{
			return data;
		}

		public void FillCommand(SqlCommand command)
		{
			foreach(SqlParameter p in command.Parameters)
			{
				foreach(DT_Struct.DT s in data.datas)
				{
					if(p.ParameterName == s.Database_Parameter)
					{
						if(p.Direction == ParameterDirection.Input && p.Direction == ParameterDirection.InputOutput)
							p.Value	= s.data;
					}
				}
			}
		}

		public void GetCommandData(SqlCommand command)
		{
			foreach(SqlParameter p in command.Parameters)
			{
				foreach(DT_Struct.DT s in data.datas)
				{
					if(p.ParameterName == s.Database_Parameter)
					{
						if(p.Direction == ParameterDirection.Output && p.Direction == ParameterDirection.InputOutput)
						{
							int i = data.datas.IndexOf(s);
							((DT_Struct.DT)data.datas[i]).SetData(p.Value);	
						}
					}
				}
			}
		}

		public bool ExecuteCommand(SqlCommand command)
		{
			return true;
		}
	}
}
