using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for CS_LicenceVehicle.
	/// </summary>
	public class CS_LicenceVehicle
	{
		// ���� ��� ����������� �������� � ���� ������
		public long		code;				// ��� ������������� �����������
		public long		version;			// ������ (��� ���� ������, ��� ���������� ��������)
		public long		code_auto;			// ��� ����������
		public long		code_owner;			// ��� ���������
		public string	licence_series;		// ����� �������������
		public string	licence_number;		// ����� �������������
		public DateTime	date;				// ���� ����������� ������������� ��������
		public string	vehicle_number;		// ��������������� ����
		public string	vehicle_region;		// ������
		

		// ��������� ������
		bool		flag_error;			// ���� ������� ������
		bool		flag_warning;		// ���� ������� ��������������
		ArrayList	list_error;			// ������ ������
		ArrayList	list_warning;		// ������ ��������������

		public CS_LicenceVehicle()
		{
			code			= 0;
			version			= 0;
			code_auto		= 0;
			code_owner		= 0;
			licence_series	= "";
			licence_number	= "";
			date			= DateTime.Now;
			vehicle_number	= "";
			vehicle_region	= "";
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt		= "";

			item.SubItems.Clear();
			item.Tag		= code;

			item.Text		= code.ToString();;
			txt				= vehicle_number + " " + vehicle_region;
			item.SubItems.Add(txt);
			txt				= licence_series + " " + licence_number;
			item.SubItems.Add(txt);
			item.SubItems.Add(date.ToShortDateString());
			
			// ����� �������������� ������ � ���������� �����
			DtPartner partner	= DbSqlPartner.Find(code_owner);
			DtAuto auto			= DbSqlAuto.Find(code_auto);

			string partner_txt	= "";
			string auto_txt		= "";
			if(partner != null) partner_txt = (string)partner.GetData("������������_�������");
			if(auto != null) auto_txt = auto.Txt();
			item.SubItems.Add(auto_txt);
			item.SubItems.Add(partner_txt);
		}

		public void AddError(string error)
		{
			flag_error		= true;
			if(list_error == null) list_error = new ArrayList();
			list_error.Add(error);
		}

		public void AddWarning(string warning)
		{
			flag_warning	= true;
			if(list_warning == null) list_warning = new ArrayList();
			list_warning.Add(warning);
		}
		public bool CheckError()
		{
			if(flag_error == false) return true;
			foreach(object o in list_error)
			{
				string str = (string)o;
				MessageBox.Show(str);
			}
			list_error.Clear();
			flag_error = false;
			return false;
		}
		public void CheckElement()
		{
			// �������� ���������������� �����
			if(vehicle_number.Length != 6)
			{
				flag_error = true;
				AddError("������������ ���������� ����/���� ���������������� �����");
			}
			if(IsRussianDigits(vehicle_number) == false)
			{
				flag_error = true;
				AddError("������������ ����� � ��������������� �����");
			}
			// �������� �������
			if(vehicle_region.Length > 3)
			{
				flag_error = true;
				AddError("������������ ���������� ���� �������");
			}
			if(IsDigits(vehicle_region) == false)
			{
				flag_error = true;
				AddError("������������ ����� � �������");
			}
			if(licence_series.Length != 4)
			{
				flag_error = true;
				AddError("������������ ���������� ����/���� � ����� ������������� ��");
			}
			if(IsRussianDigits(licence_series) == false)
			{
				flag_error = true;
				AddError("������������ ����� � ����� ������������� ��");
			}
			if(licence_number.Length != 6)
			{
				flag_error = true;
				AddError("������������ ���������� ���� � ������ ������������");
			}
			if(IsDigits(licence_number) == false)
			{
				flag_error = true;
				AddError("������������ ����� � ������ �������������");
			}
			if(code_auto == 0)
			{
				flag_error = true;
				AddError("�� ������ ����������");
			}
			if(code_owner == 0)
			{
				flag_error = true;
				AddError("�� ������ ��������");
			}
		}

		public bool IsRussianDigits(string txt)
		{
			string pattern = "��������������������������1234567890";
			char[] pattern_chr = pattern.ToCharArray();
			int index = txt.LastIndexOfAny(pattern_chr);
			while (index != -1)
			{
				txt = txt.Remove(index, 1);
				index = txt.LastIndexOfAny(pattern_chr);
			}
			if (txt.Length != 0) return false;
			return true;
		}

		public bool IsDigits(string txt)
		{
			string pattern = "1234567890";
			char[] pattern_chr = pattern.ToCharArray();
			int index = txt.LastIndexOfAny(pattern_chr);
			while (index != -1)
			{
				txt = txt.Remove(index, 1);
				index = txt.LastIndexOfAny(pattern_chr);
			}
			if (txt.Length != 0) return false;
			return true;
		}
	}
}
