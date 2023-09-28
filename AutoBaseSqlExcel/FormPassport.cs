using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public partial class FormPassport : Form
    {
        long the_type;
        long code_partner;
        public FormPassport(long code, long type)
        {
            InitializeComponent();
            if (type == 0) return;
            if (type == 1)
            {
                // Новый паспорт по коду контрагента
                if (code == 0) return;
                DtPartner partner = DbSqlPartner.Find(code);
                if (partner == null) return;    // Ошибка не нашли контрагента
                if ((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == true) return;  // Ошибка у юрлиц нет паспортов
                textBox_partner.Text = partner.GetTitle();
                code_partner = code;
                the_type = 1;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            // Сохранить введенные данные
            if (the_type == 1)
            {
                DtPassport passport = new DtPassport();
                passport.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ", code_partner);
                passport.SetData("СЕРИЯ", textBox_series.Text);
                passport.SetData("НОМЕР", textBox_number.Text);
                passport.SetData("ВЫДАН_КОГДА", dateTimePicker_date.Value);
                passport.SetData("ВЫДАН_КЕМ", textBox_place.Text);

                DtPassport thePassport = DbSqlPassport.Insert(passport);
                if (thePassport != null)
                {
                    this.Close();
                    return;
                }
                MessageBox.Show("ПАССПОРТ НЕ ДОБАВЛЕН!");
            }
        }
    }
}
