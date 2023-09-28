using System;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for Db.
	/// </summary>
	public class Db
	{
		

		// Перечислимые типы
		public enum ClickType:short{Select=1, Properties=2};
		public enum DocumentType:short{NoDOcument=0, Other=1, Invoice=2, Bill=3, Accumulate=4};
		public enum DocumentStatus:short{Opend=0, Implement=1, Null=2};
		public enum BarCodes:short{Unknown=0, Vaz=1};

		private static ArrayList faultList;	// Список описаний сбоев
		private static bool fault;			// При выполнении одной из операций - произошел сбой

		protected bool changed;				// Были ли изменены данные
		protected bool adding;				// Новый ли это элемент
		protected bool deleted;				// Элемент помечен на удаление
		protected short viewType;			// Тип отображения элемента в таблице

		public static FormFaults faultWindow;		// Окошко со списком ошибок
		public static BarCodes barcCodeType;		// Тип текущего выбранного штрих-кода

		public struct LongPair
		{
			public long data_main;
			public long data_add;
		}

		/* Конструктор */
		public Db()
		{
			changed = false;
			adding = false;
			deleted = false;
			if(faultList == null)
			{
				fault = false;
				faultList = new ArrayList();
			}
		}


		#region Работа с окном ошибок
		// Добавление в список нового исключения
		public static void SetException(Exception E)
		{
			if(faultList == null)
			{
				faultList = new ArrayList();
			}
			fault = true;
			faultList.Add("Ошибка " + E.Message);
		}
		// Добавление в список новой ошибки программы
		public static void SetErrorProgram(string err)
		{
			if(faultList == null)
			{
				faultList = new ArrayList();
			}
			fault = true;
			faultList.Add("Прграмная ошибка." + err);
		}
		// Добавление в список нового предупреждения об ошибочных данных
		public static void SetDataWarning(string message)
		{
			fault = true;
			faultList.Add("Неверные данные: " + message);
		}
		// Добавление в список нового предупреждения об ошибочных данных
		public static void SetErrorMessage(string message)
		{
			fault = true;
			faultList.Add(message);
		}
		// Показ списка всех накопившихся ошибок -  true если ошибки есть
		public static bool ShowFaults()
		{
			if(fault != true) return false;
			if(faultWindow != null)
			{
				faultWindow.InsertFaults(faultList);
				faultWindow.Show();
				faultWindow.BringToFront();
			}
			else
			{
				FormFaults dialog = new FormFaults(faultList);
				dialog.Show();
				faultWindow = dialog;
			}
			fault = false;
			faultList.Clear();
			return true;
		}
		#endregion


		// Свойства класса
		public bool Fault
		{
			get{ return fault;}
		}
		
		public bool Changed
		{
			get{ return changed;}
		}

		public bool Adding
		{
			get{ return adding;}
			set{adding = value;}
		}

		// Вспомагательные функции

		/* Округление до двух знаков */
		protected float MakeFloat(float data)
		{
			float result = (float)Math.Round(data, 2);
			return result;
		}


		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected float SetFloatNotMinus(float oldValue, float newValue, string message)
		{
			float data = MakeFloat(newValue);
			if(data == oldValue) return oldValue;
			if(data < 0.0F)
			{
				SetDataWarning(message);
				return oldValue;
			}
			changed = true;
			return data;
		}

		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected long SetLongNotZero(long oldValue, long newValue, string message)
		{
			if(newValue == oldValue) return oldValue;
			if(newValue <= 0)
			{
				SetDataWarning(message);
				return oldValue;
			}
			changed = true;
			return newValue;
		}

		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected int SetIntNotZero(int oldValue, string newValue)
		{
			int data;
			string dataTxt = newValue.Trim();
			try
			{
				data = (int)Convert.ToInt32(dataTxt);
			}
			catch(Exception)
			{
				return oldValue;
			}
			if(data == oldValue) return oldValue;
			if(data <= 0) return oldValue;
			changed = true;
			return data;
		}

		protected int SetIntNotZero(int oldValue, string newValue, string message)
		{
			int data;
			string dataTxt = newValue.Trim();
			try
			{
				data = (int)Convert.ToInt32(dataTxt);
			}
			catch(Exception E)
			{
				SetException(E);
				return oldValue;
			}
			if(data == oldValue) return oldValue;
			if(data <= 0)
			{
				SetDataWarning(message);
				return oldValue;
			}
			changed = true;
			return data;
		}

		protected int SetIntNotMinus(int oldValue, int newValue, string message)
		{
			if(newValue == oldValue) return oldValue;
			if(newValue < 0)
			{
				SetDataWarning(message);
				return oldValue;
			}
			changed = true;
			return newValue;
		}

		protected int SetInt(int oldValue, int newValue)
		{
			if(newValue == oldValue) return oldValue;
			changed = true;
			return newValue;
		}

		protected int SetIntNotMinus(int oldValue, string newValue, string message)
		{
			int data;
			string dataTxt = newValue.Trim();
			if(dataTxt.Length != 0)
			{
				try
				{
					data = (int)Convert.ToInt32(dataTxt);
				}
				catch(Exception)
				{
					SetDataWarning(message);
					return oldValue;	
				}
			}
			else data = 0;

			if(data == oldValue) return oldValue;
			if(data < 0)
			{
				SetDataWarning(message);
				return oldValue;

			}
			changed = true;
			return data;
		}

		protected long SetLongNotZero(long oldValue, string newValue, string message)
		{
			long data;
			string dataTxt = newValue.Trim();
			if(dataTxt.Length != 0)
			{
				try
				{
					data = (long)Convert.ToInt64(dataTxt);
				}
				catch(Exception)
				{
					SetDataWarning(message);
					return oldValue;	
				}
			}
			else data = 0;

			if(data == oldValue) return oldValue;
			if(data <= 0)
			{
				SetDataWarning(message);
				return oldValue;

			}
			changed = true;
			return data;
		}

		protected long SetLongNotMinus(long oldValue, string newValue, string message)
		{
			long data;
			string dataTxt = newValue.Trim();
			if(dataTxt.Length != 0)
			{
				try
				{
					data = (long)Convert.ToInt64(dataTxt);
				}
				catch(Exception)
				{
					SetDataWarning(message);
					return oldValue;	
				}
			}
			else data = 0;

			if(data == oldValue) return oldValue;
			if(data < 0)
			{
				SetDataWarning(message);
				return oldValue;

			}
			changed = true;
			return data;
		}


		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected float SetFloatNotMinus(float oldValue, string newValue, string message)
		{
			float data;
			string dataTxt = newValue.Trim();
			dataTxt = dataTxt.Replace(".", ",");
			try
			{
				data = (float)Convert.ToDouble(dataTxt);
			}
			catch(Exception)
			{
				SetDataWarning(message);
				return oldValue;
			}
			data = MakeFloat(data);
			if(data == oldValue) return oldValue;
			if(data < 0.0F)
			{
				SetDataWarning(message);
				return oldValue;
			}
			changed = true;
			return data;
		}


		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected string SetStringNotEmptyLength(string oldValue, string newValue, int length, string message)
		{
			string data = newValue.Trim();
			if(data.Length == 0 || data.Length > length)
			{
				SetDataWarning(message);
				return oldValue;
			}
			if(data == oldValue) return oldValue;
			changed = true;
			return data;
		}

		/* Устанавливаем новое значение, с проверкой на правильность и неизменность */
		protected string SetStringLength(string oldValue, string newValue, int length, string message)
		{
			string data = newValue.Trim();
			if(data.Length > length)
			{
				SetDataWarning(message);
				return oldValue;
			}
			if(data == oldValue) return oldValue;
			changed = true;
			return data;
		}
																					
		// То что связанно с формами
		public static ListViewItem GetItemPosition(ListView list)
		{
			if(list == null) return null;
			Point pnt = Cursor.Position;
			pnt = list.PointToClient(pnt);
			ListViewItem item = list.GetItemAt(pnt.X, pnt.Y);
			return item;
		}
		public static TreeNode GetItemPosition(TreeView tree)
		{
			if(tree == null) return null;
			Point pnt = Cursor.Position;
			pnt = tree.PointToClient(pnt);
			TreeNode node = tree.GetNodeAt(pnt.X, pnt.Y);
			return node;
		}

		public static int GetColumnPosition(ListView list)
		{
			Point pnt = Cursor.Position;
			if(list.Items.Count > 0)
			{
				pnt.X = pnt.X - list.Items[0].Bounds.X;
			}
			pnt = list.PointToClient(pnt);
			// Проверяем место клика
			int start = 0;
			int end = 0;
			int count = list.Columns.Count;
			for(int i = 0; i < count; i++)
			{
				start = end;
				end = start + list.Columns[i].Width;
				if((pnt.X > start)&&(pnt.X < end))
				{
					return i;
				}
			}
			return -1;
		}

		public static TextBox MakeBox(Form form, ListViewItem item, int index)
		{
			int start = 0;
			for(int i = 0; i < index; i++) start += item.ListView.Columns[i].Width;
			int end = start + item.ListView.Columns[index].Width;
			//
				start = start + item.Bounds.X;
				end = end + item.Bounds.X;
			//
			int ypos = item.Bounds.Top;
			int yhght = item.Bounds.Height;
			int xpos = start;
			int xwdth = end - start;
			Point pnt0 = new Point(xpos, ypos);
			pnt0 = item.ListView.PointToScreen(pnt0);
			pnt0 = form.PointToClient(pnt0);
			Size sz0 = new Size(xwdth, yhght);
			TextBox box = new TextBox();
			form.Controls.Add(box);
			box.Location = pnt0;
			box.Size = sz0;
			box.Visible = true;
			box.Capture = true;
			box.Show();
			box.BringToFront();
			box.Focus();
			return box;
		}

		public static ComboBox MakeComboBox(Form form, ListViewItem item, int index)
		{
			int start = 0;
			for(int i = 0; i < index; i++) start += item.ListView.Columns[i].Width;
			int end = start + item.ListView.Columns[index].Width;
			//
			start = start + item.Bounds.X;
			end = end + item.Bounds.X;
			//
			int ypos = item.Bounds.Top;
			int yhght = item.Bounds.Height;
			int xpos = start;
			int xwdth = end - start;
			Point pnt0 = new Point(xpos, ypos);
			pnt0 = item.ListView.PointToScreen(pnt0);
			pnt0 = form.PointToClient(pnt0);
			Size sz0 = new Size(xwdth, yhght);
			ComboBox box = new ComboBox();
			form.Controls.Add(box);
			box.Location = pnt0;
			box.Size = sz0;
			box.Visible = true;
			box.Capture = true;
			box.Show();
			box.BringToFront();
			box.Focus();
			return box;
		}

		public static FormSelectionType1 MakeFormSelectionType1(Form form, ListViewItem item, int index, ArrayList array, string initialText, bool emptyElement)
		{
			int start = 0;
			for(int i = 0; i < index; i++) start += item.ListView.Columns[i].Width;
			int ypos = item.Bounds.Top;
			int xpos = start;
			Point pnt0 = new Point(xpos, ypos);
			pnt0 = item.ListView.PointToScreen(pnt0);
			FormSelectionType1 box = new FormSelectionType1(array, initialText, emptyElement);
			box.StartPosition = FormStartPosition.Manual;
			box.Location = pnt0;
			box.Size	= new Size(293, 275);
			return box;
		}
		public static FormSelectionType1 MakeFormSelectionType1(Control form, TextBox textBox, ArrayList array, string initialText, bool emptyElement)
		{
			int ypos = textBox.Top;
			int xpos = textBox.Left;
			Point pnt0 = new Point(xpos, ypos);
			pnt0 = form.PointToScreen(pnt0);
			FormSelectionType1 box = new FormSelectionType1(array, initialText, emptyElement);
			box.StartPosition = FormStartPosition.Manual;
			box.Location = pnt0;
			box.Size	= new Size(textBox.Right - textBox.Left, 275);
			return box;
		}
		public static FormSelectionType2 MakeFormSelectionType2(Form form, ListViewItem item, int index, string initialText)
		{
			int start = 0;
			for(int i = 0; i < index; i++) start += item.ListView.Columns[i].Width;
			int ypos = item.Bounds.Top;
			int xpos = start;
			Point pnt0 = new Point(xpos, ypos);
			pnt0 = item.ListView.PointToScreen(pnt0);
			FormSelectionType2 box = new FormSelectionType2(initialText);
			box.StartPosition = FormStartPosition.Manual;
			box.Location = pnt0;
			box.Size	= new Size(240, 20);
			return box;
		}

		public static FormSelectionType3 MakeFormSelectionType3(Form form, ListViewItem item, int column, ListView list)
		{
			int start = 0;
			for(int i = 0; i < column; i++) start += item.ListView.Columns[i].Width;
			int xpos = start;
			Point pnt0 = new Point(xpos, 0);
			pnt0 = item.ListView.PointToScreen(pnt0);
			int ypos = list.Bounds.Bottom;
			Point pnt1 = new Point(0, ypos);
			pnt1	= form.PointToScreen(pnt1);
			pnt0 = new Point(pnt0.X, pnt1.Y);
			FormSelectionType3 box = new FormSelectionType3(list, column);
			box.StartPosition = FormStartPosition.Manual;
			box.Location = pnt0;
			box.Size	= new Size(item.ListView.Columns[column].Width, 20);
			return box;
		}

		public static ListViewItem GetItemSelected(ListView list)
		{
			if(list == null) return null;
			if(list.SelectedItems == null) return null;
			if(list.SelectedItems.Count == 0) return null;
			return list.SelectedItems[0];
		}

		public static TreeNode GetItemSelected(TreeView tree)
		{
			if(tree == null) return null;
			if(tree.SelectedNode == null) return null;
			return tree.SelectedNode;
		}

		public string FloatToCFormat(float val)
		{
			System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
			format.CurrencyGroupSeparator = " ";
			format.CurrencyGroupSizes[0]= 3;
			format.CurrencyDecimalDigits = 2;
			format.CurrencySymbol = "";
			string text = val.ToString("c", format);
			return text;
		}

		#region Чтение данных разного типа из SqlDataReader
		public string GetValueString(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return "";
				return (string)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return "";
			}
		}
		public bool GetValueBool(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return false;
				return (bool)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
		}
		public long GetValueLong(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return 0;
				return (long)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return 0;
			}
		}
		public float GetValueFloat(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return 0.0F;
				return (float)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return 0.0F;
			}
		}
		public DateTime GetValueDate(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return DateTime.Now;
				return (DateTime)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return DateTime.Now;
			}
		}
		public bool IsValueNull(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return true;
				return false;
			}
			catch(Exception E)
			{
				SetException(E);
				return true;
			}
		}
		public int GetValueInt(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return 0;
				return (int)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return 0;
			}
		}
		public int GetValueSmallInt(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return 0;
				return (short)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return 0;
			}
		}
		public short GetValueShort(SqlDataReader reader, int index)
		{
			try
			{
				if(reader.IsDBNull(index) == true) return 0;
				return (short)reader.GetValue(index);
			}
			catch(Exception E)
			{
				SetException(E);
				return 0;
			}
		}
		#endregion
		
		#region Заполнение листа, массива макет

		public delegate void DelegateInsertInList(SqlDataReader reader, ListView list);

		public static void DbFillList(ListView list, SqlCommand cmd, DelegateInsertInList func)
		{
			if(Reconnect() == true) return;
			if(cmd == null)
			{
				SetErrorProgram("Нулевая SQL комманда. FillList");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						func(reader, list);
					}
					if(reader != null) reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					SetException(E);
					ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			ShowFaults();
		}

		public delegate void DelegateInsertInArray(SqlDataReader reader, ArrayList array);
		public static void FillArray(ArrayList array, SqlCommand cmd, DelegateInsertInArray func)
		{
			if(cmd == null)
			{
				SetErrorProgram("Нулевая SQL комманда. FillArray");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						func(reader, array);
					}
					reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					SetException(E);
					ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			ShowFaults();
		}

		#endregion

		#region Заполнение дерева, макет
	
		public delegate void DelegateInsertInTree(SqlDataReader reader, TreeNodeCollection tree);

		public static void DbFillTree(TreeNodeCollection tree, SqlCommand cmd, DelegateInsertInTree func)
		{
			if(Reconnect() == true) return;
			if(cmd == null)
			{
				SetErrorProgram("Нулевая SQL комманда. FillTree");
			}
			else
			{
				SqlDataReader reader = null;
				try
				{
					reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						func(reader, tree);
					}
					reader.Close();
				}
				catch(Exception E)
				{
					if(reader != null) reader.Close();
					SetException(E);
					ShowFaults();
					return;
				}
				if(reader != null) reader.Close();
			}
			ShowFaults();
		}

		#endregion

		#region Отображалки в текст

		public static string DateToTxt(DateTime date)
		{
			string txt = date.ToShortDateString();
			return txt;
		}
		public static string SpanToShortTxt(TimeSpan span)
		{
			string tmp = span.Minutes.ToString();
			if (tmp.Length == 1) tmp = "0" + tmp;
			string txt = (span.Days*24 + span.Hours).ToString() + ":" + tmp;
			return txt;
		}

		public static string CachToTxt(double cach)
		{
			string text = "";
			System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
			format.CurrencyGroupSeparator = " ";
			format.CurrencyGroupSizes[0]= 3;
			format.CurrencyDecimalDigits = 2;
			format.CurrencySymbol = "";
			format.CurrencyDecimalSeparator = "-";
			text = cach.ToString("c", format);
			return text;
		}

		public static string CachToTxt(float cach)
		{
			string text = "";
			System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
			format.CurrencyGroupSeparator = " ";
			format.CurrencyGroupSizes[0]= 3;
			format.CurrencyDecimalDigits = 2;
			format.CurrencySymbol = "";
			format.CurrencyDecimalSeparator = "-";
			text = cach.ToString("c", format);
			return text;
		}

		public static string FloatToTxt(float data)
		{
			string text = "";
			//System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
			//format.NumberDecimalDigits = 2;
			//format.NumberDecimalSeparator = ",";
			//format.NumberGroupSeparator = "";
			text = data.ToString();
			return text;
		}

		#endregion

		protected static bool Reconnect()
		{
			if(Form1.reconnect == true)
			{
				MessageBox.Show("Необходима повторная авторизация");
				return true;
			}
			return false;
		}

		public static int ExecuteCommand(SqlCommand cmd)
		{
			int count = 0;
			if(Reconnect() == true) return 0;
			if(cmd == null)
			{
				SetErrorProgram("Нулевая SQL комманда");
			}
			else
			{
				try
				{
					count = cmd.ExecuteNonQuery();
				}
				catch(Exception E)
				{
					SetException(E);
				}
			}
			if(ShowFaults()) return -1;
			MessageBox.Show("Успешно задействовано " + count.ToString() + " записей");
			return count;
		}

		public static bool ExecuteCommandError(SqlCommand cmd)
		{
			if(Reconnect() == true) return false;
			int count = 0;
			if(cmd == null)
			{
				SetErrorProgram("Нулевая SQL комманда");
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
					SetException(E);
				}
			}
			if(ShowFaults()) return false;
			MessageBox.Show("Успешно задействовано " + count.ToString() + " записей");
			return true;
		}
	
		protected static void SetReturnError(SqlCommand cmd)
		{
			cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 120);
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

		public void SetViewType(short type)
		{
			viewType = type;
		}

		#region Виртуальные функции
		virtual public string[] Inform(int infoLevel){return null;}
		virtual public string DbTitle(){return "";}
		virtual public string DbTitleEx(){return "";}
		#endregion

		#region Дополнительные функции обработки строк
		public static string FirstUpper(string str)
		{
			// Делаем (Только если необходимо!) заглавной первую букву
			// Если строчка нулевая или пустая возвращяем ее саму без изменений
			if((str == null) || (str.Length == 0)) return str;
			string tmpStr	= str.Trim();					// Убираем пробелы
			if (tmpStr.Length == 0) return tmpStr;			// Если строчка состояла из одних пробелов - возвращаем пустую строчку
			string firstCharacter = tmpStr.Substring(0, 1);	// Получаем первую букву
			// Если первая буква уже была заглавной возвращаем строчку (возможно без пробелов)
			if(firstCharacter.CompareTo(firstCharacter.ToUpper()) == 0) return tmpStr;
			firstCharacter = firstCharacter.ToUpper();		// Делаем первый символ заглавным
			tmpStr = tmpStr.Remove(0, 1);					// Убираем первый символ из строчки
			tmpStr = tmpStr.Insert(0, firstCharacter);		// Добавляем первый заглавный символ
			return tmpStr;									// Возвращаем строчку
		}
		public static string FirstUpperSpace(string str)
		{
			// Делаем (Только если необходимо!) заглавной первую букву
			// Если строчка нулевая или пустая возвращяем ее саму без изменений
			if((str == null) || (str.Length == 0)) return str;
			string tmpStr	= str.TrimStart();				// Убираем пробелы
			if (tmpStr.Length == 0) return tmpStr;			// Если строчка состояла из одних пробелов - возвращаем пустую строчку
			string firstCharacter = tmpStr.Substring(0, 1);	// Получаем первую букву
			// Если первая буква уже была заглавной возвращаем строчку (возможно без пробелов)
			if(firstCharacter.CompareTo(firstCharacter.ToUpper()) == 0) return tmpStr;
			firstCharacter = firstCharacter.ToUpper();		// Делаем первый символ заглавным
			tmpStr = tmpStr.Remove(0, 1);					// Убираем первый символ из строчки
			tmpStr = tmpStr.Insert(0, firstCharacter);		// Добавляем первый заглавный символ
			return tmpStr;									// Возвращаем строчку
		}

		public static string Initial(string str)
		{
			if((str == null)||(str.Length == 0)) return "";
			string firstCharacter = str.Substring(0, 1);
			firstCharacter = firstCharacter.ToUpper();
			firstCharacter = firstCharacter + ".";
			return firstCharacter;
		}
		#endregion

		#region Дополнительные методы стандартных запросов
		public static bool CheckSysPass()
		{
			// метод проверки системнеого пароля
			FormSelectString dialog = new FormSelectString("Введите системный пароль", "");
			dialog.ShowDialog();
			if (dialog.SelectedText != "ybabuf")
			{
				MessageBox.Show("Пароль не верен");
				return false;
			}
			return true;
		}
		public static bool CheckReportPass()
		{
			// метод проверки системнеого пароля
			FormSelectString dialog = new FormSelectString("Введите системный пароль", "");
			dialog.ShowDialog();
			if (dialog.SelectedText != "kostic")
			{
				MessageBox.Show("Пароль не верен");
				return false;
			}
			return true;
		}
		public static bool CheckSysAction(string question)
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show(question, "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			return true;
		}
		#endregion

		#region Стандартное чтение таблицы базы данных
		public void GetFields(SqlConnection connection, string table_name)
		{
			string sql_command = "SELECT * FROM " + table_name;
			SqlCommand cmd = new SqlCommand(sql_command, connection);
			SqlDataReader reader = cmd.ExecuteReader();
			// Получение имен 
			reader.Close();
		}
		#endregion
	}
}
