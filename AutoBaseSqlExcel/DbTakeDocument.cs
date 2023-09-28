using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbTakeDocument.
	/// </summary>
	public class DbTakeDocument:Db
	{
		private long		code;
		private string		document;
		private string		number;
		private DateTime	date;
		private string		comment;
		private long		codePartner;
		private bool		closed;

		private DbPartner tmpPartner;

		private static SqlConnection conn;

		private static SqlCommand cmdSelect;
		private static SqlCommand cmdWrite;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbTakeDocument()
		{
			code				= 0;
			document			= "";
			number				= "";
			date				= DateTime.Now;
			comment				= "";
			codePartner			= 0;
			closed				= false;

			tmpPartner			= null;

			adding = true;
		}

		public DbTakeDocument(DbTakeDocument src)
		{
			code				= src.code;
			document			= src.document;
			number				= src.number;
			date				= src.date;
			comment				= src.comment;
			codePartner			= src.codePartner;
			closed				= src.closed;

			tmpPartner			= src.tmpPartner;

			adding = false;
		}

		public DbTakeDocument(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			document			= (string)GetValueString(reader, offset);	offset++;
			number				= (string)GetValueString(reader, offset);	offset++;
			date				= (DateTime)GetValueDate(reader, offset);	offset++;
			comment				= (string)GetValueString(reader, offset);	offset++;
			codePartner			= (long)GetValueLong(reader, offset);		offset++;
			closed				= (bool)GetValueBool(reader, offset);		offset++;
			
			tmpPartner			= new DbPartner(reader, offset);			offset = offset + DbPartner.ReaderLength;

			adding				= false;
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
			readerLength = 7 + DbPartner.ReaderLength;

			
			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПРИНЯТИЕ_ДОКУМЕНТ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@document", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@number", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codePartner", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@closed", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИНЯТИЕ_ДОКУМЕНТ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Отображение параметров в текст
		public string PartnerNameTxt
		{
			get
			{
				if(codePartner == 0) return "Не выбран";
				if(tmpPartner == null) return "Невозможно отобразить";
				return tmpPartner.Title;
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = Db.DateToTxt(date);
			item.SubItems[1].Text = number;
			item.SubItems[2].Text = document;
			item.SubItems[3].Text = PartnerNameTxt;
			item.SubItems[4].Text = comment;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbTakeDocument element = new DbTakeDocument(reader, 0);
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
				cmdWrite.Parameters["@document"].Value		= (string)document;
				cmdWrite.Parameters["@number"].Value		= (string)number;
				cmdWrite.Parameters["@date"].Value			= (DateTime)date;
				cmdWrite.Parameters["@comment"].Value		= (string)comment;
				cmdWrite.Parameters["@codePartner"].Value	= (long)codePartner;
				cmdWrite.Parameters["@closed"].Value		= (bool)closed;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			adding = false;		// Чтобы если документ ввелся, не вводился второй раз
			return true;
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
		public long CodePartner
		{
			get
			{
				return codePartner;
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
				number = this.SetStringNotEmptyLength(number, value, 64, "Номер документа");
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
		public DbPartner Partner
		{	
			set
			{
				if(value == null)
				{
					if(codePartner == 0) return;
					tmpPartner	= null;
					codePartner	= 0;
					changed		= true;
					return;
				}
				if(codePartner == value.Code) return;
				tmpPartner		= value;
				codePartner		= value.Code;
				changed			= true;
				return;
			}
			get
			{
				return tmpPartner;
			}
		}
		#endregion

		#region Проверки
		public void Valid()
		{
			if(codePartner == 0) Db.SetErrorMessage("Не выбран поставщик автомобилей");
		}
		#endregion
	}
}
