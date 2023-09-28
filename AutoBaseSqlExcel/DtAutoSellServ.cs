using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoSellServ.
	/// </summary>
	public class DtAutoSellServ
	{
		public long code_sell;				// ��� �������
		public long code_manager;			// ��� ���������
		public bool flag_music;				// ���� ������
		public bool flag_alarm;				// ���� ������������
		public bool flag_anti;				// ���� �������
		public bool flag_anti1;				// ���� ���������
		public bool flag_anti2;				// ���� ������
		public bool flag_tune;				// ���� ������
		public bool flag_other;				// ���� ����������
		public bool flag_gibdd;				// ���� �����
		public bool flag_sprav;				// ���� �������-����
		public bool flag_kasko;				// ���� �����
		public bool flag_osago;				// ���� �����

		public float summ_whole;			// ����� �����
		public float summ_anti;				// ����� �����
		public float summ_sprav;			// ����� ���
		public float auto_summ;				// ���������� ���������
		public float auto_discount_money;	// ���������� ������ ������
		public float auto_discount_other;	// ���������� ������ (�������)
		public float auto_discount_anti;	// ���������� ������ �������
		public float auto_discount_tunemus;	// ���������� ������ ����


		public DtAutoSellServ()
		{
			code_sell		= 0L;			
			code_manager	= 0L;			
			flag_music		= false;		
			flag_alarm		= false;		
			flag_anti		= false;		
			flag_anti1		= false;		
			flag_anti2		= false;		
			flag_tune		= false;		
			flag_other		= false;
			flag_gibdd		= false;
			flag_sprav		= false;
			flag_kasko		= false;
			flag_osago		= false;
		
			summ_whole		= 0.0F;
			summ_anti		= 0.0F;
			summ_sprav		= 0.0F;
			auto_summ		= 0.0F;
			auto_discount_money		= 0.0F;
			auto_discount_other		= 0.0F;
			auto_discount_anti		= 0.0F;
			auto_discount_tunemus	= 0.0F;
		}

		public DtAutoSellServ(DtAutoSellServ serv)
		{
			code_sell		= serv.code_sell;			
			code_manager	= serv.code_manager;			
			flag_music		= serv.flag_music;		
			flag_alarm		= serv.flag_alarm;		
			flag_anti		= serv.flag_anti;		
			flag_anti1		= serv.flag_anti1;		
			flag_anti2		= serv.flag_anti2;		
			flag_tune		= serv.flag_tune;		
			flag_other		= serv.flag_other;
			flag_gibdd		= serv.flag_gibdd;
			flag_sprav		= serv.flag_sprav;
			flag_kasko		= serv.flag_kasko;
			flag_osago		= serv.flag_osago;
		
			summ_whole		= serv.summ_whole;
			summ_anti		= serv.summ_anti;
			summ_sprav		= serv.summ_sprav;
			auto_summ		= serv.auto_summ;
			auto_discount_money		= serv.auto_discount_money;
			auto_discount_other		= serv.auto_discount_other;
			auto_discount_anti		= serv.auto_discount_anti;
			auto_discount_tunemus	= serv.auto_discount_tunemus;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_�������":
					return (object)(long)code_sell;
				case "��������":
					return (object)(long)code_manager;
				case "������":
					return (object)(bool)flag_music;
				case "������������":
					return (object)(bool)flag_alarm;
				case "������":
					return (object)(bool)flag_tune;
				case "�������":
					return (object)(bool)flag_anti;
				case "���������":
					return (object)(bool)flag_anti1;
				case "������":
					return (object)(bool)flag_anti2;
				case "����������":
					return (object)(bool)flag_other;
				case "�����":
					return (object)(bool)flag_gibdd;
				case "�����������":
					return (object)(bool)flag_sprav;
				

				case "�����":
					return (object)(bool)flag_kasko;
				case "�����":
					return (object)(bool)flag_osago;

				case "����_�����":
					return (object)(float)summ_whole;
				case "�������_�����":
					return (object)(float)summ_anti;
				case "�����������_�����":
					return (object)(float)summ_sprav;
				case "����_���������":
					return (object)(float)auto_summ;
				case "����_������_������":
					return (object)(float)auto_discount_money;
				case "����_������_�������":
					return (object)(float)auto_discount_other;
				case "����_������_�������":
					return (object)(float)auto_discount_anti;
				case "����_������_����":
					return (object)(float)auto_discount_tunemus;

				default:
					return (object)null;

				
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_�������":
					code_sell = (long)val;
					break;
				case "��������":
					code_manager = (long)val;
					break;
				case "������":
					flag_music = (bool)val;
					break;
				case "������������":
					flag_alarm = (bool)val;
					break;
				case "������":
					flag_tune = (bool)val;
					break;
				case "�������":
					flag_anti = (bool)val;
					break;
				case "���������":
					flag_anti1 = (bool)val;
					break;
				case "������":
					flag_anti2 = (bool)val;
					break;
				case "����������":
					flag_other = (bool)val;
					break;
				case "�����":
					flag_gibdd = (bool)val;
					break;
				case "�����������":
					flag_sprav = (bool)val;
					break;
				

				case "�����":
					flag_kasko = (bool)val;
					break;
				case "�����":
					flag_osago = (bool)val;
					break;

				case "����_�����":
					summ_whole = (float)val;
					break;
				case "�������_�����":
					summ_anti = (float)val;
					break;
				case "�����������_�����":
					summ_sprav = (float)val;
					break;

				case "����_���������":
					auto_summ = (float)val;
					break;
				case "����_������_������":
					auto_discount_money = (float)val;
					break;
				case "����_������_�������":
					auto_discount_other = (float)val;
					break;
				case "����_������_�������":
					auto_discount_anti = (float)val;
					break;
				case "����_������_����":
					auto_discount_tunemus = (float)val;
					break;

				default:
					break;
			}
		}
	}
}
