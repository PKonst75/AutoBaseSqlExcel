using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Печать расчетки на зарплату.
	/// </summary>
	public class DbPrintSalary:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;

		DtStaff		staff;
		// Идинтификационные данные
		string		title_staff;
		string		title_period;
		// Расчетные данные
		float		cash;
		float		cash_hour;
		float		cash_hour_count;
		float		hour;
		int			ppp_count;
		int			guarantee_ppp_count;
		float		guarantee_hour;
		float		guarantee_cash;


		// Для зарплаты
		float		salary_cash;
		float		salary_cash_hour;
		float		salary_hour;
		float		salary_guarantee;
		float		salary_ppp;
		float		salary;


		public DbPrintSalary(long code, int year, int month)
		{
			// Инструменты для печати
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);

			// Загружаем данные персонала
			staff = DbSqlStaff.Find(code);
			if(staff == null) return;

			// Идинтификационные данные
			title_staff		= staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " + staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
			title_period	= Month(month) + " " + year.ToString();

			// Получаем необходимые для расчета з/п данные
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			cash							= 0.0F;		// Выполненые договрные работы (с учетом скидки и приведенные на количество исполнителей)
			cash_hour						= 0.0F;		// Выполненые работы по нормачасу
			cash_hour_count					= 0.0F;		// Количество выполненных работ по нормачасу
			hour							= 0.0F;		// Количество нерасцененных нормачасов
			ppp_count						= 0;		// Количество выполненных предпродажных подготовок
			guarantee_ppp_count				= 0;		// Количество выполненных предпродажек, отмеченных как гарантия
			guarantee_cash					= 0.0F;		// Количество гарантийных договорных работ (приведенные на количество исполнителей)
			guarantee_hour					= 0.0F;		// Количество отработанных гарантийных нормачасов (приведенные на количество исполнителей)
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
				{
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_hour			+= summ_person;
							if((float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") == 0.0F)
							{
								// Нерасцененные нормачасы, с учетом скидки
								summ			= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
								discount		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
								hour			+= (summ - discount) /(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
							else
							{
								cash_hour_count += (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
						}
					}
				}
				else
				{
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						guarantee_ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	summ_person	= summ / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							guarantee_cash		+= summ_person;
						}
						else
						{
							guarantee_hour		+= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
						}
					}
				}
			}

			// Расчет заработной платы
			salary_cash			= 0.0F;
			salary_cash_hour	= 0.0F;
			salary_hour			= 0.0F;
			salary_guarantee	= 0.0F;
			salary_ppp			= 0.0F;
			salary				= 0.0F;

			float coef_cash			= 0.16F;
			float coef_cash_hour	= 0.3F;
			float coef_hour			= 80.0F;
			float coef_guaranty		= 160.0F;
			float coef_ppp			= 150.0F;

			salary_cash			= cash * coef_cash;
			salary_cash_hour	= cash_hour * coef_cash_hour;
			salary_hour			= hour * coef_hour;
			salary_guarantee	= guarantee_hour * coef_guaranty;
			salary_ppp			= (ppp_count + guarantee_ppp_count) * coef_ppp;
			salary				= salary_cash + salary_cash_hour + salary_hour + salary_guarantee + salary_ppp;
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			PrintText(graph, "РАСЧЕТ за " + title_period, 10, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			PrintText(graph, title_staff, 70, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			PrintText(graph, "Договорные работы", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "За нормачасы", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Не расцененные нормачасы", 10, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Гарантия", 20, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Договорные работы", 10, 30 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Нормачасы", 10, 35 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Предпродажная подготовка", 20, 40 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Количество", 10, 45 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, cash.ToString(), 60, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, cash_hour.ToString() + " ( " + cash_hour_count.ToString() + " ) ", 60, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, hour.ToString(), 60, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, guarantee_cash.ToString(), 60, 30 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, guarantee_hour.ToString(), 60, 35 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (ppp_count + guarantee_ppp_count).ToString(), 60, 45 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, salary_cash.ToString(), 120, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_cash_hour.ToString(), 120, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_hour.ToString(), 120, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_guarantee.ToString(), 120, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_ppp.ToString(), 120, 40 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary.ToString(),120, 50 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);

		}

		private string Month(int month)
		{
			switch(month)
			{
				case 1:
					return "январь";
				case 2:
					return "февраль";
				case 3:
					return "март";
				case 4:
					return "апрель";
				case 5:
					return "май";
				case 6:
					return "июнь";
				case 7:
					return "июль";
				case 8:
					return "август";
				case 9:
					return "сентябрь";
				case 10:
					return "октябрь";
				case 11:
					return "ноябрь";
				case 12:
					return "декабрь";
				default:
					return "ошибка";
			}
		}
	}
}
