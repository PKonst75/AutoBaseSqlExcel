using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
    public partial class FormBarCodeLada1 : Form
    {
        
        InputLanguage lang = null;
        InputLanguage lang_eng = null;
        public SD_BarCodeLada_1 barcode = null;
        public FormBarCodeLada1()
        {
            InitializeComponent();
            lang = InputLanguage.CurrentInputLanguage; // Получаем текущий язык ввода
           
            // Устанавливаем язык ввода на английский
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage l in coll)
            {
                if (l.Culture.ToString() == "en-US") lang_eng = l;
            }
            if (lang_eng != null) InputLanguage.CurrentInputLanguage = lang_eng;
            else
            {
                MessageBox.Show("Отсутствует язык ввода");
                this.Close();
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            // Обработка текста из поля ввода
            if (lang != null) InputLanguage.CurrentInputLanguage = lang;    // Возвращаем обратно язык ввода
            string initial= textBox_barcode.Text;
            ArrayList part = SplitString(initial);
            ArrayList result = ConvertHexAscii(part);
           // foreach (string s in result)
           // {
           //     MessageBox.Show(s);
           // }
            if (result.Count < 21)
            {
                MessageBox.Show("Неверный формат штрих-кода", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }
            barcode = new SD_BarCodeLada_1(result);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        string TranslateRusEng(string input_string)
        {
            return "";
        }

        ArrayList SplitString(string input_string)
        {
            ArrayList array;
            array = new ArrayList(input_string.Split('|'));
            return array;
        }
        ArrayList ConvertHexAscii(ArrayList input_array)
        {
            ArrayList array = new ArrayList();
            foreach (string o in input_array)
            {
                string s = o;
                s = s.Trim();
                int len = s.Length;
                string result = "";
                for (int i = 0; i < len; i=i+2)
                {
                    string pair = "";
                    if (i+1 < len)
                        pair = s.Substring(i, 2);
                    int asc = HexAscii(pair);
                    string str = AsciiStr(asc);
                    result = result + str;
                }
                array.Add(result);
            }
            return array;
        }
        int HexAscii(string hex)
        {
            // Превращаем пару сиволов обозначающих hex в число равное Ascii символу
            if (hex.Length < 2) return 0; // Ошибочный символ
            string s1 = hex.Substring(0, 1);
            string s2 = hex.Substring(1, 1);

            int i1 = HexInt(s1); if (i1 == 16) return 0;
            int i2 = HexInt(s2); if (i2 == 16) return 0;

            int ascii = i1 * 16 + i2;
            return ascii;
        }

        int HexInt(string hex_part)
        {
            hex_part = hex_part.ToUpper();
            switch(hex_part){
                case "0": return 0;
                case "1": return 1;
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "A": return 10;
                case "B": return 11;
                case "C": return 12;
                case "D": return 13;
                case "E": return 14;
                case "F": return 15;
                default: return 16;
            }
        }
        string AsciiStr(int ascii)
        {
            switch (ascii)
            {
                case 32: return " ";
                case 48: return "0";
                case 49: return "1";
                case 50: return "2";
                case 51: return "3";
                case 52: return "4";
                case 53: return "5";
                case 54: return "6";
                case 55: return "7";
                case 56: return "8";
                case 57: return "9";
                case 65: return "A";
                case 66: return "B";
                case 67: return "C";
                case 68: return "D";
                case 69: return "E";
                case 70: return "F";
                case 71: return "G";
                case 72: return "H";
                case 73: return "I";
                case 74: return "J";
                case 75: return "K";
                case 76: return "L";
                case 77: return "M";
                case 78: return "N";
                case 79: return "O";
                case 80: return "P";
                case 81: return "Q";
                case 82: return "R";
                case 83: return "S";
                case 84: return "T";
                case 85: return "U";
                case 86: return "V";
                case 87: return "W";
                case 88: return "X";
                case 89: return "Y";
                case 90: return "Z";
                case 91: return "[";
                case 92: return "\\";
                case 93: return "]";
                case 128: return "А";
                case 129: return "Б";
                case 130: return "В";
                case 131: return "Г";
                case 132: return "Д";
                case 133: return "Е";
                case 134: return "Ж";
                case 135: return "З";
                case 136: return "И";
                case 137: return "Й";
                case 138: return "К";
                case 139: return "Л";
                case 140: return "М";
                case 141: return "Н";
                case 142: return "О";
                case 143: return "П";
                case 144: return "Р";
                case 145: return "С";
                case 146: return "Т";
                case 147: return "У";
                case 148: return "Ф";
                case 149: return "Х";
                case 150: return "Ц";
                case 151: return "Ч";
                case 152: return "Ш";
                case 153: return "Щ";
                case 154: return "Ъ";
                case 155: return "Ы";
                case 156: return "Ь";
                case 157: return "Э";
                case 158: return "Ю";
                case 159: return "Я";
                default: return "#";
            }
        }
  
    }
}
