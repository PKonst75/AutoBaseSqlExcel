using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ����� �������� ����� ������� �������
	/// </summary>
	public class DbTransmissionType:Db
	{
		private long code;
		private string description;

		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbTransmissionType()
		{
			code			= 0;
			description		= "";

			adding			= true;
		}
		public DbTransmissionType(DbTransmissionType src)
		{
			code			= src.code;
			description		= src.description;
		}
		public DbTransmissionType(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);			offset++;
			description		= (string)GetValueString(reader, offset);		offset++;
		}
		#endregion

		#region �������������
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 2 ����������� ����
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_���_���", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@description", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction=ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_���_���", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region �������� ������
		public bool Write()
		{
			if((adding == false)&&(changed == false)) return true; // ��������� ���

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@description"].Value	= (string)description;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			if(adding) MessageBox.Show("����� ��� ��� ��������");
			else
				if(changed) MessageBox.Show("��� ��� �������");
			return true;
		}
		#endregion

		#region �����������
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				SetLVItem(item);
				return item;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			item.Text	= this.description;
			item.Tag	= this;
		}
		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbTransmissionType element = new DbTransmissionType(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region ������ � �������� ���������� - ���������
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = this.SetStringNotEmptyLength(description, value, 64, "�������� ���� ��������");
			}
		}
		#endregion

		#region ������ � �������� ���������� - ������
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion
	}
}
