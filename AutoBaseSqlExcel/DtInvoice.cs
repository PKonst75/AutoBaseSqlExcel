using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtInvoice.
	/// </summary>
	public class DtInvoice
	{
		public struct Pair
		{
			public long code;
			public int year;
		}

		public long		code;
		public int		year;
		public DateTime	date;
		public long		code_partner;
		public string	comment;
		public string	number_buhg;
		public DateTime	date_buhg;
		public float	summ;
		public int		type;
		public DateTime	date_controll_green;
		public DateTime	date_controll_yellow;
		public DateTime	date_controll_red;
		public bool		pay;
		public DateTime	date_pay;
		public string	comment_unpay;
		public int		pay_delay;

		public long		card_number;
		public int		card_year;

		public string	tmp_partner_name;
		
		public DtInvoice(DtCard card)
		{
			// Создание нового счета на основе карточки
			code			= 0L;
			year			= 0;
			date			= DateTime.Now;
			code_partner	= (long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА");
			comment			= "";
			number_buhg		= "";
			date_buhg		= (DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");
			summ			= card.SummWorkPay() + card.SummDetailOilPay() + card.SummDetailPay();
			type			= 0;
			SetControlDate();
			pay_delay		= 0;
			pay				= false;
			comment_unpay	= "";
			date_pay		= DateTime.Now;

			card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			card_year		= (int)card.GetData("ГОД_КАРТОЧКА");

			tmp_partner_name	= "";
		}

		public DtInvoice(DtInvoice invoice)
		{
			// Создание нового счета на основе карточки
			code			= invoice.code;
			year			= invoice.year;
			date			= invoice.date;
			code_partner	= invoice.code_partner;
			comment			= invoice.comment;
			number_buhg		= invoice.number_buhg;
			date_buhg		= invoice.date_buhg;
			date_controll_green		= invoice.date_controll_green;
			date_controll_yellow	= invoice.date_controll_yellow;
			date_controll_red		= invoice.date_controll_red;
			summ			= invoice.summ;
			type			= invoice.type;
			pay_delay		= invoice.pay_delay;
			pay				= invoice.pay;
			comment_unpay	= invoice.comment_unpay;
			date_pay		= invoice.date_pay;

			card_number		= invoice.card_number;
			card_year		= invoice.card_year;

			tmp_partner_name	= invoice.tmp_partner_name;
		}
		
		public DtInvoice()
		{
			// Создание нового счета на основе карточки
			code			= 0L;
			year			= 0;
			date			= DateTime.Now;
			code_partner	= 0;
			comment			= "";
			number_buhg		= "";
			date_buhg		= DateTime.Now;
			summ			= 0;
			type			= 0;
			date_controll_green		= DateTime.Now;
			date_controll_yellow	= DateTime.Now;
			date_controll_red		= DateTime.Now;
			pay_delay		= 0;
			pay				= false;
			comment_unpay	= "";
			date_pay		= DateTime.Now;

			card_number		= 0;
			card_year		= 0;

			tmp_partner_name	= "";
		}

		public void SetControlDate()
		{
			// Устанавливаем даты контроля
			// Зеленая +3 рабочих дня
			// Зеленая +5 рабочих дней
			// Зеленая +7 рабочих дней
			if(date_buhg.DayOfWeek == DayOfWeek.Saturday)
			{
				date_controll_green = date_buhg.AddDays(4);
				date_controll_yellow = date_buhg.AddDays(6);
				date_controll_red = date_buhg.AddDays(9);
				return;
			}
			if(date_buhg.DayOfWeek == DayOfWeek.Sunday)
			{
				date_controll_green = date_buhg.AddDays(3);
				date_controll_yellow = date_buhg.AddDays(5);
				date_controll_red = date_buhg.AddDays(8);
				return;
			}
			// Отработали выходные, теперь анализ рабочих дней
			if(date_buhg.DayOfWeek == DayOfWeek.Monday)
			{
				date_controll_green = date_buhg.AddDays(3);
				date_controll_yellow = date_buhg.AddDays(7);
				date_controll_red = date_buhg.AddDays(9);
				return;
			}
			if(date_buhg.DayOfWeek == DayOfWeek.Tuesday)
			{
				date_controll_green = date_buhg.AddDays(3);
				date_controll_yellow = date_buhg.AddDays(7);
				date_controll_red = date_buhg.AddDays(9);
				return;
			}
			if(date_buhg.DayOfWeek == DayOfWeek.Thursday)
			{
				date_controll_green = date_buhg.AddDays(5);
				date_controll_yellow = date_buhg.AddDays(7);
				date_controll_red = date_buhg.AddDays(9);
				return;
			}
			if(date_buhg.DayOfWeek == DayOfWeek.Wednesday)
			{
				date_controll_green = date_buhg.AddDays(5);
				date_controll_yellow = date_buhg.AddDays(7);
				date_controll_red = date_buhg.AddDays(11);
				return;
			}
			if(date_buhg.DayOfWeek == DayOfWeek.Friday)
			{
				date_controll_green = date_buhg.AddDays(5);
				date_controll_yellow = date_buhg.AddDays(7);
				date_controll_red = date_buhg.AddDays(11);
				return;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();
			Pair pair			= new Pair();
			pair.code			= code;
			pair.year			= year;
			item.Tag			= (Pair)pair;

			item.Text			= number_buhg;
			item.SubItems.Add(date_buhg.ToShortDateString());
			item.SubItems.Add(tmp_partner_name);
			item.SubItems.Add(summ.ToString());
			item.SubItems.Add(date_controll_green.ToShortDateString());
			item.SubItems.Add(comment);

			DateTime today = DateTime.Now;
			if (today.Date > date_controll_yellow)
				item.BackColor = Color.Yellow;
			if (today.Date > date_controll_red)
				item.BackColor = Color.Red;
		}
	}
}
