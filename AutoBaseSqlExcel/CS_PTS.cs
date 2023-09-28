using System;

namespace AutoBaseSql
{
	/// <summary>
	/// ÏÀÑÏÎĞÒ ÒĞÀÍÑÏÎĞÒÍÎÃÎ ÑĞÅÄÑÒÂÀ (ÏÒÑ).
	/// </summary>
	public class CS_PTS
	{
		public long		code;		// Êîä â áàçå äàííûõ
		public string	series;		// Ñåğèÿ ÏÒÑ
		public string	number;		// Íîìåğ ÏÒÑ

		public CS_PTS()
		{
			
		}

		public DT_Struct SaveStruct()
		{
			DT_Struct datas = new DT_Struct();
			datas.AddLong("ÊÎÄ", "@code", code);
			datas.AddString("ÑÅĞÈß", "@series", series);
			datas.AddString("ÍÎÌÅĞ", "@number", number);
			return datas;
		}

		public void LoadStruct(DT_Struct datas)
		{
			code = datas.FindLong("ÊÎÄ");
			series = datas.FindString("ÑÅĞÈß");
			number = datas.FindString("ÍÎÌÅĞ");
		}
	}
}
