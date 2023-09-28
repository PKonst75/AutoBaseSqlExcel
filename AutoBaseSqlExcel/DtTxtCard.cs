using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	public class DtTxtCard
	{
		private readonly DtCard card;
		private readonly string _number; // Номер карточки
		private readonly string _date; // Дата карточки
		private readonly string _numberDate; // Расширенный номер с датой
		private string _autoModel; // Модель автомобиля
		private string _autoYear; // Год выпуска автомобиля
		private string _licensePlate; // Номрной знак в карточке
		private string _vin; // VIN автомобиля в карточке
		public readonly string _run; // Пробег автомобиля в карточке
		public string _autoSellDate; // Дата продажи автомобиля в карточке
		public string _ownerTitle; // Наименование владельца
		public string _ownerAddress; // Адрес владельца
		public string _ownerContacts; // Контакт владельца
		public string _representTitle; // Наименование представителя
		public string _representTitleShort; // Наименование представителя - краткое (ФИО)
		public string _representAddress; // Адрес представителя
		public string _representContacts; // Контакт представителя
		public string _payerTitle; // Наименование плательщика
		public string _payerAddress; // Адрес плательщика
		public string _serviceManagerShortName; // Фамилия И.О. сервис-консультанта заказ-наряда
		public string _agreedPickUpDateTime; // Согласованние время выдачи
		public string _openDateTime; // Дата и время открытия 
		public string _closeDateTime; // Дата и время закрытия 
		public string _workendDateTime; // Дата и время окончания работ
		public readonly string _comment; // Комментарий к карточке
		public string _masterShortName; // Фамилия И.О. персонала, кто закрыл карточку
		public readonly string _warrantNumber; // Номер заказ-наряда
		public readonly string _cardState; // Статус карточки
		public string _claimsList; // Список заявок в строке
		public string _recomendationsList; // Список рекомендаций в строке

		public DtTxtCard(DtCard srcCard)
		{
			card = srcCard;
			_number = srcCard.Number.ToString();
			_date = srcCard.Date.ToShortDateString();
			_numberDate = _number + " от " + _date;
			_comment = srcCard.Comment;
			_autoModel = "";
			_autoYear = "";
			_licensePlate = "";
			_vin = "";
			_run = card.CardRun.ToString();
			_autoSellDate = "";
			_ownerTitle = "";
			_ownerAddress = "";
			_ownerContacts = "";
			_representTitle = "";
			_representTitleShort = "";
			_representAddress = "";
			_representContacts = "";
			_payerTitle = "";
			_payerAddress = "";
			_serviceManagerShortName = "";
			_agreedPickUpDateTime = "";
			_openDateTime = "";
			_closeDateTime = "";
			_workendDateTime = "";
			_masterShortName = "";
			_warrantNumber = srcCard.WarrantNumber.ToString();
			switch (srcCard.State)
			{
				case DtCard.CardState.OPEND:
					_cardState = "открыт";
					break;
				case DtCard.CardState.CLOSED:
					_cardState = "закрыт";
					break;
				case DtCard.CardState.STOPPED:
					_cardState = "приостановлен";
					break;
				case DtCard.CardState.REOPEND:
					_cardState = "возобновленн";
					break;
				case DtCard.CardState.CANCELED:
					_cardState = "отменен";
					break;
				case DtCard.CardState.CLOSECANCEL:
					_cardState = "вновь открыт";
					break;
				default:
					_cardState = "????";
					break;
			}
			_claimsList = "";
			_recomendationsList = "";
		}

		#region Простые Геттеры
		public DtCard Card
        {
            get { return card; }
        }
		public string CardState	{ get { return _cardState; }}
		public string WarrantNumber { get { return _warrantNumber; } }
		public string Number { get { return _number; } }
		public string Date { get { return _date; }}
		public string NumberDate { get { return _numberDate; } }
		public string Run { get { return _run; } }
		public string Comment { get { return _comment; } }
		#endregion
		#region Сложные Геттеры
		public string MaserShortName
        {
            get
            {
				if (_masterShortName != "") return _masterShortName;
				DtStaff staff = DbSqlStaff.Find(card.MasterCode);
				if (staff == null) return _masterShortName = "-";
				return _masterShortName = staff.Title;
            }
        }
		public string AutoModel
        {
			get
			{
				if (_autoModel != "") return _autoModel;
				if (card.DtAuto == null)
					return _autoModel = "-";
				if(card.DtAuto.AutoModel == null)
					return _autoModel = "--";
				return _autoModel = card.DtAuto.AutoModel.Name;
			}
        }
		public string AutoYear
		{
			get
			{
				if (_autoYear != "") return _autoYear;
				if (card.DtAuto == null)
					return _autoYear = "-";
				if (card.DtAuto.Year == 0)
					return _autoYear = "--";
				return _autoYear = card.DtAuto.Year.ToString();
			}
		}
		public string AutoLicensePlate
		{
            get
			{
				if (_licensePlate != "") return _licensePlate;
				if (card.DtAuto == null)
						return _licensePlate = "-";
				if(card.DtAuto.LicensePlate == null)
					return _licensePlate = "--";
				return _licensePlate = card.DtAuto.LicensePlate.LicensePlateTxt;
			}
        }
		public string AutoVin
		{
			get
			{
				if (_vin != "") return _vin;
				if (card.DtAuto == null)
					return _vin = "-";
				return _vin = card.DtAuto.VIN;
			}
		}
		public string AutoSellDate
        {
            get
            {
				if (_autoSellDate != "") return _autoSellDate;
				if (card.DtAuto == null)
					return _autoSellDate = "-";
				return _autoSellDate = card.DtAuto.SellDateTxt;
			}
        }
		public string OwnerTitle
		{
			get
			{
				if (_ownerTitle != "") return _ownerTitle;
				if (card.Owner == null)
					return _ownerTitle = "-";
				return _ownerTitle = card.Owner.GetTitle();
			}
		}
		public string OwnerAddress
		{
			get
			{
				if (_ownerAddress != "") return _ownerAddress;
				if (card.Owner == null)
					return _ownerAddress = "-";
				return _ownerAddress = card.Owner.GetAddress();
			}
		}
		public string OwnerContacts
		{
			get
			{
				if (_ownerContacts != "") return _ownerContacts;
				if (card.Owner == null)
					return _ownerContacts = "-";
				return _ownerContacts = card.Owner.GetPhone() + " / " + card.Owner.GetMail();
			}
		}
		public string RepresentTitle
		{
			get
			{
				if (_representTitle != "") return _representTitle;
				if (card.Representative == null)
					return _representTitle = OwnerTitle;
				return _representTitle = card.Representative.GetTitle();
			}
		}
		public string RepresentTitleShort
		{
			get
			{
				if (_representTitleShort != "") return _representTitleShort;
				if (card.Representative == null)
				{
					if (card.Owner == null) return _representTitleShort = "-";
					else return _representTitleShort = card.Owner.GetTitleShort();
				}
				return _representTitleShort = card.Representative.GetTitleShort();
			}
		}
		public string RepresentAddress
		{
			get
			{
				if (_representAddress != "") return _representAddress;
				if (card.Representative == null)
					return _representAddress = OwnerAddress;
				return _representAddress = card.Representative.GetAddress();
			}
		}
		public string RepresentContacts
		{
			get
			{
				if (_representContacts != "") return _representContacts;
				if (card.Representative == null)
					return _representContacts = OwnerContacts;
				return _representContacts = card.Representative.GetPhone() + " / " + card.Representative.GetMail();
			}
		}
		public string PayerTitle
		{
			get
			{
				if (_payerTitle != "") return _payerTitle;
				if (card.Owner == null) return _payerTitle = "-";
				if (card.Owner.IsJuridical() && card.PayType == DtCard.PAY_TYPE.CASHLESS)
					return _payerTitle = OwnerTitle;
				else
					return _payerTitle = RepresentTitle;
			}
		}
		public string PayerAddress
		{
			get
			{
				if (_payerAddress != "") return _payerAddress;
				if (card.Owner == null) return _payerAddress = "-";
				if (card.Owner.IsJuridical() && card.PayType == DtCard.PAY_TYPE.CASHLESS)
					return _payerAddress= OwnerAddress;
				else
					return _payerAddress= RepresentAddress;
			}
		}
		public string CardPersonTitle
		{
			get
			{
				if (card.Representative == null) return OwnerTitle;
				return RepresentTitle;
			}
		}
		public string ServiceManagerShortName
		{
			get
			{
				if (_serviceManagerShortName != "") return _serviceManagerShortName;
				if (card.ServiceManager == null)
					return _serviceManagerShortName = "-";
				return _serviceManagerShortName = card.ServiceManager.Title;
			}
		}
		public string AcceptanceDateTime
        {
            get
            {
				if (_openDateTime != "") return _openDateTime;
				DtCardAction open;
				if ((open = card.Actions.FindOpen()) == null) return "Не открыт";
				return _openDateTime = open.Date.ToString();
			}
        }
		public string OpenDateTime
		{
			get
			{
				if (_openDateTime != "") return _openDateTime;
				DtCardAction open;
				if ((open = card.Actions.FindOpen()) == null) return "Не открыт";
				return _openDateTime = open.Date.ToString();
			}
		}
		public string CloseDateTime
		{
			get
			{
				if (_closeDateTime != "") return _closeDateTime;
				DtCardAction close;
				if ((close = card.Actions.FindClose()) == null) return "Не закрыт";
				return _closeDateTime = close.Date.ToString();
			}
		}
		public string WorkendDateTime
		{
			get
			{
				if (_workendDateTime != "") return _workendDateTime;
				DtCardAction workend;
				if ((workend = card.Actions.FindEndwork()) == null) return "Не установленно";
				return _workendDateTime = workend.Date.ToString();
			}
		}
		public string AgreedPickUpDateTime
		{
			get
			{
				if (_agreedPickUpDateTime != "") return _agreedPickUpDateTime;
				if (card.IsAgreedPickUpTime)
					return _agreedPickUpDateTime = card.AgreedPickUpTime.ToString();
				else
					return _agreedPickUpDateTime = "Не согласованно!";
			}
		}
		public string ClaimsList
        {
            get
            {
				if(_claimsList != "") return _claimsList;
				if (card.Claims == null) return _claimsList = "-";
				if(card.Claims.Collection == null) return _claimsList = "-";
				if(card.Claims.Collection.Count == 0) return _claimsList = "-";
				foreach(DtCardClaim claim in Card.Claims.Collection)
                {
					DtTxtCardClaim txtCardClaim = new DtTxtCardClaim(claim);
					if(_claimsList == "")
						_claimsList = txtCardClaim.CardClaimText;
					else
						_claimsList += "; " + txtCardClaim.CardClaimText;
                }
				return _claimsList;
			}
        }
		public string RecomendationsList
		{
			get
			{
				if (_recomendationsList != "") return _recomendationsList;
				ArrayList recomends = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomends, card.Number, card.Year);
				foreach (DtCardRecomendation recomend in recomends)
				{
					if (_recomendationsList == "")
						_recomendationsList = recomend.RecomendationTxt;
					else
						_recomendationsList += ";" + recomend.RecomendationTxt;
				}
				if (_recomendationsList != "") return _recomendationsList + ";";
				else return _recomendationsList = "-";			
			}
		}
		#endregion
		#region Заполнение специальных элементов типа ListViewItem и TreeViewItem
		public static ListViewItem MakeLV_List(DtCard srcCard)
		{
			DtTxtCard txtCard = new DtTxtCard(srcCard);
			ListViewItem item = new ListViewItem();
			item.Tag = srcCard.CardCode;
			item.Text = txtCard.Number;
			item.SubItems.Add(txtCard.Date);
			item.SubItems.Add(txtCard.WarrantNumber);
			item.SubItems.Add(txtCard.OwnerTitle);
			item.SubItems.Add(txtCard.AutoModel);
			item.SubItems.Add(txtCard.AutoVin);
			item.SubItems.Add(txtCard.AutoLicensePlate);
			item.SubItems.Add(txtCard.Run);
			item.SubItems.Add(txtCard.ServiceManagerShortName + "/" + txtCard.MaserShortName);
			item.SubItems.Add(txtCard.Comment);
			switch (srcCard.State)
			{
				case DtCard.CardState.OPEND:
					item.BackColor = Color.Yellow;
					break;
				case DtCard.CardState.CLOSED:
					item.BackColor = Color.LightGreen;
					break;
				case DtCard.CardState.STOPPED:
					item.BackColor = Color.Red;
					break;
				case DtCard.CardState.REOPEND:
					item.BackColor = Color.Yellow;
					break;
				case DtCard.CardState.CANCELED:
					item.BackColor = Color.Gray;
					break;
				default:
					break;
			}
			// Обработка наличного/безналичного расчета
			switch (srcCard.StateControl)
			{
				case 0:
					item.StateImageIndex = 0;
					break;
				case 1:
					item.StateImageIndex = 1;
					break;
				case 2:
					item.StateImageIndex = 2;
					break;
				default:
					item.StateImageIndex = 0;
					break;
			}
			return item;
		}
        #endregion
    }

	#region Отображение в WindowsForm в ListView разных типоы
	public class WfListViewForm : DtTxtCard
	{
		public WfListViewForm(DtCard srcCard) : base(srcCard) { }
		public void SetListViewItemT01(ListViewItem srcItem) // Устанавливаем значения для отображение в лист певрнр типа
		{
			srcItem.SubItems.Clear();
			srcItem.Tag = Card.CardCode;
			srcItem.Text = Number;
			srcItem.SubItems.Add(Date);
			srcItem.SubItems.Add(WarrantNumber);
			srcItem.SubItems.Add(OwnerTitle);
			srcItem.SubItems.Add(AutoModel);
			srcItem.SubItems.Add(AutoVin);
			srcItem.SubItems.Add(AutoLicensePlate);
			srcItem.SubItems.Add(Run);
			srcItem.SubItems.Add(ServiceManagerShortName + "/" + MaserShortName);
			srcItem.SubItems.Add(Comment);
			switch (Card.State)
			{
				case DtCard.CardState.OPEND:
					srcItem.BackColor = Color.Yellow;
					break;
				case DtCard.CardState.CLOSED:
					srcItem.BackColor = Color.LightGreen;
					break;
				case DtCard.CardState.STOPPED:
					srcItem.BackColor = Color.Red;
					break;
				case DtCard.CardState.REOPEND:
					srcItem.BackColor = Color.Yellow;
					break;
				case DtCard.CardState.CANCELED:
					srcItem.BackColor = Color.Gray;
					break;
				default:
					break;
			}
		}
	}
	#endregion
}
