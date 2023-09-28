using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateStorageRequest.
	/// </summary>
	public class FormUpdateStorageRequest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_write;
		private System.Windows.Forms.Button button_close;

		DtStorageRequest request;

		public FormUpdateStorageRequest(DtStorageRequest source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ListViewItem item;
			// ��������� ������� � �����������
			// ���
			item = new ListViewItem();
			item.Text = "���";
			item.Tag = "���_������";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ���
			item = new ListViewItem();
			item.Text = "���";
			item.Tag = "���_������";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ���� � �����
			item = new ListViewItem();
			item.Text = "���� � �����";
			item.Tag = "����_������";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ��������� �������
			item = new ListViewItem();
			item.Text = "��������� �������";
			item.Tag = "������_���_�����_������";
			listView1.Items.Add(item);
			// ����������
			item = new ListViewItem();
			item.Text = "����������";
			item.Tag = "����������_�����_������";
			listView1.Items.Add(item);
			// ��������
			item = new ListViewItem();
			item.Text = "��������";
			item.Tag = "��������_������";
			listView1.Items.Add(item);
			// ��������� ����
			item = new ListViewItem();
			item.Text = "��������� ����";
			item.Tag = "���������_����_����������";
			listView1.Items.Add(item);
			// ��������
			item = new ListViewItem();
			item.Text = "��������";
			item.Tag = "��������";
			listView1.Items.Add(item);
			// ��� �����������
			item = new ListViewItem();
			item.Text = "���_�����������";
			item.Tag = "������_���_����������";
			listView1.Items.Add(item);
			// ��� ������������ �����
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "��� ������������ ������";
			item.Tag = "���_��������_������";
			listView1.Items.Add(item);
			// ���� ������ ������
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "���� � ����� ������ ������";
			item.Tag = "����_������_������";
			listView1.Items.Add(item);
			// ��������_������_������
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "�������� ������ ������";
			item.Tag = "��������_������_������";
			listView1.Items.Add(item);
			// ���� ���������� ������
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "���� � ����� ���������� ������";
			item.Tag = "����_������_����������";
			listView1.Items.Add(item);
			// ��������_����������_������
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "�������� ���������� ������";
			item.Tag = "��������_����������_������";
			listView1.Items.Add(item);
			// ��������_����������_������
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "�������������� ���� ��������";
			item.Tag = "����_��������";
			listView1.Items.Add(item);

			// �������� ������� ������
			DbPartner partner		= null;
			DbStaff staff_give		= null;
			DbStaff staff_execute	= null;
			DtCard	card			= null;
			if(source == null)
			{
				request = new DtStorageRequest();
			}
			else
			{
				request = new DtStorageRequest(source);
				// �������� ����������� ��� ������� ����������� ������
				partner			= DbPartner.Find((long)request.GetData("������_���_����������"));
				staff_give		= DbStaff.Find((long)request.GetData("���_��������_������_������"));
				staff_execute	= DbStaff.Find((long)request.GetData("���_��������_����������_������"));
				// ������ ������ ��������, ���� ������� �� �����
				if((long)request.GetData("���_������") != 0)
					button_write.Enabled = false;
			}

			// ��������� ������� ������
			listView1.Items[0].SubItems.Add(request.GetData("���_������").ToString());
			listView1.Items[1].SubItems.Add(request.GetData("���_������").ToString());
			listView1.Items[2].SubItems.Add(request.GetData("����_������").ToString());
			listView1.Items[3].SubItems.Add(request.GetData("������������_�����_������").ToString());
			listView1.Items[4].SubItems.Add(request.GetData("����������_�����_������").ToString());
			// �������� ������
			if((bool)request.GetData("��������_������") == true)
				listView1.Items[5].SubItems.Add("��");
			else
				listView1.Items[5].SubItems.Add("���");

			if((bool)request.GetData("����_���������_����_����������") == false)
				listView1.Items[6].SubItems.Add("");
			else
				listView1.Items[6].SubItems.Add(request.GetData("���������_����_����������").ToString());
			// ������ � �������� ������
			card	= DbSqlCard.Find((long)request.GetData("������_�����_��������"), (int)request.GetData("������_���_��������"));
			if(card == null)
				listView1.Items[7].SubItems.Add("");
			else
			{
				listView1.Items[7].SubItems.Add(card.GetData("�����_��������").ToString() + " �� " + card.GetData("����").ToString() + " / " + card.GetData("�����_�����_��������").ToString());
			}
			// ����� ������ � �������� ������
			if(partner == null)
				listView1.Items[8].SubItems.Add("");
			else
				listView1.Items[8].SubItems.Add(partner.Title);
			listView1.Items[9].SubItems.Add(request.GetData("��������_������").ToString());
			if((bool)request.GetData("����_����_������_������") == false)
				listView1.Items[10].SubItems.Add("");
			else
				listView1.Items[10].SubItems.Add(request.GetData("����_������_������").ToString());
			if(staff_give == null)
				listView1.Items[11].SubItems.Add("");
			else
				listView1.Items[11].SubItems.Add(staff_give.Title);

			if((bool)request.GetData("����_����_������_����������") == false)
				listView1.Items[12].SubItems.Add("");
			else
				listView1.Items[12].SubItems.Add(request.GetData("����_������_����������").ToString());
			if(staff_execute == null)
				listView1.Items[13].SubItems.Add("");
			else
				listView1.Items[13].SubItems.Add(staff_execute.Title);
			if((bool)request.GetData("����_����_��������") == false)
				listView1.Items[14].SubItems.Add("");
			else
				listView1.Items[14].SubItems.Add(request.GetData("����_��������").ToString());
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
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button_write = new System.Windows.Forms.Button();
			this.button_close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(424, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "��������";
			this.columnHeader1.Width = 142;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "��������";
			this.columnHeader2.Width = 256;
			// 
			// button_write
			// 
			this.button_write.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_write.Location = new System.Drawing.Point(352, 248);
			this.button_write.Name = "button_write";
			this.button_write.TabIndex = 1;
			this.button_write.Text = "��������";
			this.button_write.Click += new System.EventHandler(this.button_write_Click);
			// 
			// button_close
			// 
			this.button_close.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_close.Location = new System.Drawing.Point(280, 248);
			this.button_close.Name = "button_close";
			this.button_close.TabIndex = 2;
			this.button_close.Text = "�������";
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// FormUpdateStorageRequest
			// 
			this.AcceptButton = this.button_write;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button_close;
			this.ClientSize = new System.Drawing.Size(456, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_close,
																		  this.button_write,
																		  this.listView1});
			this.Name = "FormUpdateStorageRequest";
			this.Text = "������ �� �����";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// ������� ������ �� ����� ���� �������� ���������� ��� ���� ��� ���������
			DbPartner	partner;
			ListViewItem item = Db.GetItemSelected(listView1);
			string tag = (string)item.Tag;
			switch(tag)
			{
				case "������_���_�����_������":
					// ����� ������������ �������
					FormDetailStorageList dialog2 = new FormDetailStorageList(null, 1, null, null);
					if(dialog2.ShowDialog() != DialogResult.OK) return;
					DbDetailStorage detail = dialog2.SelectedDetailStorage;
					request.SetData("������_���_�����_������", (object)detail.Code);
					item.SubItems[1].Text = detail.DetailCodeTxt + " / " + detail.DetailName;
					break;
				case "����������_�����_������":
					// ����� ������������� ����������
					FormSelectString dialog1 = new FormSelectString("���������� ������", "0");
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					request.SetData("����������_�����_������", (object)dialog1.SelectedFloat);
					item.SubItems[1].Text = request.GetData("����������_�����_������").ToString();
					break;
				case "��������_������":
					// ������� �������������
					if(MessageBox.Show("�������� ������������ ������ ��� �����������?", "������", MessageBoxButtons.YesNo) != DialogResult.Yes)
					{
						request.SetData("��������_������", (object)false);
						item.SubItems[1].Text = "���";
					}
					else
					{
						request.SetData("��������_������", (object)true);
						item.SubItems[1].Text = "��";
					}
					break;
				case "��������":
					// ����� ��������
					FormManageCard dialog3 = new FormManageCard(Db.ClickType.Select, 0, null);
					if(dialog3.ShowDialog() != DialogResult.OK) return;
					DtCard card = dialog3.card_selected;
					request.SetData("������_�����_��������", (object)card.GetData("�����_��������"));
					request.SetData("������_���_��������", (object)card.GetData("���_��������"));
					request.SetData("������_���_����������", (object)card.GetData("��������_��������"));
					item.SubItems[1].Text = card.GetData("�����_��������").ToString() + " �� " + card.GetData("����").ToString() + " / " + card.GetData("�����_�����_��������").ToString();
					partner	= DbPartner.Find((long)request.GetData("������_���_����������"));
					if(partner == null)
						listView1.Items[8].SubItems[1].Text = "";
					else
						listView1.Items[8].SubItems[1].Text = partner.Title;
					break;
				case "������_���_����������":
					// ����� �����������
					FormPartnerList dialog4 = new FormPartnerList();
					if(dialog4.ShowDialog() != DialogResult.OK) return;
					partner = dialog4.Partner;
					request.SetData("������_���_����������", (object)partner.Code);
					if(partner == null)
						item.SubItems[1].Text = "";
					else
						item.SubItems[1].Text = partner.Title;
					break;
				case "���������_����_����������":
					// ����� �������� ���� ����������
					FormSelectDate dialog5 = new FormSelectDate();
					if(dialog5.ShowDialog() != DialogResult.OK) return;
					request.SetData("���������_����_����������", (object)dialog5.SelectedDate);
					item.SubItems[1].Text = request.GetData("���������_����_����������").ToString();
					break;
				default:
					break;
			}
		}

		private void button_write_Click(object sender, System.EventArgs e)
		{
			// ���������� ��������� ������
			// ������ ���� ������������ ������
			DbStaff staff = DbStaff.GetByESign("����������� �������");
			if(staff == null) return;
			request.SetData("���_��������_������", staff.Code);
			// ������ ������
			DtStorageRequest result = DbSqlStorageRequest.Insert(request);
			if(result == null) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_close_Click(object sender, System.EventArgs e)
		{
			// ��������� ����, ��� ���������� ���������
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
