using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for V1_UIF_CardRateManager.
	/// </summary>
	public class V1_UIF_CardRateManager : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public V1_UIF_CardRateManager()
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8,
																						this.columnHeader9,
																						this.columnHeader10});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 40);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(864, 224);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Модель";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "VIN";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "ТО";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Гарантия";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Консультант";
			this.columnHeader7.Width = 118;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Оценка";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "Список";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(88, 8);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 2;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Выходной опрос (Анкета)";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Устранено";
			this.columnHeader9.Width = 80;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Оценка посещения";
			this.columnHeader10.Width = 160;
			// 
			// V1_UIF_CardRateManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(880, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dateTimePicker_date,
																		  this.button1,
																		  this.listView1});
			this.Name = "V1_UIF_CardRateManager";
			this.Text = "Управление оценкой карточек";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.V1_UIF_CardRateManager_MouseUp);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Создание списка карточек закрытых на определенную дату
			listView1.Items.Clear();
			DateTime date = dateTimePicker_date.Value;
			ArrayList closed_cards = new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(closed_cards, date, date, 1);

			foreach(DtCard card in closed_cards)
			{
				DtCard thecard = DbSqlCard.FindRate(card.Number, card.Year);
				thecard.IsGuarantyWork();
				thecard.IsToWork();
				thecard.IsGuarantyDetail();
				ListViewItem item = new ListViewItem();
				thecard.SetLVItemCardRate(item);
				listView1.Items.Add(item);
			}
		}

		private void V1_UIF_CardRateManager_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Добавить результат выходного опроса (Анкеты)
			// Отменить карточку
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DtCard.Pair pair = (DtCard.Pair)item.Tag;
			
			// Запрашиваем данные выходного контроля
			V1_UIF_CardRate form = new V1_UIF_CardRate(pair.number, pair.year);
			if (form.ShowDialog() != DialogResult.OK) return;
			// Производим запись
			
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
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
