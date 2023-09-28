using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Заявка на склад
	/// </summary>
	public class DtStorageRequest
	{
		public struct CodeYear
		{
			public long code;
			public int year;
		};
		// Описание заявки на склад
		private long		code;				// Код заявки
		private int			year;				// Год заявки
		private DateTime	date;				// Дата заявки
		private long		code_storage;		// Код складской позиции для заявки
		private float		quontity;			// Количество для заявки
		private bool		guaranty;			// Отметка о заявке на гарантию
		private DateTime	date_perfomance;	// Желаемая дата исполнения заявки
		private long		code_requester;		// Код подписавшего заявку
		private long		card_number;		// Номер карточки
		private int			card_year;			// Год карточки
		private long		code_partner;		// Код контрагента
		private DateTime	date_give;			// Дата подачи заявки
		private long		code_giver;			// Код подавшего заявку
		private DateTime	date_execute;		// Дата выполнения заявки
		private long		code_execute;		// Код подписавшего выполнение заявку
		private long		code_archive;		// Код подписавшего архивацию
		private DateTime	date_supply;		// Предполагаемая дата поставки

		private string		tmp_detail_name;		// Наименование заказанной детали
		private string		tmp_requester_name;		// Данные подписавшего заявку
		private bool		tmp_date_perfomance_is;	// Установленна ли желаемая дата
		private bool		tmp_date_give_is;		// Установленна дата подачи заявки
		private bool		tmp_date_execute_is;	// Установленна дата выполнения заявки
		private bool		tmp_date_supply_is;		// Установленна ли дата предполагаемой поставки
		private string		tmp_partner_name;		// Наименование контрагента


		public DtStorageRequest()
		{
			code			= 0;
			year			= 0;
			date			= DateTime.Now;
			code_storage	= 0;
			quontity		= 0.0F;
			guaranty		= false;
			date_perfomance	= DateTime.Now;
			code_requester	= 0;
			card_number		= 0;
			card_year		= 0;
			code_partner	= 0;
			date_give		= DateTime.Now;
			code_giver		= 0;
			date_execute	= DateTime.Now;
			code_execute	= 0;
			code_archive	= 0;
			date_supply		= DateTime.Now;

			tmp_detail_name			= "";
			tmp_requester_name		= "";
			tmp_date_perfomance_is	= false;
			tmp_date_give_is		= false;
			tmp_date_execute_is		= false;
			tmp_date_supply_is		= false;
			tmp_partner_name		= "";
		}

		public DtStorageRequest(DtStorageRequest element)
		{
			code			= element.code;
			year			= element.year;
			date			= element.date;
			code_storage	= element.code_storage;
			quontity		= element.quontity;
			guaranty		= element.guaranty;
			date_perfomance	= element.date_perfomance;
			code_requester	= element.code_requester;
			card_number		= element.card_number;
			card_year		= element.card_year;
			code_partner	= element.code_partner;
			date_give		= element.date_give;
			code_giver		= element.code_giver;
			date_execute	= element.date_execute;
			code_execute	= element.code_execute;
			code_archive	= element.code_archive;
			date_supply		= element.date_supply;

			tmp_detail_name			= element.tmp_detail_name;
			tmp_requester_name		= element.tmp_requester_name;
			tmp_date_perfomance_is	= element.tmp_date_perfomance_is;
			tmp_date_give_is		= element.tmp_date_give_is;
			tmp_date_execute_is		= element.tmp_date_execute_is;
			tmp_date_supply_is		= element.tmp_date_supply_is;
			tmp_partner_name		= element.tmp_partner_name;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_ЗАЯВКА":
					return (object)(long)code;
				case "ГОД_ЗАЯВКА":
					return (object)(int)year;
				case "ДАТА_ЗАЯВКА":
					return (object)(DateTime)date;
				case "ССЫЛКА_КОД_СКЛАД_ДЕТАЛЬ":
					return (object)(long)code_storage;
				case "КОЛИЧЕСТВО_СКЛАД_ДЕТАЛЬ":
					return (object)(float)quontity;
				case "ГАРАНТИЯ_ЗАЯВКА":
					return (object)(bool)guaranty;
				case "ТРЕБУЕМАЯ_ДАТА_ИСПОЛНЕНИЯ":
					return (object)(DateTime)date_perfomance;
				case "ЕСТЬ_ТРЕБУЕМАЯ_ДАТА_ИСПОЛНЕНИЯ":
					return (object)(bool)tmp_date_perfomance_is;
				case "КОД_ПОДПИСАЛ_ЗАЯВКА":
					return (object)(long)code_requester;
				case "ССЫЛКА_НОМЕР_КАРТОЧКА":
					return (object)(long)card_number;
				case "ССЫЛКА_ГОД_КАРТОЧКА":
					return (object)(int)card_year;
				case "ССЫЛКА_КОД_КОНТРАГЕНТ":
					return (object)(long)code_partner;
				case "ДАТА_ЗАЯВКА_ПОДАЧА":
					return (object)(DateTime)date_give;
				case "ЕСТЬ_ДАТА_ЗАЯВКА_ПОДАЧА":
					return (object)(bool)tmp_date_give_is;
				case "КОД_ПОДПИСАЛ_ПОДАЧА_ЗАЯВКА":
					return (object)(long)code_giver;
				case "ДАТА_ЗАЯВКА_ВЫПОЛНЕНИЕ":
					return (object)(DateTime)date_execute;
				case "ЕСТЬ_ДАТА_ЗАЯВКА_ВЫПОЛНЕНИЕ":
					return (object)(bool)tmp_date_execute_is;
				case "КОД_ПОДПИСАЛ_ВЫПОЛНЕНИЕ_ЗАЯВКА":
					return (object)(long)code_execute;
				case "КОД_ПОДПИСАЛ_АРХИВАЦИЯ":
					return (object)(long)code_archive;
				case "ДАТА_ПОСТАВКИ":
					return (object)(DateTime)date_supply;
				case "ЕСТЬ_ДАТА_ПОСТАВКИ":
					return (object)(bool)tmp_date_supply_is;
				// Вспомагательные
				case "НАИМЕНОВАНИЕ_СКЛАД_ДЕТАЛЬ":
					return (object)(string)tmp_detail_name;
				case "ПОДПИСАЛ_ЗАЯВКА":
					return (object)(string)tmp_requester_name;
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
				case "КОД_ЗАЯВКА":
					code = (long)val;
					break;
				case "ГОД_ЗАЯВКА":
					year = (int)val;
					break;
				case "ДАТА_ЗАЯВКА":
					date = (DateTime)val;
					break;
				case "ССЫЛКА_КОД_СКЛАД_ДЕТАЛЬ":
					code_storage = (long)val;
					break;
				case "КОЛИЧЕСТВО_СКЛАД_ДЕТАЛЬ":
					quontity = (float)val;
					break;
				case "ГАРАНТИЯ_ЗАЯВКА":
					guaranty = (bool)val;
					break;
				case "ТРЕБУЕМАЯ_ДАТА_ИСПОЛНЕНИЯ":
					date_perfomance = (DateTime)val;
					tmp_date_perfomance_is	= true;
					break;
				case "КОД_ПОДПИСАЛ_ЗАЯВКА":
					code_requester = (long)val;
					break;
				case "ССЫЛКА_НОМЕР_КАРТОЧКА":
					card_number = (long)val;
					break;
				case "ССЫЛКА_ГОД_КАРТОЧКА":
					card_year = (int)val;
					break;
				case "ССЫЛКА_КОД_КОНТРАГЕНТ":
					code_partner = (long)val;
					break;
				case "ДАТА_ЗАЯВКА_ПОДАЧА":
					date_give = (DateTime)val;
					tmp_date_give_is	= true;
					break;
				case "КОД_ПОДПИСАЛ_ПОДАЧА_ЗАЯВКА":
					code_giver = (long)val;
					break;
				case "ДАТА_ЗАЯВКА_ВЫПОЛНЕНИЕ":
					date_execute = (DateTime)val;
					tmp_date_execute_is	= true;
					break;
				case "КОД_ПОДПИСАЛ_ВЫПОЛНЕНИЕ_ЗАЯВКА":
					code_execute = (long)val;
					break;
				case "КОД_ПОДПИСАЛ_АРХИВАЦИЯ":
					code_archive = (long)val;
					break;
				case "ДАТА_ПОСТАВКИ":
					date_supply = (DateTime)val;
					tmp_date_supply_is	= true;
					break;
				// Вспомагательные
				case "НАИМЕНОВАНИЕ_СКЛАД_ДЕТАЛЬ":
					tmp_detail_name = (string)val;
					break;
				case "ПОДПИСАЛ_ЗАЯВКА":
					tmp_requester_name = (string)val;
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

			CodeYear code_year;
			code_year.code = this.code;
			code_year.year = this.year;
			item.Tag				= code_year;
			item.Text				= this.code.ToString();
			item.SubItems.Add(this.date.ToString());
			item.SubItems.Add(this.tmp_detail_name);
			item.SubItems.Add(this.quontity.ToString());
			if(this.tmp_date_perfomance_is == true)
				item.SubItems.Add(this.date_perfomance.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_partner_name);
			if(this.tmp_date_supply_is == true)
				item.SubItems.Add(this.date_supply.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_requester_name);
			if(this.guaranty == false)
				item.StateImageIndex = 0;
			else
				item.StateImageIndex = 1;
			item.BackColor = Color.Red;
			if(tmp_date_give_is == true)
			{
				item.BackColor = Color.Blue;
			}
			if(tmp_date_execute_is == true)
			{
				item.BackColor = Color.Green;
			}
		}
	}
}
