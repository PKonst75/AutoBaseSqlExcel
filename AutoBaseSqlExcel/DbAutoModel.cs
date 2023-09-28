using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAutoModel.
	/// </summary>
	public class DbAutoModel:Db
	{
		// �������� ��������������
		private long code;				// ��� ������
		private string model;			// ������
		private string engine;			// ���������
		private long codeAutoType;		// ��� ��������������� ���� ����������
		private long codeGuarantyType;	// ��� ��������������� ���� ����������

		// ���������
		private DbAutoType		tmpAutoType;		// ��� ����������
		private DbGuarantyType	tmpGuarantyType;	// ��� ��������

		// ����� � ����� ������
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSysDelete;
		private static SqlCommand cmdSelectModelBrand;
		private static SqlCommand cmdFind;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 4 ����������� ���� � ���������
			readerLength = 5 + DbAutoType.ReaderLength + DbGuarantyType.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_����������_������", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@model", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@engine", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeGuarantyType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_����������_������", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectModelBrand = new SqlCommand("SELECT_����������_������_����������_�����", conn);
			cmdSelectModelBrand.Parameters.Add("@codeBrand", SqlDbType.BigInt);
			cmdSelectModelBrand.CommandType = CommandType.StoredProcedure;

			cmdSysDelete = new SqlCommand("SYSTEM_����������_������_�������", conn);
			cmdSysDelete.CommandType = CommandType.StoredProcedure;
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdSysDelete);

			cmdFind = new SqlCommand("SELECT_����������_������", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);
		}

		public DbAutoModel()
		{
			code				= 0;
			model				= "";
			engine				= "";
			codeAutoType		= 0;
			codeGuarantyType	= 0;

			tmpAutoType			= null;
			tmpGuarantyType		= null;

			adding				= true;
		}
		public DbAutoModel(DbAutoModel source)
		{
			code				= source.code;
			model				= source.model;
			engine				= source.engine;
			codeAutoType		= source.codeAutoType;
			codeGuarantyType	= source.codeGuarantyType;

			tmpAutoType			= source.tmpAutoType;
			tmpGuarantyType		= source.tmpGuarantyType;
		}
		public DbAutoModel(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			model				= (string)GetValueString(reader, offset);	offset++;
			engine				= (string)GetValueString(reader, offset);	offset++;
			codeAutoType		= (long)GetValueLong(reader, offset);		offset++;
			codeGuarantyType	= (long)GetValueLong(reader, offset);		offset++;
			
			tmpAutoType			= new DbAutoType(reader, offset);			offset = offset + DbAutoType.ReaderLength;
			tmpGuarantyType		= new DbGuarantyType(reader, offset);		offset = offset + DbGuarantyType.ReaderLength;
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@model"].Value				= (string)model;
				cmdWrite.Parameters["@engine"].Value			= (string)engine;
				cmdWrite.Parameters["@codeAutoType"].Value		= (long)codeAutoType;
				cmdWrite.Parameters["@codeGuarantyType"].Value	= (long)codeGuarantyType;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				Db.ShowFaults();
				return false;
			}
			return true;
		}

		public static DbAutoModel Find(long code)
		{
			SqlDataReader reader = null;
			DbAutoModel model = null;
			try
			{

				cmdFind.Parameters["@code"].Value = code;
				reader = cmdFind.ExecuteReader();
				if(reader.Read())
					model = new DbAutoModel(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return model;
		}

		#region ������ � ������� ����������
		public string Model
		{
			get
			{
				return model;
			}
			set
			{
				model = SetStringNotEmptyLength(model, value, 60, "������ ����������");
			}
		}
		public string Engine
		{
			get
			{
				return engine;
			}
			set
			{
				engine = SetStringLength(engine, value, 60, "��������� ����������");
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		public DbAutoType AutoType
		{
			get
			{
				return tmpAutoType;
			}
			set
			{
				if(value == null) return;
				if(codeAutoType != value.Code)
				{
					changed = true;
					codeAutoType = value.Code;
					tmpAutoType = value;
				}
			}
		}
		public long CodeAutoType
        {
            get { return codeAutoType; }
        }
		public DbGuarantyType GuarantyType
		{
			get
			{
				return tmpGuarantyType;
			}
			set
			{
				if(value == null) return;
				if(codeGuarantyType != value.Code)
				{
					changed				= true;
					codeGuarantyType	= value.Code;
					tmpGuarantyType			= value;
				}
			}
		}
		#endregion

		#region �����������
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = model;
			item.SubItems[1].Text = engine;
			item.SubItems[2].Text = GuarantyTypeTxt;

			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbBrand brand)
		{
			cmdSelectModelBrand.Parameters["@codeBrand"].Value = brand.Code;
			Db.DbFillList(list, cmdSelectModelBrand, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoModel element = new DbAutoModel(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbAutoModel element = new DbAutoModel(reader, 0);
			array.Add(element);
		}
		#endregion

		#region ����������� � ����� ����������
		public string AutoTypeTxt
		{
			get
			{
				if(tmpAutoType == null) return "���������� ������� ��� ����������";
				if(codeAutoType == 0) return "���������� ������� ��� ����������";
				return tmpAutoType.NameTxt;
			}
		}
		public string GuarantyTypeTxt
		{
			get
			{
				if(tmpGuarantyType == null) return "";
				if(codeGuarantyType == 0) return "";
				return tmpGuarantyType.Description;
			}
		}
		#endregion

		#region ������� ��������� �������
		public bool Delete()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("�� ������� ��� ������ ������� ������ ����������?", "��������������", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}
		#endregion

		#region ���������������� ����������� ������
		override public string DbTitle()
		{
			return this.Model;
		}
		#endregion

	}
}
