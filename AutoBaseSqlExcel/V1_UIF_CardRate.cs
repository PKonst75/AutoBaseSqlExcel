using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for V1_UIF_CardRate.
	/// </summary>
	public class V1_UIF_CardRate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox_rate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.CheckBox checkBox_change;
		private System.Windows.Forms.Button button_ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_call_comment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox_call_rate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button_save_call;
		V1_DtCardRate rate = null;
		private System.Windows.Forms.DateTimePicker dateTimePicker_call_date;
		private System.Windows.Forms.CheckBox checkBox_work_done;
		V1_DtCardRateCall call_rate = null;

		public V1_UIF_CardRate(long card_number, int card_year)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			rate = new V1_DtCardRate();
			rate.card_number	= card_number;
			rate.card_year		= card_year;

			call_rate = new V1_DtCardRateCall();
			call_rate.card_number	= card_number;
			call_rate.card_year		= card_year;

			comboBox_rate.Items.Add("НЕ ВЫБРАНО");	// 0
			comboBox_rate.Items.Add("ДА, КОНЕЧНО"); // 1
			comboBox_rate.Items.Add("МОЖЕТ БЫТЬ");	// 2
			comboBox_rate.Items.Add("ТОЧНО НЕТ");	// 3

			comboBox_call_rate.Items.Add("НЕ ВЫБРАНО");						// 0
			comboBox_call_rate.Items.Add("ОТЛИЧНО");						// 1
			comboBox_call_rate.Items.Add("ХОРОШО");							// 2
			comboBox_call_rate.Items.Add("МОГЛО БЫТЬ ЛУЧШЕ");				// 3
			comboBox_call_rate.Items.Add("УДОВЛЕТВОРИТЕЛЬНО");				// 4
			comboBox_call_rate.Items.Add("ПЛОХО");							// 5
			comboBox_call_rate.Items.Add("УЖАСНО. БОЛЬШЕ НЕ ПРИЕДУ.");		// 6
			comboBox_call_rate.Items.Add("ВЕЧНО НЕДОВОЛЕН. НЕ УГОДИТЬ.");	// 7
			comboBox_call_rate.Items.Add("ЗАТРУДНИТЕЛЬНО ОЦЕНИТЬ");			// 8
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox_rate = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.checkBox_change = new System.Windows.Forms.CheckBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dateTimePicker_call_date = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBox_work_done = new System.Windows.Forms.CheckBox();
			this.textBox_call_comment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox_call_rate = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button_save_call = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Рекомендация клиента";
			// 
			// comboBox_rate
			// 
			this.comboBox_rate.Location = new System.Drawing.Point(192, 24);
			this.comboBox_rate.Name = "comboBox_rate";
			this.comboBox_rate.Size = new System.Drawing.Size(224, 28);
			this.comboBox_rate.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Примечание";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(192, 72);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(520, 26);
			this.textBox_comment.TabIndex = 3;
			this.textBox_comment.Text = "";
			// 
			// checkBox_change
			// 
			this.checkBox_change.Location = new System.Drawing.Point(8, 120);
			this.checkBox_change.Name = "checkBox_change";
			this.checkBox_change.Size = new System.Drawing.Size(584, 24);
			this.checkBox_change.TabIndex = 6;
			this.checkBox_change.Text = "Удалось переубедить (если первоначальная рекомендация негативная)";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(600, 120);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(112, 23);
			this.button_ok.TabIndex = 7;
			this.button_ok.Text = "Сохранить";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.comboBox_rate,
																					this.checkBox_change,
																					this.textBox_comment,
																					this.button_ok,
																					this.label2,
																					this.label1});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(720, 160);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Выходная анкета";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_save_call,
																					this.label5,
																					this.comboBox_call_rate,
																					this.label4,
																					this.textBox_call_comment,
																					this.checkBox_work_done,
																					this.label3,
																					this.dateTimePicker_call_date});
			this.groupBox2.Location = new System.Drawing.Point(8, 176);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(720, 152);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Обзвон";
			this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
			// 
			// dateTimePicker_call_date
			// 
			this.dateTimePicker_call_date.Location = new System.Drawing.Point(16, 24);
			this.dateTimePicker_call_date.Name = "dateTimePicker_call_date";
			this.dateTimePicker_call_date.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(224, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 24);
			this.label3.TabIndex = 1;
			this.label3.Text = "Дата звонка";
			// 
			// checkBox_work_done
			// 
			this.checkBox_work_done.Location = new System.Drawing.Point(456, 24);
			this.checkBox_work_done.Name = "checkBox_work_done";
			this.checkBox_work_done.Size = new System.Drawing.Size(248, 24);
			this.checkBox_work_done.TabIndex = 2;
			this.checkBox_work_done.Text = "Неисправность устранена?";
			// 
			// textBox_call_comment
			// 
			this.textBox_call_comment.Location = new System.Drawing.Point(200, 64);
			this.textBox_call_comment.Name = "textBox_call_comment";
			this.textBox_call_comment.Size = new System.Drawing.Size(504, 26);
			this.textBox_call_comment.TabIndex = 3;
			this.textBox_call_comment.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Примечание";
			// 
			// comboBox_call_rate
			// 
			this.comboBox_call_rate.Location = new System.Drawing.Point(200, 104);
			this.comboBox_call_rate.Name = "comboBox_call_rate";
			this.comboBox_call_rate.Size = new System.Drawing.Size(184, 28);
			this.comboBox_call_rate.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Общая оценка";
			// 
			// button_save_call
			// 
			this.button_save_call.Location = new System.Drawing.Point(600, 120);
			this.button_save_call.Name = "button_save_call";
			this.button_save_call.Size = new System.Drawing.Size(104, 23);
			this.button_save_call.TabIndex = 7;
			this.button_save_call.Text = "Сохранить";
			this.button_save_call.Click += new System.EventHandler(this.button_save_call_Click);
			// 
			// V1_UIF_CardRate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(736, 335);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "V1_UIF_CardRate";
			this.Text = "Оценка карточки";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// Сохраняем данные
			rate.rate			= (short)comboBox_rate.SelectedIndex;
			rate.comment		= textBox_comment.Text;
			rate.rate_change	= checkBox_change.Checked;

			if (rate.rate == 0) return;		// Не выбрана рекомендация
			if (V1_DbSqlCardRate.Insert(rate) == false) return;
			MessageBox.Show("Рекомендация добавлена");
			this.Close();
		}

		private void button_save_call_Click(object sender, System.EventArgs e)
		{
			// Сохраняем данные по контрольному звонку
			call_rate.rate			= (short)comboBox_call_rate.SelectedIndex;
			call_rate.comment		= textBox_call_comment.Text;
			call_rate.work_done		= checkBox_work_done.Checked;
			call_rate.call_date		= dateTimePicker_call_date.Value;

			if (call_rate.rate == 0) return;		// Не выбрана рекомендация
			if (V1_DbSqlCardRate.InsertCall(call_rate) == false) return;
			MessageBox.Show("Звонок добавлен");
			this.Close();
		}

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
		
		}
	}
}
