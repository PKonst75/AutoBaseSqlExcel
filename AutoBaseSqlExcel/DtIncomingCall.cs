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

		// Структура описания входящего звонка
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
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			DtIncomingCall.Pair pair;
			pair.code		= this.code;
			pair.year		= this.year;
			item.Tag				= pair;
			if(this.contact_type == ContactType.CALL)
				item.Text				= "ЗВ";
			else
			{
				if(this.contact_type == ContactType.VISIT)
					item.Text				= "ПС";
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
				case "КОД":
					return (object)(long)code;
				case "ГОД":
					return (object)(int)year;
				case "ДАТА":
					return (object)(DateTime)date;
				case "ТИП":
					return (object)(ContactType)contact_type;
				case "ИНТЕРЕС":
					return (object)(short)interest;
				case "ФИО":
					return (object)(string)fio;
				case "КОНТАКТ":
					return (object)(string)contact;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД":
					code = (long)val;
					break;
				case "ГОД":
					year = (int)val;
					break;
				case "ДАТА":
					date = (DateTime)val;
					break;
				case "ТИП":
					contact_type = (ContactType)val;
					break;
				case "ИНТЕРЕС":
					interest = (short)val;
					break;
				case "ФИО":
					fio = (string)val;
					break;
				case "КОНТАКТ":
					contact = (string)val;
					break;
				default:
					break;
			}
		}
	}
}
