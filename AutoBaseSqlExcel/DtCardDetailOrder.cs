using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Заявка на поставку деталей в заказ-наряде.
	/// </summary>
	public class DtCardDetailOrder
	{
		long code;						// Код заявки в карточке
		long card_number;				// Номер карточки
		int card_year;					// Год карточки
		long code_storage_detail;		// Код складской позиции
		long code_catalogue_detail;		// Код позиции в каталоге
		bool guaranty_flag;				// Флаг обозначения гарантийной детали
		// ВРЕМЕННЫЕ
		string tmp_name;				// Наименование детали

		public DtCardDetailOrder()
		{
			code					= 0L;
			card_number				= 0L;
			card_year				= 0;
			code_catalogue_detail	= 0;
			code_storage_detail		= 0;
			guaranty_flag			= false;

			tmp_name				= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_КАРТОЧКА_ДЕТАЛЬ_ЗАЯВКА":
					return (object)(long)code;
				case "НОМЕР_КАРТОЧКА":
					return (object)(long)card_number;
				case "ГОД_КАРТОЧКА":
					return (object)(int)card_year;
				case "КОД_КАТАЛОГ_ДЕТАЛЬ":
					return (object)(long)code_catalogue_detail;
				case "КОД_СКЛАД_ДЕТАЛЬ":
					return (object)(long)code_storage_detail;
				case "ГАРАНТИЯ":
					return (object)(bool)guaranty_flag;
				// ВРЕМЕННЫЕ
				case "НАИМЕНОВАНИЕ":
					return (object)(string)tmp_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_КАРТОЧКА_ДЕТАЛЬ_ЗАЯВКА":
					code = (long)val;
					break;
				case "НОМЕР_КАРТОЧКА":
					card_number = (long)val;
					break;
				case "ГОД_КАРТОЧКА":
					card_year = (int)val;
					break;
				case "КОД_КАТАЛОГ_ДЕТАЛЬ":
					code_catalogue_detail = (long)val;
					break;
				case "КОД_СКЛАД_ДЕТАЛЬ":
					code_storage_detail = (long)val;
					break;
				case "ФЛАГ_ГАРАНТИЯ":
					guaranty_flag = (bool)val;
					break;
				// ВСПОМАГАТЕЛЬНЫЕ
				case "НАИМЕНОВАНИЕ":
					tmp_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение
			item.SubItems.Add("");

			item.Tag				= this.code;
			item.Text				= this.tmp_name;
			if(this.guaranty_flag	== true)
				item.SubItems[1].Text = "+";
			else
				item.SubItems[1].Text = "-";
		}
	}
}
