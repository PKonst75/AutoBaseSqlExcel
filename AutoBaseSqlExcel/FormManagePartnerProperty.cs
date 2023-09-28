using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManagePartnerProperty.
	/// </summary>
	public class FormManagePartnerProperty : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.Button button_remove;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormManagePartnerProperty()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первоначальное заполнение
			FillList("");
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
			this.button_new = new System.Windows.Forms.Button();
			this.button_remove = new System.Windows.Forms.Button();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader5,
																						this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 48);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(648, 312);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Контрагент";
			this.columnHeader1.Width = 240;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Безнал";
			this.columnHeader2.Width = 73;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Скидка";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Примечание";
			this.columnHeader4.Width = 180;
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(8, 24);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 23);
			this.button_new.TabIndex = 1;
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// button_remove
			// 
			this.button_remove.Location = new System.Drawing.Point(32, 24);
			this.button_remove.Name = "button_remove";
			this.button_remove.Size = new System.Drawing.Size(24, 23);
			this.button_remove.TabIndex = 2;
			this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "№";
			this.columnHeader5.Width = 80;
			// 
			// FormManagePartnerProperty
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(664, 381);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_remove,
																		  this.button_new,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormManagePartnerProperty";
			this.Text = "Управление свойствами контрагентов";
			this.ResumeLayout(false);

		}
		#endregion

		protected void FillList(string name_mask)
		{
			// Обновить список заказ-нарядов
			// Очистка предыдущих данных
			listView1.Items.Clear();
			// Подготовка команды к поиску
			DbSqlPartnerProperty.PrepareSelect(name_mask);
			// Заполнение листа
			this.Cursor = Cursors.WaitCursor;
			DbSql.FillList(listView1, DbSqlPartnerProperty.select, new DbSql.DelegateMakeLVItem(DbSqlPartnerProperty.MakeLVItem));
			this.Cursor = Cursors.Default;
		}

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Добавление нового свойства контрагента
			FormPartnerProperty dialog = new FormPartnerProperty(0);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			ListViewItem item = listView1.Items.Add("Новый");
			dialog.Element.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Изменение свойства контрагента
			long data;
			ListViewItem item;
			if(listView1.SelectedItems != null && listView1.SelectedItems[0] != null)
			{
				item = listView1.SelectedItems[0];
				data = (long)item.Tag;
			}
			else
			{
				MessageBox.Show("Ошибка");
				return;
			}
			FormPartnerProperty dialog = new FormPartnerProperty(data);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Element.SetLVItem(item);
		}

		private void button_remove_Click(object sender, System.EventArgs e)
		{
			// Удаление элемента из списка
			// Изменение свойства контрагента
			long data;
			ListViewItem item;
			if(listView1.SelectedItems != null && listView1.SelectedItems[0] != null)
			{
				item = listView1.SelectedItems[0];
				data = (long)item.Tag;
			}
			else
			{
				MessageBox.Show("Ошибка");
				return;
			}
			if(DbSqlPartnerProperty.Delete(data) == false) return;
			listView1.Items.Remove(item);
		}
	}
}
