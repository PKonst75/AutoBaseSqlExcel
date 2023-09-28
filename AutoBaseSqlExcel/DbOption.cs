using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс определяющий список возможного доп. оборудования
	/// </summary>
	public class DbOption:Db
	{
		// Основные характеристики
		private long code;				// Код опции
		private string name;			// Описание опции
		private float price;			// Цена по прайсу
		private float valuePrice;		// стоимость
		private bool removed;			// Помечено на удаление (не используется больше)

		// Связь с базой данных
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
			// 5 собственных поля и остальное
			readerLength = 5;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ДОП_ОБОРУДОВАНИЕ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@valuePrice", SqlDbType.Real);
			cmdWrite.Parameters.Add("@removed", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_ДОП_ОБОРУДОВАНИЕ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Конструкторы
		public DbOption()
		{
			code				= 0;
			name				= "";
			price				= 0.0F;
			valuePrice			= 0.0F;
			removed				= false;

			adding				= true;
		}
		public DbOption(DbOption src)
		{
			code				= src.code;
			name				= src.name;
			price				= src.price;
			valuePrice			= src.valuePrice;
			removed				= src.removed;
		}
		public DbOption(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);		offset++;
			name			= (string)GetValueString(reader, offset);	offset++;
			price			= (float)GetValueFloat(reader, offset);		offset++;
			valuePrice		= (float)GetValueFloat(reader, offset);		offset++;
			removed			= (bool)GetValueBool(reader, offset);		offset++;
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
				cmdWrite.Parameters["@name"].Value				= (string)name;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.Parameters["@valuePrice"].Value		= (float)valuePrice;
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
			return true;
		}
		#endregion

		#region Доступ к основнм параметрам
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = SetStringNotEmptyLength(name, value, 256, "Описание доп. оборудования");
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
			item.Text = name;
			item.SubItems[1].Text = Db.CachToTxt(price);
			item.SubItems[2].Text = Db.CachToTxt(valuePrice);

			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbOption element = new DbOption(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbOption element = new DbOption(reader, 0);
			array.Add(element);
		}
		#endregion

		#region Отображение в текст параметров
		#endregion

		#region Опасные системные функции
		#endregion

		#region Переопределенные виртуальные методы
		override public string DbTitle()
		{
			return this.Name;
		}
		#endregion
	}
}
