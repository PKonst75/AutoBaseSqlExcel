using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Документ прихода запчастей на склад
	/// </summary>
	public class DtStorageIncomDoc
	{
		long code;					// Уникальный код документа
		string number;				// Номер документа
		DateTime date;				// Дата документа

		public DtStorageIncomDoc()
		{
			code		= 0;
			number		= "";
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД":
					return (object)(long)code;
				case "НОМЕР":
					return (object)(string)number;
				case "ДАТА":
					return (object)(DateTime)date;
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
				case "НОМЕР":
					number = (string)val;
					break;
				case "ДАТА":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.number;
			item.SubItems.Add(this.date.ToString());
		}
	}
}
