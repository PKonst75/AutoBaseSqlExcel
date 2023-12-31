using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCreditBank.
	/// </summary>
	public class DbCreditBank:Db
	{
		private long		code;
		private string		name;

		// ����� � ����� ������
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �������������
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 2 ����������� �����
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_������_����", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_������_����", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region ������������
		public DbCreditBank()
		{
			code					= 0;
			name					= "";

			adding					= true;
		}
		public DbCreditBank(DbCreditBank src)
		{
			code					= src.code;
			name					= src.name;
		}
		public DbCreditBank(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			name					= (string)GetValueString(reader, offset);	offset++;
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
				
				cmdWrite.Parameters["@adding"].Value	= (bool)adding;
				cmdWrite.Parameters["@code"].Value		= (long)code;
				cmdWrite.Parameters["@name"].Value		= (string)name;
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
			return true;
		}
		#endregion

		#region ������ � �������� ���������� - ���������
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = SetStringNotEmptyLength(name, value, 64, "������������ ������");
			}
		}
		#endregion

		#region ����������� � ����� �������� ����������
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
			item.ImageIndex = 0;
			item.Text = name;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCreditBank element = new DbCreditBank(reader, 0);
			ListViewItem	item = list.Items.Add(element.LVItem);
		}

		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCreditBank element = new DbCreditBank(reader, 0);
			array.Add(element);
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

		#region ��������������� ����������� �������
		public override string DbTitle()
		{
			return this.name;
		}
		#endregion
	}
}
