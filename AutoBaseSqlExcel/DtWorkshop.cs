using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtWorkshop.
	/// </summary>
	public class DtWorkshop:Dt
	{
		long		code;
		string		name;
		string		description;
		string		pass_destination;

		public DtWorkshop()
		{
			code				= 0;
			name				= "";
			description			= "";
			pass_destination	= "";
		}

		public string Name
        {
            get { return name; }
        }

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_���":
					return (object)(long)code;
				case "������������_���":
					return (object)(string)name;
				case "����������_���":
					return (object)(string)description;
				case "�������_����������":
					return (object)(string)pass_destination;
				default:
					return (object)null;
			}
		}
		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_���":
					code = (long)val;
					break;
				case "������������_���":
					name = (string)val;
					name = name.Trim();
					break;
				case "����������_���":
					description = (string)val;
					break;
				case "�������_����������":
					pass_destination = (string)val;
					break;
				default:
					break;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.name;
		}
		public string Txt()
		{
			return name;
		}

		public override long Code()
		{
			return code;
		}
		public override string Title()
		{
			return name;
		}

		public long CodeWorkshop
		{
			get { return code; }
		}
	}
}
