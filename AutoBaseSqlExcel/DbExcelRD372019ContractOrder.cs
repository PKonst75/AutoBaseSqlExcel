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
    class DbExcelRD372019ContractOrder:DbExcel
    {
        private readonly DtCard card = null;
        private readonly DtAuto auto = null;
        private readonly DtPartner owner = null;
        private readonly DtPartner represent = null;
        private readonly DtPartner payer = null;
        private readonly ArrayList claimsTxt = null;
        private readonly ArrayList worksTxt = null;
        private readonly ArrayList detailsTxt = null;
        private readonly ArrayList recomendsTxt = null;
        public DbExcelRD372019ContractOrder(DtCard theCard)
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

            ArrayList recomends = new ArrayList();
            DbSqlCardRecomendation.SelectInArray(recomends, card.Number, card.Year);
            recomendsTxt = new ArrayList();
            foreach (DtCardRecomendation recomend in recomends)
            {
                recomendsTxt.Add(recomend.RecomendationTxt);
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
                CellText(ws, "C15", txtCard.Run);//CellText(ws, "C15", cardTxt.run);
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
            CellTextAdd(ws, "A21", txtCard.AcceptanceDateTime);//CellTextAdd(ws, "A21", cardTxt.acceptance_datetime);
            CellTextAdd(ws, "F21", txtCard.WorkendDateTime); //CellTextAdd(ws, "F21", cardTxt.workend_datetime);
            if (txtCard.WorkendDateTime == "Не установленно!")//if (cardTxt.workend_datetime == "Не установленно!")
                CellsColor(ws, "F21", "F21", 3);

            
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
            CellTextAdd(ws, "A23", txt);

            int count = 1;
            first = true;
            int pos = 31 ;
            int offset;
            foreach (DtTxtCardWork workTxt in worksTxt)//foreach (DtCardWork.DtCardWorkTxt workTxt in worksTxt)
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
                CellText(ws, "B" + pos.ToString(), workTxt.CatalogueNumber);//CellText(ws, "B" + pos.ToString(), workTxt.catalogNumber);
                CellText(ws, "E" + pos.ToString(), workTxt.WorkName);//CellText(ws, "E" + pos.ToString(), workTxt.workName);
                CellText(ws, "AC" + pos.ToString(), workTxt.WorkName);//CellText(ws, "AC" + pos.ToString(), workTxt.workName);
                CellText(ws, "A" + pos.ToString(), count.ToString());
                CellText(ws, "J" + pos.ToString(), workTxt.OperationAmount);//CellText(ws, "J" + pos.ToString(), workTxt.amount);
                double value = Convert.ToDouble(workTxt.Amount);//double value = Convert.ToDouble(workTxt.value);
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
                CellText(ws, "P" + pos.ToString(), workTxt.Price);//CellText(ws, "P" + pos.ToString(), workTxt.price);
                CellText(ws, "X" + pos.ToString(), workTxt.Executors);//CellText(ws, "X" + pos.ToString(), workTxt.executor);
                RowResize(ws, pos);
            }
            offset = (count - 1);
            string tmp_txt = GET_CellText(ws, "A" + (34 + offset).ToString());
            tmp_txt = tmp_txt.Replace("&ORDNUM", txtCard.NumberDate);//tmp_txt = tmp_txt.Replace("&ORDNUM", cardTxt.number_date);
            CellText(ws, "A" + (34 + offset).ToString(), tmp_txt);

            CellTextFormula(ws, "I" + (32 + offset).ToString(), "=SUM(I" + (31).ToString() + ":I" + (31 + offset).ToString() + ")");
            CellTextFormula(ws, "J" + (32 + offset).ToString(), "=SUM(J" + (31).ToString() + ":J" + (31 + offset).ToString() + ")");
            CellTextFormula(ws, "S" + (32 + offset).ToString(), "=SUM(S" + (31).ToString() + ":S" + (31 + offset).ToString() + ")");
            CellTextFormula(ws, "U" + (32 + offset).ToString(), "=SUM(U" + (31).ToString() + ":U" + (31 + offset).ToString() + ")");
            CellTextFormula(ws, "W" + (32 + offset).ToString(), "=SUM(W" + (31).ToString() + ":W" + (31 + offset).ToString() + ")");
            string summworkTxt = GET_CellText(ws, "W" + (32 + offset).ToString());
            double summWork = Convert.ToDouble(summworkTxt);

            first = true;
            pos += 8;
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
                CellText(ws, "J" + pos.ToString(), detailTxt.amount);
                CellText(ws, "P" + pos.ToString(), detailTxt.price);
            }
            offset += (count - 1);
           

            CellTextFormula(ws, "J" + (40 + offset).ToString(), "=SUM(J" + (39 + offset - count + 1).ToString() + ":J" + (39 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "S" + (40 + offset).ToString(), "=SUM(S" + (39 + offset - count + 1).ToString() + ":S" + (39 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "U" + (40 + offset).ToString(), "=SUM(U" + (39 + offset - count + 1).ToString() + ":U" + (39 + offset - count + count).ToString() + ")");
            CellTextFormula(ws, "X" + (40 + offset).ToString(), "=SUM(X" + (39 + offset - count + 1).ToString() + ":X" + (39 + offset - count + count).ToString() + ")");
            summworkTxt = GET_CellText(ws, "X" + (40 + offset).ToString());        
            summWork += Convert.ToDouble(summworkTxt);
            int panny = Convert.ToInt32((summWork - (double)Convert.ToInt32(summWork)) * 100);

            string txt_summ_text = UI_Digit2Text.Convert(Convert.ToInt32(summWork));
            string txt_panny_text = UI_Digit2Text.Panny(panny);
            summworkTxt = summWork.ToString() + " РУБ " + "(" + txt_summ_text + " " + panny.ToString() + " " + txt_panny_text + ")";


            CellTextAdd(ws, "A" + (50 + offset).ToString(), "");

            // Рекомендации
            first = true;
            txt = "";
            foreach (string recomendTxt in recomendsTxt)
            {
                if (first == false)
                {
                    txt += ", ";
                }
                else
                {
                    first = false;
                }
                txt += recomendTxt;
            }
            CellTextAdd(ws, "A" + (56 + offset), txt);



            CellTextAdd(ws, "A" + (42 + offset).ToString(), summworkTxt);
            CellTextAdd(ws, "H" + (66 + offset).ToString(), txtCard.ServiceManagerShortName); //CellTextAdd(ws, "H" + (66 + offset).ToString(), cardTxt.serviceManager);
            CellTextAdd(ws, "A" + (66 + offset).ToString(), representTxt.short_name);
        }
    }
}
