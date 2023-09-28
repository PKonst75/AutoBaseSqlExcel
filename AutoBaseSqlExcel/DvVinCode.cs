using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DvVinCode.
	/// </summary>
	public class DvVinCode
	{
		private string vin_source;
		private string vin;
		private bool iso;
		private string iso_wmi;
		private string body;
		private string model;
		private int year;		// Для года выпуска

		public DvVinCode(string source)
		{
			vin_source	= source;
			vin			= "";
			body		= "";
			model		= "";

			iso			= false;
			iso_wmi		= "";
		}

		public bool CheckStepOne()
		{
			// Проверка VIN кода первого уровня и первоначальное преобразование
			vin = vin_source.Trim();	// Убираем пробелы в начале и конце строки
			vin = vin.ToUpper();
			vin	= StandartReplace(vin);
			return CheckSymbols();
		}

		private bool CheckSymbols()
		{
			// Проверка на содержание только правильных символов
			//		разрешенных для VIN кода
			string mask = "0123456789ABCDEFGHGKLMNPRSTUVWXYZ-";
			int result = 0;
			string s = "";
			int length = vin.Length;
			if(length == 0) return true;
			foreach(char e in vin.ToCharArray())
			{
				s = new String(e, 1);
				result = mask.IndexOf(s);
				if(result == -1) return false;
			}
			return true;
		}

		private bool CheckSymbolsDigit(string str)
		{
			// Проверка на содержание только правильных символов
			//		разрешенных для VIN кода
			string mask = "0123456789";
			int result = 0;
			string s = "";
			int length = str.Length;
			if(length == 0) return true;
			foreach(char e in str.ToCharArray())
			{
				s = new String(e, 1);
				result = mask.IndexOf(s);
				if(result == -1) return false;
			}
			return true;
		}

		private string StandartReplace(string source)
		{
			source = source.Replace("А", "A");
			source = source.Replace("В", "B");
			source = source.Replace("С", "C");
			source = source.Replace("Е", "E");
			source = source.Replace("Н", "H");
			source = source.Replace("К", "K");
			source = source.Replace("М", "M");
			source = source.Replace("О", "O");
			source = source.Replace("Р", "P");
			source = source.Replace("Т", "T");
			source = source.Replace("Х", "X");
			return source;
		}

		public bool Analize()
		{
			// Определение производителя
			// Для определенеия стандартного VIN кода нужно иметь определенную длину
			int length = vin.Length;
			switch(length)
			{
				case 17:
					// Европейский код
					iso = true;
					return AnalizeISO();
				default:
					// Код не поддается расшифровке
					return false;
			}
		}

		private bool AnalizeISO()
		{
			// Определение производителя
			// для VIN кода удовлетворяющего стандарту
			iso_wmi = vin.Substring(0, 3);
			switch(iso_wmi)
			{
				case "XTA":
					return AnalizeXTA();
				default:
					return false;
			}
		}

		#region Досуп к данным
		public string Vin
		{
			get
			{
				return vin;
			}
		}
		public bool Iso
		{
			get
			{
				return iso;
			}
		}
		public string IsoWmi
		{
			get
			{
				return iso_wmi;
			}
		}
		public string Model
		{
			get
			{
				return model;
			}
		}
		public string Body
		{
			get
			{
				return body;
			}
		}
		public int Year
		{
			get
			{
				return year;
			}
		}
		#endregion
		#region Анализ VIN кодов определенных производителей
		private bool AnalizeXTA()
		{
			string s;
			// Проверяем правильность VIN кода для АВТОВАЗА
			// Последние семь символов - это номер кузова (пока только цифры)
			s	 = vin.Substring(10);
			if(CheckSymbolsDigit(s) == false) return false;
			body = s;
			// Определяем год выпуска
			s	 = vin.Substring(9, 1);
			switch(s)
			{
				case "0":
					year = 2000; break;
				case "1":
					year = 2001; break;
				case "2":
					year = 2002; break;
				case "3":
					year = 2003; break;
				case "4":
					year = 2004; break;
				case "5":
					year = 2005; break;
				case "6":
					year = 2006; break;
				case "7":
					year = 2007; break;
				case "8":
					year = 2008; break;
				default:
					year = 0; break;
			}
			// Определяем модель
			model = vin.Substring(3, 5);
			return true;
		}
		#endregion
	}
}
