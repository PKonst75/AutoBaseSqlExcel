using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AutoBaseSql
{
    // Класс отвечает за отображение пареметров объектов типа DtCard
    class DisplayDtCard
    {
        private DtCard _dtCard;
        public DisplayDtCard(DtCard srcDtCard)
        {
            _dtCard = srcDtCard;
        }
        public DtCard Card
        {
            get { return _dtCard; }
        }
        public FormDisplay.FormDisplayStruct Representative() // Отображение заголовка представителя
        { 
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtPartner owner = Card.Owner;
            if (owner == null) return displayStruct;    // Если нет владельца, то о представителе пока разговора нет
            DtPartner representative = Card.Representative;
            if (representative == null)
            {
                if (owner.IsJuridical())
                    displayStruct.SetRedText("Выбирете представителя юридического лица");
                else
                    displayStruct.SetGreenText("Автомобиль представляет владелец");
            }
            else
                displayStruct.SetGreenText(DisplayDtPartner.TitleAddress(representative));
             return displayStruct;
        }
        public FormDisplay.FormDisplayStruct RepresentativeContact() // Отображение контактов представителя
        {
            DisplayDtPartner displayerParner = new DisplayDtPartner(Card.Representative);
            return displayerParner.Contacts();
        }
        public FormDisplay.FormDisplayStruct Owner() // Отображение заголовка владельца
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtPartner owner = Card.Owner;
            if (owner == null)
                displayStruct.SetRedText("Выберите владельца автомобиля");
            else
                displayStruct.SetGreenText(DisplayDtPartner.TitleAddress(owner));
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct OwnerContact() // Отображение контактов владельца
        {
            DisplayDtPartner displayerParner = new DisplayDtPartner(Card.Owner);
            return displayerParner.Contacts();
        }
        public FormDisplay.FormDisplayStruct CardWorkDiscount() // Отображение скидки по умолчанию карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            displayStruct.SetBlueText("Скидка на работы: " + Card.Discount.ToString() + "%");
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardWorkHourPrice() // Отображение стоимости нормачаса карточки по умолчанию
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            displayStruct.SetBlueText("Стоимость Н/Ч: " + Card.HourPrice.ToString() + " руб.");
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardTitle() // Отображение заголовка карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.IsNew) displayStruct.SetText("Новая карточка заказа");
            else
            {
                switch (Card.State)
                {
                    case DtCard.CardState.CLOSED:
                        displayStruct.SetText("Закрытый заказ-наряд №" + Card.WarrantNumber.ToString() + "/" + Card.Year.ToString());
                        break;
                    case DtCard.CardState.OPEND:
                        displayStruct.SetText("Открытый заказ-наряд №" + Card.WarrantNumber.ToString() + "/" + Card.Year.ToString());
                        break;
                    default:
                        displayStruct.SetText("Карточка №" + Card.Number.ToString() + "/" + Card.Year.ToString());
                        break;
                }
                if (Card.IsChg) displayStruct.SetText(displayStruct.Text + " ***");
            }
            return displayStruct;
        }
        public  FormDisplay.FormDisplayStruct RepresentativeDocuments() // Отображение документа представителя
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.RepresentativeDocs != "")
                displayStruct.SetBlueText("Документ представителя: " + Card.RepresentativeDocs);
            else
                displayStruct.SetBlueText(Card.RepresentativeDocs);
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct AutoType() // Отображение типа автомобиля
        {  
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.AutoType != null)
            {
                displayStruct.SetBlueText("Трудоемкость: " + Card.AutoType.NameTxt);
            }
            else
            {
                displayStruct.SetRedText("Выбирете трудоемкость по умолчанию");
            }
            return displayStruct;
        }
        public  FormDisplay.FormDisplayStruct CardWorkshop() // Отображение типа автомобиля
        {
           
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.Workshop != null)
            {
                displayStruct.SetBlueText("Подразделение: " + Card.Workshop.Name);
            }
            else
            {
                displayStruct.SetRedText("Выбирете подразделение");
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardComment() // Отображение комментария к карточке
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.Comment != "")
                displayStruct.SetBlueText("Примечание: " + Card.Comment);
            else
                displayStruct.SetOrangeText("Можно установить примечание");
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardRun() // Отображение пробега
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.DtAuto == null)
                return FormDisplay.ErrorStruct("Сначала необходимо выбрать автомобиль");
            if (Card.CardRun == 0)
                displayStruct.SetRedText("Установите пробег!");
            else
                displayStruct.SetBlueText("Пробег : " + Card.CardRun.ToString("000 000") + " км");
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardDetailDiscount() // Отображение скидки по умолчанию карточки
        {        
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            displayStruct.SetBlueText("Скидка на запчасти: " + Card.DiscountDetail.ToString() + "%");
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardPayType() // Отображение скидки по умолчанию карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            displayStruct.EnumData = (DtCard.PAY_TYPE) DtCard.PAY_TYPE.CASH;
            if (Card.CreditCard)
                displayStruct.EnumData = (DtCard.PAY_TYPE) DtCard.PAY_TYPE.CREDIT_CARD; // Оплата кредитной картой
            if (Card.Inner)
                displayStruct.EnumData = (DtCard.PAY_TYPE) DtCard.PAY_TYPE.NO_PAY; // Без оплаты - внутренний
            if (Card.Cashless)
                displayStruct.EnumData = (DtCard.PAY_TYPE) DtCard.PAY_TYPE.CASHLESS; // Безналичная оплата - по счету
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardPayTypeInit() // Инициализация Отображение типа оплаты
        {
            FormDisplay.FormDisplayStruct displayStruct = null;
            FormDisplay.FormDisplayStruct displayStructPrev = null;
            FormDisplay.FormDisplayStruct displayStructFirst = null;

            foreach (DtCard.PAY_TYPE i in Enum.GetValues(typeof(DtCard.PAY_TYPE)))
            { 
                displayStruct = new FormDisplay.FormDisplayStruct();
                if (displayStructPrev != null)
                    displayStructPrev.Next = displayStruct;
                if (displayStructFirst == null)
                    displayStructFirst = displayStruct;

                displayStruct.EnumData = (DtCard.PAY_TYPE)i;

                switch (i)
                {
                    case DtCard.PAY_TYPE.NONE:
                        displayStruct.SetText("НЕОБХОДИМО ВБРАТЬ ТИП ОПЛАТЫ");
                        break;
                    case DtCard.PAY_TYPE.CASH:
                        displayStruct.SetText("НАЛИЧНАЯ ОПЛАТА");
                        break;
                    case DtCard.PAY_TYPE.CREDIT_CARD:
                        displayStruct.SetText("КРЕДИТНАЯ КАРТА");
                        break;
                    case DtCard.PAY_TYPE.CASHLESS:
                        displayStruct.SetText("БЕЗНАЛИЧНАЯ ОПЛАТА ПО СЧЕТУ");
                        break;
                    case DtCard.PAY_TYPE.NO_PAY:
                        displayStruct.SetText("БЕЗ ОПЛАТЫ - ВНУТРЕННИЙ");
                        break;
                    default:
                        displayStruct.SetRedText("НЕИЗВЕСТНОЕ ЗНАЧЕНИЕ");
                        break;
                }
                displayStructPrev = displayStruct;
            }          
            return displayStructFirst;
        }
        public FormDisplay.FormDisplayStruct CardReturn() // Возвратная или нет карточка
        {

            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            displayStruct.SetNoEnumData();
            if (Card.Returned)
                displayStruct.SetYesEnumData();
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct ServiceManager() // Отображение заголовка владельца
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtStaff serviceManager = Card.ServiceManager;
            if (serviceManager == null)
                displayStruct.SetRedText("Выберите ведущего сервис-конультанта");
            else
                displayStruct.SetBlueText("Сервис-консультант - " + serviceManager.Title);
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct Auto() // Отображение данных автомобиля
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtAuto dtAuto = Card.DtAuto;
            if(dtAuto == null)
                displayStruct.SetRedText("Выбирете автомобиль"); // Отображение не выбранного автомобиля
            else
                //displayStruct.SetGreenText(dtAuto.TitleTxt + " РЕГ. ЗНАК: " + dtAuto.IdSign);
                displayStruct.SetGreenText(dtAuto.TitleTxt);
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct AutoSellDate() // Отображение данных о дате продажи выбранного автомобиля
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtAuto dtAuto = Card.DtAuto;
            if (dtAuto == null)
            {
                displayStruct.Hide();
            }
            else
            {
                displayStruct.Show();
                string txt = dtAuto.SellDateTxt;
                if (txt == "НЕТ")
                    displayStruct.SetRedText("Дата продажи: " + txt);
                else
                {
                    if ((DateTime.Now - dtAuto.SellDate).TotalDays < 365 * 3)
                        displayStruct.SetOrangeText("Дата продажи: " + txt);
                    else
                        displayStruct.SetGreenText("Дата продажи: " + txt);
                }
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardLicensePlate() // Отображение данных регистрационного знака автомобиля
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtAuto dtAuto = Card.DtAuto;
            if (dtAuto == null)
            {
                displayStruct.Hide();
                displayStruct.SetRedText("Не выбран автомобиль"); // Отображение не выбранного автомобиля
            }
            else
            {
                if(Card.LicensePlate.LicensePlateTxt == "")
                    displayStruct.SetRedText("Автомобиль не зарегистрирован"); // Отображение не зарегистрированного автомобиля
                else
                    displayStruct.SetGreenText( "РЕГ. ЗНАК: " + Card.LicensePlate.LicensePlateTxt);
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct AutoLastRun() // Отображение данных о последнем пробеге автомобиля
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            DtAuto dtAuto = Card.DtAuto;
            if (dtAuto == null)
            {
                displayStruct.Hide();
            }
            else
            {
                displayStruct.Show();
                DateValuePair runAtDate = DtAuto.ReadDbLastRun(dtAuto);
                string txt = "Последний пробег: " + runAtDate.GetInt().ToString() + " км от " + runAtDate.GetDate().ToShortDateString();
                displayStruct.SetText(txt);
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardAgreedPickupTime() // Согласованное время выдачи
        {

            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (!Card.IsAgreedPickUpTime) displayStruct.SetRedText("Необходимо согласовать время выдачи!");
            else displayStruct.SetBlueText("Врямя выдачи: " + Card.AgreedPickUpTime.ToString());
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardState() // Отображение статуса карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.IsNew) displayStruct.SetText("Новая карточка заказа");
            else
            {
                switch (Card.State)
                {
                    case DtCard.CardState.NONE:
                        displayStruct.SetText("Карточка без з/н №" + Card.Number.ToString() + "/" + Card.Year.ToString());
                        break;
                    case DtCard.CardState.CLOSED:
                        displayStruct.SetText("Закрытый заказ-наряд №" + Card.WarrantNumber.ToString() + "/" + Card.Year.ToString());
                        break;
                    case DtCard.CardState.OPEND:
                    case DtCard.CardState.REOPEND:
                        displayStruct.SetText("Открытый заказ-наряд №" + Card.WarrantNumber.ToString() + "/" + Card.Year.ToString());
                        break;
                    case DtCard.CardState.STOPPED:
                        displayStruct.SetText("Остановленный открытый заказ-наряд №" + Card.WarrantNumber.ToString() + "/" + Card.Year.ToString());
                        break;
                    default:
                        MessageBox.Show(Card.State.ToString());
                        displayStruct.SetText("Неизвестный №" + Card.Number.ToString() + "/" + Card.Year.ToString());
                        break;
                }
                if (Card.IsChg) displayStruct.SetText(displayStruct.Text + " ***");
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardActionOpen() // Отображение даты открытия карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.IsNew)
            {
                displayStruct.Hide();
                displayStruct.SetText("Карточка не сохранена");
            }
            else
            {
                DtCardAction action = Card.Actions.FindOpen();
                if (action == null)
                    displayStruct.SetRedText("Заказ-наряд не открыт");
                else
                    displayStruct.SetBlueText("Заказ-наряд открыт:" + action.Date.ToString());
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardActionClose() // Отображение даты открытия карточки
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.IsNew)
            {
                displayStruct.Hide();
                displayStruct.SetText("Карточка не сохранена");
            }
            else
            {
                DtCardAction action = Card.Actions.FindClose();
                if (action == null)
                    displayStruct.SetRedText("Заказ-наряд не закрыт");
                else
                    displayStruct.SetBlueText("Заказ-наряд закрыт:" + action.Date.ToString());
            }
            return displayStruct;
        }
        public FormDisplay.FormDisplayStruct CardActionEndwork() // Отображение даты окончание ремонта
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();
            if (Card.IsNew)
            {
                displayStruct.Hide();
                displayStruct.SetText("Карточка не сохранена");
            }
            else
            {
                DtCardAction action = Card.Actions.FindEndwork();
                if (action == null)
                    displayStruct.SetRedText("Время окончания ремонта не установлено");
                else
                    displayStruct.SetBlueText("Окончание оемонта:" + action.Date.ToString());
            }
            return displayStruct;
        }
    }
}
