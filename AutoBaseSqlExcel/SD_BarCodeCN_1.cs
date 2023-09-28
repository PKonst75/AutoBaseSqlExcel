using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
    public class SD_BarCodeCN_1
    {
        // КЛАСС описывает и реализует обработку штрих-кода Шевроле-Нива с гарантийного талона
        public bool valid;  // Контролька правильного заполнения штрих-кода
        public string vin;  // Полученный VIN автомобиля
        public string variant;  // Строка наименования варианта
        public string engine_model;  // Строка наименования модели двигателя
        public string engine_number;  // Строка номера двигателя
        public string color_code;  // Cтрока кода цвета

        public SD_BarCodeCN_1(string initial)
        {
            valid = false;
            string tmpstr = "";
//014646464554444642444145444654424345444444455447464546544147434D43424454414544465442445444465444424754454D4D4C41414432444447464546384D2C
            // Первые проверки
            if (initial.Length != 136) { MessageBox.Show("Неверная длина штрих-кода"); return; }
            tmpstr = initial.Substring(0, 2);
            if (tmpstr != "01") { MessageBox.Show("Неверные начальные символы"); return; }
            tmpstr = initial.Substring(118, 18);
            if (tmpstr != "444447464546384D2C") { MessageBox.Show("Неверные конечные символы"); return; }
            // Формируем vin
            tmpstr = initial.Substring(102, 34);
            vin = MakeFromInitial(tmpstr);
            if (vin.Length != 17) { MessageBox.Show("Некорректный VIN: " + vin); return; }
            // Формируем код цвета
            tmpstr = initial.Substring(94, 6);
            color_code = MakeFromInitial(tmpstr);
            if (color_code.Length != 3) { MessageBox.Show("Некорректный Код цвета: " + color_code); return; }
            // Формируем номер двигателя
            tmpstr = initial.Substring(56, 14);
            engine_number = MakeFromInitial(tmpstr);
            if (engine_number.Length != 7) { MessageBox.Show("Некорректный номер двигателя: " + engine_number); return; }
            
            valid = true;
        }

        public string ConvertPair(string pair)
        {
            string first = pair.Substring(0, 1);
            string second = pair.Substring(1, 1);
            switch (first)
            {
                case "4": return ConvertPair4(second);
                case "3": return ConvertPair3(second);
                case "2": return ConvertPair2(second);
                default: return "";
            }
        }

        public string ConvertPair4(string simbol)
        {
            switch (simbol)
            {
                case "4": return "0";
                case "5": return "1";
                case "6": return "2";
                case "7": return "3";
                case "0": return "4";
                case "1": return "5";
                case "2": return "6";
                case "3": return "7";
                case "C": return "8";
                case "D": return "9";
                default: return "";
            }
        }
        public string ConvertPair3(string simbol)
        {
            switch (simbol)
            {
                case "0": return "D";
                case "1": return "E";
                case "2": return "F";
                case "8": return "L";
                default: return "";
            }
        }
        public string ConvertPair2(string simbol)
        {
            switch (simbol)
            {
                case "C": return "X";
                default: return "";
            }
        }

        public string MakeFromInitial(string data)
        {
            ArrayList array = MakePairArray(data);
            if (array == null) return "";
            string result = "";
            int len = array.Count;
            for (int i = len - 1; i >= 0; i--)
            {
                string s = (string)array[i];
                result += ConvertPair(s);
            }
            return result;
        }

        public ArrayList MakePairArray(string str)
        {
            int len = str.Length;
            int rem;
            Math.DivRem(len, 2, out rem);
            if (rem != 0) return null;
            ArrayList arr = new ArrayList();
            for (int i = 0; i < len; i += 2)
            {
                arr.Add(str.Substring(i, 2));
            }
            return arr;
        }
    }
}
