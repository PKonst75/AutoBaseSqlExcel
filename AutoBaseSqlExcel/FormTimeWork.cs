using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTimeWork.
	/// </summary>
	public class FormTimeWork : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TextBox textBoxAutoType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonOk;

		DbAutoType		autoType;
		DbTimeWork		timeWork;

		public FormTimeWork(DbAutoType autoTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполнение списка работ
			listView1.Items.Add("ТО-1");
			listView1.Items.Add("ТО-2");
			listView1.Items.Add("ТО-3");
			listView1.Items.Add("ТО-4");
			listView1.Items.Add("ТО-5");
			listView1.Items.Add("ТО-6");
			listView1.Items.Add("Сход/Развал");
			listView1.Items.Add("Диагностика    ДВС");
			listView1.Items.Add("Диагностика    ЭСУД");
			listView1.Items.Add("Диагностика    ходовая часть");
			listView1.Items.Add("Диагностика    рулевое управление");
			listView1.Items.Add("Диагностика    тормозная система");
			listView1.Items.Add("Неисправность    ДВС");
			listView1.Items.Add("Неисправность    ЭСУД");
			listView1.Items.Add("Неисправность    ходовая часть");
			listView1.Items.Add("Неисправность    рулевое управление");
			listView1.Items.Add("Неисправность    тормозная система");
			listView1.Items.Add("Неисправность    система охлаждения");
			listView1.Items.Add("Неисправность    система смазки");
			listView1.Items.Add("Неисправность    электрооборудование");
			listView1.Items.Add("Неисправность    кузов");
			listView1.Items.Add("Неисправность    система отопления и кондиционирования");

			// Тип автомобиля
			autoType		= autoTypeSrc;
			textBoxAutoType.Text	= autoType.NameTxt;

			// Попытка прочесть данные о работах
			timeWork		= DbTimeWork.Read(autoType);
			if(timeWork		== null)
			{
				return;
			}
			timeWork.FillList(listView1);
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.textBoxAutoType = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 327;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Врямя (час.)";
			this.columnHeader3.Width = 137;
			// 
			// textBoxAutoType
			// 
			this.textBoxAutoType.Location = new System.Drawing.Point(8, 0);
			this.textBoxAutoType.Name = "textBoxAutoType";
			this.textBoxAutoType.ReadOnly = true;
			this.textBoxAutoType.Size = new System.Drawing.Size(328, 23);
			this.textBoxAutoType.TabIndex = 1;
			this.textBoxAutoType.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.buttonOk.Location = new System.Drawing.Point(208, 264);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormTimeWork
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(512, 293);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxAutoType,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormTimeWork";
			this.Text = "Примерное время исполнения работ";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Сохранение полученных результатов
			if(timeWork.Write() == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			int index;
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			index = item.Index;
			if (index >= 22) return;		// Выход за пределы рабочей области

			FormSelectString dialog = new FormSelectString("Значение времени проведения " + item.Text, timeWork.TimeTxt(index));
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			timeWork.SetTime(index, dialog.SelectedFloat);
			timeWork.SetList(listView1, index);
		}
	}
}
