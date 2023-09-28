using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrServiceProduction.
	/// </summary>
	public class DrServiceProduction
	{
		public DsProduction	service = new DsProduction();

		public DrServiceProduction(ArrayList cards)
		{
			foreach(DtCard element in cards)
			{
				DtCard card	= DbSqlCard.Find((long)element.GetData("�����_��������"), (int)element.GetData("���_��������"));
				DsProduction product = new DsProduction(card);
				service.AddProduction(product);
			}
		}
	}
}
