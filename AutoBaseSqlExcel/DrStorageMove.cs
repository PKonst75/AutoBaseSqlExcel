using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Отчет о движениях по складу
	/// </summary>
	public class DrStorageMove:Db
	{
		private long		codeDetailStorage;	// Код складской позиции
		private float		quontity;			// Количество
		private float		nds;				// Ндс
		private float		price;				// Цена (с НДС)
		private long		number;				// Номер документа движения
		private DateTime	date;				// Дата документа движения
		private string		based;				// Основание документа движения

		private static SqlConnection conn;
		private static SqlCommand cmdReportMove;

		public static void Init(SqlConnection connection)
		{
			conn = connection;

			cmdReportMove = new SqlCommand("REPORT_СКЛАД_ДЕТАЛЬ_ДВИЖЕНИЯ", conn);
			cmdReportMove.CommandType = CommandType.StoredProcedure;
			cmdReportMove.Parameters.Add("@codeDetailStorage", SqlDbType.VarChar);
			cmdReportMove.Parameters.Add("@startDate", SqlDbType.DateTime);
			cmdReportMove.Parameters.Add("@endDate", SqlDbType.DateTime);
		}

		public DrStorageMove(SqlDataReader reader)
		{
			quontity		= (float)GetValueFloat(reader, 0);
			nds				= (float)GetValueFloat(reader, 1);
            price			= (float)GetValueFloat(reader, 2);
			number			= (long)GetValueLong(reader, 3);
			date			= (DateTime)GetValueDate(reader, 4);
			based			= (string)GetValueString(reader, 5);
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
				SetLVitem(item);
				return item;
			}
		}

		public void SetLVitem(ListViewItem item)
		{
			item.Text = Db.DateToTxt(date);
			if(quontity > 0)
			{
				item.SubItems[1].Text = Db.FloatToTxt(quontity);
				item.SubItems[2].Text = "";
			}
			else
			{
				item.SubItems[1].Text = "";
				item.SubItems[2].Text = Db.FloatToTxt(quontity);
			}
			item.SubItems[3].Text = Db.CachToTxt(price);
			item.SubItems[4].Text = number.ToString();
			item.SubItems[5].Text = based;
		}

		public static void FillList_ReportMove(ListView list, DbDetailStorage detailStorage, DateTime startDate, DateTime endDate)
		{
			cmdReportMove.Parameters["@codeDetailStorage"].Value = (long)detailStorage.Code;
			cmdReportMove.Parameters["@startDate"].Value = (DateTime)startDate;
			cmdReportMove.Parameters["@endDate"].Value = (DateTime)endDate;
			Db.DbFillList(list, cmdReportMove, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DrStorageMove element = new DrStorageMove(reader);
			list.Items.Add(element.LVItem);
		}
	}
}
