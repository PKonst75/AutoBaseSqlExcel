using System;

namespace AutoBaseSql
{
	/// <summary>
	/// ������� ������������� �������� (���).
	/// </summary>
	public class CS_PTS
	{
		public long		code;		// ��� � ���� ������
		public string	series;		// ����� ���
		public string	number;		// ����� ���

		public CS_PTS()
		{
			
		}

		public DT_Struct SaveStruct()
		{
			DT_Struct datas = new DT_Struct();
			datas.AddLong("���", "@code", code);
			datas.AddString("�����", "@series", series);
			datas.AddString("�����", "@number", number);
			return datas;
		}

		public void LoadStruct(DT_Struct datas)
		{
			code = datas.FindLong("���");
			series = datas.FindString("�����");
			number = datas.FindString("�����");
		}
	}
}
