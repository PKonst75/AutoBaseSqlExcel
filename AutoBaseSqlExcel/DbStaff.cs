using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Список сотрудников
	/// </summary>
	public class DbStaff:Db
	{
		private long code;
		private string firstName;
		private string name;
		private string secondName;

		private static SqlConnection conn;

		private static SqlCommand cmdWrite;
		private static SqlCommand cmdExecSelect;
		private static SqlCommand cmdSelectLogin;
		private static SqlCommand cmdSelectFind;
		private static SqlCommand cmdSelectESign;

		private static SqlCommand cmd_find;

		// Системные функции с ограниченным доступом
		private static SqlCommand cmdSysDelete;
		private static SqlCommand cmdSysChange;


		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 4 собственных поля
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ПЕРСОНАЛ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@firstName", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@secondName", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdExecSelect = new SqlCommand("SELECT_ПЕРСОНАЛ", conn);
			cmdExecSelect.CommandType = CommandType.StoredProcedure;

			cmd_find = new SqlCommand("SELECT_ПЕРСОНАЛ_ПОИСК_1", conn);
			cmd_find.Parameters.Add("@code", SqlDbType.BigInt);
			cmd_find.CommandType = CommandType.StoredProcedure;

			// Поиск по фамилии
			cmdSelectFind = new SqlCommand("SELECT_ПЕРСОНАЛ_ПОИСК", conn);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
			cmdSelectFind.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelectFind.Parameters.Add("@type", SqlDbType.Int);

			// Подбор элемента по логину
			cmdSelectLogin = new SqlCommand("SELECT_ПЕРСОНАЛ_ЛОГИН", conn);
			cmdSelectLogin.Parameters.Add("@login", SqlDbType.VarChar);
			cmdSelectLogin.CommandType = CommandType.StoredProcedure;

			// Подбор элемента по электронной подписи
			cmdSelectESign = new SqlCommand("SELECT_ПЕРСОНАЛ_ЭЛЕКТРОННАЯ_ПОДПИСЬ", conn);
			cmdSelectESign.Parameters.Add("@e_sign", SqlDbType.BigInt);
			cmdSelectESign.CommandType = CommandType.StoredProcedure;

			cmdSysDelete = new SqlCommand("SYSTEM_ПЕРСОНАЛ_УДАЛИТЬ", conn);
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysDelete.CommandType = CommandType.StoredProcedure;
			SetReturnError(cmdSysDelete);

			cmdSysChange = new SqlCommand("SYSTEM_ПЕРСОНАЛ_ЗАМЕНИТЬ", conn);
			cmdSysChange.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysChange.Parameters.Add("@codeOld", SqlDbType.BigInt);
			cmdSysChange.CommandType = CommandType.StoredProcedure;
			SetReturnError(cmdSysChange);
		}

		public DbStaff(SqlDataReader reader, int offset)
		{
			code		= this.GetValueLong(reader, offset);	offset++;
			firstName	= this.GetValueString(reader, offset);	offset++;
			name		= this.GetValueString(reader, offset);	offset++;
			secondName	= this.GetValueString(reader, offset);	offset++;
		}

		public DbStaff()
		{
			code = 0;
			firstName = "";
			name = "";
			secondName = "";

			adding = true;
		}

		public DbStaff(DbStaff source)
		{
			code = source.code;
			firstName = source.firstName;
			name = source.name;
			secondName = source.secondName;
		}

		public string FirstName
		{
			set
			{
				firstName = this.SetStringNotEmptyLength(firstName, value, 50, "ФАМИЛИЯ");
			}
			get
			{
				return firstName;
			}
		}

		public string Name
		{
			set
			{
				name = this.SetStringLength(name, value, 50, "ИМЯ");
			}
			get
			{
				return name;
			}
		}

		public string SecondName
		{
			set
			{
				secondName = this.SetStringLength(secondName, value, 50, "ОТЧЕСТВО");
			}
			get
			{
				return secondName;
			}
		}

		public string Title
		{
			get
			{
				return firstName + " " + name + " " + secondName;
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
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = this.firstName;
			item.SubItems[1].Text = this.name + " " + this.secondName;
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdExecSelect, new DelegateInsertInList(InsertInList));
		}
		public static void FindFirstName(ArrayList array, string firstName)
		{
			cmdSelectFind.Parameters["@pattern"].Value = firstName;
			cmdSelectFind.Parameters["@type"].Value = 1;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbStaff element = new DbStaff(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdExecSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbStaff element = new DbStaff(reader, 0);
			array.Add(element);
		}
		#endregion

		public long Code
		{
			get
			{
				return code;
			}
			set
			{
				code = value;
			}
		}

		/* Реализация добавления нового элемента */
		public bool Write()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value = (bool)adding;
				cmdWrite.Parameters["@code"].Value = (long)code;
				cmdWrite.Parameters["@name"].Value = (string)name;
				cmdWrite.Parameters["@firstName"].Value = (string)firstName;
				cmdWrite.Parameters["@secondName"].Value = (string)secondName;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code = (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				trans.Rollback();
				SetTransaction(null);
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			trans.Commit();
			SetTransaction(null);
			MessageBox.Show("Сотрудник добавлен");
			return true;
		}

		// Получаем новый элемент, по заданному логину
		public static DbStaff GetByLogin(string login)
		{
			DbStaff newElement = null;
			SqlDataReader reader = null;
			try
			{
				cmdSelectLogin.Parameters["@login"].Value = (string)login;
				reader = cmdSelectLogin.ExecuteReader();
				reader.Read();
				newElement = new DbStaff(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			if(ShowFaults()) return null;
			return newElement;
		}

		// Получаем новый элемент, по заданному логину
		public static DbStaff Find(long code)
		{
			DbStaff newElement = null;
			SqlDataReader reader = null;
			try
			{
				cmd_find.Parameters["@code"].Value = (long)code;
				reader = cmd_find.ExecuteReader();
				if(reader.Read())
					newElement = new DbStaff(reader, 0);
				else
					newElement = null;
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			if(ShowFaults()) return null;
			return newElement;
		}

		// Получаем новый элемент, по заданной электронной подписи
		public static DbStaff GetByESign(long e_sign)
		{
			DbStaff newElement = null;
			SqlDataReader reader = null;
			try
			{
				cmdSelectESign.Parameters["@e_sign"].Value = (long)e_sign;
				reader = cmdSelectESign.ExecuteReader();
				if(reader.Read() == true)
					newElement = new DbStaff(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			if(ShowFaults()) return null;
			return newElement;
		}

		public bool Delete()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите удалить персонал?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}
		public bool Replace(DbStaff staff)
		{
			if(Db.CheckSysPass() == false) return false;
			if(staff == null) return false;
			string text = "Вы уверены что хотите заменить " + this.Title + " на " + staff.Title + "?";
			DialogResult res = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysChange.Parameters["@codeOld"].Value = (long)code;
			cmdSysChange.Parameters["@code"].Value = (long)staff.Code;
			return Db.ExecuteCommandError(cmdSysChange);
		}
		public override string[] Inform(int infoLevel)
		{
			string[] infoStrings	= null;

			switch (infoLevel)
			{
				default:
						infoStrings = new string[3];
						infoStrings[0] = "Фамилия :\t\t" + this.FirstName;
						infoStrings[1] = "Имя :\t\t\t" + this.Name;
						infoStrings[2] = "Отчество :\t\t" + this.SecondName;
				break;
			}
			return infoStrings;
		}

		override public string DbTitle()
		{
			return this.Title;
		}

		public static DbStaff GetByESign(string caption)
		{
			// Запрос электронной подписи
			long e_sign = 0;
			FormSelectString dialog = new FormSelectString(caption, "", true);
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			e_sign = dialog.SelectedLong;
			if(e_sign == 0)
			{
				MessageBox.Show("Электронная подпись не опознана");
				return null;
			}
			DbStaff staff =  GetByESign(e_sign);
			if(staff == null)
			{
				MessageBox.Show("Электронная подпись не опознана");
				return null;
			}
			MessageBox.Show("Электронная подпись " + staff.Title);
			return staff;
		}
	}
}
