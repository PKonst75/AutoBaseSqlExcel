using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSql.
	/// </summary>
	public class DbSql
	{
		public DbSql()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public delegate object DelegateMakeElement(SqlDataReader reader);
		public delegate object DelegateMakeElement2(SqlDataReader reader, Dt srcElement);

		#region Чтение разных типов данных из SqlDataReader
		// Проверка на пустые данные
		public static bool IsValueNULL(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return true;
				return false;
			}
			catch(Exception E)
			{
				return true;
			}
		}
		public static object GetValue(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if (reader.IsDBNull(index) == true) return null;
				return (object)reader.GetValue(index);
			}
			catch (Exception E)
			{
				return null;
			}
		}
		// Строка
		public static string GetValueString(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return "";
				return (string)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return "";
			}
		}
		// Длинное целое (64 bit)
		public static long GetValueLong(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return 0;
				return (long)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return 0;
			}
		}
		public static short GetValueShort(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return 0;
				return (short)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return 0;
			}
		}
		// Целое (32 bit)
		public static int GetValueInt(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return 0;
				return (int)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return 0;
			}
		}
		// Small Int (Short)
		public static int GetValueSmallInt(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return 0;
				return (short)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return 0;
			}
		}
		// Дата
		public static DateTime GetValueDate(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return DateTime.MinValue;
				return (DateTime)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return DateTime.MinValue;
			}
		}
		public static bool GetValueBool(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return false;
				return (bool)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return false;
			}
		}
		public static float GetValueFloat(SqlDataReader reader, string name)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(name);
				if(reader.IsDBNull(index) == true) return 0.0F;
				return (float)reader.GetValue(index);
			}
			catch(Exception E)
			{
				//Db.SetException(E);
				return 0.0F;
			}
		}
		#endregion

		#region Чтение разных типов данных из SqlDataReader с превращением в текст
		public static string GetValueLongTxt(SqlDataReader reader, string name)
		{
			long data = GetValueLong(reader, name);
			return data.ToString();
		}
		public static string GetValueDateTxt(SqlDataReader reader, string name)
		{
			DateTime data = GetValueDate(reader, name);
			return data.ToShortDateString();
		}
		public static string GetValueBoolTxt(SqlDataReader reader, string name)
		{
			bool data = GetValueBool(reader, name);
			if(data == true)
				return "ДА";
			else
				return "НЕТ";
		}
		public static string GetValueFloatTxt(SqlDataReader reader, string name)
		{
			float data = GetValueFloat(reader, name);
			return data.ToString();
		}
		#endregion

		#region Превращение разных типов даных в текст
		public static string BoolTxt(bool data)
		{
			if(data == true)
				return "ДА";
			else
				return "НЕТ";
		}
		public static string FloatTxt(float data)
		{
			return data.ToString();
		}
		public static string LongTxt(long data)
		{
			return data.ToString();
		}
		#endregion

		#region Заполнение листа
		public delegate ListViewItem DelegateMakeLVItem(SqlDataReader reader);
		public delegate ListViewItem DelegateMakeLVItemFromElement(object o);

		public static void FillList(ListView list, SqlCommand command, DelegateMakeLVItem func)
		{
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						ListViewItem item = func(reader);
						list.Items.Add(item);
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			Db.ShowFaults();
		}
		public static void FillListNumerator(ListView list, SqlCommand command, DelegateMakeLVItem func)
		{
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						ListViewItem item = func(reader);
						list.Items.Add(item);
						item.Text	= (item.Index + 1).ToString();
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			Db.ShowFaults();
		}

		public static void FillListFromArray(ListView list, ArrayList array, DelegateMakeLVItemFromElement func)
		{
			foreach (object o in array)
			{
				ListViewItem item = func(o);
				list.Items.Add(item);
			}
		}
		#endregion

		#region Заполнение дерева
		public delegate TreeNode DelegateMakeTNode(SqlDataReader reader);

		public static void FillTree(TreeView tree, SqlCommand command, DelegateMakeTNode func)
		{
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						TreeNode node = func(reader);
						tree.Nodes.Add(node);
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			Db.ShowFaults();
		}
		public static void FillTreeNode(TreeNode node, SqlCommand command, DelegateMakeTNode func)
		{
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						TreeNode new_node = func(reader);
						node.Nodes.Add(new_node);
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			Db.ShowFaults();
		}
		#endregion

		#region Заполнение массива
		public delegate object DelegateReadDt(SqlDataReader srcReader);
		

		public static void FillArray(ArrayList array, SqlCommand command, DelegateMakeElement func)
		{
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillArray");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						object element = func(reader);
						array.Add(element);
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			Db.ShowFaults();
		}
		#endregion

		#region Поиск элемента

		public static object Find(SqlCommand command, DelegateMakeElement func)
		{
			object		element = null;
			int			count	= 0;
			if(command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while(reader.Read())
					{
						element = func(reader);
						count++;
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return null;
				}
				if(reader != null) reader.Close();
			}
			if(count > 1) Db.SetErrorMessage("Найдено больше одного элемента");
			Db.ShowFaults();
			return element;
		}
		public static void LoadFromDatabase(SqlCommand command, DelegateMakeElement2 func, Dt srcElement)
		{
			int count = 0;
			if (command == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = command.ExecuteReader();
					while (reader.Read())
					{
						func(reader, srcElement);
						count++;
					}
					if (reader != null) reader.Close();
				}
				catch (Exception E)
				{
					if (reader != null) reader.Close();
					Db.SetException(E);
					Db.ShowFaults();
					return;
				}
				if (reader != null) reader.Close();
			}
			if (count > 1) Db.SetErrorMessage("Найдено больше одного элемента");
			Db.ShowFaults();
		}
		#endregion

		#region Исполнение комманды
		public static bool ExecuteCommandError(SqlCommand cmd)
		{
			int count = 0;
			if(cmd == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда");
			}
			else
			{
				try
				{
					count = cmd.ExecuteNonQuery();
					ThrowReturnError(cmd);
				}
				catch(Exception E)
				{
					Db.SetException(E);
				}
			}
			if(Db.ShowFaults()) return false;
			return true;
		}
		public static void SetReturnError(SqlCommand cmd)
		{
			cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 1024);
			cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
			cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
			cmd.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
		}

		protected static void ThrowReturnError(SqlCommand cmd)
		{
			if(cmd.Parameters["RETURN_VALUE"] == null || cmd.Parameters["RETURN_VALUE"].Value == null)
				throw new ApplicationException("Нулевой возврат команды SQL");
			if((int)cmd.Parameters["RETURN_VALUE"].Value != 0)
			{
				if(cmd.Parameters["@ERROR"].Value != null && cmd.Parameters["@ERROR"].Value != System.DBNull.Value)
					throw new ApplicationException((string)cmd.Parameters["@ERROR"].Value);
				else
					throw new ApplicationException("Неизвестная ошибка исполнения");
			}
		}
		#endregion

		#region НОВЫЙ ВАРИАНТ
		public enum SQL_PARAMETER_TYPE : short { LONG = 1, INT = 2, DATETIME = 3, FLOAT = 4, BOOL = 5, STRING = 6, SHORT = 7 };
		public static SqlDbType ConvertType(SQL_PARAMETER_TYPE srcParameterType)
		{
			switch (srcParameterType)
			{
				case SQL_PARAMETER_TYPE.LONG:
					return SqlDbType.BigInt;
				case SQL_PARAMETER_TYPE.INT:
					return SqlDbType.Int;
				case SQL_PARAMETER_TYPE.DATETIME:
					return SqlDbType.DateTime;
				case SQL_PARAMETER_TYPE.FLOAT:
					return SqlDbType.Real;
				case SQL_PARAMETER_TYPE.BOOL:
					return SqlDbType.Bit;
				case SQL_PARAMETER_TYPE.STRING:
					return SqlDbType.VarChar;
				case SQL_PARAMETER_TYPE.SHORT:
					return SqlDbType.SmallInt;
				default:
					return SqlDbType.Text;
			}
		}
		public static bool IsParameter(string srcParameterName, SqlCommand srcSqlCommand)
		{
			return srcSqlCommand.Parameters.Contains(srcParameterName);
		}
		public static void SetParameterValue(string srcParameterName, object srcParameterValue, SqlCommand srcSqlCommand)
		{
			if(IsParameter(srcParameterName, srcSqlCommand))
            {
				if (srcSqlCommand.Parameters[srcParameterName].SqlDbType == SqlDbType.DateTime)
				{
					if(srcParameterValue != null)
                    {
						DateTime dateTime = (DateTime)srcParameterValue;
						if (dateTime == DateTime.MinValue) srcParameterValue = DBNull.Value;
						
                    }
				}		
				srcSqlCommand.Parameters[srcParameterName].Value = srcParameterValue;
			}
		}
		public static void AddParameter(string srcParameterName, SQL_PARAMETER_TYPE srcParameterType, SqlCommand srcSqlCommand)
		{
			SqlDbType type = ConvertType(srcParameterType);
			if (!IsParameter(srcParameterName, srcSqlCommand))
				srcSqlCommand.Parameters.Add(srcParameterName, type);
		}
		public static void SetStoredProcedure(SqlCommand srcSqlCommand)
        {
			srcSqlCommand.CommandType = CommandType.StoredProcedure;
		}
		public static object GetParameterValue(string srcParameterName, SQL_PARAMETER_TYPE srcParameterType, SqlCommand srcSqlCommand)
		{
			if (IsParameter(srcParameterName, srcSqlCommand))
			{
				if (srcSqlCommand.Parameters[srcParameterName].Value == null)
					return DefaultParameterValue(srcParameterType);
				if (srcSqlCommand.Parameters[srcParameterName].Value == DBNull.Value)
					return DefaultParameterValue(srcParameterType);
				return srcSqlCommand.Parameters[srcParameterName].Value;
			}
			else
				return DefaultParameterValue(srcParameterType);
		}
		public static object DefaultParameterValue(SQL_PARAMETER_TYPE srcParameterType)
        {
			switch (srcParameterType)
			{
				case SQL_PARAMETER_TYPE.LONG:
					return 0;
				case SQL_PARAMETER_TYPE.INT:
					return 0;
				case SQL_PARAMETER_TYPE.DATETIME:
					return DateTime.MinValue;
				case SQL_PARAMETER_TYPE.FLOAT:
					return 0.0F;
				case SQL_PARAMETER_TYPE.BOOL:
					return false;
				case SQL_PARAMETER_TYPE.STRING:
					return "";
				case SQL_PARAMETER_TYPE.SHORT:
					return 0;
				default:
					return "";
			}
		}
		public static void SetParameterOutput(string srcParameterName, SqlCommand srcSqlCommand)
		{
			if (IsParameter(srcParameterName, srcSqlCommand))
			{
				srcSqlCommand.Parameters[srcParameterName].Direction = ParameterDirection.Output;
			}
		}
		#endregion

		#region Новые варианты прямого чтения из базы данных
		public static ArrayList MakeArrayFromDatabase(SqlCommand srcSqlCommand, DelegateReadDt srcFunc)
		{
			if (srcSqlCommand == null)
			{
				Db.SetErrorProgram("Нулевая SQL комманда. FillArray");
				Db.ShowFaults();
				return null;
			}
			ArrayList elements = new ArrayList();
			SqlDataReader reader = null;
			try
			{
				reader = srcSqlCommand.ExecuteReader();
				while (reader.Read())
				{
					object element = srcFunc(reader);
					elements.Add(element);
				}
				if (reader != null) reader.Close();
			}
			catch (Exception E)
			{
				if (reader != null) reader.Close();
				Db.SetException(E);
				Db.ShowFaults();
				return null;
			}
			if (reader != null) reader.Close();
			return elements;
		}
		#endregion

		#region Вспомогательные методы - поиск и т.д.
		public static object Find(SqlCommand srcCommand, DelegateReadDt srcFunc)
		{
			ArrayList elements = DbSql.MakeArrayFromDatabase(srcCommand, srcFunc);
			if (elements.Count > 1)
			{
				Db.SetErrorMessage("В поиске более двух элементов");
				Db.ShowFaults();
				return null;
			}
			if (elements.Count == 0)
			{
				Db.SetErrorMessage("Объект не найден");
				Db.ShowFaults();
				return null;
			}
			return elements[0];
		}
		#endregion
	}
}
