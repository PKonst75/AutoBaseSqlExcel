using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using AutoBaseSql.Print;

namespace AutoBaseSql
{
    class PrintRD37Act: PrintRD37	
	{
		const float CLAIM_BLOCK_X = 20; // 
		const float CLAIM_BLOCK_WIDTH1 = 60; // 
		const float CLAIM_BLOCK_WIDTH2 = PAGE_RIGHT_X - CLAIM_BLOCK_WIDTH1 - CLAIM_BLOCK_X; // 
		const float CLAIM_BLOCK_HEIGHT = 5; // 

		const float SIGN_HEIGHT = 6; // 
		const float SIGN_X = 40; // 
		const float SIGN_WIDTH = PAGE_RIGHT_X - SIGN_X; // 

		const float RADIO_HEIGHT = 6; // 
		const float RADIO_X = PAGE_LEFT_X; // 
		const float RADIO_WIDTH = 70; // 
		const float RADIO_BOX_HEIGHT = 4; // 
		const float RADIO_BOX_WIDTH = 15; // 

		const float INSPECTION_BLOCK_INTERVAL = 4; // 
		const float INSPECTION_BLOCK_X = PAGE_LEFT_X; // 
		const float INSPECTION2_BLOCK_X = INSPECTION_BLOCK_X + INSPECTION_BLOCK_WIDTH1 + INSPECTION_BLOCK_WIDTH2 + INSPECTION_BLOCK_WIDTH3 + INSPECTION_BLOCK_INTERVAL; // 
		const float INSPECTION_BLOCK_WIDTH3 = 10; // 
		const float INSPECTION_BLOCK_WIDTH2 = 10; // 
		const float INSPECTION_BLOCK_WIDTH1 = (PAGE_RIGHT_X - INSPECTION_BLOCK_X) / 2 - INSPECTION_BLOCK_WIDTH3 - INSPECTION_BLOCK_WIDTH2 - INSPECTION_BLOCK_INTERVAL; // 
		const float INSPECTION_BLOCK_HEIGHT = 3; // 

		const float INSPECTION_IMAGE_WIDTH = 80;
		const float INSPECTION_IMAGE_HEIGHT = 30;
		const float INSPECTION_IMAGE_X = PAGE_LEFT_X;

		const float FUEL_IMAGE_WIDTH = 10;
		const float FUEL_IMAGE_HEIGHT = 20;
		const float FUEL_IMAGE_X = PAGE_LEFT_X;

		const float INSPECTIONIMAGE_LEGEND_WIDTH = 30;
	
		const float INSPECTIONDESCRIPTION_X = INSPECTION_IMAGE_WIDTH + 20;
		const float INSPECTIONDESCRIPTION_WIDTH = PAGE_RIGHT_X - INSPECTIONDESCRIPTION_X;
		const float INSPECTIONDESCRIPTION_ROWHEIGHT = 4;

		const float BLOCK_SIGNS_X = PAGE_LEFT_X;
		const float BLOCK_SIGNS_WIDTH = 90;
		const float BLOCK_SIGNS_WIDTH2 = PAGE_RIGHT_X - BLOCK_SIGNS_X - BLOCK_SIGNS_WIDTH;

		private PrintableTable _tableClaim;
		private PrintableTable _tableInspection;
		private PrintableTable _tableInspection2;
		private PrintableTable _tableInspectionImageLegend;
		private PrintableTable _tableInspectionDescription;

