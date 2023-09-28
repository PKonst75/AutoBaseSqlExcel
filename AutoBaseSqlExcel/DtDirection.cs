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
				case "���_�����������":
					return (object)(long)code;
				case "�����_�����������":
					return (object)(string)number;
				case "��������_�����������":
					return (object)(string)description;
				case "������������_�������������_�����������":
					return (object)(string)tmp_factory_name;
				case "����_�����������":
					return (object)(DateTime)date;
				case "������_��������_�����������":
					return (object)(long)interval_start;
				case "���������_��������_�����������":
					return (object)(long)interval_end;
				case "�������������_�����������":
					return (object)(long)code_factory;
				case "������_�����������":
					return (object)(string)model;
				case "���_������_�����������":
					return (object)(long)search_type;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_�����������":
					code = (long)val;
					break;
				case "�������������_�����������":
					code_factory = (long)val;
					break;
				case "������_�����������":
					model = (string)val;
					break;
				case "������_��������_�����������":
					interval_start = (long)val;
					break;
				case "���������_��������_�����������":
					interval_end = (long)val;
					break;
				case "�����_�����������":
					number = (string)val;
					break;
				case "��������_�����������":
					description = (string)val;
					break;
				case "���_������_�����������":
					search_type = (long)val;
					break;
				case "����_�����������":
					date = (DateTime)val;
					break;
				case "������������_�������������_�����������":
					tmp_factory_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

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
