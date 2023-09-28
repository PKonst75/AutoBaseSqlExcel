using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ���� ������ � ������� ������� � ���� ������
	/// </summary>
	public class DbJournal:Db
	{
		private long		codeGroup;			// ��� ������ (����� �� ������ �������)
		private long		codeWorkPlace;		// ��� �������� �����
		private DateTime	date;				// ���� ������ � ������
		private int			codeTime;			// ��� ������� (15 ��������� ���������)
		private long		cardNumber;			// ����� ��������
		private int			cardYear;			// ��� ��������
		private string		comment;			// ���������� (��� ������� ��� ��������)

		private DbCard		tmpCard;			// ��������, �� ������� ������� ������

		private static SqlConnection conn;
		private static SqlCommand cmdAdd;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdRemove;
		private static SqlCommand cmdUpdate;
		private static SqlCommand cmdCountIntervals;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 6 ����������� ����� � ���������
			readerLength = 6 + DbCard.ReaderLength;

			conn = connection;

			cmdSelect = new SqlCommand("SELECT_������", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeWorkPlace", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@date", SqlDbType.DateTime);

			cmdAdd = new SqlCommand("WRITE_������_��������", conn);
			cmdAdd.CommandType = CommandType.StoredProcedure;
			cmdAdd.Parameters.Add("@codeGroup", SqlDbType.BigInt);
			cmdAdd.Parameters.Add("@codeWorkPlace", SqlDbType.VarChar);
			cmdAdd.Parameters.Add("@date", SqlDbType.DateTime);
			cmdAdd.Parameters.Add("@codeTime", SqlDbType.Int);
			cmdAdd.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdAdd.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdAdd.Parameters.Add("@comment", SqlDbType.VarChar);
			SetReturnError(cmdAdd);

			cmdRemove = new SqlCommand("WRITE_������_�������", conn);
			cmdRemove.CommandType = CommandType.StoredProcedure;
			cmdRemove.Parameters.Add("@codeGroup", SqlDbType.BigInt);
			cmdRemove.Parameters.Add("@date", SqlDbType.DateTime);
			cmdRemove.Parameters.Add("@codeWorkPlace", SqlDbType.BigInt);
			SetReturnError(cmdRemove);

			cmdCountIntervals = new SqlCommand("REPORT_�������_��������", conn);
			cmdCountIntervals.CommandType = CommandType.StoredProcedure;
			cmdCountIntervals.Parameters.Add("@codeWorkPlace", SqlDbType.VarChar);
			cmdCountIntervals.Parameters.Add("@date", SqlDbType.DateTime);

			string cmdText;


			cmdText = "UPDATE ������ SET [����� ��������]=@cardNumber, [��� ��������]=@cardYear, [��� �������]=@codeClient, ����������=@comment WHERE [��� ������]=@codeGroup";
			cmdUpdate = new SqlCommand(cmdText);
			cmdUpdate.Connection = conn;
			cmdUpdate.Parameters.Add("@codeGroup", SqlDbType.VarChar);
			cmdUpdate.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdUpdate.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdUpdate.Parameters.Add("@codeClient", SqlDbType.VarChar);
			cmdUpdate.Parameters.Add("@comment", SqlDbType.VarChar);
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdAdd.Transaction = trans;
		}
		#endregion

		#region ������������
		public DbJournal(DbWorkPlace workPlace, DateTime srcDate, int startIndex)
		{
			codeGroup		= startIndex;			// ��� ������ �� ���������� �������
			codeWorkPlace	= workPlace.Code;
			date			= srcDate;
			codeTime		= startIndex;
			cardNumber		= 0;
			cardYear		= 0;
			comment			= "";

			tmpCard			= null;
		}

		public DbJournal(DbJournal srcJournal, int codeTimeIndex)
		{
			codeGroup		= srcJournal.codeGroup;
			codeWorkPlace	= srcJournal.codeWorkPlace;
			date			= srcJournal.date;
			codeTime		= codeTimeIndex;
			cardNumber		= srcJournal.cardNumber;
			cardYear		= srcJournal.cardYear;
			comment			= srcJournal.comment;

			tmpCard			= srcJournal.tmpCard;
		}

		public DbJournal(SqlDataReader reader, int offset)
		{
			codeGroup		= (long)GetValueLong(reader, offset);		offset++;
			codeWorkPlace	= (long)GetValueLong(reader, offset);		offset++;
			date			= (DateTime)GetValueDate(reader, offset);	offset++;
			codeTime		= (int)GetValueInt(reader, offset);			offset++;
			cardNumber		= (long)GetValueLong(reader, offset);		offset++;
			cardYear		= (int)GetValueInt(reader, offset);			offset++;
			comment			= (string)GetValueString(reader, offset);		offset++;

			tmpCard			= new DbCard(reader, offset);				offset = offset + DbCard.ReaderLength;
			if ((cardNumber == 0) && (cardYear == 0)) tmpCard = null;
		}
		#endregion

		#region ������ � �������� ����������, ���������
		public int CodeTime
		{
			set
			{
				codeTime = value;
			}
			get
			{
				return codeTime;
			}
		}
		#endregion

		public static bool Add(DbJournal[] list, int start, int end)
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);
				for(int index = start; index <= end; index++)
				{
					list[index].Add();
				}
				
			}
			catch(Exception E)
			{
				SetException(E);
			}
			if(Db.ShowFaults())
			{
				if(trans != null) trans.Rollback();
				return false;
			}
			if(trans != null) trans.Commit();
			MessageBox.Show("���������");
			return true;
		}

		public static bool Remove(DbJournal journal)
		{
			try
			{
				cmdRemove.Parameters["@codeGroup"].Value		= (long)journal.CodeGroup;
				cmdRemove.Parameters["@codeWorkPlace"].Value	= (long)journal.CodeWorkPlace;
				cmdRemove.Parameters["@date"].Value				= (DateTime)journal.Date;
				cmdRemove.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
			}
			if(ShowFaults()) return false;
			return true;
		}

		public static int CountIntervals(long codeWorkPlace, DateTime date)
		{
			int		result;
			try
			{
				cmdCountIntervals.Parameters["@codeWorkPlace"].Value	= (long)codeWorkPlace;
				cmdCountIntervals.Parameters["@date"].Value				= (DateTime)date;
				result = (int)cmdCountIntervals.ExecuteScalar();
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return 0;
			}
			if(ShowFaults()) return 0;
			return result;
		}

		public static bool Update(DbJournal journal)
		{
			try
			{
				cmdUpdate.Parameters["@codeGroup"].Value = journal.codeGroup;
				cmdUpdate.Parameters["@cardNumber"].Value = journal.cardNumber;
				cmdUpdate.Parameters["@cardYear"].Value = journal.cardYear;
				cmdUpdate.Parameters["@comment"].Value	= journal.comment;
				cmdUpdate.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
			}
			if(ShowFaults()) return false;
			return true;
		}

		public bool Add()
		{
			try
			{
				cmdAdd.Parameters["@codeGroup"].Value		= (long)codeGroup;
				cmdAdd.Parameters["@codeWorkPlace"].Value	= (long)codeWorkPlace;
				cmdAdd.Parameters["@date"].Value			= (DateTime)date;
				cmdAdd.Parameters["@codeTime"].Value		= (int)codeTime;
				cmdAdd.Parameters["@cardNumber"].Value		= (long)cardNumber;
				cmdAdd.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdAdd.Parameters["@comment"].Value			= (string)comment;
				cmdAdd.ExecuteNonQuery();
				ThrowReturnError(cmdAdd);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public static bool Select(DbJournal[] list, DateTime date, long codeWorkPlace)
		{
			SqlDataReader reader = null;
			try
			{
				cmdSelect.Parameters["@codeWorkPlace"].Value = codeWorkPlace;
				cmdSelect.Parameters["@date"].Value = date;
				reader = cmdSelect.ExecuteReader();
				while(reader.Read())
				{
					DbJournal journal = new DbJournal(reader, 0);
					list[journal.CodeTime] = journal;
				}		
			}
			catch(Exception E)
			{
				SetException(E);
			}
			if(reader != null) reader.Close();
			if(Db.ShowFaults())
			{
				return false;
			}
			return true;
		}

		public long CodeGroup
		{
			get
			{
				return codeGroup;
			}
		}

		public long ExternCodeGroup
		{
			get
			{
				return codeGroup + codeWorkPlace * 100;
			}
		}

		public long CodeWorkPlace
		{
			get
			{
				return codeWorkPlace;
			}
		}

		public DateTime Date
		{
			get
			{
				return date;
			}
		}

		public string ToolTip
		{
			get
			{
				string toolText;
				if(tmpCard == null) return this.comment;
				toolText = tmpCard.CardTxt + "\n";
				toolText += tmpCard.PartnerNameTxt + "\n";
				toolText += "�������: " + tmpCard.PartnerPhoneTxt + "\n";
				toolText += "������ ����������: " + tmpCard.AutoTypeTxt + "\n";
				toolText += tmpCard.WorksTOTxt + "\n";
				toolText += tmpCard.WorksDiagTxt + "\n";
				toolText += tmpCard.WorksFailureTxt + "\n";
				toolText += tmpCard.Comment + "\n";
				toolText += this.comment + "\n";
				return toolText;
			}
		}

		public string CardTxt
		{
			get
			{
				if(tmpCard == null) return "�������� �� �������";
				return tmpCard.CardTxt;
			}
		}

		public void SetJournal(DbJournal journal)
		{
			// ��� ��������� ����� �������� ��� ������ � ������
			cardNumber	= journal.cardNumber;
			cardYear	= journal.cardYear;

			tmpCard		= journal.tmpCard;
		}

		public void SetCard(DbCard card)
		{
			if (card != null)
			{
				cardNumber	= card.Number;
				cardYear	= card.Year;

				tmpCard		= new DbCard(card);
			}
			else
			{
				cardNumber  = 0;
				cardYear	= 0;

				tmpCard = null;
			}
		}

		public void SetComment(string comment_text)
		{
			this.comment = comment_text;
		}
	}
}
