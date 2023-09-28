using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListPartnerContact.
	/// </summary>
	public class FormListPartnerContact : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		DtPartner	partner = null;

		public FormListPartnerContact(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				// Блокировка
			}
			else
			{
				partner = DbSqlPartner.Find(code);
				if(partner != null)
				{
					this.Text = this.Text + " " + (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
				}
				else
				{
					// Блокировка
				}
				DbSqlPartnerContact.SelectInList(listView1, code);

				// Если список контактов пуст, пробуем загрузить из предыдущей версии
				if(listView1.Items.Count ==0)
				{
					if(partner.GetData("ТЕЛЕФОН").ToString().Length != 0 || partner.GetData("КОНТАКТ_ТЕЛЕФОН").ToString().Length != 0)
						MakeOldContacts();
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormListPartnerContact));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.button_new = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
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
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(656, 320);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Телефон";
			this.columnHeader1.Width = 160;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Тип";
			this.columnHeader2.Width = 146;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Примечание";
			this.columnHeader3.Width = 297;
			// 
			// button_new
			// 
			this.button_new.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new.Image")));
			this.button_new.Location = new System.Drawing.Point(8, 8);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 23);
			this.button_new.TabIndex = 1;
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5});
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
			this.menuItem2.Text = "Новый";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Удалить";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "Отметить звонок";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// FormListPartnerContact
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(672, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_new,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormListPartnerContact";
			this.Text = "Контакты";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Свойства
			// Список контактов контрагента
			if(partner == null) return;
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;

			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			FormUpdatePartnerContact dialog = new FormUpdatePartnerContact(code_partner, code);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Contact.SetLVItem(item);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Новый
			if(partner == null) return;
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;
		
			FormUpdatePartnerContact dialog = new FormUpdatePartnerContact(code_partner, 0);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			ListViewItem item = listView1.Items.Add("");
			if(item == null) return;
			dialog.Contact.SetLVItem(item);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Удалить
			if(partner == null) return;
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;

			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
	
			if(DbSqlPartnerContact.Delete(code_partner, code) == false) return;
			listView1.Items.Remove(item);
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Отметить звонок
			if(partner == null) return;
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;

			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			DtPartnerContact contact = DbSqlPartnerContact.Find(code_partner, code);
			if(contact == null) return;

			FormSelectString dialog = new FormSelectString("Цель звонка", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string comment = dialog.SelectedText;

			DtPartnerConnection connection = new DtPartnerConnection(contact);
			connection.SetData("ЦЕЛЬ", comment);

			if(DbSqlPartnerConnection.Insert(connection) == null) return;
			MessageBox.Show("Звонок зарегистрирован");

		}

		private void button_new_Click(object sender, System.EventArgs e)
		{
			if(partner == null) return;
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;
		
			FormUpdatePartnerContact dialog = new FormUpdatePartnerContact(code_partner, 0);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			ListViewItem item = listView1.Items.Add("");
			if(item == null) return;
			dialog.Contact.SetLVItem(item);
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

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Поиски по колонкам
		}

		private void MakeOldContacts()
		{
			// Загрузка старых данных о контактах
			ArrayList array = DtPartnerContact.MakeContacts((string)partner.GetData("ТЕЛЕФОН"));
			if(array != null)
			{
				foreach(object o in array)
				{
					DtPartnerContact element = (DtPartnerContact)o;
					ListViewItem item = listView1.Items.Add("");
					element.SetLVItem(item);
				}
			}
				
			array = DtPartnerContact.MakeContacts((string)partner.GetData("КОНТАКТ_ТЕЛЕФОН"));
			if(array != null)
			{
				foreach(object o in array)
				{
					DtPartnerContact element = (DtPartnerContact)o;
					ListViewItem item = listView1.Items.Add("");
					element.SetLVItem(item);
				}
			}

			// И создаем список телефонов
			foreach(ListViewItem itm in listView1.Items)
			{
				if(itm.Tag != null)
				{
					DtPartnerContact element = new DtPartnerContact();
					element.SetData("КОД_КОНТАКТ", (long)itm.Tag);
					element.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ_КОНТАКТ", partner.GetData("КОД_КОНТРАГЕНТ"));
					element.SetData("ТИП_КОНТАКТ", "ТЕЛЕФОН");
					element.SetData("КОНТАКТ", itm.Text);
					element = DbSqlPartnerContact.Insert(element);
					if(element == null) listView1.Items.Remove(itm);
				}
			}
		}
	}
}
