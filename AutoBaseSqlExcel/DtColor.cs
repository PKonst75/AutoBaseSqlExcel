using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Работа с цветами автомобиля
	/// </summary>
	public class DtColor
	{
		long		code;				// Уникальный код цвета в базе данных
		long		code_model;			// Модель к которой относится цвет	
		string		color_code;			// Код присвоеный цвету производителем
		string		color_name;			// Наименование цвета (Производителя)
		string		color_description;	// Описание цвета 

		bool		color_canceled;		// Не и спользуется в стандартном списке выбора

		public DtColor()
		{
			code				= 0;
			code_model			= 0;
			color_code			= "";
			color_name			= "";
			color_description	= "";

			color_canceled		= false;
		}

		public DtColor(DtColor element)
		{
			code				= element.code;
			code_model			= element.code_model;
			color_code			= element.color_code;
			color_name			= element.color_name;
			color_description	= element.color_description;

			color_canceled		= element.color_canceled;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ЦВЕТ":
					return (object)(long)code;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					return (object)(long)code_model;
				case "ЦВЕТ_КОД":
					return (object)(string)color_code;
				case "ЦВЕТ_НАИМЕНОВАНИЕ":
					return (object)(string)color_name;
				case "ЦВЕТ_ОПИСАНИЕ":
					return (object)(string)color_description;
				case "ЦВЕТ_ОТМЕНЕН":
					return (object)(bool)color_canceled;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_ЦВЕТ":
					code = (long)val;
					break;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					code_model = (long)val;
					break;
				case "ЦВЕТ_КОД":
					color_code = (string)val;
					color_code.Trim();
					break;
				case "ЦВЕТ_НАИМЕНОВАНИЕ":
					color_name = (string)val;
					color_name.Trim();
					break;
				case "ЦВЕТ_ОПИСАНИЕ":
					color_description = (string)val;
					color_description.Trim();
					break;
				case "ЦВЕТ_ОТМЕНЕН":
					color_canceled = (bool)val;
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
				case "ЦВЕТ_НАИМЕНОВАНИЕ":
					if(color_name.Length <= 0) return false;
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
			item.Text				= this.color_name + "(" + this.color_code + ")";
			if(this.color_canceled == true)
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
	}
}
