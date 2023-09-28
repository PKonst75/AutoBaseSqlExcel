using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Data;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardWork.
	/// </summary>
	public class DbCardWork:Db
	{
		// Основные параметры
		private long		cardNumber;
		private int			cardYear;
		private int			number;
		private long		codeWork;
		private float		val;
		private float		price;
		private float		quontity;
		private bool		guaranty;
		//private long		donePersonal;
		private bool		oil;
		private float		discount;
		
		private int			tmpDonePersonalQuontity;
		private long		tmpDonePersonal;
		private DbWork		tmpWork;
		private DbStaff		tmpDoneStaff;
		private bool		tmpExist;
		private bool		tmpDeleted;

		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectStaff;
		private static SqlCommand cmdSelectFind;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public class DataPair
        {
			DbCardWork dbCardWork;
			DtCardWork dtCardWork;
			public DataPair(DbCardWork wrk)
            {
				dbCardWork = wrk;
				dtCardWork = DbSqlCardWork.Find(wrk);
            }
		}

		#region Конструкторы
		public DbCardWork(DbWork work, DbCard card)
		{
			cardNumber		= card.Number;
			cardYear		= card.Year;
			number			= 0;
			codeWork		= work.Code;
			val				= work.Val;
			price			= work.Price;
			quontity		= 1.0F;
			guaranty		= false;
			//donePersonal	= 0;
			oil				= false;
			discount		= 0.0f;

			tmpDonePersonalQuontity	= 0;
			tmpDonePersonal			= 0;
			tmpWork					= work;
			tmpDoneStaff			= null;
			
			tmpExist		= false;
			tmpDeleted		= false;
			adding			= true;
		}

		public DbCardWork(DbWork work, DbCardWork cardWork)
		{
			cardNumber		= cardWork.cardNumber;
			cardYear		= cardWork.cardYear;
			number			= 0;
			codeWork		= work.Code;
			val				= work.Val;
			price			= work.Price;
			quontity		= 1.0F;
			guaranty		= false;
			//donePersonal	= 0;
			oil				= false;
			discount		= 0.0f;

			tmpDonePersonalQuontity = 0;
			tmpDonePersonal			= 0;
			tmpWork					= work;
			tmpDoneStaff			= null;
			
			tmpExist		= false;
			tmpDeleted		= false;
			adding			= true;
		}
		public DbCardWork(SqlDataReader reader, int offset)
		{
			cardNumber		= (long)GetValueLong(reader, offset);	offset++;
			cardYear		= (int)GetValueInt(reader, offset);		offset++;
			number			= (int)GetValueInt(reader, offset);		offset++;
			codeWork		= (long)GetValueLong(reader, offset);	offset++;
			quontity		= (float)GetValueFloat(reader, offset);	offset++;
			val				= (float)GetValueFloat(reader, offset);	offset++;
			price			= (float)GetValueFloat(reader, offset);	offset++;
			guaranty		= (bool)GetValueBool(reader, offset);	offset++;
			tmpDonePersonal	= (long)GetValueLong(reader, offset);	offset++;
			oil				= (bool)GetValueBool(reader, offset);	offset++;
			discount		= (float)GetValueFloat(reader, offset);	offset++;;

			tmpWork			= new DbWork(reader, offset);			offset = offset + DbWork.ReaderLength;
			tmpDoneStaff	= new DbStaff(reader, offset);			offset = offset + DbStaff.ReaderLength;
			tmpDonePersonalQuontity	= (int)GetValueInt(reader, offset);		offset++;
			
			tmpExist = true;
			tmpDeleted = false;
		}
		public DbCardWork(DtCardWork dtWork)
		{ 
			cardNumber = dtWork.CardNumber;
			cardYear = dtWork.CardYear;
			number = dtWork.Position;
			codeWork = dtWork.CodeWork;
			quontity = dtWork.OperationAmount;
			val = dtWork.LaborTime; // val = dtWork.Amount;
			price = dtWork.OperationCost;
			guaranty = dtWork.GuaranteeFlag();
			tmpDonePersonal = 0;// (long)GetValueLong(reader, offset); offset++;
			oil = dtWork.Oil;
			discount = dtWork.Discount;

			tmpWork = DbWork.Find(codeWork);//new DbWork(reader, offset); offset = offset + DbWork.ReaderLength;
			//tmpDoneStaff = new DbStaff(reader, offset); offset = offset + DbStaff.ReaderLength;
			//tmpDonePersonalQuontity = (int)GetValueInt(reader, offset); offset++;

			tmpExist = true;
			tmpDeleted = false;
		}
		#endregion

		#region Инициализация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 11 собственных полей и остальное
			readerLength = 11 + DbWork.ReaderLength + DbStaff.ReaderLength + 1;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КАРТОЧКА_ТРУДОЕМКОСТЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@number", SqlDbType.Int);
			cmdWrite.Parameters.Add("@codeWork", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@val", SqlDbType.Real);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@guaranty", SqlDbType.Bit);
			//cmdWrite.Parameters.Add("@donePersonal", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@quontity", SqlDbType.Float);
			cmdWrite.Parameters.Add("@oil", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@discount", SqlDbType.Real);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@deleting", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@number"].Direction=ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_КАРТОЧКА_ТРУДОЕМКОСТЬ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@cardYear", SqlDbType.Int);

			cmdSelectStaff = new SqlCommand("REPORT_ТРУДОЕМКОСТЬ_ПЕРСОНАЛ_ВЫПОЛНИЛ", conn);
			cmdSelectStaff.CommandType = CommandType.StoredProcedure;
			cmdSelectStaff.Parameters.Add("@codeStaff", SqlDbType.BigInt);
			cmdSelectStaff.Parameters.Add("@startDate", SqlDbType.DateTime);
			cmdSelectStaff.Parameters.Add("@endDate", SqlDbType.DateTime);
			cmdSelectStaff.CommandTimeout	= 600;

			cmdSelectFind = new SqlCommand("SELECT_КАРТОЧКА_ТРУДОЕМКОСТЬ_ПОИСК", conn);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
		public long CardNumber
		{
			get
			{
				return cardNumber;
			}
		}
		public int CardYear
		{
			get
			{
				return cardYear;
			}
		}
		public int Number
		{
			get
			{
				return number;
			}
		}
		public long CodeWork
		{
			get
			{
				return codeWork;
			}
		}
		public float Summ
		{
			get
			{
				if(guaranty) return 0.0F;
				if(val == 0.0F)
					//return (float)Math.Round(quontity * price, 2);
					return (float)Math.Round(quontity * price - quontity * price * discount / 100, 2);
				else
					//return (float)Math.Round(quontity * price * val, 2);
					return (float)Math.Round(quontity * price * val - quontity * price * val * discount / 100, 2);
			}
		}

		public float SummFull
		{
			get
			{
				if(val == 0.0F)
					return (float)Math.Round(quontity * price, 2);
				else
					return (float)Math.Round(quontity * price * val, 2);
			}
		}

		public int DonePersonalQuontity
		{
			get
			{
				return tmpDonePersonalQuontity;
			}
		}

		public bool Exist
		{
			get
			{
				return tmpExist;
			}
		}
		public string WorkName
		{
			get
			{
				if(tmpWork == null) return "Ошибка!";
				return tmpWork.Name;
			}
		}
		public string WorkPosition
		{
			get
			{
				if(tmpWork == null) return "Ошибка!";
				return tmpWork.Position;
			}
		}
		public float Val
		{
			get
			{
				return val;
			}
		}
		public long CodeDirectoryWork
		{
			get
			{
				return tmpWork.CodeDirectoryWork;
			}
		}
		#endregion

		#region Доступ к основным параметрам - Изменение
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

		public float Quontity
		{
			set
			{
				if(tmpDeleted == true) return;	// Работа помечена на удаление
				if(tmpDonePersonal > 0) return;	// Если работа выполнена, менять ее количество нельзя
				if(value <= 0) return;			// Меньше нуля количество работ быть не может
				if(value == quontity) return;	// Нет изменений
				quontity = value;
				changed = true;
			}
			get
			{
				return quontity;
			}
		}
		public float Price
		{
			set
			{
				if(tmpDeleted == true) return;	// Работа помечена на удаление
				if(value <= 0) return;			// Меньше нуля стоимость работ быть не может
				if(value == price) return;		// Нет изменений
				price = value;
				changed = true;
			}
			get
			{
				return price;
			}
		}
		public DbStaff DonePersonal
		{
			set
			{
				if(tmpDeleted == true) return;	// Работа помечена на удаление
				if(value == null)
				{
					if(tmpDonePersonal == 0) return;
					tmpDonePersonal	= 0;
					tmpDoneStaff	= null;
					return;
				}
				if(tmpDonePersonal == value.Code) return;
				tmpDonePersonal	= value.Code;
				tmpDoneStaff	= value;
			}
		}
		public bool Guaranty
		{
			set
			{
				if(tmpDeleted == true) return;	// Работа помечена на удаление
				if(guaranty == value) return;	// Нет изменений
				guaranty = value;
				changed = true;
				if(guaranty == true)
					price = tmpWork.PriceGuaranty;
				else
					price = tmpWork.Price;
			}
			get
			{
				return guaranty;
			}
		}
		public bool Oil
		{
			set
			{
				if(tmpDeleted == true) return;	// Работа помечена на удаление
				if(oil == value) return;		// Нет изменений
				oil = value;
				changed = true;
			}
			get
			{
				return oil;
			}
		}
		public bool Deleted
		{
			get
			{
				return tmpDeleted;
			}
			set
			{
				if(tmpDonePersonal > 0) return;		// Если работа выполнена, удалять нельзя
				tmpDeleted	= value;
				changed		= true;
			}
		}
		#endregion

		#region Отображение в текст основных параметров
		public string DonePersonalQuontityTxt
		{
			get
			{
				return tmpDonePersonalQuontity.ToString();
			}
		}
		public string DonePersonalTxt
		{
			get
			{
				if(tmpDonePersonal == 0) return "";
				if(tmpDoneStaff == null) return "";
				return tmpDoneStaff.Title;
			}
		}
		public string ValTxt
		{
			get
			{
				if (val == 0.0F) return "-";
				return val.ToString();
			}
		}
		public string GuarantyTxt
		{
			get
			{
				if (guaranty == true) return "Да";
				return "Нет";
			}
		}
		public string OilTxt
		{
			get
			{
				if (oil == true) return "Да";
				return "Нет";
			}
		}
		public string QuontityTxt
		{
			set
			{
				if(tmpDonePersonal > 0) return;
				quontity = this.SetFloatNotMinus(quontity, value, "");
			}
			get
			{
				return quontity.ToString();
			}
		}

		public string PriceTxt
		{
			get
			{
				if(guaranty) return "0-00";
				return Db.CachToTxt(price);
			}
		}

		public string PriceFullTxt
		{
			get
			{
				return Db.CachToTxt(price);
			}
		}

		public string SummFullTxt
		{
			get
			{
				float summ;
				System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
				format.CurrencyGroupSeparator = " ";
				format.CurrencyGroupSizes[0]= 3;
				format.CurrencyDecimalDigits = 2;
				format.CurrencySymbol = "";
				
				if(val == 0.0F)
					summ = price * quontity;
				else
					summ = price * quontity * val;
				return Db.CachToTxt(summ);
			}
		}

		public string SummTxt
		{
			get
			{
				float summ;
				if(guaranty) return "0-00";
				System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
				format.CurrencyGroupSeparator = " ";
				format.CurrencyGroupSizes[0]= 3;
				format.CurrencyDecimalDigits = 2;
				format.CurrencySymbol = "";
				
				if(val == 0.0F)
					summ = price * quontity;
				else
					summ = price * quontity * val;
				return Db.CachToTxt(summ);
			}
		}
		public string Name
		{
			get
			{
				return tmpWork.Name;
			}
		}
		#endregion

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				switch(viewType)
				{
					case 1:
						item.Text = "";
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						break;
					default:
						item.Text = "";
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
			float res;

			switch(viewType){
				case 1:
					item.ImageIndex = 1;
					item.UseItemStyleForSubItems = false;
					item.Text = (item.Index + 1).ToString();
					item.SubItems[1].Text = WorkPosition;
					item.SubItems[2].Text = WorkName;
                    if (this.discount != 0.0F) item.SubItems[2].Text += " / Скидка " + this.discount + "%";
					item.SubItems[3].Text = quontity.ToString();
					if(val == 0.0F)
					{
						item.SubItems[4].Text = Db.CachToTxt(price);
						res = (float)Math.Round(quontity * price, 2);
					}
					else
					{
						item.SubItems[4].Text = val.ToString() + " (" + price + ")";
						res = (float)Math.Round(val * quontity * price, 2);
					}
					item.SubItems[5].Text = Db.CachToTxt(res);
					item.ForeColor = Color.Black;
				
					if(this.guaranty == true)
					{
						item.ForeColor = Color.Red;
					}
					//item.BackColor = Color.Transparent;
					if(this.oil == true)
					{
						item.BackColor = Color.MediumOrchid;
					}
					if(this.tmpExist != true)
					{
						item.BackColor = Color.Yellow;
					}
					if(tmpDeleted)
					{
						item.BackColor = Color.DarkGray;
					}
					if(tmpDonePersonal>0)
					{
						item.BackColor = Color.LightSeaGreen;
					}
					item.SubItems[2].BackColor = Color.Red;
				break;
				default:
					item.Text = (item.Index + 1).ToString();
					item.SubItems[1].Text = WorkName;
                    if (this.discount != 0.0F) item.SubItems[1].Text += " / Скидка " + this.discount + "%";
					item.SubItems[2].Text = WorkPosition;
					item.SubItems[3].Text = quontity.ToString();
					if(val == 0.0F)
					{
						item.SubItems[4].Text = Db.CachToTxt(price);
						res = (float)Math.Round(quontity * price, 2);
					}
					else
					{
						item.SubItems[4].Text = val.ToString() + " (" + price + ")";
						res = (float)Math.Round(val * quontity * price, 2);
					}
					item.SubItems[5].Text = res.ToString();
					item.SubItems[6].Text = DonePersonalTxt;
					item.ForeColor = Color.Black;
				
					if(this.guaranty == true)
					{
						item.ForeColor = Color.Red;
					}
					item.BackColor = Color.Transparent;
					if(this.oil == true)
					{
						item.BackColor = Color.MediumOrchid;
					}
					if(this.tmpExist != true)
					{
						item.BackColor = Color.Yellow;
					}
					if(tmpDeleted)
					{
						item.BackColor = Color.DarkGray;
					}
					if(tmpDonePersonal>0)
					{
						item.BackColor = Color.LightSeaGreen;
					}
				break;
			}
			item.Tag = this;
		}
		public static void FillList(ListView list, SqlCommand cmd)
		{
			if(cmd == null) return;
			SqlDataReader reader = null;
			try
			{
				reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					DbCardWork work = new DbCardWork(reader, 0);
					list.Items.Add(work.LVItem);
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

		public static void FillList(ListView list, ArrayList dtWorksList)
		{
			foreach(DtCardWork dtWork in dtWorksList)
			{
				DbCardWork work = new DbCardWork(dtWork);
				list.Items.Add(work.LVItem);
			}
		}

		public static void FillList(ListView list, DbCard card)
		{

			cmdSelect.Parameters["@cardNumber"].Value	= card.Number;
			cmdSelect.Parameters["@cardYear"].Value		= card.Year;
			FillList(list, cmdSelect);
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
					DbCardWork work = new DbCardWork(reader, 0);
					list.Add(work);
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

		public static void FillList(ArrayList list, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value	= card.Number;
			cmdSelect.Parameters["@cardYear"].Value		= card.Year;
			FillList(list, cmdSelect);
		}
		public static void FillList(ArrayList list, DbStaff staff, DateTime startDate, DateTime endDate)
		{
			cmdSelectStaff.Parameters["@codeStaff"].Value		= staff.Code;
			cmdSelectStaff.Parameters["@startDate"].Value		= startDate;
			cmdSelectStaff.Parameters["@endDate"].Value			= endDate;
			FillList(list, cmdSelectStaff);
		}

		public static void FindWork(ArrayList array, DbWork work)
		{
			cmdSelectFind.Parameters["@code"].Value		= work.Code;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCardWork element = new DbCardWork(reader, 0);
			array.Add(element);
		}
		#endregion

		#region Основные методы
		public bool IsSame(DbWork work)
		{
			if(work == null) return false;
			if(this.codeWork != work.Code) return false;
			return true;
		}

		public bool IsSame(DbCardWork work)
		{
			if(work == null) return false;
			if(this.codeWork != work.CodeWork) return false;
			return true;
		}

		public void AddToList(ListView list, bool plus)
		{
			// Добавление с поиском по листу
			foreach(ListViewItem item in list.Items)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if(this.IsSame(work))
				{
					if(plus) list.Items.Add(this.LVItem);
					return;
				}
			}
			// Всегда добавляем новую работу
			// Не нашли - добавляем новый
			list.Items.Add(this.LVItem);
			// Проверяем на необходимость добавления связанных работ
			ArrayList listArray = new ArrayList();
			this.tmpWork.FillListArray(listArray);	// Добавляем все всязанные работы	
			foreach(Object o in listArray)
			{
				DbWork wrk = (DbWork)o;
				DbCardWork connWrk = new DbCardWork(wrk, this);
				connWrk.AddToList(list, false);
			}
		}

		public bool Write()
		{
			if((adding == false)&&(changed == false)) return true; // Изменений нет

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@deleting"].Value		= (bool)tmpDeleted;
				cmdWrite.Parameters["@cardNumber"].Value	= (long)cardNumber;
				cmdWrite.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdWrite.Parameters["@number"].Value		= (int)number;
				cmdWrite.Parameters["@codeWork"].Value		= (long)codeWork;
				cmdWrite.Parameters["@val"].Value			= (float)val;
				cmdWrite.Parameters["@price"].Value			= (float)price;
				cmdWrite.Parameters["@guaranty"].Value		= (bool)guaranty;
				cmdWrite.Parameters["@oil"].Value			= (bool)oil;
				//cmdWrite.Parameters["@donePersonal"].Value	= (long)tmpDonePersonal;
				cmdWrite.Parameters["@quontity"].Value		= (float)quontity;
				cmdWrite.Parameters["@discount"].Value		= (float)discount;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}

		public static bool WriteList(ListView list, DbCard card)
		{
			foreach(ListViewItem item in list.Items)
			{
				DbCardWork element = (DbCardWork)item.Tag;
				if(element != null)
				{
					element.Card	= card;
					element.Write();
				}
			}
			return true;
		}
		#endregion

		#region Определение виртуальных методов
		public override string[] Inform(int infoLevel)
		{
			string[] infoStrings	= null;

			switch (infoLevel)
			{
				default:
					infoStrings = new string[11];
					infoStrings[0] = "Карточка :\t\t" + "№ " + this.CardNumber + " / " + this.CardYear;
					infoStrings[1] = "Порядковый номер :\t" + this.Number;
					infoStrings[2] = "Работа :\t\t\t" + this.tmpWork.Name;
					infoStrings[3] = "\t\t\tНормачас :\t\t" + this.tmpWork.ValTxt;
					infoStrings[4] = "\t\t\tЦена :\t\t\t" + this.tmpWork.PriceTxt;
					infoStrings[5] = "\t\t\tЦена гарантии :\t\t" + this.tmpWork.PriceGuarantyTxt;
					infoStrings[6] = "Нормачас :\t\t" + this.ValTxt;
					infoStrings[7] = "Цена :\t\t\t" + this.PriceFullTxt;
					infoStrings[8] = "Количество :\t\t" + this.QuontityTxt;
					infoStrings[9] = "Гарантия :\t\t" + this.GuarantyTxt;
					infoStrings[10] = "Масло :\t\t\t" + this.OilTxt;
					break;
			}
			return infoStrings;
		}
		#endregion
	}

}
