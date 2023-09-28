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

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbWorkGroupDirectoryWork(DbWorkGroup workGroupSrc, DbDirectoryWork directoryWorkSrc, bool add)
		{
			codeWorkGroup			= workGroupSrc.Code;
			codeDirectoryWork		= directoryWorkSrc.Code;

			adding					= add;
		}
		#endregion

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 2 ����������� �����
			readerLength = 2;

			conn = connection;

			cmdWrite				= new SqlCommand("WRITE_�������_������������_����������_������������", conn);
			cmdWrite.CommandType	= CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codeWorkGroup", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDirectoryWork", SqlDbType.BigInt);
			SetReturnError(cmdWrite);
		}
		#endregion

		#region �������� ������
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
