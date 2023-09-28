using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCard.
	/// </summary>
	/// 
	
	public class DtCard : Dt
	{
		const float DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY = 30.0F;

		// ������� ������� ��� ����������� ���������
		public delegate void DisplayValueChange(object sender, string valueName = "");
		public event DisplayValueChange DisplayChanges;

		public enum PAY_TYPE : short { NONE = 0, CREDIT_CARD = 1, CASHLESS = 2, CASH = 3, NO_PAY = 4 };
		public enum NodeGroup : long { NONE = 0, ROOT = 1, GUARANTY_ROOT = 2, GUARANTY = 3, PAY_ROOT = 4, PAY = 5 };
		public enum NodeType : long { NONE = 0, WORK = 1, DETAIL = 2 };
		public enum CardState : short { NONE = 0, OPEND = 1, CLOSED = 2, REOPEND = 4, CANCELED = 5, STOPPED = 3, CLOSECANCEL = 10, MARKWORKEND = 20 };
		public struct Data
		{
			public NodeGroup group;
			public NodeType type;
			public object data;
		}
		public struct Pair
		{
			public long number;
			public int year;
		}

		// ������ ������ �������������� ���� ������
		private long _code; // ��� ��������
		private long _number; // ����� ��������
		private int _year; // ��� ��������
		private DateTime _date; // ���� �������� ��������
		private long _warrantNumber; // ����� �����-������
		private int _run; // ������ ��������
		private DateTime _agreedPickupTime;  // ������������� ����� ������
		private DtCard.CardState _state; // �������� ������ ��������
		private short _stateControl; // ������ �������� ��������
		private long _masterCode; // ��� ��� ������ �����-�����

		// �������������� ��������� ������
		private DtLicensePlate _licensePlate; // ��������������� ���� ���������� � ��������

		// ��������������� ������
		private bool _is_agreedPickupTime;   // ������������ �� ������������� ����� ������


		// �������������� ��������� ������
		private DtCardActionCollection _cardActions; // ������ �������� ��������
		private DtCardClaimCollection _cardClaims; // ������ ������ ��������
		private DtCardWorkCollection _cardWorks; // ������ ����� ��������
													 

		private long code_partner;
		private long code_represent;
		private long code_auto;
		private DtAuto _dtAuto; // ���������� � ��������
		private string represent_document;

		// ����������� ��������


		// ��� ��������
		private string tmp_auto_plate;

		private DateTime warrant_date_open;
		private DateTime warrant_date_close;
		
		private bool cashless;
		private bool credit_card;
		private bool inner;
		private string comment;
		private long code_workshop;
		private long code_guaranty;
		private long code_work;     // ��� ���� ������������� �����������
		
		
		private float discount;
		private float discount_parts; //������ �� �������� �����

		private long code_licence_vehicle;
		private bool returned;
		private long service_manager_code;
		// ���������
		private long supervisor_guaranty;
		private long supervisor_payment;
		private long supervisor_whole;

		private short card_rate;
		private bool card_rate_change;

		private short card_call_rate;
		private bool card_call_work_done;

		

		private string tmp_owner_txt;       // �������� ����������, ��������� �������������
		private string tmp_auto_model;      // ������ ����������, ��������� �������������
		private string tmp_auto_vin;        // VIN ����������
											//private string tmp_auto_plate;      // ��������������� ����
		private string tmp_master;          // ������-����������� ��������� �����
		
		private bool tmp_guaranty;      // ������� � ������� �������� � ��������
		private bool tmp_to;                // ������� � ������� �� � ��������

		private float tmpHourPrice; // ��������� ��������� �� ���������
		private float tmpHouePriceGuaranty; // ��������� ������������ ��������� �� ���������
		private DbAutoType tmpAutoType; // ������������ �� ���������
		private DtWorkshop tmpWorkshop; // ������������� ��������


		public bool FLAG_ERROR;

		// �������� ��� ��� 2022
		private ArrayList cardWorks = null; // ������ ����� ��������
		private ArrayList cardDetails = null; // ������ ����� ��������
		private DtPartner tmp_owner = null; // ��������� ���������� - �������� � ��������
		private DtPartner tmp_representative = null; // ��������� ���������� - ������������� � ��������


		//private string tmp_service_manager; // ������ ����������� ������� �����-�����
		private DtStaff _tmpServiceManager = null; // ��������� ���������� - ������ ����������� ��������
		private string tmp_service_manager_txt; // ������ ����������� ������� �����-�����

		#region ������ ��������� �������� �� �������� ����������
		public void SetCardRun(int srcNewRun)
		{
			if (srcNewRun < 0) { MessageBox.Show("������ �� ����� ���� ����� ����"); return; }
			if (srcNewRun == 0) { MessageBox.Show("������ �� ����� ���� �������"); return; }
			DateValuePair runAtDate = DtAuto.ReadDbLastRun(DtAuto); // ������� � ���� ������ ���������� ������
			int lastRun = runAtDate.GetInt();
			if (srcNewRun < lastRun) { MessageBox.Show("������ ������ �����������"); return; }
			CardRun = srcNewRun;
		}
		#endregion
		#region ������� � ������� ����������
		public DtCardActionCollection Actions
        {
            get
            {
				if(_cardActions == null)
                {
					_cardActions = new DtCardActionCollection(this);
                }
				return _cardActions;
            }
        }
		public DtCardClaimCollection Claims
		{
			get
			{
				if (_cardClaims == null)
				{
					_cardClaims = new DtCardClaimCollection(this);
				}
				return _cardClaims;
			}
		}
		public DtLicensePlate LicensePlate
		{
			get { return _licensePlate; }
			set
			{
				if (value == null) return;
				_licensePlate = value;
				IsChg = true;
			}
		}
        public int CardRun
		{
			get { return _run; }
            set
            {
				if (value < 0) { MessageBox.Show("������ �� ����� ���� ����� ����"); return; }
				if (_run == value) return;
				_run = value;
				IsChg = true;
			}
		}
		public long Number
		{
			get { return _number; }
			set { _number = value; }
		}
		public int Year
		{
			get { return _year; }
			set { _year = value; }
		}
		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		public long CodeLicenseVihicle
        {
            get { return code_licence_vehicle; }
            set { code_licence_vehicle = value; }
        }
		public DtCardWorkCollection WorkCollection
        {
			get
			{
				if (_cardWorks == null)
					LoadCardWorksForce(); // ��������� ������ ����� ��������
				return _cardWorks;
			}
        }
		#endregion

		public DtCard(long srcCardNumber, int srcCardYear):this()
        {
			Number = srcCardNumber;
			Year = srcCardYear;
			IsNew = false; // ��� �� ����� �������
			IsChg = false; // ��� �� ���������� �������
        }
		public DtCard(DtCard src)
		{
			_code = src._code;
			_number = src._number;
			_year = src._year;
			code_partner = src.code_partner;
			code_represent = src.code_represent;
			code_auto = src.code_auto;
			_run = src._run;
			represent_document = src.represent_document;
			_warrantNumber = src._warrantNumber;
			_date = src._date;
			warrant_date_open = src.warrant_date_open;
			warrant_date_close = src.warrant_date_close;
			_state = src._state;
			cashless = src.cashless;
			credit_card = src.credit_card;
			inner = src.inner;
			comment = src.comment;
			code_guaranty = src.code_guaranty;
			code_workshop = src.code_workshop;
			code_work = src.code_work;
			_stateControl = src.StateControl;
			_masterCode = src.MasterCode;
			discount = src.discount;
			discount_parts = src.discount_parts;
			service_manager_code = src.service_manager_code;

			code_licence_vehicle = src.code_licence_vehicle;
			returned = src.returned;

			// ���������
			supervisor_guaranty = src.supervisor_guaranty;
			supervisor_payment = src.supervisor_payment;
			supervisor_whole = src.supervisor_whole;

			// �������������
			tmp_owner = src.tmp_owner;
			tmp_auto_model = src.tmp_auto_model;

			tmp_auto_vin = src.tmp_auto_vin;

			tmp_auto_plate = src.tmp_auto_plate;
			tmp_master = src.tmp_master;
		//	tmp_service_manager = src.tmp_service_manager;

			tmp_guaranty = src.tmp_guaranty;
			tmp_to = src.tmp_to;

			// �������������� ��������� ������
			_cardActions = src._cardActions;
			_cardClaims = src._cardClaims;
			_cardWorks = src._cardWorks;
		}

		public DtCard()
		{
			// ���������� ����������
			_number = 0;
			_year = 0;
			_date = DateTime.MinValue;
			_run = 0;
			_licensePlate = new DtLicensePlate("", "");
			_agreedPickupTime = DateTime.MinValue;
			_is_agreedPickupTime = false;
			_state = 0;
			_warrantNumber = 0;

			_code = 0;
			
			code_partner = 0;
			code_represent = 0;
			CodeAuto = 0;
			DtAuto = null;
			
			represent_document = "";
			
			
			warrant_date_open = DateTime.Now;
			warrant_date_close = DateTime.Now;
			
			cashless = false;
			credit_card = false;
			inner = false;
			comment = "";
			code_guaranty = 0;
			code_workshop = 0;
			code_work = 0;
			_stateControl = 0;
			_masterCode = 0;
			discount = 0.0F;
			discount_parts = 0.0F;

			code_licence_vehicle = 0L;
			returned = false;
			service_manager_code = 0L;

			// ���������
			supervisor_guaranty = 0;
			supervisor_payment = 0;
			supervisor_whole = 0;

			card_rate = 0;
			card_rate_change = false;

			card_call_rate = 0;
			card_call_work_done = false;

			// �������������
			tmp_owner_txt = "";
			tmp_auto_model = "";

			tmp_auto_vin = "";

			//tmp_auto_plate = "";
			tmp_master = "";
			tmp_service_manager_txt = "";

			tmp_guaranty = false;
			tmp_to = false;

			tmp_owner = null;	
			tmpAutoType = null;
			tmpWorkshop = null;

			IsChg = false;
			IsNew = true;

			// �������������� ��������� ������
			_cardActions = null;
			_cardClaims = null;
			_cardWorks = null;
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "���_��������":
					_code = (long)val;
					break;
				case "�����_��������":
					Number = (long)val;
					break;
				case "���_��������":
					Year = (int)val;
					break;
				case "��������_��������":
					code_partner = (long)val;
					break;
				case "�������������_��������":
					code_represent = (long)val;
					break;
				case "����������_��������":
					CodeAuto = (long)val;
					break;
				case "��������_�������������_��������":
					represent_document = (string)val;
					break;
				case "������_��������":
					CardRun = (int)val;
					break;
				case "�����_�����_��������":
					WarrantNumber = (long)val;
					break;
				case "����":
					Date = (DateTime)val;
					break;
				case "����_�����_������_��������":
					warrant_date_open = (DateTime)val;
					break;
				case "����_�����_������_��������":
					warrant_date_close = (DateTime)val;
					break;
				case "������_��������":
					_state = (DtCard.CardState)(short)val;
					break;
				case "���������_�����_��������":
					credit_card = (bool)val;
					break;
				case "����������_��������":
					inner = (bool)val;
					break;
				case "�����������_��������":
					cashless = (bool)val;
					break;
				case "����������_��������":
					comment = (string)val;
					break;
				case "���_��������_��������":
					code_guaranty = (long)val;
					break;
				case "�������������_��������":
					code_workshop = (long)val;
					break;
				case "���_������������_��������":
					code_work = (long)val;
					break;
				case "������_��������_��������":
					StateControl = (short)val;
					break;
				case "������_���������_��������":
					MasterCode = (long)val;
					break;
				case "��������":
					tmp_owner_txt = (string)val;
					break;
				case "����������_������":
					tmp_auto_model = (string)val;
					break;
				case "����������_VIN":
					tmp_auto_vin = (string)val;
					break;
				case "����������_���������������_����":
					tmp_auto_plate = (string)val;
					break;
				case "������_������_��������":
					discount = (float)val;
					break;
				case "������_������_��������":
					discount_parts = (float)val;
					break;
				case "���������_��������":
					supervisor_guaranty = (long)val;
					break;
				case "���������_������":
					supervisor_payment = (long)val;
					break;
				case "���������_������":
					supervisor_whole = (long)val;
					break;
				case "�������������_��":
					code_licence_vehicle = (long)val;
					break;
				case "�������":
					returned = (bool)val;
					break;
				case "������_�����":
					tmp_master = (string)val;
					break;
				case "������_�����������":
					service_manager_code = (long)val;
					break;
				case "������_�����������_�������":
					tmp_service_manager_txt = (string)val;
					break;
				case "������������":
					card_rate = (short)val;
					break;
				case "������������_��������":
					card_rate_change = (bool)val;
					break;
				case "�������������_���������":
					card_call_work_done = (bool)val;
					break;
				case "�����_������":
					card_call_rate = (short)val;
					break;
				case "�����_������_�������������":
					_agreedPickupTime = (DateTime)val;
					break;
				case "����_�����_������_�������������":
					_is_agreedPickupTime = (bool)val;
					break;
				default:
					break;
			}
		}
		public object GetData(string data)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			switch (data)
			{
				case "���_��������":
					return (object)CardCode;
				case "�����_��������":
					return (object)(long)Number;
				case "����":
					return (object)(DateTime)Date;
				case "�����_�����_��������":
					return (object)(long)WarrantNumber;
				case "���_��������":
					return (object)(int)Year;
				case "��������_��������":
					return (object)(long)code_partner;
				case "�������������_��������":
					return (object)(long)code_represent;
				case "����������_��������":
					return (object)(long)CodeAuto;
				case "��������_�������������_��������":
					return (object)(string)represent_document;
				case "������_��������":
					return (object)(int)CardRun;
				case "������_��������":
					return (object)(short)_state;
				case "���������_�����_��������":
					return (object)(bool)credit_card;
				case "�����������_��������":
					return (object)(bool)cashless;
				case "����������_��������":
					return (object)(bool)inner;
				case "����������_��������":
					return (object)(string)comment;
				case "�������������_��������":
					return (object)(long)code_workshop;
				case "���_��������_��������":
					return (object)(long)code_guaranty;
				case "���_������������_��������":
					return (object)(long)code_work;
				case "������_��������_��������":
					return (object)StateControl;
				case "������_���������_��������":
					return (object)(long)MasterCode;
				case "��������":
					return (object)(string)tmp_owner_txt;
				case "����������_������":
					return (object)(string)tmp_auto_model;
				case "����������_VIN":
					return txtCard.AutoVin;//(object)(string)tmp_auto_vin;
				case "����������_���������������_����":
					return (object)(string)tmp_auto_plate;
				case "����_�����_������_��������":
					return (object)(DateTime)warrant_date_open;
				case "����_�����_������_��������":
					return (object)(DateTime)warrant_date_close;
				case "������_������_��������":
					return (object)(float)discount;
				case "������_������_��������":
					return (object)(float)discount_parts;
				case "���������_��������":
					return (object)(long)supervisor_guaranty;
				case "���������_������":
					return (object)(long)supervisor_payment;
				case "���������_������":
					return (object)(long)supervisor_whole;
				case "�������":
					return (object)(bool)returned;
				case "������_�����":
					return (object)(string)tmp_master;
				case "������_�����������":
					return (object)(long)service_manager_code;
				case "������_�����������_�������":
					return (object)(string)tmp_service_manager_txt;
				default:
					return (object)null;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			item.Tag = CardCode;
			item.Text = Number.ToString();
			item.SubItems.Add(Date.ToShortDateString());
			item.SubItems.Add(WarrantNumber.ToString());
			item.SubItems.Add(tmp_owner_txt);
			item.SubItems.Add(tmp_auto_model);
			item.SubItems.Add(tmp_auto_vin);//item.SubItems.Add(txtCard.AutoVin);
			item.SubItems.Add(tmp_auto_plate);
			item.SubItems.Add(this.CardRun.ToString());
			//	item.SubItems.Add(tmp_master);
			item.SubItems.Add(tmp_service_manager_txt + "/" + tmp_master);
			item.SubItems.Add(comment);
			switch (_state)
			{
				case CardState.OPEND:
					item.BackColor = Color.Yellow;
					break;
				case CardState.CLOSED:
					item.BackColor = Color.LightGreen;
					break;
				case CardState.STOPPED:
					item.BackColor = Color.Red;
					break;
				case CardState.REOPEND:
					item.BackColor = Color.Yellow;
					break;
				case CardState.CANCELED:
					item.BackColor = Color.Gray;
					break;
				default:
					break;
			}
		}
		public void SetLVItemPOSTimer(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			DtCard.Pair pair = new DtCard.Pair();
			pair.number = (long)Number;
			pair.year = (int)Year;
			item.Tag = (DtCard.Pair)pair;
			item.Text = Number.ToString();
			item.SubItems.Add(tmp_auto_plate);
			item.SubItems.Add(tmp_auto_model);
			item.SubItems.Add(txtCard.AutoVin);
			item.StateImageIndex = 0;
		}
		public void SetLVItemCardRate(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			DtCard.Pair pair = new DtCard.Pair();
			pair.number = (long)Number;
			pair.year = (int)Year;
			item.Tag = (DtCard.Pair)pair;
			item.Text = this.WarrantNumber.ToString() + "/" + Year.ToString();
			item.SubItems.Add(this.Date.ToShortDateString());
			item.SubItems.Add(this.tmp_auto_model);
			item.SubItems.Add(txtCard.AutoVin);
			if (this.tmp_to == true)
				item.SubItems.Add("��");
			else
				item.SubItems.Add("");
			if (this.tmp_guaranty == true)
				item.SubItems.Add("��");
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_master + "/" + this.tmp_service_manager_txt);
			if (this.card_rate == 0)
				item.SubItems.Add("");
			else
			{
				string txt = "������";
				if (card_rate == 1) txt = "�� �������";
				if (card_rate == 2) txt = "����� ����";
				if (card_rate == 3) txt = "����� ���";
				if (card_rate_change == true) txt += "/��";
				item.SubItems.Add(txt);
			}

			if (this.card_call_work_done == true)
				item.SubItems.Add("��");
			else
				item.SubItems.Add("");

			if (this.card_call_rate == 0)
				item.SubItems.Add("");
			else
			{
				string txt = "������";
				if (card_call_rate == 1) txt = "�������.";
				if (card_call_rate == 2) txt = "������.";
				if (card_call_rate == 3) txt = "����� ���� � �����.";
				if (card_call_rate == 4) txt = "�����������������.";
				if (card_call_rate == 5) txt = "�����.";
				if (card_call_rate == 6) txt = "������. ������ �� ������.";
				if (card_call_rate == 7) txt = "����� ���������. �� �������.";
				if (card_call_rate == 8) txt = "��������������� �������.";
				item.SubItems.Add(txt);
			}

			// ������������
			if (this.tmp_guaranty || this.tmp_to)
			{
				if (this.card_call_rate == 0 && this.card_rate == 0) item.BackColor = Color.Red;
			}
		}

		public float SummWorkPay()
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			summ = (float)summ - (float)Math.Round((float)(summ / 100 * discount), 0);
			return summ;
		}

		public float SummWorkPayNoDiscount()
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			return summ;
		}

		public float SummWorkPayDiscount(float discount_value)
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			summ = (float)summ - (float)Math.Round((float)(summ / 100 * discount_value), 0);
			return summ;
		}

		public float SummDetailPay()
		{
			float summ = 0.0F;
			ArrayList details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, details);
			foreach (object o in details)
			{
				DtCardDetail detail = (DtCardDetail)o;
				// ��������� ���������� � ������� �������
				summ += (float)Math.Ceiling(detail.DetailSummCash);
			}
			return summ;
		}
		public float SummDetailOilPay()
		{
			float summ = 0.0F;
			ArrayList details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, details);
			foreach (object o in details)
			{
				DtCardDetail detail = (DtCardDetail)o;
				summ += detail.DetailSummOilCash;
			}
			return summ;
		}
		public void SetTNode_Supervisor(TreeNode node)
		{
			// �������������
			DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
			string workshop_txt = "";
			if (workshop != null) workshop_txt = (string)workshop.GetData("������������_���");
			workshop_txt = workshop_txt.ToUpper();

			node.Text = workshop_txt + " " + Number.ToString() + "/" + Year.ToString() + " (" + WarrantNumber.ToString() + "/" + Year.ToString() + ")";
			Data data = new Data();
			Pair pair = new Pair();
			pair.number = Number;
			pair.year = Year;
			data.group = NodeGroup.ROOT;
			data.type = NodeType.NONE;
			data.data = (object)pair;
			node.Tag = data;

			if (supervisor_whole > 0)
			{
				node.ImageIndex = 2;
				node.SelectedImageIndex = 2;
				DtStaff staff = DbSqlStaff.Find(supervisor_whole);
				string staff_txt = "������";
				if (staff != null) staff_txt = staff.Title;
				node.Text = node.Text + " " + staff_txt;
			}
			else
			{
				node.ImageIndex = 1;
				node.SelectedImageIndex = 1;
			}

			#region ������
			{
				// ��������, ���������� �� ������
				string pay_type = "";
				if (inner == false && cashless == false) pay_type = "��������";
				if (cashless == true) pay_type = "�����������";
				if (inner == true) pay_type = "����������";
				TreeNode nd = new TreeNode("������ " + pay_type);
				Data dt = new Data();
				dt.group = NodeGroup.PAY_ROOT;
				dt.type = NodeType.NONE;
				nd.Tag = dt;

				if (supervisor_payment == 0)
				{
					nd.ImageIndex = 1;              // ������ �� �������
					nd.SelectedImageIndex = 1;
				}
				else
				{
					DtStaff staff = DbSqlStaff.Find(supervisor_payment);
					string staff_txt = "������";
					if (staff != null) staff_txt = staff.Title;
					nd.Text = "������ " + pay_type + " (" + staff_txt + ")";
					nd.ImageIndex = 2;
					nd.SelectedImageIndex = 2;
				}
				float summ_1 = SummWorkPay() + SummDetailOilPay();  // � ������ - ��, ������, ����� - ����� 1
				float summ_2 = SummDetailPay();                     // � ������ - �������� - ����� 2
				if (summ_1 + summ_2 > 0.0F)
				{
					// ������ �������� ������
					node.Nodes.Add(nd);

					if (inner == false && cashless == false)
					{
						if (summ_1 > 0)
						{
							// ��������� �������� ������ �� ����� 1
							TreeNode nd1 = new TreeNode("� ������ ��, ������, ����� " + summ_1.ToString());
							nd.Nodes.Add(nd1);
							// ������� �� ������ �����
							ArrayList payments = new ArrayList();
							DbSqlPayment.SelectInArrayCardWorkshopDepartment(payments, Number, Year, code_workshop, 1);
							float summ = 0.0F;
							foreach (object o in payments)
							{
								CS_Payment pay = (CS_Payment)o;
								TreeNode nd2 = new TreeNode();
								pay.SetTNode_Supervisor(nd2);
								nd1.Nodes.Add(nd2);
								summ += pay.summ;
							}
							if (summ == summ_1)
							{
								nd1.ImageIndex = 2;
								nd1.SelectedImageIndex = 2;
							}
							else
							{
								nd1.ImageIndex = 1;
								nd1.SelectedImageIndex = 1;
							}
						}
						if (summ_2 > 0)
						{
							// ��������� �������� ������ �� ����� 2
							TreeNode nd1 = new TreeNode("� ������ �������� ����� " + summ_2.ToString());
							nd.Nodes.Add(nd1);
							// ������� �� ������ �����
							ArrayList payments = new ArrayList();
							DbSqlPayment.SelectInArrayCardWorkshopDepartment(payments, Number, Year, code_workshop, 2);
							float summ = 0.0F;
							foreach (object o in payments)
							{
								CS_Payment pay = (CS_Payment)o;
								TreeNode nd2 = new TreeNode();
								pay.SetTNode_Supervisor(nd2);
								nd1.Nodes.Add(nd2);
								summ += pay.summ;
							}
							if (Math.Round(summ, 2) == Math.Round(summ_2, 2))
							{
								nd1.ImageIndex = 2;
								nd1.SelectedImageIndex = 2;
							}
							else
							{
								nd1.ImageIndex = 1;
								nd1.SelectedImageIndex = 1;
							}
						}
					}
				}

			}
			#endregion

			#region ��������
			{
				// ��������, ���������� �� ��������
				TreeNode nd = new TreeNode("��������");
				if (supervisor_guaranty == 0)
				{
					nd.ImageIndex = 1;              // �������� �� ��������
					nd.SelectedImageIndex = 1;
				}
				else
				{
					DtStaff staff = DbSqlStaff.Find(supervisor_guaranty);
					string staff_txt = "������";
					if (staff != null) staff_txt = staff.Title;
					nd.Text = "�������� (" + staff_txt + ")";
					nd.ImageIndex = 2;
					nd.SelectedImageIndex = 2;
				}
				bool exist_flag = false;
				// ���������� ��� ����������� ��������
				ArrayList details = new ArrayList();
				DbSqlCardDetail.SelectInArray(this, details);
				foreach (object o in details)
				{
					DtCardDetail detail = (DtCardDetail)o;
					if ((bool)detail.GetData("��������_��������_������") == true)
					{
						if (exist_flag == false)
						{
							node.Nodes.Add(nd);
							exist_flag = true;
						}
						detail = DbSqlCardDetail.Find(this, (long)detail.GetData("�������_��������_������"));
						TreeNode nd1 = new TreeNode("������");
						Data dt = new Data();
						dt.group = NodeGroup.GUARANTY;
						dt.type = NodeType.DETAIL;
						dt.data = (object)(long)detail.GetData("�������_��������_������");
						nd1.Tag = dt;
						detail.SetTNode_Supervisor(nd1);
						nd.Nodes.Add(nd1);
					}
				}
				// ���������� ��� ����������� ������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(this, works);
				foreach (object o in works)
				{
					DtCardWork work = (DtCardWork)o;
					if ((bool)work.GetData("��������_��������_������") == true)
					{
						if (exist_flag == false)
						{
							node.Nodes.Add(nd);
							exist_flag = true;
						}
						work = DbSqlCardWork.Find(this, (int)work.GetData("�������_��������_������"));
						TreeNode nd1 = new TreeNode("������");
						Data dt = new Data();
						dt.group = NodeGroup.GUARANTY;
						dt.type = NodeType.WORK;
						dt.data = (object)(int)work.GetData("�������_��������_������");
						nd1.Tag = dt;
						work.SetTNode_Supervisor(nd1);
						nd.Nodes.Add(nd1);
					}
				}
			}
			#endregion

		}

		// ����������, ���� �� � �������� ����������� ������
		public bool IsGuarantyWork()
		{
			// �������� ��� ������ ��������
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				if (work.GuaranteeFlag() == true)
				{
					tmp_guaranty = true;
					return true;
				}
			}
			return false;
		}

		public bool IsGuarantyDetail()
		{
			// �������� ��� ������ ��������
			ArrayList card_details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, card_details);
			foreach (DtCardDetail detail in card_details)
			{
				if (detail.IsGuaranty == true)
				{
					tmp_guaranty = true;
					return true;
				}
			}
			return false;
		}

		// ����������, ���� �� � �������� ��
		public bool IsToWork()
		{
			// �������� ��� ������ ��������
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				// ��������� ��������
				DtCardWork fwork = DbSqlCardWork.Find(this, (int)work.GetData("�������_��������_������"));
				if (fwork != null)
				{
					if (fwork.IsTo() == true)
					{
						tmp_to = true;
						return true;
					}
				}
			}
			return false;
		}

		public string NameToWork()
		{
			// �������� ��� ������ ��������
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				// ��������� ��������
				DtCardWork fwork = DbSqlCardWork.Find(this, (int)work.GetData("�������_��������_������"));
				if (fwork != null)
				{
					if (fwork.IsTo() == true)
					{
						tmp_to = true;
						return (string)fwork.GetData("������������_��������_������");
					}
				}
			}
			return "";
		}

		#region ������ � ������ ������ � ������� �������
		public ArrayList CardDetails
		{
			get
			{
				return cardDetails;
			}
		}
		public ArrayList CardWorks
		{
			get
			{
				return _cardWorks.Collection;
				//return cardWorks;
			}
		}
		public long CardCode
		{
			set { _code = value; }
			get { return _code; }
		}
		public long MasterCode
        {
            get { return _masterCode; }
			set { _masterCode = value; }
        }
		public short StateControl
        {
            get { return _stateControl; }
			set { _stateControl = value; }
        }
		
		public long LicenceVihicleCode
		{
			get { return code_licence_vehicle; }
		}
		public long ServiceManagerCode
		{
            get { return service_manager_code; }
			set { service_manager_code = value; }
		}
		public long CodeAuto
		{
			get { return code_auto; }
			set { code_auto = value; }
		}
	
		public string Comment
		{
			get { return comment; }
			set
			{
				if (comment == value) return;
				comment = value;
				IsChg = true;
			}
		}
		public long CodeOwner
        {
            get { return code_partner; }
            set { code_partner = value; }
        }
		public float Discount
        {
            get { return discount; }
            set {
				if (value < 0) return;
				if (value == discount) return;
				discount = value;
				IsChg = true;
			}
        }
		public long WarrantNumber
        {
            get { return _warrantNumber; }
			set { _warrantNumber = value; }
		}
		public long CodeWorkshop
		{
			get { return code_workshop; }
			set { code_workshop = value; }
		}
		public long CodeRepresentative
		{
			get { return code_represent; }
			set { code_represent = value; }
		}
		public string RepresentativeDocs
		{
			get { return represent_document; }
			set
			{
				if (represent_document == value) return;
				represent_document = value;
				IsChg = true;
			}
		}
		public DtCard.CardState State
		{
			get { return _state; }
			set { _state = value; }
		}
		public bool Returned
		{
			get
			{
				return returned;
			}
			set
			{
				if (returned != value)
				{
					returned = value;
					IsChg = true;
				}
			}
		}
		public DtCard.PAY_TYPE PayType
		{
			get
			{
				if (CreditCard) return PAY_TYPE.CREDIT_CARD;
				if (Cashless) return PAY_TYPE.CASHLESS;
				if (Inner) return PAY_TYPE.NO_PAY;
				return PAY_TYPE.CASH;
			}
			set
			{
				switch (value)
				{
					case PAY_TYPE.CREDIT_CARD:
						CreditCard = true;
						break;
					case PAY_TYPE.NO_PAY:
						Inner = true;
						break;
					case PAY_TYPE.CASHLESS:
						Cashless = true;
						break;
					case PAY_TYPE.CASH:
						CreditCard = false;
						Inner = false;
						Cashless = false;
						break;
					default:
						break;
				}
			}
		}
		public bool Cashless
		{
			get { return cashless; }
			set
			{
				if (cashless != value)
				{
					cashless = value;
					if (cashless == true)
					{
						inner = false;
						credit_card = false;
					}
					IsChg = true;
				}
			}
		}
		public bool Inner
		{
			get { return inner; }
			set
			{
				if (inner != value)
				{
					inner = value;
					if (inner == true)
					{
						cashless = false;
						credit_card = false;
					}
					IsChg = true;
				}
			}
		}
		public bool CreditCard
		{
			get { return credit_card; }
			set
			{
				if (credit_card != value)
				{
					credit_card = value;
					if (credit_card == true)
					{
						inner = false;
						cashless = false;
					}
					IsChg = true;
				}
			}
		}
		public float DiscountDetail
		{
			get
			{
				return discount_parts;
			}
			set
			{
				if (discount_parts != value)
				{
					discount_parts = value;
					IsChg = true;
				}
			}
		}
		public bool IsAgreedPickUpTime
		{
			get
			{
				if (_agreedPickupTime == DateTime.MinValue) return false;
				return true;
			}
		}
		public DateTime AgreedPickUpTime
		{
			get { return _agreedPickupTime; }
			set
			{
				if (value == _agreedPickupTime) return;
				_agreedPickupTime = value;
				IsChg = true;
				_is_agreedPickupTime = true;
			}
		}

		public long CodeAutoType
		{
			get { return code_work; }
			set { code_work = value; }
		}
		#endregion
		#region ������� ������� � �������
		public DtAuto DtAuto
		{
			get
			{
				if (CodeAuto == 0) return null;
				if (_dtAuto != null) return _dtAuto;
				else
				{
					_dtAuto = DbSqlAuto.Find(CodeAuto);
					return _dtAuto;
				}
			}
			set
			{
				if (value == null && CodeAuto == 0) return;
				if (value == null) CodeAuto = 0;
				else CodeAuto = value.Code;
				_dtAuto = value;
				_dtAuto.IsChg = true;
				//IsChg = true;
				ChangeMember("AUTO");
				// ��� �� ������ ����� ������������� ���� �� ����������
				if (CodeAutoType == 0)
					CodeAutoType = value.CodeAutoType;
				// � ������������ ������� ������ ��������������� ����� ��������
				LicensePlate = DtAuto.LicensePlate;

				// �������� ������� - ���������� ��������� ����������
				//DisplayChanges?.Invoke(_dtAuto, "����" );
				
			}
		}
		public DtAuto Auto
		{
			get
			{
				if (CodeAuto == 0) return null;
				if (_dtAuto != null) return _dtAuto;
				else
				{
					_dtAuto = DbSqlAuto.Find(CodeAuto);
					return _dtAuto;
				}
			}
			set
			{
				if (value == null && CodeAuto == 0) return;
				if (value == null) CodeAuto = 0;
				else CodeAuto = value.Code;
				_dtAuto = value;
				_dtAuto.IsChg = true;
				IsChg = true;
				// ��� �� ������ ����� ������������� ���� �� ����������
				if (CodeAutoType == 0)
					CodeAutoType = value.CodeAutoType;
				// � ������������ ������� ������ ��������������� ����� ��������
				LicensePlate = DtAuto.LicensePlate;
			}
		}
		public DtPartner Owner
		{
			set
			{
				if (value == null && CodeOwner == 0) return;
				if (value == null)
				{
					CodeOwner = 0;
					Discount = 0;
				}
				else
				{
					CodeOwner = value.Code;
					// ��� ��, ��� ������ ������ ���������, ������ ������� ������ �� ���������
					DtPartnerProperty tmpProperties = value.Properties;
					if (tmpProperties != null)
						Discount = tmpProperties.Discount;
					else
						Discount = 0;
				}
				tmp_owner = value;
				IsChg = true;
			}
			get
			{
				if (CodeOwner == 0) return null;
				if (tmp_owner != null) return tmp_owner;
				else
				{
					tmp_owner = DbSqlPartner.Find(CodeOwner);
					return tmp_owner;
				}
			}
		}
		public DtWorkshop Workshop
		{
			set
			{
				if (value == null && CodeWorkshop == 0) return;
				if (value == null)
				{
					CodeWorkshop = 0;
				}
				else
				{
					CodeWorkshop = value.CodeWorkshop;
				}
				tmpWorkshop = value;
				IsChg = true;
			}
			get
			{
				if (CodeWorkshop == 0) return null;
				if (tmpWorkshop != null) return tmpWorkshop;
				else
				{
					tmpWorkshop = DbSqlWorkshop.Find(CodeWorkshop);
					return tmpWorkshop;
				}
			}
		}
		
		public DtPartner Representative
		{
			set
			{
				if (value == null && CodeRepresentative == 0) return;
				if (value == null)
				{
					// ��� ��, ��� ��������� ������������� ������� �������� �������������
					CodeRepresentative = 0;
					RepresentativeDocs = "";
				}
				else
				{
					if (CodeRepresentative == value.Code) return;
					if (value.IsJuridical()) return; // ������������� ����� ���� ������ ���������� �����
					// ��� ��, ��� ������ ������ �������������, ������ �������� ������������� �� ���������
					CodeRepresentative = value.Code;
					RepresentativeDocs = "������������";
				}
				tmp_representative = value;
				IsChg = true;
			}
			get
			{
				if (CodeRepresentative == 0) return null;
				if (tmp_representative != null) return tmp_representative;
				else
				{
					tmp_representative = DbSqlPartner.Find(CodeRepresentative);
					return tmp_representative;
				}
			}
		}
		public DtStaff ServiceManager
		{
			set
			{
				if (value == null) 
                {
					if(service_manager_code == 0) return;
                    else
                    {
						service_manager_code = 0;
						_tmpServiceManager = value;
						IsChg = true;
						return;
					}
                }
				if (service_manager_code == value.Code) return;
				service_manager_code = value.Code;
				_tmpServiceManager = value;
				IsChg = true;
			}
			get
			{
				if (service_manager_code == 0) return null;
				if (_tmpServiceManager == null)
					return DbSqlStaff.Find(service_manager_code);
				else
					return _tmpServiceManager;
			}
		}	
		public DbAutoType AutoType
        {
            get
            {
				if (tmpAutoType == null)
					tmpAutoType = DbAutoType.Find(code_work);
				return tmpAutoType;
            }
            set
            {
				if (value == null) return;
				if (code_work == value.Code) return;
				code_work = value.Code;
				tmpAutoType = value;
				IsChg = true;
				tmpHourPrice = value.Price;
            }
        }	
		public float HourPrice
        {
            get { 
				if(tmpHourPrice == 0.0F)
				{
					DbAutoType dbAutoType = AutoType;
					if(dbAutoType != null)
						tmpHourPrice = dbAutoType.Price;
                }
				return tmpHourPrice;
			}
            set { tmpHourPrice = value; }
        }
		#endregion

		public void LoadCardWorksForce()
		{
			_cardWorks = new DtCardWorkCollection(this);
			cardWorks = _cardWorks.Collection;
			SetWorkDiscount();

			//if (cardWorks != null) cardWorks.Clear();
			//else cardWorks = new ArrayList();
			//DbSqlCardWork.SelectInArray(this, cardWorks);
		}

		public void LoadCardDetailsForce()
		{
			if (cardDetails != null) cardDetails.Clear();
			else cardDetails = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, cardDetails);
		}

		public static void SetWorkCalc(DbCardWork wrk, float srcPrice, float srcDiscount)
		{
			if (wrk.Val != 0.0F)
			{
				if (srcPrice != 0.0F)
					wrk.Price = srcPrice;
			}
			if(srcDiscount != 0.0F)
				wrk.Discount = srcDiscount;
        }

		public void SetWorkDiscount()
		{
			if (Discount == 0.0F) return; // ��� ����� ������
			foreach(DtCardWork work in CardWorks)
            {
				if (work.Discount == 0) work.Discount = Discount;
            }
		}

		#region ������� ���������� ������ ������ � ���������� - ���
		public bool Close()	// ������� �������� �����-������ ������� �������� � �����
        {
			if (IsNew) return Error.ErroMessageFalse("�������� - �����"); // �������� �����
			if (IsChg) return Error.ErroMessageFalse("�����-����� �������"); // ������ ��������� ���������� �����-�����
			if (!(State == DtCard.CardState.OPEND || State == DtCard.CardState.REOPEND)) return Error.ErroMessageFalse("�����-����� �� ������"); // ������� ����� ������ �������� �����-�����
			DtCardAction closeAction = new DtCardAction(this);
			closeAction.ActionCode = DtCard.CardState.CLOSED;
			closeAction = DbSqlCardAction.Insert(closeAction);
			if (closeAction == null) return false;
			_cardActions.AddAction(closeAction);
			State = DtCard.CardState.CLOSED;
			return true;
        }
		public bool Open() // ������� �������� �����-������ ������� �������� � �����
		{
			if (IsNew) return Error.ErroMessageFalse("�������� - �����"); // �������� �����
			if (IsChg) return Error.ErroMessageFalse("�����-����� �������"); // ������ ��������� ���������� �����-�����
			if (State != DtCard.CardState.NONE) return Error.ErroMessageFalse("�����-����� ��� ���� ��������"); // ������� ����� ������ �������� ��� ��������
			DtCardAction closeAction = new DtCardAction(this);
			closeAction.ActionCode = DtCard.CardState.OPEND;
			closeAction = DbSqlCardAction.Insert(closeAction);
			if (closeAction == null) return false;
			_cardActions.AddAction(closeAction);
			State = DtCard.CardState.OPEND;
			WarrantNumber = Number;
			return true;
		}
		public bool Create() // ������� ����� �������� � ���� ������
		{
			if (!IsNew) return Error.ErroMessageFalse("�������� - �� �����"); // �������� �����
			if (!DbSqlCard.Insert(this)) return Error.ErroMessageFalse("�� ����� �������� � ���� ������");
			return true;
		}
        #endregion

        #region ���������� ������� ��� �������� ������
		public bool Closed
        {
			get
            {
				if (State == DtCard.CardState.CLOSED) return true;
				return false;
            }
        }
        #endregion

		public DtBrand.DIALER DialerOfficial
        {
            get
            {
				if (DtAuto == null) return DtBrand.DIALER.unknown;
				if(DtAuto.AutoBrand == null) return DtBrand.DIALER.unknown;
				return DtAuto.AutoBrand.DialerOfficial;
            }
        }

        #region ������������
		public float ComputePayedHoursMinusDiscout()
        {
			float hours_no_discount = 0.0F;
			float hours_with_discount = 0.0F;
			LoadCardWorksForce();
			foreach(object o in CardWorks)
            {
				DtCardWork card_work = (DtCardWork)o;
                if (card_work.GuaranteeFlag())
                {
					continue;
                }
				if( (card_work.Discount > 0) && (card_work.Discount < DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY))
                {
					hours_with_discount += card_work.WorkNV;
				}
                else if (card_work.Discount >= DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY)
				{
					hours_with_discount += card_work.WorkNV * (100.0F - card_work.Discount) / 100.0F;
				}
                else
                {
					hours_no_discount += card_work.WorkNV;
				}

				if(Discount > DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY)
                {
					hours_no_discount = hours_no_discount * (100.0F - Discount) / 100.0F;
				}
			}
			return hours_no_discount + hours_with_discount;
        }
		public float ComputePayedHours()
		{
			float hours = 0.0F;
			LoadCardWorksForce();
			foreach (object o in CardWorks)
			{
				DtCardWork card_work = (DtCardWork)o;
				if (card_work.GuaranteeFlag())
				{
					continue;
				}
				hours += card_work.WorkNV;
			}
			return hours;
		}
		#endregion

		public string LoadPreviousRecomendations()
		{
			DateTime date_end = this.Date.AddDays(-1);
			DateTime date_start = date_end;
			date_start = date_start.AddYears(-3);

			ArrayList array = new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshopAuto(array, date_start, date_end, CodeWorkshop, CodeAuto);
			int count;
			if((count = array.Count) == 0){
				return "";
            }
			DtCard lastCard = (DtCard)array[0];
			foreach (object o in array)
			{ 
				if(lastCard.Year < ((DtCard)o).Year)
                {
					lastCard = (DtCard)o;
                }
				else if( (lastCard.Year == ((DtCard)o).Year) && (lastCard.Number < ((DtCard)o).Number))
                {
					lastCard = (DtCard)o;
				}
			}
			ArrayList recomends = new ArrayList();
			DbSqlCardRecomendation.SelectInArray(recomends, lastCard.Number, lastCard.Year);
			string recomendsTxt = "";
			foreach (DtCardRecomendation recomend in recomends)
			{
				recomendsTxt += recomend.RecomendationTxt + ";";
			}
			return recomendsTxt;
		}
	}

    public class DtCardDatabaseConnector
    {
		private DtCard _card;

		public DtCardDatabaseConnector(DtCard dtCard)
        {
			_card = dtCard;
        }

		public bool SaveWorks()
        {
			// ���������� �������� � ���� ������
			for(int index = 0; index < _card.WorkCollection.Count; index++)
			{
				DtCardWorkDatabaseConnector databaseConnector = new DtCardWorkDatabaseConnector(_card.WorkCollection.GetWork(index));
				databaseConnector.Save();
			};			
			return true;
        }
    }

	public class DtCardDisplay
    {
		private DtCard _card;

		public DtCardDisplay(DtCard card)
        {
			_card = card;
        }

		public void DisplayCardWorks(ListView list)
        {
			list.Items.Clear();
			foreach(DtCardWork work in _card.WorkCollection.Collection)
            {
				DtCardWorkDisplay display = new DtCardWorkDisplay(work);
				ListViewItem item = list.Items.Add("�������������� ��������");
				display.ListViewItem(item);
            }
        }

		public DtCardWorkDisplay GetSelectedCardWorkDisplay(ListView list)
        {
			ListViewItem item = Db.GetItemSelected(list);
			DtCardWork cardWork = _card.WorkCollection.FindWork(item);
			if (cardWork == null) return null;
			DtCardWorkDisplay displey =  new DtCardWorkDisplay(cardWork);
			displey.SetConnectedLVItem(item);
			return displey;
		}
    }

	
}
