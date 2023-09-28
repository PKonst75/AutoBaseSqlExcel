using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtWorkCollectionItem.
	/// </summary>
	public class DtWorkCollectionItem
	{
		long	code;					// ���������� ��� �������� � ������
		long	code_collection;		// ���������� ��� ������
		int		number;					// ���������� �����
		int		number_group;			// ������� �������������� � ������ � ���������� �������
		string	name;					// ������������ ��������
		float	time;					// ������������ �������� (����� �������)

		public DtWorkCollectionItem()
		{
			code			= 0;
			code_collection	= 0;
			number			= 0;
			number_group	= 0;
			name			= "";
			time			= 0.0F;
		}

		public DtWorkCollectionItem(long collection_code, string collection_name)
		{
			code			= 0;
			code_collection	= collection_code;
			name			= collection_name;
			time			= 0.0F;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_���������_�������":
					return (object)(long)code;
				case "������_���_���������":
					return (object)(long)code_collection;
				case "�����_���������_�������":
					return (object)(int)number;
				case "�����_������_���������_�������":
					return (object)(int)number_group;
				case "������������_���������_�������":
					return (object)(string)name;
				case "������������_���������_�������":
					return (object)(float)time;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_���������_�������":
					code = (long)val;
					break;
				case "������_���_���������":
					code_collection = (long)val;
					break;
				case "�����_���������_�������":
					number = (int)val;
					break;
				case "�����_������_���������_�������":
					number_group = (int)val;
					break;
				case "������������_���������_�������":
					name = (string)val;
					name = name.Trim();
					break;
				case "������������_���������_�������":
					time = (float)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt = "";
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag					= this.code;
			if(this.number != 0)
				item.Text				= this.number.ToString();
			else
				item.Text				= "";
			if(this.number_group != 0)
				item.SubItems.Add(this.number_group.ToString());
			else
				item.SubItems.Add("");

			item.SubItems.Add(this.name);
			if(time != 0.0F)
				txt = time.ToString();
			else
				txt	= "";
			item.SubItems.Add(txt);
		}
	}
}
