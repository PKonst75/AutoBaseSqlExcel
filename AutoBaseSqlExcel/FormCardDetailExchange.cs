using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardDetailExchange.
	/// </summary>
	public class FormCardDetailExchange : System.Windows.Forms.Form
	{
		
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button_cancel;

		bool recive_flag;

		public FormCardDetailExchange(bool recive, DbCard card)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(recive == true)
			{
				// Получение запчастей
				recive_flag		= true;
				DbCardDetail.FillListReciveReturn(listView1, card, true);
				this.Text		= "Получение запчастей со склада";
			}
			else
			{
				// Возврат запчастей
				recive_flag		= false;
				DbCardDetail.FillListReciveReturn(listView1, card, false);
				this.Text		= "Возврат запчастей на склад";
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
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
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(856, 296);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 25;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Код";
			this.columnHeader2.Width = 123;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Наименование";
			this.columnHeader3.Width = 337;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Кол-во";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цена";
			this.columnHeader5.Width = 109;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Сумма";
			this.columnHeader6.Width = 128;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Гарантия";
			this.columnHeader7.Width = 39;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(312, 320);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "ОК";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(400, 320);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.TabIndex = 2;
			this.button_cancel.Text = "Отмена";
			// 
			// FormCardDetailExchange
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(872, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_cancel,
																		  this.button1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCardDetailExchange";
			this.Text = "FormCardDetailExchange";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Начало обработки
			// Получаем электронную подпись
			bool	action = true;
			DbStaff staff = DbStaff.GetByESign("Электронная подпись");
			if(staff == null) return;
			foreach(ListViewItem itm in this.listView1.SelectedItems)
			{
				DbCardDetail dtl = (DbCardDetail)itm.Tag;
				action = true;
				if(recive_flag == true){
					if(dtl.Check == true) action = false;
					if(dtl.Outer == true) action = false;
					if(dtl.Recived == true) action = false;
					if(action)
					{
						if(dtl.WriteRecive(staff.Code) == true)
						{
							listView1.Items.Remove(itm);
						}
					}
				}
				else
				{	
					if(dtl.Recived == false) action = false;
					if(action)
					{
						if(dtl.WriteReturn(staff.Code) == true)
						{
							listView1.Items.Remove(itm);
						}
					}
				}
			}
		}
	}
}
