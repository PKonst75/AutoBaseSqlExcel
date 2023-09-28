using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoType.
	/// </summary>
	public class FormAutoType : System.Windows.Forms.Form
	{
		// Элементы формы
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxPrice;
		private System.Windows.Forms.TextBox textBoxPriceGuaranty;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		// Для функциональности формы
		private DbAutoType autoType;
		private System.Windows.Forms.Button buttonTimeWork;
		private System.Windows.Forms.Button button_setguaranty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormAutoType(DbAutoType autoTypeSource)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(autoTypeSource != null)
			{
				// Редактирование старого
				autoType = new DbAutoType(autoTypeSource);
				textBoxName.Text = autoType.Name;
				textBoxPrice.Text = autoType.PriceTxt;
				textBoxPriceGuaranty.Text = autoType.PriceGuarantyTxt;	
			}
			else
			{
				// Новый элемент
				textBoxName.Text = "Наименование";
				textBoxPrice.Text = "0,0";
				textBoxPriceGuaranty.Text = "0,0";
				autoType = new DbAutoType();
			}
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
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxPrice = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxPriceGuaranty = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonTimeWork = new System.Windows.Forms.Button();
			this.button_setguaranty = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Стоимость нормачаса";
			// 
			// textBoxName
			// 
			this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxName.Location = new System.Drawing.Point(168, 16);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(248, 23);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "Наименование";
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxPrice.Location = new System.Drawing.Point(168, 48);
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.TabIndex = 3;
			this.textBoxPrice.Text = "0,0";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(136, 144);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 5;
			this.buttonOk.Text = "Принять";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(224, 144);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Отмена";
			// 
			// textBoxPriceGuaranty
			// 
			this.textBoxPriceGuaranty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxPriceGuaranty.Location = new System.Drawing.Point(168, 80);
			this.textBoxPriceGuaranty.Name = "textBoxPriceGuaranty";
			this.textBoxPriceGuaranty.TabIndex = 4;
			this.textBoxPriceGuaranty.Text = "0,0";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(160, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Гарантийный нормачас";
			// 
			// buttonTimeWork
			// 
			this.buttonTimeWork.Location = new System.Drawing.Point(8, 112);
			this.buttonTimeWork.Name = "buttonTimeWork";
			this.buttonTimeWork.Size = new System.Drawing.Size(160, 23);
			this.buttonTimeWork.TabIndex = 10;
			this.buttonTimeWork.Text = "Время работ";
			this.buttonTimeWork.Click += new System.EventHandler(this.buttonTimeWork_Click);
			// 
			// button_setguaranty
			// 
			this.button_setguaranty.Location = new System.Drawing.Point(280, 80);
			this.button_setguaranty.Name = "button_setguaranty";
			this.button_setguaranty.Size = new System.Drawing.Size(96, 23);
			this.button_setguaranty.TabIndex = 11;
			this.button_setguaranty.Text = "Установить";
			this.button_setguaranty.Click += new System.EventHandler(this.button_setguaranty_Click);
			// 
			// FormAutoType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(464, 175);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_setguaranty,
																		  this.buttonTimeWork,
																		  this.label4,
																		  this.textBoxPriceGuaranty,
																		  this.buttonCancel,
																		  this.buttonOk,
																		  this.textBoxPrice,
																		  this.textBoxName,
																		  this.label3,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAutoType";
			this.Text = "Группы автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Принимаем изменения
			autoType.Name = textBoxName.Text;
			autoType.PriceTxt = textBoxPrice.Text;
			autoType.PriceGuarantyTxt = textBoxPriceGuaranty.Text;

			if(Db.ShowFaults())return;	// Проверка введенных данных
			
			if(autoType.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonTimeWork_Click(object sender, System.EventArgs e)
		{
			if(autoType.Adding == true) return;			// Сначала необходимо завести группу автомобилей
			// Вызов диалога времени работ
			FormTimeWork dialog = new FormTimeWork(autoType);
			dialog.ShowDialog(this);
		}

		private void button_setguaranty_Click(object sender, System.EventArgs e)
		{
			// Установить стоимость гарантийного нормачаса для данной группы автомобилей
			float price = autoType.PriceGuaranty;
			UIYesNoPrompt question = new UIYesNoPrompt("Установить стоимость гарантийного нормачаса " + price.ToString(), "");
			question.ShowDialog();
			if (question.Result == UserInterface.Buttons.Yes)
			{
				// Устанавливаем.
				if (DbSqlWork.SetGuarantyPrice(autoType.Code, price) == true)
					MessageBox.Show("Установлено");
				else
					MessageBox.Show("Ошибка установки");
			}
		}

		public DbAutoType AutoType
		{
			get
			{
				return autoType;
			}
		}

	}
}
