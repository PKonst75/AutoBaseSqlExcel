using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Анализ различных данных по карточке.
	/// </summary>
	public class DaCard:DtCard
	{
		public DaCard(DtCard card)
		{
			this.Number = card.Number;
			this.Year	= card.Year;
		}

		public bool IsGuarantyWork()
		{
			// Определяем есть ли гарантийные работы
			ArrayList array = new ArrayList();
			DbSqlCardWork.SelectInArray((DtCard)this, array);
			foreach(DtCardWork work in array)
			{
				if(work.GuaranteeFlag() == true)
				{
					array.Clear();
					return true;
				}
			}
			array.Clear();
			return false;
		}
		public bool IsGuarantyDetail()
		{
			// Определяем есть ли гарантийные работы
			ArrayList array = new ArrayList();
			DbSqlCardDetail.SelectInArray((DtCard)this, array);
			foreach(DtCardDetail detail in array)
			{
				if((bool)detail.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == true)
				{
					array.Clear();
					return true;
				}
			}
			array.Clear();
			return false;
		}
		public bool IsGuaranty()
		{
			if(IsGuarantyWork() == true) return true;
			if(IsGuarantyDetail() == true) return true;
			return false;
		}
		public bool IsTo()
		{
			long code_directorywork = 0;
			long code_work = 0;
			bool flag				= false;
			// Определяем есть ли в карточке ТО
			ArrayList array = new ArrayList();
			DbSqlCardWork.SelectInArray((DtCard)this, array);
			foreach(DtCardWork work in array)
			{
				code_work = work.CodeWork;// (long)work.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
				DtWork tmp_work = DbSqlWork.Find(code_work);
				code_directorywork = (long)tmp_work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ");
				if(code_directorywork == 1) flag = true;
				if(code_directorywork == 2) flag = true;
				if(code_directorywork == 3) flag = true;
				if(code_directorywork == 4) flag = true;
				if(code_directorywork == 5) flag = true;
				if(code_directorywork == 6) flag = true;
				if(code_directorywork == 725) flag = true;
				if(code_directorywork == 737) flag = true;
				if(code_directorywork == 738) flag = true;
				if(code_directorywork == 739) flag = true;
				if(code_directorywork == 740) flag = true;
				if(code_directorywork == 741) flag = true;
				if(code_directorywork == 9) flag = true;
				if(code_directorywork == 460) flag = true;
				if(code_directorywork == 517) flag = true;
				if(code_directorywork == 727) flag = true;
				if(code_directorywork == 518) flag = true;
				if(code_directorywork == 728) flag = true;
				if(code_directorywork == 730) flag = true;
				if(code_directorywork == 729) flag = true;
				if(code_directorywork == 732) flag = true;
				if(code_directorywork == 731) flag = true;
				if(code_directorywork == 742) flag = true;
				if(code_directorywork == 733) flag = true;
				if(code_directorywork == 734) flag = true;
				if(code_directorywork == 736) flag = true;
				if(code_directorywork == 735) flag = true;
				if(flag == true)
				{
					array.Clear();
					return true;
				}
			}
			array.Clear();
			return false;
		}
	}
}
