using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_CommonReason_List.
	/// </summary>
	public class UIF_CommonReason_List : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button button_new;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DtCommonReason selected_reason = null;

		public UIF_CommonReason_List()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			FillList();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_CommonReason_List));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.button_new = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(560, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Причина";
			this.columnHeader1.Width = 459;
			// 
			// button_new
			// 
			this.button_new.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new.Image")));
			this.button_new.Location = new System.Drawing.Point(8, 8);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(24, 23);
			this.button_new.TabIndex = 1;
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// UIF_CommonReason_List
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_new,
																		  this.listView1});
			this.Name = "UIF_CommonReason_List";
			this.Text = "Список причин";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Добваление новой причины
			string txt = "";
			txt = UserInterface.Selector_String("Введите описание причины", "");
			if(txt == "") return;

			// Попытка записать новую причину
			DtCommonReason reason = new DtCommonReason();
			reason.SetData("ОПИСАНИЕ", (object)txt);
			reason = (DtCommonReason)DbSqlCommonReason.Insert(reason);
			if(reason == null) return;
			long code = (long)reason.GetData("КОД_ПРИЧИНА");
			reason = DbSqlCommonReason.Find(code);
			if(reason == null) return;

			// Успешно записали новый элемент, осуществляем автоматический выбор
			selected_reason = reason;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор элемента
			// Двойной счелчек - выбор элемента во внешний список
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			DtCommonReason reason = DbSqlCommonReason.Find(code);
			if(reason == null) return;
			selected_reason = reason;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		void FillList()
		{
			listView1.Items.Clear();
			DbSqlCommonReason.SelectInList(listView1);
		}
	}
}
