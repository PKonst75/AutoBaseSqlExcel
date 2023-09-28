using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Дисконтные карточки
	/// </summary>
	public class DtDiscount
	{
		private long			code;					// Код дисконтной карточки (номер на карточке)
		private float			discount_service_work;	// Скидка на работы сервиса
		private long			code_partner;			// Код контрагента по данной карточке
		private bool			flag;					// Флаг выданности карточки контрагенту
		private string			comment;				// Примечание к дисконтной карточке

		private string			tmp_partner_name;		// Наименование владельца карточки

		public DtDiscount()
		{
			code					= 0;
			discount_service_work	= 0.0F;
			code_partner			= 0;
			flag					= false;
			comment					= "";

			tmp_partner_name		= "";
		}

		public DtDiscount(DtDiscount element)
		{
			code					= element.code;
			discount_service_work	= element.discount_service_work;
			code_partner			= element.code_partner;
			flag					= element.flag;
			comment					= element.comment;

			tmp_partner_name		= element.tmp_partner_name;
		}

		public bool IsEqual(DtDiscount element)
		{
			if(code					 != element.code) return false;
			if(discount_service_work !=	element.discount_service_work) return false;
			if(code_partner			 != element.code_partner) return false;
			if(flag					 != element.flag) return false;
			if(comment				 != element.comment) return false;
			
			return true;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_ДИСКОНТ":
					return (object)(long)code;
				case "СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ":
					return (object)(float)discount_service_work;
				case "КОД_КОНТРАГЕНТ_ДИСКОНТ":
					return (object)(long)code_partner;
				case "ФЛАГ_ВЫДАНО_ДИСКОНТ":
					return (object)(bool)flag;
				case "ПРИМЕЧАНИЕ_ДИСКОНТ":
					return (object)(string)comment;
				case "КОНТРАГЕНТ_НАИМЕНОВАНИЕ":
					return (object)(string)tmp_partner_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_ДИСКОНТ":
					code = (long)val;
					break;
				case "СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ":
					discount_service_work = (float)val;
					break;
				case "КОД_КОНТРАГЕНТ_ДИСКОНТ":
					code_partner = (long)val;
					break;
				case "ФЛАГ_ВЫДАНО_ДИСКОНТ":
					flag = (bool)val;
					break;
				case "ПРИМЕЧАНИЕ_ДИСКОНТ":
					comment = (string)val;
					break;
				case "КОНТРАГЕНТ_НАИМЕНОВАНИЕ":
					tmp_partner_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.code.ToString();
			item.SubItems.Add(this.discount_service_work.ToString());
			if(this.code_partner > 0)
				item.SubItems.Add(this.tmp_partner_name);
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.comment);
			if(this.code_partner != 0 && this.flag == false)
				item.BackColor = System.Drawing.Color.Red;
			if(this.flag == true)
				item.BackColor = System.Drawing.Color.LightGreen;
		}
	}
}
