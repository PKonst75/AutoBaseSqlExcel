using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAutoStorage.
	/// </summary>
	public class DbAutoStorage:Db
	{
		private long		code;
		private long		codeAutoIncom;
		private long		codeAuto;
		private long		reserv;
		private string		comment;
		private bool		preparation;
		private float		price;
		private long		codeSell;

		private DbAutoIncom	tmpAutoIncom;
		private DateTime	tmpDateIncom;
		private float		tmpOptionPrice;
		private float		tmpOptionPricePlan;
		

		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdWritePreparation;
		private static SqlCommand cmdWriteReserve;
		private static SqlCommand cmdWritePrice;
		private static SqlCommand cmdWriteOutcom;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectFind;
		private static SqlCommand cmdSelectSearchBody;
		private static SqlCommand cmdActionPreparation;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 6 ����������� ����� � ���������
			readerLength = 8 + 2 + DbAutoIncom.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_����������_�����", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoIncom", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@reserv", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@dateIncom", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@price", SqlDbType.Float);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWritePreparation = new SqlCommand("WRITE_����������_�����_���", conn);
			cmdWritePreparation.CommandType = CommandType.StoredProcedure;
			cmdWritePreparation.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWritePreparation.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWritePreparation.Parameters.Add("@year", SqlDbType.BigInt);
			Db.SetReturnError(cmdWritePreparation);

			cmdWriteReserve = new SqlCommand("WRITE_����������_�����_������", conn);
			cmdWriteReserve.CommandType = CommandType.StoredProcedure;
			cmdWriteReserve.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteReserve.Parameters.Add("@codePartner", SqlDbType.BigInt);
			Db.SetReturnError(cmdWriteReserve);

			cmdWritePrice = new SqlCommand("WRITE_����������_�����_����", conn);
			cmdWritePrice.CommandType = CommandType.StoredProcedure;
			cmdWritePrice.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWritePrice.Parameters.Add("@price", SqlDbType.Real);
			Db.SetReturnError(cmdWritePrice);

			cmdWriteOutcom = new SqlCommand("WRITE_����������_�����_����", conn);
			cmdWriteOutcom.CommandType = CommandType.StoredProcedure;
			cmdWriteOutcom.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteOutcom.Parameters.Add("@dateOutcom", SqlDbType.DateTime);
			Db.SetReturnError(cmdWriteOutcom);

			cmdSelect = new SqlCommand("SELECT_����������_�����", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectFind = new SqlCommand("SELECT_����������_�����_�����", conn);
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;

			cmdSelectSearchBody = new SqlCommand("SELECT_����������_�����_�����_�����", conn);
			cmdSelectSearchBody.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelectSearchBody.CommandType = CommandType.StoredProcedure;

			cmdActionPreparation = new SqlCommand("ACTION_����������_�����_���", conn);
			cmdActionPreparation.CommandType = CommandType.StoredProcedure;
			Db.SetReturnError(cmdActionPreparation);
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
			cmdWriteOutcom.Transaction	= trans;
		}
		#endregion

		#region ������������
		public DbAutoStorage(DbAutoIncom autoIncom, DateTime dateIncom)
		{
			code				= 0;
			codeAutoIncom		= autoIncom.Code;
			codeAuto			= autoIncom.Auto.Code;
			reserv				= 0;
			comment				= "";
			preparation			= false;
			price				= 0.0f;
			codeSell			= 0;

			tmpAutoIncom		= autoIncom;
			tmpDateIncom		= dateIncom;
			tmpOptionPrice		= 0.0f;
			tmpOptionPricePlan	= 0.0f;

			adding = true;
		}

		public DbAutoStorage(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeAutoIncom		= (long)GetValueLong(reader, offset);		offset++;
			codeAuto			= (long)GetValueLong(reader, offset);		offset++;
			reserv				= (long)GetValueLong(reader, offset);		offset++;
			comment				= (string)GetValueString(reader, offset);	offset++;
			preparation			= (bool)GetValueBool(reader, offset);		offset++;
			price				= (float)GetValueFloat(reader, offset);		offset++;
			codeSell			= (long)GetValueLong(reader, offset);		offset++;
			tmpOptionPrice		= (float)GetValueFloat(reader, offset);		offset++;
			tmpOptionPricePlan	= (float)GetValueFloat(reader, offset);		offset++;
			
			tmpAutoIncom		= new DbAutoIncom(reader, offset);				offset += DbAutoIncom.ReaderLength;

			tmpDateIncom		= tmpAutoIncom.DeliveryDate;
			adding				= false;
		}

		public DbAutoStorage(DbAutoStorage src)
		{
			code				= src.code;
			codeAutoIncom		= src.codeAutoIncom;
			codeAuto			= src.codeAuto;
			reserv				= src.reserv;
			comment				= src.comment;
			preparation			= src.preparation;
			price				= src.price;
			codeSell			= src.codeSell;
			
			tmpAutoIncom		= src.tmpAutoIncom;
			tmpOptionPrice		= src.tmpOptionPrice;
			tmpOptionPricePlan	= src.tmpOptionPricePlan;

			adding			= false;
		}
		#endregion

		#region �������� ������
		public bool Write()
		{
			SqlTransaction trans = null;
			if((adding!=true)&&(changed!=true)) return true;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@codeAutoIncom"].Value = (long)codeAutoIncom;
				cmdWrite.Parameters["@codeAuto"].Value		= (long)codeAuto;
				cmdWrite.Parameters["@reserv"].Value		= (long)reserv;
				cmdWrite.Parameters["@comment"].Value		= (string)comment;
				cmdWrite.Parameters["@dateIncom"].Value		= (DateTime)tmpDateIncom;
				cmdWrite.Parameters["@price"].Value			= (float)price;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				trans.Rollback();
				SetTransaction(null);
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			return true;
		}
		public bool WritePreparation(DbCard card)
		{
			try
			{
				cmdWritePreparation.Parameters["@code"].Value		= (long)code;
				cmdWritePreparation.Parameters["@number"].Value		= (long)card.Number;
				cmdWritePreparation.Parameters["@year"].Value		= (int)card.Year;
				cmdWritePreparation.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWritePreparation);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			this.preparation = true;
			return true;
		}
		public bool WriteReserve(DbPartner partner)
		{
			long partnerCode;
			if(partner == null) partnerCode = 0;
			else partnerCode = partner.Code;
			try
			{
				cmdWriteReserve.Parameters["@code"].Value			= (long)code;
				cmdWriteReserve.Parameters["@codePartner"].Value	= (long)partnerCode;
				cmdWriteReserve.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteReserve);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			this.reserv = partnerCode;
			return true;
		}
		public bool WritePrice(float newPrice)
		{
			try
			{
				cmdWritePrice.Parameters["@code"].Value			= (long)code;
				cmdWritePrice.Parameters["@price"].Value		= (float)newPrice;
				cmdWritePrice.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWritePrice);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			this.price = newPrice;
			return true;
		}
		public bool WriteOutcom(DateTime date)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);
				cmdWriteOutcom.Parameters["@code"].Value			= (long)code;
				cmdWriteOutcom.Parameters["@dateOutcom"].Value		= (DateTime)date;
				cmdWriteOutcom.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteOutcom);
			}
			catch(Exception E)
			{
				if (trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				ShowFaults();
				return false;
			}
			if (trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		public static DbAutoStorage Find(long code)
		{
			SqlDataReader reader = null;
			DbAutoStorage autoStorage = null;
			try
			{

				cmdSelectFind.Parameters["@code"].Value = code;
				reader = cmdSelectFind.ExecuteReader();
				if(reader.Read())
					autoStorage = new DbAutoStorage(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return autoStorage;
		}
		public static void ActionPraparation()
		{
			Db.ExecuteCommandError(cmdActionPreparation);
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
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			item.Text = IncomDateTxt;
			item.SubItems[1].Text = AutoModelTxt;
			item.SubItems[2].Text = AutoSubModelTxt;
			item.SubItems[3].Text = AutoComplectTxt;
			item.SubItems[4].Text = AutoColorTxt;
			item.SubItems[5].Text = VinTxt;
			item.SubItems[6].Text = PriceTxt;
			item.SubItems[7].Text = OptionPriceTxt;
			item.SubItems[8].Text = comment;
			item.BackColor = Color.White;
			if(this.reserv > 0)
			{
				item.BackColor = Color.Blue;
				item.SubItems[8].Text += " !����������������";
			}
			if(codeSell > 0)
			{
				item.BackColor	= Color.Green;
			}
			if(tmpOptionPricePlan > 0)
			{
				item.BackColor = Color.Yellow;
			}
			if(preparation == false)
			{
				item.BackColor = Color.Red;
			}
			if(this.AutoIncom.Incom == false)
			{
				item.BackColor = Color.Gray;
			}
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillListSearchBody(ListView list, string pattern)
		{
			cmdSelectSearchBody.Parameters["@pattern"].Value = pattern;
			Db.DbFillList(list, cmdSelectSearchBody, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoStorage element = new DbAutoStorage(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion


		#region ����������� ���������� � �����
		public string IncomDateTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.DateTxt;
			}
		}
		public string PriceTxt
		{
			get
			{
				return Db.CachToTxt(price);
			}
		}
		public string OptionPriceTxt
		{
			get
			{
				return Db.CachToTxt(tmpOptionPrice + tmpOptionPricePlan);
			}
		}

		public string AutoModelTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoModelTxt;
			}
		}
		public string AutoSubModelTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoSubModelTxt;
			}
		}
		public string AutoComplectTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoComplectTxt;
			}
		}
		public string AutoColorTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoColorsTxt;
			}
		}
		public string VinTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.VinNoTxt;
			}
		}
		#endregion

		#region ������ � ���������� - ������
		public DbAuto Auto
		{
			get
			{
				if(tmpAutoIncom == null) return null;
				return tmpAutoIncom.Auto;
			}
		}
		public DbAutoIncom AutoIncom
		{
			get
			{
				return tmpAutoIncom;
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		public long Reserv
		{
			get
			{
				return this.reserv;
			}
		}
		public float PriceOption
		{
			get
			{
				return this.tmpOptionPrice + this.tmpOptionPricePlan;
			}
		}
		#endregion

		#region ������ � ���������� - ���������
		public float Price
		{
			get
			{
				return price;
			}
			set
			{
				if(value <= 0) return;
				if(value == price) return;
				price = value;
				changed = true;
			}
		}
		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = SetStringLength(comment, value, 256, "����������");
			}
		}
		#endregion

		
		#region ����������� ����������� �������
		public override string[] Inform(int infoLevel)
		{
			string[] infoStrings	= null;
			string txt				= "";
			ArrayList optionArray	= new ArrayList();
			int	i					= 0;

			DbOptionAuto.FillArrayNotRemoved(optionArray, this.Auto);

			switch (infoLevel)
			{
				default:				
					infoStrings = new string[10 + optionArray.Count];
					infoStrings[0] = "����������";
					infoStrings[1] = "������ :\t" + this.AutoModelTxt;
					infoStrings[2] = "������� :\t\t" + this.AutoSubModelTxt;
					infoStrings[3] = "������������ :\t\t\t" + this.AutoComplectTxt;
					infoStrings[4] = "���� :\t\t\t" + this.AutoColorTxt;
					infoStrings[5] = "";
					// ���������
					txt = tmpAutoIncom.DateTxt;
					infoStrings[6] = "������� :\t\t\t" + txt;
					// �������� �����-������ � ���
					if(this.preparation == true)
					{
						ArrayList array = new ArrayList();
						DbCard.FillArrayPreparation(array, this.Auto);
						if (array.Count > 0)
						{
							DbCard card = (DbCard)array[0];
							txt = "��������� �� �����-������ " + card.WarrantNumberTxt;
						}
						else
						{
							txt = "���������";
						}
					}
					else
					{
						txt = "�������������";
					}
					infoStrings[7] = "������������� ���������� :\t\t" + txt;
					// �����
					infoStrings[8] = "�������������� ������������";
					for (i = 9; i < 9 + optionArray.Count; i++)
					{
						DbOptionAuto optionAuto = (DbOptionAuto)optionArray[i-9];
						txt = optionAuto.NameTxt + "  " + optionAuto.PriceTxt;
						if(optionAuto.CardNumber != 0)
						{
							txt += " (����������� �� ��������" + optionAuto.CardNumber.ToString() + "/" + optionAuto.CardYear.ToString() + ")";
						}
						infoStrings[i] = "\t" + txt;
					}
					// ��������������
					if (this.reserv == 0)
					{
						infoStrings[i] = "������������������";
					}
					else
					{
						DbPartner partner = DbPartner.Find(this.reserv);
						if(partner != null)
							infoStrings[i] = "���������������� : \t\t\t" + partner.Title + " " + partner.ContactTxt;
						else
							infoStrings[i] = "���������������� : \t\t\t" + "??????";
					}
					break;
			}
			return infoStrings;
		}
		#endregion
	}
}
