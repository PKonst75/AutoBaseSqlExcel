using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStaffList.
	/// </summary>
	public class FormStaffList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonChange;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonExcelDoneWorks;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;

		private DbStaff selectedStaff;

		public FormStaffList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Обновление списка персонала - упрощаем на одно действие
			listView1.Items.Clear();
			DbStaff.FillList(listView1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStaffList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonExcelDoneWorks = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Фамилмя";
			this.columnHeader1.Width = 140;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Имя Отчество";
			this.columnHeader2.Width = 200;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 3;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(232, 0);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 4;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonReplace
			// 
			this.buttonReplace.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReplace.Image")));
			this.buttonReplace.Location = new System.Drawing.Point(256, 0);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.Size = new System.Drawing.Size(24, 23);
			this.buttonReplace.TabIndex = 5;
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.buttonOk.Location = new System.Drawing.Point(224, 264);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonExcelDoneWorks
			// 
			this.buttonExcelDoneWorks.Location = new System.Drawing.Point(392, 0);
			this.buttonExcelDoneWorks.Name = "buttonExcelDoneWorks";
			this.buttonExcelDoneWorks.Size = new System.Drawing.Size(24, 23);
			this.buttonExcelDoneWorks.TabIndex = 7;
			this.buttonExcelDoneWorks.Text = "E";
			this.buttonExcelDoneWorks.Click += new System.EventHandler(this.buttonExcelDoneWorks_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "З/П";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// FormStaffList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 293);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonExcelDoneWorks,
																		  this.buttonOk,
																		  this.buttonReplace,
																		  this.buttonDelete,
																		  this.buttonChange,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormStaffList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Персонал";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Заведение нового сотрудника
			FormStaff dialog = new FormStaff(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Staff.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка персонала
			listView1.Items.Clear();
			DbStaff.FillList(listView1);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение существующего элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;
			// Изменение сотрудника
			FormStaff dialog = new FormStaff(staff);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Staff.SetLVItem(item);
		}

		public DbStaff SelectedStaff
		{
			get
			{
				return selectedStaff;
			}
		}

		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				return listView1.SelectedItems;
			}
		}
		public ListView List
		{
			get
			{
				return listView1;
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;
			selectedStaff = staff;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Удаление персонала из базы данных
			// Админовская привилегия
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;

			if(!staff.Delete()) return;
			listView1.Items.Remove(item);
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			// Замена во всей базе одного персонала на другой
			// Админовская привилегия
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;

			// Запрос нового персонала
			FormStaffList dialog = new FormStaffList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			staff.Replace(dialog.SelectedStaff);
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Успешно завершаем выполнение диалога
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;
			selectedStaff = staff;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonExcelDoneWorks_Click(object sender, System.EventArgs e)
		{
			// Выгружаем в EXCEL список выполненных за период работ
			// Делаем запрос на период времени
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			// Запускаем процесс!!!
			ExcelCardWorkStaff.DownloadList(listView1, dialog.StartDate, dialog.EndDate);
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Запуск всплывающего меню
			// Вызов меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem1.Enabled = false;
				
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem1.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Печать расчетки на зарплату
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DbStaff staff = (DbStaff)item.Tag;
			if(staff == null) return;

			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			int year = dialog.SelectedDate.Year;
			int month = dialog.SelectedDate.Month;

			DbPrintSalary prn = new DbPrintSalary(staff.Code, year, month);
			prn.Print();
		}
	}
}
