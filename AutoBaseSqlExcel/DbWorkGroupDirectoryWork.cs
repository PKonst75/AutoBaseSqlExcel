using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbWorkGroupDirectoryWork.
	/// </summary>
	public class DbWorkGroupDirectoryWork:Db
	{
		private long		codeWorkGroup;
		private long		codeDirectoryWork;

		private static SqlCommand		cmdWrite;
		private static SqlConnection	conn;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbWorkGroupDirectoryWork(DbWorkGroup workGroupSrc, DbDirectoryWork directoryWorkSrc, bool add)
		{
			codeWorkGroup			= workGroupSrc.Code;
			codeDirectoryWork		= directoryWorkSrc.Code;

			adding					= add;
		}
		#endregion

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 2 собственных полея
			readerLength = 2;

			conn = connection;

			cmdWrite				= new SqlCommand("WRITE_РУБРИКА_ТРУДОЕМКОСТЬ_СПРАВОЧНИК_ТРУДОЕМКОСТЬ", conn);
			cmdWrite.CommandType	= CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codeWorkGroup", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDirectoryWork", SqlDbType.BigInt);
			SetReturnError(cmdWrite);
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@codeWorkGroup"].Value		= (long)codeWorkGroup;
				cmdWrite.Parameters["@codeDirectoryWork"].Value	= (long)codeDirectoryWork;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			return true;
		}
		#endregion
	}
}
