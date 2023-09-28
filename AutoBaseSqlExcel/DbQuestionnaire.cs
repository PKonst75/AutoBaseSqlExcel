using System;
using System.Data.SqlClient;
using System.Data;

namespace AutoBaseSql
{
	/// <summary>
	/// Анкета клиента
	/// </summary>
	public class DbQuestionnaire:Db
	{
		private long		code;
		private DateTime	date;
		private long		codePartner;
		private string		partnerTxt;
		private bool		cash;
		private bool		credit;
		private bool		tradein;
		private string		comment;
		
		// Дополнительные описания
		private DbPartner		tmpPartner;			// Клиент из базы

		// Взаимодействие с базой
		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}		

		#region Конструкторы
		public DbQuestionnaire()
		{
			code				= 0;
			date				= DateTime.Now;
			codePartner			= 0;
			partnerTxt			= "";
			cash				= false;
			credit				= false;
			tradein				= false;
			comment				= "";

			tmpPartner			= null;
		}
		public DbQuestionnaire(DbQuestionnaire src)
		{
			code				= src.code;
			date				= src.date;
			codePartner			= src.codePartner;
			partnerTxt			= src.partnerTxt;
			cash				= src.cash;
			credit				= src.credit;
			tradein				= src.tradein;
			comment				= src.comment;

			tmpPartner			= src.tmpPartner;
		}
		public DbQuestionnaire(SqlDataReader reader, int offset)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			date				= (DateTime)GetValueDate(reader, offset);	offset++;
			codePartner			= (long)GetValueLong(reader, offset);		offset++;
			partnerTxt			= (string)GetValueString(reader, offset);	offset++;;
			cash				= (bool)GetValueBool(reader, offset);		offset++;;
			credit				= (bool)GetValueBool(reader, offset);		offset++;;
			tradein				= (bool)GetValueBool(reader, offset);		offset++;;
			comment				= (string)GetValueString(reader, offset);	offset++;;

			tmpPartner			= new DbPartner(reader, offset);			offset += DbPartner.ReaderLength;
		}
		#endregion

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 8 собственных полей и длина ридера класса DbParnter
			readerLength = 8 + DbPartner.ReaderLength;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_КЛИЕНТ_АНКЕТА", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@date", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@codePartner", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@partnerTxt", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@cash", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@credit", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@tradein", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;
			cmdWrite.Parameters["@date"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_КЛИЕНТ_АНКЕТА", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction		= trans;
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
				
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@date"].Value			= (DateTime)date;
				cmdWrite.Parameters["@codePartner"].Value	= (long)codePartner;
				cmdWrite.Parameters["@partnerTxt"].Value	= (string)partnerTxt;
				cmdWrite.Parameters["@cash"].Value			= (bool)cash;
				cmdWrite.Parameters["@credit"].Value		= (bool)credit;
				cmdWrite.Parameters["@tradein"].Value		= (bool)tradein;
				cmdWrite.Parameters["@comment"].Value		= (string)comment;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code		= (long)cmdWrite.Parameters["@code"].Value;
				date		= (DateTime)cmdWrite.Parameters["@date"].Value;
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
		#endregion
	}
}
