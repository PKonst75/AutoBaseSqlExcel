using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailStorage.
	/// </summary>
	public class DbDetailStorage:Db
	{
		// Основные параметры
		private long		code;				// Код складской позиции
		private long		codeDetail;			// Код детали (ссылка)
		private string		description;		// Описание
		private string		units;				// Единица измерения
		private float		nds;				// НДС
		private float		price;				// Цена
		private float		quontity;			// Количество
		private float		minQuontity;		// Минимальный остаток
		private long		codeStorageGroup;	// Код складской группы
		private bool		state;				// Если истина - складская позиция отключена
		// Добавления / переделки
		private bool		liquid;				// Истина, если скаладская позиция маслянистая жидкость
		private string		name;				// Полное наменклатурное наименование
		private string		number;				// Каталожный номер детали
		private long		code_1c;			// Код синхронизации с 1С
		private float		input;				// Текущая входная цена
		private float		reserv;             // Количество зарезервированного на заказ-наряды
		private string code_1c_string = "";
		
		private DbDetail		tmpDetail;
		private DbStorageGroup	tmpStorageGroup;

		private static SqlConnection conn;
		private static SqlCommand cmdDetailStorageCheck;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdWriteV1;
		private static SqlCommand cmdWrite1C;
		private static SqlCommand cmdWrite1CNew;
		private static SqlCommand cmdWritePrice;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdFind;
		// Опасные функции
		private static SqlCommand cmdSysDelete;
		private static SqlCommand cmdSysChange;
		private static SqlCommand cmdSysState;


		private static SqlCommand cmdExecNewPriceAuto;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void SetTransaction(SqlTransaction trans)
		{
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 10 собственных полей и остальное
			readerLength = 10 + 4 + 2 + DbDetail.ReaderLength + DbStorageGroup.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetail", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@description", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@units", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@nds", SqlDbType.Real);
			cmdWrite.Parameters.Add("@minQuontity", SqlDbType.Real);
			cmdWrite.Parameters.Add("@codeStorageGroup", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteV1 = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_В1", conn);
			cmdWriteV1.CommandType = CommandType.StoredProcedure;
			cmdWriteV1.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteV1.Parameters.Add("@codeDetail", SqlDbType.BigInt);
			cmdWriteV1.Parameters.Add("@description", SqlDbType.VarChar);
			cmdWriteV1.Parameters.Add("@units", SqlDbType.VarChar);
			cmdWriteV1.Parameters.Add("@nds", SqlDbType.Real);
			cmdWriteV1.Parameters.Add("@minQuontity", SqlDbType.Real);
			cmdWriteV1.Parameters.Add("@codeStorageGroup", SqlDbType.BigInt);
			cmdWriteV1.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWriteV1.Parameters.Add("@liquid", SqlDbType.Bit);
			cmdWriteV1.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWriteV1.Parameters.Add("@number", SqlDbType.VarChar);
			cmdWriteV1.Parameters.Add("@code_1c", SqlDbType.BigInt);
			SetReturnError(cmdWriteV1);
			cmdWriteV1.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWrite1C = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_1С", conn);
			cmdWrite1C.CommandType = CommandType.StoredProcedure;
			cmdWrite1C.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite1C.Parameters.Add("@quontity", SqlDbType.Real);
			cmdWrite1C.Parameters.Add("@units", SqlDbType.VarChar);
			cmdWrite1C.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite1C.Parameters.Add("@number", SqlDbType.VarChar);
			cmdWrite1C.Parameters.Add("@code_1c", SqlDbType.BigInt);
			cmdWrite1C.Parameters.Add("@input", SqlDbType.Real);
			SetReturnError(cmdWrite1C);

			cmdWrite1CNew = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_1CNEW", conn);
			cmdWrite1CNew.CommandType = CommandType.StoredProcedure;
			cmdWrite1CNew.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite1CNew.Parameters.Add("@quontity", SqlDbType.Real);
			cmdWrite1CNew.Parameters.Add("@units", SqlDbType.VarChar);
			cmdWrite1CNew.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite1CNew.Parameters.Add("@number", SqlDbType.VarChar);
			cmdWrite1CNew.Parameters.Add("@code_1c", SqlDbType.VarChar);
			cmdWrite1CNew.Parameters.Add("@input", SqlDbType.Real);
			SetReturnError(cmdWrite1CNew);

			//cmdSelect = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ", conn);
			//cmdSelect = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ_В1", conn);
			cmdSelect = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ_В2", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);
			cmdSelect.Parameters.Add("@code", SqlDbType.BigInt);

			cmdFind = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ_ПОИСК", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);

			cmdWritePrice = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_ЦЕНА", conn);
			cmdWritePrice.CommandType = CommandType.StoredProcedure;
			cmdWritePrice.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWritePrice.Parameters.Add("@price", SqlDbType.Real);
			SetReturnError(cmdWritePrice);

			cmdDetailStorageCheck = new SqlCommand("REPORT_ПРОВЕРКА_СКЛАД_ДЕТАЛЬ_КОЛИЧЕСТВО");
			cmdDetailStorageCheck.CommandType = CommandType.StoredProcedure;
			cmdDetailStorageCheck.Connection = conn;

			cmdSysDelete = new SqlCommand("SYSTEM_СКЛАД_ДЕТАЛЬ_УДАЛИТЬ", conn);
			cmdSysDelete.CommandType = CommandType.StoredProcedure;
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			SetReturnError(cmdSysDelete);

			cmdSysChange = new SqlCommand("SYSTEM_СКЛАД_ДЕТАЛЬ_СМЕНА_ДЕТАЛЬ", conn);
			cmdSysChange.CommandType = CommandType.StoredProcedure;
			cmdSysChange.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysChange.Parameters.Add("@codeDetail", SqlDbType.BigInt);
			SetReturnError(cmdSysChange);

			cmdSysState = new SqlCommand("SYSTEM_СКЛАД_ДЕТАЛЬ_ОТКЛЮЧИТЬ", conn);
			cmdSysState.CommandType = CommandType.StoredProcedure;
			cmdSysState.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysState.Parameters.Add("@state", SqlDbType.Bit);
			SetReturnError(cmdSysState);

			
			cmdExecNewPriceAuto = new SqlCommand("REPORT_NEWPRICE", conn);
			cmdExecNewPriceAuto.CommandType = CommandType.StoredProcedure;
			cmdExecNewPriceAuto.Parameters.Add("@codeDetail", SqlDbType.VarChar);
			cmdExecNewPriceAuto.Parameters.Add("@codeFirm", SqlDbType.VarChar);
			cmdExecNewPriceAuto.Parameters.Add("@price", SqlDbType.Real);
			cmdExecNewPriceAuto.Parameters["@price"].Direction = ParameterDirection.InputOutput;
		}

		public bool WriteOld()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeDetail"].Value		= (long)codeDetail;
				cmdWrite.Parameters["@description"].Value		= (string)description;
				cmdWrite.Parameters["@units"].Value				= (string)units;
				cmdWrite.Parameters["@nds"].Value				= (float)nds;
				cmdWrite.Parameters["@minQuontity"].Value		= (float)minQuontity;
				cmdWrite.Parameters["@codeStorageGroup"].Value	= (long)codeStorageGroup;
				cmdWrite.ExecuteNonQuery();
				ThrowReturnError(cmdWrite);
				code			= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			MessageBox.Show("Складская позиция добавлена/изменена");
			return true;
		}	

		public bool Write()
		{
			try
			{
				cmdWriteV1.Parameters["@adding"].Value				= (bool)adding;
				cmdWriteV1.Parameters["@code"].Value				= (long)code;
				cmdWriteV1.Parameters["@codeDetail"].Value			= (long)codeDetail;
				cmdWriteV1.Parameters["@description"].Value			= (string)description;
				cmdWriteV1.Parameters["@units"].Value				= (string)units;
				cmdWriteV1.Parameters["@nds"].Value					= (float)nds;
				cmdWriteV1.Parameters["@minQuontity"].Value			= (float)minQuontity;
				cmdWriteV1.Parameters["@codeStorageGroup"].Value	= (long)codeStorageGroup;
				cmdWriteV1.Parameters["@liquid"].Value				= (bool)liquid;
				cmdWriteV1.Parameters["@name"].Value				= (string)name;
				cmdWriteV1.Parameters["@number"].Value				= (string)number;
				cmdWriteV1.Parameters["@code_1c"].Value				= (long)code_1c;

				cmdWriteV1.ExecuteNonQuery();
				ThrowReturnError(cmdWriteV1);
				code			= (long)cmdWriteV1.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			MessageBox.Show("Складская позиция добавлена/изменена");
			return true;
		}


		public bool WritePrice()
		{
			try
			{
				cmdWritePrice.Parameters["@code"].Value			= (long)code;
				cmdWritePrice.Parameters["@price"].Value		= (float)price;
				cmdWritePrice.ExecuteNonQuery();
				ThrowReturnError(cmdWritePrice);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			//MessageBox.Show("Цена изменена");
			return true;
		}	

		public bool AutoPrice()
		{
			try
			{
				cmdExecNewPriceAuto.Parameters["@codeDetail"].Value = codeDetail;
	//			cmdExecNewPriceAuto.Parameters["@codeFirm"].Value = codeFirm;
				cmdExecNewPriceAuto.Parameters["@price"].Value = price;
				cmdExecNewPriceAuto.ExecuteNonQuery();
				price = (float)cmdExecNewPriceAuto.Parameters["@Price"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}	

		public DbDetailStorage()
		{
			code				= 0;
			codeDetail			= 0;
			description			= "";
			units				= "";
			nds					= 0.0F;
			price				= 0.0F;
			quontity			= 0.0F;
			minQuontity			= 0.0F;
			codeStorageGroup	= 0;
			state				= false;
			// Добавления/исправления
			liquid				= false;
			name				= "";
			number				= "";
			code_1c				= 0;
			input				= 0.0F;
			reserv				= 0.0F;
	
			tmpDetail			= null;
			tmpStorageGroup		= null;

			code_1c_string = "";

			adding = true;
		}

		public DbDetailStorage(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeDetail			= (long)GetValueLong(reader, offset);		offset++;
			description			= (string)GetValueString(reader, offset);	offset++;
			units				= (string)GetValueString(reader, offset);	offset++;
			nds					= (float)GetValueFloat(reader, offset);		offset++;
			price				= (float)GetValueFloat(reader, offset);		offset++;
			quontity			= (float)GetValueFloat(reader, offset);		offset++;
			minQuontity			= (float)GetValueFloat(reader, offset);		offset++;
			codeStorageGroup	= (long)GetValueLong(reader, offset);		offset++;
			state				= (bool)GetValueBool(reader, offset);		offset++;

			// Добавление/Исправление
			liquid				= (bool)GetValueBool(reader, offset);		offset++;
			name				= (string)GetValueString(reader, offset);	offset++;
			number				= (string)GetValueString(reader, offset);	offset++;
			code_1c				= (long)GetValueLong(reader, offset);		offset++;
			input				= (float)GetValueFloat(reader, offset);		offset++;
			reserv				= (float)GetValueFloat(reader, offset);		offset++;


			tmpDetail			= new DbDetail(reader, offset);				offset = offset + DbDetail.ReaderLength;
			tmpStorageGroup		= new DbStorageGroup(reader, offset);		offset = offset + DbStorageGroup.ReaderLength;

			//code_1c_string		= (string)GetValueString(reader, offset); offset++;
		}

		public DbDetailStorage(DbDetailStorage source)
		{
			code				= source.code;
			codeDetail			= source.codeDetail;
			description			= source.description;
			units				= source.units;
			nds					= source.nds;
			price				= source.price;
			quontity			= source.quontity;
			minQuontity			= source.minQuontity;
			codeStorageGroup	= source.codeStorageGroup;
			// Добавление/изменение
			liquid				= source.liquid;
			name				= source.name;
			number				= source.number;
			code_1c				= source.code_1c;
			input				= source.input;
			reserv				= source.reserv;
			
			tmpDetail			= source.tmpDetail;
			tmpStorageGroup		= source.tmpStorageGroup;

			code_1c_string = source.code_1c_string;
		}

		public string DetailName
		{
			get
			{
				return Name;
			//	if(tmpDetail != null)
			//	{
			//		return tmpDetail.Name + "/" + description;
			//		//return tmpDetail.Name;// + " / " + tmpDetail.CodeTxt;
			//	}
			//	else
			//		return "Деталь не выбрана";
			}
		}
		public string DetailCodeTxt
		{
			get
			{
				return Number;
				//if(tmpDetail == null) return "Деталь не выбрана";
				//return tmpDetail.CodeTxt;
			}
		}

		public string PriceTxt
		{
			get
			{
				return Db.CachToTxt(price);
			}
			set
			{
				price = SetFloatNotMinus(price, value, "Цена");
			}
		}
		public string QuontityTxt
		{
			get
			{
				return Db.FloatToTxt(quontity);
			}
		}
		public string ReservTxt
		{
			get
			{
				return Db.FloatToTxt(quontity);
			}
		}

		public long Code
		{
			get
			{
				return code;
			}
		}

		public float Nds
		{
			get
			{
				return nds;
			}
		}

		public string StorageGroupName
		{
			get
			{
				if(tmpStorageGroup != null)
					return tmpStorageGroup.Name;
				else
					return "";
			}
		}

		public DbDetail Detail
		{
			set
			{
				if(value == null) return;
				if(codeDetail == value.Code) return;
				
				codeDetail		= value.Code;
				tmpDetail		= value;
				this.changed	= true;
			}
			get
			{
				return tmpDetail;
			}
		}

		public DbStorageGroup StorageGroup
		{
			set
			{
				if(codeStorageGroup != value.Code)
				{
					codeStorageGroup = value.Code;
					tmpStorageGroup = value;
					this.changed = true;
				}
			}
			get
			{
				return tmpStorageGroup;
			}
		}

		public bool IsValid()
		{
			if(name.Length == 0) return false;
		//	if(tmpDetail == null)
		//	{
		//		return false;
		//	}
			return true;
		}

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
			item.Text				= DetailCodeTxt;
			item.SubItems[1].Text	= DetailName;
		//	item.SubItems[2].Text	= description;
			item.SubItems[2].Text	= quontity.ToString();
			if(reserv > 0.0F)
				item.SubItems[2].Text += " (" + reserv.ToString() + ")";
			item.SubItems[3].Text	= units;
			item.SubItems[4].Text	= Db.CachToTxt(price);
			item.SubItems[5].Text	= this.Description;
			item.BackColor			= Color.White;
			if(minQuontity > quontity) item.BackColor = Color.Yellow;
			if(state == true) item.BackColor = Color.Gray;
			if(this.code_1c > 0)
				item.BackColor			= Color.LightGreen;
			if (this.code_1c_string.Length > 0)
				item.BackColor = Color.LightBlue;
			item.Tag = this;
		}

		public static void FillList(ListView list, int type, string pattern)
		{
			cmdSelect.Parameters["@type"].Value = type;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			cmdSelect.Parameters["@code"].Value = 0;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbDetail detail)
		{
			cmdSelect.Parameters["@type"].Value = 3;
			cmdSelect.Parameters["@pattern"].Value = "";
			cmdSelect.Parameters["@code"].Value = detail.Code;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDetailStorage element = new DbDetailStorage(reader, 0);
			list.Items.Add(element.LVItem);
		}

		#endregion

		public long CodeDetail
		{
			get
			{
				return codeDetail;
			}
		}

		public bool State
		{
			get
			{
				return state;
			}
		}

		public bool Oil
		{
			get
			{
				if(tmpDetail != null)
				{
					if(tmpDetail.Oil == true) return true;
				}
				if(liquid == true) return true;
				return false;
			}
		}

		public string NdsTxt
		{
			get
			{
				return nds.ToString();
			}
			set
			{
				nds = SetFloatNotMinus(nds, value, "Значение НДС");
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				//description = SetStringNotEmptyLength(description, value, 120, "Описание складской позиции");
				description = value;
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
				changed = true;
				price = value;
			}
		}
		public float Input
		{
			get
			{
				return input;
			}
		}
		public bool Liquid
		{
			get
			{
				return liquid;
			}
			set
			{
				if(value == liquid) return;
				changed = true;
				liquid = value;
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
				string tmp = value;
				tmp.Trim();
				if(name == tmp) return;
				changed = true;
				name = value;
			}
		}
		public string Number
		{
			get
			{
				return number;
			}
			set
			{
				string tmp = value;
				tmp.Trim();
				if(number == tmp) return;
				changed = true;
				number = value;
			}
		}

		public string Code_1C_Txt
		{
			get
			{
				return code_1c.ToString();
			}
		}
		public long Code_1C
		{
			get
			{
				return code_1c;
			}
		}

		public string MinQuontityTxt
		{
			get
			{
				return Db.FloatToTxt(minQuontity);
			}
			set
			{
				minQuontity = this.SetFloatNotMinus(minQuontity, value, "Минимальное количество");
			}
		}

		public string Units
		{
			get
			{
				return units;
			}
			set
			{
				units = this.SetStringLength(units, value, 10, "Единицы измерения");
			}
		}

		public static void DetailStorageCheck()
		{
			DialogResult res = MessageBox.Show("Операйия займет длительное время. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return;
			Db.ExecuteCommand(cmdDetailStorageCheck);
		}

		public bool DetailStorageDelete()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите удалить складскую позицию?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}

		public bool DetailStorageChangeDetail(DbDetail detail)
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите изменить основную деталь?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysChange.Parameters["@code"].Value = (long)code;
			cmdSysChange.Parameters["@codeDetail"].Value = (long)detail.Code;
			if(!Db.ExecuteCommandError(cmdSysChange)) return false;
			this.Detail = detail;
			return true;
		}

		public bool DetailStorageState()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите изменить состояние складской позиции?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysState.Parameters["@code"].Value = (long)code;
			cmdSysState.Parameters["@state"].Value = (bool)!state;
			if(!Db.ExecuteCommandError(cmdSysState)) return false;
			state = !state;
			return true;
		}

		public static bool Write(TxtReadPrice.FileLine line)
		{
			try
			{
				cmdWrite1C.Parameters["@units"].Value				= (string)line.nomenclature_unit;
				cmdWrite1C.Parameters["@price"].Value				= (float)line.nomenclature_price;
				cmdWrite1C.Parameters["@quontity"].Value			= (float)line.nomenclature_quontity;
				cmdWrite1C.Parameters["@name"].Value				= (string)line.nomenclature_name;
				cmdWrite1C.Parameters["@number"].Value				= (string)line.nomenclature_number;
				cmdWrite1C.Parameters["@code_1c"].Value				= (long)line.nomenclature_code;
				cmdWrite1C.Parameters["@input"].Value				= (float)line.nomenclature_input;

				cmdWrite1C.ExecuteNonQuery();
				ThrowReturnError(cmdWrite1C);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public static DbDetailStorage Find(long code)
		{
			SqlDataReader reader = null;
			DbDetailStorage detail = null;
			try
			{

				cmdFind.Parameters["@code"].Value = code;
				reader = cmdFind.ExecuteReader();
				if(reader.Read())
					detail = new DbDetailStorage(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return detail;
		}
		public static bool Write(ReadTextStorage.NomenclatureInFileLine line)
		{
			try
			{
				cmdWrite1CNew.Parameters["@units"].Value = (string)line.unit;
				cmdWrite1CNew.Parameters["@price"].Value = (float)line.price;
				cmdWrite1CNew.Parameters["@quontity"].Value = (float)line.quontity;
				cmdWrite1CNew.Parameters["@name"].Value = (string)line.name;
				cmdWrite1CNew.Parameters["@number"].Value = (string)line.articul;
				cmdWrite1CNew.Parameters["@code_1c"].Value = (string)line.articul;
				cmdWrite1CNew.Parameters["@input"].Value = (float)line.cost_price;

				cmdWrite1CNew.ExecuteNonQuery();
				ThrowReturnError(cmdWrite1CNew);
			}
			catch (Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

	}
}
