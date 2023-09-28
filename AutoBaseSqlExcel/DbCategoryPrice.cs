using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������� ��������������� ������ �������������
	/// </summary>
	public class DbCategoryPrice:Db
	{
		private long		code;
		private string		name;

		private float		tmpPrice;
		private float		tmpPriceGuaranty;
		private bool		tmpPriceFlag;

		private static SqlConnection	conn;
		private static SqlCommand		cmdWrite;
		private static SqlCommand		cmdWriteMatrix;
		private static SqlCommand		cmdSelect;
		private static SqlCommand		cmdSelectFind;
		private static SqlCommand		cmdSelectMatrix;

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region ������������
		public DbCategoryPrice()
		{
			code		= 0;
			name		= "";

			tmpPrice			= 0.0f;
			tmpPriceGuaranty	= 0.0f;
			tmpPriceFlag		= false;

			adding		= true;
		}
		public DbCategoryPrice(DbCategoryPrice src)
		{
			code		= src.code;
			name		= src.name;

			tmpPrice			= src.tmpPrice;
			tmpPriceGuaranty	= src.tmpPriceGuaranty;
			tmpPriceFlag		= src.tmpPriceFlag;

			adding		= false;
		}
		public DbCategoryPrice(SqlDataReader reader, int offset)
		{
			code		= (long)GetValueLong(reader, offset);		offset++;
			name		= (string)GetValueString(reader, offset);	offset++;

			tmpPrice			= 0.0f;
			tmpPriceGuaranty	= 0.0f;
			tmpPriceFlag		= false;

			adding		= false;
		}
		public DbCategoryPrice(SqlDataReader reader, int offset, int extension)
		{
			code				= (long)GetValueLong(reader, offset);		offset++;
			name				= (string)GetValueString(reader, offset);	offset++;
			if(this.IsValueNull(reader, offset))
				tmpPriceFlag = false;
			else
				tmpPriceFlag = true;
			tmpPrice			= (float)GetValueFloat(reader, offset);		offset++;
			tmpPriceGuaranty	= (float)GetValueFloat(reader, offset);		offset++;

			adding		= false;
		}
		#endregion

		#region �������������
		public static void Init(SqlConnection connection)
		{
			// ������ ����� ����� ������������� ������
			// 2 ����������� ���� � ���������
			readerLength = 2;

			conn = connection;

			cmdWrite = new SqlCommand("WRITE_���������_����", conn);
			cmdWrite.CommandType = CommandType.StoredProcedure;
			cmdWrite.Parameters.Add("@code", SqlDbType.BigInt);
			cmdWrite.Parameters.Add("@name", SqlDbType.VarChar);
			cmdWrite.Parameters.Add("@adding", SqlDbType.Bit);
			Db.SetReturnError(cmdWrite);
			cmdWrite.Parameters["@code"].Direction = ParameterDirection.InputOutput;

			cmdWriteMatrix = new SqlCommand("WRITE_���������_���������_��������", conn);
			cmdWriteMatrix.CommandType = CommandType.StoredProcedure;
			cmdWriteMatrix.Parameters.Add("@codeCategoryCost", SqlDbType.BigInt);
			cmdWriteMatrix.Parameters.Add("@codeCategoryPrice", SqlDbType.BigInt);
			cmdWriteMatrix.Parameters.Add("@price", SqlDbType.Real);
			cmdWriteMatrix.Parameters.Add("@priceGuaranty", SqlDbType.Real);


			cmdSelect = new SqlCommand("SELECT_���������_����", conn);
			cmdSelect.CommandType = CommandType.StoredProcedure;

			cmdSelectFind = new SqlCommand("SELECT_���������_����_�����", conn);
			cmdSelectFind.Parameters.Add("@code", SqlDbType.BigInt);
			cmdSelectFind.CommandType = CommandType.StoredProcedure;

			cmdSelectMatrix = new SqlCommand("SELECT_���������_���������_��������", conn);
			cmdSelectMatrix.Parameters.Add("@codeCategoryCost", SqlDbType.BigInt);
			cmdSelectMatrix.CommandType = CommandType.StoredProcedure;
		}
		#endregion

		#region �����������
		public ListViewItem LVItem
		{
			get
			{	
				ListViewItem item = new ListViewItem();
				switch(viewType)
				{
					case 1:
						item.Text = "";
						item.SubItems.Add("");
						item.SubItems.Add("");
						SetLVItem(item);
						break;
					default:
						item.Text = "";
						SetLVItem(item);
						break;
				}
				return item;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			switch(viewType)
			{
				case 1:
					item.UseItemStyleForSubItems = false;
					item.Text	= name;
					item.SubItems[1].Text	= Db.CachToTxt(tmpPrice);
					item.SubItems[2].Text	= Db.CachToTxt(tmpPriceGuaranty);
					item.SubItems[1].BackColor = Color.White;
					item.SubItems[2].BackColor = Color.White;
					if(tmpPrice == 0.0)
						item.SubItems[1].BackColor = Color.Red;
					if(tmpPriceGuaranty == 0.0)
						item.SubItems[2].BackColor = Color.Red;
					if(tmpPriceFlag == false)
					{
						item.SubItems[1].BackColor = Color.Yellow;
						item.SubItems[2].BackColor = Color.Yellow;
					}
					break;
				default:
					item.Text = name;
					break;
			}
			item.Tag = this;
		}

		public static void FillList(ListView list)
		{
			Db.DbFillList(list, cmdSelect, new DelegateInsertInList(InsertInList));
		}

		public static void FillList(ListView list, DbCategoryCost categoryCost)
		{
			cmdSelectMatrix.Parameters["@codeCategoryCost"].Value = (long)categoryCost.Code;
			Db.DbFillList(list, cmdSelectMatrix, new DelegateInsertInList(InsertInList1));
		}
		public static void InsertInList1(SqlDataReader reader, ListView list)
		{
			DbCategoryPrice element = new DbCategoryPrice(reader, 0, 1);
			element.SetViewType(1);
			list.Items.Add(element.LVItem);
		}

		public static void InsertInList(SqlDataReader reader, ListView list)
		{
			DbCategoryPrice element = new DbCategoryPrice(reader, 0);
			list.Items.Add(element.LVItem);
		}
		public static void FillArray(ArrayList array)
		{
			Db.FillArray(array, cmdSelect, new DelegateInsertInArray(InsertInArray));
		}
		public static void FillArray(ArrayList array, DbCategoryCost categoryCost)
		{
			cmdSelectMatrix.Parameters["@codeCategoryCost"].Value = (long)categoryCost.Code;
			Db.FillArray(array, cmdSelectMatrix, new DelegateInsertInArray(InsertInArray1));
		}
		public static void InsertInArray1(SqlDataReader reader, ArrayList array)
		{
			DbCategoryPrice element = new DbCategoryPrice(reader, 0, 1);
			array.Add(element);
		}
		public static void InsertInArray(SqlDataReader reader, ArrayList array)
		{
			DbCategoryPrice element = new DbCategoryPrice(reader, 0);
			array.Add(element);
		}
		public static DbCategoryPrice Find(long code)
		{
			ArrayList array = new ArrayList();
			cmdSelectFind.Parameters["@code"].Value = (long)code;
			Db.FillArray(array, cmdSelectFind, new DelegateInsertInArray(InsertInArray));
			if(array.Count == 0) return null;
			return (DbCategoryPrice)array[0];
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
				name	= SetStringNotEmptyLength(name, value, 25, "������������ ��������� �����");
			}
		}
		public float Price
		{
			set
			{
				if(value < 0) return;
				if(value == tmpPrice) return;
				tmpPrice = value;
				tmpPriceFlag	= false;
			}
		}
		public float PriceGuaranty
		{
			set
			{
				if(value < 0) return;
				if(value == tmpPriceGuaranty) return;
				tmpPriceGuaranty = value;

				tmpPriceFlag	= false;
			}
		}
		#endregion

		#region ����������� ���������� � �����
		public string PriceTxt
		{
			get
			{
				return Db.CachToTxt(tmpPrice);
			}
		}
		public string PriceGuarantyTxt
		{
			get
			{
				return Db.CachToTxt(tmpPriceGuaranty);
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
		public static DbCategoryPrice Write(DbCategoryPrice src)
		{
			DbCategoryPrice categoryPrice;
			string			text;
			if(src != null)
			{
				categoryPrice	= new DbCategoryPrice(src);
				text			= "�������� ��� ��������� ����� - " + categoryPrice.Name;
			}
			else
			{
				categoryPrice	= new DbCategoryPrice();
				text			= "�������� ����� ��������� �����";
			}
			FormSelectString dialog = new FormSelectString(text, categoryPrice.Name);
			dialog.ShowDialog(null);
			if(dialog.DialogResult != DialogResult.OK) return null;
			categoryPrice.Name		= dialog.SelectedText;
			if(Db.ShowFaults() == true) return null;
			if(categoryPrice.Write() == false) return null;

			return categoryPrice;
		}
		public bool Write(DbCategoryCost categoryCost)
		{
			if(tmpPriceFlag == true) return true;
			if(categoryCost	== null) return true;
			try
			{
				cmdWriteMatrix.Parameters["@codeCategoryCost"].Value	= (long)categoryCost.Code;
				cmdWriteMatrix.Parameters["@codeCategoryPrice"].Value = (long)code;
				cmdWriteMatrix.Parameters["@price"].Value				= (float)tmpPrice;
				cmdWriteMatrix.Parameters["@priceGuaranty"].Value		= (float)tmpPriceGuaranty;
				cmdWriteMatrix.ExecuteNonQuery();
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			tmpPriceFlag	= true;
			return true;
		}
		public bool Write()
		{
			if(adding == false && changed == false) return true;
			try
			{
				cmdWrite.Parameters["@code"].Value			= (long)code;
				cmdWrite.Parameters["@name"].Value			= (string)name;
				cmdWrite.Parameters["@adding"].Value		= (bool)adding;
				cmdWrite.ExecuteNonQuery();
				Db.ThrowReturnError(cmdWrite);
				code	= (long)cmdWrite.Parameters["@code"].Value;
			}
			catch(Exception E)
			{
				SetException(E);
				ShowFaults();
				return false;
			}
			string text;
			if(adding == true)
				text = "��������� ����� ��������� �����";
			else
				text = "��������� ����� ��������";
			MessageBox.Show(text);
			adding		= false;
			changed		= false;
			return true;
		}
		#endregion
	}
}
