using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// дНЙСЛЕМР МЮ ОНКСВЕМХЕ ЮБРНЛНАХКЕИ.
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
				case "йнд_юбрнлнахкэ_онксвемхе_днйслемр":
					return (object)(long)code;
				case "дюрю_днйслемр":
					return (object)(DateTime)date;
				case "опхлевюмхе_днйслемр":
					return (object)(string)comment;
				case "йнд_онксвхк_юбрнлнахкх":
					return (object)(long)code_receiver;
				case "опнбедем_днйслемр":
					return (object)(bool)transaction;
					// бПЕЛЕММШЕ
				case "онксвюрекэ":
					return (object)(string)tmp_receiver;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ_онксвемхе_днйслемр":
					code = (long)val;
					break;
				case "дюрю_днйслемр":
					date = (DateTime)val;
					break;
				case "опхлевюмхе_днйслемр":
					comment = (string)val;
					comment = comment.Trim();
					break;
				case "йнд_онксвхк_юбрнлнахкх":
					code_receiver = (long)val;
					break;
				case "опнбедем_днйслемр":
					transaction = (bool)val;
					break;
					// бПЕЛЕММШЕ
				case "онксвюрекэ":
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
				case "опхлевюмхе":
					if(comment.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// вРНАШ ЯДЕКЮРЭ НДМНРХОМШЛ ДНАЮБКЕМХЕ Х ХГЛЕМЕМХЕ

			item.Tag				= this.code;
			item.Text				= this.date.ToString();
			item.SubItems.Add(this.comment);
			if(this.transaction == true) item.BackColor = Color.LightGreen;
		}
	}
}
