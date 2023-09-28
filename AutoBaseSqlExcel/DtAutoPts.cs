using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBaseSql
{
    class DtAutoPts
    {
        long code;					// Уникальный код паспорта
        long code_auto;			// Код партнера	
        bool active;                 // Отметка активности
        string series;
        string number;
        DateTime date;
        string place;

        public DtAutoPts()
		{
			code				= 0;
			code_auto       	= 0;
			active              = true;
			series      		= "";
			number          	= "";
            date                = DateTime.Now;
            place = "";
        }
        public object GetData(string data)
        {
            switch (data)
            {
                case "КОД_ПТС":
                    return (object)(long)code;
                case "ССЫЛКА_КОД_АВТОМОБИЛЬ":
                    return (object)(long)code_auto;
                case "АКТИВЕН":
                    return (object)(bool)active;
                case "СЕРИЯ":
                    return (object)(string)series;
                case "НОМЕР":
                    return (object)(string)number;
                // Временные
                case "ВЫДАН_КОГДА":
                    return (object)(DateTime)date;
                case "ВЫДАН_КЕМ":
                    return (object)(string)place;
                default:
                    return (object)null;
            }
        }

        public void SetData(string data, object val)
        {
            switch (data)
            {
                case "КОД_ПТС":
                    code = (long)val;
                    break;
                case "ССЫЛКА_КОД_АВТОМОБИЛЬ":
                    code_auto = (long)val;
                    break;
                case "АКТИВЕН":
                    active = (bool)val;
                    break;
                case "СЕРИЯ":
                    series = (string)val;
                    break;
                case "НОМЕР":
                    number = (string)val;
                    break;
                case "ВЫДАН_КОГДА":
                    date = (DateTime)val;
                    break;
                case "ВЫДАН_КЕМ":
                    place = (string)val;
                    break;
                default:
                    break;
            }
        }
    }
}
