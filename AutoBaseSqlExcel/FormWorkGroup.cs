using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkGroup.
	/// </summary>
	public class FormWorkGroup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxWorkGroupName;
		private System.Windows.Forms.CheckBox checkBoxFlagElement;
		private System.Windows.Forms.Button buttonOk;

		DbWorkGroup		workGroup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormWorkGroup(DbWorkGroup workGroupSrc, DbWorkGroup parentSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(workGroupSrc == null)
			{
				if(parentSrc != null) workGroup	= new DbWorkGroup(parentSrc.Code);
				else workGroup	= new DbWorkGroup();
			}
			else
				workGroup	= new DbWorkGroup(workGroupSrc);

			textBoxWorkGroupName.Text	= workGroup.Name;
			checkBoxFlagElement.Checked	= workGroup.FlagElement;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxWorkGroupName = new System.Windows.Forms.TextBox();
			this.checkBoxFlagElement = new System.Windows.Forms.CheckBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBoxWorkGroupName
			// 
			this.textBoxWorkGroupName.Location = new System.Drawing.Point(16, 40);
			this.textBoxWorkGroupName.Name = "textBoxWorkGroupName";
			this.textBoxWorkGroupName.Size = new System.Drawing.Size(408, 23);
			this.textBoxWorkGroupName.TabIndex = 1;
			this.textBoxWorkGroupName.Text = "";
			// 
			// checkBoxFlagElement
			// 
			this.checkBoxFlagElement.Location = new System.Drawing.Point(16, 72);
			this.checkBoxFlagElement.Name = "checkBoxFlagElement";
			this.checkBoxFlagElement.Size = new System.Drawing.Size(208, 24);
			this.checkBoxFlagElement.TabIndex = 2;
			this.checkBoxFlagElement.Text = "Флаг конечного элемента";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(192, 104);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 3;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormWorkGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(440, 135);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.checkBoxFlagElement,
																		  this.textBoxWorkGroupName,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormWorkGroup";
			this.Text = "FormWorkGroup";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Запись нового элемента
			workGroup.Name			= textBoxWorkGroupName.Text;
			workGroup.FlagElement	= checkBoxFlagElement.Checked;
			if(Db.ShowFaults() != false) return;

			if(workGroup.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbWorkGroup WorkGroup
		{
			get
			{
				return workGroup;
			}
		}
	}
}
