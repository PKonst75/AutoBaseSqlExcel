using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailIncomDoc.
	/// </summary>
	public class DbDetailIncomDoc:Db
	{
		private long code;					// ���������� ��� ��������� � ����
		private long number;				// ���������� ����� ���������
		private DateTime date;				// ���� ���������
		private string document;			// �������� ���������
		private long codeStaff;				// ��� ���������� ���������� �����
		private bool implement;				// �������� ��� ��� ��������
		private long codePartner;			// ��� �����������

		private float tmpSumm;				// ����� �������
		DbPartner tmpPartner;				// ����������-���������
		private DbStaff tmpStaff;			// ������

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdDelete;
		private static SqlCommand cmdImplement;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdFind;

		private static SqlCommand cmdExecAdd;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbDetailIncomDoc()
		{
			code		= 0;
			number		= 0;
			date		= DateTime.Now;
			document	= "";
			codeStaff	= 0;
			implement	= false;
			codePartner = 0;

			tmpPartner	= null;
			tmpStaff	= null;
			tmpSumm		= 0.0F;

			adding		= true;
		}
		public DbDetailIncomDoc(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			number		= (long)GetValueLong(reader, offset);		offset++;
			date		= (DateTime)GetValueDate(reader, offset);	offset++;
			document	= (string)GetValueString(reader, offset);	offset++;
			codeStaff	= (long)GetValueLong(reader, offset);		offset++;
			implement	= (bool)this.GetValueBool(reader, offset);	offset++;
			codePartner = (long)GetValueLong(reader, offset);		offset++;

			tmpPartner	= new DbPartner(reader, offset);			offset = offset + DbPartner.ReaderLength;
			tmpStaff	= new DbStaff(reader, offset);				offset = offset + DbStaff.ReaderLength;
			tmpSumm		= this.GetValueFloat(reader, offset);		offset++;
		}

		public DbDetailIncomDoc(DbDetailIncomDoc srcDoc)
		{
			code		= srcDoc.code;
			number		= srcDoc.number;
			date		= srcDoc.date;
			document	= srcDoc.document;
			codeStaff	= srcDoc.codeStaff;
			implement	= srcDoc.implement;
			codePartner = srcDoc.codePartner;
			
			tmpPartner	= srcDoc.tmpPartner;
			tmpStaff	= srcDoc.tmpStaff;
			tmpSumm		= srcDoc.tmpSumm;
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
			// 7 ����������� ����� � ���������
			readerLength = 7 + DbPartner.ReaderLength + DbStaff.ReaderLength + 1;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_�����_������_������_��������", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@document", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeStaff", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@implement", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codePartner", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction=ParameterDirection.InputOutput;
			cmdWrite.Parameters["@number"].Direction=ParameterDirection.InputOutput;
			cmdWrite.Parameters["@date"].Direction=ParameterDirection.InputOutput;

			cmdSelect= new SqlCommand("SELECT_�����_������_������_��������");
			cmdSelect.Connection = conn;
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdImplement= new SqlCommand("WRITE_�����_������_������_��������_��������", conn);
			cmdImplement.CommandType = CommandType.StoredProcedure;
			cmdImplement.Parameters.Add("@code", SqlDbType.BigInt);
			cmdImplement.Parameters.Add("@implement", SqlDbType.Bit);
			SetReturnError(cmdImplement);

			cmdDelete = new SqlCommand("WRITE_�����_������_������_��������_�������", conn);
			cmdDelete.CommandType = CommandType.StoredProcedure;
			cmdDelete.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdDelete);

			cmdFind= new SqlCommand("FIND_�����_������_������_��������", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);

			cmdExecAdd = new SqlCommand("ADD_DEBIT", conn);
			cmdExecAdd.CommandType = CommandType.StoredProcedure;
			cmdExecAdd.Parameters.Add("@number", SqlDbType.BigInt);
			cmdExecAdd.Parameters.Add("@codeAccept", SqlDbType.BigInt);
			cmdExecAdd.Parameters.Add("@codeDeliver", SqlDbType.BigInt);
			cmdExecAdd.Parameters["@number"].Direction=ParameterDirection.InputOutput;
			cmdExecAdd.Parameters.Add("@year", SqlDbType.Int);
			cmdExecAdd.Parameters.Add("@adding", SqlDbType.Bit);
			cmdExecAdd.Parameters.Add("@date", SqlDbType.DateTime);
			cmdExecAdd.Parameters["@date"].Direction=ParameterDirection.InputOutput;
			cmdExecAdd.Parameters.Add("@codepartner", SqlDbType.VarChar);
			cmdExecAdd.Parameters.Add("@document", SqlDbType.VarChar);
			cmdExecAdd.Parameters.Add("@docType", SqlDbType.SmallInt);					// ��� ���������
			cmdExecAdd.Parameters.Add("@comment", SqlDbType.VarChar);					// ����������
			cmdExecAdd.Parameters.Add("@ERROR", SqlDbType.VarChar, 60);
			cmdExecAdd.Parameters["@ERROR"].Direction=ParameterDirection.Output;
			cmdExecAdd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
			cmdExecAdd.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
		}
		#endregion

		#region ������ � �������� ���������� - ���������
		public DateTime Date
		{
			get
			{
				return date;
			}
			set
			{
				if(date == value) return;
				changed = true;
				date = value;
			}
		}
		public bool Implement
		{
			get
			{
				return implement;
			}
			set
			{
				if(implement == value) return;
				implement = value;
				changed = true;
			}
		}
		public long Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}
		public DbPartner Partner
		{
			set
			{
				if(value == null) return;
				if(codePartner == value.Code) return;
				codePartner = value.Code;
				tmpPartner = value;
				changed = true;
			}
		}
		public DbStaff Staff
		{
			set
			{
				if(value == null) return;
				if(codeStaff == value.Code) return;
				codeStaff = value.Code;
				tmpStaff = value;
				changed = true;
			}
		}
		public string Document
		{
			get
			{
				return document;
			}
			set
			{
				document = this.SetStringNotEmptyLength(document, value, 120, "�������� ���������");
			}
		}
		public string NumberTxt
		{
			get{ return number.ToString();}
			set
			{
				if(adding == false) return;
				number = this.SetLongNotZero(number, value, "����� ���������� ������");
			}
		}
		#endregion

		#region ������ � �������� ���������� - ������ ������
		public long Code
		{
			get
			{
				return code;
			}
		}
		public float Summ
		{
			set
			{
				tmpSumm = value;
			}
		}
		#endregion

		#region ����������� �������� ���������� � �����
		public string PartnerName
		{
			get
			{
				if(tmpPartner == null) return "";
				return tmpPartner.Title;
			}
		}

		public string StaffTxt
		{
			get
			{
				if(tmpStaff == null) return "";
				return tmpStaff.FirstName;
			}
		}
		public string FullNumber
		{
			get
			{
				return NumberTxt + " �� " + DateToTxt(date);
			}
		}
		public string FullNumberTxt
		{
			get{ return number.ToString() + " �� " + Db.DateToTxt(date);}
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
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = FullNumberTxt;
			item.SubItems[1].Text = DateToTxt(date);
			item.SubItems[2].Text = CachToTxt(tmpSumm);
			item.SubItems[3].Text = PartnerName;
			item.SubItems[4].Text = document;
			item.BackColor = Color.White;
			if(implement == false)
			{
				// �������, ������������� ��������
				item.BackColor = Color.Yellow;
			}
			item.Tag = this;
		}
		public static void FillList(ListView list, SqlCommand cmd)
		{
			if(cmd == null) cmd = cmdSelect;
			Db.DbFillList(list, cmd, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDetailIncomDoc element = new DbDetailIncomDoc(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region �������� ������
		public void IsValid()
		{
			if(tmpStaff == null) SetDataWarning("�� �������� ��������� �����");
		}

		// ��������� ������/���������� ��������� � ��� ������
		// ���������, ��� ����� �����-�� ���������
		public bool Update(ListView list)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);
				UpdateDoc();
				DbDetailIncom.UpdateList(list, this, trans);
				if(ShowFaults())
				{
					if(trans != null)trans.Rollback();
					return false;
				}
				if(trans != null)trans.Commit();
			}
			catch(Exception E)
			{
				//if(trans != null)trans.Rollback();
				SetException(E);
				ShowFaults();
				return false;
			}	
			MessageBox.Show("������� ��������/�������� ��������� �����");
			return true;
		}

		public bool UpdateDoc()
		{
			// ���� ������ ���������, � ���� �� ����������
			if((changed == false)&&(adding == false)) return true;
			try
			{
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@number"].Value		= (long)number;
				cmdWrite.Parameters["@date"].Value			= (DateTime)date;
				cmdWrite.Parameters["@document"].Value		= (string)document;
				cmdWrite.Parameters["@codeStaff"].Value		= (long)codeStaff;
				cmdWrite.Parameters["@implement"].Value		= (bool)implement;
				cmdWrite.Parameters["@codePartner"].Value	= (long)codePartner;
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.ExecuteNonQuery();
				ThrowReturnError(cmdWrite);
				// ��������� ���������� �� ������� ������
				code = (long)cmdWrite.Parameters["@code"].Value;
				number = (long)cmdWrite.Parameters["@number"].Value;
				date = (DateTime)cmdWrite.Parameters["@date"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public bool Delete()
		{
			try
			{
				cmdDelete.Parameters["@code"].Value			= (long)code;
				cmdDelete.ExecuteNonQuery();
				ThrowReturnError(cmdDelete);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public bool MakeImplement(bool implement)
		{
			try
			{
				cmdImplement.Parameters["@code"].Value = (long)code;
				cmdImplement.Parameters["@implement"].Value = (bool)implement;
				cmdImplement.ExecuteNonQuery();
				Db.ThrowReturnError(cmdImplement);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			this.Implement = implement;
			return true;
		}

		public static DbDetailIncomDoc Find(long code)
		{
			SqlDataReader reader = null;
			DbDetailIncomDoc result = null;
			try
			{
				cmdFind.Parameters["@code"].Value = (long)code;
				reader = cmdFind.ExecuteReader();
				if(reader.Read())
				{
					result = new DbDetailIncomDoc(reader, 0);
					reader.Close();
					return result;
				}
				else
				{
					reader.Close();
					return null;
				}
			}
			catch(Exception E)
			{
				if(reader != null) reader.Close();
				SetException(E);
				return null;
			}
		}
		#endregion
	}
}
