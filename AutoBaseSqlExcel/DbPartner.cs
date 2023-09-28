using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPartner.
	/// </summary>
	public class DbPartner:Db
	{
		public enum PartnerFunction:int{Unknown=0, SellerAuto=1, InnerCustomerAuto=2};
		// Основные параметры
		private long code;				// Код
		private long codeGroup;			// Код группы контрагентов
		private string nameShort;		// Краткое наименование
		private bool juridical;			// Флаг юридического лица
		// Для физических лиц
		private string firstName;
		private string name;
		private string secondName;
		private DateTime birth;
		private string addressRegistration;
		private string addressLiving;
		private string phone;
		// Для юридических лиц
		private string nameJuridical;
		private string addressJuridical;
		private string addressFact;
		private string inn;
		private string kpp;
		private string contact;
		private string contactPhone;
		private string settlementAccount;
		// Общие
		private string comment;

		// Связь с базой данных
		private static SqlConnection	conn;
		private static SqlCommand		cmdExecFind;
		private static SqlCommand		cmdSelect;
		private static SqlCommand		cmdSelectFunction;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdWriteFunction;
		private static SqlCommand		cmdSysDelete;
		private static SqlCommand		cmdSysChange;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Конструкторы
		public DbPartner(bool isJuridical)
		{
			code		= 0;
			codeGroup	= 0;
			nameShort	= "";
			juridical	= isJuridical;
			// Для частных лиц
			firstName				= "";
			name					= "";
			secondName				= "";
			birth					= DateTime.Today;
			addressRegistration		= "";
			addressLiving			= "";
			phone					= "";
			// Для юридических лиц
			nameJuridical			= "";
			addressJuridical		= "";
			addressFact				= "";
			inn						= "";
			kpp						= "";
			contact					= "";
			contactPhone			= "";
			settlementAccount		= "";
			//Общие
			comment					= "";

			adding = true;
		}
		public DbPartner(SqlDataReader reader, int offset)
		{
			code					= (long)GetValueLong(reader, offset);		offset++;
			codeGroup				= (long)GetValueLong(reader, offset);		offset++;
			nameShort				= (string)GetValueString(reader, offset);	offset++;
			juridical				= (bool)GetValueBool(reader, offset);		offset++;
			// Для физических лиц
			firstName				= (string)GetValueString(reader, offset);	offset++;
			name					= (string)GetValueString(reader, offset);	offset++;
			secondName				= (string)GetValueString(reader, offset);	offset++;
			birth					= (DateTime)GetValueDate(reader, offset);	offset++;
			addressRegistration		= (string)GetValueString(reader, offset);	offset++;
			addressLiving			= (string)GetValueString(reader, offset);	offset++;
			phone					= (string)GetValueString(reader, offset);	offset++;
			//Для юридических лиц
			nameJuridical			= (string)GetValueString(reader, offset);	offset++;
			addressJuridical		= (string)GetValueString(reader, offset);	offset++;
			addressFact				= (string)GetValueString(reader, offset);	offset++;
			inn						= (string)GetValueString(reader, offset);	offset++;
			kpp						= (string)GetValueString(reader, offset);	offset++;
			contact					= (string)GetValueString(reader, offset);	offset++;
			contactPhone			= (string)GetValueString(reader, offset);	offset++;
			settlementAccount		= (string)GetValueString(reader, offset);	offset++;
			// Общие
			comment					= (string)GetValueString(reader, offset);	offset++;
		}	
		public DbPartner(DbPartner source)
		{
			code		= source.code;
			codeGroup	= source.codeGroup;
			nameShort	= source.nameShort;
			juridical	= source.juridical;
			// Для частных лиц
			firstName				= source.firstName;
			name					= source.name;
			secondName				= source.secondName;
			birth					= source.birth;
			addressRegistration		= source.addressRegistration;
			addressLiving			= source.addressLiving;
			phone					= source.phone;
			// Для юридических лиц
			nameJuridical			= source.nameJuridical;
			addressJuridical		= source.addressJuridical;
			addressFact				= source.addressFact;
			inn						= source.inn;
			kpp						= source.kpp;
			contact					= source.contact;
			contactPhone			= source.contactPhone;
			settlementAccount		= source.settlementAccount;
			//Общие
			comment					= source.comment;
		}
		#endregion

		#region Инициализация
		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 20 собственных полей
			readerLength = 20;

			conn = connection;
				
			cmdExecFind = new SqlCommand("SELECT_КОНТРАГЕНТ_ПОИСК", conn);
			cmdExecFind.CommandType = CommandType.StoredProcedure;
			cmdExecFind.Parameters.Add("@code", SqlDbType.VarChar);
		
			cmdSelect = new SqlCommand("SELECT_КОНТРАГЕНТ", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;
			cmdSelect.Parameters.Add("@juridical", SqlDbType.Bit);
			cmdSelect.Parameters.Add("@pattern", SqlDbType.VarChar);
			cmdSelect.Parameters.Add("@type", SqlDbType.Int);

			cmdSelectFunction = new SqlCommand("SELECT_КОНТРАГЕНТ_ФУНКЦИЯ", conn);
			cmdSelectFunction.CommandType = CommandType.StoredProcedure;
			cmdSelectFunction.Parameters.Add("@function", SqlDbType.Int);

			cmdWrite = new SqlCommand("WRITE_КОНТРАГЕНТ", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeGroup", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@nameShort", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@juridical", SqlDbType.Bit);
			// Для физических лиц
			cmdWrite.Parameters.Add("@firstName", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@secondName", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@birth", SqlDbType.DateTime);
			cmdWrite.Parameters.Add("@addressRegistration", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@addressLiving", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@phone", SqlDbType.VarChar);
			// Для юридических лиц
			cmdWrite.Parameters.Add("@nameJuridical", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@addressJuridical", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@addressFact", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@inn", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@kpp", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@contact", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@contactPhone", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@settlementAccount", SqlDbType.VarChar);
			// Общие
			cmdWrite.Parameters.Add("@comment", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteFunction = new SqlCommand("WRITE_КОНТРАГЕНТ_ФУНКЦИЯ", conn);
			cmdWriteFunction.CommandType = CommandType.StoredProcedure;
			cmdWriteFunction.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWriteFunction.Parameters.Add("@function", SqlDbType.Int);
			cmdWriteFunction.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWriteFunction);

			cmdSysDelete = new SqlCommand("SYSTEM_КОНТРАГЕНТ_УДАЛИТЬ", conn);
			cmdSysDelete.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysDelete.CommandType = CommandType.StoredProcedure;
			SetReturnError(cmdSysDelete);

			cmdSysChange = new SqlCommand("SYSTEM_КОНТРАГЕНТ_ЗАМЕНИТЬ", conn);
			cmdSysChange.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSysChange.Parameters.Add("@codeNew", SqlDbType.BigInt);
			cmdSysChange.CommandType = CommandType.StoredProcedure;
			SetReturnError(cmdSysChange);

		}
		public static void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction = trans;
		}
		#endregion

		#region Доступ к основным параметрам - чтение
		public long Code
		{
			get
			{
				return code;
			}
		}
		public bool Juridical
		{
			get
			{
				return juridical;
			}
		}
		#endregion

		#region Доступ к основным параметрам - изменение
		public string Inn
		{
			get
			{
				return inn;
			}
			set
			{
				if(juridical)
					inn = SetStringLength(inn, value, 25, "ИНН ЮРИДИЧЕСКОГО ЛИЦА");
				else
					inn = SetStringLength(inn, value, 25, "ИНН ФИЗИЧЕСКОГО ЛИЦА");
			}
		}
		
		public string AddressFact
		{
			get
			{
				return addressFact;
			}
			set
			{
				addressFact = SetStringLength(addressFact, value, 255, "ФАКТИЧЕСКИЙ АДРЕС");
			}
		}
		public string Phone
		{
			get
			{
				return phone;
			}
			set
			{
				phone = SetStringLength(phone, value, 128, "КОНТАКТНЫЕ ТЕЛЕФОНЫ");
			}
		}
		public string Kpp
		{
			get
			{
				return kpp;
			}
			set
			{
				kpp = SetStringLength(kpp, value, 25, "КПП");
			}
		}
		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = SetStringLength(comment, value, 50, "КОМЕНТАРИЙ");
			}
		}
		public string NameShort
		{
			get
			{
				return nameShort;
			}
			set
			{
				nameShort = SetStringNotEmptyLength(nameShort, value, 60, "Краткое наименование");
			}
		}
		public string AddressRegistration
		{
			get
			{
				return addressRegistration;
			}
			set
			{
				addressRegistration = SetStringLength(addressRegistration, value,256, "Прописка");
			}
		}
		public string AddressLiving
		{
			get
			{
				return addressLiving;
			}
			set
			{
				addressLiving = SetStringLength(addressLiving, value, 256, "Адрес проживания");
			}
		}
		public string AddressJuridical
		{
			get
			{
				return addressJuridical;
			}
			set
			{
				addressJuridical = SetStringLength(addressJuridical, value, 256, "Юридический адрес");
			}
		}
		public string NameJuridical
		{
			get
			{
				return nameJuridical;
			}
			set
			{
				nameJuridical = SetStringLength(nameJuridical, value, 256, "Юридическое наименование");
			}
		}
		public string Contact
		{
			get
			{
				return contact;
			}
			set
			{
				contact = SetStringLength(contact, value, 60, "Связь с юрлицом");
			}
		}
		public string ContactPhone
		{
			get
			{
				return contactPhone;
			}
			set
			{
				contactPhone = SetStringLength(contactPhone, value, 128, "Контактные телефоны");
			}
		}
		public string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				if(juridical)
					firstName = "";
				else
					firstName = SetStringLength(firstName, value, 25, "Фамилия");
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
				if(juridical)
					name = "";
				else
					name = SetStringLength(name, value, 25, "Имя");
			}
		}
		public string SecondName
		{
			get
			{
				return secondName;
			}
			set
			{
				if(juridical)
					secondName = "";
				else
					secondName = SetStringLength(secondName, value, 25, "Отчество");
			}
		}
		#endregion

		
		public DateTime Birth
		{
			get
			{
				return birth;
			}
			set
			{
				if(juridical)
					birth = DateTime.Now;
				else
					birth = value;
			}
		}

		
		#region Отображение параметров в текст
		public string PhoneTxt
		{
			get
			{
				if(juridical == true)
				{
					return ContactPhone;
				}
				else
				{
					return Phone;
				}
			}
		}
		public string Title
		{
			get
			{
				if(juridical == false)
				{
					return firstName + " " + name + " " + secondName;
				}
				else
				{
					return nameJuridical;
				}
			}
		}
		public string TitleEx
		{
			get
			{
				if(juridical == false)
				{
					return firstName + " " + name + " " + secondName + " / " + addressRegistration;
				}
				else
				{
					return nameJuridical + " / " + addressJuridical;
				}
			}
		}
		public string AddressTxt
		{
			get
			{
				if(juridical == false)
				{
					return this.addressRegistration;
				}
				else
				{
					return this.addressJuridical;
				}
			}
		}
		public string TitleTxt
		{
			get
			{
				if(juridical == false)
				{
					return firstName + " " + name + " " + secondName;
				}
				else
				{
					return nameJuridical;
				}
			}
		}
		public string ContactTxt
		{
			get
			{
				if(juridical == false)
				{
					string txt;
					string txt1;
					string txt2;
					txt		= name.Trim();
					txt1	= secondName.Trim();
					txt2	= phone.Trim();
					if((txt.Length != 0) && (txt2.Length != 0))
						return txt + " " + txt1 + " / " + txt2;
					else
						if(txt.Length != 0)
							return txt + " " + txt1;
						else
							return txt2;
				}
				else
				{
					string txt;
					string txt1;
					txt		= contact.Trim();
					txt1	= contactPhone.Trim();
					if((txt.Length != 0) && (txt1.Length != 0))
						return txt + " / " + txt1;
					else
						return txt + "" + txt1;
				}
			}
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
				item.SubItems.Add("");
				SetLVItem(item);
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text = nameShort;
			item.SubItems[1].Text = ContactTxt;
			item.SubItems[2].Text = comment;
			item.Tag = this;
		}

		public static void FillList(ListView list, bool juridical, int searchType, string pattern)
		{
			cmdSelect.Parameters["@juridical"].Value = juridical;
			cmdSelect.Parameters["@type"].Value = searchType;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, int function)
		{
			cmdSelectFunction.Parameters["@fucntion"].Value = function;
			Db.DbFillList(list, cmdSelectFunction, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbPartner element = new DbPartner(reader, 0);
			list.Items.Add(element.LVItem);
		}

		public static void FillArray(ArrayList array, bool juridical, int searchType, string pattern)
		{
			cmdSelect.Parameters["@juridical"].Value = juridical;
			cmdSelect.Parameters["@type"].Value = searchType;
			cmdSelect.Parameters["@pattern"].Value = pattern;
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArray(ArrayList array, DbPartner.PartnerFunction function)
		{
			cmdSelectFunction.Parameters["@function"].Value = (int)function;
			Db.FillArray(array, cmdSelectFunction, new DelegateInsertInArray(InsertInArray));
		}

		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbPartner element = new DbPartner(reader, 0);
			array.Add(element);
		}

		#endregion

		#region Основные методы
		public static DbPartner Find(long code)
		{
			SqlDataReader reader = null;
			DbPartner partner = null;
			try
			{

				cmdExecFind.Parameters["@code"].Value = code;
				reader = cmdExecFind.ExecuteReader();
				if(reader.Read())
					partner = new DbPartner(reader, 0);
			}
			catch(Exception E)
			{
				SetException(E);
				if(reader != null) reader.Close();
				return null;
			}
			if(reader != null) reader.Close();
			return partner;
		}

		public bool Write()
		{
			if((adding == false)&&(changed == false)) return true;
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdWrite.Parameters["@adding"].Value				= (bool)adding;
				cmdWrite.Parameters["@code"].Value					= (long)code;
				cmdWrite.Parameters["@codeGroup"].Value				= (long)codeGroup;
				cmdWrite.Parameters["@nameShort"].Value				= (string)nameShort;
				cmdWrite.Parameters["@juridical"].Value				= (bool)juridical;
				// Для физических лиц
				cmdWrite.Parameters["@firstName"].Value				= (string)firstName;
				cmdWrite.Parameters["@name"].Value					= (string)name;
				cmdWrite.Parameters["@secondName"].Value			= (string)secondName;
				cmdWrite.Parameters["@birth"].Value					= (DateTime)birth;
				cmdWrite.Parameters["@addressRegistration"].Value	= (string)addressRegistration;
				cmdWrite.Parameters["@addressLiving"].Value			= (string)addressLiving;
				cmdWrite.Parameters["@phone"].Value					= (string)phone;
				// Для юридических лиц
				cmdWrite.Parameters["@nameJuridical"].Value			= (string)nameJuridical;
				cmdWrite.Parameters["@addressJuridical"].Value		= (string)addressJuridical;
				cmdWrite.Parameters["@addressFact"].Value			= (string)addressFact;
				cmdWrite.Parameters["@inn"].Value					= (string)inn;
				cmdWrite.Parameters["@kpp"].Value					= (string)kpp;
				cmdWrite.Parameters["@contact"].Value				= (string)contact;
				cmdWrite.Parameters["@contactPhone"].Value			= (string)contactPhone;
				cmdWrite.Parameters["@settlementAccount"].Value		= (string)settlementAccount;
				cmdWrite.Parameters["@comment"].Value				= (string)comment;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				if(trans != null) trans.Rollback();
				SetTransaction(null);
				SetException(E);
				return false;
			}
			if(trans != null) trans.Commit();
			SetTransaction(null);
			if(adding == true) MessageBox.Show("Контрагент добавлен");
			else MessageBox.Show("Контрагент изменен");
			return true;
		}

		public bool WriteFucntion(DbPartner.PartnerFunction function, bool add)
		{
			try
			{
				cmdWriteFunction.Parameters["@adding"].Value		= (bool)add;
				cmdWriteFunction.Parameters["@code"].Value			= (long)code;
				cmdWriteFunction.Parameters["@function"].Value		= (int)function;
				cmdWriteFunction.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWriteFunction);
			}
			catch(Exception E)
			{
				SetException(E);
				Db.ShowFaults();
				return false;
			}
			if(add== true) MessageBox.Show("Функция контрагента добавлена");
			else MessageBox.Show("Функция контрагента удалена");
			return true;
		}
		#endregion

		#region Определение виртуальных методов
		public override string[] Inform(int infoLevel)
		{
			string[] infoStrings	= null;

			switch (infoLevel)
			{
				default:
					if(this.juridical == true)
					{
						infoStrings = new string[8];
						infoStrings[0] = "Юридическое лицо :\t\t" + this.NameShort;
						infoStrings[1] = "Юридическое наименование :\t" + this.Title;
						infoStrings[2] = "Юридический адрес :\t\t" + this.AddressJuridical;
						infoStrings[3] = "ИНН \\ КПП :\t\t\t";
						if ((this.Inn.Length > 0)||(this.Kpp.Length > 0)) infoStrings[3] += this.Inn + " \\ " + this.Kpp;
						infoStrings[4] = "Расчетный счет :\t\t\t" + this.settlementAccount;
						infoStrings[5] = "Фактический адрес :\t\t" + this.AddressFact;
						infoStrings[6] = "Контакт :\t\t\t\t" + this.ContactTxt;
						infoStrings[7] = "Примечание :\t\t\t" + this.Comment;

					}
					else
					{
						infoStrings = new string[6];
						infoStrings[0] = "Физическое лицо :\t\t" + this.NameShort;
						infoStrings[1] = "ФИО :\t\t\t" + this.Title;
						infoStrings[2] = "Прописка :\t\t" + this.AddressRegistration;
						infoStrings[3] = "Проживает :\t\t" + this.AddressLiving;
						infoStrings[4] = "Телефон :\t\t" + this.Phone;
						infoStrings[5] = "Примечание :\t\t" + this.Comment;
					}
					break;
			}
			return infoStrings;
		}
		override public string DbTitle()
		{
			return this.Title;
		}
		override public string DbTitleEx()
		{
			return this.TitleEx;
		}
		#endregion

		#region Опасные системные методы
		public bool Delete()
		{
			if(Db.CheckSysPass() == false) return false;
			DialogResult res = MessageBox.Show("Вы уверены что хотите удалить контрагента?", "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysDelete.Parameters["@code"].Value = (long)code;
			return Db.ExecuteCommandError(cmdSysDelete);
		}
		public bool Replace(DbPartner partner)
		{
			if(Db.CheckSysPass() == false) return false;
			if(partner == null) return false;
			string text = "Вы уверены что хотите заменить " + this.Title + " на " + partner.Title + "?";
			DialogResult res = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
			if(res == DialogResult.No) return false;
			cmdSysChange.Parameters["@code"].Value = (long)code;
			cmdSysChange.Parameters["@codeNew"].Value = (long)partner.Code;
			return Db.ExecuteCommandError(cmdSysChange);
		}
		#endregion
	}
}
