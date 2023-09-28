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
    class DbExcelRD372019Act : DbExcel
    {
        private readonly DtCard card = null;
        private readonly DtAuto auto = null;
        private readonly DtPartner owner = null;
        private readonly DtPartner represent = null;
        private readonly DtPartner payer = null;
        private readonly ArrayList claimsTxt = null;
        private readonly ArrayList detailsTxt = null;
        public DbExcelRD372019Act(DtCard theCard)
        {
            card = theCard;
            long code = theCard.CodeAuto;
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
            ArrayList claims = new ArrayList();
            DbSqlCardClaim.SelectInArray(claims, card.Number, card.Year);
            claimsTxt = new ArrayList();
            foreach (DtCardClaim claim in claims)
            {
                claimsTxt.Add(new DtTxtCardClaim(claim));
            }
            /*
            ArrayList works = new ArrayList();
            DbSqlCardWork.SelectInArray(card, works);
            worksTxt = new ArrayList();
            foreach (DtCardWork work in works)
            {
                worksTxt.Add(new DtCardWork.DtCardWorkTxt(work));
            }
            */
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
            // Подготовка данных
            DtTxtCard txtCard = new DtTxtCard(card);
            DtAuto.DtAutoTxt autoTxt = new DtAuto.DtAutoTxt(auto);
            DtPartner.DtPartnerTxt ownerTxt = new DtPartner.DtPartnerTxt(owner);
            DtPartner.DtPartnerTxt representTxt = new DtPartner.DtPartnerTxt(represent);
            DtPartner.DtPartnerTxt payerTxt = new DtPartner.DtPartnerTxt(payer);
            // ШАПКА
            // НОМЕР И ДАТА
            CellTextAdd(ws, "M7", txtCard.Number); 
            CellTextAdd(ws, "P7", txtCard.Date); 
            CellTextAdd(ws, "A8", txtCard.NumberDate + " года");
            // ДАННЫЕ АВТОМОБИЛЯ
            if (auto != null)
            {
                CellText(ws, "C11", autoTxt.model);
                CellText(ws, "C13", autoTxt.vin);
                CellText(ws, "C15", autoTxt.sell_date);
                CellText(ws, "C17", autoTxt.year);
                CellText(ws, "C19", txtCard.Run);
                CellText(ws, "C21", autoTxt.licence_plate);
            }
            // Данные владельца
            CellText(ws, "N11", ownerTxt.name);
            CellText(ws, "N13", "Адрес: " + ownerTxt.address);
            CellTextAdd(ws, "N13", " Телефон: " + ownerTxt.phone);
            CellTextAdd(ws, "N13", " e-mail:" + ownerTxt.email);
            // Данные представителя
            CellText(ws, "N17", representTxt.name);
            CellText(ws, "N19", "Адрес: " + representTxt.address);
            CellTextAdd(ws, "N15", " Телефон: " + representTxt.phone);
            CellTextAdd(ws, "N15", " e-mail:" + representTxt.email);
            // Данные плательщика
            CellText(ws, "N23", payerTxt.name);
            CellText(ws, "N25", "Адрес: " + payerTxt.address);
            CellTextAdd(ws, "N25", " Телефон: " + payerTxt.phone);
            CellTextAdd(ws, "N25", " e-mail:" + payerTxt.email);

            bool first = true;
            string txt = "";
            foreach (DtTxtCardClaim claimTxt in claimsTxt)
            {
                if (first == false)
                {
                    txt += ", ";
                }
                else
                {
                    first = false;
                }
                txt += claimTxt.CardClaimText;
            }
            CellTextAdd(ws, "B31", txt);

            CellTextAdd(ws, "A81", representTxt.short_name);
            CellTextAdd(ws, "A84", representTxt.short_name);
            CellTextAdd(ws, "A88", representTxt.short_name);
            CellTextAdd(ws, "K92", representTxt.short_name);
            CellTextAdd(ws, "A92", txtCard.ServiceManagerShortName); 
            CellTextAdd(ws, "K89", txtCard.ServiceManagerShortName); 

            DateTime now = DateTime.Now;
            CellTextAdd(ws, "A93", now.ToShortDateString());
            CellTextAdd(ws, "A94", now.ToShortTimeString());

            CellText(ws, "J31", card.LoadPreviousRecomendations()); // Предыдущие рекомендации
        }

    }
}
