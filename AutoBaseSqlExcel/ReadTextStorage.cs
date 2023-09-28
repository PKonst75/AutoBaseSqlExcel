using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace AutoBaseSql
{
    public class ReadTextStorage
    {
		const int NUMBER_OF_EMPTY_LINES = 9;
		public struct NomenclatureInFileLine
		{
			public string articul;
			public string name;
			public string unit;
			public float quontity;
			public float cost_price;
			public float price;
			public bool success;
		}

		public ReadTextStorage()
		{

		}

		public static StreamReader GetReader()
		{
            OpenFileDialog dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            dlg.ShowDialog();
			if (dlg.FileName.Length == 0) return null;
			StreamReader reader;
			try
			{
				reader = new StreamReader(dlg.FileName);
			}
			catch (Exception _)
			{
				return null;
			}
			return reader;
		}
		public static void SkipHeader(StreamReader reader)
		{
			// Читаем и пропускаем первые N строк файла
			for (int i = 0; i < NUMBER_OF_EMPTY_LINES; ++i)
			{
                _ = reader.ReadLine();
			}
		}
		
		public static NomenclatureInFileLine GetLine(StreamReader reader)
		{
			var line = new NomenclatureInFileLine
			{
				success = false,
				quontity = 0.0F,
				cost_price = 0.0F,
				price = 0.0F
            };
            string s = reader.ReadLine();
			if (s == null) return line;

			char[] separators = new char[] { '\t' };
			string[] ssplit = s.Split(separators, 5);
			line.articul = ssplit[0];
			if (line.articul == "Итого") return line;
			line.name = ssplit[1];
			line.unit = ssplit[2];

			//MessageBox.Show(ssplit[3]);
			//MessageBox.Show(ssplit[4]);

			if (ssplit[3].Length > 0)
			{
				try
				{
					line.quontity = (long)Convert.ToDecimal(ssplit[3]);
				}
				catch
				{
					MessageBox.Show(ssplit[3]);
				}
			}
			if (ssplit[4].Length > 0)
			{
				try
				{
					line.cost_price = (long)Convert.ToDecimal(ssplit[4]);
				}
				catch
				{
					MessageBox.Show(ssplit[4]);
				}
			}

			if(line.quontity != 0)
            {
				line.cost_price /= line.quontity;
				if (line.cost_price < 300.0f)
				{
					line.price = line.cost_price * 1.9f;
				}
				else if (line.cost_price < 28000.0f)
				{
					line.price = line.cost_price * 1.55f;
				}
				else
				{
					line.price = line.cost_price * 1.45f;
				}
            }
			line.price = (float)(Math.Round((Math.Round(line.cost_price * 1.9f) / 10)) * 10);
			line.success = true;
			return line;
		}
		public static void ReadFile()
		{
			StreamReader reader = GetReader();
			if (reader == null)
			{
				MessageBox.Show(null, "Не удалось загрузить", "Предупреждение");
				return;
			}
			SkipHeader(reader);
			NomenclatureInFileLine line;
			while ((line = GetLine(reader)).success)
			{
				if (line.articul != "")
				{
					// Запуск записи в базу
					if (DbDetailStorage.Write(line) == false)
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
