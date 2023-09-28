using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtVariant.
	/// </summary>
	public class DtVariant
	{
		long		code;					// Уникальный код
		long		code_model;				// Код модели для которой это исполнение
		string		variant_name;			// Наименование исполнения (Код, название, описание)
		string		variant_description;	// Расширенное описание исполнения
		bool		variant_canceled;		// Отметка об отмене исполнения

		public DtVariant()
		{
			code					= 0;
			code_model				= 0;
			variant_name			= "";
			variant_description		= "";
			variant_canceled		= false;
		}

		public DtVariant(DtVariant element)
		{
			code					= element.code;
			code_model				= element.code_model;
			variant_name			= element.variant_name;
			variant_description		= element.variant_description;
			variant_canceled		= element.variant_canceled;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					return (object)(long)code;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					return (object)(long)code_model;
				case "ИСПОЛНЕНИЕ_НАИМЕНОВАНИЕ":
					return (object)(string)variant_name;
				case "ИСПОЛНЕНИЕ_ОПИСАНИЕ":
					return (object)(string)variant_description;
				case "ИСПОЛНЕНИЕ_ОТМЕНЕН":
					return (object)(bool)variant_canceled;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					code = (long)val;
					break;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					code_model = (long)val;
					break;
				case "ИСПОЛНЕНИЕ_НАИМЕНОВАНИЕ":
					variant_name = (string)val;
					variant_name.Trim();
					break;
				case "ИСПОЛНЕНИЕ_ОПИСАНИЕ":
					variant_description = (string)val;
					variant_description.Trim();
					break;
				case "ИСПОЛНЕНИЕ_ОТМЕНЕН":
					variant_canceled = (bool)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					if(code_model <= 0) return false;
					break;
				case "ИСПОЛНЕНИЕ_НАИМЕНОВАНИЕ":
					if(variant_name.Length <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.variant_name;
			if(this.variant_canceled == true)
				SetLVItemCancel(item);
		}

		public static void SetLVItemCancel(ListViewItem item)
		{
			item.BackColor = Color.Gray;
		}
		public static void SetLVItemRestore(ListViewItem item)
		{
			item.BackColor = Color.White;
		}
		public override string ToString()
		{
			return this.variant_name;
		}
	}
}
