using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Элементы счетов
	/// </summary>
	public class DbAccountDetailItem:Db
	{
		private long number;
		private string codeDetail;
		private string codeFirm;
		private long accountNumber;
		private int accountYear;
		private float priceNds;
		private float nds;
		private float quontity;

		private DbDetail tmpDetail;
		private DbFirm tmpFirm;
		private bool tmpExists;
		private bool tmpDeleted;

		private static SqlConnection conn;
		private static SqlCommand cmdExecUpdate;
		private static SqlCommand cmdExecSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 8 собственных полей и остальное
			readerLength = 8 + DbDetail.ReaderLength + DbFirm.ReaderLength;

			conn = connection;

			cmdExecUpdate = new SqlCommand("ADD_ACCOUNT_DETAIL_ITEM");
			cmdExecUpdate.Connection = conn;
			cmdExecUpdate.CommandType = CommandType.StoredProcedure;
			cmdExecUpdate.Parameters.Add("@delete", SqlDbType.Bit);
			cmdExecUpdate.Parameters.Add("@number", SqlDbType.BigInt);
			cmdExecUpdate.Parameters.Add("@accountNumber", SqlDbType.BigInt);
			cmdExecUpdate.Parameters.Add("@accountYear", SqlDbType.Int);
			cmdExecUpdate.Parameters.Add("@codeDetail", SqlDbType.VarChar);
			cmdExecUpdate.Parameters.Add("@codeFirm", SqlDbType.VarChar);
			cmdExecUpdate.Parameters.Add("@quontity", SqlDbType.Real);
			cmdExecUpdate.Parameters.Add("@price", SqlDbType.Real);
			cmdExecUpdate.Parameters.Add("@nds", SqlDbType.Real);
			cmdExecUpdate.Parameters.Add("@ERROR", SqlDbType.VarChar, 60);
			cmdExecUpdate.Parameters["@number"].Direction = ParameterDirection.InputOutput;
			cmdExecUpdate.Parameters["@ERROR"].Direction = ParameterDirection.Output;
			cmdExecUpdate.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
			cmdExecUpdate.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;

			cmdExecSelect = new SqlCommand("ACCOUNT_DETAIL_ITEM_VIEW");
			cmdExecSelect.Connection = conn;
			cmdExecSelect.CommandType = CommandType.StoredProcedure;
			cmdExecSelect.Parameters.Add("@accountNumber", SqlDbType.BigInt);
			cmdExecSelect.Parameters.Add("@accountYear", SqlDbType.Int);
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdExecUpdate.Transaction = trans;
		}

		public DbAccountDetailItem(DbDetailStorage element)
		{
			number = 0;
	//		codeDetail = element.CodeDetail;
	//		codeFirm = element.CodeFirm;
			accountNumber = 0;
			accountYear = 0;
			priceNds = element.Price;
			nds = 18.0F;
			quontity = 1.0F;

			tmpDetail = element.Detail;
	//		tmpFirm = element.Firm;
			adding = true;
			tmpExists = false;
			tmpDeleted = false;
		}

		public DbAccountDetailItem(SqlDataReader reader, int offset)
		{
			number				= (long)GetValueLong(reader, offset);		offset++;
			codeDetail			= (string)GetValueString(reader, offset);	offset++;
			codeFirm			= (string)GetValueString(reader, offset);	offset++;
			accountNumber		= (long)GetValueLong(reader, offset);		offset++;
			accountYear			= (int)GetValueInt(reader, offset);			offset++;
			quontity			= (float)GetValueFloat(reader, offset);		offset++;
			priceNds			= (float)GetValueFloat(reader, offset);		offset++;
			nds					= (float)GetValueFloat(reader, offset);		offset++;
			
			tmpDetail			= new DbDetail(reader, offset);				offset = offset + DbDetail.ReaderLength;
			tmpFirm				= new DbFirm(reader, offset);				offset = offset + DbFirm.ReaderLength;

			tmpExists = true;
			tmpDeleted = false;
		}

		private void SetDocument(DbAccountDetail account)
		{
			accountNumber = account.Number;
			accountYear = account.Year;
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
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems[1].Text = Name;
			item.SubItems[2].Text = QuontityTxt;
			item.SubItems[3].Text = PriceTxt;
			item.SubItems[4].Text = NdsTxt;
			item.SubItems[5].Text = SummTxt;
			item.Tag = this;
		}

		public static void FillList(ListView list, DbAccountDetail account)
		{
			cmdExecSelect.Parameters["@accountNumber"].Value = account.Number;
			cmdExecSelect.Parameters["@accountYear"].Value = account.Year;
			Db.DbFillList(list, cmdExecSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbAccountDetailItem element = new DbAccountDetailItem(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		public string Name
		{
			get
			{
				if(tmpDetail == null) return "";
				return tmpDetail.Name;
			}
		}

		public string QuontityTxt
		{
			get
			{
				return FloatToTxt(quontity);
			}
			set
			{
				quontity = this.SetFloatNotMinus(quontity, value, "Количество должно быть положительным");
			}
		}

		public string PriceTxt
		{
			get
			{
				return CachToTxt(priceNds);
			}
			set
			{
				priceNds = this.SetFloatNotMinus(priceNds, value, "Цена должна быть неотрицательной");
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
				nds = this.SetFloatNotMinus(nds, value, "НДС должен быть неотрицательным");
			}
			
		}

		public string SummTxt
		{
			get
			{
				return CachToTxt((double)priceNds * (double)quontity);
			}
		}

		public double Summ
		{
			get
			{
				return (double)priceNds * (double)quontity;
			}
		}

		public static bool UpdateList(ListView list, DbAccountDetail account, SqlTransaction trans)
		{
			try
			{
				DbAccountDetailItem element;
				DbAccountDetailItem.SetTransaction(trans);
				foreach(ListViewItem item in list.Items)
				{
					element = (DbAccountDetailItem)item.Tag;
					if(element != null)
					{
						if(element.Changed)
						{
							element.SetDocument(account);
							element.Update();
						}
					}
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return true;
		}

		private bool Update()
		{
			try
			{
				cmdExecUpdate.Parameters["@delete"].Value = tmpDeleted;
				cmdExecUpdate.Parameters["@accountNumber"].Value = accountNumber;
				cmdExecUpdate.Parameters["@number"].Value = number;
				cmdExecUpdate.Parameters["@codeDetail"].Value = codeDetail;
				cmdExecUpdate.Parameters["@codeFirm"].Value = codeFirm;
				cmdExecUpdate.Parameters["@price"].Value = priceNds;
				cmdExecUpdate.Parameters["@nds"].Value = nds;
				cmdExecUpdate.Parameters["@quontity"].Value = quontity;
				cmdExecUpdate.Parameters["@accountYear"].Value = accountYear;
				cmdExecUpdate.ExecuteNonQuery();
				if((int)cmdExecUpdate.Parameters["RETURN_VALUE"].Value != 0)
				{
					if(cmdExecUpdate.Parameters["@ERROR"].Value != null && cmdExecUpdate.Parameters["@ERROR"].Value != System.DBNull.Value)
						throw new ApplicationException((string)cmdExecUpdate.Parameters["@ERROR"].Value);
				}
				number = (long)cmdExecUpdate.Parameters["@number"].Value;
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
