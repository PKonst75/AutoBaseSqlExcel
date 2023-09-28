using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrDetailStoragePrice.
	/// </summary>
	public class DrDetailStoragePrice
	{
		static SqlConnection	conn;
		static SqlCommand		cmdReportDetailStoragePrice;

		DbDetailStorage			tmpDetailStorage;
		float					tmpIncomPrice;			// Цена последнего входа
		float					tmpRecomendPrice;		// Цена рекомендованная (пересчет)

		public static void Init(SqlConnection connection)
		{
			conn = connection;

			cmdReportDetailStoragePrice = new SqlCommand("REPORT_СКЛАД_ДЕТАЛЬ_ЦЕНА", conn);
			cmdReportDetailStoragePrice.CommandType = CommandType.StoredProcedure;
			cmdReportDetailStoragePrice.Parameters.Add("@codeDetailStorage", SqlDbType.VarChar);
		}

		public DrDetailStoragePrice(DbDetailIncom source)
		{
			tmpDetailStorage	= source.DetailStorage;
			tmpIncomPrice		= source.Price;
			tmpRecomendPrice	= source.PriceRecomend;
		}
		public DbDetailStorage DetailStorage
		{
			get
			{
				return tmpDetailStorage;
			}
		}

		public string RecomendPriceTxt
		{
			get
			{
				return Db.CachToTxt(tmpRecomendPrice);
			}
		}
		public string IncomPriceTxt
		{
			get
			{
				return Db.CachToTxt(tmpIncomPrice);
			}
		}

		public float StoragePrice
		{
			set
			{
				if(tmpDetailStorage != null)
				{
					tmpDetailStorage.Price = value;
				}
			}
		}
		public float RecomendPrice
		{
			get
			{
				return tmpRecomendPrice;
			}
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
			if (tmpDetailStorage != null)
			{
				item.Text = tmpDetailStorage.DetailName;
				item.SubItems[1].Text = tmpDetailStorage.QuontityTxt;
				item.SubItems[2].Text = tmpDetailStorage.PriceTxt;
			}
			item.SubItems[3].Text	= IncomPriceTxt;
			item.SubItems[4].Text	= RecomendPriceTxt;
			item.Tag				= this;
		}
		#endregion
	}
}
