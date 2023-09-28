using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbDetail.
	/// </summary>
	public class DbDetail:Db
	{
		private long code;					// Код детали (идентификатор)
		private string codeTxt;				// Код детали (по классификатору)
		private string name;				// Наименование
		private string comment;				// Примечание
		private bool oil;					// Флаг масел
		private string usage;				// Описание использования детали
		private string usage_short;			// Коротко использование детали
		private long codeDirectoryDetail;	// Ссылка на элемент классификатора деталей

		private static SqlConnection conn;
		private static SqlCommand cmdWrite;
		private static SqlCommand cmdSelect;
		private static SqlCommand cmdWriteCode;
		private static SqlCommand cmdSysDelete;

		private static SqlCommand cmdSelectSubNodeDetail;

		public enum SelectType{ByNot=0, ByCode=1, ByName=2};

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public DbDetail()
		{
			code				= 0;
			codeTxt				= "";
			name				= "";
			comment				= "";
			oil					= false;
			usage				= "";
			usage_short			= "";
			codeDirectoryDetail	= 0;

			adding		= true;
		}

		public DbDetail(DbDetail source)
		{
			code				= source.code;
			codeTxt				= source.codeTxt;
			name				= source.name;
			comment				= source.comment;
			oil					= source.oil;
			usage				= source.usage;
			usage_short			= source.usage_short;
			codeDirectoryDetail	= source.codeDirectoryDetail;
		}

		public DbDetail(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeTxt					= (string)GetValueString(reader, offset);	offset++;
			name					= (string)GetValueString(reader, offset);	offset++;
			comment					= (string)GetValueString(reader, offset);	offset++;
			oil						= (bool)GetValueBool(reader, offset);		offset++;

			usage					= (string)GetValueString(reader, offset);	offset++;
			usage_short				= (string)GetValueString(reader, offset);	offset++;
			codeDirectoryDetail		= (long)GetValueLong(reader, offset);		offset++;
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 5 + 3 собственных полей
			readerLength = 5 + 3;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_ДЕТАЛЬ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeTxt", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@oil", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@usage", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@usage_short", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@codeDirectoryDetail", SqlDbType.BigInt);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;


			cmdSelect = new SqlCommand("SELECT_ДЕТАЛЬ");
			cmdSelect.Connection = conn;
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);

			cmdWriteCode = new SqlCommand("WRITE_ДЕТАЛЬ_КОД", conn);
			cmdWriteCode.CommandType = CommandType.StoredProcedure;
			cmdWriteCode.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteCode.Parameters.Add("@newCodeTxt", SqlDbType.VarChar);
			Db.SetReturnError(cmdWriteCode);

			cmdSysDelete = new SqlCommand("SYSTEM_ДЕТАЛЬ_УДАЛИТЬ", conn);
			cmdSysDelete.CommandType = CommandType.StoredProcedure;
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			SetReturnError(cmdSysDelete);

			cmdSelectSubNodeDetail = new SqlCommand("SELECT_ПОДУЗЕЛ_ДЕТАЛЬ");
			cmdSelectSubNodeDetail.Connection = conn;
			cmdSelectSubNodeDetail.CommandType = CommandType.StoredProcedure;
			cmdSelectSubNodeDetail.Parameters.Add("@codeSubNode", SqlDbType.BigInt);
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
			item.Text				= codeTxt;
			item.SubItems[1].Text	= Name;
			item.SubItems[2].Text	= comment;
			item.SubItems[3].Text	= usage_short;
			item.SubItems[4].Text	= usage;
			item.Tag = this;
		}

		public static void FillList(ListView list, string mask, SelectType type)
		{
			cmdSelect.Parameters["@pattern"].Value = (string)mask;
			cmdSelect.Parameters["@type"].Value = (int)type;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbSubNode subNode)
		{
			cmdSelectSubNodeDetail.Parameters["@codeSubNode"].Value = (long)subNode.Code;
			Db.DbFillList(list, cmdSelectSubNodeDetail, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDetail element = new DbDetail(reader, 0);
			list.Items.Add(element.LVItem);
		}
		#endregion

		public string CodeTxt
		{
			get
			{
				return codeTxt;
			}
			set
			{
				codeTxt = value;
			}
		}

		public long Code
		{
			get
			{
				return code;
			}
		}

		public bool Oil
		{
			get
			{
				return oil;
			}
			set
			{
				if(oil == value) return;
				oil = value;
				changed = true;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string NameTxt
		{
			get
			{
				string text = name;
				if(usage_short.Length != 0) text += " " + usage_short;
				return text;
			}
		}

		

		public string Comment
		{
			get{ return comment;}
			set{ comment = this.SetStringLength(comment, value, 120, "Слишком длинное примечание");}
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@codeTxt"].Value				= (string)codeTxt;
				cmdWrite.Parameters["@name"].Value					= (string)name;
				cmdWrite.Parameters["@comment"].Value				= (string)comment;
				cmdWrite.Parameters["@oil"].Value					= (bool)oil;
				cmdWrite.Parameters["@usage"].Value					= (string)usage;
				cmdWrite.Parameters["@usage_short"].Value			= (string)usage_short;
				cmdWrite.Parameters["@codeDirectoryDetail"].Value	= (long)codeDirectoryDetail;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code =( long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			return true;
		}

		public bool WriteCode(string newCode)
		{
			newCode.Trim();
			if(newCode.Length == 0)
			{
			//  Разрешаем пустой код
			//	MessageBox.Show("Новый код не должен быит пуст");
			//	return false;
			}
			if(newCode.Length > 25)
			{
				MessageBox.Show("Длина кода не должна превышать 25 символов");
				return false;
			}
			try
			{
				cmdWriteCode.Parameters["@code"].Value = code;
				cmdWriteCode.Parameters["@newCodeTxt"].Value = newCode;
				cmdWriteCode.ExecuteNonQuery();
				ThrowReturnError(cmdWriteCode);
				codeTxt = newCode;				// Установка нового кода
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			return true;
		}

		public bool DetailDelete()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите удалить деталь?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}

		#region Доступ к основным параметрам - Изменение
		public string Usage
		{
			get
			{
				return usage;
			}
			set
			{
				usage = SetStringLength(usage, value, 256, "Применимость");
			}
		}
		public string UsageShort
		{
			get
			{
				return usage_short;
			}
			set
			{
				usage_short = SetStringLength(usage_short, value, 128, "Применимость коротко");
			}
		}
		#endregion
	}
}
