using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_AutoComment.
	/// </summary>
	public class UIF_AutoComment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_comments;
		private System.Windows.Forms.ColumnHeader columnHeader0;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button button_set_executable;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_delete;
		private System.Windows.Forms.Button button_unset_executable;
		private System.Windows.Forms.Button button_set_exe;

		long code_auto = 0;

		public UIF_AutoComment(long codeauto)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			code_auto = codeauto;
			RenewList();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_AutoComment));
			this.listView_comments = new System.Windows.Forms.ListView();
			this.columnHeader0 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.button_set_executable = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_delete = new System.Windows.Forms.Button();
			this.button_unset_executable = new System.Windows.Forms.Button();
			this.button_set_exe = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_comments
			// 
			this.listView_comments.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_comments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								this.columnHeader0,
																								this.columnHeader1,
																								this.columnHeader2,
																								this.columnHeader3});
			this.listView_comments.FullRowSelect = true;
			this.listView_comments.Location = new System.Drawing.Point(8, 24);
			this.listView_comments.Name = "listView_comments";
			this.listView_comments.Size = new System.Drawing.Size(672, 232);
			this.listView_comments.TabIndex = 0;
			this.listView_comments.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader0
			// 
			this.columnHeader0.Text = "Примечание";
			this.columnHeader0.Width = 265;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 91;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Выполнил";
			this.columnHeader2.Width = 123;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Выполнение";
			this.columnHeader3.Width = 159;
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(8, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 1;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_set_executable
			// 
			this.button_set_executable.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_set_executable.Image")));
			this.button_set_executable.Location = new System.Drawing.Point(144, 0);
			this.button_set_executable.Name = "button_set_executable";
			this.button_set_executable.Size = new System.Drawing.Size(24, 23);
			this.button_set_executable.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_set_executable, "Сделать исполняемым");
			this.button_set_executable.Click += new System.EventHandler(this.button_set_executable_Click);
			// 
			// button_delete
			// 
			this.button_delete.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete.Image")));
			this.button_delete.Location = new System.Drawing.Point(32, 0);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(24, 23);
			this.button_delete.TabIndex = 3;
			this.toolTip1.SetToolTip(this.button_delete, "Удалить примечание");
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// button_unset_executable
			// 
			this.button_unset_executable.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_unset_executable.Image")));
			this.button_unset_executable.Location = new System.Drawing.Point(168, 0);
			this.button_unset_executable.Name = "button_unset_executable";
			this.button_unset_executable.Size = new System.Drawing.Size(24, 23);
			this.button_unset_executable.TabIndex = 4;
			this.toolTip1.SetToolTip(this.button_unset_executable, "Снять обязательное исполнение");
			this.button_unset_executable.Click += new System.EventHandler(this.button_unset_executable_Click);
			// 
			// button_set_exe
			// 
			this.button_set_exe.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_set_exe.Image")));
			this.button_set_exe.Location = new System.Drawing.Point(280, 0);
			this.button_set_exe.Name = "button_set_exe";
			this.button_set_exe.Size = new System.Drawing.Size(24, 23);
			this.button_set_exe.TabIndex = 5;
			this.toolTip1.SetToolTip(this.button_set_exe, "Отметить выполнение");
			this.button_set_exe.Click += new System.EventHandler(this.button_set_exe_Click);
			// 
			// UIF_AutoComment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_set_exe,
																		  this.button_unset_executable,
																		  this.button_delete,
																		  this.button_set_executable,
																		  this.button1,
																		  this.listView_comments});
			this.Name = "UIF_AutoComment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Примечания к автомобилю";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Новое примечание
			string txt = UserInterface.Selector_String("Введите текст примечания", "");
			txt = txt.Trim();
			if(txt == "") return;

			if(DbSqlAutoComment.Insert(code_auto, txt) == 0) return;

			RenewList();			
		}

		public void RenewList()
		{
			// Перезачитывем весь список
			listView_comments.Items.Clear();
			ArrayList list = new ArrayList();
			DbSqlAutoComment.SelectInArray(list, code_auto);

			foreach(object o in list)
			{
				DtAutoComment cmt = (DtAutoComment)o;
				ListViewItem itm = listView_comments.Items.Add("");
				cmt.SetLVItem(itm);
			}
		}

		private void button_set_executable_Click(object sender, System.EventArgs e)
		{
			// Сделать данное примечание исполняемым
			// Управление списком примечаний
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			Db.LongPair pair = (Db.LongPair)item.Tag;
			if(pair.data_main == 0) return;
			if(pair.data_add == 0) return;

			// Устанавливаем отметку о необходимости исполнения
			if(DbSqlAutoComment.SetExecutable(pair.data_main, pair.data_add) == false) return;

			RenewList();
		}

		private void button_delete_Click(object sender, System.EventArgs e)
		{
			// Удаляем примечание
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			Db.LongPair pair = (Db.LongPair)item.Tag;
			if(pair.data_main == 0) return;
			if(pair.data_add == 0) return;

			// Удаляем примечание
			if(DbSqlAutoComment.Delete(pair.data_main, pair.data_add) == false) return;

			RenewList();
		}

		private void button_unset_executable_Click(object sender, System.EventArgs e)
		{
			// Удаляем примечание
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			Db.LongPair pair = (Db.LongPair)item.Tag;
			if(pair.data_main == 0) return;
			if(pair.data_add == 0) return;

			// Удаляем примечание
			if(DbSqlAutoComment.UnsetExecutable(pair.data_main, pair.data_add) == false) return;

			RenewList();
		}

		private void button_set_exe_Click(object sender, System.EventArgs e)
		{
			// Отметка о выполнении
			ListViewItem item = Db.GetItemSelected(listView_comments);
			if(item == null) return;
			Db.LongPair pair = (Db.LongPair)item.Tag;
			if(pair.data_main == 0) return;
			if(pair.data_add == 0) return;

			// Запрашиваем подпись исполнителя
			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего выполневшего примечание", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(staff.SelectedLong == 0) return;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return;
			long person_exe = (long)master.GetData("КОД_ПЕРСОНАЛ");
			if(person_exe == 0) return;
			// Запрашиваем примечание к выполнению
			string comment_exe = UserInterface.Selector_String("Введите дополнение к выполненому примечанию", "");

			// Устанавливаем исполнителя
			if(DbSqlAutoComment.SetExe(pair.data_main, pair.data_add, person_exe, comment_exe) == false) return;

			RenewList();
		}
	}
}
