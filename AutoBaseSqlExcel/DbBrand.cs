using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbBrand.
	/// </summary>
	public class DbBrand:Db
	{
		private long		code;
		private string		name;

		// ����� � ����� ������
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdWriteBrandModel;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �������������
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
			cmdWriteBrandModel.Transaction	= trans;
		}
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 2 ����������� �����
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_����������_�����", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_����������_�����", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdWriteBrandModel = new SqlCommand("WRITE_����������_�����_����������_������", conn);
			cmdWriteBrandModel.CommandType = CommandType.StoredProcedure;
			cmdWriteBrandModel.Parameters.Add("@codeBrand", SqlDbType.BigInt);
			cmdWriteBrandModel.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdWriteBrandModel.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWriteBrandModel);
		}
		#endregion

		#region ������������
		public DbBrand()
		{
			code					= 0;
			name					= "";

			adding					= true;
		}
		public DbBrand(DbBrand src)
		{
			code					= src.code;
			name					= src.name;
		}
		public DbBrand(SqlDataReader reader, int offset)
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
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		public bool WriteModel(DbAutoModel model, bool add)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWriteBrandModel.Parameters["@adding"].Value		= (bool)add;
				cmdWriteBrandModel.Parameters["@codeBrand"].Value	= (long)code;
				cmdWriteBrandModel.Parameters["@codeModel"].Value	= (long)model.Code;
				cmdWriteBrandModel.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteBrandModel);
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
			DbBrand element = new DbBrand(reader, 0);
			ListViewItem	item = list.Items.Add(element.LVItem);
			// ������������� �������
			try
			{
				string text						= ".\\Brand\\" + element.Name + ".bmp";
				System.Drawing.Bitmap image		= new System.Drawing.Bitmap(text);
				int index						= list.LargeImageList.Images.Add(image, System.Drawing.Color.White);
				item.ImageIndex					= index;
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				item.ImageIndex	= 0;
			}
		}

		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbBrand element = new DbBrand(reader, 0);
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
