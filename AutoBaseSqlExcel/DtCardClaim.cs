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
		long card_number;       // ����� ��������, � ������� ���������� ������
		int card_year;          // ��� ��������, � ������� ���������� ������
		long position;          // ������� ������ � �����/������
		long code_claim;            // ������ �� ��� ������ � ������ ������
		bool defect;                // ��������� �������
		bool defect_confirm;        // ��������� ������������� �������
		bool guaranty;          // ��������� ������������� �������
		string comment;         // ��� ������������� �����������
		bool is_agreement_date; // ���� �� �������������� ������������
		DateTime agreement_date;        // ���� � ����� ��������������� ������������

		//string		tmp_claim_name;     // �������� ������

		private DtClaim _tmpClaim; // ������� ������ � �������� ��������� ������ � ��������

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
				case "�����_��������":
					return (object)(long)card_number;
				case "���_��������":
					return (object)(int)card_year;
				case "�������":
					return (object)(long)position;
				case "������_���_������":
					return (object)(long)code_claim;
				case "������":
					return (object)(bool)defect;
				case "������������":
					return (object)(bool)defect_confirm;
				case "��������":
					return (object)(bool)guaranty;
				case "����������":
					return (object)(string)comment;
				case "����_������������":
					return (object)(DateTime)agreement_date;
				case "����_����_������������":
					return (object)(bool)is_agreement_date;
				// �������������� ����
				case "������������_������":
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
				case "�����_��������":
					card_number = (long)val;
					break;
				case "���_��������":
					card_year = (int)val;
					break;
				case "�������":
					position = (long)val;
					break;
				case "������_���_������":
					code_claim = (long)val;
					break;
				case "������":
					defect = (bool)val;
					break;
				case "������������":
					defect_confirm = (bool)val;
					break;
				case "��������":
					guaranty = (bool)val;
					break;
				case "����������":
					comment = (string)val;
					comment.Trim();
					break;
				case "����_������������":
					agreement_date = (DateTime)val;
					break;
				case "����_����_������������":
					is_agreement_date = (bool)val;
					break;
				// �������������� ����
				case "������������_������":
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
			item.SubItems.Clear();      // ����� ������� ���������� ���������� � ���������

			item.Tag = this.position;
			//item.Text = this.tmp_claim_name;
			//item.Text = Claim.Name;//  this.tmp_claim_name;
			//item.Text = "����"; //cardClaimTxt.CardClaimText;
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

		#region ������� ������� � �������
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

    #region ����� ����������� � �����
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
				if (_cardClaim.Claim == null) return "������� ������ �� ����������!";
				return _cardClaim.Claim.Name;
			}
		}
	}
    #endregion

    #region ����� ������������� ��������� ������
    public class DtCardClaimCollection
	{
		private ArrayList _collection; // ������ ������ ��������
		private readonly DtCard _card; // �������� ��� ������� ������ ������ ������

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

    #region ����� ��������� ��������� ������
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
				return _claim.Name + " " + _count.ToString() + " ��." + "���������: " + _lastDate.ToShortDateString();

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
			if (_dataCollection == null) return "����� ������ �� ����.";
			string result = "";
			foreach(ClaimAnaliticData data in _dataCollection)
            {
				result += data.ClaimString() + "\n";
            }
			if (result == "") result = "����� ������ �� ����.";
			return result;
        }
	}
    #endregion
}
