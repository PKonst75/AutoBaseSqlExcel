using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIFormListCalls.
	/// </summary>
	public class UIFormListCalls : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listViewCalls;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIFormListCalls()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIFormListCalls));
			this.listViewCalls = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_new = new System.Windows.Forms.Button();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewCalls
			// 
			this.listViewCalls.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewCalls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2,
																							this.columnHeader3,
																							this.columnHeader4,
																							this.columnHeader5});
			this.listViewCalls.Location = new System.Drawing.Point(8, 32);
			this.listViewCalls.Name = "listViewCalls";
			this.listViewCalls.Size = new System.Drawing.Size(816, 232);
			this.listViewCalls.TabIndex = 0;
			this.listViewCalls.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Тип";
			this.columnHeader1.Width = 35;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 69;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Интерес";
			this.columnHeader3.Width = 59;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "ФИО";
			this.columnHeader4.Width = 159;
			// 
			// button_new
			// 
			this.button_new.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new.Image")));
			this.button_new.Location = new System.Drawing.Point(8, 8);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 24);
			this.button_new.TabIndex = 1;
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Контакт";
			this.columnHeader5.Width = 95;
			// 
			// UIFormListCalls
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(832, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_new,
																		  this.listViewCalls});
			this.Name = "UIFormListCalls";
			this.Text = "Входящие контакты";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Вызов функции добавления нового контакта
			UIFormCall dialog = new UIFormCall();
			dialog.ShowDialog();
		}
	}
}
