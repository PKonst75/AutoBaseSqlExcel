using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtInvoicePay.
	/// </summary>
	public class DtInvoicePay
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
		public string	number_pp;
		public float	summ;
		public int		type;
		
		public long		invoice_code;
		public int		invoice_year;

		public string	tmp_partner_name;

		public DtInvoicePay(DtInvoice invoice)
		{
			// Создание нового счета на основе карточки
			code			= 0L;
			year			= 0;
			date			= DateTime.Now;
			code_partner	= invoice.code_partner;
			comment			= "";
			number_pp		= "";
			summ			= invoice.summ;
			type			= 0;
			
			invoice_code	= invoice.code;
			invoice_year	= invoice.year;

			tmp_partner_name	= "";
		}

		public DtInvoicePay()
		{
			// Создание нового счета на основе карточки
			code			= 0L;
			year			= 0;
			date			= DateTime.Now;
			code_partner	= 0;
			comment			= "";
			number_pp		= "";
			summ			= 0;
			type			= 0;
			
			invoice_code	= 0;
			invoice_year	= 0;

			tmp_partner_name	= "";
		}

		public DtInvoicePay(DtInvoicePay pay)
		{
			// Создание нового счета на основе карточки
			code			= pay.code;
			year			= pay.year;
			date			= pay.date;
			code_partner	= pay.code_partner;
			comment			= pay.comment;
			number_pp		= pay.number_pp;
			summ			= pay.summ;
			type			= pay.type;
			
			invoice_code	= pay.invoice_code;
			invoice_year	= pay.invoice_year;

			tmp_partner_name	= pay.tmp_partner_name;
		}


		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();
			Pair pair			= new Pair();
			pair.code			= code;
			pair.year			= year;
			item.Tag			= (Pair)pair;

			item.Text			= number_pp;
			item.SubItems.Add(date.ToShortDateString());
			item.SubItems.Add(summ.ToString());
			item.SubItems.Add(tmp_partner_name);
		}
	}
}
