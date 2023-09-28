using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Структура данных описывающая платеж
	/// </summary>
	public class CS_Payment
	{
		public struct Pair
		{
			public long code;
			public int year;
		}
		// Ключ для определения элемента в базе данных
		public long		code;				// Код платежа
		public int		year;				// Год
		// Параметры платежа
		public DateTime	date;				// Дата платежа
		public long		code_department;	// Отдел кассы
		public long		code_workshop;		// Подразделение сервиса
		public float	summ;				// Сумма платежа
		// Дополнительные параметры платежа
		public string	comment;			// Примечание
		public long		card_number;		// Номер карточки
		public int		card_year;			// Год карточки
		public long		code_auto;			// Код автомобиля
		public long		code_partner;		// Код контрагента
		// Параметры статуса платежа
		public long		supervisor_check;	// Платеж проведен
		// Текстовые параметры для отображения данных
		public string		str_warrant;		// Заказ-наряд
		public string		str_auto;			// Автомобиль
		public string		str_partner;		// Контрагент
		public string		str_department;		// Отдел кассы
		public string		str_workshop;		// Подразделение сервиса
		// Параметры ошибки
		bool		flag_error;			// Флаг наличия ошибки
		bool		flag_warning;		// Флаг наличия предупреждений
		ArrayList	list_error;			// Список ошибок
		ArrayList	list_warning;		// Список предупреждений

		public void AddError(string error)
		{
			flag_error		= true;
			if(list_error == null) list_error = new ArrayList();
			list_error.Add(error);
		}

		public void AddWarning(string warning)
		{
			flag_warning	= true;
			if(list_warning == null) list_warning = new ArrayList();
			list_warning.Add(warning);
		}

		public CS_Payment()
		{
			code			= 0L;
			year			= 0;

			date			= DateTime.Now;
			code_department	= 0;
			code_workshop	= 0;
			summ			= 0.0F;
			comment			= "";
			card_number		= 0L;
			card_year		= 0;
			code_auto		= 0L;
			code_partner	= 0L;

			supervisor_check	= 0L;

			str_warrant		= "";
			str_auto		= "";
			str_partner		= "";
			str_department	= "";
			str_workshop	= "";
		}

		public CS_Payment(long department)
		{
			code			= 0L;
			year			= 0;

			date			= DateTime.Now;
			code_department	= department;
			code_workshop	= 0;
			summ			= 0.0F;
			comment			= "";
			card_number		= 0L;
			card_year		= 0;
			code_auto		= 0L;
			code_partner	= 0L;

			supervisor_check = 0L;

			str_warrant		= "";
			str_auto		= "";
			str_partner		= "";
			str_department	= "";
			str_workshop	= "";

			if(department == 1)
			{
				str_department = "ТО, РЕМОНТ, МАСЛА";
			}
			if(department == 2)
			{
				str_department = "ЗАПЧАСТИ";
			}
		}

		public DtCard Card
		{
			set
			{
				card_number		= value.Number;
				card_year		= value.Year;
				// Зачитываем остальные данные
				DtCard card = DbSqlCard.Find(card_number, card_year);
				if(card == null)
				{
					AddError("Не найдена карточка");
					return;
				}
				str_warrant = card.WarrantNumber.ToString() + " / " + card.Year.ToString();
				// Проверяем статус заказ-наряда
				short status = (short)card.State;
				if (status == 0)
				{
					AddError("Нельзя оплачивать неоткрытую карточку");
					return;
				}
				// Ищем автомобиль
				code_auto = card.CodeAuto;// (long)card.GetData("АВТОМОБИЛЬ_КАРТОЧКА");
				if(code_auto != 0)
				{
					DtAuto auto = DbSqlAuto.Find(code_auto);
					if(auto == null)
					{
						AddError("Не найден автомобиль");
						return;
					}
					str_auto = auto.Txt();
				}
				// Ищем контрагента
				code_partner = card.CodeOwner;
				if(code_partner != 0)
				{
					DtPartner partner	= DbSqlPartner.Find(code_partner);
					if(partner == null)
					{
						AddError("Не найден владелец");
						return;
					}
					str_partner = partner.GetTitle();
				}
				// Ищем подразделение
				code_workshop = card.CodeWorkshop;
				if(code_workshop == 0)
				{
					AddError("Не выбрано подразделение");
					return;
				}
				else
				{
					DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
					if(workshop == null)
					{
						AddError("Не найдено подразделение");
						return;
					}
					str_workshop = workshop.Txt();
				}
				// Устанавливаем размер платежа
				if(code_department == 0)
				{
					AddError("Не выбран отдел КАССЫ");
				}
				// РАЗБИВАЕМ ПО ОТДЕЛАМ КАССЫ
				if(code_department == 1)
				{
					// Работа и масла
					summ = card.SummWorkPay() + card.SummDetailOilPay();
				}
				if(code_department == 2)
				{
					// Запчасти
					summ = card.SummDetailPay();
				}
			}
		}

		public bool CheckError()
		{
			if(flag_error == false) return true;
			foreach(object o in list_error)
			{
				string str = (string)o;
				MessageBox.Show(str);
			}
			flag_error = false;
			return false;
		}
		public void CheckElement()
		{
			if(summ == 0)
			{
				flag_error = true;
				AddError("Нельзя оформлять нулевой платеж");
			}
			if(code_department == 0)
			{
				flag_error = true;
				AddError("Нельзя оформлять платеж без отдела кассы");
			}
			if(code_workshop == 0)
			{
				flag_error = true;
				AddError("Нельзя оформлять платеж без подразделения");
			}
		}

		public DtWorkshop Workshop
		{
			set
			{
				// Ищем подразделение
				code_workshop = (long)value.GetData("КОД_ЦЕХ");
				if(code_workshop == 0)
				{
					AddError("Не выбрано подразделение");
					return;
				}
				else
				{
					DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
					if(workshop == null)
					{
						AddError("Не найдено подразделение");
						return;
					}
					str_workshop = workshop.Txt();
				}
			}
		}

		public DtAuto Auto
		{
			set
			{
				// Ищем автомобиль
				code_auto		= (long)value.GetData("КОД_АВТОМОБИЛЬ");
				if(code_auto != 0)
				{
					DtAuto auto = DbSqlAuto.Find(code_auto);
					if(auto == null)
					{
						AddError("Не найден автомобиль");
						return;
					}
					str_auto = auto.Txt();
				}
			}
		}

		public void SetTNode_Supervisor(TreeNode node)
		{
			string text = summ.ToString() + " (" + date.ToShortDateString() + ")";
			node.Text	= text;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();

			Pair pair = new Pair();
			pair.code		= code;
			pair.year		= year;
			item.Tag		= pair;

			item.Text		= date.ToLongTimeString();
			item.SubItems.Add(code_department.ToString());
			item.SubItems.Add(summ.ToString());
			
			// Поиск дополнительных данных и заполнение полей
			DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
			DtCard card = DbSqlCard.Find(card_number, card_year);
			DtAuto auto = DbSqlAuto.Find(code_auto);

			string workshop_txt = "";
			string card_txt = "";
			string auto_txt = "";
			if(workshop != null) workshop_txt = (string)workshop.GetData("НАИМЕНОВАНИЕ_ЦЕХ");
			if(card != null) card_txt = card.Number.ToString() + "/" + card.Year.ToString();
			if(auto != null) auto_txt = auto.Txt();
			item.SubItems.Add(workshop_txt);
			item.SubItems.Add(card_txt);
			item.SubItems.Add(auto_txt);

			if(supervisor_check == 0)
				item.StateImageIndex = 0;
			else
				item.StateImageIndex = 1;
		}
	}
}
