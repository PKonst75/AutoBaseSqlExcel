using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListAutoReceive.
	/// </summary>
	public class FormListAutoReceive : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button button_receive_insert;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.Button button_delete;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormListAutoReceive()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormListAutoReceive));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button_receive_insert = new System.Windows.Forms.Button();
			this.button_update = new System.Windows.Forms.Button();
			this.button_delete = new System.Windows.Forms.Button();
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
																						this.columnHeader2});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(520, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 101;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Примечание";
			this.columnHeader2.Width = 250;
			// 
			// button_receive_insert
			// 
			this.button_receive_insert.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_receive_insert.Image")));
			this.button_receive_insert.Location = new System.Drawing.Point(32, 0);
			this.button_receive_insert.Name = "button_receive_insert";
			this.button_receive_insert.Size = new System.Drawing.Size(24, 23);
			this.button_receive_insert.TabIndex = 1;
			this.button_receive_insert.Click += new System.EventHandler(this.button_receive_insert_Click);
			// 
			// button_update
			// 
			this.button_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update.Image")));
			this.button_update.Location = new System.Drawing.Point(8, 0);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 2;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_delete
			// 
			this.button_delete.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete.Image")));
			this.button_delete.Location = new System.Drawing.Point(56, 0);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(24, 23);
			this.button_delete.TabIndex = 3;
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
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
			this.menuItem1.Text = "Excel";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Приход автомобилей";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// FormListAutoReceive
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(536, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_delete,
																		  this.button_update,
																		  this.button_receive_insert,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormListAutoReceive";
			this.Text = "Получения автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_receive_insert_Click(object sender, System.EventArgs e)
		{
			// Добавление нового прихода автомобилей
			FormUpdateAutoReceive dialog = new FormUpdateAutoReceive(0);
			dialog.Show();
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновление списка документов
			listView1.Items.Clear();
			DbSqlAutoReceive.SelectInList(listView1);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Редактирование документа
			ListViewItem item	= Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag	== 0) return;

			// Добавление нового прихода автомобилей
			FormUpdateAutoReceive dialog = new FormUpdateAutoReceive((long)item.Tag);
			dialog.Show();
		}

		private void button_delete_Click(object sender, System.EventArgs e)
		{
			// Удаление выбранного документа
			ListViewItem item	= Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag	== 0) return;

			if(DbSqlAutoReceive.Delete((long)item.Tag) == false) return;
			listView1.Items.Remove(item);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Выгрузка в EXCEL выбранного документа
			ListViewItem item	= Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag	== 0) return;

			DbExcelAutoReceive excel = new DbExcelAutoReceive((long)item.Tag);
			excel.DownloadData(false, 1);
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				//menuItem1.Enabled = false;
				
				// Включаем по разрешению
				//string login = Form1.currentLogin.ToLower();
				//if (login == "админ")
				//{
				//	menuItem1.Enabled = true;
				//}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}
	}
}
