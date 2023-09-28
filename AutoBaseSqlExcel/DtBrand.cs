using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������. ��� �������� ����������. �������������.
	/// </summary>
	public class DtBrand
	{

		public enum DIALER : long { unknown = 0, chevrolet = 1, lada = 2, kia = 3 }

		long		code;			// ���������� ��� ������ � ��
		string		name;			// ������������ ������

		public DtBrand()
		{
			code		= 0;
			name		= "";
		}

		public long Code
        {
            get { return code; }
        }
		public DIALER DialerOfficial
        {
            get
            {
                switch (name)
                {
					case "�������":
						return DtBrand.DIALER.chevrolet;
					case "LADA":
						return DtBrand.DIALER.lada;
					default:
						return DtBrand.DIALER.unknown;
				}
			}
        }

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_�����":
					return (object)(long)code;
				case "������������_����������_�����":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_�����":
					code = (long)val;
					break;
				case "������������_����������_�����":
					name = (string)val;
					name.Trim();
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "������������_����������_�����":
					if(name.Length == 0) return false;
					break;
				case "���_����������_�����":
					if(code <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.name;
		}

		public void SetTNode(TreeNode node)
		{
			node.Text = "";			// ����� ������� ���������� ���������� � ���������

			node.Tag				= this.code;
			node.Text				= this.name;
		}
	}
}
