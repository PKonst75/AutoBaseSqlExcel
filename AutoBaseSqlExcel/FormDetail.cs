using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetail.
	/// </summary>
	public class FormDetail : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxCode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkBoxOil;
		private System.Windows.Forms.TextBox textBoxUsageShort;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxUsage;
		private System.Windows.Forms.Label label5;
		DbDetail detail;

		private	bool ourChange						= false;

		public FormDetail(DbDetail source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(source == null)
			{
				detail = new DbDetail();
			}
			else
			{
				textBoxCode.Enabled = false;
				detail = new DbDetail(source);
			}
			textBoxCode.Text = detail.CodeTxt;
			textBoxName.Text = detail.Name;
			textBoxComment.Text = detail.Comment;
			checkBoxOil.Checked	= detail.Oil;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxUsage = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxUsageShort = new System.Windows.Forms.TextBox();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCode = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.checkBoxOil = new System.Windows.Forms.CheckBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Location = new System.Drawing.Point(8, 16);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(536, 280);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label5,
																				   this.textBoxUsage,
																				   this.label4,
																				   this.textBoxUsageShort,
																				   this.textBoxComment,
																				   this.label3,
																				   this.textBoxName,
																				   this.label2,
																				   this.label1,
																				   this.textBoxCode});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(528, 251);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Свойства";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(136, 23);
			this.label5.TabIndex = 9;
			this.label5.Text = "Применимость";
			// 
			// textBoxUsage
			// 
			this.textBoxUsage.Location = new System.Drawing.Point(8, 216);
			this.textBoxUsage.Name = "textBoxUsage";
			this.textBoxUsage.Size = new System.Drawing.Size(416, 23);
			this.textBoxUsage.TabIndex = 8;
			this.textBoxUsage.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(168, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Применимость коротко";
			// 
			// textBoxUsageShort
			// 
			this.textBoxUsageShort.Location = new System.Drawing.Point(8, 168);
			this.textBoxUsageShort.Name = "textBoxUsageShort";
			this.textBoxUsageShort.Size = new System.Drawing.Size(416, 23);
			this.textBoxUsageShort.TabIndex = 6;
			this.textBoxUsageShort.Text = "";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(8, 112);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(416, 23);
			this.textBoxComment.TabIndex = 5;
			this.textBoxComment.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Примечание";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(8, 56);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(416, 23);
			this.textBoxName.TabIndex = 3;
			this.textBoxName.Text = "textBox1";
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Наименование";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Код";
			// 
			// textBoxCode
			// 
			this.textBoxCode.Location = new System.Drawing.Point(128, 8);
			this.textBoxCode.Name = "textBoxCode";
			this.textBoxCode.Size = new System.Drawing.Size(200, 23);
			this.textBoxCode.TabIndex = 0;
			this.textBoxCode.Text = "textBox1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.checkBoxOil});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(528, 254);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Дополнительно";
			// 
			// checkBoxOil
			// 
			this.checkBoxOil.Location = new System.Drawing.Point(16, 16);
			this.checkBoxOil.Name = "checkBoxOil";
			this.checkBoxOil.TabIndex = 0;
			this.checkBoxOil.Text = "Масла";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(232, 304);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormDetail
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(560, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormDetail";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Деталь";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Добавляем в базу элемент, или меняем его
			detail.CodeTxt		= textBoxCode.Text;
			detail.Name			= textBoxName.Text;
			detail.Comment		= textBoxComment.Text;
			detail.Oil			= checkBoxOil.Checked;
			detail.Usage		= textBoxUsage.Text;
			detail.UsageShort	= textBoxUsageShort.Text;
			if(Db.ShowFaults()) return;

			detail.Write();
			if(Db.ShowFaults()) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		private void textBoxName_TextChanged(object sender, System.EventArgs e)
		{
			// Постоянный контроль вводимого текста
			if (ourChange == true)
			{
				ourChange = false;
				return;
			}
			// Первым делом ставим заглавной первую букву
			string tmpText = Db.FirstUpperSpace(textBoxName.Text);
			if(tmpText != textBoxName.Text)
			{
				// Если есть изменения - производим их
				ourChange = true;
				int selectionStart = textBoxName.SelectionStart;
				textBoxName.Text = tmpText;
				if(selectionStart > 0)
					textBoxName.SelectionStart = selectionStart;
			}
		}

		public DbDetail Detail
		{
			get{ return detail;}
		}
	}
}
