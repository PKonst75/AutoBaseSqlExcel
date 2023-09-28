using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtIncomingCall.
	/// </summary>
	public class DtIncomingCall
	{
		public enum ContactType:short{NONE=0, CALL=1, VISIT=2};
		struct Pair
		{
			public long code;
			public int year;
		}

		// ��������� �������� ��������� ������
		public long code;
		public int year;
		public DateTime date;
		public ContactType contact_type;
		public short interest;
		public string fio;
		public string contact;

		public DtIncomingCall()
		{
			code = 0L;
			year = 0;
			date = DateTime.Now;
			contact_type = ContactType.CALL;
			interest = 0;
			fio = "";
			contact = "";
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			DtIncomingCall.Pair pair;
			pair.code		= this.code;
			pair.year		= this.year;
			item.Tag				= pair;
			if(this.contact_type == ContactType.CALL)
				item.Text				= "��";
			else
			{
				if(this.contact_type == ContactType.VISIT)
					item.Text				= "��";
				else
				{
					item.Text				= "00";
				}
			}
			item.SubItems.Add(this.date.ToShortDateString());
			item.SubItems.Add(this.interest.ToString());
			item.SubItems.Add(this.fio);
			item.SubItems.Add(this.contact);	
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���":
					return (object)(long)code;
				case "���":
					return (object)(int)year;
				case "����":
					return (object)(DateTime)date;
				case "���":
					return (object)(ContactType)contact_type;
				case "�������":
					return (object)(short)interest;
				case "���":
					return (object)(string)fio;
				case "�������":
					return (object)(string)contact;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���":
					code = (long)val;
					break;
				case "���":
					year = (int)val;
					break;
				case "����":
					date = (DateTime)val;
					break;
				case "���":
					contact_type = (ContactType)val;
					break;
				case "�������":
					interest = (short)val;
					break;
				case "���":
					fio = (string)val;
					break;
				case "�������":
					contact = (string)val;
					break;
				default:
					break;
			}
		}
	}
}
