using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс управления общим свойством - причина - отказа, установки в другом месте, и т.д.
	/// </summary>
	public class DtCommonReason
	{
		long		code;
		string		description;

		public DtCommonReason()
		{
			code		= 0;
			description	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_ПРИЧИНА":
					return (object)(long)code;
				case "ОПИСАНИЕ":
					return (object)(string)description;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_ПРИЧИНА":
					code = (long)val;
					break;
				case "ОПИСАНИЕ":
					description = (string)val;
					description.Trim();
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt = "";
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.description;
		}
	}
}
