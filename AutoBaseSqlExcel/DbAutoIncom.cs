using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Приход автомобилей
	/// </summary>
	public class DbAutoIncom:Db
	{
		long		code;				// Код прихода
		long		codeAuto;			// Код автомобиля
		long		codeAutoFactura;	// Код приходного документа
		float		incomPrice;			// Входная цена
		float		nds;				// НДС
		float		deliveryPrice;		// Цена доставки
		DateTime	deliveryDate;		// Дата прихода
		bool		incom;				// Отметка о приходе автомобиля на склад
		long		codeTakeDocument;	// Код документа принятия автомобиля
		int			position;			// Порядковый номер в документе

		// Временные
		bool		isDeliveryDate;
		DbAuto		tmpAuto;

		// Связь с базой данных
		public static SqlConnection			conn;
		public static SqlCommand			cmdSelect;
		public static SqlCommand			cmdSelectTakeDocument;
		public static SqlCommand			cmdSelectNotIncom;
		public static SqlCommand			cmdSelectNotIncomSearchBody;
		public static SqlCommand			cmdSelectNotIncomDocument;
		public static SqlCommand			cmdWrite;
		public static SqlCommand			cmdWriteDeleteIncom;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		protected static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 8 собственных полей и длина ридера класса DbAuto
			readerLength = 10 + DbAuto.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПРИХОД", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoFactura", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@incomPrice", SqlDbType.Real);
			cmdWrite.Parameters.Add("@nds", SqlDbType.Real);
			cmdWrite.Parameters.Add("@deliveryPrice", SqlDbType.Real);
			cmdWrite.Parameters.Add("@deliveryDate", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@incom", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codeTakeDocument", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@position", SqlDbType.Int);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteDeleteIncom = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПРИХОД_УДАЛИТЬ_ПРИХОД", conn);
			cmdWriteDeleteIncom.CommandType = CommandType.StoredProcedure;
			cmdWriteDeleteIncom.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdWriteDeleteIncom);

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИХОД", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeIncomDoc", SqlDbType.BigInt);

			cmdSelectTakeDocument = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИНЯТИЕ", conn);
			cmdSelectTakeDocument.CommandType = CommandType.StoredProcedure;
			cmdSelectTakeDocument.Parameters.Add("@codeTakeDocument", SqlDbType.BigInt);

			cmdSelectNotIncom = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИХОД_НЕПРИНЯТЫЕ", conn);
			cmdSelectNotIncom.CommandType = CommandType.StoredProcedure;

			cmdSelectNotIncomSearchBody = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИХОД_НЕПРИНЯТЫЕ_ПОИСК_КУЗОВ", conn);
			cmdSelectNotIncomSearchBody.CommandType = CommandType.StoredProcedure;
			cmdSelectNotIncomSearchBody.Parameters.Add("@pattern", SqlDbType.VarChar);

			cmdSelectNotIncomDocument = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПРИХОД_НЕПРИНЯТЫЕ_ДОКУМЕНТ", conn);
			cmdSelectNotIncomDocument.CommandType = CommandType.StoredProcedure;
			cmdSelectNotIncomDocument.Parameters.Add("@code", SqlDbType.BigInt);
		}
		#endregion

		#region Конструкторы
		public DbAutoIncom(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			codeAuto			= (long)GetValueLong(reader, offset);		offset++;
			codeAutoFactura		= (long)GetValueLong(reader, offset);		offset++;
			incomPrice			= (float)GetValueFloat(reader, offset);		offset++;
			nds					= (float)GetValueFloat(reader, offset);		offset++;
			deliveryPrice		= (float)GetValueFloat(reader, offset);		offset++;
			if(IsValueNull(reader, offset))
			{
				offset++;
				isDeliveryDate = false;
				deliveryDate	= DateTime.Now;
			}
			else
			{
				deliveryDate	= (DateTime)GetValueDate(reader, offset);	offset++;
				isDeliveryDate = true;
			}
			incom				= (bool)GetValueBool(reader, offset);		offset++;
			codeTakeDocument	= (long)GetValueLong(reader, offset);		offset++;
			position			= (int)GetValueInt(reader, offset);			offset++;

			tmpAuto				= new DbAuto(reader, offset);				offset += DbAuto.ReaderLength;
		}
		public DbAutoIncom(DbAutoIncom src)
		{
			code				= src.code;
			codeAuto			= src.codeAuto;
			codeAutoFactura		= src.codeAutoFactura;
			incomPrice			= src.incomPrice;
			nds					= src.nds;
			deliveryPrice		= src.deliveryPrice;
			deliveryDate		= src.deliveryDate;
			incom				= src.incom;
			codeTakeDocument	= src.codeTakeDocument;
			position			= src.position;

			tmpAuto				= new DbAuto(src.Auto);
			isDeliveryDate		= src.isDeliveryDate;
		}
		public DbAutoIncom()
		{
			code				= 0;
			codeAuto			= 0;
			codeAutoFactura		= 0;
			incomPrice			= 0.0f;
			nds					= 0.0f;
			deliveryPrice		= 0.0f;
			deliveryDate		= DateTime.Now;
			incom				= false;
			codeTakeDocument	= 0;
			position			= 0;

			tmpAuto				= new DbAuto();
			isDeliveryDate		= false;
			adding				= true;
		}
		#endregion

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();

				switch (viewType)
				{
					case 1:
						item.Text = "";
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						item.SubItems.Add("");
						break;
					case 2:
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
			switch (viewType)
			{
				case 1:
					item.Text				= AutoModelTxt;
					item.SubItems[1].Text	= VinNoTxt;
					item.SubItems[2].Text	= AutoSubModelTxt;
					item.SubItems[3].Text	= AutoComplectTxt;
					item.SubItems[4].Text	= AutoColorsTxt;
					break;
				case 2:
					item.Text				= AutoModelTxt;
					item.SubItems[1].Text	= VinNoTxt;
					item.SubItems[2].Text	= AutoSubModelTxt;
					item.SubItems[3].Text	= AutoComplectTxt;
					item.SubItems[4].Text	= AutoColorsTxt;
					item.SubItems[5].Text	= DateTxt;
					break;
				default:
					item.Text				= position.ToString();
					item.SubItems[1].Text	= AutoModelTxt;
					item.SubItems[2].Text	= AutoSubModelTxt;
					item.SubItems[3].Text	= AutoComplectTxt;
					item.SubItems[4].Text	= BodyNoTxt;
					item.SubItems[5].Text	= AutoColorsTxt;
					item.SubItems[6].Text	= IncomPriceTxt;
					item.SubItems[7].Text	= DeliveryPriceTxt;
					item.SubItems[8].Text	= FullPriceTxt;
					break;
			}
			item.Tag = this;
		}
		public static void FillList(ListView list, DbAutoFactura factura)
		{
			cmdSelect.Parameters["@codeIncomDoc"].Value = factura.Code;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FillList(ListView list, DbTakeDocument document)
		{
			cmdSelectTakeDocument.Parameters["@codeTakeDocument"].Value = document.Code;
			Db.DbFillList(list, cmdSelectTakeDocument, new DelegateInsertInList(InsertInList1));
		}
		public static void FillListNotIncom(ListView list)
		{
			Db.DbFillList(list, cmdSelectNotIncom, new DelegateInsertInList(InsertInList2));
		}
		public static void FillListNotIncomSearchBody(ListView list, string pattern)
		{
			cmdSelectNotIncomSearchBody.Parameters["@pattern"].Value = pattern;
			Db.DbFillList(list, cmdSelectNotIncomSearchBody, new DelegateInsertInList(InsertInList2));
		}
		public static void FillListNotIncomDocument(ListView list, DbAutoFactura document)
		{
			cmdSelectNotIncomDocument.Parameters["@code"].Value = document.Code;
			Db.DbFillList(list, cmdSelectNotIncomDocument, new DelegateInsertInList(InsertInList2));
		}
		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoIncom element = new DbAutoIncom(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void InsertInList1(SqlDataReader reader, ListView list)
		{
			DbAutoIncom element = new DbAutoIncom(reader, 0);
			element.SetViewType(1);
			list.Items.Add(element.LVItem);
		}
		public static void InsertInList2(SqlDataReader reader, ListView list)
		{
			DbAutoIncom element = new DbAutoIncom(reader, 0);
			element.SetViewType(2);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region Отображение в текст основных параметров
		public string AutoModelTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.ModelTxt;
			}
		}
		public string AutoSubModelTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.AutoSubModelTxt;
			}
		}
		public string AutoComplectTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.AutoComplectTxt;
			}
		}
		public string AutoColorsTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.AutoColorsTxt;
			}
		}
		public string BodyNoTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.BodyNo;
			}
		}
		public string VinNoTxt
		{
			get
			{
				if(tmpAuto == null) return "";
				return tmpAuto.Vin;
			}
		}
		public string IncomPriceTxt
		{
			get
			{
				return Db.CachToTxt(incomPrice);
			}
		}
		public string DeliveryPriceTxt
		{
			get
			{
				return Db.CachToTxt(deliveryPrice);
			}
		}
		public string FullPriceTxt
		{
			get
			{
				return Db.CachToTxt(deliveryPrice + incomPrice);
			}
		}
		public string DateTxt
		{
			get
			{
				if(isDeliveryDate == false) return "--";
				return Db.DateToTxt(deliveryDate);
			}
		}
		#endregion

		#region Доступ к основным параметрам - только чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		public DateTime DeliveryDate
		{
			get
			{
				return deliveryDate;
			}
		}
		public bool Valid
		{
			get
			{
				if(tmpAuto.BodyNo == "" && tmpAuto.Vin == "")
				{
					Db.SetErrorMessage("Нельзя оставлять пустым номер кузова и VIN");
					return false;
				}
				return true;
			}
		}
		public bool Incom
		{
			get
			{
				return incom;
			}
		}
		#endregion 

		#region Доступ к основным параметрам - Изменение
		public DbAuto Auto
		{
			get
			{
				return tmpAuto;
			}
			set
			{
				if(codeAuto != value.Code) return;
				tmpAuto = value;
			}
		}
		public float IncomPrice
		{
			set
			{
				if(incomPrice == value) return;
				changed = true;
				incomPrice = value;
			}
			get
			{
				return incomPrice;
			}
		}
		public int Position
		{
			set
			{
				if(value < 0) return;
				if(position == value) return;
				changed = true;
				position = value;
			}
			get
			{
				return position;
			}
		}
		public float DeliveryPrice
		{
			set
			{
				if(deliveryPrice == value) return;
				changed = true;
				deliveryPrice = value;
			}
			get
			{
				return deliveryPrice;
			}
		}
		#endregion

		#region Основные методы
		public static bool WriteList(ListView list, DbAutoFactura factura)
		{
			bool fault_flag			= false;
			SqlTransaction trans	= null;
			trans					= conn.BeginTransaction(IsolationLevel.Serializable);
			SetTransaction(trans);
			DbAuto.SetTransaction(trans);
			try
			{
				foreach(ListViewItem item in list.Items)
				{
					DbAutoIncom autoIncom = (DbAutoIncom)item.Tag;
					if(autoIncom != null)
					{
						// Записываем элемент списка
						if(!autoIncom.Valid)
						{
							// Отметка ошибочного элемента
							fault_flag		= true;
							item.BackColor	= Color.Yellow;
						}
						else
						{
							if(autoIncom.Write(factura)!= true)
							{
								// Отметка косячного элемента
								fault_flag		= true;
								item.BackColor	= Color.Red;
							}
						}
					}
				}
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				DbAuto.SetTransaction(null);
				return false;
			}
			if(fault_flag)
			{
				// Если мы встречали ошибки
				trans.Rollback();
				SetTransaction(null);
				DbAuto.SetTransaction(null);
				ShowFaults();
				return false;
			}
			ShowFaults();
			trans.Commit();
			SetTransaction(null);
			DbAuto.SetTransaction(null);
			return true;
		}
		public static bool WriteList(ListView list, DbTakeDocument doc)
		{
			bool fault_flag			= false;
			SqlTransaction trans	= null;
			trans					= conn.BeginTransaction(IsolationLevel.Serializable);
			SetTransaction(trans);
			DbAuto.SetTransaction(trans);
			try
			{
				foreach(ListViewItem item in list.Items)
				{
					DbAutoIncom autoIncom = (DbAutoIncom)item.Tag;
					if(autoIncom != null)
					{
						// Записываем элемент списка
						if(!autoIncom.Valid)
						{
							// Отметка ошибочного элемента
							fault_flag		= true;
							item.BackColor	= Color.Yellow;
						}
						else
						{
							if(autoIncom.Write(doc)!= true)
							{
								// Отметка косячного элемента
								fault_flag		= true;
								item.BackColor	= Color.Red;
							}
						}
					}
				}
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				DbAuto.SetTransaction(null);
				return false;
			}
			if(fault_flag)
			{
				// Если мы встречали ошибки
				trans.Rollback();
				SetTransaction(null);
				DbAuto.SetTransaction(null);
				ShowFaults();
				return false;
			}
			ShowFaults();
			trans.Commit();
			SetTransaction(null);
			DbAuto.SetTransaction(null);
			return true;
		}
		public bool Write(DbAutoFactura factura)
		{
			try
			{
				codeAutoFactura		= factura.Code;
				// При записи прихода автомобиля необходимо сначала записать
				// сам автомобиль если его нет в списке и загрузить его, если он есть
				if(tmpAuto.Adding == true || tmpAuto.Changed == true)
				{
					if(tmpAuto.WriteExtend() != true)
					{
						SetErrorMessage("Не удалось записать автомобиль");
						return false;
					}
					// Восстанавливаем код автомобиля
					codeAuto	= tmpAuto.Code;
				}

				// Если нет изменения делать ничего не нужно
				if(adding == false && changed==false) return true;

				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@delete"].Value			= (bool)deleted;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeAuto"].Value			= (long)codeAuto;
				cmdWrite.Parameters["@codeAutoFactura"].Value	= (long)codeAutoFactura;
				cmdWrite.Parameters["@incomPrice"].Value		= (float)incomPrice;
				cmdWrite.Parameters["@nds"].Value				= (float)nds;
				cmdWrite.Parameters["@deliveryPrice"].Value		= (float)deliveryPrice;
				cmdWrite.Parameters["@deliveryDate"].Value		= (DateTime)deliveryDate;
				cmdWrite.Parameters["@incom"].Value				= (bool)incom;
				cmdWrite.Parameters["@codeTakeDocument"].Value	= (long)codeTakeDocument;
				cmdWrite.Parameters["@position"].Value			= (int)position;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
		public bool Write(DbTakeDocument doc)
		{
			try
			{
				codeTakeDocument		= doc.Code;
				// При записи прихода автомобиля необходимо сначала записать
				// сам автомобиль если его нет в списке и загрузить его, если он есть
				if(tmpAuto.Adding == true || tmpAuto.Changed == true)
				{
					if(tmpAuto.WriteExtend() != true)
					{
						SetErrorMessage("Не удалось записать автомобиль");
						return false;
					}
					// Восстанавливаем код автомобиля
					codeAuto	= tmpAuto.Code;
				}

				if(adding == false && changed==false) return true;

				cmdWrite.Parameters["@adding"].Value			= (bool)adding;
				cmdWrite.Parameters["@delete"].Value			= (bool)deleted;
				cmdWrite.Parameters["@code"].Value				= (long)code;
				cmdWrite.Parameters["@codeAuto"].Value			= (long)codeAuto;
				cmdWrite.Parameters["@codeAutoFactura"].Value	= (long)codeAutoFactura;
				cmdWrite.Parameters["@incomPrice"].Value		= (float)incomPrice;
				cmdWrite.Parameters["@nds"].Value				= (float)nds;
				cmdWrite.Parameters["@deliveryPrice"].Value		= (float)deliveryPrice;
				cmdWrite.Parameters["@deliveryDate"].Value		= (DateTime)deliveryDate;
				cmdWrite.Parameters["@incom"].Value				= (bool)incom;
				cmdWrite.Parameters["@codeTakeDocument"].Value	= (long)codeTakeDocument;
				cmdWrite.Parameters["@position"].Value			= (int)position;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
		public void ResetAuto()
		{
			DbAuto auto			= new DbAuto();
			auto.AutoColors		= this.Auto.AutoColors;
			auto.AutoComplect	= this.Auto.AutoComplect;
			auto.AutoModel		= this.Auto.AutoModel;
			auto.AutoSubModel	= this.Auto.AutoSubModel;

			this.tmpAuto		= auto;
		}
		public void ResetIncom()
		{
			code	= 0;
		}
		public bool WriteDeleteIncom()
		{
			try
			{	
				cmdWriteDeleteIncom.Parameters["@code"].Value		= (long)code;
				cmdWriteDeleteIncom.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteDeleteIncom);
			}
			catch(Exception E)
			{
				Db.ShowFaults();
				SetException(E);
				return false;
			}
			return true;
		}
		#endregion
	}
}
