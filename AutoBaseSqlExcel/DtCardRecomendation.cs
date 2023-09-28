using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardRecomendation.
	/// </summary>
	public class DtCardRecomendation
	{

		long		card_number;		// ����� ��������
		int			card_year;			// ��� ��������
		long		code;				// ���������� ����� ������������
		string		recomendation;		// ����� ������������

		public DtCardRecomendation()
		{
			card_number			= 0;
			card_year			= 0;
			code				= 0;
			recomendation		= "";
		}
		public object GetData(string data)
		{
			switch(data)
			{
				case "������_�����_��������":
					return (object)(long)card_number;
				case "������_���_��������":
					return (object)(int)card_year;
				case "�����_������������":
					return (object)(long)code;
				case "������������":
					return (object)(string)recomendation;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "������_�����_��������":
					card_number = (long)val;
					break;
				case "������_���_��������":
					card_year = (int)val;
					break;
				case "�����_������������":
					code = (long)val;
					break;
				case "������������":
					recomendation = (string)val;
					break;
				default:
					break;
			}
		}

		public string RecomendationTxt
        {
            get { return recomendation; }
        }
	}
}
