using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbTimeWork.
	/// </summary>
	public class DbTimeWork:Db
	{
		// �������� ���������
		long	codeAutoType;		// ��� ���� ����������
		float[] times;				// ������ ������ �������� �����

		// ����� � ����� ������
		private static SqlConnection	conn;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdSelect;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}


		#region ������������
		public DbTimeWork(DbAutoType autoType)
		{
			codeAutoType	= (long)autoType.Code;
			times			= new float[22];
		}
		public DbTimeWork(SqlDataReader reader, int offset)
		{
			times			= new float[22];
			codeAutoType	= (long)GetValueLong(reader, 0 + offset);
			for(int i = 1; i <= 22; i++)
			{
				times[i-1]	= (float)GetValueFloat(reader, i + offset);
			}
		}
		#endregion

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 23 ����������� ����
			readerLength = 23;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_�����_������", conn);
			cmdWrite.CommandType	= CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			for(int i = 1; i <= 22; i++)
			{
				cmdWrite.Parameters.Add("@tm" + i.ToString(), SqlDbType.Real);
			}
			Db.SetReturnError(cmdWrite);

			cmdSelect = new SqlCommand("SELECT_�����_������", conn);
			cmdSelect.CommandType	= CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
		}
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		#endregion

		#region �������� ������
		public static DbTimeWork Read(DbAutoType autoType)
		{
			SqlDataReader reader = null;
			DbTimeWork timeWork = null;
			try
			{
				cmdSelect.Parameters["@codeAutoType"].Value	= (long)autoType.Code;
				reader = cmdSelect.ExecuteReader();
				if(reader.Read())
					timeWork = new DbTimeWork(reader, 0);
				else
					timeWork = new DbTimeWork(autoType);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				Db.ShowFaults();
				return null;
			}
			if(reader != null) reader.Close();
			return timeWork;
		}
		public bool Write()
		{
			// ���� ������ ���������, � ���� ������ �� ����������
			if((!adding)&&(!changed)) return true;

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@codeAutoType"].Value	= (long)codeAutoType;
				for(int i = 1; i <= 22; i++)
				{
					cmdWrite.Parameters["@tm" + i.ToString()].Value = (float)times[i - 1];
				}
				
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
			}
			catch(Exception E)
			{
				if(trans != null)trans.Rollback();
				SetTransaction(null);
				SetException(E);
				ShowFaults();
				return false;
			}
			if(trans != null)trans.Commit();
			SetTransaction(null);
			MessageBox.Show("�������� ������� �����");
			return true;
		}
		public void FillList(ListView list)
		{
			for(int i = 0; i < 22; i++)
			{
				list.Items[i].SubItems.Add(this.times[i].ToString());
			}
		}
		#endregion

		public string TimeTxt(int i)
		{
			if((i<0)||(i>=22)) return  "";
			return times[i].ToString();
		}
		public void SetTime(int i, float value)
		{
			if(value <= 0) return;
			if((i<0)||(i>=22)) return;
			if(times[i] == value) return;
			times[i] = value;
			changed = true;
		}
		public void SetList(ListView list, int i)
		{
			list.Items[i].SubItems[1].Text = times[i].ToString();
		}
		public float ApplyTimes(int works)
		{
			float result = 0;
			for(int i = 0; i < 22; i++)
			{
				if((works & (1 << i)) > 0) result += times[i];
			}
			return result;
		}
	}
}
