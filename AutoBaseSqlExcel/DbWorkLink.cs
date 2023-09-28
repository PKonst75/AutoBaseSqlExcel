using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbWorkLink.
	/// </summary>
	public class DbWorkLink:Db
	{
		private long	codeWork;
		private long	codeWorkLink;
		
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;

		private static int readerLength;			//  оличество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}


		public DbWorkLink(DbWork work, DbWork workLink, bool add)
		{
			codeWork		= work.Code;
			codeWorkLink	= workLink.Code;
			adding			= add;
		}

		public static void Init(SqlConnection connection)
		{
			string cmdText;
			conn = connection;

			cmdWrite	= new SqlCommand("WRITE_“–”ƒќ≈ћ ќ—“№_—ќѕ”“—¬”ёўјя", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@codeWork", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeWorkLink", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@codeWork"].Value		= (long)codeWork;
				cmdWrite.Parameters["@codeWorkLink"].Value	= (long)codeWorkLink;
				cmdWrite.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
	}
}
