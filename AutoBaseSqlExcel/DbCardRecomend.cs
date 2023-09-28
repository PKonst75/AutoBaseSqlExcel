using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Рекомендации по дополнительному обслуживанию автомобиля
	/// </summary>
	public class DbCardRecomend:Db
	{
		private long	cardNumber;
		private int		cardYear;
		private long	number;
		private string	recomendation;

		private bool tmpDelete;
		private bool tmpExists;

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
			// 4 собственных поля
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КАРТОЧКА_РЕКОМЕНДАЦИЯ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@recomendation", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@number"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_КАРТОЧКА_РЕКОМЕНДАЦИЯ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@cardYear", SqlDbType.Int);
		}
		#endregion

		#region Конструкторы
		public DbCardRecomend(DbCard sourceCard, string text)
		{
			cardNumber			= sourceCard.Number;
			cardYear			= sourceCard.Year;
			number				= 0;
			recomendation		= text;

			adding				= true;
			tmpExists			= false;
			tmpDelete			= false;
		}
		public DbCardRecomend(SqlDataReader reader, int offset)
		{
			cardNumber			= (long)GetValueLong(reader, offset);		offset++;
			cardYear			= (int)GetValueInt(reader, offset);			offset++;
			number				= (long)GetValueLong(reader, offset);		offset++;
			recomendation		= (string)GetValueString(reader, offset);	offset++;

			tmpDelete			= false;
			tmpExists			= true;
		}
		#endregion

		#region Основные методы
		public static bool CheckText(string text)
		{
			if(text.Length == 0) return false;
			if(text.Length > 128) return false;
			return true;
		}
		public static bool WriteList(ListView list, DbCard card)
		{
			foreach(ListViewItem item in list.Items)
			{
				DbCardRecomend element = (DbCardRecomend)item.Tag;
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
				cmdWrite.Parameters["@recomendation"].Value		= (string)recomendation;
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@delete"].Value			= (bool)tmpDelete;
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
		public string Recomendation
		{
			get
			{
				return recomendation;
			}
			set
			{
				recomendation = SetStringNotEmptyLength(recomendation, value, 128, "Рекомендация");
			}
		}
		public bool Delete
		{
			set
			{
				if(tmpDelete == value) return;
				tmpDelete = value;
				changed = true;
			}
			get
			{
				return tmpDelete;
			}
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
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
		#endregion

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				SetLVItem(item);
				return item;	
			}
		}

		public void SetLVItem(ListViewItem item)
		{	
			item.Text =	recomendation;	
			if(!tmpExists)
			{
				item.BackColor = Color.Yellow;
			}
			if(tmpDelete)
			{
				item.BackColor = Color.Gray;
			}
			item.Tag = this;
		}

		public static void FillList(ListView list, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value = card.Number;
			cmdSelect.Parameters["@cardYear"].Value = card.Year;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCardRecomend element = new DbCardRecomend(reader, 0);
			list.Items.Add(element.LVItem);
		}

		public static void FillArray(ArrayList list, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value = card.Number;
			cmdSelect.Parameters["@cardYear"].Value = card.Year;
			FillArray(list, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCardRecomend element = new DbCardRecomend(reader, 0);
			array.Add(element);
		}
		#endregion
	}
}
