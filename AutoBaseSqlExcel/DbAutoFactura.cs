using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAutoFactura.
	/// </summary>
	public class DbAutoFactura:Db
	{
		private long		code;
		private long		codeSeller;
		private long		codeBuyer;
		private string		document;
		private string		number;
		private DateTime	date;
		private string		comment;

		private DbPartner tmpPartnerSeller;
		private DbPartner tmpPartnerBuyer;

		private static SqlConnection conn;

		private static SqlCommand cmdSelect;
		private static SqlCommand cmdWrite;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbAutoFactura()
		{
			code				= 0;
			codeSeller			= 0;
			codeBuyer			= 0;
			document			= "";
			number				= "";
			date				= DateTime.Now;
			comment				= "";

			tmpPartnerSeller	= null;
			tmpPartnerBuyer		= null;

			adding = true;
		}

		public DbAutoFactura(DbAutoFactura src)
		{
			code				= src.code;
			codeSeller			= src.codeSeller;
			codeBuyer			= src.CodeBuyer;
			document			= src.document;
			number				= src.number;
			date				= src.date;
			comment				= src.comment;

			tmpPartnerSeller	= src.tmpPartnerSeller;
			tmpPartnerBuyer		= src.tmpPartnerBuyer;

			adding = false;
		}

		public DbAutoFactura(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeSeller			= (long)GetValueLong(reader, offset);		offset++;
			codeBuyer			= (long)GetValueLong(reader, offset);		offset++;
			document			= (string)GetValueString(reader, offset);	offset++;
			number				= (string)GetValueString(reader, offset);	offset++;
			date				= (DateTime)GetValueDate(reader, offset);	offset++;
			comment				= (string)GetValueString(reader, offset);	offset++;
			
			tmpPartnerSeller	= new DbPartner(reader, offset);				offset = offset + DbPartner.ReaderLength;
			tmpPartnerBuyer		= new DbPartner(reader, offset);				offset = offset + DbPartner.ReaderLength;
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
			// 7 собственных полей и остальное
			readerLength = 7 + DbPartner.ReaderLength + DbPartner.ReaderLength;

			
			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПРИХОД_ДОКУМЕНТ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeSeller", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeByer", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@document", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@number", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИХОД_ДОКУМЕНТ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Отображение параметров в текст
		public string BuyerNameTxt
		{
			get
			{
				if(tmpPartnerBuyer == null) return "";
				return tmpPartnerBuyer.Title;
			}
		}
		public string SellerNameTxt
		{
			get
			{
				if(tmpPartnerSeller == null) return "";
				return tmpPartnerSeller.Title;
			}
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = document;
			item.SubItems[1].Text = number;
			item.SubItems[2].Text = Db.DateToTxt(date);
			item.SubItems[3].Text = SellerNameTxt;
			item.SubItems[4].Text = BuyerNameTxt;
			item.SubItems[5].Text = comment;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoFactura element = new DbAutoFactura(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			if (adding==false && changed==false) return true;
			try
			{
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@codeSeller"].Value	= (long)codeSeller;
				cmdWrite.Parameters["@codeByer"].Value		= (long)codeBuyer;
				cmdWrite.Parameters["@document"].Value		= (string)document;
				cmdWrite.Parameters["@number"].Value		= (string)number;
				cmdWrite.Parameters["@date"].Value			= (DateTime)date;
				cmdWrite.Parameters["@comment"].Value		= (string)comment;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
		public long CodeSeller
		{
			get
			{
				return codeSeller;
			}
		}

		public long CodeBuyer
		{
			get
			{
				return codeBuyer;
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Доступ к основным параметрам - Изменение
		public string Document
		{
			get
			{
				return document;
			}
			set
			{
				document = SetStringNotEmptyLength(document, value, 128, "Наименование документа");
			}
		}
		public string Number
		{
			set
			{
				number = this.SetStringNotEmptyLength(number, value, 25, "Номер документа");
			}
			get
			{
				return number;
			}
		}
		public string Comment
		{
			set
			{
				comment = this.SetStringLength(comment, value, 256, "Комментарий");
			}
			get
			{
				return comment;
			}

		}
		public DateTime Date
		{
			set
			{
				DateTime noTime = value;
				noTime = new DateTime(noTime.Year, noTime.Month, noTime.Day, 0, 0, 0, 0);
				if(date == noTime) return;
				date = noTime;
				changed	= true;
			}
			get
			{
				return date;
			}
		}
		public DbPartner PartnerSeller
		{	
			set
			{
				if(value == null)
				{
					if(codeSeller == 0) return;
					tmpPartnerSeller	= null;
					codeSeller	= 0;
					changed				= true;
					return;
				}
				if(codeSeller == value.Code) return;
				tmpPartnerSeller	= value;
				codeSeller			= value.Code;
				changed				= true;
				return;
			}
			get
			{
				return tmpPartnerSeller;
			}
		}
		public DbPartner PartnerByer
		{
			set
			{
				if(value == null)
				{
					if(codeBuyer == 0) return;
					tmpPartnerBuyer	= null;
					codeBuyer	= 0;
					changed			= true;
					return;
				}
				if(codeBuyer == value.Code) return;
				tmpPartnerBuyer	= value;
				codeBuyer	= value.Code;
				changed			= true;
				return;
			}
			get
			{
				return tmpPartnerBuyer;
			}
		}
		#endregion

		#region Проверки
		public void Valid()
		{
			if(codeBuyer == 0) Db.SetErrorMessage("Не выбран получатель автомобилей");
			if(codeSeller == 0) Db.SetErrorMessage("Не выбран поставщик автомобилей");
		}
		#endregion
	}
}
