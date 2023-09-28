using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace AutoBaseSql
{
    class UI_Digit2Text
    {
        static string[] sot = new string[] {""      , "сто",   "двести","триста", "четыреста","пятьсот","шестьсот","семьсот","восемьсот","девятьсот"};
        static string[] ed = new string[] {"", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять"};
        static string[] ed_f = new string[] {"",  "одна", "две", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
        static string[] eddes = new string[] {"десять",  "одинадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семьнадцать", "восемьнадцать", "девятьнадцать" };
        static string[] des = new string[]  {"", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто"};
        static string[] unit = new string[] {"рублей", "рубль", "рубля", "рубля",  "рубля",    "рублей", "рублей",  "рублей", "рублей",   "рублей" };
        static string[] tys = new string[] { "тясяч", "тысяча", "тысячи", "тысячи", "тысячи", "тысяч", "тысяч", "тысяч", "тысяч", "тысяч", "тысяч" };

        public static string Panny(string digit)
        {
            int idigit = 0;
            int.TryParse(digit, out idigit);
            return Panny(idigit);
        }
        public static string Panny(int digit)
        {
            string txt_panny = "";
            if (digit < 21)
            {
                switch (digit)
                {
                    case 0: txt_panny = "копеек"; break;
                    case 1: txt_panny = "копейка"; break;
                    case 2:
                    case 3:
                    case 4: txt_panny = "копейки"; break;
                    default: txt_panny = "копеек"; break;
                }
            }
            else
            {
                // Определяем количество единиц
                int u10 = (int)Math.Floor((double)(digit / 10));
                int u1 = digit - u10 * 10;
                switch (u1)
                {
                    case 0: txt_panny = "копеек"; break;
                    case 1: txt_panny = "копейка"; break;
                    case 2:
                    case 3:
                    case 4: txt_panny = "копейки"; break;
                    default: txt_panny = "копеек"; break;
                }
            }
            return txt_panny;
        }
        public static string Convert(string digit)
        {
            double ddigit = 0;
            double.TryParse(digit,out ddigit);
            string sdigit = Convert(ddigit);
            return sdigit;
        }
         public static string Convert(double digit)
        {
            long dgt = (long)Math.Round(digit);

            // Определяем количество сотен тысяч
            int u100000 = (int) Math.Floor((double)(dgt / 100000));
            dgt = dgt - u100000 * 100000;
            // Определяем количество десяток тысяч
            int u10000 = (int)Math.Floor((double)(dgt / 10000));
            dgt = dgt - u10000 * 10000;
            // Определяем количество единиц тысяч
            int u1000 = (int)Math.Floor((double)(dgt / 1000));
            dgt = dgt - u1000 * 1000;

            // Определяем количество сотен
            int u100 = (int)Math.Floor((double)(dgt / 100));
            dgt = dgt - u100 * 100;
            // Определяем количество десятков
            int u10 = (int)Math.Floor((double)(dgt / 10));
            dgt = dgt - u10 * 10;
            // Определяем количество единиц
            int u1 = (int)Math.Floor((double)(dgt / 1));
            dgt = dgt - u1 * 1;

            if (dgt != 0) MessageBox.Show("ЧТО ТО ПОШЛО НЕ ТАК:" + dgt.ToString() );

            string res = "";
            // Анализируем результат и собираем строку
            if ((u100000 != 0) || (u10000 != 0) || (u1000 != 0))
            {
                if (u100000 > 0) res = sot[u100000] + " ";
                if (u10000 == 1)
                {
                    res += eddes[u1000] + " " + tys[0] + " ";
                }
                else
                {
                    res += des[u10000] + " " + ed_f[u1000] + " " + tys[u1000] + " ";
                }
            }

            if (u100 > 0) res += sot[u100] + " ";
            if (u10 == 1)
            {
                res += eddes[u1] + " " + unit[0];
            }
            else
            {
                res += des[u10] + " " + ed[u1] + " " + unit[u1];
            }
            return res;
        }

        public static string ConvertNoUnit(double digit)
        {
            long dgt = (long)Math.Round(digit);

            // Определяем количество сотен тысяч
            int u100000 = (int)Math.Floor((double)(dgt / 100000));
            dgt = dgt - u100000 * 100000;
            // Определяем количество десяток тысяч
            int u10000 = (int)Math.Floor((double)(dgt / 10000));
            dgt = dgt - u10000 * 10000;
            // Определяем количество единиц тысяч
            int u1000 = (int)Math.Floor((double)(dgt / 1000));
            dgt = dgt - u1000 * 1000;

            // Определяем количество сотен
            int u100 = (int)Math.Floor((double)(dgt / 100));
            dgt = dgt - u100 * 100;
            // Определяем количество десятков
            int u10 = (int)Math.Floor((double)(dgt / 10));
            dgt = dgt - u10 * 10;
            // Определяем количество сотен
            int u1 = (int)Math.Floor((double)(dgt / 1));
            dgt = dgt - u1 * 1;

            if (dgt != 0) MessageBox.Show("ЧТО ТО ПОШЛО НЕ ТАК:" + dgt.ToString());

            string res = "";
            // Анализируем результат и собираем строку
            if ((u100000 != 0) || (u10000 != 0) || (u1000 != 0))
            {
                if (u100000 > 0) res = sot[u100000] + " ";
                if (u10000 == 1)
                {
                    res += eddes[u1000] + " " + tys[0] + " ";
                }
                else
                {
                    res += des[u10000] + " " + ed_f[u1000] + " " + tys[u1000] + " ";
                }
            }

            if (u100 > 0) res += sot[u100] + " ";
            if (u10 == 1)
            {
                res += eddes[u1];
            }
            else
            {
                res += des[u10] + " " + ed[u1];
            }
            res = res.Trim();
            return res;
        }

        public static string ConvertOnlyUnit(double digit)
        {
            long dgt = (long)Math.Round(digit);

            // Определяем количество сотен тысяч
            int u100000 = (int)Math.Floor((double)(dgt / 100000));
            dgt = dgt - u100000 * 100000;
            // Определяем количество десяток тысяч
            int u10000 = (int)Math.Floor((double)(dgt / 10000));
            dgt = dgt - u10000 * 10000;
            // Определяем количество единиц тысяч
            int u1000 = (int)Math.Floor((double)(dgt / 1000));
            dgt = dgt - u1000 * 1000;

            // Определяем количество сотен
            int u100 = (int)Math.Floor((double)(dgt / 100));
            dgt = dgt - u100 * 100;
            // Определяем количество десятков
            int u10 = (int)Math.Floor((double)(dgt / 10));
            dgt = dgt - u10 * 10;
            // Определяем количество сотен
            int u1 = (int)Math.Floor((double)(dgt / 1));
            dgt = dgt - u1 * 1;

            if (dgt != 0) MessageBox.Show("ЧТО ТО ПОШЛО НЕ ТАК:" + dgt.ToString());

            string res = "";
            // Анализируем результат и собираем строку
           
            if (u10 == 1)
            {
                res = unit[0];
            }
            else
            {
                res = unit[u1];
            }
            return res;
        }
    }
}
