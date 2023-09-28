using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������, ����������� ��� ���������� � �����-������
	/// </summary>
	public class DbCard:Db
	{
		#region ����������� ������
		public class SearchConditions
		{
			DateTime	startDate;
			DateTime	endDate;
			long		codePartner;
			long		codeAuto;

			public SearchConditions()
			{
				startDate	= DateTime.Today;			// ��������� ��������
				startDate	= startDate.AddDays(-1);	// ������
				endDate		= DateTime.Today;			// 2 ���
				codePartner	= 0;						// ���������� �� ������� ������� ���
				codeAuto	= 0;						// ���������� ������� �� ���������� ���
			}

			public bool SetParameters(SqlCommand cmd)
			{
				try
				{
					cmd.Parameters["@codePartner"].Value	= (long)codePartner;
					cmd.Parameters["@codeAuto"].Value		= (long)codeAuto;
					cmd.Parameters["@startDate"].Value		= (DateTime)startDate;
					cmd.Parameters["@endDate"].Value		= (DateTime)endDate;
				}
				catch(Exception E)
				{
					SetException(E);
					return false;
				}
				return true;
			}

			public void SetDateInterval(DateTime start, DateTime end)
			{
				startDate = start;
				endDate = end;
			}

			public string DateIntervalTxt
			{
				get
				{
					return "(" + Db.DateToTxt(startDate) + "-" + Db.DateToTxt(endDate) + ")";
				}
			}
			public void SetCodePartner(long code)
			{
				codePartner = code;
				if (code<0) codePartner = 0;
			}
			public void SetCodeAuto(long code)
			{
				codeAuto = code;
				if (code<0) codeAuto = 0;
			}
		};
		#endregion

		public struct REP
		{
			public int to;
			public int run;
			public DateTime date;
			public int count;
			public int evrg_run;
			public bool error;
		};

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static SearchConditions searchCondition;

		// �������� ��������� ��������
		private long		number;			// ����� �������� (��������� ���������)
		private int			year;			// ��� ��������
		private DateTime	date;			// ���� �������� (� ��������� �������)
		private long		warrantNumber;	// ����� �����-������
		private int			run;			// ������ �� ��������
		
		// �������� - �������������� ���������
		private long		codeAutoType;		// ��� ������ �����������
		private long		codeAuto;			// ��� ���������� ������� �� �������� ������
		private long		codePartner;		// ��� ����������� - ������� �� �������� ������
		private string		comment;			// ���������� � �����-������
		private int			works;				// ���������� �� ���������� ������
		private bool		cashless;			// ������� ������������ �����-������
		private bool		credit_card;		// � ������� �� ��������� �����
		private long		codeGuarantyType;	// ��� ���� ��������
		private long		codeCategoryCost;	// ��� ��������� ���������
		private long		codeDiscount;		// ��� ������ ������
		private long		codeRepresent;		// ��� �������������
		private string		representDocument;	// �������� �������������
		private long		codeWorkshop;		// ��� �������������
		private bool		inner_warranty;		// ���������� ����������� �����-������
		private float		discount_work;		// ������ �� ������ � �����-������

		public long			code_licence_vehicle;	// ������������� � ����������� ��
		public bool			returned;

		// �������������� ���������� � ��������� ��������
		private DbCardAction.ActionCodes	tmpActionCode;		// ��� ���������� ��������
		private DateTime					tmpWarrantDate;		// ���� �������� �����-������
		private DateTime					tmpWarrantCloseDate;// ���� �������� �����-������
		private DbAutoType					tmpAutoType;		// ��� (������) ����������
		private DbAuto						tmpAuto;			// �������� ���������� ��������
		private DbPartner					tmpPartner;			// �������� ������� ��������
		private DbTimeWork					tmpTimeWork;		// ������� ����� ��� ��������� ����� ����������
		private DbGuarantyType				tmpGuarantyType;	// ��� ��������
		private DbPartner					tmpRepresent;		// �������������
		private DbWorkshop					tmpWorkshop;		// �������������


		// �������������� ������, ����������� �� �������������� �������
		private DbCategoryCost				tmpCategoryCost;		// ������� ��������� �����-������
		private DbGuarantyType				tmpGuarantyTypeFull;	// ������ ��� ��������
		
		private static SqlConnection conn;
		private static SqlCommand cmdExecWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectFind;
		private static SqlCommand cmdSysChangePartner;
		private static SqlCommand cmdSelectPreparation;
		private static SqlCommand cmdSelectAutoClosed;
		private static SqlCommand cmdSelectClosedDate;
		private static SqlCommand cmdSelectClosedInterval;
		private static SqlCommand cmdSelectPeriodBrand;
		private static SqlCommand cmdSelectPeriod;
		private static SqlCommand cmdSelectAutovaz;

		private static SqlCommand cmdSelectLast90;

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 13 ����������� ����� � ���������
			readerLength = 13 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 2 + DbAuto.ReaderLength + DbAutoType.ReaderLength + DbPartner.ReaderLength + DbTimeWork.ReaderLength + DbGuarantyType.ReaderLength;

			conn = connection;

			cmdExecWrite = new SqlCommand("WRITE_��������_1", conn);
			cmdExecWrite.CommandType = CommandType.StoredProcedure;
			cmdExecWrite.Parameters.Add("@number",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@year",SqlDbType.Int);
			cmdExecWrite.Parameters.Add("@date",SqlDbType.DateTime);
			cmdExecWrite.Parameters.Add("@codeAutoType",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@codeAuto",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@codePartner",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@comment",SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@adding",SqlDbType.Bit);
			cmdExecWrite.Parameters.Add("@works",SqlDbType.Int);
			cmdExecWrite.Parameters.Add("@cashless",SqlDbType.Bit);
			cmdExecWrite.Parameters.Add("@codeGuarantyType",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@codeCategoryCost",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@codeDiscount",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@codeRepresent",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@representDocument",SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@codeWorkshop",SqlDbType.BigInt);
			cmdExecWrite.Parameters.Add("@inner_warranty",SqlDbType.Bit);
			cmdExecWrite.Parameters.Add("@discount_work",SqlDbType.Real);
			Db.SetReturnError(cmdExecWrite);
			cmdExecWrite.Parameters["@number"].Direction	= ParameterDirection.InputOutput;
			cmdExecWrite.Parameters["@year"].Direction		= ParameterDirection.InputOutput;
			cmdExecWrite.Parameters["@date"].Direction		= ParameterDirection.InputOutput;

			//cmdSelect = new SqlCommand("SELECT_��������_1", conn);
			cmdSelect = new SqlCommand("SELECT_��������_V1", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@startDate", SqlDbType.DateTime);
			cmdSelect.Parameters.Add("@endDate", SqlDbType.DateTime);
			cmdSelect.Parameters.Add("@codePartner", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdSelect.CommandTimeout = 300;

			cmdSelectFind = new SqlCommand("SELECT_��������_�����", conn);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
			cmdSelectFind.Parameters.Add("@number", SqlDbType.BigInt);
			cmdSelectFind.Parameters.Add("@year", SqlDbType.Int);


			cmdSelectPreparation = new SqlCommand("SELECT_��������_���", conn);
			cmdSelectPreparation.CommandType = CommandType.StoredProcedure;
			cmdSelectPreparation.Parameters.Add("@codeAuto", SqlDbType.BigInt);

			cmdSelectAutoClosed = new SqlCommand("SELECT_��������_����������_��������", conn);
			cmdSelectAutoClosed.CommandType = CommandType.StoredProcedure;
			cmdSelectAutoClosed.Parameters.Add("@codeAuto", SqlDbType.BigInt);

			cmdSelectClosedDate = new SqlCommand("SELECT_��������_��������_������", conn);
			cmdSelectClosedDate.CommandType = CommandType.StoredProcedure;
			cmdSelectClosedDate.Parameters.Add("@date", SqlDbType.DateTime);
			cmdSelectClosedDate.CommandTimeout = 600;

			cmdSelectClosedInterval = new SqlCommand("SELECT_��������_��������_���������", conn);
			cmdSelectClosedInterval.CommandType = CommandType.StoredProcedure;
			cmdSelectClosedInterval.Parameters.Add("@date_start", SqlDbType.DateTime);
			cmdSelectClosedInterval.Parameters.Add("@date_end", SqlDbType.DateTime);
			cmdSelectClosedInterval.CommandTimeout = 300;

			cmdSelectPeriodBrand = new SqlCommand("REPORT_��������_������_�����", conn);
			cmdSelectPeriodBrand.CommandType = CommandType.StoredProcedure;
			cmdSelectPeriodBrand.Parameters.Add("@date_start", SqlDbType.DateTime);
			cmdSelectPeriodBrand.Parameters.Add("@date_end", SqlDbType.DateTime);
			cmdSelectPeriodBrand.Parameters.Add("@code_brand", SqlDbType.BigInt);
			cmdSelectPeriodBrand.CommandTimeout = 300;

			cmdSelectPeriod = new SqlCommand("REPORT_��������_������", conn);
			cmdSelectPeriod.CommandType = CommandType.StoredProcedure;
			cmdSelectPeriod.Parameters.Add("@date_start", SqlDbType.DateTime);
			cmdSelectPeriod.Parameters.Add("@date_end", SqlDbType.DateTime);
			cmdSelectPeriod.CommandTimeout = 300;

			cmdSysChangePartner = new SqlCommand("SYSTEM_��������_����������_��������", conn);
			cmdSysChangePartner.CommandType = CommandType.StoredProcedure;
			cmdSysChangePartner.Parameters.Add("@number",SqlDbType.BigInt);
			cmdSysChangePartner.Parameters.Add("@year",SqlDbType.Int);
			cmdSysChangePartner.Parameters.Add("@partnerCode",SqlDbType.BigInt);
			Db.SetReturnError(cmdSysChangePartner);

			cmdSelectLast90 = new SqlCommand("SELECT_��������_������_90", conn);
			cmdSelectLast90.CommandType = CommandType.StoredProcedure;
			cmdSelectLast90.Parameters.Add("@number", SqlDbType.BigInt);
			cmdSelectLast90.Parameters.Add("@year", SqlDbType.Int);
			cmdSelectLast90.Parameters.Add("@count", SqlDbType.Int);
			cmdSelectLast90.Parameters.Add("@evrg_run", SqlDbType.Int);
			cmdSelectLast90.Parameters.Add("@last_to", SqlDbType.Int);
			cmdSelectLast90.Parameters.Add("@last_torun", SqlDbType.Int);
			cmdSelectLast90.Parameters.Add("@last_date", SqlDbType.DateTime);
			cmdSelectLast90.Parameters["@count"].Direction = ParameterDirection.Output;
			cmdSelectLast90.Parameters["@evrg_run"].Direction = ParameterDirection.Output;
			cmdSelectLast90.Parameters["@last_to"].Direction = ParameterDirection.Output;
			cmdSelectLast90.Parameters["@last_torun"].Direction = ParameterDirection.Output;
			cmdSelectLast90.Parameters["@last_date"].Direction = ParameterDirection.Output;
			SetReturnError(cmdSelectLast90);

			cmdSelectAutovaz = new SqlCommand("SELECT_��������_�������", conn);
			cmdSelectAutovaz.CommandType = CommandType.StoredProcedure;
			cmdSelectAutovaz.Parameters.Add("@date_start", SqlDbType.DateTime);
			cmdSelectAutovaz.Parameters.Add("@date_end", SqlDbType.DateTime);
			cmdSelectAutovaz.CommandTimeout = 300;


			searchCondition = new SearchConditions();
		}

		public void SetTransaction(SqlTransaction trans)
		{
			cmdExecWrite.Transaction = trans;
		}
		#endregion

		#region ������������
		public DbCard()
		{
			number			= 0;
			year			= 0;
			date			= DateTime.Today;
			warrantNumber	= 0;
			run				= 0;

			codeAutoType		= 0;
			codeAuto			= 0;
			codePartner			= 0;
			comment				= "";
			works				= 0;
			cashless			= false;
			credit_card			= false;
			codeGuarantyType	= 0;
			codeCategoryCost	= 0;
			codeDiscount		= 0;
			codeRepresent		= 0;
			representDocument	= "";
			codeWorkshop		= 0;
			inner_warranty		= false;
			discount_work		= 0.0f;

			code_licence_vehicle	= 0;

			tmpActionCode		= 0;
			tmpWarrantDate		= DateTime.Today;
			tmpWarrantCloseDate	= DateTime.Today;
			tmpAutoType			= null;
			tmpAuto				= null;
			tmpTimeWork			= null;
			tmpGuarantyType		= null;
			tmpRepresent		= null;
			tmpWorkshop			= null;

			tmpCategoryCost		= null;

			adding = true;
		}

		public DbCard(DbCard cardSource)
		{
			// ��������
			number				= cardSource.number;
			year				= cardSource.year;
			date				= cardSource.date;
			warrantNumber		= cardSource.warrantNumber;
			codeAutoType		= cardSource.codeAutoType;
			codeAuto			= cardSource.codeAuto;
			codePartner			= cardSource.codePartner;
			comment				= cardSource.comment;
			run					= cardSource.run;
			works				= cardSource.works;
			cashless			= cardSource.cashless;
			credit_card			= cardSource.credit_card;
			codeGuarantyType	= cardSource.codeGuarantyType;
			codeCategoryCost	= cardSource.codeCategoryCost;
			codeDiscount		= cardSource.codeDiscount;
			codeRepresent		= cardSource.codeRepresent;
			representDocument	= cardSource.representDocument;
			codeWorkshop		= cardSource.codeWorkshop;
			inner_warranty		= cardSource.inner_warranty;
			discount_work		= cardSource.discount_work;

			code_licence_vehicle = cardSource.code_licence_vehicle;

			// ��������������
			tmpWarrantCloseDate	= cardSource.tmpWarrantCloseDate;
			tmpWarrantDate		= cardSource.tmpWarrantDate;
			tmpAutoType			= cardSource.tmpAutoType;
			tmpActionCode		= cardSource.tmpActionCode;
			tmpAuto				= cardSource.tmpAuto;
			tmpPartner			= cardSource.tmpPartner;
			tmpTimeWork			= cardSource.tmpTimeWork;
			tmpGuarantyType		= cardSource.tmpGuarantyType;
			tmpRepresent		= cardSource.tmpRepresent;
			tmpWorkshop			= cardSource.tmpWorkshop;

			tmpCategoryCost		= cardSource.tmpCategoryCost;
		}

		public DbCard(SqlDataReader reader, int offset)
		{
			number				= (long)GetValueLong(reader, offset);		offset++;
			year				= (int)GetValueInt(reader, offset);			offset++;
			date				= (DateTime)GetValueDate(reader, offset);	offset++;
			warrantNumber		= (long)GetValueLong(reader, offset);		offset++;
			codeAutoType		= (long)GetValueLong(reader, offset);		offset++;
			codeAuto			= (long)GetValueLong(reader, offset);		offset++;
			codePartner			= (long)GetValueLong(reader, offset);		offset++;
			comment				= (string)GetValueString(reader, offset);	offset++;
			run					= (int)GetValueInt(reader, offset);			offset++;
			works				= (int)GetValueInt(reader, offset);			offset++;
			cashless			= (bool)GetValueBool(reader, offset);		offset++;
			codeGuarantyType	= (long)GetValueLong(reader, offset);		offset++;
			codeCategoryCost	= (long)GetValueLong(reader, offset);		offset++;
			codeDiscount		= (long)GetValueLong(reader, offset);		offset++;
			codeRepresent		= (long)GetValueLong(reader, offset);		offset++;
			representDocument	= (string)GetValueString(reader, offset);	offset++;
			codeWorkshop		= (long)GetValueLong(reader, offset);		offset++;
			inner_warranty		= (bool)GetValueBool(reader, offset);		offset++;
			discount_work		= (float)GetValueFloat(reader, offset);		offset++;

			tmpActionCode		= (DbCardAction.ActionCodes)GetValueSmallInt(reader, offset);	offset++;
			tmpWarrantDate		= (DateTime)GetValueDate(reader, offset);						offset++;
			tmpWarrantCloseDate	= (DateTime)GetValueDate(reader, offset);						offset++;
			tmpAuto				= new DbAuto(reader, offset);									offset = offset + DbAuto.ReaderLength;
			tmpAutoType			= new DbAutoType(reader, offset);								offset = offset + DbAutoType.ReaderLength;
			tmpPartner			= new DbPartner(reader, offset);								offset = offset + DbPartner.ReaderLength;
			tmpTimeWork			= new DbTimeWork(reader, offset);								offset = offset + DbTimeWork.ReaderLength;
			tmpGuarantyType		= new DbGuarantyType(reader, offset);							offset = offset + DbGuarantyType.ReaderLength;
		}
		#endregion

		#region ��� ����������� ���������� � �����
		public string CardTxt
		{
			get
			{
				return "�" + number.ToString() + " �� " + Db.DateToTxt(date);
			}
		}
		public string RunTxt
		{
			get
			{
				return run.ToString();
			}
		}
		public string PartnerPhoneTxt
		{
			get
			{
				if(tmpPartner == null) return "";
				return tmpPartner.PhoneTxt;
			}
		}
		public string RepresentPhoneTxt
		{
			get
			{
				if(tmpRepresent == null) return "";
				return tmpRepresent.PhoneTxt;
			}
		}
		public string DiscountWorkTxt
		{
			get
			{
				return this.discount_work.ToString();
			}
		}
		public string AutoTypeTxt
		{
			get
			{	if(codeAutoType == 0) return "���������� ������� ������ ����������";
				if(tmpAutoType == null) return "���������� ������� ������ ����������";
				return tmpAutoType.NameTxt;
			}
		}
		public string GuarantyTypeTxt
		{
			get
			{
				if(codeGuarantyType == 0) return "�� �������";
				if(tmpGuarantyType == null) return "�� ������������";
				return tmpGuarantyType.Description;
			}
		}
		public string NumberTxt
		{
			get
			{
				return number.ToString() + "/" + date.Year.ToString();
			}
		}
		public string DateTxt
		{
			get
			{
				return Db.DateToTxt(date);
			}
		}
		public string WarrantNumberTxt
		{
			get
			{
				string txt;
				if(warrantNumber == 0) return "";
				txt = warrantNumber.ToString() + " �� " + Db.DateToTxt(tmpWarrantDate);
				return txt;
			}
		}
		public string WarrantDateTxt
		{
			get
			{
				return tmpWarrantDate.ToShortDateString();
			}
		}
		public string WarrantCloseShortTxt
		{
			get
			{
				return tmpWarrantCloseDate.ToShortDateString();
			}
		}
		public string WarrantOpenTxt
		{
			get
			{
				return tmpWarrantDate.ToShortDateString() + " " + tmpWarrantDate.ToShortTimeString();
			}
		}
		public string WarrantCloseTxt
		{
			get
			{
				return tmpWarrantCloseDate.ToShortDateString() + " " + tmpWarrantCloseDate.ToShortTimeString();
			}
		}
		public string PartnerNameTxt
		{
			get
			{
				if(codePartner == 0) return "������ �� ������";
				if(tmpPartner == null) return "������ �� ������";
				return tmpPartner.Title;
			}
		}
		public string RepresentNameTxt
		{
			get
			{
				if(codeRepresent == 0) return "������������� �� ������";
				if(tmpRepresent == null) return "������������� �� ������������";
				return tmpRepresent.Title;
			}
		}
		public string WorkshopTxt
		{
			get
			{
				if(codeWorkshop == 0) return "������������� �� �������";
				if(tmpWorkshop == null) return "������������� �� ������������";
				return tmpWorkshop.Name;
			}
		}

		public string AutoTxt
		{
			get
			{
				if(codeAuto == 0) return "���������� �� ������";
				if(tmpAuto == null) return "���������� �� ������";
				return tmpAuto.Title;
			}
		}
		public string AutoInfoTxt
		{
			get
			{
				if(codeAuto == 0) return "���";
				if(tmpAuto == null) return "���";
				return tmpAuto.Info;
			}
		}
		public string WorksTOTxt
		{
			get
			{
				string worksTxt = "������������ ������: ";
				if((works & (1 << 0)) > 0) worksTxt += " ��-1";
				if((works & (1 << 1)) > 0) worksTxt += " ��-2";
				if((works & (1 << 2)) > 0) worksTxt += " ��-3";
				if((works & (1 << 3)) > 0) worksTxt += " ��-4";
				if((works & (1 << 4)) > 0) worksTxt += " ��-5";
				if((works & (1 << 5)) > 0) worksTxt += " ��-6";
				if((works & (1 << 6)) > 0) worksTxt += " ����/������";
				return worksTxt;
			}
		}
		public string WorksDiagTxt
		{
			get
			{
				string worksTxt = "�����������: ";
				if((works & (1 << 7)) > 0) worksTxt += " ���";
				if((works & (1 << 8)) > 0) worksTxt += " ����";
				if((works & (1 << 9)) > 0) worksTxt += " ������� �����";
				if((works & (1 << 10)) > 0) worksTxt += " ������� �������";
				if((works & (1 << 11)) > 0) worksTxt += " ��������� �������";
				return worksTxt;
			}
		}
		public string WorksFailureTxt
		{
			get
			{
				string worksTxt = "�������������: ";
				if((works & (1 << 12)) > 0) worksTxt += " ���";
				if((works & (1 << 13)) > 0) worksTxt += " ����";
				if((works & (1 << 14)) > 0) worksTxt += " ������� �����";
				if((works & (1 << 15)) > 0) worksTxt += " ������� �������";
				if((works & (1 << 16)) > 0) worksTxt += " ��������� �������";
				if((works & (1 << 17)) > 0) worksTxt += " ������� ����������";
				if((works & (1 << 18)) > 0) worksTxt += " ������� ������";
				if((works & (1 << 19)) > 0) worksTxt += " �������������������";
				if((works & (1 << 20)) > 0) worksTxt += " �����";
				if((works & (1 << 21)) > 0) worksTxt += " ������� ��������� � �����������������";

				return worksTxt;
			}
		}
		public string CategoryCostTxt
		{
			get
			{
				if(codeCategoryCost == 0) return "�� ��������";
				if(tmpCategoryCost == null) return "��� = " + codeCategoryCost.ToString();
				return tmpCategoryCost.DbTitle();
			}
		}
		public string PayTypeTxt
		{
			get
			{
				if(this.cashless == true)
					return " (���/���.)";
				else
					return " (���.)";
			}
		}
		#endregion

		#region ������ � �������� ���������� - ���������
		public DbAuto Auto
		{
			set
			{
				if(value == null && codeAuto == 0) return;
				if (codeAuto == value.Code) return;
				if (value == null) codeAuto = 0;
				else codeAuto = value.Code;
				changed		= true;
				tmpAuto		= value;
				// ������������� ������ ��� ���������� - ������ ���?
				// AutoType = tmpAuto.AutoModel.AutoType;
			}
			get
			{
				return tmpAuto;
			}
		}
		public DbGuarantyType GuarantyType
		{
			set
			{
				if(value == null) return;
				if(codeGuarantyType == value.Code) return;
				codeGuarantyType	= value.Code;
				changed				= true;
				tmpGuarantyType		= value;
			}
			get
			{
				return tmpGuarantyType;
			}
		}
		public DbGuarantyType GuarantyTypeFull
		{
			set
			{
				tmpGuarantyTypeFull		= value;
			}
			get
			{
				return tmpGuarantyTypeFull;
			}
		}
		public DbAutoType AutoType
		{
			set
			{
				if(value == null) return;
				if(codeAutoType == value.Code) return;
				codeAutoType	= value.Code;
				tmpAutoType		= value;
				changed			= true;
				tmpTimeWork		= DbTimeWork.Read(tmpAutoType);
			}
			get
			{
				return tmpAutoType;
			}
		}
		public DbPartner Partner
		{
			set
			{
				if(value == null) return;
				if(codePartner == value.Code) return;
				codePartner		= value.Code;
				changed			= true;
				tmpPartner		= value;
			}
			get
			{
				return tmpPartner;
			}
		}
		public DbPartner Represent
		{
			set
			{
				if(value == null) return;
				if (value.Juridical == true) return;
				if (codeRepresent == value.Code)
				{
					tmpRepresent	= value;
					return;
				}
				codeRepresent	= value.Code;
				changed			= true;
				tmpRepresent	= value;
			}
			get
			{
				return tmpRepresent;
			}
		}
		public DbWorkshop Workshop
		{
			set
			{
				if(value == null) return;
				if(codeWorkshop == value.Code)
				{
					tmpWorkshop	= value;
					return;
				}
				codeWorkshop	= value.Code;
				changed			= true;
				tmpWorkshop	= value;
			}
			get
			{
				return tmpWorkshop;
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
				comment = SetStringLength(comment, value, 120, "����������");
			}
		}
		public float DiscountWork
		{
			set
			{
				if (value == discount_work) return;
				if(value < 0) return;
				//if(value > 30) return;
				discount_work = value;
				changed = true;
			}
			get
			{
				return discount_work;
			}
		}
		public bool IsDiscount
		{
			get
			{
				if(discount_work > 0.0F) return true;
				return false;
			}
		}
		public string RepresentDocument
		{
			get
			{
				return representDocument;
			}
			set
			{
				if(codeRepresent == codePartner || codeRepresent == 0)
					representDocument = "";
				else
					representDocument = SetStringNotEmptyLength(representDocument, value, 256, "�������� �������������");
			}
		}
		public bool Cashless
		{
			get
			{
				return cashless;
			}
			set
			{
				if(value == cashless) return;
				cashless = value;
				changed = true;
			}
		}
		public bool InnerWarranty
		{
			get
			{
				return inner_warranty;
			}
			set
			{
				if(value == inner_warranty) return;
				inner_warranty = value;
				changed = true;
			}
		}
		public int Works
		{
			get
			{
				return works;
			}
			set
			{
				if(works == value) return;
				works = value;
				changed = true;
			}
		}
		public DbCategoryCost CategoryCost
		{
			set
			{
				if(value == null) return;
				if(codeCategoryCost == value.Code) return;
				codeCategoryCost	= value.Code;
				changed				= true;
				tmpCategoryCost		= value;
			}
			get
			{
				return tmpCategoryCost;
			}
		}
		public bool Returned
		{
			set
			{
				if(returned == value) return;
				returned			= value;
				changed				= true;
			}
			get
			{
				return returned;
			}
		}
		#endregion

		#region ������ � �������� ���������� ������ ������
		public long WarrantNumber
		{
			get
			{
				return warrantNumber;
			}
		}
		public int ActionCode
		{
			get
			{
				return (int)tmpActionCode;
			}
		}
		public long CodeGuarantyType
		{
			get
			{
				return this.codeGuarantyType;
			}
		}
		public float GuarantyTypeVal
		{
			get
			{
				if(tmpGuarantyTypeFull == null) return 0.0F;
				return tmpGuarantyTypeFull.Val;
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

		public long CodePartner
		{
			get
			{
				return codePartner;
			}
		}
		public long CodeRepresent
		{
			get
			{
				return codeRepresent;
			}
		}
		public long CodeWorkshop
		{
			get
			{
				return codeWorkshop;
			}
			set
			{
				codeWorkshop = value;
			}
		}

		public DateTime Date
		{
			get
			{
				return date;
			}
		}

		public int Year
		{
			get
			{
				return year;
			}
			set
			{
				year = value;
			}
		}
		public float Time
		{
			get
			{
				if(tmpTimeWork == null) return 0;
				return tmpTimeWork.ApplyTimes(works);
			}
		}
		public string TimeTxt
		{
			get
			{
				return Time.ToString();
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
			item.Text = number.ToString();
			item.SubItems[1].Text = DateTxt;
			item.SubItems[2].Text = WarrantNumberTxt;
			item.SubItems[3].Text = PartnerNameTxt;
			item.SubItems[4].Text = AutoInfoTxt;
			item.SubItems[5].Text = Comment;

			item.BackColor = Color.White;
			if(tmpActionCode == DbCardAction.ActionCodes.Open)
				item.BackColor = Color.Yellow;
			if(tmpActionCode == DbCardAction.ActionCodes.Start)
				item.BackColor = Color.Yellow;
			if(tmpActionCode == DbCardAction.ActionCodes.Stop)
				item.BackColor = Color.Red;
			if(tmpActionCode == DbCardAction.ActionCodes.Close)
				item.BackColor = Color.LightGreen;
			if(tmpActionCode == DbCardAction.ActionCodes.Cancel)
				item.BackColor = Color.LightGray;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			searchCondition.SetParameters(cmdSelect);
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillListPreparation(ListView list, DbAuto auto)
		{
			cmdSelectPreparation.Parameters["@codeAuto"].Value = auto.Code;
			Db.DbFillList(list, cmdSelectPreparation, new DelegateInsertInList(InsertInList));
		}

		public static void FillListAutoClosed(ListView list, DbAuto auto)
		{
			cmdSelectAutoClosed.Parameters["@codeAuto"].Value = auto.Code;
			Db.DbFillList(list, cmdSelectAutoClosed, new DelegateInsertInList(InsertInList));
		}


		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCard element = new DbCard(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArrayPreparation(ArrayList array, DbAuto auto)
		{
			cmdSelectPreparation.Parameters["@codeAuto"].Value = auto.Code;
			Db.FillArray(array, cmdSelectPreparation, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayClosedDate(ArrayList array, DateTime date)
		{
			cmdSelectClosedDate.Parameters["@date"].Value = (DateTime)date;
			Db.FillArray(array, cmdSelectClosedDate, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayClosedInterval(ArrayList array, DateTime date_start, DateTime date_end)
		{
			cmdSelectClosedInterval.Parameters["@date_start"].Value = (DateTime)date_start;
			cmdSelectClosedInterval.Parameters["@date_end"].Value = (DateTime)date_end;
			Db.FillArray(array, cmdSelectClosedInterval, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayPeriodBrand(ArrayList array, DateTime date_start, DateTime date_end, long code_brand)
		{
			cmdSelectPeriodBrand.Parameters["@date_start"].Value = (DateTime)date_start;
			cmdSelectPeriodBrand.Parameters["@date_end"].Value = (DateTime)date_end;
			cmdSelectPeriodBrand.Parameters["@code_brand"].Value = (long)code_brand;
			Db.FillArray(array, cmdSelectPeriodBrand, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayPeriod(ArrayList array, DateTime date_start, DateTime date_end)
		{
			cmdSelectPeriod.Parameters["@date_start"].Value = (DateTime)date_start;
			cmdSelectPeriod.Parameters["@date_end"].Value = (DateTime)date_end;
			Db.FillArray(array, cmdSelectPeriod, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCard element = new DbCard(reader, 0);
			array.Add(element);
		}
		public static void FillArrayAutovaz(ArrayList array, DateTime date_start, DateTime date_end)
		{
			cmdSelectAutovaz.Parameters["@date_start"].Value = (DateTime)date_start;
			cmdSelectAutovaz.Parameters["@date_end"].Value = (DateTime)date_end;
			Db.FillArray(array, cmdSelectAutovaz, new DelegateInsertInArray(InsertInArray));
		}
		#endregion

		#region �������� ������
		public bool Write()
		{
			// ���� ������ ���������, � ���� ������ �� ����������
			if((!adding)&&(!changed)) return true;

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdExecWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdExecWrite.Parameters["@number"].Value			= (long)number;
				cmdExecWrite.Parameters["@year"].Value				= (int)year;
				cmdExecWrite.Parameters["@codeAutoType"].Value		= (long)codeAutoType;
				cmdExecWrite.Parameters["@date"].Value				= (DateTime)date;
				cmdExecWrite.Parameters["@codeAuto"].Value			= (long)codeAuto;
				cmdExecWrite.Parameters["@codePartner"].Value		= (long)codePartner;
				cmdExecWrite.Parameters["@comment"].Value			= (string)comment;
				cmdExecWrite.Parameters["@works"].Value				= (int)works;
				cmdExecWrite.Parameters["@cashless"].Value			= (bool)cashless;
				cmdExecWrite.Parameters["@codeGuarantyType"].Value	= (long)codeGuarantyType;
				cmdExecWrite.Parameters["@codeCategoryCost"].Value	= (long)codeCategoryCost;
				cmdExecWrite.Parameters["@codeDiscount"].Value		= (long)codeDiscount;
				cmdExecWrite.Parameters["@codeRepresent"].Value		= (long)codeRepresent;
				cmdExecWrite.Parameters["@representDocument"].Value	= (string)representDocument;
				cmdExecWrite.Parameters["@codeWorkshop"].Value		= (long)codeWorkshop;
				cmdExecWrite.Parameters["@inner_warranty"].Value	= (bool)inner_warranty;
				cmdExecWrite.Parameters["@discount_work"].Value		= (float)discount_work;
				cmdExecWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdExecWrite);
				number	= (long)cmdExecWrite.Parameters["@number"].Value;
				year	= (int)cmdExecWrite.Parameters["@year"].Value;
				date	= (DateTime)cmdExecWrite.Parameters["@date"].Value;
			}
			catch(Exception E)
			{
				if(trans != null)trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null)trans.Commit();
			SetTransaction(null);
			if(adding)MessageBox.Show("�������� ������ ��������");
			else MessageBox.Show("�������� ������ ����������");
			return true;
		}

		public bool Action(DbCardAction.ActionCodes code)
		{
			string txt = "";
			int		run = 0;

			DbCardAction action = new DbCardAction(this);
			action.ActionCode = code;

			FormSelectString dialog = new FormSelectString("���������� � ��������", "");
			dialog.ShowDialog();
			if(dialog.DialogResult == DialogResult.OK)
				txt = dialog.SelectedText;

			if(code == DbCardAction.ActionCodes.Open)
			{
				// ������ �������
				FormSelectString dialogRun = new FormSelectString("������� ������� ������", "");
				dialogRun.ShowDialog();
				if(dialogRun.DialogResult == DialogResult.OK)
					run = dialogRun.SelectedInt;
				if (run == 0)
				{
					MessageBox.Show("������ �� ������ ���� �������");
					return false;
				}
			}

			action.Run		= run;
			action.Comment = txt;
			if(action.Write() == false)
			{
				Db.ShowFaults();
				return false;
			}
			this.tmpActionCode = code;
			if(code == DbCardAction.ActionCodes.Open)
			{
				this.warrantNumber = action.WarrantNumber;
				this.run = action.Run;
				this.tmpWarrantDate	= action.Date;
			}
			if(code == DbCardAction.ActionCodes.Close)
			{
				this.tmpWarrantCloseDate	= action.Date;
			}
			return true;
		}
		public static DbCard Find(long number, int year)
		{
			SqlDataReader reader = null;
			DbCard card = null;
			try
			{

				cmdSelectFind.Parameters["@number"].Value = number;
				cmdSelectFind.Parameters["@year"].Value = year;
				reader = cmdSelectFind.ExecuteReader();
				if(reader.Read())
					card = new DbCard(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return card;
		}
		#endregion

		#region ������� �� �������
		public bool IsWarrantOpened
		{
			get
			{
				if(this.warrantNumber > 0) return true;
				return false;
			}
		}
		public bool IsWarrantClosed
		{
			get
			{
				if(this.tmpActionCode == DbCardAction.ActionCodes.Close) return true;
				return false;
			}
		}
		public bool CanUpdateWarrant
		{
			get
			{
				if(this.ActionCode == (int)DbCardAction.ActionCodes.Close) return false;
				if(this.ActionCode == (int)DbCardAction.ActionCodes.Stop) return false;
				if(this.ActionCode == (int)DbCardAction.ActionCodes.Cancel) return false;
				return true;
			}
		}
		public bool CanUpdateWarrantTitle
		{
			get
			{
				if(this.IsWarrantOpened) return false;
				if(this.ActionCode == (int)DbCardAction.ActionCodes.Cancel) return false;
				return true;
			}
		}
		#endregion

		#region ������� ��������� ������
		public bool Replace(DbPartner partner)
		{
			if(Db.CheckSysPass() == false) return false;
			if(partner == null) return false;
			string text = "�� ������� ��� ������ �������� " + this.Partner.Title + " �� " + partner.Title + "?";
			DialogResult res = MessageBox.Show(text, "��������������", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysChangePartner.Parameters["@number"].Value = (long)number;
			cmdSysChangePartner.Parameters["@year"].Value = (int)year;
			cmdSysChangePartner.Parameters["@partnerCode"].Value = (long)partner.Code;
			return Db.ExecuteCommandError(cmdSysChangePartner);
		}
		#endregion

		#region ����������� �������� ������
		public REP Report1()
		{
			REP rep;
			try
			{

				cmdSelectLast90.Parameters["@number"].Value = this.number;
				cmdSelectLast90.Parameters["@year"].Value = this.year;
				cmdSelectLast90.ExecuteNonQuery();
				ThrowReturnError(cmdSelectLast90);
				rep.count = (int)cmdSelectLast90.Parameters["@count"].Value;
				rep.evrg_run = (int)cmdSelectLast90.Parameters["@evrg_run"].Value;
				rep.to = (int)cmdSelectLast90.Parameters["@last_to"].Value;
				rep.run = (int)cmdSelectLast90.Parameters["@last_torun"].Value;
				rep.date = (DateTime)cmdSelectLast90.Parameters["@last_date"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				rep.count	= 0;
				rep.evrg_run = 0;
				rep.to = 0;
				rep.run = 0;
				rep.date = DateTime.Now;
				rep.error = true;
				return rep;
			}
			rep.error = false;
			return rep;
		}
		public static REP Report1(long number, int year)
		{
			REP rep;
			try
			{

				cmdSelectLast90.Parameters["@number"].Value = number;
				cmdSelectLast90.Parameters["@year"].Value = year;
				cmdSelectLast90.ExecuteNonQuery();
				ThrowReturnError(cmdSelectLast90);
				rep.count = (int)cmdSelectLast90.Parameters["@count"].Value;
				rep.evrg_run = (int)cmdSelectLast90.Parameters["@evrg_run"].Value;
				rep.to = (int)cmdSelectLast90.Parameters["@last_to"].Value;
				rep.run = (int)cmdSelectLast90.Parameters["@last_torun"].Value;
				rep.date = (DateTime)cmdSelectLast90.Parameters["@last_date"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				rep.count	= 0;
				rep.evrg_run = 0;
				rep.to = 0;
				rep.run = 0;
				rep.date = DateTime.Now;
				rep.error = true;
				return rep;
			}
			rep.error = false;
			return rep;
		}
		#endregion
	}
}
