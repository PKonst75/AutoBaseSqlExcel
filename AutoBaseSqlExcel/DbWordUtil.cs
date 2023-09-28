using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBaseSql
{
    class DbWordUtil:DbWord
    {
        string client_name = "!ЗАМЕНИ_ФАМИЛИЯ ИМЯ ОТЧЕСТВО";
        string client_name_short = "!ЗАМЕНИ ФАМИЛИЯ И.О.";
        string client_adr = "!ЗАМЕНИ АДРЕС";
        string client_phone = "!ЗАМЕНИ ТЕЛЕФОН";
        string passport_series = "ЗАМЕНИ! Серия паспорта";
        string passport_number = "ЗАМЕНИ! Номер паспорта";
        string passport_date = "ЗАМЕНИ! Дата выдачи паспорта";
        string passport_place = "ЗАМЕНИ! Кем выдан паспорт";
        string ptstxt = "ЗАМЕНИ! Данные ПТС";
        string cnt_day = "ЗАМЕНИ! День договора";
        string cnt_month = "ЗАМЕНИ! Месяц договора";
        string cnt_year = "ЗАМЕНИ! Год договора";
        string cnt_number = "ЗАМЕНИ! Номер договора";
        string ut_act_no = "ЗАМЕНИ! Номер акта утилизации";
        string ut_act_date = "ЗАМЕНИ! Дату акта утилизации";

        string auto_price = "ЗАМЕНИ! Стоимость автомобиля";
        string auto_price_text = "ЗАМЕНИ! Стоимость автомобиля прописью";
        string auto_price_unit = "ЗАМЕНИ! НА РУБЛИ ИЛИ РУБЛЕЙ";
        string auto_price_util = "ЗАМЕНИ! Стоимость автомобиля по утилизации";
        string auto_price_util_text = "ЗАМЕНИ! Стоимость автомобиля по утилизации прописью";
        string auto_price_util_unit = "ЗАМЕНИ! НА РУБЛИ ИЛИ РУБЛЕЙ";
        string auto_price_nds = "ЗАМЕНИ! НА ВЕЛИЧИНУ НДС";

        string auto_price_tin = "ЗАМЕНИ! НА СУММУ ПО ТРЕЙДИН";
        string auto_price_tin_nal = "ЗАМЕНИ! СУММУ ТРЕЙДИН";
        string auto_price_tin_text = "ЗАМЕНИ! НА СУММУ ПО ТРЕЙДИН";
        string auto_price_tin_unit = "ЗАМЕНИ! РУБЛИ ИЛИ РУБЛЕЙ";
        string auto_price_tin_nal_text = "ЗАМЕНИ! НА СУММУ ОСТАВШУЮСЯ ПОСЛЕ ТРЕЙДИН";
        string auto_price_tin_nal_unit = "ЗАМЕНИ! РУБЛИ ИЛИ РУБЛЕЙ";
        string price_tin = "ЗАМЕНИ! НА СТОИМОСТЬ ТРЕЙДИН";
        string price_tin_unit = "ЗАМЕНИ! РУБЛИ ИЛИ РУБЛЕЙ";
        string price_tin_text = "ЗАМЕНИ! НА СТОИМОСТЬ ТРЕЙДИН";
        string auto_price_nds_tin = "ЗАМЕНИ! НА ВЕЛИЧИНУ НДС ПО РЕЙДИН";


        string car_model = "!!!";
        string car_engine = "!!!";
        string car_year = "!!!";
        string car_vin = "!!!";
        string car_trans = "!!!";
        string car_type = "!!!";
        string car_body = "!!!";
        string car_color = "!!!";
        
        public DbWordUtil(long code_dkp)
        {
            //Находим данные по договору купли продажи
            DtAutoSell sell = DbSqlAutoSell.FindSell(code_dkp);
            if (sell == null) return;
            DateTime cnt_date = (DateTime)sell.GetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА");
            cnt_number = ((long)sell.GetData("КОД_АВТОМОБИЛЬ_ПРОДАЖА")).ToString();
            ut_act_date = cnt_date.ToShortDateString();
            ut_act_no = cnt_number;
            cnt_month   = Digit2Month(cnt_date.Month);
            cnt_day     = cnt_date.Day.ToString();
            cnt_year    = cnt_date.Year.ToString();

            long code_auto = (long)sell.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ");
            long code_client = (long)sell.GetData("ССЫЛКА_КОД_ПОКУПАТЕЛЬ");
            DtAuto auto = null;
            DtPartner client = null;
            DtAutoSellServ autosellserv = null;
            CS_SellInfo autosellinfo = null;
            if (code_auto != 0)
                auto = (DtAuto)DbSqlAuto.Find(code_auto);
            if (code_client != 0)
                client = (DtPartner)DbSqlPartner.Find(code_client);
            autosellinfo = DbSqlSellInfo.Find(code_dkp);
            autosellserv = DbSqlAutoSellServ.Find(code_dkp);

            if (auto != null)
            {
                // Собираем данные по автомобилю
                long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
                long code_color = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ");
                
                DtModel model = DbSqlModel.Find(code_model);
                DtColor color = DbSqlColor.Find(code_color);
                car_year = ((int)auto.GetData("ГОД_ВЫПУСК")).ToString();
                car_vin = (string)auto.GetData("VIN");
                car_body = (string)auto.GetData("НОМЕР_КУЗОВ");
                car_engine = (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
                
                if (model != null)
                {
                    car_model = (string)model.GetData("МАРКА_МОДЕЛЬ_ПТС");
                    car_engine = (string)model.GetData("ДВИГАТЕЛЬ") + ", " + car_engine;
                    car_type = (string)model.GetData("ТИП_ТС");
                    car_trans = (string)model.GetData("ТРАНСМИССИЯ");
                }
                if (color != null)
                {
                    car_color = (string)color.GetData("ЦВЕТ_ОПИСАНИЕ");
                }

                // Пробуем получить данные ПТС
                DtAutoPts pts = DbSqlAutoPts.Find(code_auto);
                if (pts != null)
                {
                    string pts_series = (string)pts.GetData("СЕРИЯ");
                    string pts_number = (string)pts.GetData("НОМЕР");
                    string pts_date = ((DateTime)pts.GetData("ВЫДАН_КОГДА")).ToShortDateString();
                    string pts_place = (string)pts.GetData("ВЫДАН_КЕМ");
                    ptstxt = "№" + pts_number + " " + pts_series + ", выдан " + pts_date + " " + pts_place;
                }

            }
            if (autosellserv != null)
            {
                // Собираем дополнительные
                double auto_summ = autosellserv.auto_summ;
                double auto_summ_util = auto_summ - 50000;
                double auto_nds = auto_summ * (double)18 / (double)118;
                auto_price_nds = Db.CachToTxt(auto_nds);
                auto_price = Db.CachToTxt(auto_summ);
                auto_price_util = Db.CachToTxt(auto_summ_util);
                auto_price_text = UI_Digit2Text.ConvertNoUnit(auto_summ);
                auto_price_unit = UI_Digit2Text.ConvertOnlyUnit(auto_summ);
                auto_price_util_text = UI_Digit2Text.ConvertNoUnit(auto_summ_util);
                auto_price_util_unit = UI_Digit2Text.ConvertOnlyUnit(auto_summ_util);
            }
            if ((autosellinfo != null) && (autosellserv != null))
            {
                // Собираем дополнительные
                double auto_summ1 = autosellserv.auto_summ;
                double auto_summ_tin = auto_summ1 - 40000;
                double auto_nds_tin = auto_summ1 * (double)18 / (double)118;
                double summ_tin = (double)autosellinfo.tinprice;
                double auto_summ_tin_nal = auto_summ_tin - summ_tin;
                auto_price_nds_tin = Db.CachToTxt(auto_nds_tin);
                auto_price_tin = Db.CachToTxt(auto_summ_tin);        
                auto_price_tin_text = UI_Digit2Text.ConvertNoUnit(auto_summ_tin);
                auto_price_tin_unit = UI_Digit2Text.ConvertOnlyUnit(auto_summ_tin);

                price_tin = Db.CachToTxt(summ_tin);
                price_tin_text = UI_Digit2Text.ConvertNoUnit(summ_tin);
                price_tin_unit = UI_Digit2Text.ConvertOnlyUnit(summ_tin);

                auto_price_tin_nal = Db.CachToTxt(auto_summ_tin_nal);
                auto_price_tin_nal_text = UI_Digit2Text.ConvertNoUnit(auto_summ_tin_nal);
                auto_price_tin_nal_unit = UI_Digit2Text.ConvertOnlyUnit(auto_summ_tin_nal);
            }
            if (client != null)
            {
                // Собираем данные по клиенту
                client_name = client.GetTitle();
                client_name_short = client.GetTitleShort();
                client_adr = client.GetAddress();
                client_phone = client.GetPhone();
                if ((bool)client.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == true)
                {
                }
                else
                {
                    // Пробуем получить данные паспорта
                    DtPassport passport = DbSqlPassport.Find(code_client);
                    if (passport != null)
                    {
                        passport_series = (string)passport.GetData("СЕРИЯ");
                        passport_number = (string)passport.GetData("НОМЕР");
                        passport_date = ((DateTime)passport.GetData("ВЫДАН_КОГДА")).ToShortDateString();
                        passport_place = (string)passport.GetData("ВЫДАН_КЕМ");
                    }
                    DtPartnerPerson person = (DtPartnerPerson)client.GetData("ФИЗИЧЕСКОЕ");
                    if (person != null)
                    {
                       
                    }
                }
            }
        }

        // Открываем утилизационный договор со всеми нужными нам заменами
        public bool OpenUtilDkp(string filename)
        {
            OpenFile(filename);
            ReplaceAll("#cnt_person", client_name, wordapp);
            ReplaceAll("#person_short", client_name_short, wordapp);
            ReplaceAll("#person_adr", client_adr, wordapp);
            ReplaceAll("#person_phone", client_phone, wordapp);
            ReplaceAll("#doc_ser", passport_series, wordapp);
            ReplaceAll("#doc_num", passport_number, wordapp);
            ReplaceAll("#doc_date", passport_date, wordapp);
            ReplaceAll("#doc_place", passport_place, wordapp);
            ReplaceAll("#auto_price", auto_price, wordapp);
            ReplaceAll("#auto_txtprice", auto_price_text, wordapp);
            ReplaceAll("#auto_unitprice", auto_price_unit, wordapp);
            ReplaceAll("#auto_finprice", auto_price_util, wordapp);
            ReplaceAll("#auto_txtfinprice", auto_price_util_text, wordapp);
            ReplaceAll("#auto_unitfinprice", auto_price_util_unit, wordapp);
            ReplaceAll("#ndspart", auto_price_nds, wordapp);
            ReplaceAll("#car_pts", ptstxt, wordapp);
            ReplaceAll("#cnt_day", cnt_day, wordapp);
            ReplaceAll("#cnt_month", cnt_month, wordapp);
            ReplaceAll("#cnt_year", cnt_year, wordapp);
            ReplaceAll("#cnt_number", cnt_number, wordapp);
            ReplaceAll("#ut_act_no", ut_act_no, wordapp);
            ReplaceAll("#ut_act_date", ut_act_date, wordapp);

            ReplaceAll("#car_model", car_model, wordapp);
            ReplaceAll("#car_year", car_year, wordapp);
            ReplaceAll("#car_vin", car_vin, wordapp);
            ReplaceAll("#car_engine", car_engine, wordapp);
            ReplaceAll("#car_body", car_body, wordapp);
            ReplaceAll("#car_color", car_color, wordapp);
            ReplaceAll("#car_type", car_type, wordapp);
            ReplaceAll("#car_trans", car_trans, wordapp);

            wordapp.Visible = true;
            return true;
        }

        public bool OpenTinDkp(string filename)
        {
            OpenFile(filename);
            ReplaceAll("#cnt_person", client_name, wordapp);
            ReplaceAll("#person_short", client_name_short, wordapp);
            ReplaceAll("#person_adr", client_adr, wordapp);
            ReplaceAll("#person_phone", client_phone, wordapp);
            ReplaceAll("#doc_ser", passport_series, wordapp);
            ReplaceAll("#doc_num", passport_number, wordapp);
            ReplaceAll("#doc_date", passport_date, wordapp);
            ReplaceAll("#doc_place", passport_place, wordapp);
            ReplaceAll("#auto_price", auto_price, wordapp);
            ReplaceAll("#auto_txtprice", auto_price_text, wordapp);
            ReplaceAll("#auto_unitprice", auto_price_unit, wordapp);
            ReplaceAll("#auto_finprice", auto_price_util, wordapp);
            ReplaceAll("#auto_txtfinprice", auto_price_util_text, wordapp);
            ReplaceAll("#auto_unitfinprice", auto_price_util_unit, wordapp);
            ReplaceAll("#ndspart", auto_price_nds, wordapp);
            ReplaceAll("#car_pts", ptstxt, wordapp);
            ReplaceAll("#cnt_day", cnt_day, wordapp);
            ReplaceAll("#cnt_month", cnt_month, wordapp);
            ReplaceAll("#cnt_year", cnt_year, wordapp);
            ReplaceAll("#cnt_number", cnt_number, wordapp);
            ReplaceAll("#ut_act_no", ut_act_no, wordapp);
            ReplaceAll("#ut_act_date", ut_act_date, wordapp);

            ReplaceAll("#car_model", car_model, wordapp);
            ReplaceAll("#car_year", car_year, wordapp);
            ReplaceAll("#car_vin", car_vin, wordapp);
            ReplaceAll("#car_engine", car_engine, wordapp);
            ReplaceAll("#car_body", car_body, wordapp);
            ReplaceAll("#car_color", car_color, wordapp);
            ReplaceAll("#car_type", car_type, wordapp);
            ReplaceAll("#car_trans", car_trans, wordapp);

            ReplaceAll("#tin_price", price_tin, wordapp);
            ReplaceAll("#tin_txtprice", price_tin_text, wordapp);
            ReplaceAll("#auto_tinprice", auto_price_tin, wordapp);
            ReplaceAll("#auto_txttinprice", auto_price_tin_text, wordapp);
            ReplaceAll("#auto_unittinprice", auto_price_tin_unit, wordapp);
            ReplaceAll("#tinndspart", auto_price_nds_tin, wordapp);
            ReplaceAll("#auto_nalprice", auto_price_tin_nal, wordapp);
            ReplaceAll("#auto_txtnalprice", auto_price_tin_nal_text, wordapp);

            wordapp.Visible = true;
            return true;
        }

        string Digit2Month(int month)
        {
            switch (month)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                default:
                    return "ЗАМЕНИ! Месяц";
            }
        }
    }
}
