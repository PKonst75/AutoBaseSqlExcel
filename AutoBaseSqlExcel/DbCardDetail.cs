using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardDetail.
	/// </summary>
	public class DbCardDetail:Db
	{
		private long			cardNumber;				// Номер карточки
		private int				cardYear;				// Год карточки
		private long			number;					// Номер детали по порядку
		private long			codeDetailStorage;		// Код устанавливаемой складской позиции
		private long			codeDetail;				// Код устанавливаемой детали
		private float			quontity;				// Количество деталей
		private float			price;					// цена
		private float			nds;					// НДС
		private bool			guaranty;				// Гарантия Да/Нет
		private bool			outer;					// Внешняя деталь Да/Нет
		private bool			check;					// Деталь по копии чека Да/Нет
		private float			input;					// Входня цена детали
		private long			code_reciver;			// Код получателя данной детали
		private long			code_return;			// Код вернувшего деталь на склад
		private bool			present;				// Отметка подарка
        private bool to;                                // Отметка отношения детали к ТО
        private float discount;                         // Скидка на конкретную позицию

		private DbDetail		tmpDetail;				// Устанавливаемая деталь
		private DbDetailStorage	tmpDetailStorage;		// Установленная складская позиция

		private bool done;
		private float incom;
		private int reserve;

		private bool tmpDelete;
		private bool tmpExists;

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectReciveReturn;
		private static SqlCommand cmdExecSelectNotIncom;
		private static SqlCommand cmdWriteRecive;
		private static SqlCommand cmdWriteReturn;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 11 собственных полей и остальное
			readerLength = 11 + 1 + 2 + DbDetail.ReaderLength + DbDetailStorage.ReaderLength + 1 + 1;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КАРТОЧКА_ДЕТАЛЬ_В1", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetail", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetailStorage", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@quontity", SqlDbType.Real);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@nds", SqlDbType.Real);
			cmdWrite.Parameters.Add("@guaranty", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@outer", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@check", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@input", SqlDbType.Real);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@number"].Direction = ParameterDirection.InputOutput;

			cmdWriteRecive = new SqlCommand("WRITE_КАРТОЧКА_ДЕТАЛЬ_ПОЛУЧЕНИЕ", conn);
			cmdWriteRecive.CommandType = CommandType.StoredProcedure;
			cmdWriteRecive.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWriteRecive.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWriteRecive.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWriteRecive.Parameters.Add("@code_reciver", SqlDbType.BigInt);
			Db.SetReturnError(cmdWriteRecive);

			cmdWriteReturn = new SqlCommand("WRITE_КАРТОЧКА_ДЕТАЛЬ_ВОЗВРАТ", conn);
			cmdWriteReturn.CommandType = CommandType.StoredProcedure;
			cmdWriteReturn.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWriteReturn.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWriteReturn.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWriteReturn.Parameters.Add("@code_returner", SqlDbType.BigInt);
			Db.SetReturnError(cmdWriteReturn);

			cmdSelect = new SqlCommand("SELECT_КАРТОЧКА_ДЕТАЛЬ_В4", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@cardYear", SqlDbType.Int);

			cmdSelectReciveReturn = new SqlCommand("SELECT_КАРТОЧКА_ДЕТАЛЬ_ПОЛУЧЕНИЕ_ВОЗВРАТ", conn);
			cmdSelectReciveReturn.CommandType = CommandType.StoredProcedure;
			cmdSelectReciveReturn.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelectReciveReturn.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdSelectReciveReturn.Parameters.Add("@recive", SqlDbType.Bit);

			cmdExecSelectNotIncom = new SqlCommand("SELECT_CARD_DETAIL_NOTINCOM", conn);
			cmdExecSelectNotIncom.CommandType = CommandType.StoredProcedure;
			cmdExecSelectNotIncom.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdExecSelectNotIncom.Parameters.Add("@cardYear", SqlDbType.Int);
		}
		#endregion

		#region Конструкторы

		public DbCardDetail(DbCard sourceCard, DbDetail detail)
		{
			cardNumber			= sourceCard.Number;
			cardYear			= sourceCard.Year;
			number				= 0;
			codeDetail			= detail.Code;
			codeDetailStorage	= 0;
			quontity			= 1.0F;
			price				= 0.0F;
			nds					= 0.0F;
			guaranty			= false;
			outer				= false;
			check				= false;
			input				= 0.0F;
			code_reciver		= 0;
			code_return			= 0;
			present				= false;
            to = false;
            discount = 0.0F;


			tmpDetail			= detail;
			tmpDetailStorage	= null;

			adding				= true;
			tmpExists			= false;
			tmpDelete			= false;
			
			done = false;
			incom = 0.0F;
			reserve = 0;
		}

		public DbCardDetail(DbCard sourceCard, DbDetailStorage detailStorage)
		{
			cardNumber			= sourceCard.Number;
			cardYear			= sourceCard.Year;
			number				= 0;
			codeDetail			= 0;
			codeDetailStorage	= detailStorage.Code;
			quontity			= 1.0F;
			price				= detailStorage.Price;
			nds					= detailStorage.Nds;
			guaranty			= false;
			outer				= false;
			check				= false;
			input				= detailStorage.Input;
			code_reciver		= 0;
			code_return			= 0;
			present				= false;
            to = false;
            discount = 0.0F;
			
			tmpDetail			= null;
			tmpDetailStorage	= detailStorage;

			adding				= true;
			tmpDelete			= false;
			tmpExists			= false;

			done = false;
			incom = 0.0F;
		}

		public DbCardDetail(DbCard sourceCard, DbDetail detail, DbDetailStorage detailStorage)
		{
			cardNumber			= sourceCard.Number;
			cardYear			= sourceCard.Year;
			number				= 0;
			codeDetail			= detail.Code;
			codeDetailStorage	= detailStorage.Code;
			quontity			= 1.0F;
			price				= detailStorage.Price;
			nds					= detailStorage.Nds;
			guaranty			= false;
			outer				= false;
			check				= false;
			input				= detailStorage.Input;
			code_reciver		= 0;
			code_return			= 0;
			present				= false;
            to = false;
            discount = 0.0F;
			
			tmpDetail			= detail;
			tmpDetailStorage	= detailStorage;

			adding				= true;
			tmpDelete			= false;
			tmpExists			= false;

			done = false;
			incom = 0.0F;
		}

		public DbCardDetail(SqlDataReader reader, int offset)
		{
			cardNumber			= (long)GetValueLong(reader, offset);	offset++;
			cardYear			= (int)GetValueInt(reader, offset);		offset++;
			number				= (long)GetValueLong(reader, offset);	offset++;
			codeDetailStorage	= (long)GetValueLong(reader, offset);	offset++;
			codeDetail			= (long)GetValueLong(reader, offset);	offset++;
			quontity			= (float)GetValueFloat(reader, offset);	offset++;
			price				= (float)GetValueFloat(reader, offset);	offset++;
			nds					= (float)GetValueFloat(reader, offset);	offset++;
			guaranty			= (bool)GetValueBool(reader, offset);	offset++;
			outer				= (bool)GetValueBool(reader, offset);	offset++;
			check				= (bool)GetValueBool(reader, offset);	offset++;
			input				= (float)GetValueFloat(reader, offset);	offset++;
			code_reciver		= (long)GetValueLong(reader, offset);	offset++;
			code_return			= (long)GetValueLong(reader, offset);	offset++;

			tmpDetail			= new DbDetail(reader, offset);			offset = offset + DbDetail.ReaderLength;
			tmpDetailStorage	= new DbDetailStorage(reader, offset);	offset = offset + DbDetailStorage.ReaderLength;

			present				= (bool)GetValueBool(reader, offset);	offset++;
            to = (bool)GetValueBool(reader, offset); offset++;
            discount = (float)GetValueFloat(reader, offset); offset++;

			tmpDelete			= false;
			tmpExists			= true;
		}
		#endregion

		#region Основные методы
		public static bool WriteList(ListView list, DbCard card)
		{
			foreach(ListViewItem item in list.Items)
			{
				DbCardDetail element = (DbCardDetail)item.Tag;
				if(element != null)
				{
					element.Card	= card;
					element.Write();
				}
			}
			return true;
		}
		public bool Write()
		{
			if((!adding)&&(!changed)) return true; // Нет изменений

			try
			{
				cmdWrite.Parameters["@cardNumber"].Value		= (long)cardNumber;
				cmdWrite.Parameters["@cardYear"].Value			= (int)cardYear;
				cmdWrite.Parameters["@number"].Value			= (long)number;
				cmdWrite.Parameters["@codeDetail"].Value		= (long)codeDetail;
				cmdWrite.Parameters["@codeDetailStorage"].Value	= (long)codeDetailStorage;
				cmdWrite.Parameters["@quontity"].Value			= (float)quontity;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.Parameters["@nds"].Value				= (float)nds;
				cmdWrite.Parameters["@guaranty"].Value			= (bool)guaranty;
				cmdWrite.Parameters["@outer"].Value				= (bool)outer;
				cmdWrite.Parameters["@check"].Value				= (bool)check;
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@delete"].Value			= (bool)tmpDelete;
				cmdWrite.Parameters["@input"].Value				= (float)input;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				number		= (long)cmdWrite.Parameters["@number"].Value;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return false;
			}
			return true;
		}
		public bool WriteRecive(long codeReciver)
		{
			if(this.code_reciver > 0) return true;	// Деталь уже получена
			if(this.check == true) return true;		// Деталь по чеку
			if(this.outer == true) return true;		// Деталь клиента
			try
			{
				cmdWriteRecive.Parameters["@cardNumber"].Value		= (long)cardNumber;
				cmdWriteRecive.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdWriteRecive.Parameters["@number"].Value			= (long)number;
				cmdWriteRecive.Parameters["@code_reciver"].Value	= (long)codeReciver;
				cmdWriteRecive.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteRecive);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return false;
			}
			code_reciver	= codeReciver;		// Принято получение
			return true;
		}
		public bool WriteReturn(long codeReturner)
		{
			if(this.code_reciver == 0) return true; // Деталь не получена

			try
			{
				cmdWriteReturn.Parameters["@cardNumber"].Value		= (long)cardNumber;
				cmdWriteReturn.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdWriteReturn.Parameters["@number"].Value			= (long)number;
				cmdWriteReturn.Parameters["@code_returner"].Value	= (long)codeReturner;
				cmdWriteReturn.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteReturn);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return false;
			}
			code_return	= codeReturner;				// Принят возврат
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
				switch (viewType)
				{
					case 1:
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						break;
					default:
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						break;
				}
				SetLVItem(item);
				return item;	
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			switch (viewType)
			{
				case 1:
					//			item.Text =	codeDetail;
					//			item.SubItems[1].Text = tmpDetail.Name;
					//			item.SubItems[3].Text = QuontityTxt;
					break;
				default:
					item.SubItems[1].Text = CodeDetailTxt;
					item.SubItems[2].Text = DetailNameTxt;
                    if (discount > 0.0F) item.SubItems[2].Text += " / Скидка " + discount.ToString() + " %";
					item.SubItems[3].Text = QuontityTxt;// + "(" + IncomTxt + ")";
					item.SubItems[4].Text = PriceTxt;
					item.SubItems[5].Text = SummTxt;
					item.SubItems[6].Text = "$";
					if(guaranty) item.SubItems[6].Text = "Гарантия";
					if(present) item.SubItems[6].Text = "Подарок";
                    if (to) item.SubItems[6].Text += "/ТО";
					//if(guaranty)
					//	item.SubItems[6].Text = "Гарантия";
					//else
					//	item.SubItems[6].Text = "$";
					item.BackColor = Color.White;
					/*
					if(reserve == 1)
					{
						item.BackColor = Color.LightCoral;
					}
					if(reserve == 2)
					{
						item.BackColor = Color.LightGreen;
					}*/
					if(this.outer == true)
					{
						item.BackColor = Color.LightBlue;
					}
					if(this.check == true)
					{
						item.BackColor = Color.LightGreen;
					}
					if(!tmpExists)
					{
						item.BackColor = Color.Yellow;
					}
					if(tmpDelete)
					{
						item.BackColor = Color.Gray;
					}
					// Статусы полученных - неполученных деталей
					// Работают только для существующих деталей
					if(tmpExists == true && tmpDelete == false && this.check == false && this.outer == false)
					{
						if(this.code_reciver > 0)
							item.BackColor = Color.Green;
						else
							item.BackColor = Color.Red;
					}
					break;
			}
			item.Tag = this;
		}

		public static void FillList(ListView list, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value = card.Number;
			cmdSelect.Parameters["@cardYear"].Value = card.Year;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillListNotIncom(ListView list, DbCard card)
		{
			cmdExecSelectNotIncom.Parameters["@cardNumber"].Value = card.Number;
			cmdExecSelectNotIncom.Parameters["@cardYear"].Value = card.Year;
			Db.DbFillList(list, cmdExecSelectNotIncom, new DelegateInsertInList(InsertInListView1));
		}

		public static void FillListReciveReturn(ListView list, DbCard card, bool recive)
		{
			cmdSelectReciveReturn.Parameters["@cardNumber"].Value = card.Number;
			cmdSelectReciveReturn.Parameters["@cardYear"].Value = card.Year;
			cmdSelectReciveReturn.Parameters["@recive"].Value = (bool)recive;
			Db.DbFillList(list, cmdSelectReciveReturn, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ArrayList list, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value = card.Number;
			cmdSelect.Parameters["@cardYear"].Value = card.Year;
			FillList(list, cmdSelect);
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCardDetail element = new DbCardDetail(reader, 0);
			list.Items.Add(element.LVItem);
		}

		public static void InsertInListView1(SqlDataReader reader, ListView list)
		{
			DbCardDetail element = new DbCardDetail(reader, 0);
			element.SetViewType(1);
			list.Items.Add(element.LVItem);
		}

		public static void FillList(ArrayList list, SqlCommand cmd)
		{
			if(cmd == null) return;
			SqlDataReader reader = null;
			try
			{
				reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					DbCardDetail element = new DbCardDetail(reader, 0);
					list.Add(element);
				}
				reader.Close();
			}
			catch(Exception E)
			{
				if(reader != null) reader.Close();
				SetException(E);
			}
			if(reader != null) reader.Close();
			ShowFaults();
		}
		#endregion

		#region Отображение основных параметров в Тест
		public string IncomTxt
		{
			get
			{
				return incom.ToString();
			}
		}
		public string SummTxt
		{
			get
			{
                if (discount == 0.0F)
				    return Db.CachToTxt(price * quontity);
                if (Oil == true)
                    return Db.CachToTxt(price * quontity);
                return Db.CachToTxt((price - price / 100 * discount) * quontity);
			}
		}
		public string QuontityTxt
		{
			get
			{
				return quontity.ToString();
			}
		}

		public string DetailNameTxt
		{
			get
			{
				if(tmpDetailStorage == null || codeDetailStorage == 0)
				{
					if(tmpDetail == null) return "Ошибка";
					return tmpDetail.Name;
				}
				return tmpDetailStorage.DetailName;
			}
		}

		public string CodeDetailTxt
		{
			get
			{
				if(tmpDetailStorage == null || codeDetailStorage == 0)
				{
					if(tmpDetail == null) return "Ошибка";
					return tmpDetail.CodeTxt;
				}
				return tmpDetailStorage.DetailCodeTxt;
			}
		}

		public string PriceTxt
		{
			get
			{
				return Db.CachToTxt(price);
			}
		}
		public string InputTxt
		{
			get
			{
				return Db.CachToTxt(input);
			}
		}
		public string InputSummTxt
		{
			get
			{
				return Db.CachToTxt(input * quontity);
			}
		}
		#endregion

		#region Доступ к параметрам - Только чтение
		public long CodeDetail
		{
			get
			{
				return codeDetail;
			}
		}
		public long CodeDetailStorage
		{
			get
			{
				return codeDetailStorage;
			}
		}

		public bool Oil
		{
			get
			{
				if(tmpDetailStorage != null)
				{
					if(tmpDetailStorage.Oil == true) return true;
				}
				if(tmpDetail != null)
				{
					if(tmpDetail.Oil == true) return true;
				}
				return false;
			}
		}


		public DbDetail Detail
		{
			get
			{
				return tmpDetail;
			}
		}

		public long CardNumber
		{
			get {return cardNumber;}
		}
		public int CardYear
		{
			get {return cardYear;}
		}
		public long Number
		{
			get {return number;}
		}
		public bool Exists
		{
			get
			{
				return tmpExists;
			}
		}
		public bool Recived
		{
			get
			{
				if(code_reciver > 0)
					return true;
				else
					return false;
			}
		}
		public float Summ
		{
			get
			{
				if(guaranty == true) return 0;
				if(outer == true) return 0;
                if (Oil)
				    return price * quontity;
                if (discount == 0.0F)
                    return price * quontity;
                return (price - price / 100 * discount) * quontity;
			}
		}
		public float InputSumm
		{
			get
			{
				if(outer == true) return 0;
				return input * quontity;
			}
		}
		public float SummWhole
		{
			get
			{
				if(outer == true) return 0;
				return price * quontity;
			}
		}
		public float Price
		{
			set
			{
				price = SetFloatNotMinus(price, value, "Значение цены");
			}
			get
			{
				return price;
			}
	}
		#endregion

		#region Доступ к основным параметрам - Изменение
		public bool Outer
		{
			get
			{
				return outer;
			}
			set
			{
				if(code_reciver > 0) return;
				if(outer == value) return;
				outer = value;
				changed = true;
				if(outer == true)
				{
					guaranty = false;
					check	= false;
					price = 0.0F;
				}
			}
		}
		public bool Check
		{
			get
			{
				return check;
			}
			set
			{
				if(code_reciver > 0) return;
				if(check == value) return;
				check = value;
				changed = true;
				if(check == true)
				{
					guaranty = false;
					outer	= false;
				}
			}
		}
		public DbCard Card
		{
			set
			{
				if((cardNumber > 0)&&(cardYear > 0)) return;
				cardNumber		= value.Number;
				cardYear		= value.Year;
				changed			= true;
			}
		}
		public float Quontity
		{
			get
			{
				return quontity;
			}
			set
			{
				if(code_reciver > 0) return;
				if(value <= 0) return;
				if(quontity == value) return;
				quontity = value;
				changed = true;
			}
		}
		public bool Delete
		{
			set
			{
				if(code_reciver > 0) return;
				if(tmpDelete == value) return;
				tmpDelete = value;
				changed = true;
			}
			get
			{
				return tmpDelete;
			}
		}

		public int Reserve
		{
			set
			{
				reserve = value;
			}
		}

		public bool Guaranty
		{
			set
			{
				if(guaranty == value) return;
				guaranty = value;
				changed = true;
				// Пересчет цены
				if(guaranty == true)
				{
					if(this.Connect1C() == true)
					{
						this.price = tmpDetailStorage.Input;
					}
					outer = false;
					check = false;
				}
				else
				{
					if(this.Connect1C() == true)
						this.price = tmpDetailStorage.Price;
				}
			}
			get
			{
				return guaranty;
			}
		}

		public bool Present
		{
			set
			{
				present = value;
			}
			get
			{
				return present;
			}
		}

        public bool To
        {
            set
            {
                to = value;
            }
            get
            {
                return to;
            }
        }
        public float Discount
        {
            set
            {
                discount = value;
            }
            get
            {
                return discount;
            }
        }
		#endregion

		public bool Connect1C()
		{
			if(tmpDetailStorage.Code_1C > 0) return true;
			return false;
		}
	}
}
