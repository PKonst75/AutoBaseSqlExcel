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
				case "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ":
					return (object)(long)code_auto;
				case "ÍÎÌÅĞ":
					return (object)(long)number;
				case "ÏĞÈÌÅ×ÀÍÈÅ":
					return (object)(string)comment;
				case "ÄÀÒÀ_ÂÛÏÎËÍÅÍÈÅ":
					return (object)(DateTime)date_exe;
				case "ÂÛÏÎËÍÈË":
					return (object)(long)person_exe;
				case "ÒĞÅÁÓÅÒÑß_ÂÛÏÎËÍÅÍÈÅ":
					return (object)(bool)need_exe;
				case "ÏĞÈÌÅ×ÀÍÈÅ_ÂÛÏÎËÍÅÍÈÅ":
					return (object)(string)comment_exe;

				case "ÂÛÏÎËÍÈË_ÈÌß":
					return (object)(string)tmp_person_exe_txt;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ":
					code_auto = (long)val;
					break;
				case "ÍÎÌÅĞ":
					number = (long)val;
					break;
				case "ÏĞÈÌÅ×ÀÍÈÅ":
					comment = (string)val;
					break;
				case "ÄÀÒÀ_ÂÛÏÎËÍÅÍÈÅ":
					date_exe = (DateTime)val;
					break;
				case "ÂÛÏÎËÍÈË":
					person_exe = (long)val;
					break;
				case "ÒĞÅÁÓÅÒÑß_ÂÛÏÎËÍÅÍÈÅ":
					need_exe = (bool)val;
					break;
				case "ÏĞÈÌÅ×ÀÍÈÅ_ÂÛÏÎËÍÅÍÈÅ":
					comment_exe = (string)val;
					break;

				case "ÂÛÏÎËÍÈË_ÈÌß":
					tmp_person_exe_txt = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			Db.LongPair pair = new Db.LongPair();
			pair.data_main			= this.code_auto;
			pair.data_add			= this.number;
			item.Tag				= pair;
			item.Text				= this.comment;

			if(this.person_exe > 0)
			{
				// Âûïîëíåííîå ïğèìå÷àíèå
				item.SubItems.Add(this.date_exe.ToShortDateString());
				item.SubItems.Add(this.tmp_person_exe_txt);
				item.SubItems.Add(this.comment_exe);
				item.BackColor = Color.Green;
				return;
			}
			if(this.need_exe == true)
			{
				// Âûïîëíåííîå ïğèìå÷àíèå
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
