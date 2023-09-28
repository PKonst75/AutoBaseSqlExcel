using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCallToClient.
	/// </summary>
	public class DtCallToClient
	{
		public long		code_sell;
		public DateTime date;
		public int		place;

		public string	tmp_customer_name;
		public string	tmp_auto_model;

		public DtCallToClient()
		{
			code_sell	= 0;
			place		= 0;

			tmp_customer_name	= "";
			tmp_auto_model		= "";
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// „тобы сделать однотипным добавление и изменение

			item.Tag				= code_sell;
			item.Text				= this.date.ToShortDateString();
			item.SubItems.Add(this.tmp_customer_name);
			item.SubItems.Add(this.tmp_auto_model);
			//item.SubItems.Add(this.tmp_auto_color);
			//item.SubItems.Add(this.tmp_auto_variant);
			//item.SubItems.Add(this.tmp_auto_vin);
		}
	}
}
