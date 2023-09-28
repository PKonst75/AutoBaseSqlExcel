using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
    public class DtTxtCardWork
    {

		private readonly DtCardWork _cardWork;
		private readonly string _operationAmount; // Количество операций
		private readonly string _amount; // Количество работы в единах измерения (Нормачасы или шт для сервисных пакетов)
		private readonly string _price; // Стоимость единицы измерения
		private readonly string _discount; // Скидка или бонус
		private string _workName; // Наименование работы
		private string _catalogueNumber; // Каталожный номер (код) работы
		private string _unit; // Единица измерения
		private readonly string _position; // Позиция в заказ-наряде
		private string _executorsCount; // Количество исполнителей
		private string _executors; // Список исполнителей
		private string _guarantyTypeName; // Наименования вида гарантии

		public DtTxtCardWork(DtCardWork srcCardWork)
		{
			_cardWork = srcCardWork;
			_operationAmount = _cardWork.OperationAmount.ToString();
			if (_cardWork.LaborTime == 0.0F)
			{
				_amount = "1";
				_unit = "ШТ";
			}
			else
			{
				_amount = _cardWork.LaborTime.ToString();//_cardWork.Amount.ToString();
				_unit = "Н/Ч";
			}
			_price = _cardWork.OperationCost.ToString();
			_discount = _cardWork.Discount.ToString();
			_workName = "";
			_catalogueNumber = "";
			_executors = "";
			_executorsCount = "";
			_position = _cardWork.Position.ToString();
			_guarantyTypeName = "";
		}
		#region Простые Геттеры
		public string Position
        {
            get { return _position; }
        }
		public DtCardWork CardWork { get { return _cardWork; } }
		public string OperationAmount{ get { return _operationAmount; } }
		public string Amount { get { return _amount; } }
		public string Price { get { return _price; } }
		public string Discount { get { return _discount; } }
		#endregion
		#region Сложные Геттеры
		public string WorkName
		{
			get
			{
				if (_workName != "") return _workName;
				if (_cardWork.Work == null)
					return _workName = "-";
				return _workName = _cardWork.Work.Name;
			}
		}
		public string CatalogueNumber
		{
			get
			{
				if (_catalogueNumber != "") return _catalogueNumber;
				if (_cardWork.Work == null)
					return _catalogueNumber = "-";
				return _catalogueNumber = _cardWork.Work.CatalogueNumber;
			}
		}
		public string Unit
		{
			get
			{
				if (_unit != "") return _unit;
				if (_cardWork.LaborTime == 0.0F) return _unit = "шт";
				return _unit = "Н/Ч";
			}
        }
		public string Executors
		{
			get
			{
				if (_executors != "") return _executors;
				if (CardWork.Executors == null) return _executors = "Не выполнена";
				foreach (DtStaff staff in CardWork.Executors)
                {
					_executors += new DtTxtStaff(staff).TitleShort + ";";
                }
				return _executors;
			}
		}
		public string GuarantyTypeName
		{
			get
			{
				if (_guarantyTypeName != "") return _guarantyTypeName;
				if (CardWork.GuarantyType == null) return _guarantyTypeName = "НЕТ";
				return _guarantyTypeName = CardWork.GuarantyType.Title();
			}
		}
		public string ExecutorsCount
        {
            get
            {
				if (_executorsCount != "") return _executorsCount;
				return _executorsCount = _cardWork.Executors.Count.ToString();
            }
        }
		#endregion
	}

    #region Отображение в WindowsForm в ListView разных типоы
    public class WfListViewFormCardWorkT01 : DtTxtCardWork, ListViewItemSetting
    {
        private readonly Calculator _calculator = null;
        public WfListViewFormCardWorkT01(DtCardWork srcCardWork, Calculator srcCalculator) : base(srcCardWork) { _calculator = srcCalculator; }
        public void SetListViewItem(ListViewItem srcItem) // Устанавливаем значения для отображение в лист певрнр типа
        {
            srcItem.SubItems.Clear();
            DbCardWork wrk = new DbCardWork(CardWork);
			CalculatorResult result = _calculator.Calculate(CardWork);
            srcItem.Tag = wrk;  // Пока временное решение
            srcItem.Text = (srcItem.Index + 1).ToString();
            srcItem.SubItems.Add(WorkName + " / " + Discount);
            srcItem.SubItems.Add(CatalogueNumber);
            srcItem.SubItems.Add(OperationAmount);
            srcItem.SubItems.Add(Amount + "(" + Price + ")");
			srcItem.SubItems.Add(result.SummDatabase.ToString());
			srcItem.SubItems.Add(Executors);
			if (CardWork.IsNew) srcItem.BackColor = Color.Yellow;
			if (CardWork.Executed()) srcItem.BackColor = Color.LightBlue;
		}
    }
    #endregion
}
