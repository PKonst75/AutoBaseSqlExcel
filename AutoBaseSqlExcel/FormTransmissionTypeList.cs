using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTransmissionTypeList.
	/// </summary>
	public class FormTransmissionTypeList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonProperty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Db.ClickType			clickType;
		private DbTransmissionType		transmissionTypeSel;

		public FormTransmissionTypeList(Db.ClickType clickTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType			= clickTypeSrc;
			transmissionTypeSel	= null;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTransmissionTypeList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
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
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(280, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Описание";
			this.columnHeader1.Width = 200;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonProperty
			// 
			this.buttonProperty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperty.Image")));
			this.buttonProperty.Location = new System.Drawing.Point(56, 0);
			this.buttonProperty.Name = "buttonProperty";
			this.buttonProperty.Size = new System.Drawing.Size(24, 23);
			this.buttonProperty.TabIndex = 3;
			this.buttonProperty.Click += new System.EventHandler(this.buttonProperty_Click);
			// 
			// FormTransmissionTypeList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormTransmissionTypeList";
			this.Text = "Типы коробок передач";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Новый тип коробки передач
			FormTransmissionType dialog = new FormTransmissionType(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.TransmissionType.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка типов КПП
			listView1.Items.Clear();
			DbTransmissionType.FillList(listView1);
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Изменение типа КПП
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbTransmissionType transmissionType = (DbTransmissionType)item.Tag;
			if(transmissionType == null) return;
			// Изменение элемента
			FormTransmissionType dialog = new FormTransmissionType(transmissionType);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.TransmissionType.SetLVItem(item);
		}

		public DbTransmissionType TransmissionType
		{
			get
			{
				return transmissionTypeSel;
			}
		}
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbTransmissionType transmissionType = (DbTransmissionType)item.Tag;
			if(transmissionType == null) return;
			transmissionTypeSel = transmissionType;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
