using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardDetail.
	/// </summary>
	/// 
	public enum DETAIL_TYPE:short { NONE=0, OIL=1}
	public class DtCardDetail:Calculatable
	{
		public class DtCardDetailTxt
        {
			public readonly string detailName;
			public readonly string amount;
			public readonly string price;
			public readonly string unit;
			public readonly string catalog_number;
			public DtCardDetailTxt(DtCardDetail detail)
            {
				detailName = detail.tmp_name;
				amount = detail.quontity.ToString();
				price = detail.price.ToString();
				unit = detail.tmp_unit_name;
				catalog_number = detail.tmp_catalog_no;
            }
        }
		private long	card_number;
		private int		card_year;
		private long	position;
		private long	code_storage;
		private float	quontity;
		private float	price;
		private float	input;
		private bool	guaranty;
		private bool	outer;
		private bool	check;
		private long	code_recive;
		private string	mistake;
		private long	guaranty_type;
		private long	mistake_initiator;
		private bool	present;			// Äàííàÿ äåòàëü èäåò â ïîäàğîê
        private bool    to;			        // Äàííàÿ äåòàëü èäåò â ÒÎ êàê çàï÷àñòü èëè ìàòåğèàë â ÒÎ
        private float   discount;           // Ñêèäêà íà êîíêğåòíóş ïîçèöèş

		private string	tmp_catalog_no;
		private string	tmp_name;
		private bool	tmp_liquid;
		private long	tmp_code_1c;
		private string	tmp_guaranty_name;
		private string	tmp_mistake_initiator;
		private string	tmp_unit_name;

		// Àãğèãèğîâàííå âíåøíèå äàííûå
		private DtStorageDetail _tmpStorageDetail;
		
		public DtCardDetail()
		{
			card_number			= 0;
			card_year			= 0;
			position			= 0;
			code_storage		= 0;
			quontity			= 0.0F;
			price				= 0.0F;
			input				= 0.0F;
			guaranty			= false;
			outer				= false;
			check				= false;
			code_recive			= 0;
			guaranty_type		= 0;
			mistake_initiator	= 0;
			mistake				= "";
			present				= false;
            to = false;
            discount = 0.0F;

			tmp_catalog_no			= "";
			tmp_name				= "";
			tmp_liquid				= false;
			tmp_code_1c				= 0;
			tmp_mistake_initiator	= "";
			tmp_guaranty_name		= "";
			tmp_unit_name			= "";

			_tmpStorageDetail = null;
		}

		public DtCardDetail(DbCardDetail dtl)
		{
			card_number			= 0;
			card_year			= 0;
			position			= 0;
			code_storage		= dtl.CodeDetailStorage;
			quontity			= dtl.Quontity;
			price				= dtl.Price;
			input				= dtl.InputSumm;
			guaranty			= dtl.Guaranty;
			outer				= false;
			check				= false;
			code_recive			= 0;
			guaranty_type		= 0;
			mistake_initiator	= 0;
			mistake				= "";
			present				= false;
            to = false;
            discount = dtl.Discount;

			tmp_catalog_no			= dtl.CodeDetailTxt;
			tmp_name				= dtl.DetailNameTxt;
			tmp_liquid				= dtl.Oil;
			tmp_code_1c				= 0;
			tmp_mistake_initiator	= "";
			tmp_guaranty_name		= "";
			tmp_unit_name			= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(long)card_number;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(int)card_year;
				case "ÏÎÇÈÖÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(long)position;
				case "ÊÎËÈ×ÅÑÒÂÎ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(float)quontity;
				case "ÖÅÍÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(float)price;
				case "ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(long)mistake_initiator;
				case "ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(long)guaranty_type;
				case "ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(bool)guaranty;
				case "ÆÈÄÊÎÑÒÜ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(bool)tmp_liquid;
				case "ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(string)mistake;
				case "ÏÎÄÀĞÎÊ":
					return (object)(bool)present;
                case "ÒÎ":
                    return (object)(bool)to;
                case "ÑÊÈÄÊÀ":
                    return (object)(float)discount;
				case "ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(string)tmp_guaranty_name;
				case "ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					 return (object)(string)tmp_mistake_initiator;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(string)tmp_name;
				case "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(string)tmp_unit_name;
				case "ÊÀÒÀËÎÃ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					return (object)(string)tmp_catalog_no;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					card_number = (long)val;
					break;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					card_year = (int)val;
					break;
				case "ÏÎÇÈÖÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					position = (long)val;
					break;
				case "ÊÎËÈ×ÅÑÒÂÎ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					quontity = (float)val;
					break;
				case "ÖÅÍÀ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					price = (float)val;
					break;
				case "ÂÕÎÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					input = (float)val;
					break;
				case "ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					guaranty = (bool)val;
					break;
				case "ÂÍÅØÍßß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					outer = (bool)val;
					break;
				case "×ÅÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					check = (bool)val;
					break;
				case "ÏÎËÓ×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					code_recive = (long)val;
					break;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_name = (string)val;
					break;
				case "ÅÄÈÍÈÖÀ_ÈÇÌÅĞÅÍÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_unit_name = (string)val;
					break;
				case "ÊÀÒÀËÎÃ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_catalog_no = (string)val;
					break;
				case "ÆÈÄÊÎÑÒÜ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_liquid = (bool)val;
					break;
				case "ÊÎÄ_1Ñ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_code_1c = (long)val;
					break;
				case "ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_guaranty_name = (string)val;
					break;
				case "ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					tmp_mistake_initiator = (string)val;
					break;
				case "ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					mistake = (string)val;
					break;
				case "ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					mistake_initiator = (long)val;
					break;
				case "ÏÎÄÀĞÎÊ":
					present = (bool)val;
					break;
                case "ÒÎ":
                    to = (bool)val;
                    break;
                case "ÑÊÈÄÊÀ":
                    discount = (float)val;
                    break;
				case "ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					guaranty_type = (long)val;
					break;
				case "ÊÎÄ_ÑÊËÀÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ":
					code_storage = (long)val;
					break;
				default:
					break;
			}
		}

		public void SetTNode_Supervisor(TreeNode node)
		{
			node.Text = this.tmp_guaranty_name + " - " + this.tmp_name + " (" + this.tmp_catalog_no + ") - " + this.quontity.ToString() + " " + this.tmp_unit_name;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå
			item.StateImageIndex	= 0;
			item.UseItemStyleForSubItems	= false;

			item.Tag				= this.position;
			item.Text				= this.tmp_catalog_no;
            string nm = this.tmp_name;
            if (this.discount > 0.0F)
                nm = nm + " / Ñêèäêà " + this.discount.ToString() + "%";
			//item.SubItems.Add(this.tmp_name);
            item.SubItems.Add(nm);
			item.SubItems.Add(this.quontity.ToString());
			item.SubItems.Add(this.price.ToString());
			item.SubItems.Add((this.price * this.quontity).ToString());
			item.SubItems.Add(this.input.ToString());
			item.SubItems.Add(this.tmp_guaranty_name);
			item.SubItems.Add(this.tmp_mistake_initiator);
			item.SubItems.Add(this.mistake);
			// Ğàñêğàøèâàíèå
			if (input == 0.0F)
				item.SubItems[5].BackColor = Color.Red;

			// Óñòàíîâêà èêîíîê
			if(guaranty == true)
			{
				if(tmp_liquid == true)
				{
					if(tmp_code_1c == 0)
						item.StateImageIndex	= 8;
					else
						item.StateImageIndex	= 6;
				}
				else
				{
					if(tmp_code_1c == 0)
						item.StateImageIndex	= 7;
					else
						item.StateImageIndex	= 5;
				}
			}
			else
			{
				if(tmp_liquid == true)
				{
					if(tmp_code_1c == 0)
						item.StateImageIndex	= 4;
					else
						item.StateImageIndex	= 2;
				}
				else
				{
					if(tmp_code_1c == 0)
						item.StateImageIndex	= 3;
					else
						item.StateImageIndex	= 1;
				}
			}
			if (outer == true || check == true)
				item.StateImageIndex	= 9;
		}

		public float DetailSummCash
		{
			get
			{
				if(this.guaranty == true) return 0.0F;
				if(this.tmp_liquid == true) return 0.0F;
                if (discount == 0.0F)
                    return this.price * this.quontity;
                return (this.price - this.price / 100 * this.discount) * this.quontity;
			}
		}

		public float DetailSummCashInput
		{
			get
			{
				if(this.guaranty == true) return 0.0F;
				if(this.tmp_liquid == true) return 0.0F;
				return this.input * this.quontity;
			}
		}

		public float DetailSummOilCash
		{
			get
			{
				if(this.guaranty == true) return 0.0F;
				if(this.tmp_liquid == false) return 0.0F;
				return this.price * this.quontity;
			}
		}

		public bool IsGuaranty
		{
			get
			{
				if(this.guaranty == true) return true;
				return false;
			}
		}

		public float DetailSummOilCashInput
		{
			get
			{
				if(this.guaranty == true) return 0.0F;
				if(this.tmp_liquid == false) return 0.0F;
				return this.input * this.quontity;
			}
		}
		public string DetailName
		{
			get
			{
				return this.tmp_name;
			}
		}
		public string DetailQuontityTxt
		{
			get
			{
				return this.quontity.ToString();
			}
		}

        public float DetailSummDiscount
        {
            get
            {
                if (this.guaranty == true) return this.price * this.quontity;
                if (this.tmp_liquid == true) return this.price * this.quontity;
                if (discount == 0.0F)
                    return this.price * this.quontity;
                return (this.price - this.price / 100 * this.discount) * this.quontity;
            }
        }
        public float DetailSummDiscountPure
        {
            get
            {
                if (discount == 0.0F)
                    return this.price * this.quontity;
                return (this.price - this.price / 100 * this.discount) * this.quontity;
            }
        }
        public float DetailSummForCommonDiscount
        {
            get
            {
                if (discount == 0.0F)
                    return this.price * this.quontity;
                return 0.0F;
            }
        }

        public bool IsDiscount
        {
            get
            {
                if (discount == 0.0F) return false;
                return true;
            }
        }
		#region Íîâûå ñäëæíûå ãåòòåğû è ñåòòåğû
		public DtStorageDetail StorageDetail
        {
            get
            {
				if (_tmpStorageDetail != null) return _tmpStorageDetail;
				return _tmpStorageDetail = DbSqlStorageDetail.Find(code_storage);
            }
        }
        #endregion 

        #region Ğåàëèçàöèÿ èíòåğôåéñòà Calculatable
        public float PriceUnit()
		{
			return price;
		}
		public float TotalAmount()
		{
			return quontity;
		}
		public float SummDatabase()
		{
			return PriceUnit() * TotalAmount();
		}
		public float SummDiscount()
		{
			if (discount < 100.0F && discount > 0.0F)
				return SummDatabase() * discount / 100.0F;
			else
				return 0.0F;
		}
		public float SummBonus()
		{
			if (discount == 100.0F) return SummDatabase();
			else return 0.0F;
		}
		public float SimpleAmountUnit()
		{
			return quontity;
		}
		public float SimpleAmount()
		{
			return quontity;
		}
		public bool GuaranteeFlag()
		{
			return guaranty;
		}
		public WORK_TYPE WorkType()
        {
			return WORK_TYPE.NONE;
        }
		public DETAIL_TYPE DetailType()
		{
			if(StorageDetail == null)
				return DETAIL_TYPE.NONE;
			if (StorageDetail.Liquid)
				return DETAIL_TYPE.OIL;
			return DETAIL_TYPE.NONE;
		}
		public float Expenses()
        {
			return input * TotalAmount();
        }
		#endregion
	}
}
