using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Доп оборудование установленное на автомобиль.
	/// </summary>
	public class DbOptionAuto:Db
	{
		// Основные характеристики
		private long code;				// Код
		private long codeAuto;			// Код автомобиля
		private long codeOption;		// Код опции
		private float price;			// Цена по прайсу
		private float valuePrice;		// Стоимость
		private string comment;			// Стоимость
		private long cardNumber;		// Номер карточки (по которой проводилась установка)
		private int cardYear;			// Год карточки
		private bool removed;			// Снято с автомобиля после установки

		private DbOption tmpOption;		// Опция

		// Связь с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdWriteRemove;
		private static SqlCommand cmdWriteCard;
		private static SqlCommand cmdSelectAuto;
		private static SqlCommand cmdSelectAutoNotRemoved;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 9 собственных полей + остальное
			readerLength = 9 + DbOption.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ДОП_ОБОРУДОВАНИЕ_АВТОМОБИЛЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeOption", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@valuePrice", SqlDbType.Real);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@removed", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteRemove = new SqlCommand("WRITE_ДОП_ОБОРУДОВАНИЕ_АВТОМОБИЛЬ_СНЯТЬ", conn);
			cmdWriteRemove.CommandType = CommandType.StoredProcedure;
			cmdWriteRemove.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteRemove.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWriteRemove.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWriteRemove.Parameters["@cardNumber"].Direction = ParameterDirection.InputOutput;
			cmdWriteRemove.Parameters["@cardYear"].Direction = ParameterDirection.InputOutput;
			Db.SetReturnError(cmdWriteRemove);

			cmdWriteCard = new SqlCommand("WRITE_ДОП_ОБОРУДОВАНИЕ_АВТОМОБИЛЬ_КАРТОЧКА", conn);
			cmdWriteCard.CommandType = CommandType.StoredProcedure;
			cmdWriteCard.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteCard.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWriteCard.Parameters.Add("@cardYear", SqlDbType.Int);
			Db.SetReturnError(cmdWriteCard);

			cmdSelectAuto = new SqlCommand("SELECT_ДОП_ОБОРУДОВАНИЕ_АВТОМОБИЛЬ_АВТОМОБИЛЬ", conn);
			cmdSelectAuto.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdSelectAuto.CommandType = CommandType.StoredProcedure;

			cmdSelectAutoNotRemoved = new SqlCommand("SELECT_ДОП_ОБОРУДОВАНИЕ_АВТОМОБИЛЬ_АВТОМОБИЛЬ_НЕСНЯТОЕ", conn);
			cmdSelectAutoNotRemoved.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdSelectAutoNotRemoved.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Конструкторы
		public DbOptionAuto(DbOptionAuto src)
		{
			code				= src.code;
			codeAuto			= src.codeAuto;
			codeOption			= src.codeOption;
			price				= src.price;
			valuePrice			= src.valuePrice;
			comment				= src.comment;
			cardNumber			= src.cardNumber;
			cardYear			= src.cardYear;
			removed				= src.removed;

			tmpOption			= src.tmpOption;

			adding				= false;
		}
		public DbOptionAuto(DbAuto auto, DbOption option)
		{
			code				= 0;
			codeAuto			= auto.Code;
			codeOption			= option.Code;
			price				= option.Price;
			valuePrice			= option.ValuePrice;
			comment				= "";
			cardNumber			= 0;
			cardYear			= 0;
			removed				= false;

			tmpOption			= option;

			adding				= true;
		}
		public DbOptionAuto(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);		offset++;
			codeAuto		= (long)GetValueLong(reader, offset);		offset++;
			codeOption		= (long)GetValueLong(reader, offset);		offset++;
			price			= (float)GetValueFloat(reader, offset);		offset++;
			valuePrice		= (float)GetValueFloat(reader, offset);		offset++;
			comment			= (string)GetValueString(reader, offset);	offset++;
			cardNumber		= (long)GetValueLong(reader, offset);		offset++;
			cardYear		= (int)GetValueInt(reader, offset);			offset++;
			removed			= (bool)GetValueBool(reader, offset);		offset++;

			tmpOption		= new DbOption(reader, offset);				offset += DbOption.ReaderLength;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			if((adding == false)&&(changed==false)) return true;
			try
			{
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeAuto"].Value			= (long)codeAuto;
				cmdWrite.Parameters["@codeOption"].Value		= (long)codeOption;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.Parameters["@valuePrice"].Value		= (float)valuePrice;
				cmdWrite.Parameters["@comment"].Value			= (string)comment;
				cmdWrite.Parameters["@cardNumber"].Value		= (long)cardNumber;
				cmdWrite.Parameters["@cardYear"].Value			= (long)cardYear;
				cmdWrite.Parameters["@removed"].Value			= (bool)removed;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				Db.ShowFaults();
				return false;
			}
			adding = false;
			return true;
		}
		public bool WriteRemove()
		{
			try
			{
				cmdWriteRemove.Parameters["@code"].Value		= (long)code;
				cmdWriteRemove.Parameters["@cardNumber"].Value	= (long)cardNumber;
				cmdWriteRemove.Parameters["@cardYear"].Value	= (int)cardYear;
				cmdWriteRemove.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteRemove);
				cardNumber	= (long)cmdWriteRemove.Parameters["@cardNumber"].Value;
				cardYear	= (int)cmdWriteRemove.Parameters["@cardYear"].Value;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				Db.ShowFaults();
				return false;
			}
			removed = true;
			return true;
		}
		public bool WriteCard(DbCard card)
		{
			try
			{
				cmdWriteCard.Parameters["@code"].Value			= (long)code;
				cmdWriteCard.Parameters["@cardNumber"].Value	= (long)card.Number;
				cmdWriteCard.Parameters["@cardYear"].Value		= (int)card.Year;
				cmdWriteCard.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteCard);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				Db.ShowFaults();
				return false;
			}
			cardNumber		= card.Number;
			cardYear		= card.Year;
			return true;
		}
		#endregion

		#region Доступ к основнм параметрам
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
		public float Price
		{
			get
			{
				return price;
			}
			set
			{
				if(price == value) return;
				if(value < 0.0) return;
				price = value;
				changed = true;
			}
		}
		public float ValuePrice
		{
			get
			{
				return valuePrice;
			}
			set
			{
				if(valuePrice == value) return;
				if(value < 0.0) return;
				valuePrice = value;
				changed = true;
			}
		}
		public string PriceTxt
		{
			set
			{
				price = this.SetFloatNotMinus(price, value, "Цена по прайсу");
			}
			get
			{
				return Db.CachToTxt(price);
			}
		}
		public string ValuePriceTxt
		{
			set
			{
				valuePrice = this.SetFloatNotMinus(valuePrice, value, "Стоимость");
			}
			get
			{
				return Db.CachToTxt(valuePrice);
			}
		}
		public long Code
		{
			get
			{
				return code;
			}
		}
		public bool Removed
		{
			get
			{
				return removed;
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.UseItemStyleForSubItems = false;
			item.Text = NameTxt;
			item.SubItems[1].Text = Db.CachToTxt(price);
			item.SubItems[2].Text = comment;

			item.BackColor = Color.White;
			item.SubItems[1].BackColor = Color.White;
			if(cardNumber == 0) item.BackColor = Color.Yellow;
			if(price <= valuePrice) item.SubItems[1].BackColor = Color.Red;
			if(removed == true)
			{
				item.BackColor = Color.Gray;
				item.SubItems[1].BackColor = Color.Gray;
			}

			item.Tag = this;
		}

		public static void FillList(ListView list, DbAuto auto)
		{
			cmdSelectAuto.Parameters["@codeAuto"].Value = (long)auto.Code;
			Db.DbFillList(list, cmdSelectAuto, new DelegateInsertInList(InsertInList));
		}

		public static void FillListNotRemoved(ListView list, DbAuto auto)
		{
			cmdSelectAutoNotRemoved.Parameters["@codeAuto"].Value = (long)auto.Code;
			Db.DbFillList(list, cmdSelectAutoNotRemoved, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbOptionAuto element = new DbOptionAuto(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array, DbAuto auto)
		{
			cmdSelectAuto.Parameters["@codeAuto"].Value = (long)auto.Code;
			Db.FillArray(array, cmdSelectAuto, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayNotRemoved(ArrayList array, DbAuto auto)
		{
			cmdSelectAutoNotRemoved.Parameters["@codeAuto"].Value = (long)auto.Code;
			Db.FillArray(array, cmdSelectAutoNotRemoved, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbOptionAuto element = new DbOptionAuto(reader, 0);
			array.Add(element);
		}
		#endregion

		#region Отображение в текст параметров
		public string NameTxt
		{
			get
			{
				if(codeOption == 0) return "Нет опции";
				if(tmpOption == null) return "Не отображается";
				return tmpOption.Name;
			}
		}
		#endregion

		#region Опасные системные функции
		#endregion

		#region Переопределенные виртуальные методы
		#endregion
	}
}
