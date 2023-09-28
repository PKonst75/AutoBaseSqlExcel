using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetailIncomDetailed.
	/// </summary>
	public class DbDetailIncomDetailed:Db
	{
		private DbDetailIncom		tmpDetailIncom;
		private DbDetailIncomDoc	tmpDetailIncomDoc;
		private float				tmpQuontity;		// Запрашиваемое количество

		private static SqlConnection conn;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public DbDetailIncomDetailed(SqlDataReader reader, int offset)
		{
			tmpDetailIncom		= new DbDetailIncom(reader, offset);	offset = offset + DbDetailIncom.ReaderLength;
			tmpDetailIncomDoc	= new DbDetailIncomDoc(reader, offset);	offset = offset + DbDetailIncomDoc.ReaderLength;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// Только чужие ридеры
			readerLength = DbDetailIncom.ReaderLength + DbDetailIncomDoc.ReaderLength;

			conn = connection;

			cmdSelect = new SqlCommand("REPORT_СКЛАД_ДЕТАЛЬ_ПРИХОД_ПОЗИЦИЯ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@codeDetailStorage", SqlDbType.VarChar);
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
					DbDetailIncomDetailed element = new DbDetailIncomDetailed(reader, 0);
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

		public static void FillListIncom(ListView list, DbDetailStorage detailStorage)
		{
			cmdSelect.Parameters["@codeDetailStorage"].Value = detailStorage.Code;
			DbDetailIncomDetailed.FillList(list, cmdSelect);
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = DateTxt;
			item.SubItems[1].Text = PriceNoNdsTxt;
			item.SubItems[2].Text = NdsTxt;
			item.SubItems[3].Text = PriceTxt;
			item.SubItems[4].Text = QuontityTxt;
			item.SubItems[5].Text = tmpDetailIncomDoc.Document;
			item.Tag = this;
		}

		public string DateTxt
		{
			get
			{
				return Db.DateToTxt(tmpDetailIncomDoc.Date);
			}
		}
		public float Quontity
		{
			get
			{
				return tmpQuontity;
			}
			set
			{
				tmpQuontity = value;
			}
		}
		public string PriceTxt
		{
			get
			{
				if(tmpDetailIncom == null) return "--";
				return tmpDetailIncom.PriceTxt;
			}
		}

		public string NdsTxt
		{
			get
			{
				if(tmpDetailIncom == null) return "--";
				return tmpDetailIncom.NdsTxt;
			}
		}

		public string QuontityTxt
		{
			get
			{
				if(tmpDetailIncom == null) return "--";
				return (tmpDetailIncom.Quontity - tmpDetailIncom.Expens).ToString() + " (" + tmpDetailIncom.QuontityTxt + ")";
			}
		}

		public string PriceNoNdsTxt
		{
			get
			{
				if(tmpDetailIncom == null) return "--";
				return  tmpDetailIncom.PriceNoNdsTxt;
			}
		}

		public long CodeStorageDetail
		{
			get
			{
				if(tmpDetailIncom == null) return 0;
				return tmpDetailIncom.CodeDetailStorage;
			}
		}

		public float Price
		{
			get
			{
				if(tmpDetailIncom == null) return 0.0F;
				return tmpDetailIncom.Price;
			}
		}
		public float Nds
		{
			get
			{
				if(tmpDetailIncom == null) return 0.0F;
				return tmpDetailIncom.Nds;
			}
		}
		public long Code
		{
			get
			{
				if(tmpDetailIncom == null) return 0;
				return tmpDetailIncom.Code;
			}
		}

		public DbDetailIncom DetailIncom
		{
			get
			{
				if(tmpDetailIncom == null) return null;
				return tmpDetailIncom;
			}
		}

		public DbDetailStorage DetailStorage
		{
			get
			{
				if(tmpDetailIncom == null) return null;
				return tmpDetailIncom.DetailStorage;
			}
		}
	}
}
