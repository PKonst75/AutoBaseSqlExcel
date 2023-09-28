using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public partial class TestDataGrid : Form
    {
        public TestDataGrid()
        {
            InitializeComponent();
        }

        private void dataSetPersonal1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void TestDataGrid_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetPersonal.ПЕРСОНАЛ". При необходимости она может быть перемещена или удалена.
            this.пЕРСОНАЛTableAdapter.Fill(this.dataSetPersonal.ПЕРСОНАЛ);

        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.пЕРСОНАЛTableAdapter.Fill(this.dataSetPersonal.ПЕРСОНАЛ);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