		public PrintRD37Act(DtCard srcCard) : base(srcCard)
        {
			// Подготовка таблиц для печати

			_tableClaim = new PrintableTable(2, 2);
			_tableClaim.SetColumnWidth(1, CLAIM_BLOCK_WIDTH1);
			_tableClaim.SetColumnWidth(2, CLAIM_BLOCK_WIDTH2);
			_tableClaim.RowsHeight = CLAIM_BLOCK_HEIGHT;
			_tableClaim.AddCell(1, 1, "Причина обращения (описание неисправностей) со слов Клиента");
			_tableClaim.AddCell(1, 2, "Рекомендации специалиста при последнем обращении:");
			_tableClaim.AddCell(2, 1, _cardTxt.ClaimsList);
			_tableClaim.AddCell(2, 2, _cardTxt.RecomendationsList);

			_tableInspection = new PrintableTable(18, 3);
			_tableInspection.SetColumnWidth(1, INSPECTION_BLOCK_WIDTH1);
			_tableInspection.SetColumnWidth(2, INSPECTION_BLOCK_WIDTH2);
			_tableInspection.SetColumnWidth(3, INSPECTION_BLOCK_WIDTH3);
			_tableInspection.RowsHeight = INSPECTION_BLOCK_HEIGHT;
			_tableInspection.AddCell(1, 1, "Проверка автомобиля (внутри а/м)");
			_tableInspection.AddCell(1, 2, "Да");
			_tableInspection.AddCell(1, 3, "Нет");
			_tableInspection.AddCell(2, 1, "Работоспособность звукового сигнала");
			_tableInspection.AddCell(2, 2, "");
			_tableInspection.AddCell(2, 3, "");
			_tableInspection.AddCell(3, 1, "Работоспособность систем стеклоочистителей/омывателей,щетки");
			_tableInspection.AddCell(3, 2, "");
			_tableInspection.AddCell(3, 3, "");
			_tableInspection.AddCell(4, 1, "Кондиционер/отопитель");
			_tableInspection.AddCell(4, 2, "");
			_tableInspection.AddCell(4, 3, "");
			_tableInspection.AddCell(5, 1, "Индикация приборной панели / системы самодиагностики");
			_tableInspection.AddCell(5, 2, "");
			_tableInspection.AddCell(5, 3, "");
			_tableInspection.AddCell(6, 1, "Работоспособность внутреннего освещения");
			_tableInspection.AddCell(6, 2, "");
			_tableInspection.AddCell(6, 3, "");
			_tableInspection.AddCell(7, 1, "Стояночный тормоз");
			_tableInspection.AddCell(7, 2, "");
			_tableInspection.AddCell(7, 3, "");
			_tableInspection.AddCell(8, 1, "Свободный ход педали сцепления и тормоза");
			_tableInspection.AddCell(8, 2, "");
			_tableInspection.AddCell(8, 3, "");
			_tableInspection.AddCell(9, 1, "Линия инструментального контроля");
			_tableInspection.AddCell(9, 2, "");
			_tableInspection.AddCell(9, 3, "");
			_tableInspection.AddMergedCell(10, 1, "Внешний осмотр", 3);
			_tableInspection.AddCell(11, 1, "Целостность стекол а/м");
			_tableInspection.AddCell(11, 2, "");
			_tableInspection.AddCell(11, 3, "");
			_tableInspection.AddCell(12, 1, "Целостность стекол фар/зад.фонарей, противотуманных фар");
			_tableInspection.AddCell(12, 2, "");
			_tableInspection.AddCell(12, 3, "");
			_tableInspection.AddCell(13, 1, "Работоспособность наружного освещения");
			_tableInspection.AddCell(13, 2, "");
			_tableInspection.AddCell(13, 3, "");
			_tableInspection.AddCell(14, 1, "Работоспособность парковочных датчиков");
			_tableInspection.AddCell(14, 2, "");
			_tableInspection.AddCell(14, 3, "");
			_tableInspection.AddMergedCell(15, 1, "Проверка автомобиля (моторный отсек)", 3);
			_tableInspection.AddCell(16, 1, "Жидкость системы омывателя");
			_tableInspection.AddCell(16, 2, "");
			_tableInspection.AddCell(16, 3, "");
			_tableInspection.AddCell(17, 1, "Уровень/состояние масла ДВС");
			_tableInspection.AddCell(17, 2, "");
			_tableInspection.AddCell(17, 3, "");
			_tableInspection.AddCell(18, 1, "Уровень/состояние тормозной жидкости");
			_tableInspection.AddCell(18, 2, "");
			_tableInspection.AddCell(18, 3, "");

			_tableInspection2 = new PrintableTable(18, 3);
			_tableInspection2.SetColumnWidth(1, INSPECTION_BLOCK_WIDTH1);
			_tableInspection2.SetColumnWidth(2, INSPECTION_BLOCK_WIDTH2);
			_tableInspection2.SetColumnWidth(3, INSPECTION_BLOCK_WIDTH3);
			_tableInspection2.RowsHeight = INSPECTION_BLOCK_HEIGHT;
			_tableInspection2.AddCell(1, 1, "Проверка автомобиля (моторный отсек)");
			_tableInspection2.AddCell(1, 2, "Да");
			_tableInspection2.AddCell(1, 3, "Нет");
			_tableInspection2.AddCell(2, 1, "Уровень/состояние масла ГУРа");
			_tableInspection2.AddCell(2, 2, "");
			_tableInspection2.AddCell(2, 3, "");
			_tableInspection2.AddCell(3, 1, "Уровень/состояние масла КПП (при наличии щупа)");
			_tableInspection2.AddCell(3, 2, "");
			_tableInspection2.AddCell(3, 3, "");
			_tableInspection2.AddCell(4, 1, "Уровень/состояние охлаждающей жидкости");
			_tableInspection2.AddCell(4, 2, "");
			_tableInspection2.AddCell(4, 3, "");
			_tableInspection2.AddCell(5, 1, "Состояние ремня/ремней привода навесных агрегатов");
			_tableInspection2.AddCell(5, 2, "");
			_tableInspection2.AddCell(5, 3, "");
			_tableInspection2.AddCell(6, 1, "Визуальный контроль подкапотного пространства");
			_tableInspection2.AddCell(6, 2, "");
			_tableInspection2.AddCell(6, 3, "");
			_tableInspection2.AddMergedCell(7, 1, "Проверка автомобиля (а/м поднят на половину)", 3);
			_tableInspection2.AddCell(8, 1, "Амортизаторы герметичность");
			_tableInspection2.AddCell(8, 2, "");
			_tableInspection2.AddCell(8, 3, "");
			_tableInspection2.AddCell(9, 1, "Тормозные колодки/диски");
			_tableInspection2.AddCell(9, 2, "");
			_tableInspection2.AddCell(9, 3, "");
			_tableInspection2.AddCell(10, 1, "Состояние радиаторов системы охлаждения и кондиционера, КПП");
			_tableInspection2.AddCell(10, 2, "");
			_tableInspection2.AddCell(10, 3, "");
			_tableInspection2.AddMergedCell(11, 1, "Проверка автомобиля (а/м поднят)", 3);
			_tableInspection2.AddCell(12, 1, "Система выпуска ОГ");
			_tableInspection2.AddCell(12, 2, "");
			_tableInspection2.AddCell(12, 3, "");
			_tableInspection2.AddCell(13, 1, "Герметичность ДВС/КПП/РК/Рулевого управления");
			_tableInspection2.AddCell(13, 2, "");
			_tableInspection2.AddCell(13, 3, "");
			_tableInspection2.AddCell(14, 1, "Состояние шин и колесных дисков");
			_tableInspection2.AddCell(14, 2, "");
			_tableInspection2.AddCell(14, 3, "");
			_tableInspection2.AddCell(15, 1, "Состояние рез.тех.изделий, пыльники ШРУС, шаровые, рул.наконеч.");
			_tableInspection2.AddCell(15, 2, "");
			_tableInspection2.AddCell(15, 3, "");
			_tableInspection2.AddCell(16, 1, "Люфт рулевого управления/элементов подвески (органолептически)");
			_tableInspection2.AddCell(16, 2, "");
			_tableInspection2.AddCell(16, 3, "");
			_tableInspection2.AddCell(17, 1, "Подшипники ступиц (органолептически)");
			_tableInspection2.AddCell(17, 2, "");
			_tableInspection2.AddCell(17, 3, "");
			_tableInspection2.AddCell(18, 1, "Тормозные шланги/трубки");
			_tableInspection2.AddCell(18, 2, "");
			_tableInspection2.AddCell(18, 3, "");

			_tableInspectionImageLegend = new PrintableTable(3, 3);
			_tableInspectionImageLegend.SetColumnWidth(1, INSPECTIONIMAGE_LEGEND_WIDTH);
			_tableInspectionImageLegend.SetColumnWidth(2, INSPECTIONIMAGE_LEGEND_WIDTH);
			_tableInspectionImageLegend.SetColumnWidth(3, INSPECTIONIMAGE_LEGEND_WIDTH);
			_tableInspectionImageLegend.AddCell(1, 1, "В - вмятина)");
			_tableInspectionImageLegend.AddCell(2, 1, "Н - не закреплено");
			_tableInspectionImageLegend.AddCell(3, 1, "П - пятно");
			_tableInspectionImageLegend.AddCell(1, 2, "О - отсутствует)");
			_tableInspectionImageLegend.AddCell(2, 2, "С - сколы");
			_tableInspectionImageLegend.AddCell(3, 2, "Т - трещины");
			_tableInspectionImageLegend.AddCell(1, 3, "Р - разбито");
			_tableInspectionImageLegend.AddCell(2, 3, "Ц - царапины");
			_tableInspectionImageLegend.AddCell(3, 3, "");
			_tableInspectionImageLegend.SetBorder(BORDER.NONE);

			_tableInspectionDescription = new PrintableTable(2, 1);
			_tableInspectionDescription.SetColumnWidth(1, INSPECTIONDESCRIPTION_WIDTH);
			_tableInspectionDescription.RowsHeight = INSPECTIONDESCRIPTION_ROWHEIGHT;
			_tableInspectionDescription.AddCell(1, 1, "Комментарии мастера-консультанта по результатам приемки:");
			_tableInspectionDescription.AddCell(2, 1, "");


		}

