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
    class DbExcelRD372019WorkOrder:DbExcel
    {
        private readonly DtCard card = null;
        private readonly DtAuto auto = null;
        private readonly DtPartner owner = null;
        private readonly DtPartner represent = null;
        private readonly DtPartner payer = null;
        private readonly ArrayList claimsTxt = null;
        private readonly ArrayList worksTxt = null;
        private readonly ArrayList detailsTxt = null;

        public DbExcelRD372019WorkOrder(DtCard theCard)
        {
            card = theCard; // new DtCard(theCard);
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
            ArrayList claims = new ArrayList();
            DbSqlCardClaim.SelectInArray(claims, card.Number, card.Year);
            claimsTxt = new ArrayList();
            foreach (DtCardClaim claim in claims)
            {
                claimsTxt.Add(new DtTxtCardClaim(claim));
                //claimsTxt.Add(new DtCardClaim.DtCardClaimTxt(claim));
            }
            ArrayList works = new ArrayList();
            DbSqlCardWork.SelectInArray(card, works);
            worksTxt = new ArrayList();
            foreach (DtCardWork work in works)
            {
                worksTxt.Add(new DtTxtCardWork(work));//worksTxt.Add(new DtCardWork.DtCardWorkTxt(work));
            }
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
            //DtCard.DtCardTxt cardTxt = new DtCard.DtCardTxt(card);
            DtPartner.DtPartnerTxt ownerTxt = new DtPartner.DtPartnerTxt(owner);
            DtPartner.DtPartnerTxt representTxt = new DtPartner.DtPartnerTxt(represent);
            DtPartner.DtPartnerTxt payerTxt = new DtPartner.DtPartnerTxt(payer);
            // ШАПКА
            // НОМЕР И ДАТА
            CellTextAdd(ws, "A1", txtCard.NumberDate + " года"); //CellTextAdd(ws, "A1", cardTxt.number_date + " года");
            // ДАННЫЕ АВТОМОБИЛЯ
            if (auto != null)
            {
                CellText(ws, "I4", autoTxt.vin);
                CellText(ws, "D4", autoTxt.licence_plate);
            }
        
            bool first = true;
            string txt = "";
            //foreach (DtCardClaim.DtCardClaimTxt claimTxt in claimsTxt)
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
                //txt += claimTxt.claim_txt;
                txt += claimTxt.CardClaimText;
            }
            CellTextAdd(ws, "A6", txt);
            CellTextAdd(ws, "AB6", txt);

            first = true;
            int pos = 8;
            int count = 1;
            foreach (DtTxtCardWork workTxt in worksTxt) //foreach (DtCardWork.DtCardWorkTxt workTxt in worksTxt)
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
                CellText(ws, "B" + pos.ToString(), workTxt.WorkName);//CellText(ws, "B" + pos.ToString(), workTxt.workName);
                CellText(ws, "I" + pos.ToString(), workTxt.WorkName);//CellText(ws, "I" + pos.ToString(), workTxt.workName);
                CellText(ws, "AC" + pos.ToString(), workTxt.WorkName);//CellText(ws, "AC" + pos.ToString(), workTxt.workName);
                CellText(ws, "A" + pos.ToString(), count.ToString());
            }
        }
    }
}
