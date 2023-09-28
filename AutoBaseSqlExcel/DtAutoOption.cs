using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoOption.
	/// </summary>
	public class DtAutoOption
	{
		public long code;
		public string name;
		public long code_group;

		public long	tmp_option_variant;				// Вариант опции, для создания комплектации
		public string	tmp_option_variant_name;	// Вариант опции, для создания комплектации Наименование

		public bool tmp_active;						// Флаг присутствия в комплектации
		public bool tmp_change;						// Флаг изменения позиции

		public long tmp_code_model;
		public long tmp_code_model_variant;

		public DtAutoOption()
		{
			code = 0L;
			name = "";
			code_group = 0L;

			tmp_option_variant = 0L;
			tmp_option_variant_name	= "";

			tmp_active	= false;
			tmp_change	= false;

			tmp_code_model = 0;
			tmp_code_model_variant = 0;;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД":
					return (object)(long)code;
				case "ОПЦИЯ_НАИМЕНОВАНИЕ":
					return (object)(string)name;
				case "ССЫЛКА_КОД_ОПЦИЯ_ГРУППА":
					return (object)(long)code_group;
				case "КОД_МОДЕЛЬ":
					return (object)(long)tmp_code_model;
				case "КОД_МОДЕЛЬ_ВАРИАНТ":
					return (object)(long)tmp_code_model_variant;
				case "ВАРИАНТ_НАИМЕНОВАНИЕ":
					return (object)(string)tmp_option_variant_name;
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
				case "ОПЦИЯ_НАИМЕНОВАНИЕ":
					name = (string)val;
					name.Trim();
					break;
				case "ССЫЛКА_КОД_ОПЦИЯ_ГРУППА":
					code_group = (long)val;
					break;
				case "КОД_МОДЕЛЬ":
					tmp_code_model = (long)val;
					break;
				case "КОД_МОДЕЛЬ_ВАРИАНТ":
					tmp_code_model_variant = (long)val;
					break;
				case "ВАРИАНТ_НАИМЕНОВАНИЕ":
					tmp_option_variant_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this;
			item.Text				= this.name;
			item.SubItems.Add(this.tmp_option_variant_name);
		}

		public void SetLVItemComplect(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this;
			item.Text				= this.name;
			item.SubItems.Add(this.tmp_option_variant_name);

			if(tmp_code_model != 0) item.Checked = true;
		}
	}
}
