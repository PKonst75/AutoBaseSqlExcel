using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Selector_Array.
	/// </summary>
	public class UIF_Selector_Array : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listView_list;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		long selected_code = 0;
		object selectedObject = null;

		public UIF_Selector_Array(ArrayList array)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			foreach(object o in array)
			{
				Dt element	= (Dt)o;
				ListViewItem item = new ListViewItem("ОШИБКА");
				Dt.Pair pair = new Dt.Pair(element);
				//item.Tag	= element.Code();
				item.Tag = pair;
				item.Text	= element.Title();

				listView_list.Items.Add(item);
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
            this.listView_list = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView_list
            // 
            this.listView_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView_list.HideSelection = false;
            this.listView_list.Location = new System.Drawing.Point(10, 18);
            this.listView_list.Name = "listView_list";
            this.listView_list.Size = new System.Drawing.Size(637, 245);
            this.listView_list.TabIndex = 0;
            this.listView_list.UseCompatibleStateImageBehavior = false;
            this.listView_list.View = System.Windows.Forms.View.Details;
            this.listView_list.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Наименование";
            this.columnHeader1.Width = 611;
            // 
            // UIF_Selector_Array
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(656, 273);
            this.Controls.Add(this.listView_list);
            this.Name = "UIF_Selector_Array";
            this.Text = "Выбор элемента";
            this.ResumeLayout(false);

		}
		#endregion

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Двойной счелчек инициирует выбор элемента
			this.DialogResult = DialogResult.Cancel;
			ListViewItem item = Db.GetItemSelected(listView_list);
			if(item == null) return;
			if(item.Tag	== null) return;

			//long code = (long)item.Tag;
			Dt.Pair pair = (Dt.Pair)item.Tag;
			long code = pair.Code;
			object o = pair.Obj;

			if(code == 0) return;
			if (o == null) return;
			selected_code	= code;
			selectedObject = o;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public long SelectedCode
		{
			get
			{
				return selected_code;
			}
		}
		public object SelectedObject
		{
			get
			{
				return selectedObject;
			}
		}
	}
}
