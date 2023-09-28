using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardWorkComment.
	/// </summary>
	public class DtCardWorkComment:Dt
	{
		public long code;
		public string text;

		public DtCardWorkComment(string txt)
		{
			code		= 0;
			text		= txt;
		}

		public DtCardWorkComment()
		{
			code		= 0;
			text		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���":
					return (object)(long)code;
				case "�����":
					return (object)(string)text;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���":
					code = (long)val;
					break;
				case "�����":
					text = (string)val;
					text = text.Trim();
					break;
				default:
					break;
			}
		}

		public override string Title()
		{
			return text;
		}

		public override long Code()
		{
			return code;
		}

		public void SetLVItem(ListViewItem item, bool visible)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������
			item.StateImageIndex	= 0;

			item.Tag				= this.code;
			item.Text				= this.text;

			if(visible == true)
				item.ForeColor = System.Drawing.Color.Black;
			else
				item.ForeColor = System.Drawing.Color.Blue;
		}
	}
}
