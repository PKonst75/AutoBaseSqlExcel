using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ����� ����������� ������� �������� ������.
	/// </summary>
	public class DtCatalogueParts
	{
		long	code;					// ���������� ��� � ��������
		long	code_group;				// ��� ������ ������� ����������� ��������
		bool	flag_group;				// ���� ����, ��� ������ ������� �������� �������
		string	name;					// ������������ �������� / ������
		
		public DtCatalogueParts()
		{
			code		= 0;
			code_group	= 0;
			flag_group	= false;
			name		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_�������_������":
					return (object)(long)code;
				case "���_������":
					return (object)(long)code_group;
				case "����_������":
					return (object)(bool)flag_group;
				case "������������":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_�������_������":
					code = (long)val;
					break;
				case "���_������":
					code_group = (long)val;
					break;
				case "����_������":
					flag_group = (bool)val;
					break;
				case "������������":
					name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetTNode(TreeNode node)
		{
			node.Text	= "";		// ����� ������� ���������� ���������� � ���������

			node.Tag				= this.code;
			node.Text				= this.name;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text	= "";		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.name;
		}
	}
}
