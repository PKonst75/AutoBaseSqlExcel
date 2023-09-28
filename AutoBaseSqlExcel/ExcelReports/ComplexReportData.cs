using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardReport.
	/// </summary>
	public class ComplexReportData
	{
		private readonly DtCard _dtCard; // Карточка по новой схеме
		private readonly DtTxtCard _txtCard; // Для отображения карточки в текст
		private readonly CalculatorCard _calculatorCardPay; // Оплачиваемая часть карточки
		private readonly CalculatorCard _calculatorCardPayTO; // Оплачиваемая часть карточки - работы по ТО
		private readonly CalculatorCard _calculatorCardPayWash; // Оплачиваемая часть карточки - мойка
		private readonly  CalculatorCard _calculatorCardPayOil; // Оплачиваемая часть карточки - масла
		private readonly CalculatorCard _calculatorCardDatabase; // Чистый рассчет без учета НДС, Гарантии и всего остального

		public class Guaranty
		{
			public long		guaranty_type;				// Выид гарантии, для которого работает структура
			public string	guaranty_name;				// Наименование вида гарантии
			public bool		is_work_guaranty;			// Флаг наличия Гарантийных работ
			public float	summ_to_guaranty;			// Сумма за ТО по Гарантии
			public float	summ_work_guaranty;			// Сумма гарантийных работ
			public float	summ_wash_guaranty;			// Сумма моечных_гарантия
			public float	guaranty_value;				// Гарантийный нормачас
			public bool		is_detail_guaranty;			// Флаг наличия Гарантийных деталей
			public float	summ_oil_guaranty_input;	// Сумма масел - входные цены по гарантии
			public float	summ_detail_guaranty_input; // Сумма деталей - входные цены по гарантии
		}
		private ArrayList guaranty_array = new  ArrayList();

		// Исходные параметры
		private DbCard				card;
		private ArrayList			cardWorks;
		private ArrayList			cardDetails;
		
		//Расчетные параметры
		private float				summ_to;				// Сумма за ТО
		private float				summ_to_guaranty;		// Сумма за ТО по Гарантии
		private float				summ_work;				// Сумма остальных работ
		private float				summ_work_guaranty;		// Сумма гарантийных работ
		private float				summ_wash;				// Сумма моечных
		private float				summ_wash_guaranty;		// Сумма моечных_гарантия
		private float				guaranty_value;			// Гарантийный нормачас
		private float				summ_oil;				// Сумма масел
		private float				summ_oil_guaranty;		// Сумма масел Гарантия
		private float				summ_detail;			// Сумма деталей
		private float				summ_detail_guaranty;	// Сумма деталей Гарантия
		private bool				is_ppp;					// Флаг наличия предпродажной подготовки
		private bool				is_work_guaranty;		// Флаг наличия Гарантийных работ
		private bool				is_detail_guaranty;		// Флаг наличия Гарантийных деталей
		private int					count_detail;			// Подсчет количества деталей
		private int					count_detail_guaranty;	// Подсчет количества Гарантийных деталей
		private float				summ_oil_input;
		private float				summ_detail_input;
		private float				summ_oil_guaranty_input;
		private float				summ_detail_guaranty_input;

		private float summ_payed_hours_minus_discount_; // Оплаченные часы с учетом индексации скидок
		private float summ_hours_; // Закрытые негарантийные часы

		private float summ_discount_work;
		private float summ_discount_detail;
		private float summ_pay_worktooil;

		private Dictionary<long, Guaranty> MakeGuarantiesDictianory(ArrayList works)
        {
			Dictionary<long, Guaranty> guaranty_in_card = new Dictionary<long, Guaranty>();
			foreach (DtCardWork work in works)
			{
				if (work.GuaranteeFlag())
				{
					long guaranty_type = work.CodeGuarantyType;
					if (!guaranty_in_card.ContainsKey(guaranty_type))
					{
						guaranty_in_card[guaranty_type] = new Guaranty
						{
							guaranty_type = guaranty_type,
							guaranty_name = new DtTxtCardWork(work).GuarantyTypeName
						};
					}
				}
			}
			return guaranty_in_card;
		}

		private ArrayList MakeFilledGuarantyArray(DtCard card)
        {
			ArrayList dtCardWorks = DbSqlCardWork.Select(_dtCard);
			ArrayList guaranties = new ArrayList();

			foreach (KeyValuePair<long, Guaranty> pair in MakeGuarantiesDictianory(dtCardWorks))
			{
				Guaranty gar = pair.Value;
				CalculatorResult resTo = _calculatorCardDatabase.CalculateWorkGuaranty(dtCardWorks, WORK_TYPE.TO, gar.guaranty_type);
				gar.summ_to_guaranty += resTo.SummTotal;
				CalculatorResult resWash = _calculatorCardDatabase.CalculateWorkGuaranty(dtCardWorks, WORK_TYPE.WASH, gar.guaranty_type);
				gar.summ_wash_guaranty += resWash.SummTotal;
				CalculatorResult res = _calculatorCardDatabase.CalculateWorkGuaranty(dtCardWorks, WORK_TYPE.NONE, gar.guaranty_type);
				gar.summ_work_guaranty += res.SummTotal - resTo.SummTotal - resWash.SummTotal;
				guaranties.Add(gar);
			}
			return guaranties;
		}

		public ComplexReportData(DbCard cardSrc)
		{

			_dtCard = DbSqlCard.Find(cardSrc.Number, cardSrc.Year);
			_txtCard = new DtTxtCard(_dtCard);
			_calculatorCardPay = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
			_calculatorCardPayTO = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAYTO, VAT_TYPE.VAT_NON, 0);
			_calculatorCardPayWash = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAYWASH, VAT_TYPE.VAT_NON, 0);
			_calculatorCardDatabase = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_DATABASE, VAT_TYPE.VAT_NON, 0);
			_calculatorCardPayOil = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAYOIL, VAT_TYPE.VAT_NON, 0);

			_calculatorCardPay.Calculate(_dtCard);
			_calculatorCardPayTO.Calculate(_dtCard);
			_calculatorCardPayWash.Calculate(_dtCard);
			_calculatorCardPayOil.Calculate(_dtCard);
			_calculatorCardDatabase.Calculate(_dtCard);

			guaranty_array = MakeFilledGuarantyArray(_dtCard);

			summ_payed_hours_minus_discount_ = _dtCard.ComputePayedHoursMinusDiscout();
			summ_hours_ = _dtCard.ComputePayedHours();

			card		= cardSrc;
			// Получаем список работ данной карточки
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);
			// Получаем список деталей данной карточки
			cardDetails = new ArrayList();
			DbCardDetail.FillList(cardDetails, card);
			// Получаем список рекомендаций данной карточки
			//cardRecomend = new ArrayList();
			//DbCardRecomend.FillArray(cardRecomend, card);

			// Получение необходимых расчетных параметров
			// Сумма работ
			summ_to					= 0;
			summ_work				= 0;
			summ_work_guaranty		= 0;
			guaranty_value			= 0;
			count_detail_guaranty	= 0;
			count_detail			= 0;
			//summ_payed_hours_minus_discount_ = 0; 
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				DtCardWork cardWork = DbSqlCardWork.Find(card, wrk.Number);
				DtTxtCardWork txtCardWork = new DtTxtCardWork(cardWork);

				if (cardWork.WorkType() == WORK_TYPE.TO)//if(IsTO(wrk) == true)
					summ_to += wrk.Summ;
				else 
					summ_work += wrk.Summ;
				if(cardWork.WorkType() == WORK_TYPE.WASH)//if (IsWash(wrk) == true)
					summ_wash += wrk.Summ;
				if(cardWork.GuaranteeFlag())//if(wrk.Guaranty == true)
				{
					is_work_guaranty = true;
					if(cardWork.WorkType() == WORK_TYPE.TO)//if(IsTO(wrk) == true) 
						summ_to_guaranty += wrk.SummFull;
					else
						summ_work_guaranty += wrk.SummFull;
					if(cardWork.WorkType() == WORK_TYPE.WASH)//if(IsWash(wrk) == true)
						summ_wash_guaranty += wrk.SummFull;
					if(wrk.Val != 0) guaranty_value = wrk.Val;

				
				}
				if (cardWork.WorkType() == WORK_TYPE.PPP) is_ppp = true;
			}
			// Сумма деталей
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Oil == true)
				{
					summ_oil += dtl.Summ;
					if(dtl.Guaranty == false)
						summ_oil_input += dtl.InputSumm;
				}
				else
				{
					summ_detail += dtl.Summ;
					if(dtl.Guaranty == false)
						summ_detail_input += dtl.InputSumm;
				}
				if(dtl.Guaranty == true)
				{
					is_detail_guaranty = true;
					count_detail_guaranty++;
					if(dtl.Oil == true)
					{
						summ_oil_guaranty += dtl.SummWhole;
						summ_oil_guaranty_input += dtl.InputSumm;
					}
					else
					{
						summ_detail_guaranty += dtl.SummWhole;
						summ_detail_guaranty_input += dtl.InputSumm;
					}
					
				}
				else
				{
					count_detail++;
				}
			}


			// Новый вариант рассчета
			
			summ_to = _calculatorCardPayTO.SummWork();
			summ_wash = _calculatorCardPayWash.SummWork();
			summ_work = _calculatorCardPay.SummWork() - summ_to - summ_wash;

			summ_oil = _calculatorCardPayOil.SummDetail();
			summ_detail = _calculatorCardPay.SummDetail() - summ_oil;
			summ_oil_input = _calculatorCardPayOil.SummDetailExpences();
			summ_detail_input = _calculatorCardPay.SummDetailExpences() - summ_oil_input;

			summ_discount_work = _calculatorCardPay.SummWorkDiscount();
			summ_discount_detail = _calculatorCardPay.SummDetailDiscount();

			summ_pay_worktooil = _calculatorCardPay.SummPayWork() + _calculatorCardPayOil.SummPayDetail();
		}

		// Отображение необходимых данных
		public int CountDetail
		{
			get
			{
				return count_detail;
			}
		}
		public int CountDetailGuaranty
		{
			get
			{
				return count_detail_guaranty;
			}
		}
		public string WarrantNumber
		{
			get
			{
				return card.WarrantNumber.ToString();
			}
		}
		public float WorkD
		{
			get
			{
				float summ = summ_work + summ_to + summ_oil + summ_wash;
				return summ;
			}
		}
		// Отображение необходимых данных
		public float PayedHoursMinusDiscount
        {
            get { return summ_payed_hours_minus_discount_; }
        }
		public float PayedHours
		{
			get { return summ_hours_; }
		}
		public string IsGuarantyWorkDetailTxt
		{	
			get
			{
				string txt;
				if(is_work_guaranty == true)
					txt		= "ДА/";
				else
					txt		= "НЕТ/";
				if(is_detail_guaranty == true)
					txt		+= "ДА";
				else
					txt		+= "НЕТ";
				return txt;
			}
		}
		public bool IsGuarantyWorkDetail
		{	
			get
			{
				if(is_work_guaranty == true) return true;
				if(is_detail_guaranty == true) return true;
				return false;
			}
		}
		public float CardTOD
		{
			get
			{
				return summ_to;
			}
		}
		public string CardGuarantyType
		{
			get
			{
				return card.GuarantyTypeTxt;
			}
		}
		public float CardTOGuarantyD
		{
			get
			{
				return summ_to_guaranty;
			}
		}
		public float CardWorkD
		{
			get
			{	
				return summ_work;
			}
		}
		public float CardDiscountD
		{
			get
			{
				return summ_discount_work;
			}
		}
		public float CardPayD
		{
			get
			{
				return summ_pay_worktooil;
			}
		}
		public float CardWashD
		{
			get
			{
				return summ_wash;
			}
		}
		public string CardWorkNoWash
		{
			get
			{
				if(summ_work - summ_wash== 0) return "";
				return (summ_work-summ_wash).ToString();
			}
		}
		public float CardWorkNoWashD
		{
			get
			{
				return summ_work;
			}
		}
		public float CardWorkNoWashGuarantyD
		{
			get
			{
				return (summ_work_guaranty - summ_wash_guaranty);
			}
		}
		public float CardWashGuarantyD
		{
			get
			{
				return summ_wash_guaranty;
			}
		}
		public float CardWorkGuarantyD
		{
			get
			{
				return summ_work_guaranty;
			}
		}
		public float CardDetailD
		{
			get
			{
				return summ_detail - summ_discount_detail;
			}
		}
		public float CardDetailInputD
		{
			get
			{
				return summ_detail_input;
			}
		}
		public float CardDetailGuarantyD
		{
			get
			{ 
				return summ_detail_guaranty;
			}
		}
		public float CardDetailGuarantyInputD
		{
			get
			{
				return summ_detail_guaranty_input;
			}
		}
		public float CardOilD
		{
			get
			{
				
				return summ_oil;
			}
		}
		public float CardOilInputD
		{
			get
			{
				
				return summ_oil_input;
			}
		}
		public float CardOilGuarantyD
		{
			get
			{
				
				return summ_oil_guaranty;
			}
		}
		public float CardOilGuarantyInputD
		{
			get
			{
				return summ_oil_guaranty_input;
			}
		}
		public string AutoModel
		{
			get
			{
				return card.Auto.ModelTxt;
			}
		}
		public string AutoVIN
		{
			get
			{
				return card.Auto.Vin;
			}
		}
		public string AutoSignNo
		{
			get
			{
				return card.Auto.SignNo;
			}
		}
		public long CardWorkshop
		{
			get
			{
				return card.CodeWorkshop;
			}
		}
		public string CardWorkshopTxt
		{
			get
			{
				if(card.CodeWorkshop == 0) return "Неустановлен";
				if(card.CodeWorkshop == 1) return "Сервис";
				if(card.CodeWorkshop == 2) return "Тюнинг";
				if(card.CodeWorkshop == 3) return "Малярка";
				if(card.CodeWorkshop == 4) return "Кузня";
				if(card.CodeWorkshop == 5) return "Мойка";
				if(card.CodeWorkshop == 6) return "Антикор";
				if(card.CodeWorkshop == 7) return "Музыка";
				return "Ошибка";
			}
		}

		public string CloseDate
		{
			get
			{
				return card.WarrantCloseShortTxt;
			}
		}
		public string NumberTxt
		{
			get
			{
				return card.Number.ToString();
			}
		}
		public string WarrantDate
		{
			get
			{
				return card.WarrantDateTxt;
			}
		}
		// Анализ выгрузок
		public bool HaveTO()
		{
			if(summ_to > 0) return true;
			if(summ_oil > 0) return true;
			if(summ_detail > 0) return true;
			if(summ_work > 0) return true;
			return false;
		}
		public bool HaveGuaranty()
		{
			if(summ_to_guaranty > 0) return true;
			if(summ_oil_guaranty > 0) return true;
			if(summ_detail_guaranty > 0) return true;
			if(summ_work_guaranty > 0) return true;
			if(is_work_guaranty == true) return true;
			if(is_detail_guaranty == true) return true;
			return false;
		}
		public bool HavePPP()
		{
			if(is_ppp == true) return true;
			return false;
		}
		public bool Is_Cashless()
		{
			return card.Cashless;
		}

		public ArrayList CardDetails
		{
			get
			{
				return cardDetails;
			}
		}

		public ArrayList GuarantyArray
		{
			get
			{
				return guaranty_array;
			}
		}
		public string ServiceManager
		{
			get
			{
				return _txtCard.ServiceManagerShortName;
			}
		}
	}
}
