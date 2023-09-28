using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlWorkshop.
	/// </summary>
	public class DbSqlWorkshop
	{
		public static SqlCommand		select;
		public static SqlCommand		find;

		public DbSqlWorkshop()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ЦЕХ_ВЫБОРКА", connection);
			select.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ЦЕХ_ПОИСК", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtWorkshop element			= new DtWorkshop();
			element.SetData("КОД_ЦЕХ", DbSql.GetValueLong(reader, "КОД_ЦЕХ"));
			element.SetData("НАИМЕНОВАНИЕ_ЦЕХ", DbSql.GetValueString(reader, "НАИМЕНОВАНИЕ_ЦЕХ"));
			element.SetData("ПРИМЕНЕНИЕ_ЦЕХ", DbSql.GetValueString(reader, "ПРИМЕНЕНИЕ_ЦЕХ"));
			element.SetData("ПРОПУСК_НАЗНАЧЕНИЕ", DbSql.GetValueString(reader, "ПРОПУСК_НАЗНАЧЕНИЕ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtWorkshop element = (DtWorkshop)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Ошибка";
			}
			return item;
		}
		public static void SelectInList(ListView list)
		{
			// Подготовка команды поиска по маске
			DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
		}

		public static void SelectInArray(ArrayList array)
		{
			// Подготовка команды поиска по маске
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtWorkshop Find(long code)
		{
			// Подготовка команды поиска по маске
			find.Parameters["@code"].Value	= (long)code;
			return (DtWorkshop)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

	}
}
