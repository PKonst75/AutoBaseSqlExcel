using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Управление наборами работ
	/// </summary>
	public class DtWorkCollection
	{
		long	code;		// Уникальный код набора
		string	name;		// Наименование набора

		public DtWorkCollection()
		{
			code	= 0;
			name	= "";
		}

		public DtWorkCollection(string collection_name)
		{
			code	= 0;
			name	= collection_name;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_КОЛЛЕКЦИЯ":
					return (object)(long)code;
				case "НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_КОЛЛЕКЦИЯ":
					code = (long)val;
					break;
				case "НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ":
					name = (string)val;
					name = name.Trim();
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
