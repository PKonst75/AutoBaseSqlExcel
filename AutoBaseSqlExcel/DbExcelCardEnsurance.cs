using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public class DbExcelCardEnsurance:DbExcel
    {
        DtCard card_data = null;
        string txt_order = "";  // Номер заказ-наряда
        string txt_date = "";  // Дата заказ-наряда
        string txt_date_open = "";  // Дата открытия заказ-наряда
        string txt_date_close = "";  // Дата закрытия заказ-наряда

        string txt_model = "";
        string txt_vin = "";
        string txt_body = "";
        string txt_engine = "";
        string txt_plate = "";
        string txt_run = "";

        string txt_partner = "";

        ArrayList works = null;
        double works_summ = 0;
        string txt_works_summ = "";

        ArrayList details = null;
        double details_summ = 0;
        string txt_details_summ = "";

        float detail_discount = 0;
        string txt_detal_discount = "";


        public DbExcelCardEnsurance(DtCard theCard)
        {
            // Подготовка данных для формирования выгрузки в ECXEL заказ-наряда для страховой компании
            card_data = theCard; // new DtCard(theCard);
            DtTxtCard txtCard = new DtTxtCard(theCard);

            // Получение данных
            txt_order = txtCard.WarrantNumber;//txt_order = theCard.GetDataTxt("НОМЕР_НАРЯД_КАРТОЧКА");
            //DateTime dt = (DateTime)theCard.GetData("ДАТА");
            //dt = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
            txt_date = txtCard.Date;//dt.ToShortDateString();
            txt_date_open = txtCard.OpenDateTime;//theCard.GetDataTxt("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
            txt_date_close = txtCard.CloseDateTime;//theCard.GetDataTxt("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");

            
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
            txt_run = theCard.GetData("ПРОБЕГ_КАРТОЧКА").ToString(); ;

            long code_partner = 0;
            DtPartner partner = new DtPartner();
            code_partner = (long)theCard.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА");
            if (code_partner != 0)
                partner = DbSqlPartner.Find(code_partner);

            // Описание владельца
            txt_partner = partner.GetTitleName() + "\n" + partner.GetAddress() + " " + partner.GetPhone();


            works = new ArrayList();
            DbSqlCardWork.SelectInArray(theCard, works);

            detail_discount = (float)theCard.GetData("СКИДКА_ДЕТАЛЬ_КАРТОЧКА");
            txt_detal_discount = detail_discount.ToString();
            details = new ArrayList();
            DbSqlCardDetail.SelectInArray(theCard, details);
        }

      
        protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Дата закрытия заказ/наряда
			FormatColumn(ws, "A1", 4, 11, "Center");
            FormatColumn(ws, "B1", 8, 11, "Center");
            FormatColumn(ws, "C1", 12, 11, "Left");
            FormatColumn(ws, "D1", 26, 11, "Left");
            FormatColumn(ws, "E1", 1, 11, "Left");
            FormatColumn(ws, "F1", 9, 11, "Center");
            FormatColumn(ws, "G1", 11, 11, "Center");
            FormatColumn(ws, "H1", 11, 11, "Center");
            FormatColumn(ws, "I1", 11, 11, "Center");
            FormatColumn(ws, "J1", 11, 11, "Center");

         //   MergeCells(ws, "F5", "J11");
         //   CellsBorderOuter(ws, "F5", "F5", 0);
         //   UnMergeCells(ws, "F5", "J11");
            MergeCells(ws, "F6", "J11");

            MergeCells(ws, "A5", "D11");
            CellsBorderOuter(ws, "A5", "A5", 0);
            UnMergeCells(ws, "A5", "D11");

            // Шапка заказ-наряда
            CellText(ws, "A1", "ЗАКАЗ-НАРЯД №", "Left", 11, true);
            CellText(ws, "A2", "Открыт:", "Left", 11, true);
            CellText(ws, "A3", "Закрыт:", "Left", 11, true);
            CellText(ws, "F1", "от", "Left", 11, false);
            // Шапка автомобиля
            CellText(ws, "A5", "АВТОМОБИЛЬ", "Left", 11, true);
            CellText(ws, "A6", "МОДЕЛЬ:", "Left", 11, false);
            CellText(ws, "A7", "VIN:", "Left", 11, false);
            CellText(ws, "A8", "КУЗОВ №:", "Left", 11, false);
            CellText(ws, "A9", "ДВИГАТЕЛЬ №:", "Left", 11, false);
            CellText(ws, "A10", "РЕГ. ЗНАК:", "Left", 11, false);
            CellText(ws, "A11", "ПРОБЕГ:", "Left", 11, false);
            // Шапка клиента
            CellText(ws, "F5", "КЛИЕНТ", "Left", 11, true);
            FormatCellsVAlign(ws, "F6", "J11", "Top");
           

            // Шапка Работ
            CellText(ws, "C13", "РАБОТЫ", "Left", 11, true);
		}

        protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
        {
            string row_last = "1";
            string row_txt;
            string cell_txt;
            string cell_txt2;
            int row = start;
            int row_summ = 2;
            string txt;

            CellText(ws, "D1", txt_order, "Right", 11, false);
            CellText(ws, "G1", txt_date, "Right", 11, false);
            CellText(ws, "D2", txt_date_open, "Right", 11, false);
            CellText(ws, "D3", txt_date_close, "Right", 11, false);

            CellText(ws, "D6", txt_model, "Left", 11, false);
            CellText(ws, "D7", txt_vin, "Left", 11, false);
            CellText(ws, "D8", txt_body, "Left", 11, false);
            CellText(ws, "D9", txt_engine, "Left", 11, false);
            CellText(ws, "D10", txt_plate, "Left", 11, false);
            CellText(ws, "D11", txt_run, "Left", 11, false);

            CellText(ws, "F6", txt_partner, "Left", 11, false);

            row = 14;
            DownloadWorkHead(ws, row);
            row++;
            foreach (DtCardWork w in works)
            {
                DownloadWorkRow(ws, w, row);
                row++;
            }

            row_txt = row.ToString();
            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            cell_txt2 = "I" + row_txt;
            CellText(ws, cell_txt, "ИТОГО:", "Right", 11, true);
            MergeCells(ws, cell_txt, cell_txt2);

            txt_works_summ = Db.CachToTxt(works_summ);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, txt_works_summ, "Center", 11, false);

            row = row + 2;
            row_txt = row.ToString();
            cell_txt = "C" + row_txt;
            CellText(ws, cell_txt, "ДЕТАЛИ", "Center", 11, true);

            row++;
            DownloadDetailHead(ws, row);
            row++;
            foreach (DtCardDetail d in details)
            {
                DownloadDetailRow(ws, d, row);
                row++;
            }

            row_txt = row.ToString();
            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            cell_txt2 = "I" + row_txt;
            CellText(ws, cell_txt, "ИТОГО:", "Right", 11, true);
            MergeCells(ws, cell_txt, cell_txt2);

            txt_details_summ = Db.CachToTxt(details_summ);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, txt_details_summ, "Center", 11, false);

            string txt_summ = "";
            double summ = details_summ + works_summ;
            txt_summ = Db.CachToTxt(summ);
            row = row + 2;
            row_txt = row.ToString();
            cell_txt = "I" + row_txt;
            CellText(ws, cell_txt, "ИТОГО ПО ЗАКАЗ-НАРЯДУ", "Right", 11, true);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, txt_summ, "Center", 11, true);
        }

        protected void DownloadWorkHead(Excel.Worksheet ws, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;
            string cell_txt2;

            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            CellText(ws, cell_txt, "№", "Center", 11, false);
            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, "Код", "Center", 11, false);
            cell_txt = "C" + row_txt;
            cell_txt2 = "E" + row_txt;
            CellText(ws, cell_txt, "Наименование работ", "Center", 11, false);
            MergeCells(ws, cell_txt, cell_txt2);
            cell_txt = "F" + row_txt;
            CellText(ws, cell_txt, "К-во", "Center", 11, false);
            cell_txt = "G" + row_txt;
            CellText(ws, cell_txt, "Ч.", "Center", 11, false);
            cell_txt = "H" + row_txt;
            CellText(ws, cell_txt, "Цена", "Center", 11, false);
            cell_txt = "I" + row_txt;
            CellText(ws, cell_txt, "Скидка %", "Center", 11, false);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, "Сумма", "Center", 11, false);
        }

        protected void DownloadDetailHead(Excel.Worksheet ws, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;
            string cell_txt2;

            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            CellText(ws, cell_txt, "№", "Center", 11, false);
            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, "Код", "Center", 11, false);
            cell_txt2 = "C" + row_txt;
            MergeCells(ws, cell_txt, cell_txt2);
            cell_txt = "D" + row_txt;
            cell_txt2 = "F" + row_txt;
            CellText(ws, cell_txt, "Наименование", "Center", 11, false);
            MergeCells(ws, cell_txt, cell_txt2);
            cell_txt = "G" + row_txt;
            CellText(ws, cell_txt, "К-во", "Center", 11, false);
            cell_txt = "H" + row_txt;
            CellText(ws, cell_txt, "Цена", "Center", 11, false);
            cell_txt = "I" + row_txt;
            CellText(ws, cell_txt, "Скидка %", "Center", 11, false);
            cell_txt = "J" + row_txt;
            CellText(ws, cell_txt, "Сумма", "Center", 11, false);
        }

        protected void DownloadWorkRow(Excel.Worksheet ws, DtCardWork work, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;
            string cell_txt2;

            string txt_work_number = "";
            string txt_work_code = "";
            string txt_work_name = "";
            string txt_work_qty = "";
            string txt_work_val = "";
            string txt_work_price = "";
            string txt_work_discount = "";
            string txt_work_summ = "";
            string txt_works_summ = "";

            double work_summ = 0.0;

            DtTxtCardWork txtCardWork = new DtTxtCardWork(work);
            CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
            CalculatorResult res = calc.WorkCalculator.Calculate(work);

            if (work != null)
            {
                txt_work_number = txtCardWork.Position;// work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА").ToString();
                txt_work_code = txtCardWork.CatalogueNumber;//(string)work.GetData("НОМЕР_ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
                txt_work_name = txtCardWork.WorkName;// (string)work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
                txt_work_qty = txtCardWork.OperationAmount;// work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
                txt_work_val = txtCardWork.Amount;// work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString();
                txt_work_price = txtCardWork.Price;// work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА").ToString();
                txt_work_discount = txtCardWork.Discount;// work.GetData("СКИДКА_КАРТОЧКА_РАБОТА").ToString();
                work_summ = res.SummTotal; // work_summ = work.WorkSummCash;
                works_summ = works_summ + work_summ;
                txt_work_summ = Db.CachToTxt(work_summ);
            }

            cell_txt = "A" + row_txt;
            cell_txt2 = "J" + row_txt;
            CellsBorderOuter(ws, cell_txt, cell_txt2, 0);
            CellText(ws, cell_txt, txt_work_number, "Center", 11, false);

            cell_txt = "B" + row_txt;
            CellText(ws, cell_txt, txt_work_code, "Center", 11, false);
            cell_txt = "C" + row_txt;
            cell_txt2 = "E" + row_txt;
            CellText(ws, cell_txt, txt_work_name, "Left", 11, false);
            MergeCells(ws, cell_txt, cell_txt2);
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
            
        }

        protected void DownloadDetailRow(Excel.Worksheet ws, DtCardDetail detail, int row)
        {
            string row_txt = row.ToString();
            string cell_txt;
            string cell_txt2;

            string txt_detail_number = "";
            string txt_detail_code = "";
            string txt_detail_name = "";
            string txt_detail_qty = "";
            string txt_detail_price = "";
            string txt_detail_discount = "";
            string txt_detail_summ = "";
            

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
            }

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

        }


        #region Обязательные процедуры для выгрузки в EXCEL
        override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
        {
            if (sheet == 1)
            {
                DataToExcelSheet1(ws, start);
                return;
            }
        }

        override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
        {
            if (sheet == 1)
            {
                TitleFormatSheet1(ws);
                return;
            }
        }
        #endregion
    }
}
