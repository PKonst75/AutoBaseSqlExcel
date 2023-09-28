using System;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������������� ���������� � ������� ����������.
	/// </summary>
	public class CS_SellInfo
	{
		public long code_sell;				// ��� �������
		public long code_position;			// ��� �������������� ����������
		public long code_reklama;			// ��� ���������� ���������
		public bool flag_credit_inner;		// ���� ����������� �������
		public bool flag_credit_outer;		// ���� �������� �������
		public bool flag_lising;			// ���� ������� � ������
		public bool flag_cashless;			// ���� ����������� �������
		public bool flag_partner;			// ���� ������� ����� ���������
		public bool flag_util;				// ���� ����������

        public bool flag_tin;				// ���� trade-in
        public long tinprice;               // ��������� ���������� � ����� ����������

		public CS_SellInfo()
		{
			code_sell			= 0;
			code_position		= 0;
			code_reklama		= 0;
			flag_credit_inner	= false;
			flag_credit_outer	= false;
			flag_lising			= false;
			flag_cashless		= false;
			flag_partner		= false;
			flag_util			= false;

            flag_tin = false;
            tinprice = 0;
		}

		public CS_SellInfo(long sell)
		{
			code_sell			= sell;
			code_position		= 0;
			code_reklama		= 0;
			flag_credit_inner	= false;
			flag_credit_outer	= false;
			flag_lising			= false;
			flag_cashless		= false;
			flag_partner		= false;
			flag_util			= false;

            flag_tin = false;
            tinprice = 0;
		}
	}
}
