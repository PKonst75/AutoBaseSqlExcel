using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Список действий произведенных с карточкой
	/// </summary>
	public class FormListCardAction : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView		listView1;
		private System.Windows.Forms.ColumnHeader	head_action;
		private System.Windows.Forms.ColumnHeader	head_time;
		private System.Windows.Forms.ColumnHeader	head_comment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormListCardAction(DtCard card)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Установка наименование окна
			this.Text = "Карточка №" + card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + card.GetData("ГОД_КАРТОЧКА").ToString() + " от " + card.GetData("ДАТА").ToString();

			head_action = new System.Windows.Forms.ColumnHeader();
			head_action.Width = 120;
			head_action.Text = "Действие";
			head_time = new System.Windows.Forms.ColumnHeader();
			head_time.Width = 120;
			head_time.Text = "Дата и время";
			head_comment = new System.Windows.Forms.ColumnHeader();
			head_comment.Width = 320;
			head_comment.Text = "Примечание";
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.head_action,
																						this.head_time,
																						this.head_comment
																					});

			// Заполняем
			DbSqlCardAction.SelectInList(listView1, card);
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
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(608, 296);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// FormListCardAction
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "FormListCardAction";
			this.Text = "FormListCardAction";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
