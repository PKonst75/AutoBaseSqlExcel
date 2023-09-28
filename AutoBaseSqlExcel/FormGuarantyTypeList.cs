using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormGuarantyTypeList.
	/// </summary>
	public class FormGuarantyTypeList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Db.ClickType		clickType;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonProperty;
		private DbGuarantyType		guarantyTypeSel;

		public FormGuarantyTypeList(Db.ClickType clickTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType		= clickTypeSrc;
			guarantyTypeSel	= null;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormGuarantyTypeList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
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
			this.columnHeader1.Width = 245;
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
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(32, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 2;
			this.button1.Click += new System.EventHandler(this.button1_Click);
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
			// FormGuarantyTypeList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.button1,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormGuarantyTypeList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Виды гарантии";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление нового вида гарантии
			FormGuarantyType dialog = new FormGuarantyType(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.GuarantyType.LVItem);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Обновление списка видов гарантии
			listView1.Items.Clear();
			DbGuarantyType.FillList(listView1);
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Выбор изменяемого элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbGuarantyType guarantyType = (DbGuarantyType)item.Tag;
			if(guarantyType == null) return;
			// Изменение элемента
			FormGuarantyType dialog = new FormGuarantyType(guarantyType);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.GuarantyType.SetLVItem(item);
		}

		public DbGuarantyType GuarantyType
		{
			get
			{
				return guarantyTypeSel;
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbGuarantyType guarantyType = (DbGuarantyType)item.Tag;
			if(guarantyType == null) return;
			guarantyTypeSel = guarantyType;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
