using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtFactory.
	/// </summary>
	public class DtFactory
	{
		private long		code;
		private string		name;
		private string		prefix;

		public DtFactory()
		{
			code		= 0;
			name		= "";
			prefix		= "";
		}
		public DtFactory(DtFactory data)
		{
			code		= data.code;
			name		= data.name;
			prefix		= data.prefix;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ_опнхгбндхрекэ":
					return (object)(long)code;
				case "мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ":
					return (object)(string)name;
				case "опетхйя_юбрнлнахкэ_опнхгбндхрекэ":
					return (object)(string)prefix;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ_опнхгбндхрекэ":
					code = (long)val;
					break;
				case "мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ":
					name = (string)val;
					break;
				case "опетхйя_юбрнлнахкэ_опнхгбндхрекэ":
					prefix = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// вРНАШ ЯДЕКЮРЭ НДМНРХОМШЛ ДНАЮБКЕМХЕ Х ХГЛЕМЕМХЕ

			item.Tag				= this.code;
			item.Text				= this.name;
			item.SubItems.Add(this.prefix);
		}
	}
}
