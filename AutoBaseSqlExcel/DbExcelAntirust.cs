using System;
using System.Collections;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelAntirust.
	/// </summary>
	public class DbExcelAntirust : DbExcel
	{
		public ArrayList cards_storage_start;   // ������� �� ������ �������
		public ArrayList cards_interval;         // ���������� � ���������

		protected struct AntirustData{
			public AntirustData(ArrayList works)
			{
				antirust_summ = 0.0F;
				antirustex_summ = 0.0F;
				protect_summ = 0.0F;
				underwing_summ = 0.0F;
				antirustunderwing_summ = 0.0F;
				other_summ = 0.0F;
				executor = "";

				foreach (object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					// 24.08.2023 - �����������
					ArrayList l;
					if (((l = work.Executors) != null) && (l.Count > 0))
					{
						executor = ((DtStaff)l[0]).Title;
					}

					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag = false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 747)
					{
						antirustunderwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						if ((long)wrk.GetData("������_���_����������_������������") == 722)
						{
							// �����
						}
						else
						{
							other_summ += work.WorkSumm;
						}
					}
				}
			}
			public float antirust_summ;
			public float antirustex_summ;
			public float protect_summ;
			public float underwing_summ;
			public float antirustunderwing_summ;
			public float other_summ;
			public string executor;
		}

		

		public DbExcelAntirust(DateTime date, long workshop)
		{
			DateTime date_start		= new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
			cards_storage_start		= new ArrayList();
			cards_interval			= new ArrayList();
			DateTime date_end		= date_start.AddMonths(1);
			date_end				= date_end.AddDays(-1.0);
			// ��������
			FormSelectDateInterval form = new FormSelectDateInterval();
			if(form.ShowDialog() == DialogResult.OK)
			{
				date_start = form.StartDate;
				date_end = form.EndDate;
			}
			DbSqlCard.SelectCardNotClosedDateNumberWorkshop(cards_storage_start, workshop, date_start);
			DbSqlCard.SelectCardOpenIntervalNumberWorkshop(cards_interval, workshop, date_start, date_end);
			
		}

		protected void TitleFormat1(Excel.Worksheet ws)
		{
			// ���� ���������� �����
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "����", "Center", 10, true);
			// ������
			FormatColumn(ws, "B1", 22, 8, "Left");
			CellText(ws, "B1", "������", "Center", 10, true);
			// �����
			FormatColumn(ws, "C1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "C1", "@");
			CellText(ws, "C1", "�����", "Center", 10, true);
			// ����� ��������������� ���������
			FormatColumn(ws, "D1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "D1", "# ##0,00");
			CellText(ws, "D1", "�������", "Center", 10, true);
			// ����� ��������������� ���������
			FormatColumn(ws, "E1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "E1", "# ##0,00");
			CellText(ws, "E1", "�������", "Center", 10, true);
			// ����� ���������� ��������������� ���������
			FormatColumn(ws, "F1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "D1", "# ##0,00");
			CellText(ws, "F1", "���������-�������", "Center", 10, true);
			// ����� ������ �������
			FormatColumn(ws, "G1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "F1", "# ##0,00");
			CellText(ws, "G1", "������", "Center", 10, true);
			// ���������
			FormatColumn(ws, "H1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "G1", "# ##0,00");
			CellText(ws, "H1", "���������", "Center", 10, true);
			// ������
			FormatColumn(ws, "I1", 12, 8, "Left");
			FormatColumnNumberFormat(ws, "H1", "# ##0,00");
			CellText(ws, "I1", "������", "Center", 10, true);
			// ���� �������
			FormatColumn(ws, "J1", 12, 8, "Left");
			CellText(ws, "J1", "�������", "Center", 10, true);
			// ��� �������
			FormatColumn(ws, "K1", 12, 8, "Left");
			CellText(ws, "K1", "���", "Center", 10, true);
			// ��� �������
			FormatColumn(ws, "O1", 12, 8, "Left");
			CellText(ws, "O1", "��������", "Center", 10, true);
			// 24.08.2023 - �����������
			FormatColumn(ws, "P1", 12, 8, "Left");
			CellText(ws, "P1", "�����������", "Center", 10, true);
		}

		protected void DataToExcel1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = 2;//start;
			int row_summ = 2;
			string txt;
			DateTime dt_last = DateTime.Now;

			foreach(object o in cards_storage_start)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				// ������ � �������� ����-������
				short status		= (short)card_data.GetData("������_��������");
				DateTime date_sell	= (DateTime)card_data.GetData("����_�����_������_��������");
				date_sell			= new DateTime(date_sell.Year, date_sell.Month, date_sell.Day, 0, 0, 0, 0);
				// ������ � ���� ������
				bool inner			= (bool)card_data.GetData("����������_��������");
				bool cashless		= (bool)card_data.GetData("�����������_��������");
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				//	float summ	= 0.0F;
				//	foreach(object o1 in works)
				//	{
				//		DtCardWork work = (DtCardWork)o1;
				//		summ	+= work.WorkSummCash;
				//	}

				// ���������� �� ����������� ���� �����
				/*
				float antirust_summ		= 0.0F;
				float antirustex_summ	= 0.0F;
				float protect_summ		= 0.0F;
				float underwing_summ	= 0.0F;
				float antirustunderwing_summ = 0.0F;
				float other_summ		= 0.0F;
				string executor = "";
				foreach(object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					// 24.08.2023 - �����������
					ArrayList l;
					if ( ((l = work.Executors) != null) && (l.Count > 0)  )
					{
						executor = ((DtStaff)l[0]).Title;
					}

					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag	= false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 747)
					{
						antirustunderwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						if((long)wrk.GetData("������_���_����������_������������") == 722)
						{
							// �����
						}
						else
						{
							other_summ	+= work.WorkSumm;
						}
					}
				}
				*/
				AntirustData data = new AntirustData(works);
				float antirust_summ = data.antirust_summ;
				float antirustex_summ = data.antirustex_summ;
				float protect_summ = data.protect_summ;
				float underwing_summ = data.underwing_summ;
				float antirustunderwing_summ = data.antirustunderwing_summ;
				float other_summ = data.other_summ;
				string executor = data.executor;

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();

				// ���� ���������� �����
				DateTime dt = (DateTime)card_data.GetData("����");
				dt			= new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
				cell_txt = "A" + row_txt;
				if(dt != dt_last)
				{
					txt			= dt.ToShortDateString();
					dt_last	= dt;
				}
				else
				{
					txt = "";
				}
				CellText(ws, cell_txt, txt);

				// ������
				cell_txt = "B" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("������").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);

				// ������ ���������� 
				cell_txt = "C" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("�����_�����").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);

				// ����� ��������������� ���������
				cell_txt = "D" + row_txt;
				if (antirust_summ != 0)
					txt = antirust_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ������� ��������
				cell_txt = "E" + row_txt;
				if (antirustex_summ != 0)
					txt = antirustex_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ���������� ��� ��������
				cell_txt = "F" + row_txt;
				if (antirustunderwing_summ != 0)
					txt = antirustunderwing_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ������ �������
				cell_txt = "G" + row_txt;
				if (protect_summ != 0)
					txt = protect_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ���������
				cell_txt = "H" + row_txt;
				if (underwing_summ != 0)
					txt = underwing_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ������
				cell_txt = "I" + row_txt;
				if (other_summ != 0)
					txt = other_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// �������� �����-������
				cell_txt = "J" + row_txt;
				if (status == 2)
					txt = date_sell.ToShortDateString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� ������
				cell_txt = "K" + row_txt;
				if (status == 2)
				{
					txt = "";
					if (cashless == true) txt = "��";
					if (inner == true) txt = "���";
				}
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ������������ ������ �������
				if (status == 2)
				{
					CellsColor(ws, "A" + row_txt, "K" + row_txt, 4);
				}
				// ��������� �����
				CellsBorderOuter(ws, "A" + row_txt, "K" + row_txt, 4);

				// ���������� - ����� �����-������
				cell_txt = "O" + row_txt;
				txt		= card.GetData("�����_��������").ToString();
				CellText(ws, cell_txt, txt);

				// 23.08.2023 - �����������
				cell_txt = "P" + row_txt;
				txt = executor;
				CellText(ws, cell_txt, txt);

				row++;
			}

			row += 4;
			foreach(object o in cards_interval)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				// ������ � �������� ����-������
				short status		= (short)card_data.GetData("������_��������");
				DateTime date_sell	= (DateTime)card_data.GetData("����_�����_������_��������");
				date_sell			= new DateTime(date_sell.Year, date_sell.Month, date_sell.Day, 0, 0, 0, 0);
				// ������ � ���� ������
				bool inner			= (bool)card_data.GetData("����������_��������");
				bool cashless		= (bool)card_data.GetData("�����������_��������");
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				//	float summ	= 0.0F;
				//	foreach(object o1 in works)
				//	{
				//		DtCardWork work = (DtCardWork)o1;
				//		summ	+= work.WorkSummCash;
				//	}

				// ���������� �� ����������� ���� �����
				float antirust_summ		= 0.0F;
				float antirustex_summ	= 0.0F;
				float protect_summ		= 0.0F;
				float underwing_summ	= 0.0F;
				float antirustunderwing_summ	= 0.0F;
				float other_summ		= 0.0F;
				string executor = "";
				foreach(object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					// 24.08.2023 - �����������
					ArrayList l;
					if (((l = work.Executors) != null) && (l.Count > 0))
					{
						executor = ((DtStaff)l[0]).Title;
					}

					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag	= false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 747)
					{
						antirustunderwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						if((long)wrk.GetData("������_���_����������_������������") == 722)
						{
							// �����
						}
						else
						{
							other_summ	+= work.WorkSumm;
						}
					}
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();
				// ���� ���������� �����
				DateTime dt = (DateTime)card_data.GetData("����");
				dt			= new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
				cell_txt = "A" + row_txt;
				if(dt != dt_last)
				{
					txt		= dt.ToShortDateString();
					dt_last	= dt;
				}
				else
				{
					txt = "";
				}
				CellText(ws, cell_txt, txt);
				// ������
				cell_txt = "B" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("������").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "C" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("�����_�����").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);
				// ����� ��������������� ���������
				cell_txt = "D" + row_txt;
				if (antirust_summ != 0)
					txt = antirust_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ������� ��������
				cell_txt = "E" + row_txt;
				if (antirustex_summ != 0)
					txt = antirustex_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ���������� ��� ��������
				cell_txt = "F" + row_txt;
				if (antirustunderwing_summ != 0)
					txt = antirustunderwing_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);

				// ����� ������ �������
				cell_txt = "G" + row_txt;
				if (protect_summ != 0)
					txt = protect_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ���������
				cell_txt = "H" + row_txt;
				if (underwing_summ != 0)
					txt = underwing_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ������
				cell_txt = "I" + row_txt;
				if (other_summ != 0)
					txt = other_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// �������� �����-������
				cell_txt = "J" + row_txt;
				if (status == 2)
					txt = date_sell.ToShortDateString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� ������
				cell_txt = "K" + row_txt;
				if (status == 2)
				{
					txt = "";
					if (cashless == true) txt = "��";
					if (inner == true) txt = "���";
				}
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ������������ ������ �������
				if (status == 2)
					CellsColor(ws, "A" + row_txt, "K" + row_txt, 4);
				// ��������� �����
				CellsBorderOuter(ws, "A" + row_txt, "K" + row_txt, 4);

				// ���������� - ����� �����-������
				cell_txt = "O" + row_txt;
				txt		= card.GetData("�����_��������").ToString();
				CellText(ws, cell_txt, txt);

				// 23.08.2023 - �����������
				cell_txt = "P" + row_txt;
				txt = executor;
				CellText(ws, cell_txt, txt);

				row++;
			}
		}

		protected void DataToExcel1a(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = 2;//start;
			int row_summ = 2;
			string txt;
			DateTime dt_last = DateTime.Now;

			foreach(object o in cards_storage_start)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				// ������ � �������� ����-������
				short status		= (short)card_data.GetData("������_��������");
				DateTime date_sell	= (DateTime)card_data.GetData("����_�����_������_��������");
				date_sell			= new DateTime(date_sell.Year, date_sell.Month, date_sell.Day, 0, 0, 0, 0);
				// ������ � ���� ������
				bool inner			= (bool)card_data.GetData("����������_��������");
				bool cashless		= (bool)card_data.GetData("�����������_��������");
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				//	float summ	= 0.0F;
				//	foreach(object o1 in works)
				//	{
				//		DtCardWork work = (DtCardWork)o1;
				//		summ	+= work.WorkSummCash;
				//	}

				// ���������� �� ����������� ���� �����
				float antirust_summ		= 0.0F;
				float antirustex_summ	= 0.0F;
				float protect_summ		= 0.0F;
				float underwing_summ	= 0.0F;
				float other_summ		= 0.0F;
				foreach(object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag	= false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						other_summ	+= work.WorkSumm;
					}
				}

				// ����������� ������� ������
				row_txt = row.ToString();
				cell_txt	= "O" + row_txt;
				string txt_value	= GET_CellText(ws, cell_txt);
				bool check	= false;
				if(txt_value	== card.GetData("�����_��������").ToString())
				{
					// ����������
					CellsColor(ws, cell_txt, cell_txt, 4);
					check	= true;
					// ���������� ����
					cell_txt	= "A" + row_txt;
					txt_value	= GET_CellTextDateShort(ws, cell_txt);
					int i = row - 1;
					while(txt_value == "" && i > 1)
					{
						cell_txt	= "A" + i;
						txt_value	= GET_CellTextDateShort(ws, cell_txt);
						i--;
					}
				}
				else
				{
					// �������� - ���� ����� ��� ��������� ������
					if(txt_value != "")
					{
						// ����� �� �� �������, ����� ����� ������
						int i = row + 1;
						row_txt = row.ToString();
						cell_txt	= "O" + row_txt;
						txt_value	= GET_CellText(ws, cell_txt);
						while(txt_value != "" && txt_value	!= card.GetData("�����_��������").ToString())
						{
							i++;
							row_txt = i.ToString();
							cell_txt	= "O" + row_txt;
							txt_value	= GET_CellText(ws, cell_txt);
						}
						if(txt_value == "")
						{
							// ������ ��������� ����� �������
						}
						else
						{
							// �������� � ���������
						}
					}
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();

				// ���� ���������� �����
				DateTime dt = (DateTime)card_data.GetData("����");
				dt			= new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
				cell_txt = "A" + row_txt;
				if(dt != dt_last)
				{
					txt			= dt.ToShortDateString();
					dt_last	= dt;
				}
				else
				{
					txt = "";
				}
				if(check == true)
				{
					if(txt_value != dt.ToShortDateString()) CellsColor(ws, cell_txt, cell_txt, 46);
				}
				CellText(ws, cell_txt, txt);

				// ������
				cell_txt = "B" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("������").ToString();
				else
					txt = "--";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ������ ���������� 
				cell_txt = "C" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("�����_�����").ToString();
				else
					txt = "--";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ����� ��������������� ���������
				cell_txt = "D" + row_txt;
				if (antirust_summ != 0)
					txt = antirust_summ.ToString();
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ����� ������� ��������
				cell_txt = "E" + row_txt;
				if (antirustex_summ != 0)
					txt = antirustex_summ.ToString();
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ����� ������ �������
				cell_txt = "G" + row_txt;
				if (protect_summ != 0)
					txt = protect_summ.ToString();
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ����� ���������
				cell_txt = "H" + row_txt;
				if (underwing_summ != 0)
					txt = underwing_summ.ToString();
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// ����� ������
				cell_txt = "I" + row_txt;
				if (other_summ != 0)
					txt = other_summ.ToString();
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);

				// �������� �����-������
				cell_txt = "J" + row_txt;
				if (status == 2)
					txt = date_sell.ToShortDateString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� ������
				cell_txt = "K" + row_txt;
				if (status == 2)
				{
					txt = "";
					if (cashless == true) txt = "��";
					if (inner == true) txt = "���";
				}
				else
					txt = "";
				CheckValue(ws, txt, cell_txt, check);
				CellText(ws, cell_txt, txt);
				// ������������ ������ �������
				if (status == 2)
				{
					if(check == false) CellsColor(ws, "A" + row_txt, "J" + row_txt, 4);
				}
				// ��������� �����
				CellsBorderOuter(ws, "A" + row_txt, "J" + row_txt, 4);

				// ���������� - ����� �����-������
				cell_txt = "O" + row_txt;
				txt		= card.GetData("�����_��������").ToString();
				CellText(ws, cell_txt, txt);

				row++;
			}

			row += 4;
			foreach(object o in cards_interval)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				// ������ � �������� ����-������
				short status		= (short)card_data.GetData("������_��������");
				DateTime date_sell	= (DateTime)card_data.GetData("����_�����_������_��������");
				date_sell			= new DateTime(date_sell.Year, date_sell.Month, date_sell.Day, 0, 0, 0, 0);
				// ������ � ���� ������
				bool inner			= (bool)card_data.GetData("����������_��������");
				bool cashless		= (bool)card_data.GetData("�����������_��������");
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				//	float summ	= 0.0F;
				//	foreach(object o1 in works)
				//	{
				//		DtCardWork work = (DtCardWork)o1;
				//		summ	+= work.WorkSummCash;
				//	}

				// ���������� �� ����������� ���� �����
				float antirust_summ		= 0.0F;
				float antirustex_summ	= 0.0F;
				float protect_summ		= 0.0F;
				float underwing_summ	= 0.0F;
				float other_summ		= 0.0F;
				foreach(object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag	= false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						if((long)wrk.GetData("������_���_����������_������������") == 722)
						{
							// �����
						}
						else
						{
							other_summ	+= work.WorkSumm;
						}
					}
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();
				// ���� ���������� �����
				DateTime dt = (DateTime)card_data.GetData("����");
				dt			= new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
				cell_txt = "A" + row_txt;
				if(dt != dt_last)
				{
					txt		= dt.ToShortDateString();
					dt_last	= dt;
				}
				else
				{
					txt = "";
				}
				CellText(ws, cell_txt, txt);
				// ������
				cell_txt = "B" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("������").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "C" + row_txt;
				if(auto_data != null)
					txt = auto_data.GetData("�����_�����").ToString();
				else
					txt = "--";
				CellText(ws, cell_txt, txt);
				// ����� ��������������� ���������
				cell_txt = "D" + row_txt;
				if (antirust_summ != 0)
					txt = antirust_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ������� ��������
				cell_txt = "E" + row_txt;
				if (antirustex_summ != 0)
					txt = antirustex_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ������ �������
				cell_txt = "G" + row_txt;
				if (protect_summ != 0)
					txt = protect_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ���������
				cell_txt = "H" + row_txt;
				if (underwing_summ != 0)
					txt = underwing_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ����� ������
				cell_txt = "I" + row_txt;
				if (other_summ != 0)
					txt = other_summ.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// �������� �����-������
				cell_txt = "J" + row_txt;
				if (status == 2)
					txt = date_sell.ToShortDateString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� ������
				cell_txt = "K" + row_txt;
				if (status == 2)
				{
					txt = "";
					if (cashless == true) txt = "��";
					if (inner == true) txt = "���";
				}
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ������������ ������ �������
				if (status == 2)
					CellsColor(ws, "A" + row_txt, "J" + row_txt, 4);
				// ��������� �����
				CellsBorderOuter(ws, "A" + row_txt, "J" + row_txt, 4);

				// ���������� - ����� �����-������
				cell_txt = "O" + row_txt;
				txt		= card.GetData("�����_��������").ToString();
				CellText(ws, cell_txt, txt);

				row++;
			}
		}

		protected void DataToExcelCheck(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = 2;
			int row_summ = 2;
			string txt;
			DateTime dt_last = DateTime.Now;

			foreach(object o in cards_storage_start)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				// ������ � �������� ����-������
				short status		= (short)card_data.GetData("������_��������");
				DateTime date_sell	= (DateTime)card_data.GetData("����_�����_������_��������");
				date_sell			= new DateTime(date_sell.Year, date_sell.Month, date_sell.Day, 0, 0, 0, 0);
				// ������ � ���� ������
				bool inner			= (bool)card_data.GetData("����������_��������");
				bool cashless		= (bool)card_data.GetData("�����������_��������");
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ���������� �� ����������� ���� �����
				float antirust_summ		= 0.0F;
				float antirustex_summ	= 0.0F;
				float protect_summ		= 0.0F;
				float underwing_summ	= 0.0F;
				float other_summ		= 0.0F;
				foreach(object o1 in works)
				{
					DtCardWork work = (DtCardWork)o1;
					DtWork wrk = DbSqlWork.Find(work.CodeWork);
					bool flag	= false;
					if ((long)wrk.GetData("������_���_����������_������������") == 743)
					{
						antirust_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 744)
					{
						antirustex_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 745)
					{
						protect_summ = work.WorkSumm;
						flag = true;
					}
					if ((long)wrk.GetData("������_���_����������_������������") == 746)
					{
						underwing_summ = work.WorkSumm;
						flag = true;
					}
					if (flag == false)
					{
						if((long)wrk.GetData("������_���_����������_������������") == 722)
						{
							// �����
						}
						else
						{
							other_summ	+= work.WorkSumm;
						}
					}
				}

				// ����������� ������� ������ - �� ������������ �������
				row_txt				= row.ToString();
				cell_txt			= "N" + row_txt;
				string txt_value	= GET_CellText(ws, cell_txt);
				bool check			= false;
				if(txt_value	== card.GetData("�����_��������").ToString())
				{
					// ������ �������
					// ������������ ������������� ����������
					CellsColor(ws, cell_txt, cell_txt, 4);
					// ���������� ���� �������� ������
					check	= true;
				}
				else
				{
					// ������ �� �������
					// ������������ ����� ����������
					int i = row + 1;
					row_txt = row.ToString();
					cell_txt	= "O" + row_txt;
					txt_value	= GET_CellText(ws, cell_txt);
					while(txt_value != "" && txt_value	!= card.GetData("�����_��������").ToString())
					{
						i++;
						row_txt = i.ToString();
						cell_txt	= "O" + row_txt;
						txt_value	= GET_CellText(ws, cell_txt);
					}
					// ���� � ���������� ������� �� �����
					if(txt_value == "")
					{
						// �������� ������ ������� � ��������� � ������ �����
						string cell1 = "A" + i.ToString();
						string cell2 = "N" + i.ToString();
						string cell3 = "A" + row.ToString();
						string cell4 = "A" + row.ToString();
						RowInsert(ws, cell1, cell2, cell3, cell4);
						row_txt				= row.ToString();
						cell_txt	= "O" + row_txt;
						CellText(ws, cell_txt, card.GetData("�����_��������").ToString());
						CellsColor(ws, cell_txt, cell_txt, 46);
						check = true;
						return;
					}
				}

					
				string check_value	= "";
				row_txt				= row.ToString();
				if(check == true)
				{
					// ��������� ���� �������� ������

					// ��������� ���� �������
					// �������� �����-������
					cell_txt = "I" + row_txt;
					txt_value	= GET_CellTextDateShort(ws, cell_txt);
					if(txt_value == "" && status==2)
					{
						// ������� ����� �� ���������� ��� ���������
						// ���������� ���� ��������
						txt = date_sell.ToShortDateString();
						CellText(ws, cell_txt, txt);
						// ������������ ������� �������
						CellsColor(ws, "A" + row_txt, "J" + row_txt, 4);
					}
					if(txt_value != "")
					{
						// ��������� ������������ ���� �������
						if(status == 2)
							txt = date_sell.ToShortDateString();
						else
							txt = "";
						if(txt_value != txt)
						{
							CellText(ws, cell_txt, txt);
							CellsColor(ws, cell_txt, cell_txt, 46);
						}
					}

					// ��������� ���� ����������
					// ���������� ���� � �����
					cell_txt	= "A" + row_txt;
					txt_value	= GET_CellTextDateShort(ws, cell_txt);
					int i = row - 1;
					while(txt_value == "" && i > 1)
					{
						cell_txt	= "A" + i;
						txt_value	= GET_CellTextDateShort(ws, cell_txt);
						i--;
					}
					// ���������� ���� � �����-������
					DateTime dt = (DateTime)card_data.GetData("����");
					dt			= new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
					txt			= dt.ToShortDateString();
					if(txt_value != txt)
					{
						// �������� �� ���� �� ������
						CellsColor(ws, cell_txt, cell_txt, 46);
						CellText(ws, cell_txt, txt);
					}

					// ��������� ��������� ������
					// ������
					cell_txt = "B" + row_txt;
					if(auto_data != null)
						txt = auto_data.GetData("������").ToString();
					else
						txt = "--";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� 
					cell_txt = "C" + row_txt;
					if(auto_data != null)
						txt = auto_data.GetData("�����_�����").ToString();
					else
						txt = "--";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� ��������������� ���������
					cell_txt = "D" + row_txt;
					if (antirust_summ != 0)
						txt = antirust_summ.ToString();
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� ������� ��������
					cell_txt = "E" + row_txt;
					if (antirustex_summ != 0)
						txt = antirustex_summ.ToString();
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� ������ �������
					cell_txt = "G" + row_txt;
					if (protect_summ != 0)
						txt = protect_summ.ToString();
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� ���������
					cell_txt = "H" + row_txt;
					if (underwing_summ != 0)
						txt = underwing_summ.ToString();
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ����� ������
					cell_txt = "I" + row_txt;
					if (other_summ != 0)
						txt = other_summ.ToString();
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
					// ��� ������
					cell_txt = "K" + row_txt;
					if (status == 2)
					{
						txt = "";
						if (cashless == true) txt = "��";
						if (inner == true) txt = "���";
					}
					else
						txt = "";
					if(!CheckValue(ws, txt, cell_txt, check))
						CellText(ws, cell_txt, txt);
				}
				row++;
			}
		}

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			TitleFormat1(ws);
			return;

			if(sheet == 1)
			{
				TitleFormat1(ws);
				return;
			}
			if(sheet == 2)
			{
				TitleFormat1(ws);
				return;
			}
		}
		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcel1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				if(file_continue == false)
					DataToExcel1(ws, start);
				else
					DataToExcelCheck(ws, start);
				return;
			}
		}
		private bool CheckValue(Excel.Worksheet ws, string txt_value_new, string txt_cell, bool check)
		{
			if(check == false) return true;
			string txt_cell_value = GET_CellText(ws, txt_cell);
			if (txt_cell_value == txt_value_new) return true;
			// ������������ ������
			CellsColor(ws, txt_cell, txt_cell, 46);
			return false;
		}
	}
}
