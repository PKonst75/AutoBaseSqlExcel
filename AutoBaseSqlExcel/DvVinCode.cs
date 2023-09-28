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
		private int year;		// ��� ���� �������

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
			// �������� VIN ���� ������� ������ � �������������� ��������������
			vin = vin_source.Trim();	// ������� ������� � ������ � ����� ������
			vin = vin.ToUpper();
			vin	= StandartReplace(vin);
			return CheckSymbols();
		}

		private bool CheckSymbols()
		{
			// �������� �� ���������� ������ ���������� ��������
			//		����������� ��� VIN ����
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
			// �������� �� ���������� ������ ���������� ��������
			//		����������� ��� VIN ����
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
			source = source.Replace("�", "A");
			source = source.Replace("�", "B");
			source = source.Replace("�", "C");
			source = source.Replace("�", "E");
			source = source.Replace("�", "H");
			source = source.Replace("�", "K");
			source = source.Replace("�", "M");
			source = source.Replace("�", "O");
			source = source.Replace("�", "P");
			source = source.Replace("�", "T");
			source = source.Replace("�", "X");
			return source;
		}

		public bool Analize()
		{
			// ����������� �������������
			// ��� ������������ ������������ VIN ���� ����� ����� ������������ �����
			int length = vin.Length;
			switch(length)
			{
				case 17:
					// ����������� ���
					iso = true;
					return AnalizeISO();
				default:
					// ��� �� ��������� �����������
					return false;
			}
		}

		private bool AnalizeISO()
		{
			// ����������� �������������
			// ��� VIN ���� ���������������� ���������
			iso_wmi = vin.Substring(0, 3);
			switch(iso_wmi)
			{
				case "XTA":
					return AnalizeXTA();
				default:
					return false;
			}
		}

		#region ����� � ������
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
		#region ������ VIN ����� ������������ ��������������
		private bool AnalizeXTA()
		{
			string s;
			// ��������� ������������ VIN ���� ��� ��������
			// ��������� ���� �������� - ��� ����� ������ (���� ������ �����)
			s	 = vin.Substring(10);
			if(CheckSymbolsDigit(s) == false) return false;
			body = s;
			// ���������� ��� �������
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
			// ���������� ������
			model = vin.Substring(3, 5);
			return true;
		}
		#endregion
	}
}
