using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormPartnerList.
	/// </summary>
	public class FormPartnerList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.RadioButton radioButtonJur;
		private System.Windows.Forms.RadioButton radioButtonNotJur;
		private System.Windows.Forms.Button buttonChange;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private DbPartner partner = null;
		private System.Windows.Forms.Button buttonAutoList;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonChangePartner;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.ComponentModel.Container components = null;

		DbPartner contextMenuElement	= null;

		public FormPartnerList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// ����������� �������� �� ���������
			radioButtonNotJur.Checked = true;
			radioButtonJur.Checked = false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPartnerList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.radioButtonJur = new System.Windows.Forms.RadioButton();
			this.radioButtonNotJur = new System.Windows.Forms.RadioButton();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonAutoList = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonChangePartner = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 56);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(624, 280);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "������������";
			this.columnHeader1.Width = 180;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "�������";
			this.columnHeader2.Width = 180;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "����������";
			this.columnHeader3.Width = 120;
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
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 8);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// radioButtonJur
			// 
			this.radioButtonJur.Location = new System.Drawing.Point(256, 0);
			this.radioButtonJur.Name = "radioButtonJur";
			this.radioButtonJur.Size = new System.Drawing.Size(128, 24);
			this.radioButtonJur.TabIndex = 3;
			this.radioButtonJur.Text = "����������� ����";
			// 
			// radioButtonNotJur
			// 
			this.radioButtonNotJur.Location = new System.Drawing.Point(256, 24);
			this.radioButtonNotJur.Name = "radioButtonNotJur";
			this.radioButtonNotJur.Size = new System.Drawing.Size(128, 24);
			this.radioButtonNotJur.TabIndex = 4;
			this.radioButtonNotJur.Text = "���������� ����";
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 8);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 5;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonAutoList
			// 
			this.buttonAutoList.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAutoList.Image")));
			this.buttonAutoList.Location = new System.Drawing.Point(112, 8);
			this.buttonAutoList.Name = "buttonAutoList";
			this.buttonAutoList.Size = new System.Drawing.Size(24, 23);
			this.buttonAutoList.TabIndex = 6;
			this.buttonAutoList.Click += new System.EventHandler(this.buttonAutoList_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(560, 8);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 7;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonChangePartner
			// 
			this.buttonChangePartner.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChangePartner.Image")));
			this.buttonChangePartner.Location = new System.Drawing.Point(584, 8);
			this.buttonChangePartner.Name = "buttonChangePartner";
			this.buttonChangePartner.Size = new System.Drawing.Size(24, 23);
			this.buttonChangePartner.TabIndex = 8;
			this.buttonChangePartner.Click += new System.EventHandler(this.buttonChangePartner_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "������� �����������";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem2.Text = "��������";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "��������� �����������";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "���������� ���������� �����������";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// FormPartnerList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonChangePartner,
																		  this.buttonDelete,
																		  this.buttonAutoList,
																		  this.buttonChange,
																		  this.radioButtonNotJur,
																		  this.radioButtonJur,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormPartnerList";
			this.Text = "�����������";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			bool isJuridical;
			if(radioButtonJur.Checked == true) isJuridical = true;
			else isJuridical = false;
			// ����� ����������
			FormPartner dialog = new FormPartner(null, isJuridical);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Partner.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// ��������� ����
			bool isJuridical;
			listView1.Items.Clear();
			if(radioButtonJur.Checked == true) isJuridical = true;
			else isJuridical = false;
			DbPartner.FillList(listView1, isJuridical, 0, "");
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// ��������� ���������� ��������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbPartner partner = (DbPartner)item.Tag;
			if(partner == null) return;
			// ��������� �����������
			FormPartner dialog = new FormPartner(partner, false);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Partner.SetLVItem(item);
		}

		public DbPartner Partner
		{
			get
			{
				return partner;
			}
		}

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			partner = (DbPartner)item.Tag;
			if(partner == null) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		protected void listView1_MouseUp(object sender, MouseEventArgs e)
		{
			contextMenuElement = null;

			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			partner = (DbPartner)item.Tag;
			if(partner == null) return;

			if(e.Button == MouseButtons.Right)
			{
				contextMenuElement = partner;
				Point pos = new Point(e.X, e.Y);
				contextMenu1.Show(listView1, pos);
			}
		}

		private void buttonAutoList_Click(object sender, System.EventArgs e)
		{
			// ���������� ������� ����������� �����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			partner = (DbPartner)item.Tag;
			if(partner == null) return;
			FormAutoList dialog = new FormAutoList(Db.ClickType.Properties, partner);
			dialog.ShowDialog(this);
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			string mask = "";
			bool isJuridical;
			FormSelectString dialog = null;
			if(radioButtonJur.Checked == true) isJuridical = true;
			else isJuridical = false;

			switch(e.Column)
			{
				case 0:
					// ������ �� ������� � ������� �������������
					dialog = new FormSelectString("������� ������������", "������� ������������ ��� ������");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					DbPartner.FillList(listView1, isJuridical, 1, mask);
					return;
			}
		}

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			FormInfo form		= null;
			DbPartner element	= null;

			if(e.Control == true)
			{
				// ��������� ������ ������� � Ctrl
				if(e.KeyCode == Keys.I)
				{
					// ������ ������� I - ����� ���������� �� ��������� ��������
					foreach(ListViewItem item in listView1.SelectedItems)
					{
						if(item != null)
						{
							element = (DbPartner)item.Tag;
							if(element != null)
							{
								if(form == null) form = new FormInfo(element, 0);
								else form.InsertInfo(element);
							}
						}
					}
					if (form != null) form.Show();
				}
			}
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// �������� ����������� �� ������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			partner = (DbPartner)item.Tag;
			if(partner == null) return;
			if(partner.Delete() == false) return;
			listView1.Items.Remove(item);
		}

		private void buttonChangePartner_Click(object sender, System.EventArgs e)
		{
			// ������ ������ ����������� �� �������
			// ����������� ����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbPartner partner = (DbPartner)item.Tag;
			if(partner == null) return;

			// ������ ������ ���������
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			partner.Replace(dialog.Partner);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// �������� ������� ��������� �����������
			contextMenuElement.WriteFucntion(DbPartner.PartnerFunction.SellerAuto, true);
			contextMenuElement = null;
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// �������� ������� ����������� ���������� �����������
			contextMenuElement.WriteFucntion(DbPartner.PartnerFunction.InnerCustomerAuto, true);
			contextMenuElement = null;
		}
	}
}
