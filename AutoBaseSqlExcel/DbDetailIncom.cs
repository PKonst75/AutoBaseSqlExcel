using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailIncom.
	/// </summary>
	public class DbDetailIncom:Db
	{
		private long code;					// ��� ������� � ����������
		private long codeDetailStorage;		// ��� ��������� ������� ��� ������� � ����������
		private long codeDetailIncomDoc;	// ��� ����������
		private float quontity;				// ����������
		private float nds;					// ���
		private float price;				// ����� ���� (� ���)
		private float expens;				// ������������ � ������� �������

		private DbDetailStorage tmpDetailStorage;	// ��������� �������

		private bool tmpDeleted;
		private bool tmpExists;

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdExpensCheck;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbDetailIncom(DbDetailStorage detailStorage)
		{
			code				= 0;
			codeDetailStorage	= detailStorage.Code;
			codeDetailIncomDoc	= 0;
			quontity			= 1.0F;
			nds					= 18.0F;
			price				= 0.0F;
			expens				= 0.0F;

			tmpDetailStorage	= detailStorage;

			adding			= true;
			tmpExists		= false;
			tmpDeleted		= false;
		}

		public DbDetailIncom(DbDetailIncom srcDetailIncom)
		{
			code				= srcDetailIncom.code;
			codeDetailStorage	= srcDetailIncom.codeDetailStorage;
			codeDetailIncomDoc	= srcDetailIncom.codeDetailIncomDoc;
			quontity			= srcDetailIncom.quontity;
			nds					= srcDetailIncom.nds;
			price				= srcDetailIncom.price;
			expens				= srcDetailIncom.expens;

			tmpDetailStorage	= srcDetailIncom.tmpDetailStorage;

			tmpExists		= true;
			tmpDeleted		= false;
		}


		public DbDetailIncom(DbDetailIncomDetailed source)
		{
			/*
			year = source.Year;
			orderNumber = source.OrderNumber;
			number = source.Number;
			codeDetail = source.CodeDetail;
			codeFirm = source.CodeFirm;
			price = source.Price;
			nds = source.Nds;
			quontity = source.Quontity;

			tmpFirm = source.Firm;
			tmpDetail = source.Detail;
			*/
		}

		public DbDetailIncom(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);	offset++;
			codeDetailStorage	= (long)GetValueLong(reader, offset);	offset++;
			codeDetailIncomDoc	= (long)GetValueLong(reader, offset);	offset++;
			quontity			= (float)GetValueFloat(reader, offset);	offset++;
			nds					= (float)GetValueFloat(reader, offset);	offset++;
			price				= (float)GetValueFloat(reader, offset);	offset++;
			expens				= (float)GetValueFloat(reader, offset);	offset++;

			tmpDetailStorage	= new DbDetailStorage(reader, offset);	offset = offset + DbDetailStorage.ReaderLength;

			tmpExists		= true;
			tmpDeleted		= false;
		}
		#endregion

		#region �������������
		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 7 ����������� ����� � ���������
			readerLength = 7 + DbDetailStorage.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_�����_������_������");
			cmdWrite.Connection = conn;
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetailStorage", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetailIncomDoc", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@quontity", SqlDbType.Real);
			cmdWrite.Parameters.Add("@nds", SqlDbType.Real);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_�����_������_������");
			cmdSelect.Connection = conn;
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeDetailIncomDoc", SqlDbType.BigInt);

			cmdExpensCheck = new SqlCommand("REPORT_��������_�����_������_������_����������_������");
			cmdExpensCheck.Connection = conn;
			cmdExpensCheck.CommandType = CommandType.StoredProcedure;	
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
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems[1].Text = DetailCodeTxt;
			item.SubItems[2].Text = DetailNameTxt;
			item.SubItems[3].Text = QuontityTxt;
			item.SubItems[4].Text = PriceNoNdsTxt;
			item.SubItems[5].Text = NdsTxt;
			item.SubItems[6].Text = PriceTxt;
			item.SubItems[7].Text = SummTxt;
			item.BackColor = System.Drawing.Color.White;
			if(tmpDeleted == true)
			{
				item.BackColor = System.Drawing.Color.LightGray;
			}
			if(tmpDetailStorage != null && tmpDetailStorage.State==true)
			{
				item.ForeColor = System.Drawing.Color.Red;
			}
			item.Tag = this;
		}

		public static void FillList(ListView list, DbDetailIncomDoc debitDetailDoc)
		{
			cmdSelect.Parameters["@codeDetailincomDoc"].Value = debitDetailDoc.Code;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDetailIncom element = new DbDetailIncom(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region ������ � �������� ���������� - ������ ������
		public bool Exists
		{
			get
			{
				return tmpExists;
			}
		}
		public float Price
		{
			get
			{
				return price;
			}
		}
		public float PriceRecomend
		{
			get
			{
				if(tmpDetailStorage == null)
					return price;
				if(tmpDetailStorage.StorageGroup == null)
					return price;
				return price + price * tmpDetailStorage.StorageGroup.Charge / 100.0F;
			}
		}
		public float Nds
		{
			get
			{
				return nds;
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		public long CodeDetailIncomDoc
		{
			get
			{
				return codeDetailIncomDoc;
			}
		}
		public DbDetailStorage DetailStorage
		{
			get
			{
				return tmpDetailStorage;
			}
		}
		public float Expens
		{
			get
			{
				return expens;
			}
		}
		public long CodeDetailStorage
		{
			get
			{
				return codeDetailStorage;
			}
		}
		public float Summ
		{
			get
			{
				float summ = quontity * price;
				summ = MakeFloat(summ);
				return summ;
			}
		}
		#endregion

		#region ����������� �������� ���������� � �����
		public string DetailNameTxt
		{
			get
			{
				if(tmpDetailStorage == null) return "";
				return tmpDetailStorage.DetailName;
			}
		}
		public string DetailCodeTxt
		{
			get
			{
				if(tmpDetailStorage == null) return "";
				return tmpDetailStorage.DetailCodeTxt;
			}
		}
		public string SummTxt
		{
			get
			{
				return Db.CachToTxt(Summ);
			}
		}
		#endregion

		#region ������ � �������� ���������� - ���������
		public float Quontity
		{
			get
			{
				return quontity;
			}
			set
			{
				if(value == quontity) return;
				if(value <= 0) return;
				quontity = value;
				changed = true;
			}
		}
		public string PriceNoNdsTxt
		{
			get
			{
				float priceNoNds = price * 100.0F / (100.0F + nds);
				priceNoNds	= MakeFloat(priceNoNds);
				return  Db.CachToTxt(priceNoNds);
			}
			set
			{
				float priceNoNds = price * 100.0F / (100.0F + nds);
				priceNoNds = this.SetFloatNotMinus(priceNoNds, value, "����");
				price	= priceNoNds + nds * priceNoNds / 100.0F;
			}
		}
		public bool Del
		{
			get
			{
				return tmpDeleted;
			}
			set
			{
				if(tmpDeleted == value) return;
				tmpDeleted = value;
				changed = true;
			}
		}
		public string NdsTxt
		{
			set
			{
				nds = this.SetFloatNotMinus(nds, value, "���");
			}
			get
			{
				return nds.ToString();
			}
		}
		public string PriceTxt
		{
			set
			{
				price = this.SetFloatNotMinus(price, value, "���� c ���");
			}
			get
			{
				return Db.CachToTxt(price);
			}
		}
		public string QuontityTxt
		{
			set
			{
				quontity = this.SetFloatNotMinus(quontity, value, "����������");
			}
			get
			{
				return quontity.ToString();
			}
		}
		#endregion

		#region �������� ������
		public void SetDocument(DbDetailIncomDoc document)
		{
			codeDetailIncomDoc = document.Code;
		}

		public void IsValid()
		{
			if(price < 0.0F) SetDataWarning("���� ������ ���� ���������������");
			if((nds < 0.0F)||(nds > 100.0F)) SetDataWarning("��� ������ ���� � ��������� �� 0 �� 100%");
			if(quontity <= 0.0F) SetDataWarning("���������� ������ ���� �������������");
		}

		public static bool UpdateList(ListView list, DbDetailIncomDoc document, SqlTransaction trans)
		{
			// ������ �������� ���������� ������������ ���������
			if(document.Implement == true)
			{
				Db.SetErrorMessage("�������� ��������");
				return true;
			}
			try
			{
				DbDetailIncom detailIncom;
				DbDetailIncom.SetTransaction(trans);
				foreach(ListViewItem item in list.Items)
				{
					detailIncom = (DbDetailIncom)item.Tag;
					if(detailIncom != null)
					{
						detailIncom.SetDocument(document);
						detailIncom.Write();
					}
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return true;
		}

		private bool Write()
		{
			// ���� �� ��������� ����� � ������ ��������� - � ���� ������ �� �����
			if((changed == false)&&(adding == false)) return true;
			try
			{
				cmdWrite.Parameters["@delete"].Value			= (bool)tmpDeleted;
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeDetailStorage"].Value = (long)codeDetailStorage;
				cmdWrite.Parameters["@codeDetailIncomDoc"].Value= (long)codeDetailIncomDoc;
				cmdWrite.Parameters["@quontity"].Value			= (float)quontity;
				cmdWrite.Parameters["@nds"].Value				= (float)nds;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.ExecuteNonQuery();
				ThrowReturnError(cmdWrite);
				code		= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public static void ExpensCheck()
		{
			DialogResult res = MessageBox.Show("�������� ������ ���������� �����. ����������?", "��������������", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return;
			Db.ExecuteCommand(cmdExpensCheck);
		}
		#endregion
	}
}
