using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageDirection.
	/// </summary>
	public class FormManageDirection : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.Button button_make;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button_set_other_diler;
		private System.Windows.Forms.Button button_set_exception;
		private System.ComponentModel.IContainer components;

		public FormManageDirection()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DbSql.FillList(listView1, DbSqlDirection.select, new DbSql.DelegateMakeLVItem(DbSqlDirection.MakeLVItem));
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.button_new = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_update = new System.Windows.Forms.Button();
			this.button_make = new System.Windows.Forms.Button();
			this.button_set_other_diler = new System.Windows.Forms.Button();
			this.button_set_exception = new System.Windows.Forms.Button();
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
																						this.columnHeader6,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(16, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(712, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "�������������";
			this.columnHeader1.Width = 125;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "�����";
			this.columnHeader2.Width = 84;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "����";
			this.columnHeader3.Width = 74;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "������";
			this.columnHeader6.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "��������";
			this.columnHeader4.Width = 152;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "��������";
			this.columnHeader5.Width = 200;
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(16, 8);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 23);
			this.button_new.TabIndex = 1;
			this.toolTip1.SetToolTip(this.button_new, "����� �����������");
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// button_update
			// 
			this.button_update.Location = new System.Drawing.Point(40, 8);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_update, "��������� �����������");
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_make
			// 
			this.button_make.Location = new System.Drawing.Point(224, 8);
			this.button_make.Name = "button_make";
			this.button_make.Size = new System.Drawing.Size(24, 23);
			this.button_make.TabIndex = 3;
			this.toolTip1.SetToolTip(this.button_make, "�������� ���������� �����������");
			this.button_make.Click += new System.EventHandler(this.button_make_Click);
			// 
			// button_set_other_diler
			// 
			this.button_set_other_diler.Location = new System.Drawing.Point(272, 8);
			this.button_set_other_diler.Name = "button_set_other_diler";
			this.button_set_other_diler.Size = new System.Drawing.Size(24, 23);
			this.button_set_other_diler.TabIndex = 4;
			this.toolTip1.SetToolTip(this.button_set_other_diler, "�������� ���������� ����������� ������ ����");
			this.button_set_other_diler.Click += new System.EventHandler(this.button_set_other_diler_Click);
			// 
			// button_set_exception
			// 
			this.button_set_exception.Location = new System.Drawing.Point(328, 8);
			this.button_set_exception.Name = "button_set_exception";
			this.button_set_exception.Size = new System.Drawing.Size(24, 23);
			this.button_set_exception.TabIndex = 5;
			this.toolTip1.SetToolTip(this.button_set_exception, "������� �� ���������� �� �����������");
			this.button_set_exception.Click += new System.EventHandler(this.button_set_exception_Click);
			// 
			// FormManageDirection
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(744, 285);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_set_exception,
																		  this.button_set_other_diler,
																		  this.button_make,
																		  this.button_update,
																		  this.button_new,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormManageDirection";
			this.Text = "���������� �������������";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// ���� ������ �����������
			FormDirection dialog = new FormDirection(0);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			ListViewItem item = listView1.Items.Add("");
			dialog.Direction.SetLVItem(item);
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// �������������� �����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			FormDirection dialog = new FormDirection(code);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Direction.SetLVItem(item);
		}

		private void button_make_Click(object sender, System.EventArgs e)
		{
			// ������� � ���������� ����������� �� ����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			DtDirection direction = DbSqlDirection.Find(code);
			if(direction == null) return;
			// ����������� ��������
			FormCardList dialog = new FormCardList(Db.ClickType.Select, 0, null);
			if(dialog.ShowDialog() !=  DialogResult.OK) return;
			// ��������� ������������ ������
			DbCard card = dialog.SelectedCard;

			if(direction.CheckAuto(card.Auto) == false)
			{
				MessageBox.Show("���������� �� �������� ��� �������!");
				return;
			}

			DbSqlDirection.InsertDone(card.Auto, card, direction, false);
		}

		private void button_set_other_diler_Click(object sender, System.EventArgs e)
		{
			// �������� ���������� ����������� ������ ����������� ���
			// ������� � ���������� ����������� �� ����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			DtDirection direction = DbSqlDirection.Find(code);
			if(direction == null) return;
			// ����������� ��������
			FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
			if(dialog.ShowDialog() !=  DialogResult.OK) return;
			// ��������� ������������ ������
			DbAuto auto = dialog.Auto;

			if(direction.CheckAuto(auto) == false)
			{
				MessageBox.Show("���������� �� �������� ��� �������!");
				return;
			}

			DbSqlDirection.InsertDone(auto, null, direction, false);
		}

		private void button_set_exception_Click(object sender, System.EventArgs e)
		{
			// �������� �� ����������� �� ������� ������
			// ������� � ���������� ����������� �� ����������
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			DtDirection direction = DbSqlDirection.Find(code);
			if(direction == null) return;
			// ����������� ��������
			FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
			if(dialog.ShowDialog() !=  DialogResult.OK) return;
			// ��������� ������������ ������
			DbAuto auto = dialog.Auto;

			if(direction.CheckAuto(auto) == false)
			{
				MessageBox.Show("���������� �� �������� ��� �������!");
				return;
			}

			DbSqlDirection.InsertDone(auto, null, direction, true);
		}
	}
}
