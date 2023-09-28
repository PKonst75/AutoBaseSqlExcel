using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
    public class DbExcelReportFBS:DbExcel
    {
        ArrayList array = new ArrayList();		// Список продаж
        ArrayList array1 = new ArrayList();		// Список сервисных обслуживаний

        public DbExcelReportFBS()
        {
            DateTime date = DateTime.Now;
            DateTime nowdate = DateTime.Now;
            ArrayList sells = new ArrayList();
            ArrayList service = new ArrayList();
            FormSelectDate form = new FormSelectDate();
            if (form.ShowDialog() == DialogResult.OK)
            {
                date = form.SelectedDate;
            }
            else
            {
                // Дата уже установленна в сегодняшеюю
            }
            // Теперь собираем данные о продажах за выбранный день.
            DbSqlAutoSell.SearchMask mask = new DbSqlAutoSell.SearchMask();
            mask.timeon = true;
            mask.date_start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            mask.date_stop = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 0);
            DbSqlAutoSell.SelectInArray(sells, mask);
            FormInfoTable info = new FormInfoTable("Начало отсчета");
            info.Show();
            bool flag;
            DtPartner client = null;
            foreach (DtAutoSell element in sells)
            {
                client = DbSqlPartner.Find((long)element.GetData("ССЫЛКА_КОД_ПОКУПАТЕЛЬ"));
                flag = false;
                if (client != null)
                {
                    if (((bool)client.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО")) != true)
                        flag = true;
                }
                if(flag)array.Add(element);
                info.SetText(element.GetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА").ToString());
            }
            info.SetText("Конец отсчета");
            info.Close();

            // Теперь собираем данные о сервисном обслуживании, за выбранный день.
            ArrayList cards = new ArrayList();
            DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 1);

            FormInfoTable info1 = new FormInfoTable("Начало отсчета");
            info1.Show();
            foreach (DtCard element in cards)
            {
                DtCard card = (DtCard)element;
                card = DbSqlCard.Find((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
                flag = false;
                if (card != null)
                {
                    client = DbSqlPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
                    if (client != null)
                    {
                        if (((bool)client.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО")) != true)
                            flag = true;
                    }
                }
                if (flag) array1.Add(card);
                info1.SetText(element.GetData("НОМЕР_КАРТОЧКА").ToString());
            }
            info1.SetText("Конец отсчета");
            info1.Close();
        }

        protected void TitleFormatSheet1(Excel.Worksheet ws)
        {
            // Номер по порядку
            FormatColumn(ws, "A1", 6, 8, "Center");
            CellText(ws, "A1", "№ П/П", "Center", 10, true);
            // ФИО Клиента
            FormatColumn(ws, "B1", 24, 8, "Left", true);
            CellText(ws, "B1", "ФИО Клиента", "Center", 10, true);
            // Телефон Клиента
            FormatColumn(ws, "C1", 14, 8, "Left");
            CellText(ws, "C1", "Телефон Клиента", "Center", 10, true);
            // Продавец
            FormatColumn(ws, "D1", 14, 8, "Right");
            CellText(ws, "D1", "Продавец", "Center", 10, true);
            // Дата покупки
            FormatColumn(ws, "E1", 12, 8, "Center");
            CellText(ws, "E1", "Дата покупки", "Center", 10, true);
            // Модель
            FormatColumn(ws, "F1", 20, 8, "Left");
            CellText(ws, "F1", "Модель", "Center", 10, true);
            // Дата звонка
            FormatColumn(ws, "G1", 12, 8, "Center");
            CellText(ws, "G1", "Дата звонка", "Center", 10, true);
            // Вопрос 1
            FormatColumn(ws, "H1", 10, 8, "Center");
            CellText(ws, "H1", "Вопрос 1", "Center", 10, true);
            // Вопрос 2
            FormatColumn(ws, "I1", 10, 8, "Center");
            CellText(ws, "I1", "Вопрос 2", "Center", 10, true);
            // Вопрос 3
            FormatColumn(ws, "J1", 10, 8, "Center");
            CellText(ws, "J1", "Вопрос 3", "Center", 10, true);
            // Комментарии клиента
            FormatColumn(ws, "K1", 60, 8, "Left");
            CellText(ws, "K1", "Комментарии клиента", "Center", 10, true);
        }

        protected void TitleFormatSheet2(Excel.Worksheet ws)
        {
            // Номер по порядку
            FormatColumn(ws, "A1", 8, 8, "Center");
            CellText(ws, "A1", "№ П/П", "Center", 10, true);
            // ФИО Клиента
            FormatColumn(ws, "B1", 24, 8, "Left", true);
            CellText(ws, "B1", "ФИО Клиента", "Center", 10, true);
            // Телефон Клиента
            FormatColumn(ws, "C1", 14, 8, "Left");
            CellText(ws, "C1", "Телефон Клиента", "Center", 10, true);
            // Продавец
            FormatColumn(ws, "D1", 14, 8, "Right");
            CellText(ws, "D1", "Консультант", "Center", 10, true);
            // Дата покупки
            FormatColumn(ws, "E1", 12, 8, "Center");
            CellText(ws, "E1", "Дата выдачи", "Center", 10, true);
            // Модель
            FormatColumn(ws, "F1", 20, 8, "Left");
            CellText(ws, "F1", "Модель", "Center", 10, true);
            // Дата звонка
            FormatColumn(ws, "G1", 12, 8, "Center");
            CellText(ws, "G1", "Дата звонка", "Center", 10, true);
            // Вопрос 1
            FormatColumn(ws, "H1", 10, 8, "Center");
            CellText(ws, "H1", "Вопрос 1", "Center", 10, true);
            // Вопрос 2
            FormatColumn(ws, "I1", 10, 8, "Center");
            CellText(ws, "I1", "Вопрос 2", "Center", 10, true);
            // Вопрос 3
            FormatColumn(ws, "J1", 10, 8, "Center");
            CellText(ws, "J1", "Вопрос 3", "Center", 10, true);
            // Комментарии клиента
            FormatColumn(ws, "K1", 60, 8, "Left");
            CellText(ws, "K1", "Комментарии клиента", "Center", 10, true);
        }

        protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
        {
            string row_last = "1";
            string row_txt;
            string cell_txt;
            int row = start;
            int row_summ = 2;
            string txt;

            DtAuto auto = null;
            DtPartner client = null;
            CS_SellInfo info = null;
            DtAutoSellServ serv = null;

            foreach (object o in array)
            {
                // Продажи
                DtAutoSell sell = (DtAutoSell)o;

                // Загружаем дополнительные данные
                auto = DbSqlAuto.Find((long)sell.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ"));
                client = DbSqlPartner.Find((long)sell.GetData("ССЫЛКА_КОД_ПОКУПАТЕЛЬ"));
                info = DbSqlSellInfo.Find((long)sell.GetData("КОД_АВТОМОБИЛЬ_ПРОДАЖА"));
                serv = DbSqlAutoSellServ.Find((long)sell.GetData("КОД_АВТОМОБИЛЬ_ПРОДАЖА"));
                DtStaff manager = null;
                if (serv != null)
                {
                    manager = DbSqlStaff.Find(serv.code_manager);
                }

                // Выставление данных в Excel
                row_txt = row.ToString();
                // Номер по порядку
                cell_txt = "A" + row_txt;
                txt = (row - 1).ToString();
                CellText(ws, cell_txt, txt);
                // Покупатель/клиент
                txt = "Ошибка!";
                if (client != null)
                    txt = client.GetTitle();
                cell_txt = "B" + row_txt;
                CellText(ws, cell_txt, txt);
                // Контакт
                txt = "Ошибка!";
                if (client != null)
                    txt = client.GetPhone();
                cell_txt = "C" + row_txt;
                CellText(ws, cell_txt, txt);
                // Продавец
                txt = "Ошибка!";
                if (manager != null)
                    txt = manager.Title;
                cell_txt = "D" + row_txt;
                CellText(ws, cell_txt, txt);
                // Дата продажи
                cell_txt = "E" + row_txt;
                txt = ((DateTime)sell.GetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА")).ToShortDateString();
                CellText(ws, cell_txt, txt);
                // МОДЕЛЬ
                cell_txt = "F" + row_txt;
                txt = "Ошибка!";
                if (auto != null)
                    txt = (string)auto.GetData("МОДЕЛЬ");
                CellText(ws, cell_txt, txt);
                // Дата звонка
                cell_txt = "G" + row_txt;
                txt = DateTime.Now.ToShortDateString();
                CellText(ws, cell_txt, txt);
                row++;	// переход на следующую стоку
            }
        }

        protected void DataToExcelSheet2(Excel.Worksheet ws, int start)
        {
            string row_last = "1";
            string row_txt;
            string cell_txt;
            int row = start;
            int row_summ = 2;
            string txt;

            DtAuto auto = null;
            DtPartner client = null;
            CS_SellInfo info = null;
            DtAutoSellServ serv = null;
            DtStaff manager = null;

            foreach (object o in array1)
            {
                // Продажи
                DtCard card = (DtCard)o;

                // Загружаем дополнительные данные
                auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
                client = DbSqlPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
                
                manager = DbSqlStaff.Find((long)card.GetData("СЕРВИС_КОНСУЛЬТАНТ"));
             
                // Выставление данных в Excel
                row_txt = row.ToString();
                // Номер по порядку
                cell_txt = "A" + row_txt;
                txt = (row - 1).ToString();
                if (card.IsGuarantyWork() == true) txt = txt + "ГР";
                if (card.IsGuarantyDetail() == true) txt = txt + "ГД";
                if (card.IsToWork() == true) txt = txt + "Т";
                CellText(ws, cell_txt, txt);
                // Покупатель/клиент
                txt = "Ошибка!";
                if (client != null)
                    txt = client.GetTitle();
                cell_txt = "B" + row_txt;
                CellText(ws, cell_txt, txt);
                // Контакт
                txt = "Ошибка!";
                if (client != null)
                    txt = client.GetPhone();
                cell_txt = "C" + row_txt;
                CellText(ws, cell_txt, txt);
                // Продавец
                txt = "Ошибка!";
                if (manager != null)
                    txt = manager.Title;
                cell_txt = "D" + row_txt;
                CellText(ws, cell_txt, txt);
                // Дата продажи
                cell_txt = "E" + row_txt;
                txt = ((DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА")).ToShortDateString();
                CellText(ws, cell_txt, txt);
                // МОДЕЛЬ
                cell_txt = "F" + row_txt;
                txt = "Ошибка!";
                if (auto != null)
                    txt = (string)auto.GetData("МОДЕЛЬ");
                CellText(ws, cell_txt, txt);
                // Дата звонка
                cell_txt = "G" + row_txt;
                txt = DateTime.Now.ToShortDateString();
                CellText(ws, cell_txt, txt);
                row++;	// переход на следующую стоку
            }
        }


        override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
        {
            if (sheet == 1)
            {
                DataToExcelSheet1(ws, start);
                return;
            }
            if (sheet == 2)
            {
                DataToExcelSheet2(ws, start);
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
            if (sheet == 2)
            {
                TitleFormatSheet2(ws);
                return;
            }
        }
    }
}
