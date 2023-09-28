
namespace AutoBaseSql
{
    partial class TestDataGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataSetPersonal = new AutoBaseSql.DataSetPersonal();
            this.пЕРСОНАЛBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.пЕРСОНАЛTableAdapter = new AutoBaseSql.DataSetPersonalTableAdapters.ПЕРСОНАЛTableAdapter();
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.лОГИНDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.рАБОТАЕТDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.оКЛАДDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.бОНУСМАСЛАDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fillToolStrip = new System.Windows.Forms.ToolStrip();
            this.fillToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPersonal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.пЕРСОНАЛBindingSource)).BeginInit();
            this.fillToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn,
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn,
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn,
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn,
            this.лОГИНDataGridViewTextBoxColumn,
            this.рАБОТАЕТDataGridViewCheckBoxColumn,
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn,
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn,
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn,
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn,
            this.оКЛАДDataGridViewTextBoxColumn,
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn,
            this.бОНУСМАСЛАDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.пЕРСОНАЛBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(776, 408);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataSetPersonal
            // 
            this.dataSetPersonal.DataSetName = "DataSetPersonal";
            this.dataSetPersonal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // пЕРСОНАЛBindingSource
            // 
            this.пЕРСОНАЛBindingSource.DataMember = "ПЕРСОНАЛ";
            this.пЕРСОНАЛBindingSource.DataSource = this.dataSetPersonal;
            // 
            // пЕРСОНАЛTableAdapter
            // 
            this.пЕРСОНАЛTableAdapter.ClearBeforeFill = true;
            // 
            // кОДПЕРСОНАЛDataGridViewTextBoxColumn
            // 
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn.DataPropertyName = "КОД_ПЕРСОНАЛ";
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn.HeaderText = "КОД_ПЕРСОНАЛ";
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn.Name = "кОДПЕРСОНАЛDataGridViewTextBoxColumn";
            this.кОДПЕРСОНАЛDataGridViewTextBoxColumn.Width = 125;
            // 
            // фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn
            // 
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn.DataPropertyName = "ФАМИЛИЯ_ПЕРСОНАЛ";
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn.HeaderText = "ФАМИЛИЯ_ПЕРСОНАЛ";
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn.Name = "фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn";
            this.фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn.Width = 125;
            // 
            // иМЯПЕРСОНАЛDataGridViewTextBoxColumn
            // 
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn.DataPropertyName = "ИМЯ_ПЕРСОНАЛ";
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn.HeaderText = "ИМЯ_ПЕРСОНАЛ";
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn.Name = "иМЯПЕРСОНАЛDataGridViewTextBoxColumn";
            this.иМЯПЕРСОНАЛDataGridViewTextBoxColumn.Width = 125;
            // 
            // оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn
            // 
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn.DataPropertyName = "ОТЧЕСТВО_ПЕРСОНАЛ";
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn.HeaderText = "ОТЧЕСТВО_ПЕРСОНАЛ";
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn.Name = "оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn";
            this.оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn.Width = 125;
            // 
            // лОГИНDataGridViewTextBoxColumn
            // 
            this.лОГИНDataGridViewTextBoxColumn.DataPropertyName = "ЛОГИН";
            this.лОГИНDataGridViewTextBoxColumn.HeaderText = "ЛОГИН";
            this.лОГИНDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.лОГИНDataGridViewTextBoxColumn.Name = "лОГИНDataGridViewTextBoxColumn";
            this.лОГИНDataGridViewTextBoxColumn.Width = 125;
            // 
            // рАБОТАЕТDataGridViewCheckBoxColumn
            // 
            this.рАБОТАЕТDataGridViewCheckBoxColumn.DataPropertyName = "РАБОТАЕТ";
            this.рАБОТАЕТDataGridViewCheckBoxColumn.HeaderText = "РАБОТАЕТ";
            this.рАБОТАЕТDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.рАБОТАЕТDataGridViewCheckBoxColumn.Name = "рАБОТАЕТDataGridViewCheckBoxColumn";
            this.рАБОТАЕТDataGridViewCheckBoxColumn.Width = 125;
            // 
            // эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn
            // 
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn.DataPropertyName = "ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ";
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn.HeaderText = "ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ";
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn.Name = "эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn";
            this.эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn.Width = 125;
            // 
            // сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn
            // 
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn.DataPropertyName = "ССЫЛКА_КОД_ДОЛЖНОСТЬ";
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn.HeaderText = "ССЫЛКА_КОД_ДОЛЖНОСТЬ";
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn.Name = "сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn";
            this.сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn.Width = 125;
            // 
            // сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn
            // 
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn.DataPropertyName = "ССЫЛКА_КОД_ПОДРАЗДЕЛЕНИЕ";
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn.HeaderText = "ССЫЛКА_КОД_ПОДРАЗДЕЛЕНИЕ";
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn.Name = "сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn";
            this.сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn.Width = 125;
            // 
            // рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn
            // 
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn.DataPropertyName = "РАЗРЯД_КОЭФФИЦИЕНТ";
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn.HeaderText = "РАЗРЯД_КОЭФФИЦИЕНТ";
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn.Name = "рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn";
            this.рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn.Width = 125;
            // 
            // оКЛАДDataGridViewTextBoxColumn
            // 
            this.оКЛАДDataGridViewTextBoxColumn.DataPropertyName = "ОКЛАД";
            this.оКЛАДDataGridViewTextBoxColumn.HeaderText = "ОКЛАД";
            this.оКЛАДDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.оКЛАДDataGridViewTextBoxColumn.Name = "оКЛАДDataGridViewTextBoxColumn";
            this.оКЛАДDataGridViewTextBoxColumn.Width = 125;
            // 
            // бОНУСДЕТАЛИDataGridViewTextBoxColumn
            // 
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn.DataPropertyName = "БОНУС_ДЕТАЛИ";
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn.HeaderText = "БОНУС_ДЕТАЛИ";
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn.Name = "бОНУСДЕТАЛИDataGridViewTextBoxColumn";
            this.бОНУСДЕТАЛИDataGridViewTextBoxColumn.Width = 125;
            // 
            // бОНУСМАСЛАDataGridViewTextBoxColumn
            // 
            this.бОНУСМАСЛАDataGridViewTextBoxColumn.DataPropertyName = "БОНУС_МАСЛА";
            this.бОНУСМАСЛАDataGridViewTextBoxColumn.HeaderText = "БОНУС_МАСЛА";
            this.бОНУСМАСЛАDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.бОНУСМАСЛАDataGridViewTextBoxColumn.Name = "бОНУСМАСЛАDataGridViewTextBoxColumn";
            this.бОНУСМАСЛАDataGridViewTextBoxColumn.Width = 125;
            // 
            // fillToolStrip
            // 
            this.fillToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.fillToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillToolStripButton});
            this.fillToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillToolStrip.Name = "fillToolStrip";
            this.fillToolStrip.Size = new System.Drawing.Size(800, 31);
            this.fillToolStrip.TabIndex = 1;
            this.fillToolStrip.Text = "fillToolStrip";
            // 
            // fillToolStripButton
            // 
            this.fillToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillToolStripButton.Name = "fillToolStripButton";
            this.fillToolStripButton.Size = new System.Drawing.Size(32, 22);
            this.fillToolStripButton.Text = "Fill";
            this.fillToolStripButton.Click += new System.EventHandler(this.fillToolStripButton_Click);
            // 
            // TestDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fillToolStrip);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TestDataGrid";
            this.Text = "TestDataGrid";
            this.Load += new System.EventHandler(this.TestDataGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPersonal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.пЕРСОНАЛBindingSource)).EndInit();
            this.fillToolStrip.ResumeLayout(false);
            this.fillToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSetPersonal dataSetPersonal;
        private System.Windows.Forms.BindingSource пЕРСОНАЛBindingSource;
        private DataSetPersonalTableAdapters.ПЕРСОНАЛTableAdapter пЕРСОНАЛTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn кОДПЕРСОНАЛDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn фАМИЛИЯПЕРСОНАЛDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn иМЯПЕРСОНАЛDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn оТЧЕСТВОПЕРСОНАЛDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn лОГИНDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn рАБОТАЕТDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn эЛЕКТРОННАЯПОДПИСЬПЕРСОНАЛDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn сСЫЛКАКОДДОЛЖНОСТЬDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn сСЫЛКАКОДПОДРАЗДЕЛЕНИЕDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn рАЗРЯДКОЭФФИЦИЕНТDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn оКЛАДDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn бОНУСДЕТАЛИDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn бОНУСМАСЛАDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStrip fillToolStrip;
        private System.Windows.Forms.ToolStripButton fillToolStripButton;
    }
}