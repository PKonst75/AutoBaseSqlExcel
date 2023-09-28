using System;
using System.Windows.Forms;
using System.IO;

namespace AutoBaseSql
{
	/// <summary>
	/// Читаем прайс из текстового файла, записываем/изменяем данные.
	/// </summary>
	public class TxtReadPrice
	{

		public struct FileLine
		{
			public long		nomenclature_code;
			public string	nomenclature_name;
			public string	nomenclature_number;
			public string	nomenclature_unit;
			public float	nomenclature_quontity;
			public float	nomenclature_price;
			public float	nomenclature_input;
		}
		
		public TxtReadPrice()
		{
			
		}

		public static StreamReader GetReader()
		{
			StreamReader reader = null;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect		= false;
			dlg.Filter = "Текстовые файлы (*.txt)|*.txt";
			dlg.ShowDialog();
			if(dlg.FileName.Length == 0) return null;
			try
			{
				reader = new StreamReader(dlg.FileName);
			}
			catch(Exception e)
			{
				return null;
			}
			if(reader == null) return null;
			return reader;
		}
		public static void SkipHeader(StreamReader reader)
		{
			// Читаем и пропускаем первые N строк файла
			string s;
			for(int i = 0; i < 4; i++)
			{
				s = reader.ReadLine();
			}
		}
		public static FileLine GetLine0(StreamReader reader)
		{
			// Читаем и пропускаем первые N строк файла
			string s;
			FileLine line = new FileLine();
			line.nomenclature_code	= -1;

			s = reader.ReadLine();
			if(s == null) return line;
			
			char[] separators = new Char[] {'\t'};
			string[] ssplit = s.Split(separators, 9);
			line.nomenclature_name		= ssplit[1];
            if (ssplit[2].Length > 0)
            {
                try
                {
                    line.nomenclature_code = (long)Convert.ToInt64(ssplit[2]);
                }
                catch
                {
                    line.nomenclature_code = 0;
                }
            }
            else
                line.nomenclature_code = 0;
			line.nomenclature_number	= ssplit[3];
			line.nomenclature_unit		= ssplit[4];
			if(ssplit[5].Length > 0)
				line.nomenclature_quontity	= (float)Convert.ToDecimal(ssplit[5]);
			else
				line.nomenclature_quontity	= 0.0F;
			if(ssplit[6].Length > 0)
			{
				//ssplit[6].Replace("руб.", "");
				//ssplit[6].Replace(",", ".");
				string[] ssplit2 = ssplit[6].Split(new Char[] {' '}, 2);
				try
				{
					line.nomenclature_price		= (float)Convert.ToDecimal(ssplit2[0]);
				}
				catch(Exception e)
				{
					line.nomenclature_price		= 0.0F;
				}
			}
			else
				line.nomenclature_price		= 0.0F;
			if(ssplit[7].Length > 0)
				line.nomenclature_quontity	= (float)Convert.ToDecimal(ssplit[7]);
			else
				line.nomenclature_quontity	= 0.0F;
			return line;
		}
		public static FileLine GetLine(StreamReader reader)
		{
			// Читаем и пропускаем первые N строк файла
			string s;
			FileLine line = new FileLine();
			line.nomenclature_code	= -1;

			s = reader.ReadLine();
			if(s == null) return line;
			
			char[] separators = new Char[] {'\t'};
			string[] ssplit = s.Split(separators, 9);
            if (ssplit[1].Length > 0)
            {
                try
                {
                    line.nomenclature_code = (long)Convert.ToInt64(ssplit[1]);
                }
                catch
                {
                    MessageBox.Show(ssplit[1]);
                    line.nomenclature_code = 0;
                }
            }
            else
                line.nomenclature_code = 0;
			line.nomenclature_name		= ssplit[2];
			line.nomenclature_number	= ssplit[3];
			line.nomenclature_unit		= ssplit[4];
			if(ssplit[5].Length > 0)
				line.nomenclature_quontity	= (float)Convert.ToDecimal(ssplit[5]);
			else
				line.nomenclature_quontity	= 0.0F;
			if(ssplit[6].Length > 0)
			{
				//ssplit[6].Replace("руб.", "");
				//ssplit[6].Replace(",", ".");
				string[] ssplit2 = ssplit[6].Split(new Char[] {' '}, 2);
				try
				{
					line.nomenclature_price		= (float)Convert.ToDecimal(ssplit2[0]);
				}
				catch(Exception e)
				{
					line.nomenclature_price		= 0.0F;
				}
			}
			else
				line.nomenclature_price		= 0.0F;
			if(ssplit[7].Length > 0)
				line.nomenclature_input	= (float)Convert.ToDecimal(ssplit[7]);
			else
				line.nomenclature_input	= 0.0F;
			return line;
		}
		public static void ReadFile()
		{
			StreamReader reader = null;
			reader	= GetReader();
			if(reader == null)
			{
				MessageBox.Show(null, "Не удалось загрузить", "Предупреждение");
				return;
			}
			SkipHeader(reader);
			FileLine line;
			line.nomenclature_code = 0;
			while(line.nomenclature_code != -1)
			{
				line = GetLine(reader);
				if(line.nomenclature_code > 0)
				{
					// Запуск записи в базу
					if(DbDetailStorage.Write(line) == false)
					{
						reader.Close();
						MessageBox.Show(null, "Не удалось загрузить!", "Предупреждения");
						Db.ShowFaults();
						return;
					}
				}
			}
			reader.Close();
			MessageBox.Show(null, "Загрузили!!!", "ОК");
			return;
		}
	}
}
