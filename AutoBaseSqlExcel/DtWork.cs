using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
    /// <summary>
    /// Summary description for DtWork.
    /// </summary>
    /// 
    public enum WORK_TYPE : short { NONE = 0, TO = 1, WASH = 2, PPP = 3 }
    public class DtWork
    {
        private string _name; // ������������
        private long _codeDirectoryWork; // ��� ������ �� ����������� (����, �� � �.�)
        string _catalogueNumber; // ���������� ����� ������

        long code;                  // ���������� ��� ������������
        long code_work_group;       // ������ �������������

        string code_detail;         // ��� ������
        string code_work;               // ��� ������

        string description;         // ��������
        float val;                  // ������������
        float price;                    // ��������� ���������
        float price_guaranty;           // ��������� ��������� - ��������

        private long code_collection_;      // ��� ������ ���������� � �������
        private float nv_;                      // ����� ������� (��������) ��� ������ ������


        public DtWork()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region ������� � �������
        public float TimeNorm // ����������� ����� ������� ��� ���������� ������ ������ - ��� ������� �� �����
        {
            get { return val; }
        }
        public float TimeNormFact // ����������� ����� ������� ��� ���������� ������ ������ ����������� - ��� ������� ��������
        {
            get
            {
                if (nv_ == 0.0F) return val;
                else return nv_;
            }
        }
        public string Name
        {
            get { return _name; }
        }
        public long CodeDirectoryWork
        {
            get { return _codeDirectoryWork; }
            set { _codeDirectoryWork = value; }
        }
        public string CatalogueNumber
        {
            get { return _catalogueNumber; }
            set
            {
                _catalogueNumber = value;
                _catalogueNumber.Trim();
            }
        }

        public float ServicePackageRealLaborTime
        {
            get { return nv_; }
            set { nv_ = value; }
        }
        public long CodeConnectedWorkCollection
        {
            get { return code_collection_; }
            set { code_collection_ = value; }
        }
        #endregion

        public object GetData(string data)
        {
            switch (data)
            {
                case "���_������������":
                    return (object)(long)code;
                case "������_���_����������_���":
                    return (object)(long)code_work_group;
                case "�����_�������":
                    return (object)(string)CatalogueNumber;
                case "���_������_�����":
                    return (object)(string)code_detail;
                case "���_������_�����":
                    return (object)(string)code_work;
                case "������������":
                    return (object)(string)_name;
                case "��������":
                    return (object)(string)description;
                case "������������":
                    return (object)(float)val;
                case "��������":
                    return (object)(float)price;
                case "��������_�������":
                    return (object)(float)price_guaranty;
                case "������_���_����������_������������":
                    return (object)CodeDirectoryWork;
                case "������_���_���������":
                    return (object)(long)CodeConnectedWorkCollection; //code_collection;
                case "��":
                    return (object)(float)ServicePackageRealLaborTime;// nv;
                default:
                    return (object)null;
            }
        }

        public void SetData(string data, object val)
        {
            switch (data)
            {
                case "���_������������":
                    code = (long)val;
                    break;
                case "������_���_����������_���":
                    code_work_group = (long)val;
                    break;
                case "�����_�������":
                    CatalogueNumber = (string)val;
                    break;
                case "���_������_�����":
                    code_detail = (string)val;
                    code_detail = code_detail.Trim();
                    break;
                case "���_������_�����":
                    code_work = (string)val;
                    code_work = code_work.Trim();
                    break;
                case "������������":
                    _name = (string)val;
                    _name = _name.Trim();
                    break;
                case "��������":
                    description = (string)val;
                    description = description.Trim();
                    break;
                case "������������":
                    this.val = (float)val;
                    break;
                case "��������":
                    price = (float)val;
                    break;
                case "��������_��������":
                    price_guaranty = (float)val;
                    break;
                case "������_���_����������_������������":
                    CodeDirectoryWork = (long)val;
                    break;
                case "������_���_���������":
                    CodeConnectedWorkCollection = (long)val; // code_collection = (long)val;
                    break;
                case "��":
                    ServicePackageRealLaborTime = (float)val;// nv = (float)val;
                    break;
                default:
                    break;
            }
        }

        public void SetLVItem(ListViewItem item)
        {
            item.SubItems.Clear();      // ����� ������� ���������� ���������� � ���������
            string txt;

            item.Tag = this.code;
            item.Text = this.CatalogueNumber;
            item.SubItems.Add(this.code_detail);
            item.SubItems.Add(this.code_work);
            item.SubItems.Add(this._name);
            item.SubItems.Add(this.val.ToString());
            item.SubItems.Add(this.price.ToString() + "/" + this.price_guaranty.ToString());
            if (this.val == 0.0F)
                txt = Db.CachToTxt(this.price);
            else
                txt = Db.CachToTxt(this.price * this.val);
            item.SubItems.Add(txt);
            if (CodeConnectedWorkCollection > 0)
                txt = "+";
            else
                txt = "";
            item.SubItems.Add(txt);
            if (ServicePackageRealLaborTime != 0.0F)
                txt = ServicePackageRealLaborTime.ToString();
            else
                txt = "";
            item.SubItems.Add(txt);
        }

        public WORK_TYPE WorkType // ���������� ���� ������
        {
            get
            {
                switch (CodeDirectoryWork)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 9:
                    case 460:
                    case 517:
                    case 518:
                    case 725:
                    case 737:
                    case 738:
                    case 739:
                    case 740:
                    case 741:
                    case 727:
                    case 728:
                    case 729:
                    case 730:
                    case 731:
                    case 732:
                    case 733:
                    case 734:
                    case 735:
                    case 736:
                    case 742:
                    case 748:
                        return WORK_TYPE.TO;
                    case 188:
                        return WORK_TYPE.PPP;
                    case 722:
                        return WORK_TYPE.WASH;
                    default:
                        return WORK_TYPE.NONE;
                }
            }
        }
    }
}
