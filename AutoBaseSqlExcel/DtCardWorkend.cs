using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardWorkend.
	/// </summary>
	public class DtCardWorkend
	{
		long		card_number;	// ����� ��������
		int			card_year;		// ��� ��������
		DateTime	date;			// ���� � ����� ��������� �������

		public DtCardWorkend(long the_card_number, int the_card_year, DateTime the_date)
		{
			card_number = the_card_number;
			card_year	= the_card_year;
			date		= the_date;
		}

		public DtCardWorkend()
		{
			card_number = 0L;
			card_year	= 0;
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "������_��������_�����":
					return (object)(long)card_number;
				case "������_��������_���":
					return (object)(int)card_year;
				case "����_���������_�������":
					return (object)(DateTime)date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "������_��������_�����":
					card_number = (long)val;
					break;
				case "������_��������_���":
					card_year = (int)val;
					break;
				case "����_���������_�������":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}
	}
}
