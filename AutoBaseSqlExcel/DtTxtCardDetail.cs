using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql
{
    class DtTxtCardDetail
    {

		private readonly DtCardDetail _cardDetail;
		private readonly string _amount; // Количество работы в единах измерения (Нормачасы или шт для сервисных пакетов)
		private readonly string _price; // Стоимость единицы измерения
		private readonly string _discount; // Скидка или бонус


		private string _detailName; // Наименование работы
		private string _catalogueNumber; // Каталожный номер (код) работы
		private string _unit; // Единица измерения

		public DtTxtCardDetail(DtCardDetail srcCardDetail)
		{
			_cardDetail = srcCardDetail;
			_amount = _cardDetail.SimpleAmount().ToString();
			_price = _cardDetail.PriceUnit().ToString();
			_discount = _cardDetail.DetailSummDiscountPure.ToString();
			_detailName = "";
			_unit = "";
			_catalogueNumber = "";
		}

		#region Простые Геттеры
		public string Amount { get { return _amount; } }
		public string Price { get { return _price; } }
		public string Discount { get { return _discount; } }
		#endregion

		#region Сложные Геттеры
		public string WorkName
		{
			get
			{
				if (_detailName != "") return _detailName;
				if (_cardDetail.StorageDetail == null)
					return _detailName = "-";
				return _detailName = _cardDetail.StorageDetail.Name;
			}
		}
		public string CatalogueNumber
		{
			get
			{
				if (_catalogueNumber != "") return _catalogueNumber;
				if (_cardDetail.StorageDetail == null)
					return _catalogueNumber = "-";
				return _catalogueNumber = _cardDetail.StorageDetail.CatalogueNumber;
			}
		}
		public string Unit
		{
			get
			{
				if (_unit != "") return _unit;
				return _unit = _cardDetail.StorageDetail.Unit;
			}
		}
        #endregion
    }
}
