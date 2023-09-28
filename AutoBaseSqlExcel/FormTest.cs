using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTest.
	/// </summary>
	public class FormTest : System.Windows.Forms.Form
	{
		private System.Data.DataView dataView1;
		private System.Windows.Forms.DataGrid dataGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormTest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Подготовка просмотрщика данных
			/*
			DataSet myDataSet = new DataSet();
			SqlCommand command = new SqlCommand("ВЫБОРКА_КАРТОЧКА_ЛИСТ");
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@date_start", SqlDbType.DateTime);
			command.Parameters.Add("@date_end", SqlDbType.DateTime);
			command.Parameters.Add("@no_date", SqlDbType.Bit);
			command.Parameters.Add("@owner_mask", SqlDbType.VarChar);
			command.Parameters.Add("@vin_mask", SqlDbType.VarChar);
			command.Parameters.Add("@sign_mask", SqlDbType.VarChar);
			command.Parameters.Add("@show_cancel", SqlDbType.Bit);

			command.Parameters["@date_start"].Value = DateTime.Now;
			command.Parameters["@date_end"].Value = DateTime.Now;
			command.Parameters["@no_date"].Value = true;
			command.Parameters["@owner_mask"].Value = "";
			command.Parameters["@vin_mask"].Value = "";
			command.Parameters["@sign_mask"].Value = "";
			command.Parameters["@show_cancel"].Value = true;
			command.Connection	= Form1.connection;

			SqlDataAdapter myDataAdapter = new SqlDataAdapter(command);
			myDataAdapter.Fill(myDataSet);
			dataGrid1.SetDataBinding(myDataSet, "");
			*/
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
			this.dataView1 = new System.Data.DataView();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(16, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(528, 232);
			this.dataGrid1.TabIndex = 0;
			// 
			// FormTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(560, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dataGrid1});
			this.Name = "FormTest";
			this.Text = "FormTest";
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
