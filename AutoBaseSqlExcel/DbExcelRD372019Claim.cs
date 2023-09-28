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
    class DbExcelRD372019Claim : DbExcel
    {
        private readonly DtCard card = null;
        private readonly DtAuto auto = null;
        private readonly DtPartner owner = null;
        private readonly DtPartner represent = null;
        private readonly DtPartner payer = null;
        private readonly ArrayList claimsTxt = null;
        private readonly ArrayList worksTxt = null;
        private readonly ArrayList detailsTxt = null;
      
       
        public DbExcelRD372019Claim(DtCard theCard)
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
            ArrayList claims = new ArrayList();
            DbSqlCardClaim.SelectInArray(claims, card.Number, card.Year);
            claimsTxt = new ArrayList();
            foreach(DtCardClaim claim in claims)
            {
                claimsTxt.Add(new DtTxtCardClaim(claim));
                //claimsTxt.Add(new DtCardClaim.DtCardClaimTxt(claim));
            }
            ArrayList works = new ArrayList();
            DbSqlCardWork.SelectInArray(card, works);
            worksTxt = new ArrayList();
            foreach (DtCardWork work in works)
            {
                // worksTxt.Add(new DtCardWork.DtCardWorkTxt(work));
                worksTxt.Add(new DtTxtCardWork(work));
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
           // DtCard.DtCardTxt cardTxt = new DtCard.DtCardTxt(card);
            DtPartner.DtPartnerTxt ownerTxt = new DtPartner.DtPartnerTxt(owner);
            DtPartner.DtPartnerTxt representTxt = new DtPartner.DtPartnerTxt(represent);
            DtPartner.DtPartnerTxt payerTxt = new DtPartner.DtPartnerTxt(payer);
            // ШАПКА
            // НОМЕР И ДАТА
            CellTextAdd(ws, "A8", txtCard.NumberDate + " года"); //CellTextAdd(ws, "A8", cardTxt.number_date + " года");
            // ДАННЫЕ АВТОМОБИЛЯ
            if (auto != null)
            {
                CellText(ws, "C11", autoTxt.model);
                CellText(ws, "C12", autoTxt.vin);
                CellText(ws, "C13", autoTxt.sell_date);
                CellText(ws, "C14", autoTxt.year);
                CellText(ws, "C15", txtCard.Run); //CellText(ws, "C15", cardTxt.run);
                CellText(ws, "C16", autoTxt.licence_plate);
            }
            // Данные владельца
            CellText(ws, "O11", ownerTxt.name);
            CellText(ws, "O12", "Адрес: " + ownerTxt.address);
            CellTextAdd(ws, "O12", " Телефон: " + ownerTxt.phone);
            CellTextAdd(ws, "O12", " e-mail:" + ownerTxt.email);
            // Данные представителя
            CellText(ws, "O14", representTxt.name);
            CellText(ws, "O15", "Адрес: " + representTxt.address);
            CellTextAdd(ws, "O15", " Телефон: " + representTxt.phone);
            CellTextAdd(ws, "O15", " e-mail:" + representTxt.email);
            // Данные плательщика
            CellText(ws, "O17", payerTxt.name);
            CellText(ws, "O19", "Адрес: " + payerTxt.address);
            CellTextAdd(ws, "O19", " Телефон: " + payerTxt.phone);
            CellTextAdd(ws, "O19", " e-mail:" + payerTxt.email);

            // Данные карточки - время
            CellTextAdd(ws, "A21", txtCard.AcceptanceDateTime); //CellTextAdd(ws, "A21", cardTxt.acceptance_datetime);
            CellTextAdd(ws, "F21", txtCard.AgreedPickUpDateTime);  //CellTextAdd(ws, "F21", cardTxt.agreedPickUpTime);
            if (txtCard.AgreedPickUpDateTime == "Не согласованно!")//if (cardTxt.agreedPickUpTime == "Не согласованно!")
                CellsColor(ws, "F21", "F21", 3);

            int offset;
            bool first = true;
            int pos = 24;
            int count = 1;
            //foreach(DtCardClaim.DtCardClaimTxt claimTxt in claimsTxt)
            foreach (DtTxtCardClaim claimTxt in claimsTxt)
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
                // CellText(ws, "B" + pos.ToString(), claimTxt.claim_txt);
                //CellText(ws, "AB" + pos.ToString(), claimTxt.claim_txt);
                CellText(ws, "B" + pos.ToString(), claimTxt.CardClaimText);
                CellText(ws, "AB" + pos.ToString(), claimTxt.CardClaimText);
                CellText(ws, "A" + pos.ToString(), count.ToString());
            }
            offset = count - 1;
            first = true;
            pos += 5;
            count = 1;
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
                CellText(ws, "B" + pos.ToString(), workTxt.CatalogueNumber); // CellText(ws, "B" + pos.ToString(), workTxt.catalogNumber);
                CellText(ws, "E" + pos.ToString(), workTxt.WorkName); //CellText(ws, "E" + pos.ToString(), workTxt.workName);
                CellText(ws, "AC" + pos.ToString(), workTxt.WorkName); //CellText(ws, "AC" + pos.ToString(), workTxt.workName);
                CellText(ws, "A" + pos.ToString(), count.ToString());
                CellText(ws, "H" + pos.ToString(), workTxt.OperationAmount); //CellText(ws, "H" + pos.ToString(), workTxt.amount);
                double value = Convert.ToDouble(workTxt.Amount); //double value = Convert.ToDouble(workTxt.value);
                if (value != 0)
                {
                    CellDouble(ws, "I" + pos.ToString(), value);
                    CellText(ws, "L" + pos.ToString(), "Н/Ч");
                }
                else
                {
                    CellDouble(ws, "I" + pos.ToString(), 1);
                    CellText(ws, "L" + pos.ToString(), "ШТ");
                }
                //CellText(ws, "I" + pos.ToString(), ",1");
                //CellText(ws, "I" + pos.ToString(), "="+workTxt.value);
                //CellTextNumber(ws, "I" + pos.ToString(), workTxt.value);
                CellText(ws, "N" + pos.ToString(), workTxt.Price); //CellText(ws, "N" + pos.ToString(), workTxt.price);
                RowResize(ws, pos);
            }
            offset += (count - 1);
            CellTextFormula(ws, "H" + (30 + offset).ToString(), "=SUM(H" + (29 + offset - count + 1).ToString() + ":H" + (29 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "I" + (30 + offset).ToString(), "=SUM(I" + (29 + offset - count + 1).ToString() + ":I" + (29 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "R" + (30 + offset).ToString(), "=SUM(R" + (29 + offset - count + 1).ToString() + ":R" + (29 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "U" + (30 + offset).ToString(), "=SUM(U" + (29 + offset - count + 1).ToString() + ":U" + (29 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "W" + (30 + offset).ToString(), "=SUM(W" + (29 + offset - count + 1).ToString() + ":W" + (29 + offset - count + count).ToString() + ")");
            string summworkTxt = GET_CellText(ws, "W" + (30 + offset).ToString());
            double summWork = Convert.ToDouble(summworkTxt);

            first = true;
            pos += 6;
            count = 1;
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
                CellText(ws, "I" + pos.ToString(), detailTxt.amount);
                CellText(ws, "N" + pos.ToString(), detailTxt.price);
            }
            offset += (count - 1);
            CellTextFormula(ws, "I" + (36 + offset).ToString(), "=SUM(I" + (35 + offset - count + 1).ToString() + ":I" + (35 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "R" + (36 + offset).ToString(), "=SUM(R" + (35 + offset - count + 1).ToString() + ":R" + (35 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "U" + (36 + offset).ToString(), "=SUM(U" + (35 + offset - count + 1).ToString() + ":U" + (35 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "W" + (36 + offset).ToString(), "=SUM(W" + (35 + offset - count + 1).ToString() + ":W" + (35 + offset - count + count).ToString() + ")");
            summworkTxt = GET_CellText(ws, "W" + (36 + offset).ToString());
            summWork += Convert.ToDouble(summworkTxt);
            int panny = Convert.ToInt32((summWork - (double)Convert.ToInt32(summWork)) * 100);
          
            string txt_summ_text = UI_Digit2Text.Convert(Convert.ToInt32(summWork));
            string txt_panny_text = UI_Digit2Text.Panny(panny);
            summworkTxt = summWork.ToString() + " РУБ " + "(" + txt_summ_text + " " + panny.ToString() + " " + txt_panny_text + ")";

          


            CellTextAdd(ws, "N" + (52 + offset).ToString(), " Телефон: " + representTxt.phone);
            CellTextAdd(ws, "N" + (52 + offset).ToString(), " e-mail:" + representTxt.email);

            CellTextAdd(ws, "A" + (38 + offset).ToString(), summworkTxt);
            CellTextAdd(ws, "A" + (56 + offset).ToString(), txtCard.ServiceManagerShortName); //CellTextAdd(ws, "A" + (56 + offset).ToString(), cardTxt.serviceManager);
            CellTextAdd(ws, "A" + (54 + offset).ToString(), representTxt.short_name);
            CellTextAdd(ws, "R" + (51 + offset).ToString(), representTxt.short_name);
        }

    }
}
