using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql
{
    class DtOrganization
    {
        long code;  // Уникальный код в базе данных
        string name;    // Наименование организации ПОЛНОЕ
        string address_fact;    //Фактический адрес
        string web; // Адрес сайта (может быть NULL)
        string e_mail;  // Электронный адрес (NULL)
        string phone; // Телефон (NULL)

        public DtOrganization()
        {
            code = 0;
            name = "";
            address_fact = "";
            web = "";
            e_mail = "";
            phone = "";
        }
    }

    
}
