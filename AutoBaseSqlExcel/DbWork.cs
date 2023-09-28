using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Трудоемкости выполняемых работ
	/// </summary>
	public class DbWork:Db
	{
		// Описание группы данных
		private long		code;
		private long		codeAutoType;
		private string		position;
		private string		codeDetail;
		private string		codeWork;
		private string		name;
		private string		description;
		private float		val;
		private float		price;
		private float		priceGuaranty;
		private long		codeDirectoryWork;

		// Дополнительные, не входящие непосредственно в базу
		private int				tmpLinks;
		private DbDirectoryWork	tmpDirectoryWork;

		// Дополнительные, для удобства работы
		private bool tmpDeleted;
		private bool tmpExist;
		private bool tmpExpand;

		// Обеспечение связи с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdWritePrice;
		private static SqlCommand cmdWriteAutoType;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelect1;
		private static SqlCommand cmdSelectLink;
		private static SqlCommand cmdSelectFind;
		private static SqlCommand cmdFind;

		private static SqlCommand cmdSystemRemove;
		private static SqlCommand cmdSystemReplace;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}


		public DbWork(DbWork work)
		{
			code				= work.code;
			codeAutoType		= work.codeAutoType;
			position			= work.position;
			codeDetail			= work.codeDetail;
			codeWork			= work.codeWork;
			name				= work.name;
			description			= work.description;
			val					= work.val;
			price				= work.price;
			priceGuaranty		= work.priceGuaranty;
			codeDirectoryWork	= work.codeDirectoryWork;

			tmpLinks			= work.tmpLinks;
			tmpDirectoryWork	= work.tmpDirectoryWork;

			tmpExist			= work.tmpExist;
		}

		public DbWork(DbAutoType autoType)
		{
			code				= 0;
			codeAutoType		= 0;
			position			= "";
			codeDetail			= "";
			codeWork			= "";
			name				= "";
			description			= "";
			val					= 0.0F;
			price				= 0.0F;
			priceGuaranty		= 0.0F;
			codeDirectoryWork	= 0;

			tmpLinks			= 0;
			tmpDirectoryWork	= null;

			adding			= true;
			tmpExist		= true;
			tmpDeleted		= false;
			tmpExpand		= false;

			if(autoType != null)
			{
				codeAutoType	= autoType.Code;
				price			= autoType.Price;
				priceGuaranty	= autoType.PriceGuaranty;
			}
		}

		public DbWork(DbAutoType autoType, DbDirectoryWork directoryWork)
		{
			code				= 0;
			codeAutoType		= 0;
			position			= "";
			codeDetail			= "";
			codeWork			= "";
			name				= directoryWork.Name;
			description			= "";
			val					= 0.0F;
			price				= 0.0F;
			priceGuaranty		= 0.0F;
			codeDirectoryWork	= directoryWork.Code;

			tmpLinks			= 0;
			tmpDirectoryWork	= directoryWork;

			adding			= true;
			tmpExist		= true;
			tmpDeleted		= false;
			tmpExpand		= false;

			if(autoType != null)
			{
				codeAutoType	= autoType.Code;
				price			= autoType.Price;
				priceGuaranty	= autoType.PriceGuaranty;
			}
		}

		public void Default()
		{
			code				= 0;
			codeAutoType		= 0;
			position			= "";
			codeDetail			= "";
			codeWork			= "";
			name				= "";
			description			= "";
			val					= 0.0F;
			price				= 0.0F;
			priceGuaranty		= 0.0F;
			codeDirectoryWork	= 0;

			tmpLinks			= 0;
			tmpDirectoryWork	= null;

			adding = false;
			tmpExist = true;
			tmpDeleted = false;
			tmpExpand = false;

		}

		public DbWork(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeAutoType		= (long)GetValueLong(reader, offset);		offset++;
			position			= (string)GetValueString(reader, offset);	offset++;
			codeDetail			= (string)GetValueString(reader, offset);	offset++;
			codeWork			= (string)GetValueString(reader, offset);	offset++;
			name				= (string)GetValueString(reader, offset);	offset++;
			description			= (string)GetValueString(reader, offset);	offset++;
			val					= (float)GetValueFloat(reader, offset);		offset++;
			price				= (float)GetValueFloat(reader, offset);		offset++;
			priceGuaranty		= (float)GetValueFloat(reader, offset);		offset++;
			codeDirectoryWork	= (long)GetValueLong(reader, offset);		offset++;

			tmpDirectoryWork	= new DbDirectoryWork(reader, offset);		offset += DbDirectoryWork.ReaderLength;

			tmpLinks			= (int)GetValueInt(reader, offset);			offset++;

			tmpExist		= true;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 12 собственных полей + длина DbDirectoryWork
			readerLength = 12 + DbDirectoryWork.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ТРУДОЕМКОСТЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@position", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeDetail", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeWork", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@description", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@val", SqlDbType.Real);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@priceGuaranty", SqlDbType.Real);
			cmdWrite.Parameters.Add("@codeDirectoryWork", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_ТРУДОЕМКОСТЬ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);

			cmdSelect1 = new SqlCommand("SELECT_ТРУДОЕМКОСТЬ_1", conn);
			cmdSelect1.CommandType = CommandType.StoredProcedure;
			cmdSelect1.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdSelect1.Parameters.Add("@codeCategorySearch", SqlDbType.BigInt);
			cmdSelect1.Parameters.Add("@showCommon", SqlDbType.Bit);
			cmdSelect1.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect1.Parameters.Add("@type", SqlDbType.Int);

			cmdSelectFind = new SqlCommand("SELECT_ТРУДОЕМКОСТЬ_СПРАВОЧНИК_НАЙТИ", conn);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
			cmdSelectFind.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			cmdSelectFind.Parameters.Add("@codeDirectoryWork", SqlDbType.BigInt);

			cmdFind = new SqlCommand("SELECT_ТРУДОЕМКОСТЬ_ПОИСК", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);

			cmdSelectLink = new SqlCommand("SELECT_ТРУДОЕМКОСТЬ_СОПУТСТВУЮЩАЯ", conn);
			cmdSelectLink.CommandType = CommandType.StoredProcedure;
			cmdSelectLink.Parameters.Add("@codeWork", SqlDbType.BigInt);

			cmdWritePrice = new SqlCommand("WRITE_ТРУДОЕМКОСТЬ_ЦЕНА", conn);
			cmdWritePrice.CommandType = CommandType.StoredProcedure;
			cmdWritePrice.Parameters.Add("@codeWork", SqlDbType.BigInt);
			cmdWritePrice.Parameters.Add("@price", SqlDbType.Real);
			cmdWritePrice.Parameters.Add("@priceGuaranty", SqlDbType.Real);

			cmdSystemRemove = new SqlCommand("SYSTEM_ТРУДОЕМКОСТЬ_УДАЛИТЬ", conn);
			cmdSystemRemove.CommandType = CommandType.StoredProcedure;
			cmdSystemRemove.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdSystemRemove);

			cmdSystemReplace= new SqlCommand("SYSTEM_ТРУДОЕМКОСТЬ_ЗАМЕНИТЬ", conn);
			cmdSystemReplace.CommandType = CommandType.StoredProcedure;
			cmdSystemReplace.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSystemReplace.Parameters.Add("@codeOld", SqlDbType.BigInt);
			Db.SetReturnError(cmdSystemReplace);

			cmdWriteAutoType = new SqlCommand("WRITE_ТРУДОЕМКОСТЬ_АВТОМОБИЛЬ_ТИП", conn);
			cmdWriteAutoType.CommandType = CommandType.StoredProcedure;
			cmdWriteAutoType.Parameters.Add("@codeWork", SqlDbType.BigInt);
			cmdWriteAutoType.Parameters.Add("@codeAutoType", SqlDbType.BigInt);
			Db.SetReturnError(cmdWriteAutoType);
		}

		private static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction	= trans;
		}

		public bool WritePrice(float newPrice, float newPriceGuaranty)
		{
			try
			{
				price			= newPrice;
				priceGuaranty	= newPriceGuaranty;
				cmdWritePrice.Parameters["@codeWork"].Value			= (long)code;
				cmdWritePrice.Parameters["@price"].Value			= (float)newPrice;
				cmdWritePrice.Parameters["@priceGuaranty"].Value	= (float)newPriceGuaranty;
				cmdWritePrice.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
		public bool WriteAutoType(long newCodeAutoType)
		{
			try
			{
				cmdWriteAutoType.Parameters["@codeWork"].Value			= (long)code;
				cmdWriteAutoType.Parameters["@codeAutoType"].Value				= (long)newCodeAutoType;
				cmdWriteAutoType.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteAutoType);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			codeAutoType	= newCodeAutoType;
			return true;
		}

		public bool WriteNullAutoType()
		{
			return WriteAutoType(0);
		}

		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeAutoType"].Value		= (long)codeAutoType;
				cmdWrite.Parameters["@position"].Value			= (string)position;
				cmdWrite.Parameters["@codeDetail"].Value		= (string)codeDetail;
				cmdWrite.Parameters["@codeWork"].Value			= (string)codeWork;
				cmdWrite.Parameters["@name"].Value				= (string)name;
				cmdWrite.Parameters["@description"].Value		= (string)description;
				cmdWrite.Parameters["@val"].Value				= (float)val;
				cmdWrite.Parameters["@price"].Value				= (float)price;
				cmdWrite.Parameters["@priceGuaranty"].Value		= (float)priceGuaranty;
				cmdWrite.Parameters["@codeDirectoryWork"].Value	= (long)codeDirectoryWork;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetException(E);
				ShowFaults();
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("Добавлена/исправлена трудоемкость");
			return true;
		}

		#region Доступ к основным параметрам
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion
		public string CodeAuto
		{
			set
			{
			//	codeAuto = SetStringNotEmptyLength(codeAuto, value, 10, "КОД МАРКИ АВТОМОБИЛЯ");
			}
			get
			{
				return "";
				//return codeAuto;
			}
		}
		public string Position
		{
			set
			{
				position = SetStringLength(position, value, 10, "НОМЕР ПОЗИЦИИ");
			}
			get
			{
				return position;
			}
		}
		public string CodeDetail
		{
			set
			{
				codeDetail = SetStringLength(codeDetail, value, 10, "КОД ДЕТАЛИ");
			}
			get
			{
				return codeDetail;
			}
		}
		public string CodeWork
		{
			set
			{
				codeWork = SetStringLength(codeWork, value, 10, "КОД РАБОТЫ");
			}
			get
			{
				return codeWork;
			}
		}
		public string Name
		{
			set
			{
				name = SetStringNotEmptyLength(name, value, 512, "НАИМЕНОВАНИЕ");
			}
			get
			{
				return name;
			}
		}
		public string Description
		{
			set
			{
				description = SetStringLength(description, value, 1024, "ОПИСАНИЕ");
			}
			get
			{
				return description;
			}
		}
		public float Val
		{
			set
			{
				val = this.SetFloatNotMinus(val, value, "ТРУДОЕМКОСТЬ");
			}
			get
			{
				return val;
			}
		}

		public string CostTxt
		{
			// Стоимость работы
			get
			{
				float cost = 0.0F;
				if(val == 0.0F)
					cost = price;
				else
					cost = val * price;
				return Db.CachToTxt(cost);
			}
		}

		public string ValTxt
		{
			set
			{
				val = this.SetFloatNotMinus(val, value, "ТРУДОЕМКОСТЬ");
			}
			get
			{
				return val.ToString();
			}
		}

		public float Price
		{
			set
			{
				price = this.SetFloatNotMinus(price, value, "СТОИМОСТЬ НОРМАЧАСА");
			}
			get
			{
				return price;
			}
		}

		public long CodeDirectoryWork
		{
			get
			{
				if (tmpDirectoryWork == null) return 0;
				return tmpDirectoryWork.Code;
			}
		}

		public string PriceTxt
		{
			set
			{
				price = this.SetFloatNotMinus(price, value, "СТОИМОСТЬ НОРМАЧАСА");
			}
			get
			{
				return price.ToString();
			}
		}

		public float PriceGuaranty
		{
			set
			{
				priceGuaranty = this.SetFloatNotMinus(priceGuaranty, value, "СТОИМОСТЬ ГАРАНТИЙНОГО НОРМАЧАСА");
			}
			get
			{
				return priceGuaranty;
			}
		}

		public string PriceGuarantyTxt
		{
			set
			{
				priceGuaranty = this.SetFloatNotMinus(priceGuaranty, value, "СТОИМОСТЬ ГАРАНТИЙНОГО НОРМАЧАСА");
			}
			get
			{
				return priceGuaranty.ToString();
			}
		}

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				switch(viewType){
					
					case 1:
						item.Text = "";
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.BackColor = Color.Transparent;
					break;
					default:
						item.Text = "";
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.BackColor = Color.Transparent;
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
					item.Tag = this;
					item.Text = codeDetail;
					item.SubItems[1].Text = position;
					if(this.codeDirectoryWork == 0)
					{
						if(codeAutoType == 0)
							item.BackColor = Color.Honeydew;
						else
							item.BackColor = Color.LightYellow;
						item.SubItems[2].Text = "";
					}
					else
					{
						if(this.tmpDirectoryWork == null)
							item.SubItems[2].Text = "";
						else
							item.SubItems[2].Text = tmpDirectoryWork.Name;
						if(codeAutoType == 0)
							item.BackColor = Color.GreenYellow;
					}
					item.SubItems[3].Text = name;
					item.SubItems[4].Text = this.WorkPriceTxt;
					item.SubItems[5].Text = this.CostTxt;
				break;
				default:
					item.Tag = this;
					item.Text = position;
					item.SubItems[1].Text = codeDetail;
					item.SubItems[2].Text = codeWork;
					item.SubItems[3].Text = name;
					// Переделка
					//if(codeDirectoryWork != 0)
					//{
					//	item.SubItems[3].Text = tmpDirectoryWork.Name + " " + name;
					//}
					//
					item.SubItems[4].Text = val.ToString();
					item.SubItems[5].Text = price.ToString() + "/" + priceGuaranty.ToString();

					// Новое поле - общая цена
					item.SubItems[7].Text = this.CostTxt;
					// Конец новое поле

					item.BackColor = Color.Transparent;
					if(this.tmpLinks > 0)
					{
						if(this.Expand)
							item.SubItems[6].Text = "-";
						else
							item.SubItems[6].Text = "+";
					}
					else
					{
						item.SubItems[6].Text = "";
					}
					if(this.codeAutoType == 0)
					{
						item.BackColor = Color.GreenYellow;
					}
					if(this.codeDirectoryWork == 0)
					{
						item.BackColor = Color.LightYellow;
					}
					if(this.codeAutoType == 0 && this.codeDirectoryWork == 0)
					{
						item.BackColor = Color.Honeydew;
					}
					if(this.tmpExist == false)
					{
						item.BackColor = Color.Yellow;
					}
					if(this.tmpDeleted == true)
					{
						item.BackColor = Color.Red;
					}
				break;
			}
		}

		public static void FillList(ListView list, DbAutoType autoType, int type, string pattern)
		{
			if(autoType == null)
			{
				MessageBox.Show("Необходимо выбрать марку автомобиля");
				return;
			}
			cmdSelect.Parameters["@type"].Value = type;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			cmdSelect.Parameters["@codeAutoType"].Value = autoType.Code;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FillListLinks(ListView list, DbWork work)
		{
			cmdSelectLink.Parameters["@codeWork"].Value = (long)work.Code;
			Db.DbFillList(list, cmdSelectLink, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbWork element = new DbWork(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillList1(ListView list, DbAutoType autoType, DbCategorySearch categorySearch, bool showCommon, int type, string pattern)
		{
			long codeAutoType = 0;
			if(autoType == null)
				codeAutoType = 0;
			else
				codeAutoType = autoType.Code;
			long codeCategorySearch = 0;
			if(categorySearch == null)
				codeCategorySearch = 0;
			else
				codeCategorySearch = categorySearch.Code;
			cmdSelect1.Parameters["@type"].Value = type;
			cmdSelect1.Parameters["@pattern"].Value = pattern;
			cmdSelect1.Parameters["@codeAutoType"].Value = codeAutoType;
			cmdSelect1.Parameters["@codeCategorySearch"].Value = codeCategorySearch;
			cmdSelect1.Parameters["@showCommon"].Value = showCommon;
			Db.DbFillList(list, cmdSelect1, new DelegateInsertInList(InsertInList1));
		}
		public static void FillList2(ListView list, DbAutoType autoType, DbWorkGroup group, int type)
		{
			long codeAutoType = 0;
			if(autoType == null)
				codeAutoType = 0;
			else
				codeAutoType = autoType.Code;
			cmdSelect1.Parameters["@type"].Value = type;
			cmdSelect1.Parameters["@pattern"].Value = "";
			cmdSelect1.Parameters["@codeAutoType"].Value = codeAutoType;
			cmdSelect1.Parameters["@codeCategorySearch"].Value = group.Code;
			cmdSelect1.Parameters["@showCommon"].Value = 0;
			Db.DbFillList(list, cmdSelect1, new DelegateInsertInList(InsertInList1));
		}
		public static void InsertInList1(SqlDataReader reader, ListView list)
		{
			DbWork element = new DbWork(reader, 0);
			element.SetViewType(1);
			list.Items.Add(element.LVItem);
		}
		public void FillListLinksTmp(ListView list, int startIndex)
		{
			cmdSelectLink.Parameters["@codeWork"].Value = (long)code;
			SqlDataReader reader = cmdSelectLink.ExecuteReader();
			int index = startIndex;
			while(reader.Read())
			{
				DbWork work = new DbWork(reader, 0);
				work.Exist = false;
				list.Items.Insert(index, work.LVItem);
				index++;
			}
			if(reader != null) reader.Close();
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbWork element = new DbWork(reader, 0);
			array.Add(element);
		}
		public static void FillArray(ArrayList array, DbAutoType autoType, DbDirectoryWork directoryWork)
		{
			if(autoType != null)
				cmdSelectFind.Parameters["@codeAutoType"].Value = (long)autoType.Code;
			else
				cmdSelectFind.Parameters["@codeAutoType"].Value = (long)0;
			cmdSelectFind.Parameters["@codeDirectoryWork"].Value = (long)directoryWork.Code;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
		}

		#endregion

		public void FillListArray(ArrayList list)
		{
			cmdSelectLink.Parameters["@codeWork"].Value = (long)code;
			SqlDataReader reader = cmdSelectLink.ExecuteReader();
			while(reader.Read())
			{
				DbWork work = new DbWork(reader, 0);
				list.Add(work);
			}
			if(reader != null) reader.Close();
		}

		

		public bool Exist
		{
			set
			{
				tmpExist = value;
			}
			get
			{
				return tmpExist;
			}
		}

		public bool Deleted
		{
			set
			{
				tmpDeleted = value;
			}
			get
			{
				return tmpDeleted;
			}
		}

		public DbDirectoryWork DirectoryWork
		{
			set
			{
				if(value == null) return;
				if(value.Code	== codeDirectoryWork) return;
				codeDirectoryWork	= value.Code;
				tmpDirectoryWork	= value;
				changed	= true;
			}
			get
			{
				return tmpDirectoryWork;
			}
		}

		public string DirectoryWorkTxt
		{
			get
			{
				if(codeDirectoryWork == 0) return "";
				if(tmpDirectoryWork == null) return "Код = " + codeDirectoryWork.ToString();
				return tmpDirectoryWork.Name;
			}
		}

		public bool Expand
		{
			set
			{
				tmpExpand = value;
			}
			get
			{
				return tmpExpand;
			}
		}

		public string ToolTipText
		{
			get
			{
				return name + "\n" + description;
			}
		}

		public int Links
		{
			get
			{
				return tmpLinks;
			}
			set
			{
				tmpLinks = value;
			}
		}

		public bool Delete()
		{
			//if(Db.CheckSysAction("Вы уверены что хотите удалить трудоемкость?") == false) return false;
			cmdSystemRemove.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSystemRemove);
		}
		public bool Replace(DbWork work)
		{
			//if(Db.CheckSysPass() == false) return false;
			//if(work == null) return false;
			//string text = "Вы уверены что хотите заменить " + this.Name + " на " + work.Name + "?";
			//DialogResult res = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
			//if(res == DialogResult.No) return false;
			cmdSystemReplace.Parameters["@codeOld"].Value = (long)code;
			cmdSystemReplace.Parameters["@code"].Value = (long)work.Code;
			return Db.ExecuteCommandError(cmdSystemReplace);
		}
		public string WorkPriceTxt
		{
			get
			{
				return ValTxt + " / " + PriceTxt + " / " + PriceGuarantyTxt;
			}
		}

		public static DbWork Find(long code)
		{
			SqlDataReader reader = null;
			DbWork work = null;
			try
			{

				cmdFind.Parameters["@code"].Value = code;
				reader = cmdFind.ExecuteReader();
				if(reader.Read())
					work = new DbWork(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return work;
		}
	}
}
