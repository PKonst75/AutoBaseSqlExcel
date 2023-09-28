using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAuto.
	/// </summary>
	public class DbAuto:Db
	{
		public struct TO
		{
			public int to;
			public int run;
			public DateTime date;
		};

		// Основные параметры
		private long		code;				// Код автомобиля в базе
		private long		codeModel;			// Код модели/марки автомобиля
		private int			year;				// Год выпуска
		private string		vin;				// VIN автомобиля
		private string		engineNo;			// Номер двигателя
		private string		frameNo;			// Номер рамы (шасси)
		private string		bodyNo;				// Номер кузова
		private string		signNo;				// Номер, регистрационный знак
		private string		comment;			// Примечание
		private DateTime	sellDate;			// Дата продажи автомобиля
		private long		codeGuarantyType;	// Код вида гарантии по автомобилю
		private long		codeAutoComplect;	// Код комплектации автомобиля
		private long		codeAutoColors;		// Код цветов автомобиля
		private long		codeAutoSubModel;	// Код подмодели автомобиля
		private long		spare_part_number;	// Номер для запчастей
		private bool		no_spare_part_number;// Присутсвует ли номер для запчастей
		private long		code_factory;		// Ссылка (через код) на завод-изготовитель
		
		// Дополнительные описания
		private DbAutoModel		tmpAutoModel;	// Модель автомобиля
		private DbGuarantyType	tmpGuarantyType;// Вид гарантии
		private DbAutoComplect	tmpAutoComplect;// Комплектация атвомобиля
		private DbAutoColors	tmpAutoColors;	// Цвет автомобиля
		private DbAutoSubmodel	tmpAutoSubModel;// Подмодель автомобиля

		// Вспомогательные данные
		private bool		isSellDate;		// Установлена ли дата продажи

		// То что загружаем отдельно
		private DtFactory		tmp_factory;


		// Взаимодействие с базой
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdWriteExtend;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectPartnerAuto;
		private static SqlCommand cmdWritePartnerAuto;
		private static SqlCommand cmdFind;
		private static SqlCommand cmdSelectBarCode;
		private static SqlCommand cmdSelectLastTo;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public DbAuto()
		{
			code					= 0;
			codeModel				= 0;
			year					= 0;
			vin						= "";
			engineNo				= "";
			frameNo					= "";
			bodyNo					= "";
			signNo					= "";
			comment					= "";
			codeGuarantyType		= 0;
			codeAutoComplect		= 0;
			codeAutoColors			= 0;
			codeAutoSubModel		= 0;
			spare_part_number		= 0;
			no_spare_part_number	= false;
			code_factory			= 0;

			tmpAutoModel	= null;
			tmpGuarantyType = null;
			tmpAutoComplect	= null;
			tmpAutoColors	= null;
			tmpAutoSubModel = null;

			adding			= true;
			isSellDate		= false;

			tmp_factory		= null;
		}
		public DbAuto(DbAuto source)
		{
			code				= source.code;
			codeModel			= source.codeModel;
			year				= source.year;
			vin					= source.vin;
			engineNo			= source.engineNo;
			frameNo				= source.frameNo;
			bodyNo				= source.bodyNo;
			signNo				= source.signNo;
			comment				= source.comment;
			sellDate			= source.sellDate;
			codeGuarantyType	= source.codeGuarantyType;
			codeAutoComplect	= source.codeAutoComplect;
			codeAutoColors		= source.codeAutoColors;
			codeAutoSubModel	= source.codeAutoSubModel;
			spare_part_number	= source.spare_part_number;
			no_spare_part_number= source.no_spare_part_number;
			code_factory		= source.code_factory;

			tmpAutoModel		= source.tmpAutoModel;
			tmpGuarantyType		= source.tmpGuarantyType;
			tmpAutoComplect		= source.tmpAutoComplect;
			tmpAutoColors		= source.tmpAutoColors;
			tmpAutoSubModel		= source.tmpAutoSubModel;

			isSellDate			= source.isSellDate;

			tmp_factory			= source.tmp_factory;
		}
		public DbAuto(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			codeModel	= (long)GetValueLong(reader, offset);		offset++;
			year		= (int)GetValueInt(reader, offset);			offset++;
			vin			= (string)GetValueString(reader, offset);	offset++;
			engineNo	= (string)GetValueString(reader, offset);	offset++;
			frameNo		= (string)GetValueString(reader, offset);	offset++;
			bodyNo		= (string)GetValueString(reader, offset);	offset++;
			signNo		= (string)GetValueString(reader, offset);	offset++;
			comment		= (string)GetValueString(reader, offset);	offset++;
			if (IsValueNull(reader, offset))
			{
				isSellDate		= false;
				offset++;
			}
			else
			{
				isSellDate		= true;
				sellDate		= (DateTime)GetValueDate(reader, offset);
				offset++;
			}
			codeGuarantyType	= (long)GetValueLong(reader, offset);		offset++;
			codeAutoComplect	= (long)GetValueLong(reader, offset);		offset++;
			codeAutoColors		= (long)GetValueLong(reader, offset);		offset++;
			codeAutoSubModel	= (long)GetValueLong(reader, offset);		offset++;
			spare_part_number	= (long)GetValueLong(reader, offset);		offset++;
			no_spare_part_number= (bool)GetValueBool(reader, offset);		offset++;
			code_factory		= (long)GetValueLong(reader, offset);		offset++;


			tmpAutoModel	= new DbAutoModel(reader, offset);			offset = offset + DbAutoModel.ReaderLength;
			tmpGuarantyType = new DbGuarantyType(reader, offset);		offset = offset + DbGuarantyType.ReaderLength;
			tmpAutoComplect = new DbAutoComplect(reader, offset);		offset = offset + DbAutoComplect.ReaderLength;
			tmpAutoColors	= new DbAutoColors(reader, offset);			offset = offset + DbAutoColors.ReaderLength;
			tmpAutoSubModel = new DbAutoSubmodel(reader, offset);		offset = offset + DbAutoSubmodel.ReaderLength;

			tmp_factory		= null;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 14 собственных полей и длина ридера класса DbAutoModel
			readerLength = 14 + 2 + 1 + DbAutoModel.ReaderLength + DbGuarantyType.ReaderLength + DbAutoComplect.ReaderLength + DbAutoColors.ReaderLength + DbAutoSubmodel.ReaderLength;

			conn = connection;

			//cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ", conn);
			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_V1", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@year", SqlDbType.Int);
			cmdWrite.Parameters.Add("@vin", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@engineNo", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@frameNo", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@bodyNo", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@signNo", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@sellDate", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@codeGuarantyType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoComplect", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoColors", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeAutoSubModel", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@spare_part_number", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@no_spare_part_number", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@code_factory", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteExtend = new SqlCommand("WRITE_АВТОМОБИЛЬ_РАСШИРЕННО", conn);
			cmdWriteExtend.CommandType = CommandType.StoredProcedure;
			cmdWriteExtend.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@year", SqlDbType.Int);
			cmdWriteExtend.Parameters.Add("@vin", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@engineNo", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@frameNo", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@bodyNo", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@signNo", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWriteExtend.Parameters.Add("@sellDate", SqlDbType.DateTime);
			cmdWriteExtend.Parameters.Add("@codeGuarantyType", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@codeAutoComplect", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@codeAutoColors", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@codeAutoSubModel", SqlDbType.BigInt);
			cmdWriteExtend.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWriteExtend);
			cmdWriteExtend.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWritePartnerAuto = new SqlCommand("WRITE_СВЯЗЬ_КОНТРАГЕНТ_АВТОМОБИЛЬ", conn);
			cmdWritePartnerAuto.CommandType = CommandType.StoredProcedure;
			cmdWritePartnerAuto.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdWritePartnerAuto.Parameters.Add("@codePartner", SqlDbType.BigInt);
			cmdWritePartnerAuto.Parameters.Add("@adding", SqlDbType.Bit);

			//cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ", conn);
			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_V1", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);

			cmdSelectBarCode = new SqlCommand("SELECT_АВТОМОБИЛЬ_ШТРИХ", conn);
			cmdSelectBarCode.CommandType = CommandType.StoredProcedure;
			cmdSelectBarCode.Parameters.Add("@patternModel", SqlDbType.VarChar);
			cmdSelectBarCode.Parameters.Add("@patternBody", SqlDbType.VarChar);
			cmdSelectBarCode.Parameters.Add("@patternVin", SqlDbType.VarChar);
			cmdSelectBarCode.Parameters.Add("@type", SqlDbType.Int);

			//cmdSelectPartnerAuto = new SqlCommand("SELECT_АВТОМОБИЛЬ_КОНТРАГЕНТА", conn);
			cmdSelectPartnerAuto = new SqlCommand("SELECT_АВТОМОБИЛЬ_КОНТРАГЕНТА_V1", conn);
			cmdSelectPartnerAuto.Parameters.Add("@codePartner", SqlDbType.BigInt);
			cmdSelectPartnerAuto.CommandType = CommandType.StoredProcedure;

			//cmdFind = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПОИСК", conn);
			cmdFind = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПОИСК_V1", conn);
			cmdFind.CommandType = CommandType.StoredProcedure;
			cmdFind.Parameters.Add("@code", SqlDbType.BigInt);

			cmdSelectLastTo = new SqlCommand("SELECT_КАРТОЧКА_ПОСЛЕДНЕЕ_ТО", conn);
			cmdSelectLastTo.CommandType = CommandType.StoredProcedure;
			cmdSelectLastTo.Parameters.Add("@codeAuto", SqlDbType.BigInt);
			cmdSelectLastTo.Parameters.Add("@last_to", SqlDbType.Int);
			cmdSelectLastTo.Parameters.Add("@last_run", SqlDbType.Int);
			cmdSelectLastTo.Parameters.Add("@last_date", SqlDbType.DateTime);
			cmdSelectLastTo.Parameters["@last_to"].Direction = ParameterDirection.Output;
			cmdSelectLastTo.Parameters["@last_run"].Direction = ParameterDirection.Output;
			cmdSelectLastTo.Parameters["@last_date"].Direction = ParameterDirection.Output;
			SetReturnError(cmdSelectLastTo);
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction		= trans;
			cmdWriteExtend.Transaction	= trans;
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value	= (bool)adding;
				cmdWrite.Parameters["@code"].Value		= (long)code;
				cmdWrite.Parameters["@codeModel"].Value = (long)codeModel;
				cmdWrite.Parameters["@year"].Value		= (int)year;
				cmdWrite.Parameters["@vin"].Value		= (string)vin;
				cmdWrite.Parameters["@engineNo"].Value	= (string)engineNo;
				cmdWrite.Parameters["@frameNo"].Value	= (string)frameNo;
				cmdWrite.Parameters["@bodyNo"].Value	= (string)bodyNo;
				cmdWrite.Parameters["@signNo"].Value	= (string)signNo;
				cmdWrite.Parameters["@comment"].Value	= (string)comment;
				if(isSellDate)
					cmdWrite.Parameters["@sellDate"].Value	= (DateTime)sellDate;
				else
					cmdWrite.Parameters["@sellDate"].Value	= DBNull.Value;
				cmdWrite.Parameters["@codeGuarantyType"].Value	= (long)codeGuarantyType;
				cmdWrite.Parameters["@codeAutoComplect"].Value	= (long)codeAutoComplect;
				cmdWrite.Parameters["@codeAutoColors"].Value	= (long)codeAutoColors;
				cmdWrite.Parameters["@codeAutoSubModel"].Value	= (long)codeAutoSubModel;
				cmdWrite.Parameters["@spare_part_number"].Value = (long)spare_part_number;
				cmdWrite.Parameters["@no_spare_part_number"].Value = (bool)no_spare_part_number;
				cmdWrite.Parameters["@code_factory"].Value		= (long)code_factory;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			MessageBox.Show("Автомобиль добавлен/изменен");
			return true;
		}

		public bool WriteExtend()
		{
			try
			{
				cmdWriteExtend.Parameters["@adding"].Value				= (bool)adding;
				cmdWriteExtend.Parameters["@code"].Value				= (long)code;
				cmdWriteExtend.Parameters["@codeModel"].Value			= (long)codeModel;
				cmdWriteExtend.Parameters["@year"].Value				= (int)year;
				cmdWriteExtend.Parameters["@vin"].Value					= (string)vin;
				cmdWriteExtend.Parameters["@engineNo"].Value			= (string)engineNo;
				cmdWriteExtend.Parameters["@frameNo"].Value				= (string)frameNo;
				cmdWriteExtend.Parameters["@bodyNo"].Value				= (string)bodyNo;
				cmdWriteExtend.Parameters["@signNo"].Value				= (string)signNo;
				cmdWriteExtend.Parameters["@comment"].Value				= (string)comment;
				if(isSellDate)
					cmdWriteExtend.Parameters["@sellDate"].Value		= (DateTime)sellDate;
				else
					cmdWriteExtend.Parameters["@sellDate"].Value		= DBNull.Value;
				cmdWriteExtend.Parameters["@codeGuarantyType"].Value	= (long)codeGuarantyType;
				cmdWriteExtend.Parameters["@codeAutoComplect"].Value	= (long)codeAutoComplect;
				cmdWriteExtend.Parameters["@codeAutoColors"].Value		= (long)codeAutoColors;
				cmdWriteExtend.Parameters["@codeAutoSubModel"].Value	= (long)codeAutoSubModel;
				cmdWriteExtend.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteExtend);
				code = (long)cmdWriteExtend.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			return true;
		}

		public bool WritePartnerAuto(DbPartner partner, bool add)
		{
			try
			{
				cmdWritePartnerAuto.Parameters["@adding"].Value		= (bool)add;
				cmdWritePartnerAuto.Parameters["@codeAuto"].Value	= (long)code;
				cmdWritePartnerAuto.Parameters["@codePartner"].Value= (long)partner.Code;
				cmdWritePartnerAuto.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			MessageBox.Show("Успешно изменили список автомобилей клиента");
			return true;
		}

		public static DbAuto Find(long code)
		{
			SqlDataReader reader = null;
			DbAuto auto = null;
			try
			{

				cmdFind.Parameters["@code"].Value = code;
				reader = cmdFind.ExecuteReader();
				if(reader.Read())
					auto = new DbAuto(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return auto;
		}

		public TO LastTo()
		{
			TO lastTo;
			try
			{

				cmdSelectLastTo.Parameters["@codeAuto"].Value = this.code;
				cmdSelectLastTo.ExecuteNonQuery();
				lastTo.to = (int)cmdSelectLastTo.Parameters["@last_to"].Value;
				lastTo.run = (int)cmdSelectLastTo.Parameters["@last_run"].Value;
				lastTo.date = (DateTime)cmdSelectLastTo.Parameters["@last_date"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				lastTo.to = 0;
				lastTo.run = 0;
				lastTo.date = DateTime.Now;
				return lastTo;
			}
			return lastTo;
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = ModelTxt;
			item.SubItems[1].Text = bodyNo;
			item.SubItems[2].Text = signNo;
			item.SubItems[3].Text = comment;
			item.Tag = this;
		}

		public static void FillList(ListView list, int searchType, string pattern)
		{
			cmdSelect.Parameters["@type"].Value = searchType;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbPartner partner)
		{
			cmdSelectPartnerAuto.Parameters["@codePartner"].Value = (long)partner.Code;
			Db.DbFillList(list, cmdSelectPartnerAuto, new DelegateInsertInList(InsertInList));
		}

		public static void FillListBarCode(ListView list, string patternModel, string patternBody)
		{
			cmdSelectBarCode.Parameters["@patternModel"].Value = (string)patternModel;
			cmdSelectBarCode.Parameters["@patternBody"].Value = (string)patternBody;
			cmdSelectBarCode.Parameters["@patternVin"].Value = (string)"";
			cmdSelectBarCode.Parameters["@type"].Value = 1;
			Db.DbFillList(list, cmdSelectBarCode, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAuto element = new DbAuto(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		#region Доступ к основным параметрам
		public long Code
		{
			get
			{
				return code;
			}
		}
		public long CodeModel
		{
			get
			{
				return codeModel;
			}
		}
		public DbAutoModel AutoModel
		{
			set
			{
				if(value == null) return;
				if(value.Code == this.codeModel) return;
				changed = true;
				this.codeModel = value.Code;
				tmpAutoModel = value;
			}
			get
			{
				return tmpAutoModel;
			}
		}
		public string Vin
		{
			set
			{
				vin = this.SetStringLength(vin, value, 25, "VIN автомобиля");
			}
			get
			{
				return vin;
			}
		}
		public int Year
		{
			set
			{
				year = this.SetIntNotMinus(year, value, "Год выпуска");
			}
			get
			{
				return year;
			}
		}
		public long SparePartNumber
		{
			set
			{
				spare_part_number = value;
			}
			get
			{
				return spare_part_number;
			}
		}
		public bool NoSparePartNumber
		{
			set
			{
				no_spare_part_number = value;
			}
			get
			{
				return no_spare_part_number;
			}
		}
		public string EngineNo
		{
			set
			{
				engineNo = this.SetStringLength(engineNo, value, 25, "Номер двигателя");
			}
			get
			{
				return engineNo;
			}
		}
		public string FrameNo
		{
			set
			{
				frameNo = this.SetStringLength(frameNo, value, 25, "Номер шасси");
			}
			get
			{
				return frameNo;
			}
		}
		public string BodyNo
		{
			set
			{
				value = value.ToUpper();
				bodyNo = this.SetStringLength(bodyNo, value, 25, "Номер кузова");
			}
			get
			{
				return bodyNo;
			}
		}
		public string SignNo
		{
			set
			{
				signNo = this.SetStringLength(signNo, value, 10, "Гос. номер");
			}
			get
			{
				return signNo;
			}
		}
		public string Comment
		{
			set
			{
				comment = this.SetStringLength(comment, value, 100, "Комментарий");
			}
			get
			{
				return comment;
			}
		}
		public DateTime SellDate
		{
			set
			{
				sellDate	= value;
			}
			get
			{
				return sellDate;
			}
		}
		public bool IsSellDate
		{
			get
			{
				return isSellDate;
			}
			set
			{
				isSellDate = value;
			}
		}
		#endregion

		#region Отображение параметров в текст
		public string YearTxt
		{
			set
			{
				year = this.SetIntNotMinus(year, value, "Год Выпуска");
			}
			get
			{
				return year.ToString();
			}
		}
		public string SparePartNumberTxt
		{
			set
			{
				spare_part_number = this.SetLongNotMinus(spare_part_number, value, "Номер для запчастей");
			}
			get
			{
				return spare_part_number.ToString();
			}
		}
		public string SellDateTxt
		{
			get
			{
				if(this.IsSellDate)
					return this.SellDate.ToShortDateString();
				else
					return "Не установлена";
			}
		}
		public string ModelTxt
		{
			get
			{
				if(codeModel == 0) return "Марка автомобиля не выбрана";
				if(tmpAutoModel == null) return "Марка автомобиля не выбрана";
				return tmpAutoModel.Model;
			}
		}
		public string AutoSubModelTxt
		{
			get
			{
				if(codeAutoSubModel == 0) return "";
				if(tmpAutoSubModel == null) return "";
				return tmpAutoSubModel.DbTitle();
			}
		}
		public string AutoComplectTxt
		{
			get
			{
				if(codeAutoComplect == 0) return "";
				if(tmpAutoComplect == null) return "";
				return tmpAutoComplect.DbTitle();
			}
		}
		public string AutoColorsTxt
		{
			get
			{
				if(codeAutoColors == 0) return "";
				if(tmpAutoColors == null) return "";
				return tmpAutoColors.DbTitle();
			}
		}
		#endregion

		public string Title
		{
			get
			{	
				string txt = "";
				txt += ModelTxt;
				if(bodyNo.Length != 0) txt += " / № кузова " + bodyNo;
				return txt;
			}
		}
		public string GuarantyTypeTxt
		{
			get
			{	
				if(tmpGuarantyType == null) return "Необходимо выбрать вид гарантии";
				if(codeGuarantyType == 0) return "Необходимо выбрать вид гарантии";
				return tmpGuarantyType.Description;
			}
		}
		public DbGuarantyType GuarantyType
		{
			get
			{	
				return tmpGuarantyType;
			}
			set
			{
				if(value == null) return;
				if(value.Code == codeGuarantyType) return;
				changed = true;
				codeGuarantyType = value.Code;
				tmpGuarantyType		= value;
			}
		}
		public DbAutoComplect AutoComplect
		{
			get
			{	
				if(codeAutoComplect == 0) return null;
				if(tmpAutoComplect == null) return null;
				if(tmpAutoComplect.Code == 0) return null;
				return tmpAutoComplect;
			}
			set
			{
				if(value == null)
				{
					if(codeAutoComplect == 0) return;
					codeAutoComplect	= 0;
					tmpAutoComplect		= value;
					changed = true;
					return;
				}
				if(value.Code == codeAutoComplect) return;
				changed = true;
				codeAutoComplect	= value.Code;
				tmpAutoComplect		= value;
			}
		}
		public DbAutoColors AutoColors
		{
			get
			{	
				if(codeAutoColors == 0) return null;
				if(tmpAutoColors == null) return null;
				if(tmpAutoColors.Code == 0) return null;
				return tmpAutoColors;
			}
			set
			{
				if(value == null)
				{
					if(codeAutoColors == 0) return;
					codeAutoColors	= 0;
					tmpAutoColors		= value;
					changed = true;
					return;
				}
				if(value.Code == codeAutoColors) return;
				changed = true;
				codeAutoColors	= value.Code;
				tmpAutoColors	= value;
			}
		}
		public DbAutoSubmodel AutoSubModel
		{
			get
			{	
				if(codeAutoSubModel == 0) return null;
				if(tmpAutoSubModel == null) return null;
				if(tmpAutoSubModel.Code == 0) return null;
				return tmpAutoSubModel;
			}
			set
			{
				if(value == null)
				{
					if(codeAutoSubModel== 0) return;
					codeAutoSubModel	= 0;
					tmpAutoSubModel		= value;
					changed = true;
					return;
				}
				if(value.Code == codeAutoSubModel) return;
				changed = true;
				codeAutoSubModel	= value.Code;
				tmpAutoSubModel		= value;
			}
		}
		public string Info
		{
			get
			{	
				string txt = "";
				txt += ModelTxt;
				if(bodyNo.Length != 0) txt += " / " + bodyNo;
				else txt += " / бн";
				if(signNo.Length != 0) txt += " / " + signNo;
				else txt += " / бн";
				return txt;
			}
		}

		public void Load()
		{
			// Загрузка всех дополнительных параметров
			if(code_factory != 0)
			{
				tmp_factory = DbSqlFactory.Find(code_factory);
			}
		}

		public string FactoryTxt
		{
			get
			{
				if(code_factory == 0) return "";
				if(tmp_factory != null) return (string)tmp_factory.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ") + "(" + (string)tmp_factory.GetData("ПРЕФИКС_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ") + ")";
				return "КОД := " + code_factory.ToString();
			}
		}

		public long CodeFactory
		{
			set
			{
				code_factory = value;
				tmp_factory = DbSqlFactory.Find(code_factory);
				changed = true;
			}
			get
			{
				return code_factory;
			}
		}

		public void DirectionReport()
		{
			// Выдача отчета по необходимым предписаниям
			DbSqlDirection.PrepareSelectList(this);
			ArrayList array = new ArrayList();
			DbSql.FillArray(array, DbSqlDirection.select_list, new DbSql.DelegateMakeElement(DbSqlDirection.MakeElement));
			foreach(object o in array)
			{
				DtDirection direction = (DtDirection)o;
				string text = "Необходимо выполнить предписание\n";
				text += direction.GetData("НАИМЕНОВАНИЕ_ПРОИЗВОДИТЕЛЬ_ПРЕДПИСАНИЕ") + "\n";
				text += "Номер:" + (string)direction.GetData("НОМЕР_ПРЕДПИСАНИЕ") + " от " + ((DateTime)direction.GetData("ДАТА_ПРЕДПИСАНИЕ")).ToShortDateString() + "\n";
				text += (string)direction.GetData("ОПИСАНИЕ_ПРЕДПИСАНИЕ");
				MessageBox.Show(text);
			}
			
			// Остальные предписания, по винам
			array = new ArrayList();
			DbSqlAuto.SelectInArrayDirection(array, vin);
			foreach(object o in array)
			{
				string s = (string)o;
				string text = "Необходимо выполнить предписание:   " + s;
				MessageBox.Show(text);
			}

            // Остальные предписания, по винам - чистые VIN
            array = new ArrayList();
            DbSqlAuto.SelectInArrayDirectionVIN(array, vin);
            foreach (object o in array)
            {
                string s = (string)o;
                string text = "Необходимо выполнить предписание:   " + s;
                MessageBox.Show(text);
            }
		}
	}
}
