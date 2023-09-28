using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
    class DbExcelRD372019Storage:DbExcel
    {
        private readonly DtCard card = null;
        private readonly DtAuto auto = null;
        private readonly DtPartner owner = null;
        private readonly DtPartner represent = null;
        private readonly DtPartner payer = null;
        private readonly ArrayList detailsTxt = null;

        public DbExcelRD372019Storage(DtCard theCard)
        {
            card = theCard;// new DtCard(theCard);
            long code = theCard.CodeAuto;// (long)theCard.GetData("АВТОМОБИЛЬ_КАРТОЧКА");
            if (code != 0)
                auto = DbSqlAuto.Find(code);
            code = (long)theCard.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА");
            if (code != 0)
                owner = DbSqlPartner.Find(code);
            code = (long)theCard.GetData("ПРЕДСТАВИТЕЛЬ_КАРТОЧКА");
            if (code != 0)
                represent = DbSqlPartner.Find(code);
            else
                represent = owner;
            payer = represent;
        
            ArrayList details = new ArrayList();
            DbSqlCardDetail.SelectInArray(card, details);
            detailsTxt = new ArrayList();
            foreach (DtCardDetail detail in details)
            {
                detailsTxt.Add(new DtCardDetail.DtCardDetailTxt(detail));
            }
        }

        override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
        {
            if (sheet == 1)
            {
                FillData(ws);
                return;
            }
        }

        public void FillData(Excel.Worksheet ws)
        {
            DtTxtCard txtCard = new DtTxtCard(card);
            // Подготовка данных
            DtAuto.DtAutoTxt autoTxt = new DtAuto.DtAutoTxt(auto);
           // DtCard.DtCardTxt cardTxt = new DtCard.DtCardTxt(card);
            DtPartner.DtPartnerTxt ownerTxt = new DtPartner.DtPartnerTxt(owner);
            DtPartner.DtPartnerTxt representTxt = new DtPartner.DtPartnerTxt(represent);
            DtPartner.DtPartnerTxt payerTxt = new DtPartner.DtPartnerTxt(payer);
            // ШАПКА
            // НОМЕР И ДАТА
            CellTextAdd(ws, "A8", txtCard.NumberDate + " года"); //CellTextAdd(ws, "A8", cardTxt.number_date + " года");
            CellTextAdd(ws, "A9", txtCard.NumberDate + " года"); //CellTextAdd(ws, "A9", cardTxt.number_date + " года");
            // ДАННЫЕ АВТОМОБИЛЯ
            if (auto != null)
            {
                CellText(ws, "C12", autoTxt.model);
                CellText(ws, "C13", autoTxt.vin);
                CellText(ws, "C14", autoTxt.sell_date);
                CellText(ws, "C15", autoTxt.year);
                CellText(ws, "C16", txtCard.Run); //CellText(ws, "C16", cardTxt.run);
                CellText(ws, "C17", autoTxt.licence_plate);
            }
            // Данные владельца
            CellText(ws, "O12", ownerTxt.name);
            CellText(ws, "O13", "Адрес: " + ownerTxt.address);
            CellTextAdd(ws, "O13", " Телефон: " + ownerTxt.phone);
            CellTextAdd(ws, "O13", " e-mail:" + ownerTxt.email);
            // Данные представителя
            CellText(ws, "O15", representTxt.name);
            CellText(ws, "O16", "Адрес: " + representTxt.address);
            CellTextAdd(ws, "O16", " Телефон: " + representTxt.phone);
            CellTextAdd(ws, "O16", " e-mail:" + representTxt.email);
            // Данные плательщика
            CellText(ws, "O18", payerTxt.name);
            CellText(ws, "O20", "Адрес: " + payerTxt.address);
            CellTextAdd(ws, "O20", " Телефон: " + payerTxt.phone);
            CellTextAdd(ws, "O20", " e-mail:" + payerTxt.email);

            bool first = true;
            string txt = "";
            int pos = 0;
            first = true;
            pos = 26;
            int count = 1;
            foreach (DtCardDetail.DtCardDetailTxt detailTxt in detailsTxt)
            {
                if (first == false)
                {
                    RowInsertCopy(ws, pos);
                    pos++;
                    count++;
                }
                else
                {
                    first = false;
                }
                CellText(ws, "B" + pos.ToString(), detailTxt.catalog_number);
                CellText(ws, "E" + pos.ToString(), detailTxt.detailName);
                CellText(ws, "AD" + pos.ToString(), detailTxt.detailName);
                CellText(ws, "A" + pos.ToString(), count.ToString());
                CellText(ws, "L" + pos.ToString(), detailTxt.unit);
                CellText(ws, "J" + pos.ToString(), detailTxt.amount);
                CellText(ws, "P" + pos.ToString(), detailTxt.price);
            }
            int offset = count - 1;


            CellTextFormula(ws, "J" + (27 + offset).ToString(), "=SUM(J" + (26 + offset - count + 1).ToString() + ":J" + (26 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "S" + (27 + offset).ToString(), "=SUM(S" + (26 + offset - count + 1).ToString() + ":S" + (26 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "U" + (27 + offset).ToString(), "=SUM(U" + (26 + offset - count + 1).ToString() + ":U" + (26 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "X" + (27 + offset).ToString(), "=SUM(X" + (26 + offset - count + 1).ToString() + ":X" + (26 + offset - count + count).ToString() + ")");

            CellTextAdd(ws, "A" + (34 + offset).ToString(), txtCard.ServiceManagerShortName); //CellTextAdd(ws, "A" + (34 + offset).ToString(), cardTxt.serviceManager);
        }
    }
}
