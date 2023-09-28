using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Работа с внешним справочником кладр.
	/// </summary>
	public class DbDbfKladr
	{
		static string			connection_string = "";
		static string			select_regions;
		static string			select_all;
		static string			select_streets;

		static ArrayList		array_regions		= null;
		static ArrayList		array_level1		= null;
		static ArrayList		array_level2		= null;
		static ArrayList		array_level3		= null;
		static ArrayList		array_departments	= null;
		static ArrayList		array_citys			= null;
		static ArrayList		array_points		= null;

		static ArrayList		array_streets		= null;
		static string			code_street			= "";

		public struct KladrData
		{
			public string		name;
			public string		socr;
			public string		code;
			public string		index;
		};

		public DbDbfKladr()
		{

		}

		public static void Init()
		{
			connection_string	= "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='d:/kladr/';Extended Properties='dBASE IV'";
			select_regions		= "SELECT * FROM kladr.dbf WHERE CODE LIKE('__00000000000') ORDER BY NAME";
			select_all			= "SELECT * FROM kladr.dbf WHERE CODE LIKE(@code_region + '_________00') AND CODE<>(@code_region + '00000000000') ORDER BY NAME";
			select_streets		= "SELECT * FROM street.dbf WHERE CODE LIKE(@code_region + '_____________00') ORDER BY NAME";
		}

		public static ListView FillRegions()
		{
			// Новый формат справочника кладр
			ListView list = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in array_regions)
			{
				data		= (KladrData)o;
				item		= list.Items.Add(data.name + ", " + data.socr);
				item.Tag	= data;
			}
			return list;
		}

		public static ListView FillDepartments(string code_region)
		{
			// Новый формат справочника кладр
			ListView list = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in array_departments)
			{
				// Проверка на принадлежность
				data = (KladrData)o;	
				if(code_region.Substring(0, 2) == data.code.Substring(0, 2))
				{
					item		= list.Items.Add(data.name + ", " + data.socr);
					item.Tag	= data;
				}
			}
			return list;
		}

		public static ListView FillCity(string code)
		{
			// Новый формат справочника кладр
			// Новый формат справочника кладр
			ListView list = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in array_citys)
			{
				// Проверка на принадлежность
				data = (KladrData)o;	
				if(code.Substring(0, 5) == data.code.Substring(0, 5))
				{
					item		= list.Items.Add(data.name + ", " + data.socr);
					item.Tag	= data;
				}
			}
			return list;
		}

		public static ListView FillPoints(string code)
		{
			// Новый формат справочника кладр
			// Новый формат справочника кладр
			ListView list = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in array_points)
			{
				// Проверка на принадлежность
				data = (KladrData)o;	
				if(code.Substring(0, 8) == data.code.Substring(0, 8))
				{
					item		= list.Items.Add(data.name + ", " + data.socr);
					item.Tag	= data;
				}
			}
			return list;
		}
		public static ListView FillStreets(string code)
		{
			// Новый формат справочника кладр
			// Новый формат справочника кладр
			ListView list = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in array_streets)
			{
				// Проверка на принадлежность
				data = (KladrData)o;
				if(data.code.Substring(0, 11) == code.Substring(0, 11))
				{
					data		= (KladrData)o;
					item		= list.Items.Add(data.name + ", " + data.socr);
					item.Tag	= data;
				}
			}
			return list;
		}

		public static string MakeTitle(string name, string socr)
		{
			string txt = "";
			switch(socr)				   
			{
				case "АО":
					txt	= name + " " + socr;
					break;
				case "Аобл":
					txt	= name + " " + socr + ".";
					break;
				case "г":
					txt	= socr + "." + name;
					break;
				case "край":
					txt	= name + " " + socr;
					break;
				case "обл":
					txt	= name + " " + socr + ".";
					break;
				case "Респ":
					txt	= socr + ". " + name;
					break;
				case "р-н":
					txt	= name + " " + socr + ".";
					break;
				case "тер":
					txt	= name + " " + socr + ".";
					break;
				case "у":
					txt	= socr + ". " + name;
					break;
				case "д":
					txt	= socr + ". " + name;
					break;
				case "дп":
					txt	= socr + ". " + name;
					break;
				case "кп":
					txt	= socr + ". " + name;
					break;
				case "м":
					txt	= socr + ". " + name;
					break;
				case "мкр":
					txt	= socr + ". " + name;
					break;
				case "нп":
					txt	= socr + ". " + name;
					break;
				case "п":
					txt	= socr + ". " + name;
					break;
				case "ул":
					txt	= socr + "." + name;
					break;
				default:
					txt = socr + " " + name;
					break;
			}
			return txt;	
		}

		private static void Allocate()
		{
			// Уровни
			if(array_level1 != null)
				array_level1.Clear();
			else
				array_level1	= new ArrayList();
			if(array_level2 != null)
				array_level2.Clear();
			else
				array_level2	= new ArrayList();
			if(array_level3 != null)
				array_level3.Clear();
			else
				array_level3	= new ArrayList();
			// Региональное деление
			if(array_departments != null)
				array_departments.Clear();
			else
				array_departments	= new ArrayList();
			if(array_citys != null)
				array_citys.Clear();
			else
				array_citys	= new ArrayList();
			if(array_points != null)
				array_points.Clear();
			else
				array_points	= new ArrayList();
		}
		public static void DeAllocate()
		{
			array_regions.Clear();
			array_departments.Clear();
			array_citys.Clear();
			array_points.Clear();
			array_level1.Clear();
			array_level2.Clear();
			array_level3.Clear();
		}
		public static bool MakeAll(string code_region)
		{
			// Заполняем данные о структуре региона
			OleDbConnection connection		= null;
			OleDbCommand command			= null;
			OleDbDataReader reader			= null;
			string	code					= "";
			try
			{	
				// Подготовка соединения
				connection = new System.Data.OleDb.OleDbConnection(connection_string);
				connection.Open();
				command = new System.Data.OleDb.OleDbCommand(select_all, connection);
				command.Parameters.Add("@code_region", OleDbType.VarChar);
				command.Parameters["@code_region"].Value = (string)code_region.Substring(0, 2);
				reader = command.ExecuteReader();
				Allocate();			// Готовим масивы для данных
				while(reader.Read())
				{
					KladrData	data = new KladrData();
					data.name	= reader.GetString(reader.GetOrdinal("NAME"));
					data.socr	= reader.GetString(reader.GetOrdinal("SOCR"));
					data.code	= reader.GetString(reader.GetOrdinal("CODE"));
					if(reader.IsDBNull(reader.GetOrdinal("INDEX")) == false)
						data.index	= reader.GetString(reader.GetOrdinal("INDEX"));
					else
						data.index = "";
					// Анализируем данные
					code		= data.code;
					if(code.Substring(2, 3) != "000" && code.Substring(5, 8) == "00000000" )
					{
						// Это район
						array_departments.Add(data);
						array_level1.Add(data); // Первый уровень
					}
					if(code.Substring(5, 3) != "000" && code.Substring(8, 5) == "00000")
					{
						// Это Город
						array_citys.Add(data);
						if(code.Substring(2, 3) != "000")
							array_level2.Add(data); // Второй уровень
						if(code.Substring(2, 3) == "000")
							array_level1.Add(data); // Первый уровень
					}
					if(code.Substring(8, 3) != "000" && code.Substring(11, 2) == "00")
					{
						// Это Населенный Пункт
						array_points.Add(data);
						if(code.Substring(5, 3) == "000" && code.Substring(2, 3) != "000")
							array_level2.Add(data); // Второй уровень
						if(code.Substring(2, 6) == "000000")
							array_level1.Add(data); // Первый уровень
						if(code.Substring(5, 3) != "000" && code.Substring(2, 3) == "000")
							array_level2.Add(data); // Второй уровень
						if(code.Substring(5, 3) != "000" && code.Substring(2, 3) != "000")
							array_level3.Add(data); // Третий уровень
					}
				}
				connection.Close();
			}
			catch(Exception E)
			{
				MessageBox.Show(E.Message);
				return false;
			}
			return true;
		}
		private static void AllocateStreets()
		{
			if(array_streets != null)
				array_streets.Clear();
			else
				array_streets = new ArrayList();
		}
		private static void DeAllocateStreets()
		{
			if(array_streets != null)
				array_streets.Clear();
			array_streets = null;
		}
		public static bool MakeStreets(string code_region)
		{
			// Заполняем данные о структуре региона
			OleDbConnection connection		= null;
			OleDbCommand command			= null;
			OleDbDataReader reader			= null;
			try
			{	
				// Подготовка соединения
				connection = new System.Data.OleDb.OleDbConnection(connection_string);
				connection.Open();
				command = new System.Data.OleDb.OleDbCommand(select_streets, connection);
				command.Parameters.Add("@code_region", OleDbType.VarChar);
				command.Parameters["@code_region"].Value = (string)code_region.Substring(0, 2);
				reader = command.ExecuteReader();
				AllocateStreets();			// Готовим масивы для данных
				while(reader.Read())
				{
					KladrData	data = new KladrData();
					data.name	= reader.GetString(reader.GetOrdinal("NAME"));
					data.socr	= reader.GetString(reader.GetOrdinal("SOCR"));
					data.code	= reader.GetString(reader.GetOrdinal("CODE"));
					if(reader.IsDBNull(reader.GetOrdinal("INDEX")) == false)
						data.index	= reader.GetString(reader.GetOrdinal("INDEX"));
					else
						data.index = "";
					// Заполняем массив
					array_streets.Add(data);
				}
				connection.Close();
			}
			catch(Exception E)
			{
				MessageBox.Show(E.Message);
				return false;
			}
			code_street = code_region;
			return true;
		}
		public static void FillKladr(TreeView tree)
		{
			TreeNode	node;
			KladrData	data;
			// Районы
			foreach(object o in array_level1)
			{
				data		= (KladrData)o;
				node		= tree.Nodes.Add(data.name + ", " + data.socr);
				node.Tag	= data;
				// Заполняем второй уровень
				FillLevel_2(node, data.code);
			}
		}

		public static void FillLevel_2(TreeNode node, string code)
		{
			// Первый уровень - районы
			// Города прямого подчинения региона
			// Населенные пункты прямого подчинения региона
			string code_1		= "";
			bool flag			= false;
			TreeNode node_1		= null;
			KladrData data;
			foreach(object o in array_level2)
			{
				data	= (KladrData)o;
				code_1	= data.code;
				flag	= true;
				if(code_1.Substring(0, 8) != code.Substring(0,8) && code_1.Substring(8, 3) != "000") flag = false;	// Нет принадлежности к Городу
				if(code_1.Substring(0, 5) != code.Substring(0,5) && code_1.Substring(5, 3) != "000") flag = false;	// Нет принадлежности к Району
				
				if(flag == true)
				{
					// Есть подчинение
					node_1		= node.Nodes.Add(data.name + ", " + data.socr);
					node_1.Tag	= data;
					FillLevel_3(node_1, data.code);
				}
			}
		}
		public static void FillLevel_3(TreeNode node, string code)
		{
			// Первый уровень - районы
			// Города прямого подчинения региона
			// Населенные пункты прямого подчинения региона
			string code_1		= "";
			bool flag			= false;
			TreeNode node_1		= null;
			KladrData data;
			foreach(object o in array_level3)
			{
				data	= (KladrData)o;
				code_1	= data.code;
				flag	= true;
				if(code_1.Substring(0, 8) != code.Substring(0,8)) flag = false;	// Нет принадлежности к району
				
				if(flag == true)
				{
					// Есть подчинение
					node_1		= node.Nodes.Add(data.name  + ", " + data.socr);
					node_1.Tag	= data;
				}
			}
		}
		public static bool MakeRegions()
		{
			// Новый формат справочника кладр
			OleDbConnection connection		= null;
			OleDbCommand command			= null;
			OleDbDataReader reader			= null;
			try
			{	
				connection = new System.Data.OleDb.OleDbConnection(connection_string);
				connection.Open();
				command = new System.Data.OleDb.OleDbCommand(select_regions, connection);
				reader = command.ExecuteReader();
				// Место для хранения регионов
				if(array_regions != null)
					array_regions.Clear();
				else
					array_regions = new ArrayList();
				while(reader.Read())
				{
					KladrData	data = new KladrData();
					data.name	= reader.GetString(reader.GetOrdinal("NAME"));
					data.socr	= reader.GetString(reader.GetOrdinal("SOCR"));
					data.code	= reader.GetString(reader.GetOrdinal("CODE"));
					if(reader.IsDBNull(reader.GetOrdinal("INDEX")) == false)
						data.index	= reader.GetString(reader.GetOrdinal("INDEX"));
					else
						data.index = "";
					array_regions.Add(data);
				}
				connection.Close();
			}
			catch(Exception E)
			{
				if(connection != null)
					connection.Close();
				MessageBox.Show(E.Message);
				return false;
			}
			return true;
		}

		public static object FindPoint(string name)
		{
			KladrData data;
			string s;
			foreach(object o in array_points)
			{
				data	= (KladrData)o;
				s		= data.name;
				if(s.ToLower() == name.ToLower()) return data;
			}
			return null;
		}
		public static object FindCity(string name)
		{
			KladrData data;
			string s;
			foreach(object o in array_citys)
			{
				data	= (KladrData)o;
				s		= data.name;
				if(s.ToLower() == name.ToLower()) return data;
			}
			return null;
		}
		public static object FindDepartment(string name)
		{
			KladrData data;
			string s;
			foreach(object o in array_departments)
			{
				data	= (KladrData)o;
				s		= data.name;
				if(s.ToLower() == name.ToLower()) return data;
			}
			return null;
		}
		public static object FindStreet(string name)
		{
			ArrayList list = new ArrayList();
			KladrData data;
			string s;
			foreach(object o in array_streets)
			{
				data	= (KladrData)o;
				s		= data.name;
				if(s.ToLower() == name.ToLower())
				{
					list.Add(data);
				}
			}
			if(list.Count == 0)
				return null;
			else
				return list;
		}
		public static object SelectPoint(string code)
		{
			KladrData data;
			string s;
			foreach(object o in array_points)
			{
				data	= (KladrData)o;
				s		= data.code;
				if(s.Substring(0, 11) == code.Substring(0, 11)) return data;
			}
			return null;
		}
		public static object SelectCity(string code)
		{
			KladrData data;
			string s;
			foreach(object o in array_citys)
			{
				data	= (KladrData)o;
				s		= data.code;
				if(s.Substring(0, 8) == code.Substring(0, 8)) return data;
			}
			return null;
		}
		public static object SelectDepartment(string code)
		{
			KladrData data;
			string s;
			foreach(object o in array_departments)
			{
				data	= (KladrData)o;
				s		= data.code;
				if(s.Substring(0, 5) == code.Substring(0, 5)) return data;
			}
			return null;
		}
		public static object SelectStreet(string code)
		{
			KladrData data;
			string s;
			foreach(object o in array_streets)
			{
				data	= (KladrData)o;
				s		= data.code;
				if(s == code) return data;
			}
			return null;
		}

		public static string MakeAddrShort(KladrData street)
		{
			string title = "";
			KladrData data;
			object o;
			string code = street.code.Substring(0, 11) + "00";
			o = SelectDepartment(code);
			if (o != null)
			{
				data = (KladrData)o;
				title = MakeTitle(data.name, data.socr);
			}
			o = SelectCity(code);
			if (o != null)
			{
				data = (KladrData)o;
				if(title == "")
					title = MakeTitle(data.name, data.socr);
				else
					title = title + ", " + MakeTitle(data.name, data.socr);
			}
			o = SelectPoint(code);
			if (o != null)
			{
				data = (KladrData)o;
				if(title == "")
					title = MakeTitle(data.name, data.socr);
				else
					title = title + ", " + MakeTitle(data.name, data.socr);
			}
			data = street;
			title = title + ", " + MakeTitle(data.name, data.socr);
			return title;
		}
		public static ListView FillAdresses(ArrayList list)
		{
			// Новый формат справочника кладр
			// Новый формат справочника кладр
			ListView list_view = new ListView();
			ListViewItem item;
			KladrData data;
			foreach(object o in list)
			{
				// Проверка на принадлежность
				data = (KladrData)o;
				item		= list_view.Items.Add(MakeAddrShort(data));
				item.Tag	= data;
			}
			return list_view;
		}
	}
}
