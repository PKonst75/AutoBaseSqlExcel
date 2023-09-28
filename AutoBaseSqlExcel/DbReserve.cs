using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbReserve.
	/// </summary>
	public class DbReserve:Db
	{
		private static SqlConnection conn;
		private static SqlCommand cmdExecWrite;

		private string codeDetail;
		private string codeFirm;
		private short typeDoc;
		private long numberDoc;
		private int yearDoc;
		private long numberPos;
		private float quontity;
		private int reserve;
		private DateTime date;

		private bool tmpDelete;

		public static void Init(SqlConnection connection)
		{
			conn = connection;

			cmdExecWrite = new SqlCommand("WRITE_RESERVE", conn);
			cmdExecWrite.CommandType = CommandType.StoredProcedure;
			cmdExecWrite.Parameters.Add("@codeDetail", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@codeFirm", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@typeDoc", SqlDbType.SmallInt);
			cmdExecWrite.Parameters.Add("@numberDoc", SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@yearDoc", SqlDbType.Int);
			cmdExecWrite.Parameters.Add("@numberPos", SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@quontity", SqlDbType.Real);
			cmdExecWrite.Parameters.Add("@reserve", SqlDbType.Int);
			cmdExecWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdExecWrite.Parameters.Add("@delete", SqlDbType.Bit);
			Db.SetReturnError(cmdExecWrite);
			cmdExecWrite.Parameters["@reserve"].Direction = ParameterDirection.InputOutput;
		}

		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				cmdExecWrite.Parameters["@codeDetail"].Value = (string)codeDetail;
				cmdExecWrite.Parameters["@codeFirm"].Value = (string)codeFirm;
				cmdExecWrite.Parameters["@typeDoc"].Value = (short)typeDoc;
				cmdExecWrite.Parameters["@numberDoc"].Value = (long)numberDoc;
				cmdExecWrite.Parameters["@yearDoc"].Value = (int)yearDoc;
				cmdExecWrite.Parameters["@numberPos"].Value = (long)numberPos;
				cmdExecWrite.Parameters["@quontity"].Value = (float)quontity;
				cmdExecWrite.Parameters["@reserve"].Value = (int)reserve;
				cmdExecWrite.Parameters["@adding"].Value = (bool)adding;
				cmdExecWrite.Parameters["@delete"].Value = (bool)tmpDelete;
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				cmdExecWrite.Transaction = trans;
				cmdExecWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdExecWrite);
				trans.Commit();
				cmdExecWrite.Transaction = null;
				reserve = (int)cmdExecWrite.Parameters["@reserve"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetException(E);
				return false;
			}
			return true;
		}

		public DbReserve(DbCardDetail element)
		{
	//		codeDetail = element.CodeDetail;
	//		codeFirm = element.CodeFirm;
			typeDoc = 1;
			numberDoc = element.CardNumber;
			yearDoc = element.CardYear;
			numberPos = element.Number;
			quontity = element.Quontity;

			adding = false;
			tmpDelete = false;
		}

		public void Add()
		{
			adding = true;
			tmpDelete = false;
		}

		public void Delete()
		{
			tmpDelete = true;
			adding = false;
		}

		public int Reserve
		{
			get
			{
				return reserve;
			}
		}
	}
}
