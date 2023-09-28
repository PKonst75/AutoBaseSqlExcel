using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStorageDetailBalance.
	/// </summary>
	public class FormStorageDetailBalance : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.DateTimePicker dateTimePicker_start;
		private System.Windows.Forms.DateTimePicker dateTimePicker_end;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormStorageDetailBalance()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStorageDetailBalance));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.button_update = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
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
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(0, 80);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(752, 192);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "��� ������";
			this.columnHeader1.Width = 109;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "������������";
			this.columnHeader2.Width = 307;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "����������";
			this.columnHeader3.Width = 90;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "������";
			this.columnHeader4.Width = 90;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "�������";
			this.columnHeader5.Width = 90;
			// 
			// dateTimePicker_start
			// 
			this.dateTimePicker_start.Location = new System.Drawing.Point(112, 8);
			this.dateTimePicker_start.Name = "dateTimePicker_start";
			this.dateTimePicker_start.TabIndex = 1;
			// 
			// dateTimePicker_end
			// 
			this.dateTimePicker_end.Location = new System.Drawing.Point(112, 32);
			this.dateTimePicker_end.Name = "dateTimePicker_end";
			this.dateTimePicker_end.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "������";
			// 
			// button_update
			// 
			this.button_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update.Image")));
			this.button_update.Location = new System.Drawing.Point(0, 56);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 4;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "�������� ������";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// FormStorageDetailBalance
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(752, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_update,
																		  this.label1,
																		  this.dateTimePicker_end,
																		  this.dateTimePicker_start,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormStorageDetailBalance";
			this.Text = "������ ��������� �������";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// �������� ������
			listView1.Items.Clear();
			DbSqlStorageDetail.SelectInListBalance(listView1, dateTimePicker_start.Value, dateTimePicker_end.Value);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// �������� ������ � ������
			ListViewItem item	= null;
			long code			= 0;
			float balance		= 0.0F;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			balance	= Math.Abs((float)Convert.ToDouble(item.SubItems[4].Text));
			// �������� ������ �� ����������� ����������
			DtStorageRequest request = new DtStorageRequest();
			request.SetData("������_���_�����_������", code);
			request.SetData("������������_�����_������", item.SubItems[1].Text);
			request.SetData("������_�����_��������", 0L);
			request.SetData("������_���_��������", 0);
			request.SetData("������_���_����������", 0L);
			request.SetData("����������_�����_������", balance);
			request.SetData("��������_������", false);

			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(request);
			dialog.Show();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// �� ���������� ������ ������ ����� - ����������� ����
				// ��������� ����
				// ����� ������������
				// ��������� ��� �����������
				// �������� �� ����������
				string login = Form1.currentLogin.ToLower();
				if (login == "�������" || login == "�����" || login == "�����������")
				{
				}
				if (login == "�����")
				{
				}
				if (login == "�������")
				{
				}
				// ��������� ���� ������ �� ������� ��������� ��������
				// ����� ����
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}
	}
}
