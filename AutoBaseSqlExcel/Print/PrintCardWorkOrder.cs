using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoBaseSql
{
    class PrintCardWorkOrder:PrintCard
    {
        const float BLOCKDATETIME_WIDTH_COLUMN1 = 40.0F;
        const float BLOCKDATETIME_WIDTH_COLUMN2 = 40.0F;
        const float BLOCKDATETIME_WIDTH_COLUMN3 = 60.0F;
        const float BLOCKDATETIME_WIDTH_COLUMN4 = 15.0F;
        const float BLOCKDATETIME_WIDTH_COLUMN5 = PAGE_RIGHT_X - BLOCKDATETIME_WIDTH_COLUMN1 - BLOCKDATETIME_WIDTH_COLUMN2 - BLOCKDATETIME_WIDTH_COLUMN3 - BLOCKDATETIME_WIDTH_COLUMN4 - PAGE_LEFT_X;
        const float BLOCKDATETIME_ROW_HEIGHT = 8.0F;
        const float BLOCKDATETIME_BOTTOMOFFSET = 2.0F;

        private readonly CalculatorCard _calculator;
        public PrintCardWorkOrder(DtCard srcCard, CalculatorCard srcCalculatorCard) : base(srcCard)
        {
            _calculator = srcCalculatorCard;
			_calculator.Calculate(_card);
		}
        public override void PrintDocument()
        {
            PrintBlockHead(BlockDateTime, null);
			PrintBlockHead(BlockClaim, null);
			PrintBlockHead(BlockClaimAnalize, null);
			bool first = true;
            int count = 1;
            if (_card.CardWorks.Count > 0)
            {
                foreach (DtCardWork element in _card.CardWorks)
                {
                    PrintBlockHead(new DelegatePrintBlock(PrintWorkBlock), element, new DelegatePrintBlock(PrintWorkBlockHead), first, count);
                    count++;
                    first = false;
                }
                PrintBlockHead(new DelegatePrintBlock(PrintWorkBlockTotal), null);
            }
			PrintBlockHead(BlockClaimWarning, null);
			first = true;
			count = 1;
			if (_card.CardDetails.Count > 0)
			{
				foreach (DtCardDetail element in _card.CardDetails)
				{
					PrintBlockHead(new DelegatePrintBlock(PrintDetailBlock), element, new DelegatePrintBlock(PrintDetailBlockHead), first, count);
					count++;
					first = false;
				}
				PrintBlockHead(new DelegatePrintBlock(PrintDetailBlockTotal), null);
			}
			PrintBlockHead(BlockTotalSumm, null);
			PrintBlockHead(BlockGuaranty, null);
			PrintBlockHead(BlockQuolity, null);
			PrintBlockHead(BlockRecomendation, null);
			PrintBlockHead(BlockFinal, null);
		}
        public override float PrintDocumentTitle(float offset)
        {
            const int DOCTITLE_HEIGHT = 8;
            string txt = "ДОГОВОР ЗАКАЗ-НАРЯД " + _cardTxt.NumberDate + "\n";
            txt += "к акту приема передачи" + _cardTxt.NumberDate;
            SetStringFormat(StringAlignment.Center, StringAlignment.Center);
            return SelectedPrintTextNoBox(txt, PAGE_LEFT_X, offset, PAGE_RIGHT_X - PAGE_LEFT_X, DOCTITLE_HEIGHT);       
        }
        #region Блоки
        public float BlockDateTime(float offset, object print_data, int coutCounter = 0)
        {
            float y = offset;
            object[] line_data = new object[5];
            line_data[0] = new object[] { "Дата и время приемки автомобиля: " + _cardTxt.AcceptanceDateTime, BLOCKDATETIME_WIDTH_COLUMN1 };
            line_data[1] = new object[] { "Дата и время окончания работ: " + _cardTxt.WorkendDateTime, BLOCKDATETIME_WIDTH_COLUMN2 };
            line_data[2] = new object[] { "Стоимость передаваемого автомобиля по согласованию сторон составляет:", BLOCKDATETIME_WIDTH_COLUMN3 };
            line_data[3] = new object[] { "Статус:\nЗакрыт", BLOCKDATETIME_WIDTH_COLUMN4 };
            line_data[4] = new object[] { "Вид ремонта:", BLOCKDATETIME_WIDTH_COLUMN5 };
            y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, BLOCKDATETIME_ROW_HEIGHT);
            return y + BLOCKDATETIME_BOTTOMOFFSET;
        }

		public float BlockClaim(float offset, object print_data = null, int coutCounter = 0)
		{
			const int BLOCK_CLAIM_HEIGHT = 8;
			string txt = "Причина обращения: " + _cardTxt.ClaimsList;
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return offset + SelectedPrintTextBox(txt, PAGE_LEFT_X, offset, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_CLAIM_HEIGHT);
		}
		public float BlockClaimAnalize(float offset, object print_data = null, int coutCounter = 0)
		{
			const int BLOCK_CLAIM_HEIGHT = 10;
			string txt = "Описание дефектов и результаты диагностики: ";
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return offset + SelectedPrintTextBox(txt, PAGE_LEFT_X, offset, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_CLAIM_HEIGHT);
		}
		public float BlockClaimWarning(float offset, object print_data = null, int coutCounter = 0)
		{
			const int BLOCK_CLAIMWARNING_HEIGHT = 10;
			string txt = "Другие заявленные заказчиком причины обращения указанные в графе \"Причина обращения\" Акта приема-передачи автомобиля ";
			txt += _cardTxt.NumberDate + " и заявки-договоре-заказе-наряде ";
			txt += _cardTxt.NumberDate + " и  не указанные в графе «Выполненные работы», не подтвердились.";

			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return offset + SelectedPrintTextBox(txt, PAGE_LEFT_X, offset, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_CLAIMWARNING_HEIGHT);
		}

		public float BlockTotalSumm(float offset, object print_data = null, int coutCounter = 0)
		{
			const float BLOCK_TOTALSUMM_HEIGHT = 4;
			const float BLOCK_TOTALSUMM_OFFSETT = 2;

			float y = offset + BLOCK_TOTALSUMM_OFFSETT;

			CalculatorResult resW = _calculator.WorksTotal;
			CalculatorResult resD = _calculator.DetailsTotal;

			float summ = resW.SummTotal + resD.SummTotal;

			string txt = "Итого по договору-заказу-наряду ";
			int panny = Convert.ToInt32((summ - (double)Convert.ToInt32(summ)) * 100);

			string txt_summ_text = UI_Digit2Text.Convert(Convert.ToInt32(summ));
			string txt_panny_text = UI_Digit2Text.Panny(panny);
			txt += summ.ToString() + " РУБ " + "(" + txt_summ_text + " " + panny.ToString() + " " + txt_panny_text + ")";

			SelectFont(_fontArial8Bold);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return y + SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_TOTALSUMM_HEIGHT);
		}

		public float BlockGuaranty(float offset, object print_data = null, int coutCounter = 0)
		{
			const float BLOCK_GUARANTY_HEIGHT = 4;
			const float BLOCK_GUARANTY_OFFSET = 1;

			float y = offset + BLOCK_GUARANTY_OFFSET;
			string txt = "Гарантия на работы: по ремонту и обслуживанию, а также на приобретенные и установленные у Исполнителя запасные части – 30 дней. пробега; техническое обслуживание - 20 дней. На работы по заправке системы кондиционирования – 14 дней; На контрольно-регулировочные (регулировка развала-схождения колес и т.п.) – 2 дня.";
			SelectFont(_fontArial6);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return y + SelectedPrintTextBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_GUARANTY_HEIGHT);
		}

		public float BlockQuolity(float offset, object print_data = null, int coutCounter = 0)
		{
			const float BLOCK_QUOLITY_HEIGHT = 4;
			const float BLOCK_QUOLITY_OFFSET = 1;

			float y = offset + BLOCK_QUOLITY_OFFSET;
			string txt = "Автомобиль по итогам выполнения заявленных работ проверил (полноту, качество работ, комплектность). Мастер цеха ________________________/____________________/";
			SelectFont(_fontArial6);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			return y + SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_QUOLITY_HEIGHT);
		}

		public float BlockRecomendation(float offset, object print_data = null, int coutCounter = 0)
		{
			const float BLOCK_RECOMEND_HEIGHT = 4;
			const float BLOCK_RECOMEND_OFFSET = 1;

			float y = offset + BLOCK_RECOMEND_OFFSET;
			string txt = "Следующее ТО нужно пройти до пробега _______________  км или не позднее  ___._____.20____ г., что наступит ранее.";
			SelectFont(_fontArial6);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			y += SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_RECOMEND_HEIGHT);
			txt = "Рекомендации по поддержанию автомобиля в технически исправном состоянии.";
			y += SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_RECOMEND_HEIGHT);
			txt = _cardTxt.RecomendationsList;
			y += SelectedPrintTextBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_RECOMEND_HEIGHT);
			txt = "От выполнения рекомендаций, указанных в настоящем договоре-заказе-наряде, по поддержанию автомобиля в технически исправном состоянии заказчик отказался.";
			y += SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_RECOMEND_HEIGHT);
			return y;
		}

		public float BlockFinal(float offset, object print_data = null, int coutCounter = 0)
		{
			const float BLOCK_FINAL_HEIGHT = 4;
			const float BLOCK_FINAL_OFFSET = 1;
			const float BLOCK_FINAL_RINGHTPART_X = 120;
			const float BLOCK_FINAL_DATEWIDTH = 60;

			float y = offset + BLOCK_FINAL_OFFSET;
			string txt = "Вышеперечисленные услуги выполнены полностью и в срок. Заказчик по объему, качеству и срокам оказания услуг, а также внешнему виду и комплектности автомобиля претензий не имеет. Экземпляр договора-заказа-наряда получил.";
			SelectFont(_fontArial6);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			y += SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, BLOCK_FINAL_HEIGHT);
			txt = "Заказчик ___________________ /" + _cardTxt.RepresentTitleShort + "/";
			SelectedPrintTextNoBox(txt, PAGE_LEFT_X, y, BLOCK_FINAL_RINGHTPART_X - PAGE_LEFT_X, BLOCK_FINAL_HEIGHT);
			txt = "Мастер Консультант ___________________ /" + _cardTxt.ServiceManagerShortName +"/";
			y += SelectedPrintTextNoBox(txt, BLOCK_FINAL_RINGHTPART_X, y, PAGE_RIGHT_X - BLOCK_FINAL_RINGHTPART_X, BLOCK_FINAL_HEIGHT);
			txt = "Дата печати документа " + DateTime.Now.ToShortDateString();
			y += SelectedPrintTextBox(txt, PAGE_RIGHT_X - BLOCK_FINAL_DATEWIDTH, y, BLOCK_FINAL_DATEWIDTH, BLOCK_FINAL_HEIGHT);
			return y;
		}
        #endregion
        #region Работы
        private float PrintWorkBlock(float offset, object o, int count)
		{
			const int ROW_HEIGHT = 4;
			const int TOP_OFFSET = 0;

			DtCardWork work = (DtCardWork)o;
			CalculatorResult res = _calculator.WorkCalculator.Calculate(work);
			DtTxtCardWork txtCardWork = new DtTxtCardWork(work);

			object[] line_data;
			if (work.GuaranteeFlag())
			{
				line_data = new object[8];
				line_data[0] = new object[] { count.ToString(), WORK_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardWork.CatalogueNumber, WORK_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardWork.WorkName, WORK_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { txtCardWork.OperationAmount, WORK_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardWork.Amount, WORK_WIDTH_COLUMN5 };
				line_data[5] = new object[] { txtCardWork.Unit, WORK_WIDTH_COLUMN6 };
				line_data[6] = new object[] { "Гарантия", WORK_WIDTH_COLUMN7 + WORK_WIDTH_COLUMN8 + WORK_WIDTH_COLUMN9 + WORK_WIDTH_COLUMN10 + WORK_WIDTH_COLUMN11 };
				line_data[5] = new object[] { txtCardWork.Executors, WORK_WIDTH_COLUMN12 };
			}
			else
			{
				line_data = new object[12];
				line_data[0] = new object[] { count.ToString(), WORK_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardWork.CatalogueNumber, WORK_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardWork.WorkName, WORK_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { txtCardWork.OperationAmount, WORK_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardWork.Amount, WORK_WIDTH_COLUMN5 };
				line_data[5] = new object[] { txtCardWork.Unit, WORK_WIDTH_COLUMN6 };
				line_data[6] = new object[] { txtCardWork.Price, WORK_WIDTH_COLUMN7 };
				line_data[7] = new object[] { res.SummTotalDiscountBonus.ToString(), WORK_WIDTH_COLUMN8 };
				line_data[8] = new object[] { res.SummVAT.ToString(), WORK_WIDTH_COLUMN9 };
				line_data[9] = new object[] { res.SummTotalNoVAT.ToString(), WORK_WIDTH_COLUMN10 };
				line_data[10] = new object[] { res.SummTotal.ToString(), WORK_WIDTH_COLUMN11 };
				line_data[11] = new object[] { txtCardWork.Executors.ToString(), WORK_WIDTH_COLUMN12 };
			}

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial6);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintWorkBlockTotal(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;

			CalculatorResult res = _calculator.WorksTotal;

			object[] line_data = new object[9];
			line_data[0] = new object[] { "ИТОГО", WORK_WIDTH_COLUMN1 + WORK_WIDTH_COLUMN2 + WORK_WIDTH_COLUMN3 };
			line_data[1] = new object[] { res.SimpleAmount.ToString(), WORK_WIDTH_COLUMN4 };
			line_data[2] = new object[] { res.SimpleAmountUnit.ToString(), WORK_WIDTH_COLUMN5 };
			line_data[3] = new object[] { "", WORK_WIDTH_COLUMN6 };
			line_data[4] = new object[] { "", WORK_WIDTH_COLUMN7 };
			line_data[5] = new object[] { res.SummTotalDiscountBonus.ToString(), WORK_WIDTH_COLUMN8 };
			line_data[6] = new object[] { res.SummVAT.ToString(), WORK_WIDTH_COLUMN9 };
			line_data[7] = new object[] { res.SummTotalNoVAT.ToString(), WORK_WIDTH_COLUMN10 };
			line_data[8] = new object[] { res.SummTotal.ToString(), WORK_WIDTH_COLUMN11 + WORK_WIDTH_COLUMN12 };


			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial8Bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintWorkBlockHead(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;
			float y = offset + TOP_OFFSET;

			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial6Bold);

			y += SelectedPrintTextBox("Выполненные работы", PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, ROW_HEIGHT);

			object[] line_data = new object[12];
			line_data[0] = new object[] { "№ПП", WORK_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "Код работы", WORK_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Наименование работы", WORK_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "К.оп.", WORK_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "К-во", WORK_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "Ед.Изм.", WORK_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "Цена", WORK_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "Скидка", WORK_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "Сумма НДС", WORK_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "Всего без НДС", WORK_WIDTH_COLUMN10 };
			line_data[10] = new object[] { "Всего с НДС", WORK_WIDTH_COLUMN11 };
			line_data[11] = new object[] { "Исполнитель", WORK_WIDTH_COLUMN12 };		
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);

			line_data[0] = new object[] { "1", WORK_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "2", WORK_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "3", WORK_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "4", WORK_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "5", WORK_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "6", WORK_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "7", WORK_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "8", WORK_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "9", WORK_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "10", WORK_WIDTH_COLUMN10 };
			line_data[10] = new object[] { "11", WORK_WIDTH_COLUMN11 };
			line_data[11] = new object[] { "12", WORK_WIDTH_COLUMN12 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);

			return y;
		}
		#endregion
		#region Детали
		private float PrintDetailBlockHead(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 3;

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial6Bold);

			y += SelectedPrintTextBox("Расходная накладная к заказ-наряду", PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, ROW_HEIGHT);

			object[] line_data = new object[10];
			line_data[0] = new object[] { "% ПП", DETAIL_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "№ по каталогу", DETAIL_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Наименование детали", DETAIL_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "Кол-во", DETAIL_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "Ед. изм.", DETAIL_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "Скидка", DETAIL_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "Цена", DETAIL_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "Сумма НДС", DETAIL_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "Всего без НДС", DETAIL_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "Всего с НДС", DETAIL_WIDTH_COLUMN10 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			line_data[0] = new object[] { "1", DETAIL_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "2", DETAIL_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "3", DETAIL_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "4", DETAIL_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "5", DETAIL_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "6", DETAIL_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "7", DETAIL_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "8", DETAIL_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "9", DETAIL_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "10", DETAIL_WIDTH_COLUMN10 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintDetailBlock(float offset, object o, int count)
		{
			const int ROW_HEIGHT = 4;
			const int TOP_OFFSET = 0;

			DtCardDetail detail = (DtCardDetail)o;
			CalculatorResult res = _calculator.DetailCalculator.Calculate(detail);
			DtTxtCardDetail txtCardDetail = new DtTxtCardDetail(detail);

			object[] line_data;
			if (detail.GuaranteeFlag())
			{
				line_data = new object[10];
				line_data[0] = new object[] { count.ToString(), DETAIL_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardDetail.CatalogueNumber, DETAIL_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardDetail.WorkName, DETAIL_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardDetail.Unit, DETAIL_WIDTH_COLUMN5 };
				line_data[5] = new object[] { "Гарантия", DETAIL_WIDTH_COLUMN6 + DETAIL_WIDTH_COLUMN7 + DETAIL_WIDTH_COLUMN8 + DETAIL_WIDTH_COLUMN9 + DETAIL_WIDTH_COLUMN10 };
			}
			else
			{
				line_data = new object[10];
				line_data[0] = new object[] { count.ToString(), DETAIL_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardDetail.CatalogueNumber, DETAIL_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardDetail.WorkName, DETAIL_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardDetail.Unit, DETAIL_WIDTH_COLUMN5 };
				line_data[5] = new object[] { res.SummTotalDiscountBonus.ToString(), DETAIL_WIDTH_COLUMN6 };
				line_data[6] = new object[] { txtCardDetail.Price, DETAIL_WIDTH_COLUMN7 };
				line_data[7] = new object[] { res.SummVAT.ToString(), DETAIL_WIDTH_COLUMN8 };
				line_data[8] = new object[] { res.SummTotalNoVAT.ToString(), DETAIL_WIDTH_COLUMN9 };
				line_data[9] = new object[] { res.SummTotal.ToString(), DETAIL_WIDTH_COLUMN10 };
			}
			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial6);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintDetailBlockTotal(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;

			CalculatorResult res = _calculator.DetailsTotal;

			object[] line_data = new object[8];
			line_data[0] = new object[] { "ИТОГО", DETAIL_WIDTH_COLUMN1 + DETAIL_WIDTH_COLUMN2 + DETAIL_WIDTH_COLUMN3, ALIGN_X.RIGHT };
			line_data[1] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
			line_data[2] = new object[] { "", DETAIL_WIDTH_COLUMN5 };
			line_data[3] = new object[] { res.SummTotalDiscountBonus.ToString(), DETAIL_WIDTH_COLUMN6 };
			line_data[4] = new object[] { "", DETAIL_WIDTH_COLUMN7 };
			line_data[5] = new object[] { res.SummVAT.ToString(), DETAIL_WIDTH_COLUMN8 };
			line_data[6] = new object[] { res.SummTotalNoVAT.ToString(), DETAIL_WIDTH_COLUMN9 };
			line_data[7] = new object[] { res.SummTotal.ToString(), DETAIL_WIDTH_COLUMN10 };

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(_fontArial8Bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		#endregion
	}
}
