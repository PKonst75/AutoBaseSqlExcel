using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������� �������, ������ ��������.
	/// </summary>
	public class DtStorageDetail
	{
		private bool _liquidFlag; // ���� �����

		long		code;
		string		name;
		string		detail_code;
		float		quontity;
		float		reserve;
		float		price;
		float		input;
		string		unit;
		string		description;
		long		code_1c;

		float		tmp_balance;
		float		tmp_expence;

		float		new_quontity;
		
		public DtStorageDetail()
		{
			code			= 0;
			name			= "";
			detail_code		= "";
			quontity		= 0.0F;
			reserve			= 0.0F;
			price			= 0.0F;
			input			= 0.0F;
			unit			= "";
			description		= "";
			code_1c			= 0;

			tmp_balance		= 0.0F;
			tmp_expence		= 0.0F;

			new_quontity	= 0.0F;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_�����_������":
					return (object)(long)code;
				case "������������_�����_������":
					return (object)(string)name;
				case "�����_�����_������":
					return (object)(string)detail_code;
				case "����������_�����_������":
					return (object)(float)quontity;
				case "����_�����_������":
					return (object)(float)price;
				case "����_�����_������":
					return (object)(float)input;
				case "�������_���������":
					return (object)(string)unit;
				case "��������":
					return (object)(string)description;
				case "���_1�_�����_������":
					return (object)(long)code_1c;
				case "������":
					return (object)(float)tmp_expence;
				case "�������":
					return (object)(float)tmp_balance;
				case "����������":
					return (object)(float)new_quontity;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_�����_������":
					code = (long)val;
					break;
				case "������������_�����_������":
					name = (string)val;
					name.Trim();
					break;
				case "�����_�����_������":
					detail_code = (string)val;
					detail_code.Trim();
					break;
				case "����������_�����_������":
					quontity = (float)val;
					break;
				case "����_�����_������":
					price = (float)val;
					break;
				case "����_�����_������":
					input = (float)val;
					break;
				case "�������_���������":
					unit = (string)val;
					unit.Trim();
					break;
				case "��������":
					description = (string)val;
					description.Trim();
					break;
				case "���_1�_�����_������":
					code_1c = (long)val;
					break;
				case "������":
					tmp_expence = (float)val;
					break;
				case "�������":
					tmp_balance = (float)val;
					break;
				case "����������":
					new_quontity = (float)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItemBalance(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.detail_code;
			item.SubItems.Add(this.name);
			item.SubItems.Add(this.quontity.ToString() + " / " + this.new_quontity.ToString());
			item.SubItems.Add(this.tmp_expence.ToString());
			item.SubItems.Add(this.tmp_balance.ToString());
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.detail_code;
			item.SubItems.Add(this.name);
			item.SubItems.Add(this.quontity.ToString() + " / " + this.new_quontity.ToString());
			item.SubItems.Add(this.unit);
			item.SubItems.Add(Db.CachToTxt(this.price));
			item.SubItems.Add(this.description);
			// ��������� �� 1�
			if(this.code_1c > 0)
				item.BackColor	= Color.LightGreen;
		}

        #region ������� � ������
		public string Name
        {
			get { return name; }
		}
		public string Unit
		{
			get { return unit; }
		}
		public string CatalogueNumber
		{
			get { return detail_code; }
		}
		public bool Liquid
        {
            get { return _liquidFlag; }
			set { _liquidFlag = value; }
        }
		#endregion
	}
}
