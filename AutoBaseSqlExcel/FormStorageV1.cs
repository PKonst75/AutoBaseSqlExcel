using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStorageV1.
	/// </summary>
	public class FormStorageV1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.Button button_noppp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormStorageV1()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStorageV1));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.button_update = new System.Windows.Forms.Button();
			this.button_noppp = new System.Windows.Forms.Button();
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
			this.listView1.Location = new System.Drawing.Point(0, 64);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(928, 208);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 91;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Исполнение";
			this.columnHeader2.Width = 93;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Цвет";
			this.columnHeader3.Width = 75;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "VIN";
			this.columnHeader4.Width = 122;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Кузов";
			this.columnHeader5.Width = 113;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Дата";
			this.columnHeader6.Width = 72;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Примечание";
			this.columnHeader7.Width = 101;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Дата";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Покупатель";
			this.columnHeader9.Width = 97;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Примечание";
			this.columnHeader10.Width = 88;
			// 
			// button_update
			// 
			this.button_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update.Image")));
			this.button_update.Location = new System.Drawing.Point(0, 40);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 1;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_noppp
			// 
			this.button_noppp.Location = new System.Drawing.Point(384, 0);
			this.button_noppp.Name = "button_noppp";
			this.button_noppp.Size = new System.Drawing.Size(72, 23);
			this.button_noppp.TabIndex = 2;
			this.button_noppp.Text = "Без ППП";
			this.button_noppp.Click += new System.EventHandler(this.button_noppp_Click);
			// 
			// FormStorageV1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(928, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_noppp,
																		  this.button_update,
																		  this.listView1});
			this.Name = "FormStorageV1";
			this.Text = "Управление автомобильным запасом";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновление списка
			listView1.Items.Clear();
			DbSqlAuto.SelectInListStorageV1(listView1);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Разные колонки - разные варианты поиска
			switch (e.Column)
			{
				case 3:
				{
					// Поиск по VIN
					FormSelectString dialog = new FormSelectString("Поиск по VIN номеру", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlAuto.SelectInListStorageV1_Vin(listView1, dialog.SelectedTextMask);
				}
					break;
				case 6:
				{
					// Поиск по примечанию к приходу
					FormSelectString dialog = new FormSelectString("Поиск по примечанию", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlAuto.SelectInListStorageV1_ReceiveComment(listView1, dialog.SelectedTextMask);
				}
					break;
				default:
					break;
			}
		}

		private void button_noppp_Click(object sender, System.EventArgs e)
		{
			// Список автомобилей без ППП
			listView1.Items.Clear();
			DbSqlAuto.SelectInListStorageV1_Noppp(listView1);
		}
	}
}
