using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDiscountList.
	/// </summary>
	public class FormDiscountList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.Button button_show;
		private System.Windows.Forms.Button button_give;
		private System.ComponentModel.IContainer components;

		public FormDiscountList()
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
			this.components = new System.ComponentModel.Container();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_new = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_update = new System.Windows.Forms.Button();
			this.button_show = new System.Windows.Forms.Button();
			this.button_give = new System.Windows.Forms.Button();
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
			this.listView1.Location = new System.Drawing.Point(0, 48);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(664, 224);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Скидка";
			this.columnHeader2.Width = 54;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Клиент";
			this.columnHeader3.Width = 261;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Примечание";
			this.columnHeader4.Width = 267;
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(40, 24);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 23);
			this.button_new.TabIndex = 1;
			this.toolTip1.SetToolTip(this.button_new, "Новая группа карт");
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// button_update
			// 
			this.button_update.Location = new System.Drawing.Point(64, 24);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_update, "Изменение свойств дисконтной карты");
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_show
			// 
			this.button_show.Location = new System.Drawing.Point(0, 24);
			this.button_show.Name = "button_show";
			this.button_show.Size = new System.Drawing.Size(24, 23);
			this.button_show.TabIndex = 3;
			this.button_show.Click += new System.EventHandler(this.button_show_Click);
			// 
			// button_give
			// 
			this.button_give.Location = new System.Drawing.Point(136, 24);
			this.button_give.Name = "button_give";
			this.button_give.Size = new System.Drawing.Size(24, 23);
			this.button_give.TabIndex = 4;
			this.toolTip1.SetToolTip(this.button_give, "Отметить выдачу карточки");
			this.button_give.Click += new System.EventHandler(this.button_give_Click);
			// 
			// FormDiscountList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_give,
																		  this.button_show,
																		  this.button_update,
																		  this.button_new,
																		  this.listView1});
			this.Name = "FormDiscountList";
			this.Text = "Дисконтные карты";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Запрос на количество создаваемых карт
			FormSelectString dialog = new FormSelectString("Количество новых карт", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			long count = dialog.SelectedLong;
			if(count <= 0) return;
			// Запрос на скидку по создаваемым картам
			dialog = new FormSelectString("Скидка по картам", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			float discount = dialog.SelectedFloat;
			if(discount > 10 && discount <= 0) return;
			// Добавляем новые карточки
			long count_success = 0;
			for(long l=0; l < count; l++)
			{
				if(DbSqlDiscount.Insert(discount) == true)
				{
					count_success++;
				}
			}
			// Перезаполненние листа карточек
			listView1.Items.Clear();
			DbSqlDiscount.PrepareSelect(0, 0);
			DbSql.FillList(listView1, DbSqlDiscount.select, new DbSql.DelegateMakeLVItem(DbSqlDiscount.MakeLVItem));
			// Отчет о добавленных
			MessageBox.Show("Добавлено " + count_success.ToString() + " новых карточек");
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Организация поиска и фильтров
			// Поиск по
			switch(e.Column)
			{
				case 0:
					// Поиск по интервалу кодов
					FormSelectNumberInterval dialog = new FormSelectNumberInterval();
					if(dialog.ShowDialog() != DialogResult.OK) return;
					long from = dialog.SelectedLong_from;
					long to = dialog.SelectedLong_to;
					listView1.Items.Clear();
					DbSqlDiscount.PrepareSelect(from, to);
					DbSql.FillList(listView1, DbSqlDiscount.select, new DbSql.DelegateMakeLVItem(DbSqlDiscount.MakeLVItem));
					break;
				default:
					break;
			}
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранной дисконтной карты
			if(listView1.SelectedItems == null) return;
			if(listView1.SelectedItems.Count == 0) return;
			ListViewItem item = listView1.SelectedItems[0];
			if(item == null) return;
			if((long)item.Tag == 0) return;
			DtDiscount discount = DbSqlDiscount.Find((long)item.Tag);
			if(discount == null) return;
			FormDiscount dialog = new FormDiscount(discount);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Перезачитываем нужный элемент
			discount = DbSqlDiscount.Find((long)item.Tag);
			if(discount != null)
				discount.SetLVItem(item);
		}

		private void button_show_Click(object sender, System.EventArgs e)
		{
			// Показать весь список карт
			listView1.Items.Clear();
			DbSqlDiscount.PrepareSelect(0, 0);
			DbSql.FillList(listView1, DbSqlDiscount.select, new DbSql.DelegateMakeLVItem(DbSqlDiscount.MakeLVItem));
		}

		private void button_give_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранной дисконтной карты
			if(listView1.SelectedItems == null) return;
			if(listView1.SelectedItems.Count == 0) return;
			ListViewItem item = listView1.SelectedItems[0];
			if(item == null) return;
			if((long)item.Tag == 0) return;
			if(DbSqlDiscount.Give((long)item.Tag ) != true) return;
			DtDiscount discount = DbSqlDiscount.Find((long)item.Tag);
			if(discount != null)
				discount.SetLVItem(item);
		}
	}
}
