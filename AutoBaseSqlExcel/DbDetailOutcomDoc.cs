using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailOutcomDoc.
	/// </summary>
	public class DbDetailOutcomDoc:Db
	{
		private long		code;		// Код требования
		private long		number;		// Номер требования
		private DateTime	date;		// Дата требования
		private long		codeGive;	// Код выдавшего персонала
		private long		codeGet;	// Код получившего персонала
		private string		based;		// Документ основание
		private bool		implement;	// Проведен - в данном случае означает что выдано на руки
		private bool		guaranty;	// Гарантийное требование (цена расхода равна цене прихода)

		DbStaff				tmpGive;
		DbStaff				tmpGet;
		float				tmpSumm;			// Сумма требования
		int					tmpNotImplemented;	// Не списанных с прихода позиций	

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdDelete;
		private static SqlCommand cmdImplement;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 8 собственных полей и остальное
			readerLength = 8 + 1 + 1 + DbStaff.ReaderLength + DbStaff.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_РАСХОД_ДОКУМЕНТ");
			cmdWrite.Connection = conn;
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@number", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@codeGive", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeGet", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@based", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@guaranty", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			SetReturnError(cmdWrite);
			cmdWrite.Parameters["@number"].Direction = ParameterDirection.InputOutput;
			cmdWrite.Parameters["@date"].Direction = ParameterDirection.InputOutput;
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ_РАСХОД_ДОКУМЕНТ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdImplement = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_РАСХОД_ДОКУМЕНТ_ПРОВОДКА", conn);
			cmdImplement.CommandType = CommandType.StoredProcedure;
			cmdImplement.Parameters.Add("@code", SqlDbType.BigInt);
			cmdImplement.Parameters.Add("@implement", SqlDbType.Bit);
			Db.SetReturnError(cmdImplement);

			cmdDelete = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_РАСХОД_ДОКУМЕНТ_УДАЛИТЬ", conn);
			cmdDelete.CommandType = CommandType.StoredProcedure;
			cmdDelete.Parameters.Add("@code", SqlDbType.BigInt);
			Db.SetReturnError(cmdDelete);
		}
		#endregion

		#region Конструкторы
		public DbDetailOutcomDoc()
		{
			code		= 0;
			number		= 0;
			date		= DateTime.Now;
			codeGive	= 0;
			codeGet		= 0;
			based		= "";
			implement	= false;
			guaranty	= false;

			tmpGive				= null;
			tmpGet				= null;
			tmpSumm				= 0.0F;
			tmpNotImplemented	= 0;

			adding = true;
		}

		public DbDetailOutcomDoc(DbDetailOutcomDoc source)
		{
			code		= source.code;
			number		= source.number;
			date		= source.date;
			codeGive	= source.codeGive;
			codeGet		= source.codeGet;
			based		= source.based;
			implement	= source.implement;
			guaranty	= source.guaranty;

			tmpGive				= source.tmpGive;
			tmpGet				= source.tmpGet;
			tmpSumm				= source.tmpSumm;
			tmpNotImplemented	= source.tmpNotImplemented;
		}

		public DbDetailOutcomDoc(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);			offset++;
			number		= (long)GetValueLong(reader, offset);			offset++;
			date		= (DateTime)GetValueDate(reader, offset);		offset++;
			codeGive	= (long)GetValueLong(reader, offset);			offset++;
			codeGet		= (long)GetValueLong(reader, offset);			offset++;
			based		= (string)GetValueString(reader, offset);		offset++;
			implement	= (bool)GetValueBool(reader, offset);			offset++;
			guaranty	= (bool)GetValueBool(reader, offset);			offset++;

			tmpSumm				= (float)GetValueFloat(reader, offset);	offset++;
			tmpNotImplemented	= (int)GetValueInt(reader, offset);		offset++;
			tmpGive				= new DbStaff(reader, offset);			offset = offset + DbStaff.ReaderLength;
			tmpGet				= new DbStaff(reader, offset);			offset = offset + DbStaff.ReaderLength;
		}
		#endregion

		#region Отображение основных параметров в текст
		public string GiveTxt
		{
			get
			{
				if(tmpGive == null) return "";
				return tmpGive.FirstName + " " + tmpGive.Name + " " + tmpGive.SecondName;
			}
		}

		public string GetTxt
		{
			get
			{
				if(tmpGet == null) return "";
				return tmpGet.FirstName + " " + tmpGet.Name + " " + tmpGet.SecondName;
			}
		}
		#endregion

		public DbStaff Give
		{
			set
			{
				if(value == null) return;
				codeGive = value.Code;
				tmpGive = value;
			}
		}

		public DbStaff Get
		{
			set
			{
				if(value == null) return;
				codeGet = value.Code;
				tmpGet = value;
			}
		}

		public string Based
		{
			set
			{
				based = this.SetStringNotEmptyLength(based, value, 120, "ОСНОВАНИЕ");
			}
			get
			{
				return based;
			}
		}

		public string NumberTxt
		{
			get
			{
				return number.ToString();
			}
			set
			{
				number = this.SetLongNotZero(number, value, "НОМЕР");
			}
		}

		public string DateTxt
		{
			get
			{
				return date.ToShortDateString();
			}
		}

		public string SummTxt
		{
			get
			{
				return Db.CachToTxt(tmpSumm);
			}
		}

		#region Доступ к основным параметрам - изменение
		public long Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}
		public bool Implement
		{
			get
			{
				return implement;
			}
			set
			{
				implement = value;
			}
		}
		public float Summ
		{
			set
			{
				tmpSumm = value;
			}
		}
		public int NotImplemented
		{
			set
			{
				tmpNotImplemented = value;
			}
		}
		public bool Guaranty
		{
			get
			{
				return guaranty;
			}
			set
			{
				if(guaranty == value) return;
				guaranty	= value;
				changed		= true;
			}
		}
		public DateTime Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
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
		#endregion

		public void IsValid()
		{
			if(tmpGive == null) SetDataWarning("КТО ОТПУСТИЛ");
			if(tmpGet == null) SetDataWarning("КТО ПОЛУЧИЛ");
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
			item.Text = NumberTxt;
			item.SubItems[1].Text = DateTxt;
			item.SubItems[2].Text = SummTxt;
			item.SubItems[3].Text = based;
			item.BackColor = Color.White;
			item.ForeColor = Color.Black;
			if(tmpNotImplemented > 0)
			{
				item.BackColor = Color.Yellow;
			}
			if(guaranty == true)
			{
				item.ForeColor = Color.Blue;
			}
			if(implement == false)
			{
				item.ForeColor = Color.Red;
			}
			item.Tag = this;
		}
		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDetailOutcomDoc element = new DbDetailOutcomDoc(reader, 0);
			list.Items.Add(element.LVItem);
		}

		#endregion
		
		public bool Update(ListView list)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);
				Write();
				DbDetailOutcom.UpdateList(list, this, trans);
				if(ShowFaults())
				{
					if(trans != null)trans.Rollback();
					return false;
				}
				if(trans != null)trans.Commit();
			}
			catch(Exception E)
			{
				if(trans != null)trans.Rollback();
				SetException(E);
				ShowFaults();
				return false;
			}
			MessageBox.Show("Успешно добавили/изменили требование");
			return true;
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@code"].Value		= (long)code;
				cmdWrite.Parameters["@number"].Value	= (long)number;
				cmdWrite.Parameters["@date"].Value		= (DateTime)date;
				cmdWrite.Parameters["@codeGive"].Value	= (long)codeGive;
				cmdWrite.Parameters["@codeGet"].Value	= (long)codeGet;
				cmdWrite.Parameters["@based"].Value		= (string)based;
				cmdWrite.Parameters["@guaranty"].Value	= (bool)guaranty;
				cmdWrite.Parameters["@adding"].Value	= (bool)adding;
				cmdWrite.ExecuteNonQuery();
				ThrowReturnError(cmdWrite);
				// Установка полученых от сервера данных
				code		= (long)cmdWrite.Parameters["@code"].Value;
				number		= (long)cmdWrite.Parameters["@number"].Value;
				date		= (DateTime)cmdWrite.Parameters["@date"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public bool MakeImplement(bool implement)
		{
			try
			{
				cmdImplement.Parameters["@code"].Value = (long)code;
				cmdImplement.Parameters["@implement"].Value = (bool)implement;
				cmdImplement.ExecuteNonQuery();
				Db.ThrowReturnError(cmdImplement);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			this.Implement = implement;
			return true;
		}

		public bool Delete()
		{
			try
			{
				cmdDelete.Parameters["@code"].Value			= (long)code;
				cmdDelete.ExecuteNonQuery();
				ThrowReturnError(cmdDelete);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}
	}
}
