using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoFacturaList.
	/// </summary>
	public class FormAutoFacturaList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button buttonProperty;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		private Db.ClickType clickType = Db.ClickType.Properties;
		DbAutoFactura selectedDocument = null;

		public FormAutoFacturaList(Db.ClickType clickTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType	= clickTypeSrc;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoFacturaList));
			this.buttonNew = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonProperty = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 0;
			this.toolTip1.SetToolTip(this.buttonNew, "Новый приходный документ");
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(664, 300);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Документ";
			this.columnHeader1.Width = 90;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Номер";
			this.columnHeader2.Width = 70;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Продавец";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Покупатель";
			this.columnHeader5.Width = 120;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Примечание";
			this.columnHeader6.Width = 100;
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 8);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonUpdate, "Обновить список документов");
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonProperty
			// 
			this.buttonProperty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperty.Image")));
			this.buttonProperty.Location = new System.Drawing.Point(56, 8);
			this.buttonProperty.Name = "buttonProperty";
			this.buttonProperty.Size = new System.Drawing.Size(24, 23);
			this.buttonProperty.TabIndex = 3;
			this.toolTip1.SetToolTip(this.buttonProperty, "Исправить документ");
			this.buttonProperty.Click += new System.EventHandler(this.buttonProperty_Click);
			// 
			// FormAutoFacturaList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(680, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.buttonUpdate,
																		  this.listView1,
																		  this.buttonNew});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoFacturaList";
			this.Text = "Список приходных документов";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Новая фактура
			FormAutoFactura dialog = new FormAutoFactura(null);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoFactura.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление листа
			listView1.Items.Clear();
			DbAutoFactura.FillList(listView1);
		}

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAutoFactura factura = (DbAutoFactura)item.Tag;
			if(factura == null) return;

			if(clickType == Db.ClickType.Properties)
			{
				FormAutoFactura dialog = new FormAutoFactura(factura);
				dialog.ShowDialog(this);
				return;
			}
			if(clickType == Db.ClickType.Select)
			{
				selectedDocument = factura;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога изменения выбранной счет фактуры
			ListViewItem item  = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoFactura factura = (DbAutoFactura)item.Tag;
			if(factura == null) return;

			FormAutoFactura dialog = new FormAutoFactura(factura);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoFactura.SetLVItem(item);
		}

		public DbAutoFactura SelectedDocument
		{
			get
			{
				return selectedDocument;
			}
		}
	}
}
