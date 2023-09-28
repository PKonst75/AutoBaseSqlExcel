using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardSimple.
	/// </summary>
	public class FormCardSimple : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxPartnerName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxPartnerAddress;
		private System.Windows.Forms.TextBox textBoxPartnerPhone;
		private System.Windows.Forms.Button buttonSelectPartner;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormCardSimple()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxPartnerName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxPartnerAddress = new System.Windows.Forms.TextBox();
			this.textBoxPartnerPhone = new System.Windows.Forms.TextBox();
			this.buttonSelectPartner = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBoxPartnerPhone,
																					this.textBoxPartnerAddress,
																					this.label3,
																					this.label2,
																					this.label1,
																					this.textBoxPartnerName,
																					this.buttonSelectPartner});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(744, 104);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Владелец";
			// 
			// textBoxPartnerName
			// 
			this.textBoxPartnerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPartnerName.Location = new System.Drawing.Point(152, 24);
			this.textBoxPartnerName.Name = "textBoxPartnerName";
			this.textBoxPartnerName.Size = new System.Drawing.Size(560, 23);
			this.textBoxPartnerName.TabIndex = 0;
			this.textBoxPartnerName.Text = "";
			this.textBoxPartnerName.KeyDown += new KeyEventHandler(this.textBoxPartnerName_KeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "ФИО/Наименование";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Адрес";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.TabIndex = 3;
			this.label3.Text = "Телефоны";
			// 
			// textBoxPartnerAddress
			// 
			this.textBoxPartnerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPartnerAddress.Location = new System.Drawing.Point(152, 48);
			this.textBoxPartnerAddress.Name = "textBoxPartnerAddress";
			this.textBoxPartnerAddress.ReadOnly = true;
			this.textBoxPartnerAddress.Size = new System.Drawing.Size(560, 23);
			this.textBoxPartnerAddress.TabIndex = 4;
			this.textBoxPartnerAddress.Text = "";
			// 
			// textBoxPartnerPhone
			// 
			this.textBoxPartnerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPartnerPhone.Location = new System.Drawing.Point(152, 72);
			this.textBoxPartnerPhone.Name = "textBoxPartnerPhone";
			this.textBoxPartnerPhone.ReadOnly = true;
			this.textBoxPartnerPhone.Size = new System.Drawing.Size(560, 23);
			this.textBoxPartnerPhone.TabIndex = 5;
			this.textBoxPartnerPhone.Text = "";
			// 
			// buttonSelectPartner
			// 
			this.buttonSelectPartner.Location = new System.Drawing.Point(712, 24);
			this.buttonSelectPartner.Name = "buttonSelectPartner";
			this.buttonSelectPartner.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectPartner.TabIndex = 1;
			this.buttonSelectPartner.Text = "...";
			this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
			// 
			// FormCardSimple
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(760, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCardSimple";
			this.Text = "Карточка заказа";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Выбор владельца автомобиля
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			// Заполнение полей
			textBoxPartnerName.Text		= dialog.Partner.TitleTxt;
			textBoxPartnerAddress.Text	= dialog.Partner.AddressTxt;
			textBoxPartnerPhone.Text	= dialog.Partner.PhoneTxt;
		}


		#region Обработчики стандартный событий
		private void textBoxPartnerName_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Инициализация поиска контрагента по наименованию
				string pattern	= textBoxPartnerName.Text;
				pattern			= "%" + pattern + "%";
				ArrayList array = new ArrayList();
				DbPartner.FillArray(array, false, 3, pattern);
				if(array.Count == 1)
				{
					// В точности один контрагент попадающий под условия поиска
					DbPartner partner = (DbPartner)array[0];
					textBoxPartnerName.Text		= partner.TitleTxt;
					textBoxPartnerAddress.Text	= partner.AddressTxt;
					textBoxPartnerPhone.Text	= partner.PhoneTxt;
					return;
				}
				if(array.Count > 1)
				{
					// Инициация выбора из существующего массива
					FormSelectElement dialog = new FormSelectElement(array);
					dialog.ShowDialog();
					if(dialog.DialogResult != DialogResult.OK)
					{
						textBoxPartnerName.Text		= "";
						textBoxPartnerAddress.Text	= "";
						textBoxPartnerPhone.Text	= "";
						return;
					}
					if(dialog.SelectedElement == null)
					{
						array.Clear();	// Подготовка к введению нового контрагента
					}
					else
					{
						// Выбор сделан
						DbPartner partner = (DbPartner)dialog.SelectedElement;
						textBoxPartnerName.Text		= partner.TitleTxt;
						textBoxPartnerAddress.Text	= partner.AddressTxt;
						textBoxPartnerPhone.Text	= partner.PhoneTxt;
						return;
					}

				}
				if(array.Count == 0)
				{
					// Нужно вводить нового контрагента
					// Запрос введения физического лица
					bool	juridical;
					DialogResult res = MessageBox.Show(this, "Ввод нового физического лица?", "Запрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if(res == DialogResult.Cancel)
					{
						textBoxPartnerName.Text		= "";
						textBoxPartnerAddress.Text	= "";
						textBoxPartnerPhone.Text	= "";
						return;
					}
					if(res == DialogResult.Yes)
						juridical	= false;
					else
						juridical	= true;	
				}
			}
		}
		#endregion
	}
}
