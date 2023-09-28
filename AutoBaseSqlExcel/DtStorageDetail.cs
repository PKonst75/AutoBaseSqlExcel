using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Ñêëàäñêàÿ ïîçèöèÿ, ïîëíîå îïèñàíèå.
	/// </summary>
	public class DtStorageDetail
	{
		private bool _liquidFlag; // Ôëàã ìàñåë

		long		code;
		string		name;
		string		detail_code;
		float		quontity;
		float		reserve;
		float		price;
		float		input;
		string		unit;
		string		description;
		long		code_1c;

		float		tmp_balance;
		float		tmp_expence;

		float		new_quontity;
		
		public DtStorageDetail()
		{
			code			= 0;
			name			= "";
			detail_code		= "";
			quontity		= 0.0F;
			reserve			= 0.0F;
			price			= 0.0F;
			input			= 0.0F;
			unit			= "";
			description		= "";
			code_1c			= 0;

			tmp_balance		= 0.0F;
			tmp_expence		= 0.0F;

			new_quontity	= 0.0F;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(long)code;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(string)name;
				case "ÍÎÌÅĞ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(string)detail_code;
				case "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(float)quontity;
				case "ÖÅÍÀ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(float)price;
				case "ÂÕÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(float)input;
				case "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß":
					return (object)(string)unit;
				case "ÎÏÈÑÀÍÈÅ":
					return (object)(string)description;
				case "ÊÎÄ_1Ñ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					return (object)(long)code_1c;
				case "ĞÀÑÕÎÄ":
					return (object)(float)tmp_expence;
				case "ÎÑÒÀÒÎÊ":
					return (object)(float)tmp_balance;
				case "ÊÎËÈ×ÅÑÒÂÎ":
					return (object)(float)new_quontity;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					code = (long)val;
					break;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					name = (string)val;
					name.Trim();
					break;
				case "ÍÎÌÅĞ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					detail_code = (string)val;
					detail_code.Trim();
					break;
				case "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					quontity = (float)val;
					break;
				case "ÖÅÍÀ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					price = (float)val;
					break;
				case "ÂÕÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					input = (float)val;
					break;
				case "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß":
					unit = (string)val;
					unit.Trim();
					break;
				case "ÎÏÈÑÀÍÈÅ":
					description = (string)val;
					description.Trim();
					break;
				case "ÊÎÄ_1Ñ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					code_1c = (long)val;
					break;
				case "ĞÀÑÕÎÄ":
					tmp_expence = (float)val;
					break;
				case "ÎÑÒÀÒÎÊ":
					tmp_balance = (float)val;
					break;
				case "ÊÎËÈ×ÅÑÒÂÎ":
					new_quontity = (float)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItemBalance(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.code;
			item.Text				= this.detail_code;
			item.SubItems.Add(this.name);
			item.SubItems.Add(this.quontity.ToString() + " / " + this.new_quontity.ToString());
			item.SubItems.Add(this.tmp_expence.ToString());
			item.SubItems.Add(this.tmp_balance.ToString());
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.code;
			item.Text				= this.detail_code;
			item.SubItems.Add(this.name);
			item.SubItems.Add(this.quontity.ToString() + " / " + this.new_quontity.ToString());
			item.SubItems.Add(this.unit);
			item.SubItems.Add(Db.CachToTxt(this.price));
			item.SubItems.Add(this.description);
			// Ğàñöâåòêà ïî 1Ñ
			if(this.code_1c > 0)
				item.BackColor	= Color.LightGreen;
		}

        #region Ãåòòåğû è ñåòòåğ
		public string Name
        {
			get { return name; }
		}
		public string Unit
		{
			get { return unit; }
		}
		public string CatalogueNumber
		{
			get { return detail_code; }
		}
		public bool Liquid
        {
            get { return _liquidFlag; }
			set { _liquidFlag = value; }
        }
		#endregion
	}
}
