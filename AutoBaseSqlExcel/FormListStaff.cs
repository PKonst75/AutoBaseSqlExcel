using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListStaff.
	/// </summary>
	public class FormListStaff : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_update;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_select;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

		DtStaff selected_staff;

		public FormListStaff(long code_job, long code_workshop)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Настройка параметров первоначального выбора
			if(code_job == 0 && code_workshop == 0)
			{
				// Выбор всех
				DbSqlStaff.SelectInList(listView1);
			}
			if(code_job != 0 && code_workshop != 0)
			{
				// Выбор всех
				DbSqlStaff.SelectInListJobWorkshop(listView1, code_job, code_workshop);
				return;
			}
			if(code_job != 0 && code_workshop == 0)
			{
				// Выбор всех
				DbSqlStaff.SelectInListJob(listView1, code_job);
			}
			if(code_job == 0 && code_workshop != 0)
			{
				// Выбор всех
				DbSqlStaff.SelectInListWorkshop(listView1, code_workshop);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormListStaff));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_update = new System.Windows.Forms.Button();
			this.button_select = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
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
																						this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 56);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(672, 272);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Фамилия";
			this.columnHeader1.Width = 134;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Имя Отчество";
			this.columnHeader2.Width = 174;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Подразделение";
			this.columnHeader3.Width = 198;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Должность";
			this.columnHeader4.Width = 140;
			// 
			// button_update
			// 
			this.button_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update.Image")));
			this.button_update.Location = new System.Drawing.Point(8, 32);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 1;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_select
			// 
			this.button_select.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_select.Location = new System.Drawing.Point(600, 336);
			this.button_select.Name = "button_select";
			this.button_select.TabIndex = 2;
			this.button_select.Text = "Выбрать";
			this.button_select.Click += new System.EventHandler(this.button_select_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Свойства";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Информация";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// FormListStaff
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(688, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_select,
																		  this.button_update,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormListStaff";
			this.Text = "Список персонала";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			selected_staff	= DbSqlStaff.Find(code);
			if(selected_staff == null) return;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Получение полного списка
			listView1.Items.Clear();
			DbSqlStaff.SelectInList(listView1);
		}

		private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// В случае множественного выбора, отдаем целый список
				if(listView1.SelectedItems == null) return;
				if(listView1.SelectedItems.Count == 0) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		private void button_select_Click(object sender, System.EventArgs e)
		{
			// Корректное закрытие диалога
			if(listView1.SelectedItems == null) return;
			if(listView1.SelectedItems.Count == 0) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Определяем код выбранного персонала.
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			FormUpdateStaff dialog = new FormUpdateStaff(code);
			dialog.ShowDialog();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызов меню
			if(e.Button == MouseButtons.Right)
			{
				menuItem1.Enabled	= false;
				menuItem2.Enabled	= false;

				if(Form1.currentLogin.ToLower() == "админ")
				{
					menuItem1.Enabled	= true;
					menuItem2.Enabled	= true;
				}
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Вызов окна с информацией
			FormInfoTransparent dialog = new FormInfoTransparent("1", "", null);
			dialog.Show();
		}
	
		public DtStaff SelectedStaff
		{
			get
			{
				return selected_staff;
			}
		}

		public ListView List
		{
			get
			{
				return listView1;
			}
		}
	}
}
