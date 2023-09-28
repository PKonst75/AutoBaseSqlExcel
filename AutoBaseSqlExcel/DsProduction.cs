using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������� ����������� ������, � ������� �������
	/// ������������� ��� � �������� ������, ��� � � �����-������ � �����
	/// </summary>
	public class DsProduction
	{
		public DsProductionWork		cash;				// �� ������
		public DsProductionWork		cash_bn;			// �� ������ �� �������
		public DsProductionWork		inner;				// ����������
		public DsProductionWork		ppp;				// ������������� ����������
		public DsProductionWork		guaranty_cash;		// ������������ ����� - �� ������
		public DsProductionWork		guaranty_cash_bn;	// ������������ ����� - �� �������
		public DsProductionWork		guaranty_inner;		// ������������ ����� - �� ����������
		public DsProductionWork		guaranty_ppp;		// ������������ ����� - �� ������������

		public int					count_work_cash;	// ���������� �������������, ���������� ������� ������ - ��������
		public int					count_work_cash_bn;	// ���������� �������������, ���������� ������� ������ - ������

		public ArrayList			man_work;		// �� ������������
		public ArrayList			guaranty_work;	// �� ����� ��������

		public DsProduction()
		{
			SetInitial();
		}
		public DsProduction(DtCard card)
		{
			SetInitial();
			// ������� ��������� �� ��������
			bool is_inner		= false;
			bool is_ppp			= false;
			bool is_cash		= false;
			bool is_cash_bn		= false;

			// �������� ������ ����� ��������
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(card, works);

			// ����������� ��� ��������
			is_inner			= (bool)card.GetData("����������_��������");
			is_cash_bn			= (bool)card.GetData("�����������_��������");
			if(is_inner == false && is_cash_bn == false) is_cash = true;
			long owner			= (long)card.GetData("��������_��������");
			if (owner == 399 || owner == 1088)
			{
				is_inner	= false;
				is_cash		= false;
				is_cash_bn	= false;
				is_ppp		= true;
			}

			foreach(object o in works)
			{
				DtCardWork card_work = (DtCardWork)o;
				DsProductionWork production_work = new DsProductionWork(card_work);
				if(is_cash == true)
				{
					if(production_work.is_guaranty)
						guaranty_cash.AddProduction(production_work);
					else
						cash.AddProduction(production_work);
				}
				if(is_cash_bn == true)
				{
					if(production_work.is_guaranty)
						guaranty_cash_bn.AddProduction(production_work);
					else
						cash_bn.AddProduction(production_work);
				}
				if(is_inner == true)
				{
					if(production_work.is_guaranty)
						guaranty_inner.AddProduction(production_work);
					else
						inner.AddProduction(production_work);
				}
				if(is_ppp == true)
				{
					if(production_work.is_guaranty)
						guaranty_ppp.AddProduction(production_work);
					else
						ppp.AddProduction(production_work);
				}
			}
			// ���������� ������ ������
			float discount = (float)card.GetData("������_������_��������");
			this.cash.SetDiscount(discount);
			this.cash_bn.SetDiscount(discount);
			this.inner.SetDiscount(discount);

			// ������� �������������
			if(cash.count_work > 0) count_work_cash = 1;
			if(cash_bn.count_work > 0) count_work_cash_bn = 1;
		}

		public void AddProduction(DsProduction product)
		{
			cash.AddProduction(product.cash);
			cash_bn.AddProduction(product.cash_bn);
			inner.AddProduction(product.inner);
			ppp.AddProduction(product.ppp);

			guaranty_cash.AddProduction(product.guaranty_cash);
			guaranty_cash_bn.AddProduction(product.guaranty_cash_bn);
			guaranty_inner.AddProduction(product.guaranty_inner);
			guaranty_ppp.AddProduction(product.guaranty_ppp);

			count_work_cash		+= product.count_work_cash;
			count_work_cash_bn	+= product.count_work_cash_bn;

			man_work			= null;
			guaranty_work		= null;
		}

		public void SetInitial()
		{
			cash				= new DsProductionWork();
			cash_bn				= new DsProductionWork();
			inner				= new DsProductionWork();
			ppp					= new DsProductionWork();
			guaranty_cash		= new DsProductionWork();
			guaranty_cash_bn	= new DsProductionWork();
			guaranty_inner		= new DsProductionWork();
			guaranty_ppp		= new DsProductionWork();

			count_work_cash		= 0;
			count_work_cash_bn	= 0;

			man_work			= null;
			guaranty_work		= null;
		}
	}
}
