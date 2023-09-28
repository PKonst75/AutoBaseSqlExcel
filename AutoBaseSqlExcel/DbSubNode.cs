using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// �������
	/// </summary>
	public class DbSubNode:Db
	{
		private long		code;
		private long		codeNode;
		private string		name;
		private string		implement;

		// ����� � ����� ������
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectSubNodeSubModel;

		// ������ �������������� ������
		private static SqlCommand cmdWriteSubNodeDetail;
		private static SqlCommand cmdWriteSubNodeSubModel;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �������������
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
			cmdWriteSubNodeDetail.Transaction = trans;
			cmdWriteSubNodeSubModel.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 4 ����������� �����
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_�������", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeNode", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@implement", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_�������", conn);
			cmdSelect.Parameters.Add("@codeNode", SqlDbType.BigInt);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectSubNodeSubModel = new SqlCommand("SELECT_�������_���������", conn);
			cmdSelectSubNodeSubModel.Parameters.Add("@codeNode", SqlDbType.BigInt);
			cmdSelectSubNodeSubModel.Parameters.Add("@codeSubModel", SqlDbType.BigInt);
			cmdSelectSubNodeSubModel.CommandType = CommandType.StoredProcedure;

			cmdWriteSubNodeDetail = new SqlCommand("WRITE_�������_������", conn);
			cmdWriteSubNodeDetail.CommandType = CommandType.StoredProcedure;
			cmdWriteSubNodeDetail.Parameters.Add("@codeSubNode", SqlDbType.BigInt);
			cmdWriteSubNodeDetail.Parameters.Add("@codeDetail", SqlDbType.BigInt);
			cmdWriteSubNodeDetail.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWriteSubNodeDetail);

			cmdWriteSubNodeSubModel = new SqlCommand("WRITE_�������_����������_���������", conn);
			cmdWriteSubNodeSubModel.CommandType = CommandType.StoredProcedure;
			cmdWriteSubNodeSubModel.Parameters.Add("@codeSubNode", SqlDbType.BigInt);
			cmdWriteSubNodeSubModel.Parameters.Add("@codeSubModel", SqlDbType.BigInt);
			cmdWriteSubNodeSubModel.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWriteSubNodeSubModel);
		}
		#endregion

		#region ������������
		public DbSubNode(DbNode src)
		{
			code					= 0;
			codeNode				= src.Code;
			name					= "";
			implement				= "";

			adding					= true;
		}
		public DbSubNode(DbSubNode src)
		{
			code					= src.code;
			codeNode				= src.codeNode;
			name					= src.name;
			implement				= src.implement;
		}
		public DbSubNode(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeNode				= (long)GetValueLong(reader, offset);		offset++;
			name					= (string)GetValueString(reader, offset);	offset++;
			implement				= (string)GetValueString(reader, offset);	offset++;
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
				cmdWrite.Parameters["@codeNode"].Value		= (long)codeNode;
				cmdWrite.Parameters["@name"].Value			= (string)name;
				cmdWrite.Parameters["@implement"].Value		= (string)implement;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		public bool WriteDetail(DbDetail detail, bool add)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWriteSubNodeDetail.Parameters["@adding"].Value		= (bool)add;
				cmdWriteSubNodeDetail.Parameters["@codeSubNode"].Value	= (long)code;
				cmdWriteSubNodeDetail.Parameters["@codeDetail"].Value	= (long)detail.Code;
				cmdWriteSubNodeDetail.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteSubNodeDetail);
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		public bool WriteSubModel(DbAutoSubmodel subModel, bool add)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWriteSubNodeSubModel.Parameters["@adding"].Value			= (bool)add;
				cmdWriteSubNodeSubModel.Parameters["@codeSubNode"].Value	= (long)code;
				cmdWriteSubNodeSubModel.Parameters["@codeSubModel"].Value	= (long)subModel.Code;
				cmdWriteSubNodeSubModel.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteSubNodeSubModel);
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
				name = SetStringNotEmptyLength(name, value, 64, "������������ ����");
			}
		}
		public string Implement
		{
			get
			{
				return implement;
			}
			set
			{
				implement = SetStringNotEmptyLength(name, value, 128, "������������");
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

		public static void FillList(ListView list, DbNode parentNode)
		{
			cmdSelect.Parameters["@codeNode"].Value	= (long)parentNode.Code;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbNode parentNode, DbAutoSubmodel subModel)
		{
			if(subModel != null) cmdSelectSubNodeSubModel.Parameters["@codeSubModel"].Value	= (long)subModel.Code;
			else cmdSelectSubNodeSubModel.Parameters["@codeSubModel"].Value	= (long)0;
			cmdSelectSubNodeSubModel.Parameters["@codeNode"].Value	= (long)parentNode.Code;
			Db.DbFillList(list, cmdSelectSubNodeSubModel, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbSubNode element = new DbSubNode(reader, 0);
			ListViewItem	item = list.Items.Add(element.LVItem);
			// ������������� �������
			try
			{
				string text						= ".\\SubNodes\\" + element.Name + ".bmp";
				System.Drawing.Bitmap image		= new System.Drawing.Bitmap(text);
				int index						= list.LargeImageList.Images.Add(image, System.Drawing.Color.White);
				item.ImageIndex					= index;
			}
			catch(Exception E)
			{
				item.ImageIndex	= 0;
			}
		}

		public static void FillArray(ArrayList array, DbNode parentNode)
		{
			cmdSelect.Parameters["@codeNode"].Value	= (long)parentNode.Code;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbSubNode element = new DbSubNode(reader, 0);
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
