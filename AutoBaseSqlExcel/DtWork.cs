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
        private string _name; // Наименование
        private long _codeDirectoryWork; // Код работы из справочника (мока, ТО и д.д)
        string _catalogueNumber; // Католожный номер работы

        long code;                  // Уникальный код трудоемкости
        long code_work_group;       // Группа трудоемкостей

        string code_detail;         // Код детали
        string code_work;               // Код работы

        string description;         // Описание
        float val;                  // Трудоемкость
        float price;                    // Стоимость нормачаса
        float price_guaranty;           // Стоимость нормачаса - гарантия

        private long code_collection_;      // Код набора связанного с работой
        private float nv_;                      // Норма времени (реальная) для сервис пакета


        public DtWork()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region геттеры и сеттеры
        public float TimeNorm // Стандартная норма времени для выполнения данной работы - при расчете по часам
        {
            get { return val; }
        }
        public float TimeNormFact // Стандартная норма времени для выполнения данной работы фактическая - для расчета зарплаты
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
                case "КОД_ТРУДОЕМКОСТЬ":
                    return (object)(long)code;
                case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП":
                    return (object)(long)code_work_group;
                case "НОМЕР_ПОЗИЦИЯ":
                    return (object)(string)CatalogueNumber;
                case "КОД_ДЕТАЛЬ_ТЕКСТ":
                    return (object)(string)code_detail;
                case "КОД_РАБОТА_ТЕКСТ":
                    return (object)(string)code_work;
                case "НАИМЕНОВАНИЕ":
                    return (object)(string)_name;
                case "ОПИСАНИЕ":
                    return (object)(string)description;
                case "ТРУДОЕМКОСТЬ":
                    return (object)(float)val;
                case "НОРМАЧАС":
                    return (object)(float)price;
                case "НОРМАЧАС_ГАРАНТЯ":
                    return (object)(float)price_guaranty;
                case "ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ":
                    return (object)CodeDirectoryWork;
                case "ССЫЛКА_КОД_КОЛЛЕКЦИЯ":
                    return (object)(long)CodeConnectedWorkCollection; //code_collection;
                case "НВ":
                    return (object)(float)ServicePackageRealLaborTime;// nv;
                default:
                    return (object)null;
            }
        }

        public void SetData(string data, object val)
        {
            switch (data)
            {
                case "КОД_ТРУДОЕМКОСТЬ":
                    code = (long)val;
                    break;
                case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП":
                    code_work_group = (long)val;
                    break;
                case "НОМЕР_ПОЗИЦИЯ":
                    CatalogueNumber = (string)val;
                    break;
                case "КОД_ДЕТАЛЬ_ТЕКСТ":
                    code_detail = (string)val;
                    code_detail = code_detail.Trim();
                    break;
                case "КОД_РАБОТА_ТЕКСТ":
                    code_work = (string)val;
                    code_work = code_work.Trim();
                    break;
                case "НАИМЕНОВАНИЕ":
                    _name = (string)val;
                    _name = _name.Trim();
                    break;
                case "ОПИСАНИЕ":
                    description = (string)val;
                    description = description.Trim();
                    break;
                case "ТРУДОЕМКОСТЬ":
                    this.val = (float)val;
                    break;
                case "НОРМАЧАС":
                    price = (float)val;
                    break;
                case "НОРМАЧАС_ГАРАНТИЯ":
                    price_guaranty = (float)val;
                    break;
                case "ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ":
                    CodeDirectoryWork = (long)val;
                    break;
                case "ССЫЛКА_КОД_КОЛЛЕКЦИЯ":
                    CodeConnectedWorkCollection = (long)val; // code_collection = (long)val;
                    break;
                case "НВ":
                    ServicePackageRealLaborTime = (float)val;// nv = (float)val;
                    break;
                default:
                    break;
            }
        }

        public void SetLVItem(ListViewItem item)
        {
            item.SubItems.Clear();      // Чтобы сделать однотипным добавление и изменение
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

        public WORK_TYPE WorkType // Получаение Типа работы
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
