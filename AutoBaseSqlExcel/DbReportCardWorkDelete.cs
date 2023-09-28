using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbReportCardWorkDelete.
	/// </summary>
	public class DbReportCardWorkDelete:Db
	{
		long		cardNumber;
		int			cardYear;
		string		nameWork;
		string		user;

		private static SqlConnection conn;
		private static SqlCommand cmdSelect;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}


		public DbReportCardWorkDelete(SqlDataReader reader, int offset)
		{
			cardNumber	= (long)GetValueLong(reader, offset);		offset++;
			cardYear	= (int)GetValueInt(reader, offset);			offset++;
			nameWork	= (string)GetValueString(reader, offset);	offset++;
			user		= (string)GetValueString(reader, offset);	offset++;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 4 собственных полей и длина ридера класса DbAutoModel
			readerLength = 4;

			conn = connection;

			cmdSelect = new SqlCommand("REPORT_КАРТОЧКА_РАБОТА_УДАЛЕННЫЕ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@date", SqlDbType.DateTime);
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
			item.Text = cardNumber.ToString();
			item.SubItems[1].Text = cardYear.ToString();
			item.SubItems[2].Text = nameWork;
			item.SubItems[3].Text = user;
			item.Tag = this;
		}

		public static void FillList(ListView list, DateTime date)
		{
			cmdSelect.Parameters["@date"].Value = date;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbReportCardWorkDelete element = new DbReportCardWorkDelete(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array, DateTime date)
		{
			cmdSelect.Parameters["@date"].Value = date;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbReportCardWorkDelete element = new DbReportCardWorkDelete(reader, 0);
			array.Add(element);
		}
		#endregion


		public string CardNumberTxt
		{
			get
			{
				return cardNumber.ToString();
			}
		}
		public string CardYearTxt
		{
			get
			{
				return cardYear.ToString();
			}
		}
		public string WorkName
		{
			get
			{
				return nameWork;
			}
		}
		public string User
		{
			get
			{
				return user;
			}
		}
		
	}
}
