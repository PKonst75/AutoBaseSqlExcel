using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ������������ � ���������� ��������� ��������
	/// </summary>
	public class DbCardAction:Db
	{
		private static SqlConnection conn;				// ���������� � �����
		private static SqlCommand cmdSelect;			// ����� �������� ������������� � ���������
		private static SqlCommand cmdWrite;				// ������ � ���� ��������
		private static SqlCommand cmdWriteCancelClose;	// ������ �������� ��������

		private long		cardNumber;		// ����� ��������
		private int			cardYear;		// ��� ��������
		private ActionCodes	actionCode;		// ��� ��������
		private	DateTime	date;			// ���� �������� (��� �������� �������)
		private string		comment;		// ����������
		private	long		warrantNumber;	// ����� ������ (��� ��������)
		private	int			run;			// ������ (��� ��������)

		public struct Analize
		{
			public long stop_count;
			public double first_stop;
			public double first_start;
			public double work_time;
			public TimeSpan first_stop_span;
			public TimeSpan first_start_span;
			public TimeSpan work_time_span;
			public bool	error;
		}

		// ���� ��������� �������� � ����������
		public enum ActionCodes:short
		{
			Non = 0,
			Open = 1,
			Close = 2,
			Stop = 3,
			Start = 4,
			Cancel = 5
		}

		public DbCardAction(DbCard card)
		{
			cardNumber		= card.Number;
			cardYear		= card.Year;
			actionCode		= 0;
			comment			= "";
			warrantNumber	= 0;
			run				= 0;
		}
		public DbCardAction(long card_number, int card_year)
		{
			cardNumber		= card_number;
			cardYear		= card_year;
			actionCode		= 0;
			comment			= "";
			warrantNumber	= 0;
			run				= 0;
		}

		public DbCardAction(DbCardAction cardAction)
		{
			cardNumber		= cardAction.cardNumber;
			cardYear		= cardAction.cardYear;
			actionCode		= cardAction.actionCode;
			date			= cardAction.date;
			comment			= cardAction.comment;

			warrantNumber	= cardAction.warrantNumber;
			run				= cardAction.run;
		}
		public DbCardAction(SqlDataReader reader, int offset)
		{
			short ac = 0;
			cardNumber				= (long)GetValueLong(reader, offset);		offset++;
			cardYear				= (int)GetValueInt(reader, offset);			offset++;
			ac						= (short)GetValueShort(reader, offset);	offset++;
			actionCode				= (ActionCodes)ac;
			date					= (DateTime)GetValueDate(reader, offset);	offset++;
			comment					= (string)GetValueString(reader, offset);	offset++;

			warrantNumber	= 0;
			run				= 0;
		}	

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// ������ �� ������������
			readerLength = 5;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_��������_��������", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@cardYear", SqlDbType.Int);
			cmdWrite.Parameters.Add("@actionCode", SqlDbType.SmallInt);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@run", SqlDbType.Int);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@warrantNumber", SqlDbType.BigInt);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@warrantNumber"].Direction = ParameterDirection.Output;
			cmdWrite.Parameters["@date"].Direction = ParameterDirection.Output;

			cmdWriteCancelClose = new SqlCommand("WRITE_��������_��������_������_�������", conn);
			cmdWriteCancelClose.CommandType = CommandType.StoredProcedure;
			cmdWriteCancelClose.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdWriteCancelClose.Parameters.Add("@cardYear", SqlDbType.Int);
			Db.SetReturnError(cmdWriteCancelClose);

			cmdSelect = new SqlCommand("SELECT_��������_��������", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@cardNumber", SqlDbType.BigInt);
			cmdSelect.Parameters.Add("@cardYear", SqlDbType.Int);
		}

		protected void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
			cmdWriteCancelClose.Transaction = trans;
		}

		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@cardNumber"].Value	= (long)cardNumber;
				cmdWrite.Parameters["@cardYear"].Value		= (int)cardYear;
				cmdWrite.Parameters["@actionCode"].Value	= (short)actionCode;
				cmdWrite.Parameters["@comment"].Value		= (string)comment;
				cmdWrite.Parameters["@run"].Value			= (int)run;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				warrantNumber	= (long)cmdWrite.Parameters["@warrantNumber"].Value;
				date			= (DateTime)cmdWrite.Parameters["@date"].Value;
			}
			catch(Exception E)
			{
				trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("������ �������");
			return true;
		}

		public bool WriteCancelClose()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWriteCancelClose.Parameters["@cardNumber"].Value	= (long)cardNumber;
				cmdWriteCancelClose.Parameters["@cardYear"].Value	= (int)cardYear;
				cmdWriteCancelClose.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteCancelClose);
			}
			catch(Exception E)
			{
				trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("������ �������");
			return true;
		}

		public string Comment
		{
			set
			{
				comment = value;
			}
		}

		public ActionCodes ActionCode
		{
			set
			{
				actionCode = value;
			}
		}

		public int Run
		{
			set
			{
				run = value;
			}
			get
			{
				return run;
			}
		}

		public long WarrantNumber
		{
			get
			{
				return warrantNumber;
			}
		}
		public DateTime Date
		{
			get
			{
				return date;
			}
		}
		public static void FillArray(ArrayList array, DbCard card)
		{
			cmdSelect.Parameters["@cardNumber"].Value = card.Number;
			cmdSelect.Parameters["@cardYear"].Value = card.Year;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCardAction element = new DbCardAction(reader, 0);
			array.Add(element);
		}

		public static Analize AnalizeIt(DbCard card)
		{
			TimeSpan	span;
			long count_stop = 0;
			long count_start = 0;
			bool is_open = false;
			bool is_close = false;
			DbCardAction	action;
			DateTime stop_date = DateTime.Now;
			DateTime start_date = DateTime.Now;
			DateTime open_date = DateTime.Now;
			DateTime close_date = DateTime.Now;
			// ����������� �������� ������������� � ���������
			Analize analiz;
			analiz.first_start	= 0.0;
			analiz.first_stop	= 0.0;
			analiz.stop_count	= 0;
			analiz.work_time	= 0.0;
			analiz.first_stop_span	= new TimeSpan(0);
			analiz.first_start_span	= new TimeSpan(0);
			analiz.work_time_span	= new TimeSpan(0);
			analiz.error		= true;

			ArrayList actions = new ArrayList();
			DbCardAction.FillArray(actions, card);

			// ��������� ���� ������
			foreach(object e in actions)
			{
				action = (DbCardAction)e;
				if((short)action.actionCode == 1)
				{
					is_open = true;
					open_date = action.Date;
				}
				if((short)action.actionCode == 2)
				{
					is_close = true;
					close_date = action.Date;
				}
				if((short)action.actionCode == 3) count_stop++;
				if((short)action.actionCode == 4) count_start++;
			}
			// ��������� ������
			if (actions.Count < 2) return analiz;			// ��� ������� ����� - ������
			if (is_open == false) return analiz;			// ��� ������� ����� - ������
			if (is_close == false) return analiz;			// ��� ������� ����� - ������
			if (count_start != count_stop) return analiz;	// ��� ������� ����� - ������
			// ��������� ������ �������
			analiz.stop_count = count_stop;

			// ���������� ������ ���� ����� ��������
			action = (DbCardAction)actions[0];
			if((short)action.actionCode != 1) return analiz; // ������ �������� �� �������� - ������
			action = (DbCardAction)actions[actions.Count - 1];
			if((short)action.actionCode != 2) return analiz; // ��������� �������� �� �������� - ������

			if(count_stop == 0)
			{
				// ������ ��� ������������
				span = close_date - open_date;
				analiz.work_time = span.TotalHours;
				analiz.work_time_span = span;
			}
			else
			{
				// ������������ ������ ����
				// ����������� ��� �� ����������
				action = (DbCardAction)actions[1];
				stop_date	= action.Date;
				span = stop_date - open_date;
				analiz.first_stop = span.TotalHours;
				analiz.first_stop_span = span;
				action = (DbCardAction)actions[2];
				start_date	= action.Date;
				span = start_date - stop_date;
				analiz.first_start = span.TotalHours;
				analiz.first_start_span = span;

				for(int i = 3; i < actions.Count; i++)
				{
					action = (DbCardAction)actions[i];
					if((short)action.actionCode == 3)
					{
						start_date = action.Date;
					}
					else
					{
						stop_date = action.Date;
						span = stop_date - start_date;
						analiz.work_time += span.Hours;
						analiz.work_time_span += span;
					}
				}
			}
			
			analiz.error = false;
			return analiz;
		}
	}
}
