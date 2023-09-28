using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_POS_AutoTiming.
	/// </summary>
	public class UIF_POS_AutoTiming : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_exit;
		private System.Windows.Forms.ListView listView_auto;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.Timer timer_auto_update;
		private System.ComponentModel.IContainer components;

		public UIF_POS_AutoTiming()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			string text;
			System.Drawing.Bitmap img;
			ImageList imglst = new ImageList();
			imglst.ImageSize = new Size(40, 40);
			text						= ".\\push.bmp";
			img		= new System.Drawing.Bitmap(text);
			imglst.Images.Add(img);
			text						= ".\\push_red.bmp";
			img		= new System.Drawing.Bitmap(text);
			imglst.Images.Add(img);
			listView_auto.StateImageList = imglst;


			ListUpdate();
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
			this.button_exit = new System.Windows.Forms.Button();
			this.listView_auto = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_update = new System.Windows.Forms.Button();
			this.timer_auto_update = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// button_exit
			// 
			this.button_exit.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_exit.Location = new System.Drawing.Point(918, 184);
			this.button_exit.Name = "button_exit";
			this.button_exit.Size = new System.Drawing.Size(104, 80);
			this.button_exit.TabIndex = 0;
			this.button_exit.Text = "Выход";
			this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
			// 
			// listView_auto
			// 
			this.listView_auto.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_auto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2,
																							this.columnHeader3,
																							this.columnHeader4});
			this.listView_auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView_auto.FullRowSelect = true;
			this.listView_auto.Location = new System.Drawing.Point(8, 8);
			this.listView_auto.Name = "listView_auto";
			this.listView_auto.Size = new System.Drawing.Size(894, 256);
			this.listView_auto.TabIndex = 1;
			this.listView_auto.View = System.Windows.Forms.View.Details;
			this.listView_auto.Click += new System.EventHandler(this.listView_auto_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ З/Н";
			this.columnHeader1.Width = 110;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Рег. Знак";
			this.columnHeader2.Width = 140;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Модель";
			this.columnHeader3.Width = 320;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "VIN";
			this.columnHeader4.Width = 270;
			// 
			// button_update
			// 
			this.button_update.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.button_update.Location = new System.Drawing.Point(920, 8);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(104, 88);
			this.button_update.TabIndex = 2;
			this.button_update.Text = "Обновить список";
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// timer_auto_update
			// 
			this.timer_auto_update.Enabled = true;
			this.timer_auto_update.Interval = 60000;
			this.timer_auto_update.Tick += new System.EventHandler(this.timer_auto_update_Tick);
			// 
			// UIF_POS_AutoTiming
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(12, 28);
			this.ClientSize = new System.Drawing.Size(1030, 273);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_update,
																		  this.listView_auto,
																		  this.button_exit});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_POS_AutoTiming";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Учет времени нахождения автомобиля в сервисе";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.UIF_POS_AutoTiming_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void listView_auto_Click(object sender, System.EventArgs e)
		{
			// Останавливаем таймер
			timer_auto_update.Stop();

			// Щелчек на листе.
			// Нужно определить позицию где щелкнули и есть ли на ней элемент
			ListViewItem itm = Db.GetItemPosition(listView_auto);
			if(itm == null)
			{
				StartTimer();
				return;
			}
			if(itm.Tag == null)
			{
				StartTimer();
				return;
			}
			DtCard.Pair pair = (DtCard.Pair)itm.Tag;
			long number = pair.number;
			int year = pair.year;
			if(number == 0 || year == 0)
			{
				StartTimer();
				return;
			}
			// Скрываем выбор элемента
			
			// Поиск Элемента
			// Запуск фомы разрешенных действий
			string info = "З/Н №" + itm.Text + " (" + itm.SubItems[2].Text + " " + itm.SubItems[1].Text + " " + itm.SubItems[3].Text + ")";
			UIF_POS_AutoTiming_01 dialog = new UIF_POS_AutoTiming_01(number, year, info);
			if(dialog.ShowDialog() != DialogResult.OK)
			{
				StartTimer();
				return;
			}

			ListUpdate();
			StartTimer();
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			timer_auto_update.Stop();
			ListUpdate();
			StartTimer();
		}

		public void ListUpdate()
		{
			// Перезачитываем список
			ListViewItem itm;
			listView_auto.Items.Clear();

			ArrayList array0 = new ArrayList();
			DbSqlCard.SelectCardGoinTimePause(array0);

			ArrayList array1 = new ArrayList();
			DbSqlCard.SelectCardGoinTime(array1);

			ArrayList array2 = new ArrayList();
			DbSqlCard.SelectCardTodayTime(array2, DateTime.Now);

			foreach(object o in array0)
			{
				DtCard card = (DtCard)o;
				itm = new ListViewItem();
				card.SetLVItemPOSTimer(itm);
				itm.StateImageIndex = 0;
				itm.BackColor = Color.Yellow;
				listView_auto.Items.Add(itm);
			}

			foreach(object o in array1)
			{
				DtCard card = (DtCard)o;
				itm = new ListViewItem();
				card.SetLVItemPOSTimer(itm);
				itm.StateImageIndex = 0;
				listView_auto.Items.Add(itm);
			}

			foreach(object o in array2)
			{
				DtCard card = (DtCard)o;
				itm = new ListViewItem();
				card.SetLVItemPOSTimer(itm);
				itm.StateImageIndex = 1;
				listView_auto.Items.Add(itm);
			}
		}

		private void timer_auto_update_Tick(object sender, System.EventArgs e)
		{
			// Обновляем список, автоматически
			ListUpdate();
		}

		public void StartTimer()
		{
			timer_auto_update.Start();
		}

		private void UIF_POS_AutoTiming_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timer_auto_update.Stop();
		}
	}
}
