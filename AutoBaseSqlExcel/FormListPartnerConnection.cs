using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListPartnerConnection.
	/// </summary>
	public class FormListPartnerConnection : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormListPartnerConnection()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DbSqlPartnerConnection.SelectInList(listView1);
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
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader4});
			this.listView1.Location = new System.Drawing.Point(8, 40);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(672, 320);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 101;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Телефон";
			this.columnHeader2.Width = 131;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Контрагент";
			this.columnHeader3.Width = 148;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Примечание";
			this.columnHeader4.Width = 267;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Удалить";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// FormListPartnerConnection
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "FormListPartnerConnection";
			this.Text = "Связь с контрагентами";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Удаление звонка
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DateTime date = (DateTime)item.Tag;
			string contact = item.SubItems[1].Text;

			// Запрос подтверждения
			if(MessageBox.Show("Удалить звонок " + contact + " " + date.ToString() + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlPartnerConnection.Delete(date, contact) == false) return;
			listView1.Items.Remove(item);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Поиск по колонкам
			switch(e.Column)
			{
				case 0:
					// Поиск по дате
					FormSelectDateInterval dialog = new FormSelectDateInterval();
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlPartnerConnection.SelectInList(listView1, dialog.StartDate, dialog.EndDate);
					break;
				case 1:
					// Поиск по телефону
					FormSelectString dialog1 = new FormSelectString();
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlPartnerConnection.SelectInList(listView1, dialog1.SelectedTextMask);
					break;
				case 2:
					// Поиск по контрагенту
					FormListPartner dialog2 = new FormListPartner(0, null);
					if(dialog2.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlPartnerConnection.SelectInList(listView1, dialog2.SelectedCode);
					break;
				default:
					break;
			}
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызов контекстного меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem1.Enabled = false;
				
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem1.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}
	}
}
