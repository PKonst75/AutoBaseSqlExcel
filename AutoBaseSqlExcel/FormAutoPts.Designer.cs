namespace AutoBaseSql
{
    partial class FormAutoPts
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
            this.textBox_place = new System.Windows.Forms.TextBox();
            this.textBox_series = new System.Windows.Forms.TextBox();
            this.textBox_number = new System.Windows.Forms.TextBox();
            this.textBox_auto = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Серия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Номер";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Выдан когда";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Выдан кем";
            // 
            // dateTimePicker_date
            // 
            this.dateTimePicker_date.Location = new System.Drawing.Point(49, 75);
            this.dateTimePicker_date.Name = "dateTimePicker_date";
            this.dateTimePicker_date.Size = new System.Drawing.Size(143, 20);
            this.dateTimePicker_date.TabIndex = 4;
            // 
            // textBox_place
            // 
            this.textBox_place.Location = new System.Drawing.Point(198, 75);
            this.textBox_place.Name = "textBox_place";
            this.textBox_place.Size = new System.Drawing.Size(418, 20);
            this.textBox_place.TabIndex = 5;
            // 
            // textBox_series
            // 
            this.textBox_series.Location = new System.Drawing.Point(73, 46);
            this.textBox_series.Name = "textBox_series";
            this.textBox_series.Size = new System.Drawing.Size(76, 20);
            this.textBox_series.TabIndex = 6;
            // 
            // textBox_number
            // 
            this.textBox_number.Location = new System.Drawing.Point(226, 46);
            this.textBox_number.Name = "textBox_number";
            this.textBox_number.Size = new System.Drawing.Size(139, 20);
            this.textBox_number.TabIndex = 7;
            // 
            // textBox_auto
            // 
            this.textBox_auto.Location = new System.Drawing.Point(26, 12);
            this.textBox_auto.Name = "textBox_auto";
            this.textBox_auto.ReadOnly = true;
            this.textBox_auto.Size = new System.Drawing.Size(590, 20);
            this.textBox_auto.TabIndex = 8;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(541, 108);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 9;
            this.button_ok.Text = "Сохранить";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // FormAutoPts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 143);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_auto);
            this.Controls.Add(this.textBox_number);
            this.Controls.Add(this.textBox_series);
            this.Controls.Add(this.textBox_place);
            this.Controls.Add(this.dateTimePicker_date);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormAutoPts";
            this.Text = "ПАСПОРТ ТРАНСПОРТНОГО СРЕДСТВА (ПТС)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker_date;
        private System.Windows.Forms.TextBox textBox_place;
        private System.Windows.Forms.TextBox textBox_series;
        private System.Windows.Forms.TextBox textBox_number;
        private System.Windows.Forms.TextBox textBox_auto;
        private System.Windows.Forms.Button button_ok;
    }
}