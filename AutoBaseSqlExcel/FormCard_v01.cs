using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCard_v01.
	/// </summary>
	public class FormCard_v01 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		bool flag_new	= false;
		private System.Windows.Forms.GroupBox groupBox_partner;
		private System.Windows.Forms.GroupBox groupBox_represent;
		private System.Windows.Forms.GroupBox groupBox_auto;
		private System.Windows.Forms.TextBox textBox_juridical;
		private System.Windows.Forms.TextBox textBox_name;		// ���� ����� ��������

		DtCard card			= null;			// ������ �������� �����-������
		DtPartner partner	= null;
		DtPartner represent = null;
		DtAuto auto			= null;

		public FormCard_v01(long number, int year)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(number == 0)
			{
				card		= new DtCard();
				flag_new	= true;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox_partner = new System.Windows.Forms.GroupBox();
			this.groupBox_represent = new System.Windows.Forms.GroupBox();
			this.groupBox_auto = new System.Windows.Forms.GroupBox();
			this.textBox_juridical = new System.Windows.Forms.TextBox();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox_partner.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1});
			this.tabControl1.Location = new System.Drawing.Point(16, 40);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(600, 384);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox_auto,
																				   this.groupBox_represent,
																				   this.groupBox_partner});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(592, 355);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "����� ������";
			// 
			// groupBox_partner
			// 
			this.groupBox_partner.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox_partner.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.textBox_name,
																						   this.textBox_juridical});
			this.groupBox_partner.Location = new System.Drawing.Point(16, 8);
			this.groupBox_partner.Name = "groupBox_partner";
			this.groupBox_partner.Size = new System.Drawing.Size(560, 96);
			this.groupBox_partner.TabIndex = 0;
			this.groupBox_partner.TabStop = false;
			this.groupBox_partner.Text = "��������";
			// 
			// groupBox_represent
			// 
			this.groupBox_represent.Location = new System.Drawing.Point(16, 104);
			this.groupBox_represent.Name = "groupBox_represent";
			this.groupBox_represent.Size = new System.Drawing.Size(560, 100);
			this.groupBox_represent.TabIndex = 1;
			this.groupBox_represent.TabStop = false;
			this.groupBox_represent.Text = "�������������";
			// 
			// groupBox_auto
			// 
			this.groupBox_auto.Location = new System.Drawing.Point(16, 208);
			this.groupBox_auto.Name = "groupBox_auto";
			this.groupBox_auto.Size = new System.Drawing.Size(560, 100);
			this.groupBox_auto.TabIndex = 2;
			this.groupBox_auto.TabStop = false;
			this.groupBox_auto.Text = "����������";
			// 
			// textBox_juridical
			// 
			this.textBox_juridical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_juridical.Location = new System.Drawing.Point(8, 24);
			this.textBox_juridical.Name = "textBox_juridical";
			this.textBox_juridical.Size = new System.Drawing.Size(232, 23);
			this.textBox_juridical.TabIndex = 0;
			this.textBox_juridical.Text = "";
			// 
			// textBox_name
			// 
			this.textBox_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_name.Location = new System.Drawing.Point(8, 56);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(544, 23);
			this.textBox_name.TabIndex = 1;
			this.textBox_name.Text = "";
			// 
			// FormCard_v01
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(632, 429);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCard_v01";
			this.Text = "������������� �����";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox_partner.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnCreateControl()
		{
			string txt	= "";
			bool need_represent = false;
			UserInterface.Buttons res;

			if(flag_new == true)
			{
				// ������� ����������� ����������.....
				// �������������
				ListView list = new ListView();
				DbSqlWorkshop.SelectInList(list);
				FormSelectionList dialog = new FormSelectionList(list);
				dialog.ShowDialog();
				// ��������� - ������������� � �����������
				txt = UserInterface.InquiryPrompt("������������� � ����������� ��� ������� ������������� ��������", "������� ������� (��� ����� ������������ ��� ��. ����) ��������� ����������.");
				// ������ �����������
				partner = (DtPartner)UserInterface.PartnerList(0, txt, 2, UserInterface.ClickType.Select);
				if (partner == null)
				{
					// ��������� ���������� ��������
					// ������ �� ������� ����
					return;
				}
				// ������ ������� ��������� ����������
				// ���� �������� ����������� ���� - ������� ����� �������������
				if ((bool)partner.GetData("�����������_����") == true)
					need_represent = true;
				else
				{
					// ���� �������� - ���������� ���� - ������ �����������
					res = UserInterface.YesNoPrompt("������������ �������������", "������������ ������������� ����������� ��������� ����������?");
					if(res == UserInterface.Buttons.Yes)
						need_represent = false;
					else
						need_represent = true;
				}
				// ���� ���������� ���� - ��������� �������������
				if(need_represent == true)
				{
					// ��������� - ������������� � �����������
					txt = UserInterface.InquiryPrompt("������������ �������������", "������� ������� �������������.");
					// �������� �������������
					represent = (DtPartner)UserInterface.PartnerList(0, txt, 2, UserInterface.ClickType.Select);
					if (represent == null)
					{
						// ��������� ���������� ��������
						// ������ �� ������� ����
						return;
					}
				}
				// �������������� �������� � �������������� ������ ��������������
				// ------
				// ------


				// ����� ���������� �����������
				// ��������� - ������������� � �����������
				txt = UserInterface.InquiryPrompt("������������� � ����������� ��� ������� ������������� ��������", "������� VIN ���������� (����� ������ � ������ ���������� VIN ������).");
				auto = (DtAuto)UserInterface.AutoList(0, txt, 2, UserInterface.ClickType.Select);
				if (auto == null)
				{
					// �������������� ������ ���������� � �������� ����������
				}
				// �������������� ����������� ���������� � ������ ����������� �����������
				ShowData();
			}
		}

		protected void ShowData()
		{
			// ���������� ����������� ������ � ��������
			if((bool)partner.GetData("�����������_����") == true)
			{
				DtPartnerJuridical partner_juridical = (DtPartnerJuridical)partner.GetData("�����������");
				textBox_juridical.Text = "����������� ����";
				textBox_name.Text = (string)partner_juridical.GetData("������������_�����������");
			}
			else
			{
				DtPartnerPerson partner_person = (DtPartnerPerson)partner.GetData("����������");
				textBox_juridical.Text = "���������� ����";
				textBox_name.Text = (string)partner_person.GetData("�������") + " " + (string)partner_person.GetData("���") + " " + (string)partner_person.GetData("��������");
			}
		}
	}
}
