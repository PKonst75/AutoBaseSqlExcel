using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// оПНДЮФЮ ЮБРНЛНАХКЪ
	/// </summary>
	public class DtAutoSell
	{
		long				code;
		long				code_auto;
		long				code_customer;
		DateTime			date;
		string				comment;

		DtPartner			tmp_partner;
		DtAuto				tmp_auto;
		string				tmp_customer_name;
		string				tmp_auto_model;
		string				tmp_auto_color;
		string				tmp_auto_variant;
		string				tmp_auto_vin;

		public DtAutoSell()
		{
			code			= 0;
			code_auto		= 0;
			code_customer	= 0;
			date			= DateTime.Now;
			comment			= "";

			tmp_partner		= null;
			tmp_auto		= null;

			tmp_customer_name	= "";
			tmp_auto_model		= "";
			tmp_auto_color		= "";
			tmp_auto_variant	= "";
			tmp_auto_vin		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ_опндюфю":
					return (object)(long)code;
				case "яяшкйю_йнд_юбрнлнахкэ":
					return (object)(long)code_auto;
				case "яяшкйю_йнд_онйсоюрекэ":
					return (object)(long)code_customer;
				case "дюрю_юбрнлнахкэ_опндюфю":
					return (object)(DateTime)date;
				case "опхлевюмхе_юбрнлнахкэ_опндюфю":
					return (object)(string)comment;
				// дНОНКМХРЕКЭМШЕ
				case "онйсоюрекэ":
					return (object)(DtPartner)tmp_partner;
				case "юбрнлнахкэ":
					return (object)(DtAuto)tmp_auto;
				case "онйсоюрекэ_мюхлемнбюмхе":
					return (object)(string)tmp_customer_name;
				case "юбрнлнахкэ_лндекэ":
					return (object)(string)tmp_auto_model;
				case "юбрнлнахкэ_жбер":
					return (object)(string)tmp_auto_color;
				case "юбрнлнахкэ_хяонкмемхе":
					return (object)(string)tmp_auto_variant;
				case "юбрнлнахкэ_VIN":
					return (object)(string)tmp_auto_vin;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ_опндюфю":
					code = (long)val;
					break;
				case "яяшкйю_йнд_юбрнлнахкэ":
					code_auto = (long)val;
					break;
				case "яяшкйю_йнд_онйсоюрекэ":
					code_customer = (long)val;
					break;
				case "дюрю_юбрнлнахкэ_опндюфю":
					date = (DateTime)val;
					break;
				case "опхлевюмхе_юбрнлнахкэ_опндюфю":
					comment = (string)val;
					comment = comment.Trim();
					break;
				// дНОНКМХРЕКЭМШЕ
				case "онйсоюрекэ":
					tmp_partner			= (DtPartner)val;
					code_customer		= (long)tmp_partner.GetData("йнд_йнмрпюцемр");
					tmp_customer_name	= (string)tmp_partner.GetData("мюхлемнбюмхе_йпюрйне");
					break;
				case "юбрнлнахкэ":
					tmp_auto		= (DtAuto)val;
					code_auto		= (long)tmp_auto.GetData("йнд_юбрнлнахкэ");
					tmp_auto_model	= (string)tmp_auto.GetData("лндекэ");
					tmp_auto_color	= (string)tmp_auto.GetData("юбрнлнахкэ_жбер");
					tmp_auto_variant= (string)tmp_auto.GetData("юбрнлнахкэ_хяонкмемхе");
					tmp_auto_vin	= (string)tmp_auto.GetData("VIN");
					break;
				case "онйсоюрекэ_мюхлемнбюмхе":
					tmp_customer_name	= (string)val;
					break;
				case "юбрнлнахкэ_лндекэ":
					tmp_auto_model		= (string)val;
					break;
				case "юбрнлнахкэ_жбер":
					tmp_auto_color		= (string)val;
					break;
				case "юбрнлнахкэ_хяонкмемхе":
					tmp_auto_variant	= (string)val;
					break;
				case "юбрнлнахкэ_VIN":
					tmp_auto_vin		= (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "яяшкйю_йнд_юбрнлнахкэ":
					if(code_auto == 0) return false;
					break;
				case "яяшкйю_йнд_онйсоюрекэ":
					if(code_customer == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// вРНАШ ЯДЕКЮРЭ НДМНРХОМШЛ ДНАЮБКЕМХЕ Х ХГЛЕМЕМХЕ

			item.Tag				= code;
			item.Text				= this.date.ToShortDateString();
			item.SubItems.Add(this.tmp_customer_name);
			item.SubItems.Add(this.tmp_auto_model);
			item.SubItems.Add(this.tmp_auto_color);
			item.SubItems.Add(this.tmp_auto_variant);
			item.SubItems.Add(this.tmp_auto_vin);
		}
	}
}
