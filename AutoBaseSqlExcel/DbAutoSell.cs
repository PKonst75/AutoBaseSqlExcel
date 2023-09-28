using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAutoSell.
	/// </summary>
	public class DbAutoSell:Db
	{
		private long		code;
		private long		codeAutoIncom;
		private long		codeAuto;
		private float		price;
		private float		priceOption;
		private DateTime	dateSell;
		private long		partner;
		private string		comment;
		private long		form;
		private DateTime	dateOutcom;
		private bool		outcom;
		private string		document;
		private bool		cashless;
		private long		credit;
		private long		tradein;


		private long		codeAutoStorage;
		
		private DbAutoIncom	tmpAutoIncom;
		private DbPartner	tmpPartner;
		
		
		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
	
		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 10 собственных полей и остальное
			readerLength = 15 + DbAutoIncom.ReaderLength + DbPartner.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПРОДАЖА", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoIncom", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoStorage", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@price", SqlDbType.Float);
			cmdWrite.Parameters.Add("@priceOption", SqlDbType.Float);
			cmdWrite.Parameters.Add("@dateSell", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@partner", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@form", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@dateOutcom", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@outcom", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@document", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@cashless", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@credit", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@tradein", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРОДАЖА", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		#endregion

		#region Конструкторы
		public DbAutoSell(DbAutoStorage autoStorage)
		{
			code				= 0;
			codeAutoIncom		= autoStorage.AutoIncom.Code;
			codeAuto			= autoStorage.AutoIncom.Auto.Code;
			codeAutoStorage		= autoStorage.Code;
			price				= autoStorage.Price;
			priceOption			= autoStorage.PriceOption;
			dateSell			= DateTime.Now;
			partner				= autoStorage.Reserv;
			comment				= "";
			form				= 0;
			dateOutcom			= DateTime.Now;
			outcom				= false;
			document			= "";
			cashless			= false;
			credit				= 0;
			tradein				= 0;
		

			tmpAutoIncom		= autoStorage.AutoIncom;
			// Дочитать покупателя
			if(partner != 0)
				tmpPartner			= DbPartner.Find(partner);
			else
				tmpPartner = null;

			adding = true;
		}

		public DbAutoSell(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeAutoIncom		= (long)GetValueLong(reader, offset);		offset++;
			codeAuto			= (long)GetValueLong(reader, offset);		offset++;
			price				= (float)GetValueFloat(reader, offset);		offset++;
			priceOption			= (float)GetValueFloat(reader, offset);		offset++;
			dateSell			= (DateTime)GetValueDate(reader, offset);	offset++;
			partner				= (long)GetValueLong(reader, offset);		offset++;
			comment				= (string)GetValueString(reader, offset);	offset++;
			form				= (long)GetValueLong(reader, offset);		offset++;
			dateOutcom			= (DateTime)GetValueDate(reader, offset);	offset++;
			outcom				= (bool)GetValueBool(reader, offset);		offset++;
			document			= (string)GetValueString(reader, offset);	offset++;
			cashless			= (bool)GetValueBool(reader, offset);		offset++;
			credit				= (long)GetValueLong(reader, offset);		offset++;
			tradein				= (long)GetValueLong(reader, offset);		offset++;
			
			tmpAutoIncom		= new DbAutoIncom(reader, offset);			offset += DbAutoIncom.ReaderLength;
			tmpPartner			= new DbPartner(reader, offset);			offset += DbPartner.ReaderLength;

			adding				= false;
		}

		public DbAutoSell(DbAutoSell src)
		{
			code				= src.code;
			codeAutoIncom		= src.codeAutoIncom;
			codeAuto			= src.codeAuto;
			price				= src.price;
			priceOption			= src.priceOption;
			dateSell			= src.dateSell;
			partner				= src.partner;
			comment				= src.comment;
			form				= src.form;
			dateOutcom			= src.dateOutcom;
			outcom				= src.outcom;
			document			= src.document;
			cashless			= src.cashless;
			credit				= src.credit;
			tradein				= src.tradein;
			
			tmpAutoIncom		= src.tmpAutoIncom;
			tmpPartner			= src.tmpPartner;
			
			adding			= false;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			SqlTransaction trans = null;
			if((adding!=true)&&(changed!=true)) return true;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeAutoIncom"].Value		= (long)codeAutoIncom;
				cmdWrite.Parameters["@codeAuto"].Value			= (long)codeAuto;
				cmdWrite.Parameters["@codeAutoStorage"].Value	= (long)codeAutoStorage;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.Parameters["@priceOption"].Value		= (float)priceOption;
				cmdWrite.Parameters["@dateSell"].Value			= (DateTime)dateSell;
				cmdWrite.Parameters["@partner"].Value			= (long)partner;
				cmdWrite.Parameters["@comment"].Value			= (string)comment;
				cmdWrite.Parameters["@form"].Value				= (long)form;
				cmdWrite.Parameters["@dateOutcom"].Value		= (DateTime)dateOutcom;
				cmdWrite.Parameters["@outcom"].Value			= (bool)outcom;
				cmdWrite.Parameters["@document"].Value			= (string)document;
				cmdWrite.Parameters["@cashless"].Value			= (bool)cashless;
				cmdWrite.Parameters["@credit"].Value			= (long)credit;
				cmdWrite.Parameters["@tradein"].Value			= (long)tradein;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				trans.Rollback();
				SetTransaction(null);
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			return true;
		}
		#endregion
		
		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			item.Text = DateSellTxt;
			item.SubItems[1].Text = AutoModelTxt;
			item.SubItems[2].Text = AutoSubModelTxt;
			item.SubItems[3].Text = AutoComplectTxt;
			item.SubItems[4].Text = AutoColorTxt;
			item.SubItems[5].Text = VinTxt;
			item.SubItems[6].Text = PriceAutoTxt + " + " + PriceOptionTxt;
			item.SubItems[7].Text = PartnerTxt;
			item.BackColor = Color.White;
			if(this.outcom != true)
				item.BackColor = Color.Yellow;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoSell element = new DbAutoSell(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion


		#region Отображение параметров в текст
		public string DateSellTxt
		{
			get
			{
				return Db.DateToTxt(dateSell);
			}
		}
		public string PriceAutoTxt
		{
			get
			{
				return Db.CachToTxt(price);
			}
			set
			{
				price	= SetFloatNotMinus(price, value, "Цена автомобиля");
			}
		}
		public string PriceOptionTxt
		{
			get
			{
				return Db.CachToTxt(priceOption);
			}
			set
			{
				priceOption	= SetFloatNotMinus(priceOption, value, "Цена доп. оборудования");
			}
		}
		public string PartnerTxt
		{
			get
			{
				if(partner == 0) return "Не выбран покупатель";
				if(tmpPartner == null) return "?????";
				return tmpPartner.Title;
			}
		}
		public string AutoModelTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoModelTxt;
			}
		}
		public string AutoSubModelTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoSubModelTxt;
			}
		}
		public string AutoComplectTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoComplectTxt;
			}
		}
		public string AutoColorTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.AutoColorsTxt;
			}
		}
		public string VinTxt
		{
			get
			{
				if(tmpAutoIncom == null) return "??";
				return tmpAutoIncom.VinNoTxt;
			}
		}
		#endregion

		#region Доступ к параметрам - чтение
		public DbAuto Auto
		{
			get
			{
				if(tmpAutoIncom == null) return null;
				return tmpAutoIncom.Auto;
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		public float PriceAuto
		{
			get
			{
				return price;
			}
		}
		public float PriceOption
		{
			get
			{
				return priceOption;
			}
		}
		#endregion

		#region Доступ к параметрам - изменение
		public long Credit
		{
			get
			{
				return credit;
			}
			set
			{
				if(value == credit) return;
				credit = value;
				changed = true;
			}
		}
		public long Tradein
		{
			get
			{
				return tradein;
			}
			set
			{
				if(value == tradein) return;
				tradein = value;
				changed = true;
			}
		}
		public bool Cashless
		{
			get
			{
				return cashless;
			}
			set
			{
				if(value == cashless) return;
				cashless = value;
				changed = true;
			}
		}
		public float Price
		{
			get
			{
				return price;
			}
			set
			{
				if(value <= 0) return;
				if(value == price) return;
				price = value;
				changed = true;
			}
		}
		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = SetStringLength(comment, value, 256, "Примечание");
			}
		}
		public string Document
		{
			get
			{
				return document;
			}
			set
			{
				document = SetStringLength(document, value, 256, "Документ продажи");
			}
		}
		public DateTime DateSell
		{
			get
			{
				return dateSell;
			}
			set
			{
				dateSell = value;
			}
		}
		public DbPartner Partner
		{
			get
			{
				if(partner == 0) return null;
				return tmpPartner;
			}
			set
			{
				if(value == null)
				{
					if(partner == 0) return;
					partner = 0;
					tmpPartner = null;
					changed = true;
					return;
				}
				if(partner == value.Code) return;
				partner = value.Code;
				tmpPartner = value;
				changed = true;
			}
		}
		#endregion

		
		#region Определение виртуальных методов
		#endregion
	}
}
