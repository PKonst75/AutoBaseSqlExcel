using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Группа трудоемкостей по умолчанию
	/// </summary>
	public class DtWorkGroup
	{
		long		code;			// Уникальный код
		string		name;			// Наименование группы трудоемкостей

		public DtWorkGroup()
		{
			code		= 0;
			name		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ТИП":
					return (object)(long)code;
				case "НАИМЕНОВАНИЕ":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ТИП":
					code = (long)val;
					break;
				case "НАИМЕНОВАНИЕ":
					name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.name;
		}

	}
}
