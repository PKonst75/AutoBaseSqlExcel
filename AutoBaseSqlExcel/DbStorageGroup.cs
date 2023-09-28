using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbStorageGroup.
	/// </summary>
	public class DbStorageGroup:Db
	{
		private long		code;
		private long		codeParent;
		private string		name;
		private float		charge;

		public static SqlConnection conn;
		public static SqlCommand cmdWrite;
		public static SqlCommand cmdSelectLevel;

		private bool tmpExpand;

		private static int readerLength;			// Количество полей при считывании из базы данных
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		public static void Init(SqlConnection connection)
		{
			// Расчет общей длины использования ридера
			// 4 собственных поля
			readerLength = 4;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_СКЛАД_ГРУППА", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@codeParent", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@charge", SqlDbType.Real);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelectLevel = new SqlCommand("SELECT_СКЛАД_ГРУППА_УРОВЕНЬ", conn);
			cmdSelectLevel.CommandType = CommandType.StoredProcedure;
			cmdSelectLevel.Parameters.Add("@codeParent", SqlDbType.BigInt);
		}

		public DbStorageGroup(long parentCode)
		{
			code		= 0;
			codeParent	= parentCode;
			name		= "";
			charge		= 0.0F;

			adding = true;
		}

		public DbStorageGroup(DbStorageGroup source)
		{
			code			= source.code;
			codeParent		= source.codeParent;
			name			= source.name;
			charge			= source.charge;

			tmpExpand = false;
		}

		public DbStorageGroup(SqlDataReader reader, int offset)
		{
			code			= (long)GetValueLong(reader, offset);		offset++;
			codeParent		= (long)GetValueLong(reader, offset);		offset++;
			name			= (string)GetValueString(reader, offset);	offset++;
			charge			= (float)GetValueFloat(reader, offset);		offset++;
		}

		public bool Write()
		{
			try
			{
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@codeParent"].Value	= (long)codeParent;
				cmdWrite.Parameters["@name"].Value			= (string)name;
				cmdWrite.Parameters["@charge"].Value		= (float)charge;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code		= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				return false;
			}
			MessageBox.Show("Складская группа добавлена/изменена");
			return true;
		}

		public long Code
		{
			get
			{
				return code;
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
				name = this.SetStringNotEmptyLength(name, value, 60, "Наименование группы");
			}
		}

		public string ChargeTxt
		{
			set
			{
				charge = this.SetFloatNotMinus(charge, value, "Наценка");
			}
			get
			{
				return Db.FloatToTxt(charge);
			}
		}
		public float Charge
		{
			get
			{
				return charge;
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
				tmpExpand = value;
			}
		}

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
			
			DbStorageGroup element = (DbStorageGroup)node.Tag;
			cmdSelectLevel.Parameters["@codeParent"].Value = element.code;
			Db.DbFillTree(node.Nodes, cmdSelectLevel, new DelegateInsertInTree(InsertInTree));
		}

		public static void FillTree(TreeNodeCollection nodes)
		{	
			cmdSelectLevel.Parameters["@codeParent"].Value = 0;
			Db.DbFillTree(nodes, cmdSelectLevel, new DelegateInsertInTree(InsertInTree));
		}

		public static void InsertInTree(SqlDataReader reader, TreeNodeCollection tree)
		{
			DbStorageGroup element = new DbStorageGroup(reader, 0);
			tree.Add(element.TVItem);
		}
		#endregion
	}
}
