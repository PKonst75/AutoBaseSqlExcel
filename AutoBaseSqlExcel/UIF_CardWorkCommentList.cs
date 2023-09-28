using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_CardWorkCommentList.
	/// </summary>
	public class UIF_CardWorkCommentList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_comments;
		private System.Windows.Forms.ColumnHeader columnHeader1;
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

		long number		= 0L;
		int year		= 0;
		private System.Windows.Forms.MenuItem menuItem6;
		int position	= 0;

		public UIF_CardWorkCommentList(DtCardWork card_work)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			number		= (long)card_work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА");
			year		= (int)card_work.GetData("ГОД_КАРТОЧКА_КАРТОЧКА_РАБОТА");
			position	= (int)card_work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");

			FillList();
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
			this.listView_comments = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView_comments
			// 
			this.listView_comments.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_comments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								this.columnHeader1});
			this.listView_comments.Location = new System.Drawing.Point(0, 32);
			this.listView_comments.Name = "listView_comments";
			this.listView_comments.Size = new System.Drawing.Size(560, 240);
			this.listView_comments.TabIndex = 0;
			this.listView_comments.View = System.Windows.Forms.View.Details;
			this.listView_comments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_comments_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Примечание";
			this.columnHeader1.Width = 518;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem6,
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "Исправить ошибку в примечании";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "Сделать видимым";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Сделать невидимым";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Удалить примечание";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Добавить примечание";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// UIF_CardWorkCommentList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(560, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView_comments});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "UIF_CardWorkCommentList";
			this.Text = "Управление примечаниями";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Запуск процедуры исправления текста примечания
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			string text = item.Text;
			string new_text = UserInterface.Selector_String("Исправьте текст примечания", text);
			if(new_text.Length == 0) return;
			bool res = DbSqlCardWorkComment.Update(code, new_text);
			if(res == false) return;
			MessageBox.Show("Примечание исправленно", "Сообщение");
			item.Text	= new_text;
		}

		private void listView_comments_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Запуск контекстного меню
			// На поднятие кнопки мышки - меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu1.Show(listView_comments, new Point(e.X, e.Y));
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Удаляем выбранное примечание
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			bool res = DbSqlCardWorkComment.DeleteConnection(number, year, position, code);
			if(res == false) return;
			MessageBox.Show("Примечание удалено", "Сообщение");
			listView_comments.Items.Remove(item);
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Запуск процедуры добавления нового примечания
			// Добавляем примечание к выбранной работе
			long code			= 0;
			bool show_flag		= false;

			if(position == 0)
			{
				MessageBox.Show("Сначала нужно СОХРАНИТЬ заказ-наряд");
				return;
			}

			// Зпрос примечания
			ArrayList array = new ArrayList();
			DbSqlCardWorkComment.SelectInArray(array);
			UIF_Selector_Array form = new UIF_Selector_Array(array);
			if(form.ShowDialog() == DialogResult.OK)
			{
				// Произвели выбор из списка
				code			= form.SelectedCode;
			}
			else
			{
				// Зпрос нового примечания
				string text = "";
				FormSelectString form1 = new FormSelectString("Текст примечания", "");
				if(form1.ShowDialog() != DialogResult.OK) return;
				text = form1.SelectedText;
				if(text == "") return;
				DtCardWorkComment comment = new DtCardWorkComment(text);
				comment = DbSqlCardWorkComment.Insert(comment);
				if(comment == null) return;
				code = comment.code;
			}
			if(code == 0) return;
			DialogResult result = MessageBox.Show("Сделать примечание видимым?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if(result == DialogResult.Yes) show_flag = true;

			// Проверка на возможные ошибки!!!
			bool flg = true;
			if (number == 0) flg =  false;
			if (year == 0) flg =  false;
			if (position == 0) flg =  false;
			if (code == 0) flg =  false;
			if(flg == false)
			{
				MessageBox.Show("Ошибка");
				return;
			}

			bool res = DbSqlCardWorkComment.InsertConnection(number, year, position, code, show_flag);
			if(res == true) MessageBox.Show("Примечание добавленно");
			// Перезачтение списка
			FillList();
		}

		void FillList()
		{
			listView_comments.Items.Clear();

			// Зачитываем список примечаний
			ArrayList comments = new ArrayList();
			// Заполняем ListView - видимыми
			DbSqlCardWorkComment.SelectInArrayConnectionVisible(comments, number, year, position);
			foreach(object o in comments)
			{
				DtCardWorkComment comment = (DtCardWorkComment)o;
				ListViewItem itm = new ListViewItem();
				comment.SetLVItem(itm, true);
				listView_comments.Items.Add(itm);
			}
			comments.Clear();
			DbSqlCardWorkComment.SelectInArrayConnectionInvisible(comments, number, year, position);
			foreach(object o in comments)
			{
				DtCardWorkComment comment = (DtCardWorkComment)o;
				ListViewItem itm = new ListViewItem();
				comment.SetLVItem(itm, false);
				listView_comments.Items.Add(itm);
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Показать примечание
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			// Проверка на спрятанность примечания
			if(item.ForeColor != System.Drawing.Color.Blue)
			{
				MessageBox.Show("Примечание уже видимое");
				return;
			}

			bool res = DbSqlCardWorkComment.ShowConnection(number, year, position, code);
			if(res == false) return;
			FillList();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Спрятать примечание
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;

			// Проверка на спрятанность примечания
			if(item.ForeColor == System.Drawing.Color.Blue)
			{
				MessageBox.Show("Примечание уже спрятано");
				return;
			}

			bool res = DbSqlCardWorkComment.HideConnection(number, year, position, code);
			if(res == false) return;
			FillList();
		}
	}
}
