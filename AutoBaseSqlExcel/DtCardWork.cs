using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace AutoBaseSql
{
    /// <summary>
    /// Summary description for DtCardWork.
    /// </summary>
    public class DtCardWork : Dt, Calculatable
    {

        const float UNINITIALIZED = -1.0f;
        const float FLOATNULL = 0.0f;

        struct Values
        {
            private int operation_amount;
            private float labor_time;
            private float price;
            private float discount_percent;
            public float WorkSumm()
            {
                if (labor_time == FLOATNULL)
                {
                    return price * operation_amount;
                }
                else
                {
                    return price * operation_amount * labor_time;
                }
            }
        }

        // ������ ������ �� ����
        private long _cardNumber; // ����� ��������
        private int _cardYear; // ��� ��������
        private int _position; // ������� � �����-������
        private float _laborTime; // ������������ ��� ����� �� ����� - ���������� ����� ������ ���������� ��������
        private long _codeWork; // ��� ������ �� �������� ��� ������ � ���� ������
        private long _guarantyType; // ��� ��������

        private float _quontity = 0.0F;           // ���������� ����������� ��������

        private float price;                // ��������� ����� �������� �� ������� ������������
        private bool guaranty;          // ���� ��������
        private bool liquid;                // ���� ������ � �������������� ���-��������
        private float discount;         // ������ ������ %
        private string mistake;         // � ������ ���� ��������

        private long mistake_initiator; // ��� �������� �������
        private long special_type;      // ����������� ��� ������ ????


        private float tmp_spnv_;                 // ����������� ���������� �� � ������ ������
        private long tmp_code_collection_;       // ��� ����������� ���������

        //private string	tmp_work_name;
        private string tmp_guaranty_name;
        private string tmp_mistake_initiator;
        private int tmp_staff_count;
        private long tmp_directorywork_code;
        private bool tmp_cashless;
        private bool tmp_inner;
        private long tmp_code_partner;
        private string tmp_work_position_number;
        private string tmp_executors_tnum;          // ������ ������ ��������� ������� ������������


        //private bool tmp_pure_nv;                // ���������, ��� �������� ������ ��, �� �� ������ ������

        // �������������� �������������� ������
        private DtWork _tmpWork; // ������ �� ������� ��������� ������ ������� � �����-������
        private DtGuarantyType _tmpGuarantyType; // ��� �������� ������������� ��� �����

        // �������������� �������������� ������
        private DtStaffCollection _tmpExecutors; // ������ ������������ ������ ������

        public DtCardWork()
        {
            _cardNumber = _codeWork = _guarantyType = 0L;
            _cardYear = _position = 0;
            _laborTime = 0.0F;

            //	quontity			= 0.0F;				
            price = 0.0F;
            guaranty = false;
            liquid = false;
            discount = 0.0F;
            mistake_initiator = 0;
            mistake = "";
            special_type = 0;

            //tmp_work_name				= "";
            tmp_mistake_initiator = "";
            tmp_guaranty_name = "";
            tmp_staff_count = 0;
            tmp_directorywork_code = 0;
            tmp_cashless = false;
            tmp_inner = false;
            tmp_code_partner = 0;
            tmp_work_position_number = "";
            tmp_executors_tnum = "";


            //tmp_pure_nv = false;

            _tmpExecutors = null;
            _tmpGuarantyType = null;
            _tmpWork = null;

            tmp_spnv_ = UNINITIALIZED;
            tmp_code_collection_ = 0;
        }

        public DtCardWork(DbCardWork wrk) : base()
        {
            if (wrk.Number == 0) IsNew = true;  // ��� ����� �������
            _cardNumber = wrk.CardNumber;
            _cardYear = wrk.CardYear;
            _position = wrk.Number;
            _laborTime = wrk.Val;
            _codeWork = wrk.CodeWork;

            OperationAmount = wrk.Quontity;//quontity			= wrk.Quontity;


            price = wrk.Price;
            guaranty = wrk.Guaranty;
            liquid = wrk.Oil;
            discount = wrk.Discount;

            mistake_initiator = 0;
            mistake = "";
            special_type = 0;

            //tmp_work_name				= wrk.WorkName;
            tmp_mistake_initiator = "";
            tmp_guaranty_name = "";
            tmp_staff_count = 0;
            tmp_directorywork_code = wrk.CodeDirectoryWork;
            tmp_cashless = false;
            tmp_inner = false;
            tmp_code_partner = 0;
            tmp_work_position_number = wrk.WorkPosition;
            tmp_executors_tnum = "";


            //tmp_pure_nv = false;

            tmp_spnv_ = UNINITIALIZED;
            tmp_code_collection_ = 0;
        }



        public object GetData(string data)
        {
            switch (data)
            {
                case "�����_��������_��������_������":
                    return (object)(long)CardNumber;
                case "���_��������_��������_������":
                    return (object)(int)CardYear;
                case "�������_��������_������":
                    return (object)(int)Position;
                case "����������_��������_������":
                    return (object)(float)OperationAmount;//quontity;
                case "������������_��������_������":
                    return (object)(float)LaborTime;
                case "��������_��������_������":
                    return (object)(float)price;
                case "������_��������_������":
                    return (object)(float)discount;
                case "���������_��������_������":
                    return (object)(long)mistake_initiator;
                case "��������_���_��������_������":
                    return (object)CodeGuarantyType;
                case "��������_��������_������":
                    return (object)(bool)guaranty;
                case "�����_��������_������":
                    return (object)(string)mistake;
                case "�����������_���":
                    return (object)(long)special_type;
                case "��������_���_������������_��������_������":
                    return (object)(string)tmp_guaranty_name;
                case "������������_��������_������":
                    return (object)WorkName;// (string)tmp_work_name;
                case "�����_�������_��������_������":
                    return (object)(string)tmp_work_position_number;
                case "����������_������������":
                    return (object)(int)tmp_staff_count;
                case "������_���_����������_������������":
                    return (object)(long)tmp_directorywork_code;
                case "������":
                    return (object)(bool)tmp_cashless;
                case "����������_��������":
                    return (object)(bool)tmp_inner;
                case "��������_������_���_����������":
                    return (object)(long)tmp_code_partner;
                case "�����_���������_�����_�����������":
                    return (object)(string)tmp_executors_tnum;
                //case "������_�����_��":
                //	return (object)(float)TmpServicePackageLaborTime;//tmp_spnv;
                case "������_���_���������":
                    return (object)(long)TmpCodeConnectedWorkCollection;// tmp_code_collection;
                case "���_������������_��������_������":
                    return (object)CodeWork;
                //case "����_��":
                //	return (object)(bool)tmp_pure_nv;
                default:
                    return (object)null;
            }
        }

        public void SetData(string data, object val)
        {
            switch (data)
            {
                case "�����_��������_��������_������":
                    CardNumber = (long)val;
                    break;
                case "���_��������_��������_������":
                    _cardYear = (int)val;
                    break;
                case "�������_��������_������":
                    Position = (int)val;
                    break;
                case "���_������������_��������_������":
                    CodeWork = (long)val;
                    break;
                case "������������_��������_������":
                    //tmp_work_name = (string)val;
                    break;
                case "�����_�������_��������_������":
                    tmp_work_position_number = (string)val;
                    break;
                case "����������_��������_������":
                    OperationAmount = (float)val;//quontity = (float)val;
                    break;
                case "������������_��������_������":
                    LaborTime = (float)val;
                    break;
                case "��������_��������_������":
                    price = (float)val;
                    break;
                case "��������_��������_������":
                    guaranty = (bool)val;
                    break;
                case "�����_��������_������":
                    liquid = (bool)val;
                    break;
                case "������_��������_������":
                    discount = (float)val;
                    break;
                case "���������_��������_������":
                    mistake_initiator = (long)val;
                    break;
                case "���������_������������_��������_������":
                    tmp_mistake_initiator = (string)val;
                    break;
                case "��������_���_��������_������":
                    CodeGuarantyType = (long)val;
                    break;
                case "�����������_���":
                    special_type = (long)val;
                    break;
                case "��������_���_������������_��������_������":
                    tmp_guaranty_name = (string)val;
                    break;
                case "�����_��������_������":
                    mistake = (string)val;
                    break;
                case "����������_������������":
                    tmp_staff_count = (int)val;
                    break;
                case "������_���_����������_������������":
                    tmp_directorywork_code = (long)val;
                    break;
                case "������":
                    tmp_cashless = (bool)val;
                    break;
                case "����������_��������":
                    tmp_inner = (bool)val;
                    break;
                case "��������_������_���_����������":
                    tmp_code_partner = (long)val;
                    break;
                case "�����_���������_�����_�����������":
                    tmp_executors_tnum = (string)val;
                    break;
                //case "������_�����_��":
                //	TmpServicePackageLaborTime = (float)val;//tmp_spnv = (float)val;
                //	break;
                case "������_���_���������":
                    TmpCodeConnectedWorkCollection = (long)val;// tmp_code_collection = (long)val;
                    break;
                //case "����_��":
                //	tmp_pure_nv = (bool)val;
                //	break;
                default:
                    break;
            }
        }

        public void SetTNode_Supervisor(TreeNode node)
        {
            DtTxtCardWork txtCardWork = new DtTxtCardWork(this);
            node.Text = this.tmp_guaranty_name + "-" + txtCardWork.WorkName + " (" + this.LaborTime + "*" + this.price + "*" + this.WorkQuontity + ")";
        }

        public void SetLVItem(ListViewItem item)
        {
            DtTxtCardWork txtCardWork = new DtTxtCardWork(this);
            item.SubItems.Clear();      // ����� ������� ���������� ���������� � ���������
            item.StateImageIndex = 0;

            item.Tag = this.Position;
            item.Text = txtCardWork.WorkName;
            item.SubItems.Add(this.WorkQuontity);
            item.SubItems.Add(this._laborTime.ToString());
            item.SubItems.Add(this.price.ToString());
            item.SubItems.Add(this.WorkSumm.ToString());
            item.SubItems.Add(this.WorkSummDiscount.ToString());
            item.SubItems.Add(this.tmp_guaranty_name);
            item.SubItems.Add(this.tmp_mistake_initiator);
            item.SubItems.Add(this.mistake);

            // ��������� ������
            if (guaranty == true)
            {
                if (liquid == true)
                {
                    item.StateImageIndex = 6;
                }
                else
                {
                    item.StateImageIndex = 5;
                }
            }
            else
            {
                if (liquid == true)
                {
                    item.StateImageIndex = 2;
                }
                else
                {
                    item.StateImageIndex = 1;
                }
            }
        }

        #region ������� �������� ����������
        public float WorkSumm   // ��������� ������� ����� ������ ��� ������
        {
            get
            {
                if (LaborTime == 0.0f)
                    return price * OperationAmount;
                else
                    return price * OperationAmount * LaborTime;
            }
        }
        public float WorkSummCash       // � ������ (��� ��������) ������� ����� ��� ����� ������
        {
            get
            {
                if (guaranty == true) return 0.0F;
                return WorkSumm;
            }
        }
        public float WorkSummDiscount   // ��������� ������� ����� � ������ ������
        {
            get
            {
                return WorkSumm - WorkSumm / 100.0f * discount;
            }
        }

        #endregion


        public bool IsTo()
        {
            if (tmp_directorywork_code == 1) return true;
            if (tmp_directorywork_code == 2) return true;
            if (tmp_directorywork_code == 3) return true;
            if (tmp_directorywork_code == 4) return true;
            if (tmp_directorywork_code == 5) return true;
            if (tmp_directorywork_code == 6) return true;
            if (tmp_directorywork_code == 725) return true;
            if (tmp_directorywork_code == 737) return true;
            if (tmp_directorywork_code == 738) return true;
            if (tmp_directorywork_code == 739) return true;
            if (tmp_directorywork_code == 740) return true;
            if (tmp_directorywork_code == 741) return true;
            if (tmp_directorywork_code == 9) return true;
            if (tmp_directorywork_code == 460) return true;
            if (tmp_directorywork_code == 517) return true;
            if (tmp_directorywork_code == 727) return true;
            if (tmp_directorywork_code == 518) return true;
            if (tmp_directorywork_code == 728) return true;
            if (tmp_directorywork_code == 730) return true;
            if (tmp_directorywork_code == 729) return true;
            if (tmp_directorywork_code == 732) return true;
            if (tmp_directorywork_code == 731) return true;
            if (tmp_directorywork_code == 742) return true;
            if (tmp_directorywork_code == 733) return true;
            if (tmp_directorywork_code == 734) return true;
            if (tmp_directorywork_code == 736) return true;
            if (tmp_directorywork_code == 735) return true;
            if (tmp_directorywork_code == 748) return true;
            return false;
        }

        //public bool IsWash()
        //{
        //	if (tmp_directorywork_code == 722) return true;
        //	return false;
        //}

        public float WorkToSummCash
        {
            get
            {
                if (IsTo()) return this.WorkSummCash;
                else return 0.0F;
            }
        }
        public float WorkWashSummCash
        {
            get
            {
                if (WorkType() == WORK_TYPE.WASH) return WorkSummCash;
                else return 0.0F;
            }
        }
        public float WorkLaborSummCash
        {
            get
            {
                if (WorkType() == WORK_TYPE.TO) return 0.0F;
                if (WorkType() == WORK_TYPE.WASH) return 0.0F;
                return this.WorkSummCash;
            }
        }
        public string WorkQuontity
        {
            get
            {
                return OperationAmount.ToString();
            }
        }

        public bool IsDiscount => discount == FLOATNULL;
        public float WorkNV => LaborTime == FLOATNULL ? TmpServicePackageLaborTime * OperationAmount : LaborTime * OperationAmount;
        public float LaborTimeInHours => LaborTime == FLOATNULL ? FLOATNULL : LaborTime * OperationAmount;
        public float LaborTimeInServicePackage => LaborTime == FLOATNULL ? TmpServicePackageLaborTime * OperationAmount : FLOATNULL;
        private float ComputeServicePackageRealLaborTime()
        {
            DtWork work = DbSqlWork.Find(CodeWork);
            // ��� ������ ���� ������ ��
            float sp_labor_time = (float)work?.ServicePackageRealLaborTime; // (float)work.GetData("��");
            if (sp_labor_time != FLOATNULL)
            {
                return sp_labor_time;
            }

            long code_collection = work.CodeConnectedWorkCollection;// (long)work.GetData("������_���_���������");
            if (code_collection != 0)
            {
                TmpCodeConnectedWorkCollection = code_collection;
                ArrayList array = new ArrayList();
                DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
                float local_sp = FLOATNULL;
                foreach (DtWorkCollectionItem elm in array)
                {
                    local_sp += (float)elm.GetData("������������_���������_�������");
                }
                return local_sp;
            }
            return FLOATNULL;
        }
        public float TmpServicePackageLaborTime
        {
            get
            {
                return tmp_spnv_ == UNINITIALIZED ? tmp_spnv_ = ComputeServicePackageRealLaborTime() : tmp_spnv_;
            }
            set { tmp_spnv_ = value; }
        }

        public long TmpCodeConnectedWorkCollection
        {
            get { return tmp_code_collection_; }
            set { tmp_code_collection_ = value; }
        }

        #region Calculator of complex parameters
        public float SalaryHours() // ����������� ����� ���������� ������ ������ - ��� ������� �������� � ��������
        {
            if (LaborTime > 0.0F) return LaborTime * OperationAmount;
            DtWork work = DbSqlWork.Find(CodeWork); // ������� ������ � ����������� �������������
            if (work != null)
                return work.TimeNormFact * OperationAmount; // ���������� ����� ��� ������ �����������
            else
                return 0.0F;
        }
        public float SalaryServiceMechanic() // �������� �� ����������� ������ ������������ - ����� �������� ���� ��� ����� �������� � ������
        {
            ArrayList executors = this.Executors;
            int executorsCount = executors.Count;
            float salaryHours = SalaryHours();
            float salary = 0.0F;

            foreach (DtStaff executor in executors)
            {
                salary += executor.Salary * salaryHours / executorsCount;
            }
            return salary;
        }
        public float SalaryServiceManager(DtStaff serviceManager) // �������� �� ���������� ���� ������ ������������� - ��� �������� � 100% �������
        {
            if (serviceManager == null)
                return 0.0F;
            else
                return serviceManager.Salary * BillableHours();
        }
        public float BillableHours() // ������������ �������� ���� - ��� ����, �������� ������ 100%, ����� � ��������
        {
            if (GuaranteeFlag()) return 0.0F;   // �� ��������� ��������
            if (Discount == 100.0F) return 0.0F; // �� ��������� 100% ������
            return SalaryHours();
        }

        public float SummTotal() // ����� � ������ � ������ ������(��� ����� �������� � ������)
        {
            if (Discount == 100.0F) return 0.0F; // �� ��������� 100% ������
            float summToPay;
            if (LaborTime > 0.0F)
                summToPay = LaborTime * OperationAmount * price;
            else
                summToPay = OperationAmount * price;

            summToPay -= summToPay * Discount / 100.0F;

            return summToPay;
        }
        public float PaidAmount() // ����� � ����� ���������� �������� - ���� �������� ����, ���� ������, �� ��������
        {
            if (GuaranteeFlag()) return 0.0F;
            return SummTotal();
        }
        public float OperationTotal()
        {
            if (LaborTime == 0)
                return OperationCost * OperationAmount;
            else
                return LaborTime * OperationAmount * OperationCost;
        }

        #endregion
        #region ������� ������� � �������
        public long CodeWork
        {
            get { return _codeWork; }
            set { _codeWork = value; }
        }

        public bool Oil
        {
            get { return liquid; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public long CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        public int CardYear
        {
            get { return _cardYear; }
            set { _cardYear = value; }
        }
        public float OperationAmount // ���������� �������� ������
        {
            // ��� ����������, ���������, ����� ���������� �������� ���� ���������������
            get { return _quontity; }
            set
            {
                if (value < 0)
                    _quontity = 0.0F;
                else
                    _quontity = value;
            }
        }
        public float LaborTime
        {
            get { return _laborTime; }
            set { _laborTime = value; }
        }

        public float OperationCost// Price
        {
            get { return price; }
            set
            {
                if (value < 0) return; // ��������� �� ����� ���� �������������
                if (price != value)
                {
                    IsChg = true;
                    price = value;
                }
            }
        }
        public float Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public long CodeGuarantyType
        {
            get { return _guarantyType; }
            set { _guarantyType = value; }
        }
        #endregion
        #region ������� ������� � �������
        public string CatalogueNumber
        {
            get
            {
                if (Work == null) return "";
                return Work.CatalogueNumber;
            }
        }
        public DtWork Work
        {
            get
            {
                if (_tmpWork != null) return _tmpWork;
                return _tmpWork = DbSqlWork.Find(CodeWork);
            }
        }
        public ArrayList Executors
        {
            get
            {
                if (_tmpExecutors != null) return _tmpExecutors.Collection;
                _tmpExecutors = new DtStaffCollection(this);
                return _tmpExecutors.Collection;
            }
        }
        public DtGuarantyType GuarantyType
        {
            get
            {
                if (_tmpGuarantyType != null) return _tmpGuarantyType;
                return _tmpGuarantyType = DbSqlGuarantyType.Find(CodeGuarantyType);
            }
        }
        public string WorkName
        {
            get
            {
                if (Work == null) return "������������";
                else return Work.Name;
            }
        }
        #endregion

        #region ������ - �����������
        public bool Executed()
        {
            // Check list of Executors of work. If elements exists - work executed - return true
            if (_tmpExecutors.ExistElements()) return true;
            return false;
        }
        #endregion

        #region ���������� ����������� Calculatable
        public float PriceUnit()
        {
            return price;
        }
        public float TotalAmount()
        {
            if (_laborTime == 0.0f)
                return OperationAmount;
            else
                return OperationAmount * _laborTime;
        }
        public float SummDatabase()
        {
            return PriceUnit() * TotalAmount();
        }
        public float SummDiscount()
        {
            if (discount < 100.0F && discount > 0.0F)
                return SummDatabase() * discount / 100.0F;
            return 0.0F;
        }
        public float SummBonus()
        {
            if (discount == 100.0F) return SummDatabase();
            else return 0.0F;
        }
        public float SimpleAmountUnit()
        {
            if (_laborTime == 0.0F) return 1.0F;
            return _laborTime;
        }
        public float SimpleAmount()
        {
            return OperationAmount;
        }
        public bool GuaranteeFlag()
        {
            return guaranty;
        }
        public WORK_TYPE WorkType()
        {
            if (Work != null) return Work.WorkType;
            else return WORK_TYPE.NONE;
        }
        public DETAIL_TYPE DetailType()
        {
            return DETAIL_TYPE.NONE;
        }

        public float Expenses() { return 0.0F; }
        #endregion
    }

    #region ����� ������������� ��������� ����� ��������
    public class DtCardWorkCollection
    {
        private ArrayList _collection; // ������ ������ ��������
        private readonly DtCard _card; // �������� ��� ������� ������ ������ ������

        public DtCardWorkCollection(DtCard srcDtCard)
        {
            _collection = DbSqlCardWork.Select(srcDtCard);
            _card = srcDtCard;
        }
        public void AddWork(DtCardWork srcCardWork)
        {
            if (_collection == null) _collection = new ArrayList();
            srcCardWork.IsNew = true;   // ������� ����� ������� ��� �����!
            _collection.Add(srcCardWork);
        }
        public void ChangeWork(DtCardWork srcCardWork)
        {
            if (_collection == null) _collection = new ArrayList();
            srcCardWork.IsNew = true;   // ������� ����� ������� ��� �����!
            _collection.Add(srcCardWork);
        }

        public DtCardWork FindWork(DtCardWork workToFind) // ����� ������ � ������ ����� ��������
        {
            return FindWork(workToFind.Position);
        }
        public DtCardWork FindWork(DbCardWork workToFind) // ����� ������ � ������ ����� �������� - ���������� ������!
        {
            return FindWork(workToFind.Number);
        }

        public DtCardWork FindWork(ListViewItem item) // ����� ������ � ������ ����� �������� �� ���������� �������� �����
        {
            if (item == null) return null;
            if (item.Tag == null) return null;
            // ������������, ��� Tag - ��� int ����� ���������
            if (item.Tag.GetType() == typeof(int))
                return FindWork((int)item.Tag);
            else if (item.Tag.GetType() == typeof(DbCardWork))  // ��� �������� �������������
                return FindWork((DbCardWork)item.Tag);
            else
                return null;
        }

        public DtCardWork FindWork(int pos) // ����� ����� � ������ ����� �������� - �� ������ �������
        {
            if (_collection == null) return null;
            foreach (DtCardWork wrk in _collection)
            {
                if (wrk.Position == pos) return wrk;
            }
            return null;
        }

        public ArrayList Collection
        {
            get { return _collection; }
        }
        public int Count
        {
            get
            {
                if (_collection != null)
                    return _collection.Count;
                else
                    return 0;
            }
        }
        public DtCardWork GetWork(int index)
        {
            if (_collection == null) return null;
            return (DtCardWork)_collection[index];
        }
    }
    #endregion

    #region ����� ��� ������ ������ � ���� ������
    public class DtCardWorkDatabaseConnector
    {
        DtCardWork _cardWork;
        public DtCardWorkDatabaseConnector(DtCardWork cardWork)
        {
            _cardWork = cardWork;
        }

        public bool Save()
        {
            if (_cardWork.IsNew)
                return DbSqlCardWork.Create(_cardWork);
            else if (_cardWork.IsChg)
                return DbSqlCardWork.UpdateValues(_cardWork);
            else
                return false;
        }
    }
    #endregion

    #region ����� ��� ����������� ��������� � ���������� WindowsForms
    public class DtCardWorkDisplay
    {
        private DtCardWork _cardWork;
        private DtCardWorkTxt _cardWorkTxt;
        private ListViewItem _conectedItem = null;

        public DtCardWork CardWork
        {
            get { return _cardWork; }
        }
        public DtCardWorkDisplay(DtCardWork cardWork)
        {
            _cardWork = cardWork;
            _cardWorkTxt = new DtCardWorkTxt(cardWork);
        }

        public void SetConnectedLVItem(ListViewItem item)
        {
            _conectedItem = item;
        }

        public void ListViewItem(ListViewItem item)
        {
            /* ����������� ����������� ������ � ������� ������ ListView
			 * �������� ��������� ������ - ����� ������� (Position)
			 */
            if (item == null) return;
            item.SubItems.Clear();
            item.Tag = _cardWork.Position;
            item.Text = (item.Index + 1).ToString();
            item.SubItems.Add(_cardWorkTxt.Text());
            item.SubItems.Add("");
            item.SubItems.Add(_cardWorkTxt.OperationAmount());
            item.SubItems.Add(_cardWorkTxt.LaborTimeUnitCoast());
            item.SubItems.Add(_cardWorkTxt.OperationTotal());
        }

        public void ListViewItem()
        {
            ListViewItem(_conectedItem);
        }
    }
    #endregion

    #region ����� ��� �������� ��������� ����������� ���������� ���������� ��������� ������
    public class DtCardWorkTxt
    {
        private DtCardWork _cardWork;
        public DtCardWorkTxt(DtCardWork cardWork)
        {
            _cardWork = cardWork;
            // �������� �������������� ����������� �� ���� ������ ��� �����������
        }

        public string Text()
        {
            if (_cardWork.Work == null)
                return "";
            else
                return _cardWork.Work.Name;
        }

        public string OperationAmount()
        {
            return _cardWork.OperationAmount.ToString();
        }

        public string OperationCoast()
        {
            return _cardWork.OperationCost.ToString();
        }

        public string LaborTime()
        {
            return _cardWork.LaborTime.ToString();
        }

        public string LaborTimeUnitCoast()
        {
            if (_cardWork.LaborTime == 0)
                return OperationCoast();
            else
                return LaborTime() + "(" + OperationCoast() + ")";
        }

        public string OperationTotal()
        {
            return (_cardWork.OperationTotal()).ToString();

        }
    }
    #endregion
}