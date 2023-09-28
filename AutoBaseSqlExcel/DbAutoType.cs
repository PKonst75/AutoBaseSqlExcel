using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Группы типов автомобилей (для применения к трудоемкостям)
	/// </summary>
	public class DbAutoType : Db
	{
		// Взаимодействвие с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdFind;
		private static SqlCommand cmdSystemReplace;
		private static SqlCommand cmdSystemRemove;

		// Основные данные
		private long code;
		private string name;
		private float price;
		private float priceGuaranty;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		/* Конструктор */
		public DbAutoType()
		{
			code = 0;
			name = "";
			price = 0.0F;
			priceGuaranty = 0.0F;
			adding = true;
		}

		/* Конструктор копирования*/
		public DbAutoType(DbAutoType autoType)
		{
			code			= autoType.code;
			name			= autoType.name;
			price			= autoType.price;
			priceGuaranty	= autoType.priceGuaranty;
		}

		public DbAutoType(SqlDataReader reader, int offset)
		{		
			code			= (long)GetValueLong(reader, offset);		offset++;
			name			= (string)GetValueString(reader,offset);	offset++;
			price			= (float)GetValueFloat(reader, offset);		offset++;
			priceGuaranty	= (float)GetValueFloat(reader, offset);		offset++;
		}


		/* Установка транзакции для всех комманд */
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		/* Инициализация всех комманд */
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 4 собственных поля и остальное
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ТИП", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@priceGuaranty", SqlDbType.Real);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ТИП", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSystemReplace = new SqlCommand("SYSTEM_АВТОМОБИЛЬ_ТИП_ЗАМЕНИТЬ", conn);
			cmdSystemReplace.CommandType = CommandType.StoredProcedure;
			cmdSystemReplace.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSystemReplace.Parameters.Add("@codeOld", SqlDbType.BigInt);
			Db.SetReturnError(cmdSystemReplace);

			cmdSystemRemove = new SqlCommand("SYSTEM_АВТОМОБИЛЬ_ТИП_УДАЛИТЬ", conn);
			cmdSystemRemove.CommandType = CommandType.StoredProcedure;
			cmdSystemRemove.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdSystemRemove);

			cmdFind = new SqlCommand("SELECT_АВТОМОБИЛЬ_ТИП_ПОИСК", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);
		}

		// Свойства класса
		public float Price
		{
			get
			{
				return price;
			}
			set
			{
				price = this.SetFloatNotMinus(price, value, "Стоимость нормачаса");
			}
		}

		public string PriceTxt
		{
			get
			{
				return price.ToString();
			}
			set
			{
				price = this.SetFloatNotMinus(price, value, "Стоимость нормачаса");
			}
		}

		public float PriceGuaranty
		{
			get
			{
				return priceGuaranty;
			}
			set
			{
				priceGuaranty = this.SetFloatNotMinus(priceGuaranty, value, "Стоимость гарантийного нормачаса");
			}
		}

		public string PriceGuarantyTxt
		{
			get
			{
				return priceGuaranty.ToString();
			}
			set
			{
				priceGuaranty = this.SetFloatNotMinus(priceGuaranty, value, "Стоимость гарантийного нормачаса");
			}
		}
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = SetStringNotEmptyLength(name, value, 120, "Наименование");
			}
		}

		public long Code
		{
			get
			{
				return code;
			}
			set
			{
				code = value;
			}
		}

		public string NameTxt
		{
			get
			{
				if(code != 0)
					return name;
				else
					return "Необходимо выбрать группу автомобиля";
			}
		}

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = name;
			item.SubItems[1].Text = price.ToString() + "/" + priceGuaranty.ToString();
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}


		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoType element = new DbAutoType(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbAutoType element = new DbAutoType(reader, 0);
			array.Add(element);
		}
		#endregion

		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value = (bool)adding;
				cmdWrite.Parameters["@code"].Value = (long)code;
				cmdWrite.Parameters["@name"].Value = (string)name;
				cmdWrite.Parameters["@price"].Value = (float)price;
				cmdWrite.Parameters["@priceGuaranty"].Value = (float)priceGuaranty;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				trans.Rollback();
				SetTransaction(null);
				SetException(E);
				ShowFaults();
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("Группа автомобиля добавлена/изменена");
			return true;
		}

		override public string  DbTitle()
		{
			return this.Name;
		}

		public bool Replace(DbAutoType autoType)
		{
			if(Db.CheckSysPass() == false) return false;
			if(autoType == null) return false;
			string text = "Вы уверены что хотите заменить " + this.NameTxt + " на " + autoType.NameTxt + "?";
			DialogResult res = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSystemReplace.Parameters["@codeOld"].Value = (long)code;
			cmdSystemReplace.Parameters["@code"].Value = (long)autoType.Code;
			return Db.ExecuteCommandError(cmdSystemReplace);
		}

		public bool Remove()
		{
			if(Db.CheckSysPass() == false) return false;
			string text = "Вы уверены что хотите удалить " + this.NameTxt;
			DialogResult res = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSystemRemove.Parameters["@code"].Value = this.Code;
			return Db.ExecuteCommandError(cmdSystemRemove);
		}

		public static DbAutoType Find(long code)
		{
			ArrayList array =  new ArrayList();
			FillArrayFind(array, code);
			if(array.Count == 0) return null;
			return (DbAutoType)array[0];
		}
		public static void FillArrayFind(ArrayList array, long code)
		{
			cmdFind.Parameters["@code"].Value = (long) code;
			Db.FillArray(array, cmdFind, new DelegateInsertInArray(InsertInArray));
		}
	}
}
