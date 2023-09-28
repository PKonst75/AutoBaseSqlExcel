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
			// ������ �������� ��������� �� ������������������ �����
			StreamReader reader = null;
			string result		= "";
			bool block			= false;
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// ��������� ���� ��� ������
			
			
			string s = reader.ReadLine();		// ������ �� ����� ������
			while(s != null && block == false)
			{
				// ������������ ������, ������ ������� � �������
				s = s.Trim();
				if(s == name)
					block = true;	
				if(block == true)
				{
					// ���������� �������� �� ����� ���� ������ � ����������� ���
					// ������ ����
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
			reader.Close();						// ��������� ����
			return result;
		}
	}
}
