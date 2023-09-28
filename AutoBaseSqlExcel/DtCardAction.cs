using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardAction.
	/// </summary>
	public class DtCardAction : Dt
	{
		private DtCard.CardState _code = DtCard.CardState.NONE;

		private long card_number = 0;
		private int card_year = 0;
		private DateTime date = DateTime.MinValue;
		private string comment = "";

		#region Правильные геттеры и сеттеры
		public DtCard.CardState ActionCode
		{
			get { return _code; }
			set { _code = value; }
		}
		public long CardNumber
		{
			get { return card_number; }
			set { card_number = value; }
		}
		public int CardYear
		{
			get { return card_year; }
			set { card_year = value; }
		}
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}
		public string Comment
		{
			get { return comment; }
			set { comment = value; }
		}
		public bool Opening
		{
			get
			{
				if (_code == DtCard.CardState.OPEND) return true;
				else return false;
			}
		}
		public bool Closing
		{
			get
			{
				if (_code == DtCard.CardState.CLOSED) return true;
				else return false;
			}
		}
		public bool Endingwork
		{
			get
			{
				if (_code == DtCard.CardState.MARKWORKEND) return true;
				else return false;
			}
		}
		#endregion

		public DtCardAction()
		{
		}
		public DtCardAction(DtCard srcDtCard) : this()
		{
			card_number = srcDtCard.Number;
			card_year = srcDtCard.Year;
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "ССЫЛКА_НОМЕР_КАРТОЧКА":
					card_number = (long)val;
					break;
				case "ССЫЛКА_ГОД_КАРТОЧКА":
					card_year = (int)val;
					break;
				case "КОД":
					_code = (DtCard.CardState)val;
					break;
				case "ДАТА":
					date = (DateTime)val;
					break;
				case "ПРИМЕЧАНИЕ":
					comment = (string)val;
					break;
				default:
					break;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();      // Чтобы сделать однотипным добавление и изменение

			item.Tag = 0;
			item.Text = CodeTxt((short)_code) + " (" + this._code.ToString() + ")";
			item.SubItems.Add(this.date.ToString());
			item.SubItems.Add(this.comment);
		}
		private string CodeTxt(short action_code)
		{
			switch (action_code)
			{
				case 1:
					return ("Открытие");
				case 2:
					return ("Закрытие");
				case 3:
					return ("Приостановка");
				case 4:
					return ("Возобновление");
				case 10:
					return ("Отмена закрытия");
				default:
					return ("Неизвестное");
			}
		}
		public static bool Action(long card_number, int card_year, DbCardAction.ActionCodes code, string comment)
		{
			int run = 0;

			DbCardAction action = new DbCardAction(card_number, card_year);
			action.ActionCode = code;

			if (code == DbCardAction.ActionCodes.Open)
			{
				// Запрос пробега
				FormSelectString dialogRun = new FormSelectString("Укажите текущий пробег", "");
				dialogRun.ShowDialog();
				if (dialogRun.DialogResult == DialogResult.OK)
					run = dialogRun.SelectedInt;
				if (run == 0)
				{
					MessageBox.Show("Пробег не должен быть нулевым");
					return false;
				}
			}

			action.Run = run;
			action.Comment = comment;
			if (action.Write() == false)
			{
				Db.ShowFaults();
				return false;
			}
			return true;
		}
	}


	public class DtCardActionCollection
	{
		private ArrayList _actions;	// Список действий карточки
		private readonly DtCard _card; // Карточка для которой создан список действий
		public DtCardActionCollection(DtCard srcDtCard)
        {
			_actions = DbSqlCardAction.Select(srcDtCard);
			_card = srcDtCard;
        }
		public DtCardAction FindOpen()
        {
			if (_actions == null) return null;
			foreach(DtCardAction act in _actions)
            {
				if (act.Opening) return act;
            }
			return null;
        }
		public DtCardAction FindClose()
		{
			if (_actions == null) return null;
			foreach (DtCardAction act in _actions)
			{
				if (act.Closing) return act;
			}
			return null;
		}
		public DtCardAction FindEndwork()
		{
			if (_actions == null) return null;
			foreach (DtCardAction act in _actions)
			{
				if (act.Endingwork) return act;
			}
			return null;
		}
		public void AddAction(DtCardAction srcCardAction)
        {
			if (_actions == null) _actions = new ArrayList();
			_actions.Add(srcCardAction);
        }
	}
}

