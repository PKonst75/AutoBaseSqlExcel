using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbKladr.
	/// </summary>
	public class DbKladr:Db
	{
		private long		code;
		private string		name;
		private string		mark;

		private static SqlConnection	conn;
		private static SqlCommand		cmdSelectRegion;
		private static SqlCommand		cmdSelectArea;
		private static SqlCommand		cmdSelectCity;
		private static SqlCommand		cmdSelectSettlement;
		private static SqlCommand		cmdSelectStreet;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbKladr(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			name		= (string)GetValueString(reader, offset);	offset++;
			mark		= (string)GetValueString(reader, offset);	offset++;

			adding		= false;
		}
		#endregion

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 3 собственных поля и остальное
			readerLength = 3;

			conn = connection;

			cmdSelectRegion = new SqlCommand("SELECT_РЕГИОН", conn);
			cmdSelectRegion.CommandType = CommandType.StoredProcedure;

			cmdSelectArea = new SqlCommand("SELECT_РАЙОН", conn);
			cmdSelectArea.Parameters.Add("@region", SqlDbType.BigInt);
			cmdSelectArea.CommandType = CommandType.StoredProcedure;

			cmdSelectCity = new SqlCommand("SELECT_ГОРОД", conn);
			cmdSelectCity.Parameters.Add("@region", SqlDbType.BigInt);
			cmdSelectCity.Parameters.Add("@area", SqlDbType.BigInt);
			cmdSelectCity.CommandType = CommandType.StoredProcedure;

			cmdSelectSettlement= new SqlCommand("SELECT_ПУНКТ", conn);
			cmdSelectSettlement.Parameters.Add("@region", SqlDbType.BigInt);
			cmdSelectSettlement.Parameters.Add("@area", SqlDbType.BigInt);
			cmdSelectSettlement.Parameters.Add("@city", SqlDbType.BigInt);
			cmdSelectSettlement.CommandType = CommandType.StoredProcedure;

			cmdSelectStreet= new SqlCommand("SELECT_УЛИЦА", conn);
			cmdSelectStreet.Parameters.Add("@region", SqlDbType.BigInt);
			cmdSelectStreet.Parameters.Add("@area", SqlDbType.BigInt);
			cmdSelectStreet.Parameters.Add("@city", SqlDbType.BigInt);
			cmdSelectStreet.Parameters.Add("@settlement", SqlDbType.BigInt);
			cmdSelectStreet.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region Отображение
		public ListViewItem LVItem
		{
			get
			{
				ListViewItem item = new ListViewItem();
				item.Text = "";
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text				= name;
			item.SubItems[1].Text	= mark;
			item.Tag				= this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelectRegion, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbKladr element = new DbKladr(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArrayRegion(ArrayList array)
		{
			Db.FillArray(array, cmdSelectRegion, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayArea(ArrayList array, DbKladr region)
		{
			cmdSelectArea.Parameters["@region"].Value = (long)region.Code;
			Db.FillArray(array, cmdSelectArea, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayCity(ArrayList array, DbKladr region, DbKladr area)
		{
			cmdSelectCity.Parameters["@region"].Value = (long)region.Code;
			if(area != null)
				cmdSelectCity.Parameters["@area"].Value = (long)area.Code;
			else 
				cmdSelectCity.Parameters["@area"].Value = (long)0;
			Db.FillArray(array, cmdSelectCity, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArraySettlement(ArrayList array, DbKladr region, DbKladr area, DbKladr city)
		{
			cmdSelectSettlement.Parameters["@region"].Value = (long)region.Code;
			if(area != null)
				cmdSelectSettlement.Parameters["@area"].Value = (long)area.Code;
			else 
				cmdSelectSettlement.Parameters["@area"].Value = (long)0;
			if(city != null)
				cmdSelectSettlement.Parameters["@city"].Value = (long)city.Code;
			else 
				cmdSelectSettlement.Parameters["@city"].Value = (long)0;
			Db.FillArray(array, cmdSelectSettlement, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArrayStreet(ArrayList array, DbKladr region, DbKladr area, DbKladr city, DbKladr settlement)
		{
			cmdSelectStreet.Parameters["@region"].Value = (long)region.Code;
			if(area != null)
				cmdSelectStreet.Parameters["@area"].Value = (long)area.Code;
			else 
				cmdSelectStreet.Parameters["@area"].Value = (long)0;
			if(city != null)
				cmdSelectStreet.Parameters["@city"].Value = (long)city.Code;
			else 
				cmdSelectStreet.Parameters["@city"].Value = (long)0;
			if(settlement != null)
				cmdSelectStreet.Parameters["@settlement"].Value = (long)settlement.Code;
			else 
				cmdSelectStreet.Parameters["@settlement"].Value = (long)0;
			Db.FillArray(array, cmdSelectStreet, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbKladr element = new DbKladr(reader, 0);
			array.Add(element);
		}
		#endregion

		#region Доступ к основным параметрам - Только чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region Доступ к основным параметрам
		public string Name
		{
			get
			{
				return name;
			}
		}
		public string Mark
		{
			get
			{
				return mark;
			}
		}
		public string Title
		{
			get
			{
				if(mark.Length != 0)
					return this.name + ", " + this.mark;
				else
					return this.name;
			}
		}
		#endregion

		#region Переопределенные виртуальные методы
		override public string DbTitle()
		{
			return this.Name + "," + this.Mark;
		}
		#endregion

		#region Основные методы
		#endregion

		#region Отображение  в текст
		public string NameTxt
		{
			get
			{
				string txt = "";
				switch(this.mark)				   
				{
					case "АО":
						txt	= this.Name + " " + this.mark;
						break;
					case "Аобл":
						txt	= this.Name + " " + this.mark + ".";
						break;
					case "г":
						txt	= this.mark + "." + this.Name;
						break;
					case "край":
						txt	= this.Name + " " + this.mark;
						break;
					case "обл":
						txt	= this.Name + " " + this.mark + ".";
						break;
					case "Респ":
						txt	= this.mark + ". " + this.Name;
						break;
					case "р-н":
						txt	= this.Name + " " + this.mark + ".";
						break;
					case "тер":
						txt	= this.Name + " " + this.mark + ".";
						break;
					case "у":
						txt	= this.mark + ". " + this.Name;
						break;
					case "д":
						txt	= this.mark + ". " + this.Name;
						break;
					case "дп":
						txt	= this.mark + ". " + this.Name;
						break;
					case "кп":
						txt	= this.mark + ". " + this.Name;
						break;
					case "м":
						txt	= this.mark + ". " + this.Name;
						break;
					case "мкр":
						txt	= this.mark + ". " + this.Name;
						break;
					case "нп":
						txt	= this.mark + ". " + this.Name;
						break;
					case "п":
						txt	= this.mark + ". " + this.Name;
						break;
					case "ул":
						txt	= this.mark + "." + this.Name;
						break;
					default:
						txt = this.mark + " " + this.Name;
						break;
				}
				return txt;
			}
		}
		#endregion
	}
}
