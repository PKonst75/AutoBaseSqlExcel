using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� �� ��������� �����������.
	/// </summary>
	public class DtAutoReceive
	{
		long				code;
		DateTime			date;
		long				code_receiver;
		string				comment;
		bool				transaction;

		string				tmp_receiver;

		public DtAutoReceive()
		{
			code			= 0;
			date			= DateTime.Now;
			code_receiver	= 0;
			comment			= "";
			transaction		= false;

			tmp_receiver	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_���������_��������":
					return (object)(long)code;
				case "����_��������":
					return (object)(DateTime)date;
				case "����������_��������":
					return (object)(string)comment;
				case "���_�������_����������":
					return (object)(long)code_receiver;
				case "��������_��������":
					return (object)(bool)transaction;
					// ���������
				case "����������":
					return (object)(string)tmp_receiver;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_���������_��������":
					code = (long)val;
					break;
				case "����_��������":
					date = (DateTime)val;
					break;
				case "����������_��������":
					comment = (string)val;
					comment = comment.Trim();
					break;
				case "���_�������_����������":
					code_receiver = (long)val;
					break;
				case "��������_��������":
					transaction = (bool)val;
					break;
					// ���������
				case "����������":
					tmp_receiver = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "����������":
					if(comment.Length == 0) return false;
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
			item.Text				= this.date.ToString();
			item.SubItems.Add(this.comment);
			if(this.transaction == true) item.BackColor = Color.LightGreen;
		}
	}
}
