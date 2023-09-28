using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Описание таблицы общего справочника трудоемкостей
	/// </summary>
	public class DbDirectoryWork:Db
	{
		private	long		code;
		private string		name;
		private bool		groupFlag;
		private bool		discount;
		private long		codeCategorySearch;
		private long		codeCategoryPrice;

		private DbCategorySearch	tmpCategorySearch;
		private DbCategoryPrice		tmpCategoryPrice;

		// Дополнительные параметры для других видов прочтения
		private DbWork		tmpWork;
		private ArrayList	tmpWorkArray;
		private int			tmpLevel;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static SqlConnection	conn;
		private static SqlCommand	cmdSelect;
		private static SqlCommand	cmdWrite;
		private static SqlCommand	cmdSysDelete;
		private static SqlCommand	cmdSelectWork;

		#region Конструкторы
		public DbDirectoryWork()
		{
			code				= 0;
			name				= "";
			groupFlag			= false;
			discount			= true;
			codeCategorySearch	= 0;
			codeCategoryPrice	= 0;

			tmpCategorySearch	= null;
			tmpCategoryPrice	= null;
			tmpWork				= null;
			tmpWorkArray		= null;
			tmpLevel			= 0;
	
			adding		= true;
		}
		public DbDirectoryWork(DbDirectoryWork src)
		{
			code				= src.code;
			name				= src.name;
			groupFlag			= src.groupFlag;
			discount			= src.discount;
			codeCategorySearch	= src.codeCategorySearch;
			codeCategoryPrice	= src.codeCategoryPrice;

			tmpCategorySearch	= src.tmpCategorySearch;
			tmpCategoryPrice	= src.tmpCategoryPrice;
			tmpWork				= src.tmpWork;
			tmpWorkArray		= src.tmpWorkArray;
			tmpLevel			= 0;

			adding		= false;
		}
		public DbDirectoryWork(DbDirectoryWork src, int level)
		{
			code				= src.code;
			name				= src.name;
			groupFlag			= src.groupFlag;
			discount			= src.discount;
			codeCategorySearch	= src.codeCategorySearch;
			codeCategoryPrice	= src.codeCategoryPrice;

			tmpCategorySearch	= src.tmpCategorySearch;
			tmpCategoryPrice	= src.tmpCategoryPrice;
			tmpWork				= src.tmpWork;
			tmpWorkArray		= src.tmpWorkArray;

			tmpLevel			= level;
			viewType			= 2;

			adding		= false;
		}
		public DbDirectoryWork(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			name				= (string)GetValueString(reader, offset);	offset++;
			groupFlag			= (bool)GetValueBool(reader, offset);		offset++;
			discount			= (bool)GetValueBool(reader, offset);		offset++;
			codeCategorySearch	= (long)GetValueLong(reader, offset);		offset++;
			codeCategoryPrice	= (long)GetValueLong(reader, offset);		offset++;

			tmpCategorySearch	= null;
			tmpCategoryPrice	= null;
			tmpWork				= null;
			tmpWorkArray		= null;
			tmpLevel			= 0;

			adding		= false;
		}
		public DbDirectoryWork(SqlDataReader reader, int offset, int type)
		{
			switch(type)
			{
				case 1:
					code				= (long)GetValueLong(reader, offset);		offset++;
					name				= (string)GetValueString(reader, offset);	offset++;
					groupFlag			= (bool)GetValueBool(reader, offset);		offset++;
					discount			= (bool)GetValueBool(reader, offset);		offset++;
					codeCategorySearch	= (long)GetValueLong(reader, offset);		offset++;
					codeCategoryPrice	= (long)GetValueLong(reader, offset);		offset++;

					tmpWork				= new DbWork(reader, offset);				offset += DbWork.ReaderLength;

					tmpCategorySearch	= null;
					tmpCategoryPrice	= null;
					tmpWorkArray		= null;

					viewType			= 1;
					break;
				case 2:
					code				= (long)GetValueLong(reader, offset);		offset++;
					name				= (string)GetValueString(reader, offset);	offset++;
					groupFlag			= (bool)GetValueBool(reader, offset);		offset++;
					discount			= (bool)GetValueBool(reader, offset);		offset++;
					codeCategorySearch	= (long)GetValueLong(reader, offset);		offset++;
					codeCategoryPrice	= (long)GetValueLong(reader, offset);		offset++;

					tmpWorkArray		= new ArrayList();

					tmpWork				= null;
					tmpCategorySearch	= null;
					tmpCategoryPrice	= null;

					viewType			= 2;
					break;
				default:
					break;

			}
		}
		#endregion

		#region Иницаилизация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 6 собственных полея
			readerLength = 6;

			conn = connection;

			cmdSelect					= new SqlCommand("SELECT_СПРАВОЧНИК_ТРУДОЕМКОСТЬ", conn);
			cmdSelect.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);
			cmdSelect.CommandType		= CommandType.StoredProcedure;

			cmdSelectWork				= new SqlCommand("SELECT_СПРАВОЧНИК_ТРУДОЕМКОСТЬ_ТРУДОЕМКОСТЬ", conn);
			cmdSelectWork.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdSelectWork.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelectWork.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelectWork.CommandType	= CommandType.StoredProcedure;

			cmdSysDelete				= new SqlCommand("SYSTEM_УДАЛИТЬ_СПРАВОЧНИК_ТРУДОЕМКОСТЬ", conn);
			cmdSysDelete.CommandType	= CommandType.StoredProcedure;
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			SetReturnError(cmdSysDelete);

			cmdWrite				= new SqlCommand("WRITE_СПРАВОЧНИК_ТРУДОЕМКОСТЬ", conn);
			cmdWrite.CommandType	= CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@groupFlag", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@discount", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codeCategorySearch", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeCategoryPrice", SqlDbType.BigInt);
			SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction	= ParameterDirection.InputOutput;
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
						break;
					case 2:
						item.Text = "";
						for(int i=0; i < tmpWorkArray.Count; i++)
							item.SubItems.Add("");
						break;
					default:
						item.Text = "";
						break;
				
				}
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			switch(viewType)
			{
				case 1:
					//item.Text				= this.name;
					if(tmpLevel == 0)
						item.Text	= this.name;
					else
						item.Text	= "------//------//------";
					item.SubItems[1].Text	= WorkNameTxt;
					item.SubItems[2].Text	= WorkPriceTxt;
					if(tmpWork == null || tmpWork.Code == 0)
						item.BackColor	= Color.Gray;
					break;
				case 2:
					item.UseItemStyleForSubItems = false;
					if(tmpLevel == 0)
						item.Text	= this.name;
					else
						item.Text	= "------//------//------";
					for(int i = 0; i < tmpWorkArray.Count; i++)
					{
						ArrayList array = (ArrayList)tmpWorkArray[i];
						if(tmpLevel >= array.Count)
						{
							item.SubItems[i+1].BackColor	= Color.Gray;
						}
						else
						{
							DbWork wk = (DbWork)array[tmpLevel];
							item.SubItems[i+1].Text	= wk.WorkPriceTxt;
						}
					}
					break;
				default:
					item.Text		= this.name;
					if(groupFlag)
						item.BackColor	= Color.Aqua;
					else
						item.BackColor	= Color.White;
					// Предупреждения по справочнику
					if(codeCategorySearch == 0) item.BackColor = Color.Yellow;
					if(codeCategoryPrice == 0) item.BackColor = Color.Red;
					break;
			}

			item.Tag		= this;
		}

		public static void FillList(ListView list)
		{
			cmdSelect.Parameters["@type"].Value = 0;
			cmdSelect.Parameters["@code"].Value = 0;
			cmdSelect.Parameters["@pattern"].Value = "";
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillArray2(ArrayList array, DbCategorySearch categorySearch, string pattern)
		{
			int type	= 0;
			long code	= 0;
			if(categorySearch == null && pattern == "")
			{
				type	= 0;
				code	= 0;
			}
			if(categorySearch != null && pattern == "")
			{
				type	= 3;
				code	= categorySearch.Code;
			}
			if(categorySearch == null && pattern != "")
			{
				type	= 2;
				code	= 0;
			}
			if(categorySearch != null && pattern != "")
			{
				type	= 4;
				code	= categorySearch.Code;
			}
			cmdSelect.Parameters["@type"].Value = type;
			cmdSelect.Parameters["@code"].Value = code;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray2));
		}

		public static void FillList(ListView list, DbAutoType autoType, string pattern, long codeSearch)
		{
			if(autoType != null)
				cmdSelectWork.Parameters["@codeAutoType"].Value = autoType.Code;
			else
				cmdSelectWork.Parameters["@codeAutoType"].Value = 0;
			cmdSelectWork.Parameters["@code"].Value = codeSearch;
			cmdSelectWork.Parameters["@pattern"].Value = pattern;

			Db.DbFillList(list, cmdSelectWork, new DelegateInsertInList(InsertInList1));
		}

		public static void FillList(ListView list, DbWorkGroup workGroup)
		{
			cmdSelect.Parameters["@type"].Value = 1;
			cmdSelect.Parameters["@code"].Value = workGroup.Code;
			cmdSelect.Parameters["@pattern"].Value = "";
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FillList(ListView list, string pattern)
		{
			cmdSelect.Parameters["@type"].Value = 2;
			cmdSelect.Parameters["@code"].Value = 0;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FillList(ListView list, DbCategorySearch categorySearch)
		{
			if(categorySearch == null)
			{
				cmdSelect.Parameters["@type"].Value = 0;
				cmdSelect.Parameters["@code"].Value = 0;
				cmdSelect.Parameters["@pattern"].Value = "";
			}
			else
			{
				cmdSelect.Parameters["@type"].Value = 3;
				cmdSelect.Parameters["@code"].Value = categorySearch.Code;
				cmdSelect.Parameters["@pattern"].Value = "";
			}
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FillList(ListView list, DbCategorySearch categorySearch, string pattern)
		{
			if(categorySearch == null)
			{
				cmdSelect.Parameters["@type"].Value = 2;
				cmdSelect.Parameters["@code"].Value = 0;
				cmdSelect.Parameters["@pattern"].Value = pattern;
			}
			else
			{
				cmdSelect.Parameters["@type"].Value = 4;
				cmdSelect.Parameters["@code"].Value = categorySearch.Code;
				cmdSelect.Parameters["@pattern"].Value = pattern;
			}
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDirectoryWork element = new DbDirectoryWork(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void InsertInList1(SqlDataReader reader, ListView list)
		{
			DbDirectoryWork element = new DbDirectoryWork(reader, 0, 1);
			list.Items.Add(element.LVItem);
		}
		public static void InsertInArray2(SqlDataReader reader, ArrayList array)
		{
			DbDirectoryWork element = new DbDirectoryWork(reader, 0, 2);
			array.Add(element);
		}
		#endregion

		#region Основные методы
		public bool Delete()
		{
			//if(Db.CheckSysPass() == false) return false;
			//DialogResult res = MessageBox.Show("Вы уверены что хотите удалить элемент справочника трудоемкостей?", "Предупреждение", MessageBoxButtons.YesNo);
			//if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}
		public bool Write()
		{
			if (adding == false && changed == false) return true;
			try
			{
				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@name"].Value					= (string)name;
				cmdWrite.Parameters["@groupFlag"].Value				= (bool)groupFlag;
				cmdWrite.Parameters["@discount"].Value				= (bool)discount;
				cmdWrite.Parameters["@codeCategorySearch"].Value	= (long)codeCategorySearch;
				cmdWrite.Parameters["@codeCategoryPrice"].Value		= (long)codeCategoryPrice;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			if (adding == true)
				MessageBox.Show("Элемент добавлен");
			else
				MessageBox.Show("Элемент изменен");
			return true;
		}
		public void LoadCategorySearch()
		{
			if(codeCategorySearch == 0) return;
			tmpCategorySearch = DbCategorySearch.Find(codeCategorySearch);
		}
		public void LoadCategoryPrice()
		{
			if(codeCategoryPrice == 0) return;
			tmpCategoryPrice = DbCategoryPrice.Find(codeCategoryPrice);
		}
		#endregion

		#region Доступ к основным параметрам - изменение
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				string txt = value;
				txt = Db.FirstUpper(txt);
				name = SetStringNotEmptyLength(name, txt, 256, "Наименование элемента справочника трудоемкостей");
			}
		}
		public bool GroupFlag
		{
			get
			{
				return groupFlag;
			}
			set
			{
				if(value == groupFlag) return;
				groupFlag	= value;
				changed		= true;
			}
		}
		public bool Discount
		{
			get
			{
				return discount;
			}
			set
			{
				if(value == discount) return;
				discount	= value;
				changed		= true;
			}
		}
		public DbCategorySearch CategorySearch
		{
			get
			{
				return tmpCategorySearch;
			}
			set
			{
				if(value == null)
				{
					if(codeCategorySearch == 0) return;
					codeCategorySearch	= 0;
					tmpCategorySearch	= null;
					changed				= true;
					return;
				}
				if(codeCategorySearch != value.Code)
				{
					codeCategorySearch	= value.Code;
					tmpCategorySearch	= value;
					changed				= true;
					return;
				}
			}
		}
		public DbCategoryPrice CategoryPrice
		{
			get
			{
				return tmpCategoryPrice;
			}
			set
			{
				if(value == null)
				{
					if(codeCategoryPrice == 0) return;
					codeCategoryPrice	= 0;
					tmpCategoryPrice	= null;
					changed				= true;
					return;
				}
				if(codeCategoryPrice != value.Code)
				{
					codeCategoryPrice	= value.Code;
					tmpCategoryPrice	= value;
					changed				= true;
					return;
				}
			}
		}
		public ArrayList WorkArray
		{
			get
			{
				return tmpWorkArray;
			}
			set
			{
				tmpWorkArray = value;
			}
		}
		public void LevelUp()
		{
			tmpLevel++;
		}
		public DbWork Work
		{
			get
			{
				return tmpWork;
			}
			set
			{
				tmpWork = value;
			}
		}
		#endregion

		#region Доступ к основным параметрам - чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Отображение основных параметров в текст
		public string CategorySearchTxt
		{
			get
			{
				if(codeCategorySearch == 0) return "";
				if(tmpCategorySearch == null) return "Не отображается";
				return tmpCategorySearch.Name;
			}
		}
		public string CategoryPriceTxt
		{
			get
			{
				if(codeCategoryPrice == 0) return "";
				if(tmpCategoryPrice == null) return "Не отображается";
				return tmpCategoryPrice.Name;
			}
		}
		public string WorkNameTxt
		{
			get
			{
				if(tmpWork	== null) return "";
				if(tmpWork.Code == 0) return "";
				return tmpWork.Name;
			}
		}
		public string WorkPriceTxt
		{
			get
			{
				if(tmpWork	== null) return "";
				if(tmpWork.Code == 0) return "";
				return tmpWork.ValTxt + " / " + tmpWork.PriceTxt + " / " + tmpWork.PriceGuarantyTxt;
			}
		}
		#endregion
		
	}
}
