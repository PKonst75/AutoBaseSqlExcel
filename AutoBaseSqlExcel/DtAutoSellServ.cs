using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoSellServ.
	/// </summary>
	public class DtAutoSellServ
	{
		public long code_sell;				// Êîä ïğîäàæè
		public long code_manager;			// Êîä ìåíåäæåğà
		public bool flag_music;				// Ôëàã ìóçûêà
		public bool flag_alarm;				// Ôëàã ñèãíàëèçàöèÿ
		public bool flag_anti;				// Ôëàã àíòèêîğ
		public bool flag_anti1;				// Ôëàã ïîäêğûëêè
		public bool flag_anti2;				// Ôëàã çàùèòà
		public bool flag_tune;				// Ôëàã òşíèíã
		public bool flag_other;				// Ôëàã àêñêññóàğû
		public bool flag_gibdd;				// Ôëàã ÃÈÁÄÄ
		public bool flag_sprav;				// Ôëàã ÑÏĞÀÂÊÀ-Ñ×ÅÒ
		public bool flag_kasko;				// Ôëàã ÊÀÑÊÎ
		public bool flag_osago;				// Ôëàã ÎÑÀÃÎ

		public float summ_whole;			// Ñóììà äîïîâ
		public float summ_anti;				// Ñóììà äîïîâ
		public float summ_sprav;			// Ñóììà ÄÊÏ
		public float auto_summ;				// Àâòîìîáèëü ñòîèìîñòü
		public float auto_discount_money;	// Àâòîìîáèëü ñêèäêà äåíüãè
		public float auto_discount_other;	// Àâòîìîáèëü ñêèäêà (ïîäàğîê)
		public float auto_discount_anti;	// Àâòîìîáèëü ñêèäêà àíòèêîğ
		public float auto_discount_tunemus;	// Àâòîìîáèëü ñêèäêà äîïû


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
				case "ÊÎÄ_ÏĞÎÄÀÆÀ":
					return (object)(long)code_sell;
				case "ÌÅÍÅÄÆÅĞ":
					return (object)(long)code_manager;
				case "ÌÓÇÛÊÀ":
					return (object)(bool)flag_music;
				case "ÑÈÃÍÀËÈÇÀÖÈß":
					return (object)(bool)flag_alarm;
				case "ÒŞÍÈÍÃ":
					return (object)(bool)flag_tune;
				case "ÀÍÒÈÊÎĞ":
					return (object)(bool)flag_anti;
				case "ÏÎÄÊĞÛËÊÈ":
					return (object)(bool)flag_anti1;
				case "ÇÀÙÈÒÀ":
					return (object)(bool)flag_anti2;
				case "ÀÊÑÅÑÑÓÀĞÛ":
					return (object)(bool)flag_other;
				case "ÃÈÁÄÄ":
					return (object)(bool)flag_gibdd;
				case "ÑÏĞÀÂÊÀÑ×ÅÒ":
					return (object)(bool)flag_sprav;
				

				case "ÊÀÑÊÎ":
					return (object)(bool)flag_kasko;
				case "ÎÑÀÃÎ":
					return (object)(bool)flag_osago;

				case "ÄÎÏÛ_ÑÓÌÌÀ":
					return (object)(float)summ_whole;
				case "ÀÍÒÈÊÎĞ_ÑÓÌÌÀ":
					return (object)(float)summ_anti;
				case "ÑÏĞÀÂÊÀÑ×ÅÒ_ÑÓÌÌÀ":
					return (object)(float)summ_sprav;
				case "ÀÂÒÎ_ÑÒÎÈÌÎÑÒÜ":
					return (object)(float)auto_summ;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÅÍÜÃÈ":
					return (object)(float)auto_discount_money;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÏÎÄÀĞÎÊ":
					return (object)(float)auto_discount_other;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÀÍÒÈÊÎĞ":
					return (object)(float)auto_discount_anti;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÎÏÛ":
					return (object)(float)auto_discount_tunemus;

				default:
					return (object)null;

				
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÏĞÎÄÀÆÀ":
					code_sell = (long)val;
					break;
				case "ÌÅÍÅÄÆÅĞ":
					code_manager = (long)val;
					break;
				case "ÌÓÇÛÊÀ":
					flag_music = (bool)val;
					break;
				case "ÑÈÃÍÀËÈÇÀÖÈß":
					flag_alarm = (bool)val;
					break;
				case "ÒŞÍÈÍÃ":
					flag_tune = (bool)val;
					break;
				case "ÀÍÒÈÊÎĞ":
					flag_anti = (bool)val;
					break;
				case "ÏÎÄÊĞÛËÊÈ":
					flag_anti1 = (bool)val;
					break;
				case "ÇÀÙÈÒÀ":
					flag_anti2 = (bool)val;
					break;
				case "ÀÊÑÅÑÑÓÀĞÛ":
					flag_other = (bool)val;
					break;
				case "ÃÈÁÄÄ":
					flag_gibdd = (bool)val;
					break;
				case "ÑÏĞÀÂÊÀÑ×ÅÒ":
					flag_sprav = (bool)val;
					break;
				

				case "ÊÀÑÊÎ":
					flag_kasko = (bool)val;
					break;
				case "ÎÑÀÃÎ":
					flag_osago = (bool)val;
					break;

				case "ÄÎÏÛ_ÑÓÌÌÀ":
					summ_whole = (float)val;
					break;
				case "ÀÍÒÈÊÎĞ_ÑÓÌÌÀ":
					summ_anti = (float)val;
					break;
				case "ÑÏĞÀÂÊÀÑ×ÅÒ_ÑÓÌÌÀ":
					summ_sprav = (float)val;
					break;

				case "ÀÂÒÎ_ÑÒÎÈÌÎÑÒÜ":
					auto_summ = (float)val;
					break;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÅÍÜÃÈ":
					auto_discount_money = (float)val;
					break;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÏÎÄÀĞÎÊ":
					auto_discount_other = (float)val;
					break;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÀÍÒÈÊÎĞ":
					auto_discount_anti = (float)val;
					break;
				case "ÀÂÒÎ_ÑÊÈÄÊÀ_ÄÎÏÛ":
					auto_discount_tunemus = (float)val;
					break;

				default:
					break;
			}
		}
	}
}
