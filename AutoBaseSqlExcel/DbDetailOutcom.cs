using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailOutcom.
	/// </summary>
	public class DbDetailOutcom:Db
	{
		private long	code;					// Уникальный код
		private long	codeDetailStorage;		// Код складской позиции
		private long	codeDetailOutcomDoc;	// Код требования
		private float	quontity;				// Затребованное количество
		private float	nds;					// НДС
		private float	price;					// Отпускная цена (c НДС)
		private long	codeDetailIncom;		// Код прихода, с которого списали

		private DbDetailStorage tmpDetailStorage;	// Складская позиция
		private DbDetailIncom	tmpDetailIncom;		// Приход с которого списали
		
		private bool tmpExists;

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;

		private static SqlCommand cmdSelect;
		
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
			// 7 собственных полей и остальное
			readerLength = 7 + DbDetailStorage.ReaderLength + DbDetailIncom.ReaderLength;

			conn = connection;

			cmdSelect = new SqlCommand("SELECT_СКЛАД_ДЕТАЛЬ_РАСХОД");
			cmdSelect.Connection = conn;
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeOutcomDoc", SqlDbType.BigInt);

			cmdWrite = new SqlCommand("WRITE_СКЛАД_ДЕТАЛЬ_РАСХОД");
			cmdWrite.Connection = conn;
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetailStorage", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeDetailOutcomDoc", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@quontity", SqlDbType.Real);
			cmdWrite.Parameters.Add("@nds", SqlDbType.Real);
			cmdWrite.Parameters.Add("@price", SqlDbType.Real);
			cmdWrite.Parameters.Add("@codeDetailIncom", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@delete", SqlDbType.Bit);
			SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;
		}
		#endregion

		private void SetDocument(DbDetailOutcomDoc doc)
		{
			codeDetailOutcomDoc		= doc.Code;
		}

		public void SetConnectedPosition(DbCardDetail cardDetail)
		{
		//	this.tmpNumber = cardDetail.Number;
		//	this.tmpDocNumber = cardDetail.CardNumber;
		//	this.tmpDocYear = cardDetail.CardYear;
		}

		
		#region Конструкторы
		public DbDetailOutcom(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeDetailStorage		= (long)GetValueLong(reader, offset);		offset++;
			codeDetailOutcomDoc		= (long)GetValueLong(reader, offset);		offset++;
			quontity				= (float)GetValueFloat(reader, offset);		offset++;
			nds						= (float)GetValueFloat(reader, offset);		offset++;
			price					= (float)GetValueFloat(reader, offset);		offset++;
			codeDetailIncom			= (long)GetValueLong(reader, offset);		offset++;
			
			tmpDetailStorage		= new DbDetailStorage(reader, offset);		offset = offset + DbDetailStorage.ReaderLength;
			tmpDetailIncom			= new DbDetailIncom(reader, offset);		offset = offset + DbDetailIncom.ReaderLength;

			tmpExists = true;
		}
		public DbDetailOutcom(DbDetailOutcom source)
		{
			code					= source.code;
			codeDetailStorage		= source.codeDetailStorage;
			codeDetailOutcomDoc		= source.codeDetailOutcomDoc;
			quontity				= source.quontity;
			nds						= source.nds;
			price					= source.price;
			codeDetailIncom			= source.codeDetailIncom;

			tmpDetailStorage		= source.tmpDetailStorage;
			tmpDetailIncom			= source.tmpDetailIncom;
		}
		public DbDetailOutcom(DbDetailStorage source)
		{
			code					= 0;
			codeDetailStorage		= source.Code;
			codeDetailOutcomDoc		= 0;
			quontity				= 1;
			nds						= 18;
			price					= source.Price;
			codeDetailIncom			= 0;

			tmpDetailStorage		= new DbDetailStorage(source);
			tmpDetailIncom			= null;

			adding					= true;
			tmpExists				= false;
		}

		public DbDetailOutcom(DbDetailIncomDetailed source)
		{
			code					= 0;
			codeDetailStorage		= source.CodeStorageDetail;
			codeDetailOutcomDoc		= 0;
			quontity				= source.Quontity;
			nds						= source.Nds;
			price					= source.Price;
			codeDetailIncom			= source.Code;

			tmpDetailStorage		= source.DetailStorage;
			tmpDetailIncom			= source.DetailIncom;

			adding					= true;
			tmpExists				= false;
		}

		public DbDetailOutcom(DbCardDetail source)
		{
		//	codeDetail = source.CodeDetail;
		//	codeFirm = source.CodeFirm;
		//	quontity = source.Quontity;
		//	returnQuontity = 0;

	//		tmpDetail = source.Detail;
		//	tmpLimit = quontity;

		//	adding = true;
		//	tmpExists = false;
		//	tmpReturningReason = "";
		}
		#endregion

		#region Отображение параметров в текст
		public string CodeDetailTxt
		{
			get
			{
				if(tmpDetailStorage == null) return "";
				return tmpDetailStorage.DetailCodeTxt;
			}
		}
		public string PriceIncomTxt
		{
			get
			{
				if(codeDetailIncom == 0) return "---";
				if(tmpDetailIncom == null) return "---";
				return tmpDetailIncom.PriceTxt;
			}
		}
		#endregion

		public bool Exists
		{
			get
			{
				return tmpExists;
			}
		}

		public bool Deleted
		{
			get
			{
				return deleted;
			}
			set
			{
				if(deleted == value) return;
				deleted = value;
				changed = true;
			}
		}

		public string QuontityTxt
		{
			get
			{
				return quontity.ToString();
			}
			set
			{
				quontity = this.SetFloatNotMinus(quontity, value, "КОЛИЧЕСТВО");
			}
		}
		public long CodeDetailIncom
		{
			get
			{
				return codeDetailIncom;
			}
			set
			{
				if(value < 0) return;
				if(value == codeDetailIncom) return;
				changed = true;
				codeDetailIncom = value;
			}
		}
		public DbDetailIncom DetailIncom
		{
			get
			{
				return tmpDetailIncom;
			}
			set
			{
				if((value == null)&&(codeDetailIncom == 0)) return;
				if(value.Code == codeDetailIncom) return;
				if(value == null)
				{
					tmpDetailIncom = null;
					codeDetailIncom = 0;
					changed = true;
					return;
				}
				changed = true;
				codeDetailIncom = value.Code;
				tmpDetailIncom = value;
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
				nds = this.SetFloatNotMinus(nds, value, "НДС");
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
				price = this.SetFloatNotMinus(price, value, "ЦЕНА");
			}
		}

		public string PriceNoNdsTxt
		{
			get
			{
				float priceNoNds	= 100.0F * price / (100.0F + nds);
				return Db.CachToTxt(priceNoNds);
			}
			set
			{
				float priceNoNds = 100.0F * price / (100.0F + nds);
				priceNoNds	= this.SetFloatNotMinus(priceNoNds, value, "Цена без НДС");
				price = this.SetFloatNotMinus(price, priceNoNds + priceNoNds * nds / 100.0F, "Цена с НДС");
			}
		}

		public string SummTxt
		{
			get
			{
				return Db.CachToTxt(Summ);
			}
		}

		public float Summ
		{
			get
			{
				return MakeFloat(quontity * price);
			}
		}

		public string FullName
		{
			get
			{
				if(tmpDetailStorage == null)return "";
				return tmpDetailStorage.DetailName;
			}
		}

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
			item.SubItems[1].Text = FullName;
			item.SubItems[2].Text = QuontityTxt;
			item.SubItems[3].Text = PriceNoNdsTxt;
			item.SubItems[4].Text = NdsTxt;
			item.SubItems[5].Text = PriceTxt;
			item.SubItems[6].Text = SummTxt;
			item.SubItems[7].Text = PriceIncomTxt;

			item.BackColor = Color.White;
			if(tmpExists == false) item.BackColor = Color.Yellow;
			if(codeDetailIncom	== 0) item.BackColor = Color.LightPink;
			if(Deleted == true) item.BackColor = Color.Gray;
			if(tmpDetailStorage != null && tmpDetailStorage.State == true) item.ForeColor = Color.Red;
			
			item.Tag = this;
		}

		public void IsValid()
		{
			if(price <= 0.0F) SetDataWarning("ЦЕНА");
			if(nds < 0.0F) SetDataWarning("НДС");
			if(quontity <= 0.0F) SetDataWarning("КОЛИЧЕСТВО");
		}

		public float Quontity
		{
			get
			{
				return quontity;
			}
			set
			{
				quontity = value;
			}
		}

		public long CodeDetailStorage
		{
			get
			{
				return codeDetailStorage;
			}
		}

		public DbDetailStorage DetailStorage
		{
			get
			{
				return tmpDetailStorage;
			}
		}

		public string DetailStorageName
		{
			get
			{
				if(tmpDetailStorage == null) return "ОШИБКА";
				return tmpDetailStorage.DetailName;
			}
		}

		public static void FillList(ListView list, SqlCommand cmd)
		{
			if(cmd == null) cmd = cmdSelect;
			SqlDataReader reader = null;
			try
			{
				reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					DbDetailOutcom element = new DbDetailOutcom(reader, 0);
					list.Items.Add(element.LVItem);
				}
				reader.Close();
			}
			catch(Exception E)
			{
				if(reader != null) reader.Close();
				SetException(E);
			}
			if(reader != null) reader.Close();
			ShowFaults();
		}

		public static void FillList(ListView list, DbDetailOutcomDoc doc)
		{
			cmdSelect.Parameters["@codeOutcomDoc"].Value = doc.Code;
			FillList(list, cmdSelect);
		}

		public static void FillList(ArrayList list, SqlCommand cmd)
		{
			if(cmd == null) return;
			SqlDataReader reader = null;
			try
			{
				reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					DbDetailOutcom element = new DbDetailOutcom(reader, 0);
					list.Add(element);
				}
				reader.Close();
			}
			catch(Exception E)
			{
				if(reader != null) reader.Close();
				SetException(E);
			}
			if(reader != null) reader.Close();
			ShowFaults();
		}

		public static void FillList(ArrayList list, DbDetailOutcomDoc doc)
		{
			cmdSelect.Parameters["@codeOutcomDoc"].Value = doc.Code;
			FillList(list, cmdSelect);
		}

		public static bool UpdateList(ListView list, DbDetailOutcomDoc document, SqlTransaction trans)
		{
			try
			{
				DbDetailOutcom.SetTransaction(trans);
				foreach(ListViewItem item in list.Items)
				{
					DbDetailOutcom detailOutcom = (DbDetailOutcom)item.Tag;
					if(detailOutcom != null)
					{
						detailOutcom.SetDocument(document);
						detailOutcom.Write();
					}
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return false;
			}
			return true;
		}

		public void SetPrice(bool guaranty)
		{
			if(guaranty == true)
			{
				if(codeDetailIncom == 0){ price = 0; return;}
				if(tmpDetailIncom == null){ price = 0; return;}
				price = tmpDetailIncom.Price;
				return;
			}
			if(codeDetailStorage == 0){ price = 0; return;}
			if(tmpDetailStorage == null){ price = 0; return;}
			price = tmpDetailStorage.Price;
		}

		private bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@delete"].Value				= (bool)deleted;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@codeDetailStorage"].Value		= (long)codeDetailStorage;
				cmdWrite.Parameters["@codeDetailOutcomDoc"].Value	= (long)codeDetailOutcomDoc;
				cmdWrite.Parameters["@quontity"].Value				= (float)quontity;
				cmdWrite.Parameters["@nds"].Value					= (float)nds;
				cmdWrite.Parameters["@price"].Value					= (float)price;
				cmdWrite.Parameters["@codeDetailIncom"].Value		= (long)codeDetailIncom;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code			= (long)cmdWrite.Parameters["@code"].Value;
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
