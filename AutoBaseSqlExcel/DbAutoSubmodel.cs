using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Подмодель автомобиля
	/// </summary>
	public class DbAutoSubmodel:Db
	{
		private long			code;
		private long			codeModel;
		private string			codeModelTxt;
		private string			codeEngineTxt;
		private long			codeEngineType;
		private string			body;
		private bool			fourWeelDrive;
		private long			codeTransmissionType;
		private int				productYearStart;
		private int				productYearEnd;

		// Временные
		private DbEngineType		tmpEngineType;			// Тип двигателя
		private DbTransmissionType	tmpTransmissionType;	// Тип коробки передач
		private DbAutoModel			tmpAutoModel;			// Модель к которой подмодель привязана

		// Связь с базой данных
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdSelectSubModelSubNode;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 10 собственных полей и остальное
			readerLength = 10 + DbEngineType.ReaderLength + DbTransmissionType.ReaderLength + DbAutoModel.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_АВТОМОБИЛЬ_ПОДМОДЕЛЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeModelTxt", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeEngineTxt", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@body", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@fourWeelDrive", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@codeTransmissionType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeEngineType", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@productYearStart", SqlDbType.Int);
			cmdWrite.Parameters.Add("@productYearEnd", SqlDbType.Int);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПОДМОДЕЛЬ", conn);
			cmdSelect.Parameters.Add("@codeModel", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@productYear", SqlDbType.Int);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectSubModelSubNode = new SqlCommand("SELECT_АВТОМОБИЛЬ_ПОДМОДЕЛЬ_ПОДУЗЕЛ", conn);
			cmdSelectSubModelSubNode.Parameters.Add("@codeSubNode", SqlDbType.BigInt);
			cmdSelectSubModelSubNode.Parameters.Add("@productYear", SqlDbType.Int);
			cmdSelectSubModelSubNode.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Конструкторы
		public DbAutoSubmodel(DbAutoModel src)
		{
			code					= 0;
			if(src != null)
				codeModel			= src.Code;
			else
				codeModel			= 0;
			codeModelTxt			= "";
			codeEngineTxt			= "";
			codeEngineType			= 0;
			body					= "";
			fourWeelDrive			= false;
			codeTransmissionType	= 0;
			productYearStart		= 0;
			productYearEnd			= 0;

			// Временные
			tmpEngineType			= null;
			tmpTransmissionType		= null;
			tmpAutoModel			= new DbAutoModel(src);

			adding					= true;
		}
		public DbAutoSubmodel(DbAutoSubmodel src)
		{
			code					= src.code;
			codeModel				= src.codeModel;
			codeModelTxt			= src.codeModelTxt;
			codeEngineTxt			= src.codeEngineTxt;
			codeEngineType			= src.codeEngineType;
			body					= src.body;
			fourWeelDrive			= src.fourWeelDrive;
			codeTransmissionType	= src.codeTransmissionType;
			productYearStart		= src.productYearStart;
			productYearEnd			= src.productYearEnd;

			// Временные
			tmpEngineType			= src.tmpEngineType;
			tmpTransmissionType		= src.tmpTransmissionType;
			tmpAutoModel			= src.tmpAutoModel;
		}
		public DbAutoSubmodel(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeModel				= (long)GetValueLong(reader, offset);		offset++;
			codeModelTxt			= (string)GetValueString(reader, offset);	offset++;
			codeEngineTxt			= (string)GetValueString(reader, offset);	offset++;
			codeEngineType			= (long)GetValueLong(reader, offset);		offset++;
			body					= (string)GetValueString(reader, offset);	offset++;
			fourWeelDrive			= (bool)GetValueBool(reader, offset);		offset++;
			codeTransmissionType	= (long)GetValueLong(reader, offset);		offset++;
			productYearStart		= (int)GetValueInt(reader, offset);			offset++;
			productYearEnd			= (int)GetValueInt(reader, offset);			offset++;
			
			tmpEngineType			= new DbEngineType(reader, offset);			offset = offset + DbEngineType.ReaderLength;
			tmpTransmissionType		= new DbTransmissionType(reader, offset);	offset = offset + DbTransmissionType.ReaderLength;
			tmpAutoModel			= new DbAutoModel(reader, offset);			offset = offset + DbAutoModel.ReaderLength;
		}
		#endregion

		#region Основные методы
		public bool Write()
		{
			if((adding == false)&&(changed == false)) return true; // Изменений нет

			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				
				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@codeModel"].Value				= (long)codeModel;
				cmdWrite.Parameters["@codeModelTxt"].Value			= (string)codeModelTxt;
				cmdWrite.Parameters["@codeEngineTxt"].Value			= (string)codeEngineTxt;
				cmdWrite.Parameters["@body"].Value					= (string)body;
				cmdWrite.Parameters["@fourWeelDrive"].Value			= (bool)fourWeelDrive;
				cmdWrite.Parameters["@codeTransmissionType"].Value	= (long)codeTransmissionType;
				cmdWrite.Parameters["@codeEngineType"].Value		= (long)codeEngineType;
				cmdWrite.Parameters["@productYearStart"].Value		= (int)productYearStart;
				cmdWrite.Parameters["@productYearEnd"].Value		= (int)productYearEnd;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			return true;
		}
		public void Check()
		{
			//if(codeEngineType == 0) SetDataWarning("Не выбран тип двигателя");
			//if(codeTransmissionType == 0) SetDataWarning("Не выбран тип коробки передач");
		}
		#endregion

		#region Доступ к основным параметрам - Чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Доступ к основным параметрам - Изменение
		public string CodeModelTxt
		{
			get
			{
				return codeModelTxt;
			}
			set
			{
				codeModelTxt = SetStringLength(codeModelTxt, value, 25, "Код модели");
			}
		}
		public string CodeEngineTxt
		{
			get
			{
				return codeEngineTxt;
			}
			set
			{
				codeEngineTxt = SetStringLength(codeEngineTxt, value, 25, "Код двигателя");
			}
		}
		public string Body
		{
			get
			{
				return body;
			}
			set
			{
				body = SetStringLength(body, value, 25, "Кузов");
			}
		}
		public bool FourWeelDrive
		{
			get
			{
				return fourWeelDrive;
			}
			set
			{
				if(value == fourWeelDrive) return;
				fourWeelDrive	= value;
				changed			= true;
			}
		}
		public string ProductYearStartTxt
		{
			get
			{
				if(productYearStart == 0) return "";
				return productYearStart.ToString();
			}
			set
			{
				if(value == "")
				{
					if(productYearStart == 0) return;
					productYearStart	= 0;
					changed				= true;
				}
				productYearStart		= SetIntNotMinus(productYearStart, value, "Год начала выпуска");
			}
		}
		public string ProductYearEndTxt
		{
			get
			{
				if(productYearEnd == 0) return "";
				return productYearEnd.ToString();
			}
			set
			{
				if(value == "")
				{
					if(productYearEnd == 0) return;
					productYearEnd	= 0;
					changed				= true;
				}
				productYearEnd		= SetIntNotMinus(productYearEnd, value, "Год окончания выпуска");
			}
		}
		public DbEngineType EngineType
		{
			get
			{
				return tmpEngineType;
			}
			set
			{
				if(value == null) return;
				if(value.Code == this.codeEngineType) return;
				tmpEngineType	= value;
				codeEngineType	= value.Code;
				changed			= true;
			}
		}
		public DbTransmissionType TransmissionType
		{
			get
			{
				return tmpTransmissionType;
			}
			set
			{
				if(value == null) return;
				if(value.Code == this.codeTransmissionType) return;
				tmpTransmissionType	= value;
				codeTransmissionType	= value.Code;
				changed			= true;
			}
		}
		#endregion

		#region Отображение в текст основных параметров
		public string EngineTypeTxt
		{
			get
			{
				if(codeEngineType == 0) return "";
				if(tmpEngineType == null) return "";
				return tmpEngineType.Description;
			}
		}
		public string TransmissionTypeTxt
		{
			get
			{
				if(codeTransmissionType == 0) return "";
				if(tmpTransmissionType == null) return "";
				return tmpTransmissionType.Description;
			}
		}
		public string AutoModelTxt
		{
			get
			{
				if(codeModel == 0) return "";
				if(tmpAutoModel == null) return "";
				return tmpAutoModel.Model;
			}
		}
		public string LongTitleTxt
		{
			get
			{
				string text = "";
				if(tmpAutoModel != null) text += tmpAutoModel.Model + " ";
				if(codeModelTxt.Length > 0) text += codeModelTxt + " ";
				if(codeEngineTxt.Length > 0) text += codeEngineTxt + " ";
				text += EngineTypeTxt + " ";
				text += TransmissionTypeTxt + " ";
				if(fourWeelDrive == true) text += "4x4 ";
				text += body + " ";
				text += ProductYearStartTxt + " - " + ProductYearEndTxt;
				return text;
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
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = codeModelTxt;
			item.SubItems[1].Text = codeEngineTxt;
			item.SubItems[2].Text = EngineTypeTxt;
			item.SubItems[3].Text = TransmissionTypeTxt;
			if(fourWeelDrive == true)
				item.SubItems[4].Text = "Да";
			else
				item.SubItems[4].Text = "";
			item.SubItems[5].Text = body;
			item.SubItems[6].Text = ProductYearStartTxt + " - " + ProductYearEndTxt;
			item.SubItems[7].Text = AutoModelTxt;
			item.Tag = this;
		}

		public static void FillList(ListView list, DbAutoModel model, int productYear)
		{
			cmdSelect.Parameters["@codeModel"].Value	= model.Code;
			cmdSelect.Parameters["@productYear"].Value	= productYear;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbSubNode subNode, int productYear)
		{
			cmdSelectSubModelSubNode.Parameters["@codeSubNode"].Value	= subNode.Code;
			cmdSelectSubModelSubNode.Parameters["@productYear"].Value	= productYear;
			Db.DbFillList(list, cmdSelectSubModelSubNode, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAutoSubmodel element = new DbAutoSubmodel(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array, DbAutoModel model, int productYear)
		{
			cmdSelect.Parameters["@codeModel"].Value	= model.Code;
			cmdSelect.Parameters["@productYear"].Value	= productYear;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbAutoSubmodel element = new DbAutoSubmodel(reader, 0);
			array.Add(element);
		}
		#endregion

		public override string DbTitle()
		{
			string txt;
			if (codeModelTxt.Length != 0) return codeModelTxt;
			if (codeEngineTxt.Length != 0) return "Двг. " + codeEngineTxt;
			txt = this.EngineTypeTxt + " " + this.TransmissionTypeTxt + " " + this.Body;
			return txt;
		}

		public override string[] Inform(int infoLevel)
		{
			if(code == 0) return null;
			string[]		text = new string[6];
			if((codeModelTxt.Length != 0)&&(codeEngineTxt.Length != 0))
				text[0] = codeModelTxt + " / " + codeEngineTxt;
			else
				text[0] = codeModelTxt + codeEngineTxt;
			text[1]		= "Двигатель :\t\t" + EngineTypeTxt;
			text[2]		= "Коробка передач :\t\t" + TransmissionTypeTxt;
			if(fourWeelDrive == true)
				text[3]	= "Полный привод :\t\t" + "ДА";
			else
				text[3]	= "Полный привод :\t\t" + "НЕТ";
			text[4]		= "Кузов :\t\t\t" + body;
			text[5]		= "Года выпуска :\t\t" + ProductYearStartTxt + " - " + ProductYearEndTxt;
			return text;
		}
	}
}