		private void InspectionTableTool()
        {
			_printTools.SelectFont(new Font("Arial", 5));
			_printTools.SetStringFormat(StringAlignment.Near, StringAlignment.Near);
		}
		private void SignsTool()
		{
			_printTools.SelectFont(new Font("Arial", 6));
			_printTools.SetStringFormat(StringAlignment.Near, StringAlignment.Near);
		}

		public float BlockClaim(float offset, object print_data = null, int coutCounter = 0)
		{

			_printTools.PrintImage("RD37Act02.png", FUEL_IMAGE_X, offset, FUEL_IMAGE_WIDTH, FUEL_IMAGE_HEIGHT);
			_tableClaim.MesureTable(_printTools);
			offset += _tableClaim.Print(CLAIM_BLOCK_X, offset, _printTools);
			offset += _printTools.PrintSignRight("Ассистент сервисного центра", "", SIGN_X, offset, SIGN_WIDTH, SIGN_HEIGHT);
			return offset;
		}
		public float BlockRadio(float offset, object print_data = null, int coutCounter = 0)
		{
			for (int i = 0; i <= 6; i++)
			{
				_printTools.PrintBox(RADIO_X + RADIO_WIDTH + i *(RADIO_BOX_WIDTH + 2), offset + (RADIO_HEIGHT - RADIO_BOX_HEIGHT) / 2, RADIO_BOX_WIDTH, RADIO_BOX_HEIGHT);
			}
			offset += _printTools.PrintText("Настройки радио/любимое радио", RADIO_X, offset, RADIO_WIDTH, RADIO_HEIGHT);
			return offset;
		}
		public float BlockInspection(float offset, object print_data = null, int coutCounter = 0)
		{
			
			InspectionTableTool();
			_tableInspection.MesureTable(_printTools);
			_tableInspection2.MesureTable(_printTools);
			_tableInspectionImageLegend.MesureTable(_printTools);
			_tableInspectionDescription.MesureTable(_printTools);
			float offsetTable = _tableInspection.Print(INSPECTION_BLOCK_X, offset, _printTools);
			float offsetTable2 = _tableInspection2.Print(INSPECTION2_BLOCK_X, offset, _printTools);
			if(offsetTable > offsetTable2)
				offset += offsetTable;
			else
				offset += offsetTable2;
			offset += 2;
			
			float startOffset = offset;

			// Печать первой колонки - картинка, легенда, отказы и согласия
			offset += _printTools.PrintImage("RD37Act01.png", INSPECTION_IMAGE_X, offset, INSPECTION_IMAGE_WIDTH, INSPECTION_IMAGE_HEIGHT);
			offset += 2;
			offset += _tableInspectionImageLegend.Print(INSPECTION_IMAGE_X, offset, _printTools);
			offset += 2;
			offset += _printTools.PrintText("От осмотра состояния кузова автомобиля отказываюсь, а/м принят в ремонт без проведения предварительного осмотра кузова. Претензий по незначительным повреждениям кузова автомобиля, которые не могли быть выявлены в ходе осмотра, не имею.", INSPECTION_IMAGE_X, offset, INSPECTION_IMAGE_WIDTH, 0);
			offset += _printTools.PrintSignRight("Заказчик", _cardTxt.RepresentTitleShort, INSPECTION_IMAGE_X, offset, INSPECTION_IMAGE_WIDTH, SIGN_HEIGHT);
			offset += 2;
			offset += _printTools.PrintText("Согласен на осмотр методом фотофиксации, а/м принят в ремонт без проведения предварительного визуального осмотра кузова. Претензий по незначительным повреждениям кузова автомобиля, которые не могли быть зафиксированны фотографическим методом, не имею.", INSPECTION_IMAGE_X, offset, INSPECTION_IMAGE_WIDTH, 0);
			offset += _printTools.PrintSignRight("Заказчик", _cardTxt.RepresentTitleShort, INSPECTION_IMAGE_X, offset, INSPECTION_IMAGE_WIDTH, SIGN_HEIGHT);
			offset += 2;

			// Печаь второй колонки с примечаниями
			_tableInspectionDescription.SetReminderRowHeight(2, offset - startOffset);
			_tableInspectionDescription.Print(INSPECTIONDESCRIPTION_X, startOffset, _printTools);

			

			RestoreStandartTool();
			return offset;
		}

