using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public partial class FormBarCodeCN1 : Form
    {
        InputLanguage lang = null;
        InputLanguage lang_eng = null;
        public SD_BarCodeCN_1 barcode = null;
            
        public FormBarCodeCN1()
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

            // Обработка ввода - первичные проверки
            string barcode_string = textBox_barcode.Text;
            barcode_string = barcode_string.Trim();
            if (barcode_string.Length == 0) { this.DialogResult = DialogResult.Cancel; this.Close(); return; }

            barcode = new SD_BarCodeCN_1(barcode_string);
            if (barcode.valid == false) { this.DialogResult = DialogResult.Cancel; this.Close(); return; }

            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
