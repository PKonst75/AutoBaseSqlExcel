using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtWorkCollectionItem.
	/// </summary>
	public class DtWorkCollectionItem
	{
		long	code;					// Уникальный код элемента в наборе
		long	code_collection;		// Уникальный код набора
		int		number;					// Порядковый номер
		int		number_group;			// Отметка принадлежности к группе с порядковым номером
		string	name;					// Наименование элемента
		float	time;					// Трудоемкость элемента (норма времени)

		public DtWorkCollectionItem()
		{
			code			= 0;
			code_collection	= 0;
			number			= 0;
			number_group	= 0;
			name			= "";
			time			= 0.0F;
		}

		public DtWorkCollectionItem(long collection_code, string collection_name)
		{
			code			= 0;
			code_collection	= collection_code;
			name			= collection_name;
			time			= 0.0F;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					return (object)(long)code;
				case "ССЫЛКА_КОД_КОЛЛЕКЦИЯ":
					return (object)(long)code_collection;
				case "НОМЕР_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					return (object)(int)number;
				case "НОМЕР_ГРУППА_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					return (object)(int)number_group;
				case "НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					return (object)(string)name;
				case "ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					return (object)(float)time;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					code = (long)val;
					break;
				case "ССЫЛКА_КОД_КОЛЛЕКЦИЯ":
					code_collection = (long)val;
					break;
				case "НОМЕР_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					number = (int)val;
					break;
				case "НОМЕР_ГРУППА_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					number_group = (int)val;
					break;
				case "НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					name = (string)val;
					name = name.Trim();
					break;
				case "ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ":
					time = (float)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt = "";
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag					= this.code;
			if(this.number != 0)
				item.Text				= this.number.ToString();
			else
				item.Text				= "";
			if(this.number_group != 0)
				item.SubItems.Add(this.number_group.ToString());
			else
				item.SubItems.Add("");

			item.SubItems.Add(this.name);
			if(time != 0.0F)
				txt = time.ToString();
			else
				txt	= "";
			item.SubItems.Add(txt);
		}
	}
}
