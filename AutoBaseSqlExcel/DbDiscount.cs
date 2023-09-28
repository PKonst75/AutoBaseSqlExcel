using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ ������
	/// </summary>
	public class DbDiscount:Db
	{
		private long		code;
		private string		name;
		private float		discount;

		private static SqlConnection	conn;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdSelect;
		private static SqlCommand		cmdSelectFind;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbDiscount()
		{
			code		= 0;
			name		= "";
			discount	= 0.0f;

			adding		= true;
		}
		public DbDiscount(DbDiscount src)
		{
			code		= src.code;
			name		= src.name;
			discount	= src.discount;

			adding		= false;
		}
		public DbDiscount(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			name		= (string)GetValueString(reader, offset);	offset++;
			discount	= (float)GetValueFloat(reader, offset);		offset++;

			adding		= false;
		}
		#endregion

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 3 ����������� ���� � ���������
			readerLength = 3;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_������", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@discount", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdSelect = new SqlCommand("SELECT_������", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectFind = new SqlCommand("SELECT_������_�����", conn);
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region �����������
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
			item.Text				= name;
			item.SubItems[1].Text	= DiscountTxt;
			item.Tag				= this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbDiscount element = new DbDiscount(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbDiscount element = new DbDiscount(reader, 0);
			array.Add(element);
		}
		public static DbDiscount Find(long code)
		{
			ArrayList array = new ArrayList();
			cmdSelectFind.Parameters["@code"].Value = (long)code;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
			if(array.Count == 0) return null;
			return (DbDiscount)array[0];
		}
		#endregion

		#region ������ � �������� ���������� - ������ ������
		public long Code
		{
			get
			{
				return code;
			}
		}
		#endregion

		#region ������ � �������� ����������
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = SetStringNotEmptyLength(name, value, 50, "������������ ������� ���������");
			}
		}
		public float Discount
		{
			get
			{
				return discount;
			}
			set
			{
				if(value < 0.0f) return;
				if(value == discount) return;
				discount	= value;
				changed		= true;
			}
		}
		#endregion

		#region ���������������� ����������� ������
		override public string DbTitle()
		{
			return this.Name;
		}
		#endregion

		#region �������� ������
		public bool Write()
		{
			if(adding == false && changed == false) return true;
			try
			{
				cmdWrite.Parameters["@code"].Value		= (long)code;
				cmdWrite.Parameters["@name"].Value		= (string)name;
				cmdWrite.Parameters["@discount"].Value	= (float)discount;
				cmdWrite.Parameters["@adding"].Value	= (bool)adding;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code		= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			string text;
			if(adding == true)
				text	= "��������� ������ ���������";
			else
				text	= "��������� ������ ��������";
			MessageBox.Show(text);
			adding		= false;
			changed		= false;
			return true;
		}
		#endregion

		#region ����������� �������� ���������� � �����
		public string DiscountTxt
		{
			get
			{
				return discount.ToString();
			}
		}
		#endregion
	}
}
