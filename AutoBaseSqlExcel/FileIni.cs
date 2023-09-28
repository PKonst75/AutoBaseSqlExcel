using System;
using System.IO;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FileIni.
	/// </summary>
	public class FileIni
	{
		public FileIni()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string GetParameter(string file, string name)
		{
			// Чтение значение параметра из инициализационного файла
			StreamReader reader = null;
			string result		= "";
			bool block			= false;
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// Открываем файл для чтения
			
			
			string s = reader.ReadLine();		// Читаем из файла строку
			while(s != null && block == false)
			{
				// Обрабатываем строку, удаляя пробелы и перенос
				s = s.Trim();
				if(s == name)
					block = true;	
				if(block == true)
				{
					// Необходимо зачитать из файла блок печати и распечатать его
					// Читаем блок
					string str = reader.ReadLine();
					while (str != null && str != "#END")
					{
						if(result == "")
							result = str;
						else
							result += "\n" + str;
						str = reader.ReadLine();
					}
				}
				s = reader.ReadLine();
			}
			reader.Close();						// Закрываем файл
			return result;
		}
	}
}
