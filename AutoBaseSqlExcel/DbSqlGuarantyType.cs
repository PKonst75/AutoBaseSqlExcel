using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlGuarantyType.
	/// </summary>
	public class DbSqlGuarantyType
	{

		public static SqlCommand		select;
		public static SqlCommand		find;
		public static SqlCommand		find_default;
		public static SqlCommand		select_child;
		public static SqlCommand		select_root;
		public DbSqlGuarantyType()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ГАРАНТИЯ_ВИД_ВЫБОРКА", connection);
			select.CommandType = CommandType.StoredProcedure;

			select_root = new SqlCommand("ГАРАНТИЯ_ВИД_ВЫБОРКА_ВЕРХНИЕ", connection);
			select_root.CommandType = CommandType.StoredProcedure;

			select_child = new SqlCommand("ГАРАНТИЯ_ВИД_ВЫБОРКА_ДОЧЕРНИЕ", connection);
			select_child.Parameters.Add("@code_parent", SqlDbType.BigInt);
			select_child.CommandType = CommandType.StoredProcedure;

			find = new SqlCommand("ГАРАНТИЯ_ВИД_ПОИСК", connection);
			find.Parameters.Add("@code", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;

			find_default = new SqlCommand("ГАРАНТИЯ_ВИД_ПОИСК_УМОЛЧАНИЕ", connection);
			find_default.CommandType = CommandType.StoredProcedure;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtGuarantyType element			= new DtGuarantyType();
			element.SetData("КОД_ГАРАНТИЯ", DbSql.GetValueLong(reader, "КОД_ГАРАНТИЯ"));
			element.SetData("ОПИСАНИЕ_ГАРАНТИЯ", DbSql.GetValueString(reader, "ОПИСАНИЕ_ГАРАНТИЯ"));
			element.SetData("ОТВЕТСТВЕННЫЙ", DbSql.GetValueBool(reader, "ОТВЕТСТВЕННЫЙ"));

			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtGuarantyType element = (DtGuarantyType)MakeElement(reader);
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

		public static DtGuarantyType Find(long code)
		{
			// Подготовка команды поиска по маске
			find.Parameters["@code"].Value	= (long)code;
			return (DtGuarantyType)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static DtGuarantyType FindDefault()
		{
			// Подготовка команды поиска по умолчанию
			return (DtGuarantyType)DbSql.Find(find_default, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayRoot(ArrayList array)
		{
			// Подготовка команды поиска по маске
			DbSql.FillArray(array, select_root, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static void SelectInArrayChild(ArrayList array, long code_parent)
		{
			// Подготовка команды поиска по маске
			select_child.Parameters["@code_parent"].Value	= (long)code_parent;
			DbSql.FillArray(array, select_child, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
