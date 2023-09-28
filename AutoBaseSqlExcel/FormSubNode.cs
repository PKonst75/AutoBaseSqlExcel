using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSubNode.
	/// </summary>
	public class FormSubNode : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxImplement;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbSubNode	subNode;

		public FormSubNode(DbNode nodeSrc, DbSubNode subNodeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(subNodeSrc == null)
				subNode		= new DbSubNode(nodeSrc);
			else
				subNode		= new DbSubNode(subNodeSrc);

			textBoxName.Text		= subNode.Name;
			textBoxImplement.Text	= subNode.Implement;
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxImplement = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(8, 32);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(408, 23);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Наименование";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Применимость";
			// 
			// textBoxImplement
			// 
			this.textBoxImplement.Location = new System.Drawing.Point(8, 88);
			this.textBoxImplement.Name = "textBoxImplement";
			this.textBoxImplement.Size = new System.Drawing.Size(408, 23);
			this.textBoxImplement.TabIndex = 3;
			this.textBoxImplement.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(176, 120);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormSubNode
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(424, 149);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxImplement,
																		  this.label2,
																		  this.label1,
																		  this.textBoxName});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormSubNode";
			this.Text = "Подузел";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Запись нового подузла
			subNode.Name		= textBoxName.Text;
			subNode.Implement	= textBoxImplement.Text;
			if(Db.ShowFaults() == true) return;
			if(subNode.Write() != true) return;

			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		public DbSubNode SubNode
		{
			get
			{
				return subNode;
			}
		}
	}
}
