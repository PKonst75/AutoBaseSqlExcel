using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbWorkGroup.
	/// </summary>
	public class DbWorkGroup:Db
	{
		private long		code;
		private long		code_parent;
		private string		name;
		private bool		flag_element;

		private bool		tmpExpand;

		private static SqlConnection	conn;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdDelete;
		private static SqlCommand		cmdSelectLevel;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region Инициализация
		public void SetTransaction(SqlTransaction trans)
		{
			cmdWrite.Transaction	= trans;
			cmdDelete.Transaction	= trans;
		}
		public static void Init(SqlConnection connection)
		{
			readerLength			= 2;	// Два собственных элемента
			conn					= connection;

			cmdWrite				= new SqlCommand("WRITE_РУБРИКА_ТРУДОЕМКОСТЬ", conn);
			cmdWrite.CommandType	= CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@code_parent", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@flag_element", SqlDbType.Bit);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			cmdWrite.Parameters["@code"].Direction	= ParameterDirection.InputOutput;
			SetReturnError(cmdWrite);

			cmdDelete				= new SqlCommand("SYSTEM_РУБРИКА_ТРУДОЕМКОСТЬ_УДАЛИТЬ", conn);
			cmdDelete.CommandType	= CommandType.StoredProcedure;
			cmdDelete.Parameters.Add("@code", SqlDbType.BigInt);
			SetReturnError(cmdDelete);

			cmdSelectLevel = new SqlCommand("SELECT_РУБРИКА_ТРУДОЕМКОСТЬ_УРОВЕНЬ", conn);
			cmdSelectLevel.CommandType = CommandType.StoredProcedure;
			cmdSelectLevel.Parameters.Add("@code_parent", SqlDbType.BigInt);
		}
		#endregion

		#region Конструкторы
		public DbWorkGroup()
		{
			code			= 0;
			code_parent		= 0;
			name			= "";
			flag_element	= false;

			adding			= true;

			tmpExpand		= false;
		}
		public DbWorkGroup(DbWorkGroup src)
		{
			code			= src.code;
			code_parent		= src.code_parent;
			name			= src.name;
			flag_element	= src.flag_element;

			adding			= false;

			tmpExpand		= false;
		}
		public DbWorkGroup(long code_parent_src)
		{
			code			= 0;
			code_parent		= code_parent_src;
			name			= "";
			flag_element	= false;

			adding			= true;

			tmpExpand		= false;
		}
		public DbWorkGroup(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);		offset++;
			code_parent		= (long)GetValueLong(reader, offset);		offset++;
			name			= (string)GetValueString(reader, offset);	offset++;
			flag_element	= (bool)GetValueBool(reader, offset);		offset++;;

			adding			= false;

			tmpExpand		= false;
		}
		#endregion

		#region Основные методы
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
				cmdWrite.Parameters["@code_parent"].Value			= (long)code_parent;
				cmdWrite.Parameters["@name"].Value					= (string)name;
				cmdWrite.Parameters["@flag_element"].Value			= (bool)flag_element;
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
			if(adding == true) MessageBox.Show("Элемент каталога добавлен");
			else MessageBox.Show("Элемент каталога изменен");
			return true;
		}
		public bool Delete()
		{
			SqlTransaction trans = null;
			try
			{
				trans = conn.BeginTransaction();
				SetTransaction(trans);

				cmdDelete.Parameters["@code"].Value					= (long)code;
				cmdDelete.ExecuteNonQuery();
				Db.ThrowReturnError(cmdDelete);
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
			return true;
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

		#region Доступ к основным параметрам - Изменение
		public long CodeParent
		{
			get
			{
				return code_parent;
			}
			set
			{
				if(code_parent == value) return;
				code_parent = value;
				changed		= true;
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
				string txt = Db.FirstUpper(value);
				name = SetStringNotEmptyLength(name, txt, 128, "Наименование элемента каталога");
			}
		}
		public bool FlagElement
		{
			get
			{
				return flag_element;
			}
			set
			{
				if(adding == false) return;
				flag_element = value;
			}
		}
		public bool Expand
		{
			get
			{
				return tmpExpand;
			}
			set
			{
				tmpExpand	= value;
			}
		}
		#endregion

		#region Отображение
		public TreeNode TVItem
		{
			get
			{
				TreeNode item = new TreeNode();
				SetTVItem(item);
				return item;
			}
		}

		public void SetTVItem(TreeNode item)
		{
			item.ImageIndex			= 0;
			item.SelectedImageIndex	= 0;
			if(this.flag_element == true)
			{
				item.ImageIndex			= 1;
				item.SelectedImageIndex	= 1;
			}
			item.Text = Name;
			item.Tag = this;
		}

		public static void FillTree(TreeView tree)
		{
			FillTree(tree.Nodes);			
			foreach(TreeNode node in tree.Nodes)
			{
				FillTree(node);
			}
		}

		public static void FillTree(TreeNode node)
		{
			
			DbWorkGroup element = (DbWorkGroup)node.Tag;
			cmdSelectLevel.Parameters["@code_parent"].Value = element.code;
			Db.DbFillTree(node.Nodes, cmdSelectLevel, new DelegateInsertInTree(InsertInTree));
		}

		public static void FillTree(TreeNodeCollection nodes)
		{	
			cmdSelectLevel.Parameters["@code_parent"].Value = 0;
			Db.DbFillTree(nodes, cmdSelectLevel, new DelegateInsertInTree(InsertInTree));
		}

		public static void InsertInTree(SqlDataReader reader, TreeNodeCollection tree)
		{
			DbWorkGroup element = new DbWorkGroup(reader, 0);
			tree.Add(element.TVItem);
		}
		#endregion
	}
}
