using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardClaim.
	/// </summary>
	public class DtCardClaim
	{
		long card_number;       // Номер карточки, к которой относиться заявка
		int card_year;          // Год карточки, к которой относиться заявка
		long position;          // Розиция заявки в заказ/наряде
		long code_claim;            // Ссылка на код заявки в списке заявок
		bool defect;                // Указатель дефекта
		bool defect_confirm;        // Указатель подтверждения дефекта
		bool guaranty;          // Указатель гарантийности дефекта
		string comment;         // При необходимости комментарий
		bool is_agreement_date; // Есть ли дополнительное согласование
		DateTime agreement_date;        // Дата и время дополнительного согласования

		//string		tmp_claim_name;     // Описание заявки

		private DtClaim _tmpClaim; // Элемент заявка к которуму привязана заявка в карточке

		/*public class DtCardClaimTxt
        {
			public string position_txt;
			public string claim_txt;
			public DtCardClaimTxt(DtCardClaim claim)
            {
				position_txt = claim.position.ToString();
				//claim_txt = claim.tmp_claim_name;
				claim_txt = claim.Claim.Name;
            }
        }
		*/

		public DtCardClaim()
		{
			card_number = 0;
			card_year = 0;
			position = 0;
			code_claim = 0;
			defect = false;
			defect_confirm = false;
			guaranty = false;
			comment = "";
			is_agreement_date = false;
			agreement_date = DateTime.MinValue;

			//tmp_claim_name		= "";
		}

		public DtCardClaim(DtClaim claim, DtCard card) : this()
		{
			card_number = card.Number;
			card_year = card.Year;
			code_claim = claim.Code;
			_tmpClaim = claim;

			//tmp_claim_name		= claim.Name;
		}

		public object GetData(string data)
		{
			switch (data)
			{
				case "НОМЕР_КАРТОЧКА":
					return (object)(long)card_number;
				case "ГОД_КАРТОЧКА":
					return (object)(int)card_year;
				case "ПОЗИЦИЯ":
					return (object)(long)position;
				case "ССЫЛКА_КОД_ЗАЯВКА":
					return (object)(long)code_claim;
				case "ДЕФЕКТ":
					return (object)(bool)defect;
				case "ПОДТВЕРЖДЕНО":
					return (object)(bool)defect_confirm;
				case "ГАРАНТИЯ":
					return (object)(bool)guaranty;
				case "ПРИМЕЧАНИЕ":
					return (object)(string)comment;
				case "ДАТА_СОГЛАСОВАНИЯ":
					return (object)(DateTime)agreement_date;
				case "ЕСТЬ_ДАТА_СОГЛАСОВАНИЯ":
					return (object)(bool)is_agreement_date;
				// Дополнительное поле
				case "НАИМЕНОВАНИЕ_ЗАЯВКА":
					return (object)Claim.Name;
				//return (object)(string)tmp_claim_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "НОМЕР_КАРТОЧКА":
					card_number = (long)val;
					break;
				case "ГОД_КАРТОЧКА":
					card_year = (int)val;
					break;
				case "ПОЗИЦИЯ":
					position = (long)val;
					break;
				case "ССЫЛКА_КОД_ЗАЯВКА":
					code_claim = (long)val;
					break;
				case "ДЕФЕКТ":
					defect = (bool)val;
					break;
				case "ПОДТВЕРЖДЕНО":
					defect_confirm = (bool)val;
					break;
				case "ГАРАНТИЯ":
					guaranty = (bool)val;
					break;
				case "ПРИМЕЧАНИЕ":
					comment = (string)val;
					comment.Trim();
					break;
				case "ДАТА_СОГЛАСОВАНИЯ":
					agreement_date = (DateTime)val;
					break;
				case "ЕСТЬ_ДАТА_СОГЛАСОВАНИЯ":
					is_agreement_date = (bool)val;
					break;
				// Дополнительное поле
				case "НАИМЕНОВАНИЕ_ЗАЯВКА":
					//tmp_claim_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			DtTxtCardClaim cardClaimTxt = new DtTxtCardClaim(this);
			string txt = "";
			item.SubItems.Clear();      // Чтобы сделать однотипным добавление и изменение

			item.Tag = this.position;
			//item.Text = this.tmp_claim_name;
			//item.Text = Claim.Name;//  this.tmp_claim_name;
			//item.Text = "Тест"; //cardClaimTxt.CardClaimText;
			item.Text = cardClaimTxt.CardClaimText;
			item.SubItems.Add(this.comment);
			if (this.defect == true)
				txt = "+";
			else
				txt = "-";
			item.SubItems.Add(txt);
			if (this.defect_confirm == true)
				txt = "+";
			else
				txt = "-";
			item.SubItems.Add(txt);
			if (this.guaranty == true)
				txt = "+";
			else
				txt = "-";
			item.SubItems.Add(txt);
			if (this.is_agreement_date == true)
				txt = this.agreement_date.ToString();
			else
				txt = "";
			item.SubItems.Add(txt);
		}

		#region Сложные геттеры и сеттеры
		public DtClaim Claim
		{
			get
			{
				if (_tmpClaim != null) return _tmpClaim;
				return _tmpClaim = DbSqlClaim.Find(code_claim);
			}
		}
		#endregion
	}

    #region Класс отображения в текст
    public class DtTxtCardClaim
	{
		private readonly DtCardClaim _cardClaim;

		public DtTxtCardClaim(DtCardClaim srcCardClaim)
		{
			_cardClaim = srcCardClaim;
		}

		public string CardClaimText
		{
			get
			{
				if (_cardClaim.Claim == null) return "Базовая заявка не определена!";
				return _cardClaim.Claim.Name;
			}
		}
	}
    #endregion

    #region Класс представления коллекции заявок
    public class DtCardClaimCollection
	{
		private ArrayList _collection; // Список заявок карточки
		private readonly DtCard _card; // Карточка для которой создан список заявок

		public DtCardClaimCollection(DtCard srcDtCard)
		{
			_collection = DbSqlCardClaim.Select(srcDtCard);
			_card = srcDtCard;
		}
		public void AddAClaim(DtCardClaim srcCardClaim)
		{
			if (_collection == null) _collection = new ArrayList();
			_collection.Add(srcCardClaim);
		}

		public ArrayList Collection
		{
			get { return _collection; }
		}
	}
    #endregion

    #region Класс аналитики коллекции заявок
    public class DtCardClaimCollectionAnalitic
	{
		private ArrayList _dataCollection;
		private class ClaimAnaliticData
		{
			private readonly DtClaim _claim;
			private long _count;
			private DateTime _lastDate;

			public ClaimAnaliticData(DtCardClaim srcCadrClaim, DtCard srcCard)
            {
				_claim = srcCadrClaim.Claim;
				_count = 1;
				_lastDate = srcCard.Date;
            }

			public bool Compare(DtCardClaim srcCardClaim)
            {
				if (_claim.Code == srcCardClaim.Claim.Code) return true;
				return false;
            }
			public void AddDate(DateTime srcDate)
            {
				if (srcDate > _lastDate) _lastDate = srcDate;
				_count++;
			}

			public string ClaimString()
            {
				return _claim.Name + " " + _count.ToString() + " шт." + "Последняя: " + _lastDate.ToShortDateString();

            }
		}
		public DtCardClaimCollectionAnalitic()
		{

		}

		public void Add(DtCardClaim dtCardClaim, DtCard srcCard)
		{
			if (_dataCollection == null)
			{
				_dataCollection = new ArrayList();
				_dataCollection.Add(new ClaimAnaliticData(dtCardClaim, srcCard));
				return;
			}
			foreach (ClaimAnaliticData data in _dataCollection)
			{
				if (data.Compare(dtCardClaim))
				{
					data.AddDate(srcCard.Date);
					return;
				}
			}		
		}
		public void Add(DtCard srcCard)
		{
			DtCardClaimCollection claims = srcCard.Claims;
			foreach (DtCardClaim data in claims.Collection)
			{
				Add(data, srcCard);
			}
		}

		public string ClaimsStringList()
        {
			if (_dataCollection == null) return "Ранее заявок не было.";
			string result = "";
			foreach(ClaimAnaliticData data in _dataCollection)
            {
				result += data.ClaimString() + "\n";
            }
			if (result == "") result = "Ранее заявок не было.";
			return result;
        }
	}
    #endregion
}
