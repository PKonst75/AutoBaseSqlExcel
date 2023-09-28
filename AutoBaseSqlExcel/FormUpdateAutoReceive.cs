using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateAutoReceive.
	/// </summary>
	public class FormUpdateAutoReceive : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_receiver;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button button_new_auto;
		private System.Windows.Forms.Button button_save;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Button button_delete_receive;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.ColumnHeader columnHeader6;

		DtAutoReceive		auto_receive = null;

		public FormUpdateAutoReceive(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
				auto_receive	= new DtAutoReceive();
			else
			{
				auto_receive	= DbSqlAutoReceive.Find(code);
				if(auto_receive == null)
					auto_receive	= new DtAutoReceive();
			}
			// Отображение имеющихся данных
			dateTimePicker1.Value	= (DateTime)auto_receive.GetData("ДАТА_ДОКУМЕНТ");
			textBox_receiver.Text	= (string)auto_receive.GetData("ПОЛУЧАТЕЛЬ");
			textBox_comment.Text	= (string)auto_receive.GetData("ПРИМЕЧАНИЕ_ДОКУМЕНТ");

			// Заполнение списка
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") > 0)
			{
				DbSqlAuto.SelectInListReceive(listView1, (long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ"));
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormUpdateAutoReceive));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button_save = new System.Windows.Forms.Button();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.textBox_receiver = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.button_new_auto = new System.Windows.Forms.Button();
			this.button_delete_receive = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_save,
																					this.textBox_comment,
																					this.label3,
																					this.label2,
																					this.dateTimePicker1,
																					this.textBox_receiver,
																					this.label1});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(640, 120);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Данные документа";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(520, 88);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(104, 23);
			this.button_save.TabIndex = 6;
			this.button_save.Text = "Сохранить";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(112, 56);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(520, 23);
			this.textBox_comment.TabIndex = 5;
			this.textBox_comment.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Примечание";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(280, 24);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Получатель";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(64, 24);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 2;
			// 
			// textBox_receiver
			// 
			this.textBox_receiver.Location = new System.Drawing.Point(400, 24);
			this.textBox_receiver.Name = "textBox_receiver";
			this.textBox_receiver.Size = new System.Drawing.Size(232, 23);
			this.textBox_receiver.TabIndex = 1;
			this.textBox_receiver.Text = "";
			this.textBox_receiver.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_receiver_KeyDown);
			this.textBox_receiver.DoubleClick += new System.EventHandler(this.textBox_receiver_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата";
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 160);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(640, 144);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 162;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Исполнение";
			this.columnHeader2.Width = 125;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Цвет";
			this.columnHeader3.Width = 113;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "VIN";
			this.columnHeader4.Width = 150;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Примечание";
			this.columnHeader5.Width = 48;
			// 
			// button_new_auto
			// 
			this.button_new_auto.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_auto.Image")));
			this.button_new_auto.Location = new System.Drawing.Point(8, 136);
			this.button_new_auto.Name = "button_new_auto";
			this.button_new_auto.Size = new System.Drawing.Size(24, 23);
			this.button_new_auto.TabIndex = 2;
			this.button_new_auto.Click += new System.EventHandler(this.button_new_auto_Click);
			// 
			// button_delete_receive
			// 
			this.button_delete_receive.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete_receive.Image")));
			this.button_delete_receive.Location = new System.Drawing.Point(32, 136);
			this.button_delete_receive.Name = "button_delete_receive";
			this.button_delete_receive.Size = new System.Drawing.Size(24, 23);
			this.button_delete_receive.TabIndex = 3;
			this.button_delete_receive.Click += new System.EventHandler(this.button_delete_receive_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4});
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
			this.menuItem2.Text = "Карточки автомобиля";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Установить примечание";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Очистить примечание";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "№";
			this.columnHeader6.Width = 25;
			// 
			// FormUpdateAutoReceive
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(656, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_delete_receive,
																		  this.button_new_auto,
																		  this.listView1,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormUpdateAutoReceive";
			this.Text = "Получение автомобилей";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_auto_Click(object sender, System.EventArgs e)
		{
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") == 0)return;
			// Добавление нового автомобиля в документ
			// Запрос VIN кода
			FormSelectString dialog = new FormSelectString("Введите VIN автомобиля", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string s = dialog.SelectedText;
			// Пробуем найти автомобиль по VIN коду
			DtAuto auto = DbSqlAuto.Find(s);
			FormUpdateAuto dialog1 = null;
			if(auto == null)
			{
				// Ввод нового автомобиля
				dialog1 = new FormUpdateAuto(0, s);
			}
			else
			{
				// Получение существующего
				dialog1 = new FormUpdateAuto((long)auto.GetData("КОД_АВТОМОБИЛЬ"), "");
			}
			if(dialog1.ShowDialog() != DialogResult.OK) return;
			// Отмечаем получение элемента
			if(DbSqlAutoReceive.Receive(dialog1.Auto, auto_receive, "") == false) return;
			ListViewItem item = listView1.Items.Add("Ошибка");
			dialog1.Auto.SetLVItemReceive(item);
		}

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Получаем данные
			auto_receive.SetData("ДАТА_ДОКУМЕНТ", dateTimePicker1.Value);
			auto_receive.SetData("ПРИМЕЧАНИЕ_ДОКУМЕНТ", textBox_comment.Text);

			// Сохраняем данные о документе получения
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") == 0)
			{
				DtAutoReceive new_auto_receive = DbSqlAutoReceive.Insert(auto_receive);
				if(new_auto_receive == null) return;
				auto_receive = new_auto_receive;
				MessageBox.Show("Сохранили изменения");
			} 
			else
			{
				if(DbSqlAutoReceive.Update(auto_receive) == false) return;
				MessageBox.Show("Сохранили изменения");
			}
		}

		private void textBox_receiver_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Выбор получателя по документу
			if(e.KeyCode == Keys.Enter)
			{
				FormStaffList dialog = new FormStaffList();
				if(dialog.ShowDialog() != DialogResult.OK) return;
				if(dialog.SelectedStaff == null) return;
				textBox_receiver.Text	= dialog.SelectedStaff.Title;
				auto_receive.SetData("КОД_ПОЛУЧИЛ_АВТОМОБИЛИ", (long)dialog.SelectedStaff.Code);
			}
		}

		private void button_delete_receive_Click(object sender, System.EventArgs e)
		{
			// Убираем приход
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") == 0) return;
			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				if(itm.Tag != null && (long)itm.Tag != 0)
				{
					if(DbSqlAutoReceive.ReceiveDelete((long)itm.Tag, (long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ")) == true)
					{
						listView1.Items.Remove(itm);
					}
				}
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Вызов редактирование свойств автомобиля
			// Удалить выбранный автомобиль
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			// Запрос подтверждения
			FormUpdateAuto dialog = new FormUpdateAuto(code_auto, "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Auto.SetLVItemReceive(item);
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызов меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//menuItem6.Enabled = false;
				
				// Включаем по разрешению
				//string login = Form1.currentLogin.ToLower();
				//if (login == "админ")
				//{
				//	menuItem6.Enabled = true;
				//}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void textBox_receiver_DoubleClick(object sender, System.EventArgs e)
		{
			FormStaffList dialog = new FormStaffList();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedStaff == null) return;
			textBox_receiver.Text	= dialog.SelectedStaff.Title;
			auto_receive.SetData("КОД_ПОЛУЧИЛ_АВТОМОБИЛИ", (long)dialog.SelectedStaff.Code);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Получение списка карточке автомобиля
			// Вызов редактирование свойств автомобиля
			// Удалить выбранный автомобиль
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 1, (object)code_auto);
			dialog.Show();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Установить примечание к приходу этого автомобиля
			// Получение списка карточке автомобиля
			// Вызов редактирование свойств автомобиля
			// Удалить выбранный автомобиль
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") == 0) return;

			// Запрос примечания
			FormSelectString dialog = new FormSelectString("Примечание к полученному автомобилю", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			
			if(DbSqlAutoReceive.UpdateComment(code_auto, (long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ"), dialog.SelectedText) == false) return;
			DtAuto auto = DbSqlAuto.Find(code_auto);
			if(auto == null) return;
			auto.SetData("ПОЛУЧЕНИЕ_ПРИМЕЧАНИЕ", dialog.SelectedText);
			auto.SetLVItemReceive(item);
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;
			if((long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ") == 0) return;

			
			if(DbSqlAutoReceive.UpdateComment(code_auto, (long)auto_receive.GetData("КОД_АВТОМОБИЛЬ_ПОЛУЧЕНИЕ_ДОКУМЕНТ"), "") == false) return;
			DtAuto auto = DbSqlAuto.Find(code_auto);
			if(auto == null) return;
			auto.SetLVItemReceive(item);
		}
	}
}
