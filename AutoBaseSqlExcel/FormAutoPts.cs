using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public partial class FormAutoPts : Form
    {
        long the_type;
        long code_auto;
        public FormAutoPts(long code, long type)
        {
            InitializeComponent();
            if (type == 0) return;
            if (type == 1)
            {
                // Новый ПТС по коду автомобиля   
                if (code == 0) return;
                DtAuto auto = DbSqlAuto.Find(code);
                if (auto == null) return;    // Ошибка не нашли автомобиль
                textBox_auto.Text = auto.Txt();
                code_auto = code;
                the_type = 1;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (the_type == 1)
            {
                DtAutoPts autoPts = new DtAutoPts();
                autoPts.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ", code_auto);
                autoPts.SetData("СЕРИЯ", textBox_series.Text);
                autoPts.SetData("НОМЕР", textBox_number.Text);
                autoPts.SetData("ВЫДАН_КОГДА", dateTimePicker_date.Value);
                autoPts.SetData("ВЫДАН_КЕМ", textBox_place.Text);

                DtAutoPts theAutoPts = DbSqlAutoPts.Insert(autoPts);
                if (theAutoPts != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                MessageBox.Show("ПТС НЕ ДОБАВЛЕН!");
            }
        }
    }
}
