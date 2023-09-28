using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbAccountDetail.
	/// </summary>
	public class DbAccountDetail:Db
	{
		private long number;
		private int year;
		private DateTime date;
		private string codeClient;
		private string codeSeller;
		private long codeStaff;
		private string comment;

		private DbPartner tmpClient;
		private DbPartner tmpSeller;
		private DbStaff tmpStaff;
		private float tmpSumm;

		private static SqlConnection conn;
		private static SqlCommand cmdExecWrite;
		private static SqlCommand cmdExecSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 7 собственных полей и остальное
			readerLength = 7 + DbPartner.ReaderLength + DbPartner.ReaderLength + DbStaff.ReaderLength + 1;

			conn = connection;

			cmdExecWrite = new SqlCommand("WRITE_ACCOUNT_DETAIL");
			cmdExecWrite.Connection = conn;
			cmdExecWrite.CommandType = CommandType.StoredProcedure;
			cmdExecWrite.Parameters.Add("@number", SqlDbType.BigInt);
			cmdExecWrite.Parameters["@number"].Direction = ParameterDirection.InputOutput;
			cmdExecWrite.Parameters.Add("@year", SqlDbType.Int);
			cmdExecWrite.Parameters["@year"].Direction = ParameterDirection.InputOutput;
			cmdExecWrite.Parameters.Add("@codeClient", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@codeStaff", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@codeSeller", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdExecWrite.Parameters.Add("@ERROR", SqlDbType.VarChar, 60);
			cmdExecWrite.Parameters["@ERROR"].Direction = ParameterDirection.Output;
			cmdExecWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdExecWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdExecWrite.Parameters["@date"].Direction = ParameterDirection.InputOutput;
			cmdExecWrite.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
			cmdExecWrite.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;

			cmdExecSelect = new SqlCommand("ACCOUNT_DETAIL_VIEW");
			cmdExecSelect.Connection = conn;
			cmdExecSelect.CommandType = CommandType.StoredProcedure;
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdExecWrite.Transaction = trans;
		}

		public DbAccountDetail()
		{
			number = 0;
			year = 0;
			date = DateTime.Today;
			codeClient = "";
			codeSeller = "";
			codeStaff = 0;
			comment = "";

			tmpStaff = null;
			tmpClient = null;
			tmpSeller = null;

			adding = true;
		}

		public DbAccountDetail(DbAccountDetail source)
		{
			number = source.number;
			year = source.year;
			date = source.date;
			codeClient = source.codeClient;
			codeSeller = source.codeSeller;
			codeStaff = source.codeStaff;
			comment = source.comment;

			tmpClient = source.tmpClient;
			tmpSeller = source.tmpSeller;
			tmpStaff = source.tmpStaff;
		}

		public DbAccountDetail(SqlDataReader reader, int offset)
		{
			number			= (long)GetValueLong(reader, offset);		offset++;
			year			= (int)GetValueInt(reader, offset);			offset++;
			date			= (DateTime)GetValueDate(reader, offset);	offset++;
			codeClient		= (string)GetValueString(reader, offset);	offset++;
			codeSeller		= (string)GetValueString(reader, offset);	offset++;
			codeStaff		= (long)GetValueLong(reader, offset);		offset++;
			comment			= (string)GetValueString(reader, offset);	offset++;

			tmpSeller		= new DbPartner(reader, offset);			offset = offset + DbPartner.ReaderLength;
			tmpClient		= new DbPartner(reader, offset);			offset = offset + DbPartner.ReaderLength;
			tmpStaff		= new DbStaff(reader, offset);				offset = offset + DbStaff.ReaderLength;

			tmpSumm			= GetValueFloat(reader, offset);
		}

		public bool Write()
		{
			try
			{
				cmdExecWrite.Parameters["@number"].Value = (long)number;
				cmdExecWrite.Parameters["@year"].Value = (int)year;
				cmdExecWrite.Parameters["@date"].Value = date;
				cmdExecWrite.Parameters["@codeClient"].Value = (string)codeClient;
				cmdExecWrite.Parameters["@codeSeller"].Value = (string)codeSeller;
				cmdExecWrite.Parameters["@codeStaff"].Value = (long)codeStaff;
				cmdExecWrite.Parameters["@comment"].Value = (string)comment;
				cmdExecWrite.Parameters["@adding"].Value = (bool)adding;
				cmdExecWrite.ExecuteNonQuery();
				if((int)cmdExecWrite.Parameters["RETURN_VALUE"].Value != 0)
				{
					if(cmdExecWrite.Parameters["@ERROR"].Value != null && cmdExecWrite.Parameters["@ERROR"].Value != System.DBNull.Value)
						throw new ApplicationException((string)cmdExecWrite.Parameters["@ERROR"].Value);
				}
				// Установка полученных от сервера данных
				date = (DateTime)cmdExecWrite.Parameters["@date"].Value;
				year = (int)cmdExecWrite.Parameters["@year"].Value;
				number = (long)cmdExecWrite.Parameters["@number"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		// Считается, что нашли какие-то изменения
		public bool Update(ListView list)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction(IsolationLevel.Serializable);
				SetTransaction(trans);
				Write();
				DbAccountDetailItem.UpdateList(list, this, trans);
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
			MessageBox.Show("Успешно добавили/изменили счет");
			return true;
		}

		public DbPartner Client
		{
			set
			{
			//	if(codeClient != value.Code)
			//	{
			//		codeClient = value.Code;
			//		tmpClient = value;
			//		changed = true;
			//	}
			}
			get
			{
				return tmpClient;
			}
		}

		public DbPartner Seller
		{
			set
			{
			//	if(codeSeller != value.Code)
			//	{
			//		codeSeller = value.Code;
			//		tmpSeller = value;
			//		changed = true;
			//	}
			}
			get
			{
				return tmpSeller;
			}
		}

		public DbStaff Staff
		{
			set
			{
				if(codeStaff != value.Code)
				{
					codeStaff = value.Code;
					tmpStaff = value;
					changed = true;
				}
			}
			get
			{
				return tmpStaff;
			}
		}

		public string ClientTitle
		{
			get
			{
				if(tmpClient == null) return "";
				return tmpClient.Title;
			}
		}

		public string SellerTitle
		{
			get
			{
				if(tmpSeller == null) return "";
				return tmpSeller.Title;
			}
		}

		public string StaffTitle
		{
			get
			{
				if(tmpStaff == null) return "";
				return tmpStaff.FirstName;
			}
		}

		public string Comment
		{
			set
			{
				comment = this.SetStringLength(comment, value, 120, "Комментарий к счету");
			}
			get
			{
				return comment;
			}
		}

		public string NumberTxt
		{
			get
			{
				return number.ToString() + "/" + year.ToString();
			}
		}

		public long Number
		{
			get
			{
				return number;
			}
		}

		public int Year
		{
			get
			{
				return year;
			}
		}

		public string SummTxt
		{
			get
			{
				return Db.CachToTxt(tmpSumm);
			}
		}

		public float Summ
		{
			set
			{
				tmpSumm = MakeFloat(value);
			}
		}

		public void IsValid()
		{
			if(tmpClient == null) SetDataWarning("Не заполнен покупатель");
			if(tmpSeller == null) SetDataWarning("Не заполнен продавец");
			if(tmpStaff == null) SetDataWarning("Не заполнен менеджер");
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = NumberTxt;
			item.SubItems[1].Text = Db.DateToTxt(date);
			item.SubItems[2].Text = ClientTitle;
			item.SubItems[3].Text = SummTxt;
			item.SubItems[4].Text = Comment;
			item.Tag = this;
		}

		public static void FillList(ListView list, SqlCommand cmd)
		{
			if(cmd == null) cmd = cmdExecSelect;
			Db.DbFillList(list, cmd, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAccountDetail element = new DbAccountDetail(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion
	}
}
