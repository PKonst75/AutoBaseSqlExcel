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
				DtCard card	= DbSqlCard.Find((long)element.GetData("臀膛衉世幸巫世"), (int)element.GetData("梦腳世幸巫世"));
				DsProduction product = new DsProduction(card);
				service.AddProduction(product);
			}
		}
	}
}
