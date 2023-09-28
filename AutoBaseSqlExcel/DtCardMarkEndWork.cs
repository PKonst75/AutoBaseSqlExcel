using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardMarkEndWork.
	/// </summary>
	public class DtCardMarkEndWork
	{
		long		card_number;	// ����� ��������
		int			card_year;		// ��� ��������
		DateTime	date;			// ���� � ����� ��������� �������

		public DtCardMarkEndWork(long the_card_number, int the_card_year)
		{
			card_number = the_card_number;
			card_year	= the_card_year;
			date		= DateTime.Now;
		}

		public DtCardMarkEndWork()
		{
			card_number = 0L;
			card_year	= 0;
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "�����_��������":
					return (object)(long)card_number;
				case "���_��������":
					return (object)(int)card_year;
				case "����":
					return (object)(DateTime)date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "�����_��������":
					card_number = (long)val;
					break;
				case "���_��������":
					card_year = (int)val;
					break;
				case "����":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}
	}
}
