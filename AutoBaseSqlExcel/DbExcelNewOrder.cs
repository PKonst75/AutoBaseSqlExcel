using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
    class DbExcelNewOrder : DbExcel
    {
        DtCard card_data = null;
        string txt_order = "";  // Номер заказ-наряда
        string txt_date = "";  // Дата заказ-наряда
        string txt_date_open = "";  // Дата открытия заказ-наряда
        string txt_date_close = "";  // Дата закрытия заказ-наряда

        string txt_model = "";      // Модель автомобиля
        string txt_vin = "";        // VIN автомобиля
        string txt_body = "";       
        string txt_engine = "";
        string txt_plate = "";
        string txt_run = "";        // Пробег
        string txt_sell_date = "";  // Дата продажи
        string txt_year = "";       // Год выпуска

        // Описание владельца
        string txt_partner = "";
        string txt_partner_surname = "";   // Владелец ФАМИЛИЯ
        string txt_partner_name = "";       // Владелец ИМЯ
        string txt_partner_secondname = ""; // Владелец ОТЧЕСТВО
        string txt_partner_address = "";    // Владелец АДРЕС
        string txt_partner_phone = "";      // Владелец ТЕЛЕФОН
        string txt_partner_mail = "";       // Владелец E-MAIL
        string txt_partner_contact = "";    // Владелец КОНТАКТЫ

        string txt_card_date_open = "";     // Время открытия
        string txt_card_date_close = "";    // Время закрытия
        string txt_card_date_finish = "";   // Время окончания ремонта
        string txt_card_time_open = "";     // Время открытия
        string txt_card_time_close = "";    // Время закрытия
        string txt_card_time_finish = "";   // Время окончания ремонта



        ArrayList works = null;
        double works_summ = 0;
        string txt_works_summ = "";

        ArrayList details = null;
        double details_summ = 0;
        string txt_details_summ = "";

        float detail_discount = 0;
        string txt_detal_discount = "";
        public DbExcelNewOrder(DtCard theCard)
        {
            // Подготовка данных для формирования выгрузки в ECXEL заказ-наряда для страховой компании
            card_data = theCard;// new DtCard(theCard);
            DtTxtCard txtCard = new DtTxtCard(theCard);
            long num = theCard.Number;// (long)card_data.GetData("НОМЕР_КАРТОЧКА");
            int year = theCard.Year;// (int)card_data.GetData("ГОД_КАРТОЧКА");

            // Получение данных
            txt_order = txtCard.WarrantNumber;// theCard.GetDataTxt("НОМЕР_НАРЯД_КАРТОЧКА");
            //DateTime dt = (DateTime)theCard.GetData("ДАТА");
            //dt = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
            txt_date = txtCard.Date;// dt.ToShortDateString();
            txt_date_open = txtCard.OpenDateTime;//theCard.GetDataTxt("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
            txt_date_close = txtCard.CloseDateTime;// theCard.GetDataTxt("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");

            // Дата начала, окончания ремонта и выдачи
            // Время начала, окончания ремонта и ввыдачи
            DateTime time = (DateTime)theCard.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
            txt_card_time_open = time.ToShortTimeString();
            txt_card_date_open = time.ToShortDateString();
            time = (DateTime)theCard.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");
            txt_card_time_close = time.ToShortTimeString();
            txt_card_date_close = time.ToShortDateString();

            DtCardMarkEndWork mark_workend = DbSqlCardMarkEndWork.Find(num,year);
            if (mark_workend != null)
            {
                time = (DateTime)mark_workend.GetData("ДАТА");
                txt_card_time_finish = time.ToShortTimeString();
                txt_card_date_finish = time.ToShortDateString();
            }

            
            DtAuto auto = new DtAuto();
            long code_auto = theCard.CodeAuto;// (long)theCard.GetData("АВТОМОБИЛЬ_КАРТОЧКА");
            if (code_auto != 0)
                auto = DbSqlAuto.Find(code_auto);

            // Описание автомобиля
            txt_model = (string)auto.GetData("МОДЕЛЬ");
            txt_vin = (string)auto.GetData("VIN");
            txt_body = (string)auto.GetData("НОМЕР_КУЗОВ");
            txt_engine = (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
            txt_plate = (string)auto.GetData("НОМЕР_ЗНАК");
            txt_year = auto.GetData("ГОД_ВЫПУСК").ToString();
            txt_sell_date = auto.GetData("ПРОДАЖА_ДАТА").ToString();
            txt_run = theCard.GetData("ПРОБЕГ_КАРТОЧКА").ToString();
            

            long code_partner = 0;
            DtPartner partner = new DtPartner();
            code_partner = (long)theCard.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА");
            if (code_partner != 0)
                partner = DbSqlPartner.Find(code_partner);

            // Описание владельца
            txt_partner = partner.GetTitleName() + "\n" + partner.GetAddress() + " " + partner.GetPhone();
            if (partner.IsJuridical())
            {
            }
            else
            {
                txt_partner_surname = partner.PersonSurname();
                txt_partner_name = partner.PersonName();
                txt_partner_secondname = partner.PersonSecondname();
                txt_partner_address = partner.GetAddress();
                txt_partner_phone = partner.GetPhone();
                txt_partner_mail = partner.GetMail();

                if (txt_partner_phone == "") txt_partner_phone = "-";
                if (txt_partner_mail == "") txt_partner_mail = "-";

            }

            works = new ArrayList();
            DbSqlCardWork.SelectInArray(theCard, works);

            details = new ArrayList();
            DbSqlCardDetail.SelectInArray(theCard, details);
        }

        protected void FillData(Excel.Worksheet ws, int start)
        {
            // ДАННЫЕ ФИЗИЧЕСКОГО ЛИЦА
            CellText(ws, "B2", txt_partner_surname);
            CellText(ws, "B3", txt_partner_name);
            CellText(ws, "B4", txt_partner_secondname);
            CellText(ws, "B5", txt_partner_address);
            CellText(ws, "B6", txt_partner_phone);
            CellText(ws, "B7", txt_partner_mail);

            // Данные автомобиля
            CellText(ws, "B9", txt_model);
            CellText(ws, "B10", txt_vin);
            CellText(ws, "B11", txt_sell_date);
            CellText(ws, "B12", txt_year);
            CellText(ws, "B13", txt_run);
            CellText(ws, "B14", txt_plate);

            // Данные карточки
            CellText(ws, "B16", txt_order);
            CellText(ws, "C16", txt_date);
            CellText(ws, "C18", txt_card_date_open);
            CellText(ws, "C19", txt_card_time_open);
            CellText(ws, "D18", txt_card_date_close);
            CellText(ws, "D19", txt_card_time_close);
            if (txt_card_date_finish != "")
            {
                CellText(ws, "E18", txt_card_date_finish);
                CellText(ws, "E19", txt_card_time_finish);
            }

            // Выгрузка списка работ
            int row = 34;
            foreach (DtCardWork w in works)
            {
                DownloadWorkRow(ws, w, row);
                row++;
            }

            row = 88;
            foreach (DtCardDetail d in details)
            {
                DownloadDetailRow(ws, d, row);
                row++;
            }

            // Теперь превращаем сумму  в пропись
            string txt_summ = this.GET_CellText(ws, "J140");
            string txt_panny = this.GET_CellText(ws, "C141");
            string txt_summ_text = UI_Digit2Text.Convert(txt_summ);
            string txt_panny_text = UI_Digit2Text.Panny(txt_panny);
            CellText(ws, "C142", txt_summ_text);
            CellText(ws, "D141", txt_panny_text);
        }
        override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
        {
            if (sheet == 1)
            {
                FillData(ws, start);
                return;
            }
        }

        protected void DownloadWorkRow(Excel.Worksheet ws, DtCardWork work, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;

            string txt_work_number = "";
            string txt_work_code = "";
            string txt_work_name = "";
            string txt_work_qty = "";
            string txt_work_val = "";
            string txt_work_price = "";
            string txt_work_discount = "";
            string txt_work_summ = "";
            string txt_work_unit = "";

            double work_summ = 0.0;

            DtTxtCardWork txtCardWork = new DtTxtCardWork(work);
            CalculatorCard  calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
            CalculatorResult res = calc.WorkCalculator.Calculate(work);
            if (work != null)
            {

                // txt_work_number = work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА").ToString();
                // txt_work_code = (string)work.GetData("НОМЕР_ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
                // txt_work_name = (string)work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
                // txt_work_qty = work.OperationAmount.ToString(); work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();


                float val = work.LaborTime;// work.Amount;// (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
                if (val == 0)
                {
                   // txt_work_price = work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА").ToString();
                   // txt_work_val = "1";
                   // txt_work_unit = "шт";
                }
                else
                {
                   // txt_work_price = work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА").ToString();
                   // txt_work_val = work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString();
                   // txt_work_unit = "н/ч";
                }
                //txt_work_discount = work.GetData("СКИДКА_КАРТОЧКА_РАБОТА").ToString();
                work_summ = res.SummTotal; //work_summ = work.WorkSummCash;
                works_summ = works_summ + work_summ;
                txt_work_summ = Db.CachToTxt(work_summ);
            }

            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, txtCardWork.WorkName); // CellText(ws, cell_txt, txt_work_name);
            cell_txt = "D" + row_txt;
            CellText(ws, cell_txt, txtCardWork.OperationAmount); //CellText(ws, cell_txt, txt_work_qty);
            cell_txt = "E" + row_txt;
            CellText(ws, cell_txt, txtCardWork.Amount); //CellText(ws, cell_txt, txt_work_val);
            cell_txt = "C" + row_txt;
            CellText(ws, cell_txt, txtCardWork.CatalogueNumber); //CellText(ws, cell_txt, txt_work_code);
            cell_txt = "G" + row_txt;
            CellText(ws, cell_txt, txtCardWork.Price); //CellText(ws, cell_txt, txt_work_price);
            cell_txt = "F" + row_txt;
            CellText(ws, cell_txt, txtCardWork.Unit); //CellText(ws, cell_txt, txt_work_unit);

            /*
            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            
            CellText(ws, cell_txt, txt_work_number, "Center", 11, false);

            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, txt_work_code, "Center", 11, false);
            cell_txt = "C" + row_txt;
            cell_txt2 = "E" + row_txt;
            CellText(ws, cell_txt, txt_work_name, "Left", 11, false);
            cell_txt = "F" + row_txt;
            CellText(ws, cell_txt, txt_work_qty, "Center", 11, false);
            cell_txt = "G" + row_txt;
            CellText(ws, cell_txt, txt_work_val, "Center", 11, false);
            cell_txt = "H" + row_txt;
            CellText(ws, cell_txt, txt_work_price, "Center", 11, false);
            cell_txt = "I" + row_txt;
            CellText(ws, cell_txt, txt_work_discount, "Center", 11, false);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, txt_work_summ, "Center", 11, false);
            */

        }

        protected void DownloadDetailRow(Excel.Worksheet ws, DtCardDetail detail, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;

            string txt_detail_number = "";
            string txt_detail_code = "";
            string txt_detail_name = "";
            string txt_detail_qty = "";
            string txt_detail_price = "";
            string txt_detail_discount = "";
            string txt_detail_summ = "";
            string txt_detail_unit = "шт";


            double detail_summ = 0.0;

            if (detail != null)
            {
                txt_detail_number = detail.GetData("ПОЗИЦИЯ_КАРТОЧКА_ДЕТАЛЬ").ToString();
                txt_detail_code = (string)detail.GetData("КАТАЛОГ_НОМЕР_КАРТОЧКА_ДЕТАЛЬ");
                txt_detail_name = (string)detail.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_ДЕТАЛЬ");
                txt_detail_qty = detail.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ").ToString();
                txt_detail_price = detail.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ").ToString();
                float discount = (float)detail.GetData("СКИДКА");
                if (discount == 0.0F) detail.SetData("СКИДКА", detail_discount);
                txt_detail_discount = detail.GetData("СКИДКА").ToString();
                detail_summ = detail.DetailSummCash;
                details_summ = details_summ + detail_summ;
                txt_detail_summ = Db.CachToTxt(detail_summ);

                txt_detail_unit = detail.GetData("ЕДИНИЦА_ИЗМЕРЕНИЯ_КАРТОЧКА_ДЕТАЛЬ").ToString();

                               
            }

            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, txt_detail_name);
            cell_txt = "C" + row_txt;
            CellText(ws, cell_txt, txt_detail_code);
            cell_txt = "D" + row_txt;
            CellText(ws, cell_txt, txt_detail_qty);
            cell_txt = "F" + row_txt;
            CellText(ws, cell_txt, txt_detail_price);
            cell_txt = "E" + row_txt;
            CellText(ws, cell_txt, txt_detail_unit);
            /*
            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            CellText(ws, cell_txt, txt_detail_number, "Center", 11, false);

            cell_txt = "B" + row_txt;
            cell_txt2 = "C" + row_txt;
            CellText(ws, cell_txt, txt_detail_code, "Center", 11, false);
            MergeCells(ws, cell_txt, cell_txt2);
            cell_txt = "D" + row_txt;
            cell_txt2 = "F" + row_txt;
            CellText(ws, cell_txt, txt_detail_name, "Left", 11, false);
            MergeCells(ws, cell_txt, cell_txt2);
            cell_txt = "G" + row_txt;
            CellText(ws, cell_txt, txt_detail_qty, "Center", 11, false);
            cell_txt = "H" + row_txt;
            CellText(ws, cell_txt, txt_detail_price, "Center", 11, false);
            cell_txt = "I" + row_txt;
            CellText(ws, cell_txt, txt_detail_discount, "Center", 11, false);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, txt_detail_summ, "Center", 11, false);
            */
        }

    }
}

