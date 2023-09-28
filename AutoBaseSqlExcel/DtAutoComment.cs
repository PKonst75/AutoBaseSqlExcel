using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoComment.
	/// </summary>
	public class DtAutoComment
	{
		long		code_auto;
		long		number;
		string		comment;
		DateTime	date_exe;
		bool		need_exe;
		long		person_exe;
		string		comment_exe;

		string		tmp_person_exe_txt;

		public DtAutoComment()
		{
			code_auto	= 0L;
			number		= 0L;
			comment		= "";
			date_exe	= DateTime.Now;
			need_exe	= false;
			person_exe	= 0L;
			comment_exe	= "";

			tmp_person_exe_txt	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������":
					return (object)(long)code_auto;
				case "�����":
					return (object)(long)number;
				case "����������":
					return (object)(string)comment;
				case "����_����������":
					return (object)(DateTime)date_exe;
				case "��������":
					return (object)(long)person_exe;
				case "���������_����������":
					return (object)(bool)need_exe;
				case "����������_����������":
					return (object)(string)comment_exe;

				case "��������_���":
					return (object)(string)tmp_person_exe_txt;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������":
					code_auto = (long)val;
					break;
				case "�����":
					number = (long)val;
					break;
				case "����������":
					comment = (string)val;
					break;
				case "����_����������":
					date_exe = (DateTime)val;
					break;
				case "��������":
					person_exe = (long)val;
					break;
				case "���������_����������":
					need_exe = (bool)val;
					break;
				case "����������_����������":
					comment_exe = (string)val;
					break;

				case "��������_���":
					tmp_person_exe_txt = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			Db.LongPair pair = new Db.LongPair();
			pair.data_main			= this.code_auto;
			pair.data_add			= this.number;
			item.Tag				= pair;
			item.Text				= this.comment;

			if(this.person_exe > 0)
			{
				// ����������� ����������
				item.SubItems.Add(this.date_exe.ToShortDateString());
				item.SubItems.Add(this.tmp_person_exe_txt);
				item.SubItems.Add(this.comment_exe);
				item.BackColor = Color.Green;
				return;
			}
			if(this.need_exe == true)
			{
				// ����������� ����������
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.SubItems.Add("");
				item.BackColor = Color.Red;
				return;
			}
			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");
		}

	}
}
