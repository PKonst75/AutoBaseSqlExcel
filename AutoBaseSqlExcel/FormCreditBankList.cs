using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCreditBankList.
	/// </summary>
	public class FormCreditBankList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonProperty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbCreditBank selectedCreditBank = null;

		public FormCreditBankList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DbCreditBank.FillList(listView1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCreditBankList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonProperty = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(448, 176);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 417;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonProperty
			// 
			this.buttonProperty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperty.Image")));
			this.buttonProperty.Location = new System.Drawing.Point(32, 8);
			this.buttonProperty.Name = "buttonProperty";
			this.buttonProperty.Size = new System.Drawing.Size(24, 23);
			this.buttonProperty.TabIndex = 2;
			this.buttonProperty.Click += new System.EventHandler(this.buttonProperty_Click);
			// 
			// FormCreditBankList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(464, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.buttonNew,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCreditBankList";
			this.Text = "Список кредитных банков";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление нового кредитного банка
			FormCreditBank dialog = new FormCreditBank(null);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.CreditBank.LVItem);
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Изменение кредитного банка
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCreditBank element = (DbCreditBank)item.Tag;
			if(element == null) return;

			FormCreditBank dialog = new FormCreditBank(element);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.CreditBank.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Изменение кредитного банка
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCreditBank element = (DbCreditBank)item.Tag;
			if(element == null) return;

			selectedCreditBank = element;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbCreditBank SelectedCreditBank
		{
			get
			{
				return selectedCreditBank;
			}
		}
	}
}
