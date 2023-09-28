using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageClaim.
	/// </summary>
	public class FormManageClaim : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TextBox textBox_new_claim;
		private System.Windows.Forms.Button button_new_claim;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//		FormCard	outer_form	= null;
		private readonly Form	outer_form	= null; // ������� �����, � ������� ����� ��������� 
		DtCard card		= null;
		private System.Windows.Forms.Button button_remove;
		ListView	outer_list	= null;

		//public FormManageClaim(FormCard form)
		public FormManageClaim(Form form, DtCard srcCard = null, ListView srcOuterList = null)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// ���������� ����� � ������� �����
			outer_form	= form;
			//if(form != null)
			//	card	= form.the_card;
			//else
			//	card	= null;
			card = srcCard;
			if (card != null)
			{
				string txt;
				txt			= "������ � �����-������ �";
				txt			+= card.GetData("�����_��������").ToString();
				txt			+= " �� " + card.GetData("����").ToString();
				this.Text	= txt;
				//outer_list	= form.GetClaimList();
			}
			outer_list = srcOuterList;

			// ������ ������
			DbSqlClaim.SelectInList(listView1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageClaim));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.textBox_new_claim = new System.Windows.Forms.TextBox();
			this.button_new_claim = new System.Windows.Forms.Button();
			this.button_remove = new System.Windows.Forms.Button();
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
			this.listView1.Location = new System.Drawing.Point(8, 72);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(632, 256);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "������";
			this.columnHeader1.Width = 606;
			// 
			// textBox_new_claim
			// 
			this.textBox_new_claim.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBox_new_claim.Location = new System.Drawing.Point(8, 24);
			this.textBox_new_claim.Name = "textBox_new_claim";
			this.textBox_new_claim.Size = new System.Drawing.Size(552, 20);
			this.textBox_new_claim.TabIndex = 1;
			this.textBox_new_claim.Text = "";
			// 
			// button_new_claim
			// 
			this.button_new_claim.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_new_claim.Location = new System.Drawing.Point(576, 24);
			this.button_new_claim.Name = "button_new_claim";
			this.button_new_claim.Size = new System.Drawing.Size(64, 23);
			this.button_new_claim.TabIndex = 2;
			this.button_new_claim.Text = "��������";
			this.button_new_claim.Click += new System.EventHandler(this.button_new_claim_Click);
			// 
			// button_remove
			// 
			this.button_remove.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_remove.Image")));
			this.button_remove.Location = new System.Drawing.Point(8, 48);
			this.button_remove.Name = "button_remove";
			this.button_remove.Size = new System.Drawing.Size(24, 23);
			this.button_remove.TabIndex = 3;
			this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
			// 
			// FormManageClaim
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(648, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_remove,
																		  this.button_new_claim,
																		  this.textBox_new_claim,
																		  this.listView1});
			this.Name = "FormManageClaim";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "������ ��������/���������� �����";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_claim_Click(object sender, System.EventArgs e)
		{
			// ��������� � ������ ���������� ��������/����� ����� �������
			string text	= textBox_new_claim.Text;
			text		= text.Trim();
			if(text == "") return;				// �� ������ ������ ������
			DtClaim claim = new DtClaim(text);
			claim = DbSqlClaim.Insert(claim);
			if(claim == null) return;			// �������� �� �������
			// ��������� � ������
			ListViewItem item = new ListViewItem();
			claim.SetLVItem(item);
			listView1.Items.Add(item);

			// ���������� � ��������� �����-�����, ���� ����
			SendToOuterList(claim);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// ����� �� ������������
			switch(e.Column)
			{
				case 0:
					FormSelectString form = new FormSelectString("����� ��� ������", "");
					if(form.ShowDialog() != DialogResult.OK) return;
					string mask = form.SelectedTextMask;
					listView1.Items.Clear();
					DbSqlClaim.SelectInList(listView1, mask);
					break;
				default:
					break;
			}
		}

		protected void SendToOuterList(DtClaim claim)
		{
			// ���������� �� ������� �������� � �������� ������� �����
			if(card == null) return;
			if(outer_form == null) return;
			if(outer_form.IsDisposed == true) return;
			// ����� ����� ���������!
			DtCardClaim	card_claim = new DtCardClaim(claim, card);
			card_claim = DbSqlCardClaim.Insert(card_claim);
			if(card_claim == null) return;	// �� ������ ��������
			// �������� � ����, ������ ����������
			ListViewItem item = new ListViewItem();
			card_claim.SetLVItem(item);
			outer_list.Items.Add(item);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// ��������� ��������� ������ �� ������ � �����-�����
			// ����������, ��� �������� ������
			ListViewItem item = null;
			DtClaim claim = null;
			long code = 0;
			
			item = Db.GetItemPosition(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// ��������� ������
			claim = DbSqlClaim.Find(code);
			if(claim == null) return;
			// ���������� ������ � ��������� �����-�����
			SendToOuterList(claim);
		}

		private void button_remove_Click(object sender, System.EventArgs e)
		{
			// ������� ��������� ������� ������
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// ��������� ������
			if(DbSqlClaim.Remove(code) == false) return;
			listView1.Items.Remove(item);
			
		}
	}
}