		public float BlockSigns(float offset, object print_data = null, int coutCounter = 0)
		{
			SignsTool();
			float startOffset = offset;
			offset += _printTools.PrintText("ПРИЕМ АВТОМОБИЛЯ", BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH);
			offset += _printTools.PrintText("С осмотром согласен. Автомобиль сдал. Экземпляр акта приёма-передачи автомобиля получил.", BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH);
			offset += _printTools.PrintSignRight("Заказчик", _cardTxt.RepresentTitleShort, BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH, SIGN_HEIGHT);
			offset += _printTools.PrintText("Автомобиль в ремонт принял: документы, удостоверяющие личность, право эксплуатации автомобиля и полномочия проверил.", BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH);
			offset += _printTools.PrintSignRight("Мастер-консультант", _cardTxt.ServiceManagerShortName, BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH, SIGN_HEIGHT);
			offset += _printTools.PrintText("Дата и время: " + DateTime.Now.ToString(), BLOCK_SIGNS_X, offset, BLOCK_SIGNS_WIDTH);


			float endOffset = offset;
			offset = startOffset;
			
			offset += _printTools.PrintText("ВЫДАЧА АВТОМОБИЛЯ", BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2);
			offset += _printTools.PrintText("Автомобиль сдал:.", BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2);
			offset += _printTools.PrintSignRight("Мастер-консультант", _cardTxt.ServiceManagerShortName, BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2, SIGN_HEIGHT);
			offset += _printTools.PrintText("Претензий к выполненным работам, к наружному состоянию и комплектности автомобиля, объёму и качеству услуг не имею. Автомобиль принял.", BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2);
			offset += _printTools.PrintSignRight("Заказчик", _cardTxt.RepresentTitleShort, BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2, SIGN_HEIGHT);
			offset += _printTools.PrintText("Дата и время: ________________________________", BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, offset, BLOCK_SIGNS_WIDTH2);

			if (endOffset < offset) endOffset = offset;

			_printTools.PrintBox(BLOCK_SIGNS_X, startOffset, BLOCK_SIGNS_WIDTH, endOffset - startOffset);
			_printTools.PrintBox(BLOCK_SIGNS_X + BLOCK_SIGNS_WIDTH, startOffset, BLOCK_SIGNS_WIDTH2, endOffset - startOffset);

			RestoreStandartTool();
			return endOffset;
		}
		public override DocumentPrintSchema CreatePrintingSchema()
		{
			ArrayList delegates = new ArrayList();

			delegates.Add(new DocumentPrintSchemaElement(BlockClaim));
			delegates.Add(new DocumentPrintSchemaElement(BlockRadio));
			delegates.Add(new DocumentPrintSchemaElement(BlockInspection));
			delegates.Add(new DocumentPrintSchemaElement(BlockSigns));


			DocumentPrintSchema schema = new DocumentPrintSchema(null);
			schema.SchemaElements = delegates;
			schema.PrintPageHeader = PrintStandartHead;
			return schema;
		}

		public override float PrintDocumentTitle(float offset)
		{
			const int DOCTITLE_HEIGHT = 8;
			string txt = "АКТПРИЕМА ПЕРЕДАЧИ АВТОМОБИЛЯ " + _cardTxt.NumberDate + "\n";
			txt += "к договор заказ-нарядам " + _cardTxt.NumberDate;
			_printTools.SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			return _printTools.SelectedPrintTextNoBox(txt, PAGE_LEFT_X, offset, PAGE_RIGHT_X - PAGE_LEFT_X, DOCTITLE_HEIGHT);
		}
	}
}
