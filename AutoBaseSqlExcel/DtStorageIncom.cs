using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Приход запчастей на склад.
	/// </summary>
	public class DtStorageIncom
	{
		struct Pair
		{
			public long code;
			public long poition;
		}

		long code_doc;			// Код документа
		long position;			// Позиция в документе
		long code_detail;		// Код складской позиции
		float quontity;			// Количество
		float price;			// Цена за единицу

		string tmp_name;		// Наименование детали
		string tmp_articul;		// Номер детали / артикул
		string tmp_number;		// Номер документа
		DateTime tmp_date;		// Дата документа

		public DtStorageIncom()
		{
			code_doc		= 0;
			position		= 0;
			code_detail		= 0;
			quontity		= 0.0F;
			price			= 0.0F;

			tmp_name		= "";
			tmp_articul		= "";
			tmp_number		= "";
			tmp_date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_ДОКУМЕНТ":
					return (object)(long)code_doc;
				case "ПОЗИЦИЯ":
					return (object)(long)position;
				case "КОД_СКЛАД_ДЕТАЛЬ":
					return (object)(long)code_detail;
				case "КОЛИЧЕСТВО":
					return (object)(float)quontity;
				case "ЦЕНА":
					return (object)(float)price;
				case "НАИМЕНОВАНИЕ":
					return (object)(string)tmp_name;
				case "АРТИКУЛ":
					return (object)(string)tmp_articul;
				case "НОМЕР":
					return (object)(string)tmp_number;
				case "ДАТА":
					return (object)(DateTime)tmp_date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_ДОКУМЕНТ":
					code_doc = (long)val;
					break;
				case "ПОЗИЦИЯ":
					position = (long)val;
					break;
				case "КОД_СКЛАД_ДЕТАЛЬ":
					code_detail = (long)val;
					break;
				case "КОЛИЧЕСТВО":
					quontity = (float)val;
					break;
				case "ЦЕНА":
					price = (float)val;
					break;
				case "НАИМЕНОВАНИЕ":
					tmp_name = (string)val;
					break;
				case "АРТИКУЛ":
					tmp_articul = (string)val;
					break;
				case "НОМЕР":
					tmp_number = (string)val;
					break;
				case "ДАТА":
					tmp_date = (DateTime)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			Pair pair;
			pair.code				= this.code_doc;
			pair.poition			= this.position;
			item.Tag				= pair;

			item.Text				= this.position.ToString();
			item.SubItems.Add(this.tmp_articul);
			item.SubItems.Add(this.tmp_name);
			item.SubItems.Add(this.quontity.ToString());
			item.SubItems.Add(this.price.ToString());
		}

		public void SetLVItemMove(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			Pair pair;
			pair.code				= this.code_doc;
			pair.poition			= this.position;
			item.Tag				= pair;

			item.Text				= this.tmp_articul;
			item.SubItems.Add(this.tmp_name);
			item.SubItems.Add(this.tmp_date.ToString());
			item.SubItems.Add(this.quontity.ToString());
			item.SubItems.Add(this.tmp_number);
		}
	}
}
