using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtDirection.
	/// </summary>
	public class DtDirection
	{
		private long		code;
		private long		code_factory;
		private string		model;
		private long		interval_start;
		private long		interval_end;
		private string		number;
		private string		description;
		private long		search_type;
		private DateTime	date;

		private string		tmp_factory_name;

		public DtDirection()
		{
			code			= 0;
			code_factory	= 0;
			model			= "";
			interval_start	= 0;
			interval_end	= 0;
			number			= "";
			description		= "";
			search_type		= 0;
			date			= DateTime.Now;

			tmp_factory_name	= "";
		}

		public DtDirection(DtDirection element)
		{
			code			= element.code;
			code_factory	= element.code_factory;
			model			= element.model;
			interval_start	= element.interval_start;
			interval_end	= element.interval_end;
			number			= element.number;
			description		= element.description;
			search_type		= element.search_type;
			date			= element.date;

			tmp_factory_name	= element.tmp_factory_name;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "йнд_опедохяюмхе":
					return (object)(long)code;
				case "мнлеп_опедохяюмхе":
					return (object)(string)number;
				case "нохяюмхе_опедохяюмхе":
					return (object)(string)description;
				case "мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе":
					return (object)(string)tmp_factory_name;
				case "дюрю_опедохяюмхе":
					return (object)(DateTime)date;
				case "мювюкн_хмрепбюк_опедохяюмхе":
					return (object)(long)interval_start;
				case "нйнмвюмхе_хмрепбюк_опедохяюмхе":
					return (object)(long)interval_end;
				case "опнхгбндхрекэ_опедохяюмхе":
					return (object)(long)code_factory;
				case "лндекэ_опедохяюмхе":
					return (object)(string)model;
				case "рхо_онхяйю_опедохяюмхе":
					return (object)(long)search_type;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "йнд_опедохяюмхе":
					code = (long)val;
					break;
				case "опнхгбндхрекэ_опедохяюмхе":
					code_factory = (long)val;
					break;
				case "лндекэ_опедохяюмхе":
					model = (string)val;
					break;
				case "мювюкн_хмрепбюк_опедохяюмхе":
					interval_start = (long)val;
					break;
				case "нйнмвюмхе_хмрепбюк_опедохяюмхе":
					interval_end = (long)val;
					break;
				case "мнлеп_опедохяюмхе":
					number = (string)val;
					break;
				case "нохяюмхе_опедохяюмхе":
					description = (string)val;
					break;
				case "рхо_онхяйю_опедохяюмхе":
					search_type = (long)val;
					break;
				case "дюрю_опедохяюмхе":
					date = (DateTime)val;
					break;
				case "мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе":
					tmp_factory_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// вРНАШ ЯДЕКЮРЭ НДМНРХОМШЛ ДНАЮБКЕМХЕ Х ХГЛЕМЕМХЕ

			item.Tag				= this.code;
			item.Text				= this.tmp_factory_name;
			item.SubItems.Add(this.number);
			item.SubItems.Add(this.date.ToShortDateString());
			item.SubItems.Add(this.model);
			item.SubItems.Add(this.interval_start.ToString() + "-" +this.interval_end.ToString());
			item.SubItems.Add(this.description);
		}

		public bool CheckAuto(DbAuto auto)
		{
			if(auto.CodeFactory != code_factory) return false;
			if(auto.SparePartNumber < interval_start) return false;
			if(auto.SparePartNumber > interval_end) return false;
			if(auto.ModelTxt.IndexOf(model) == -1) return false;
			return true;
		}
	}
}
