using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_LicenceVehicleList.
	/// </summary>
	public class UIF_LicenceVehicleList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_list;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button_new_licence;
		private System.Windows.Forms.Button button_update_list;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_view;

		public CS_LicenceVehicle licence_selected = null;

		public UIF_LicenceVehicleList(ArrayList array)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			if(array != null)
			{
				foreach(object o in array)
				{
					ListViewItem item = listView_list.Items.Add("");
					CS_LicenceVehicle licence = (CS_LicenceVehicle)o;
					licence.SetLVItem(item);
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_LicenceVehicleList));
			this.listView_list = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.button_new_licence = new System.Windows.Forms.Button();
			this.button_update_list = new System.Windows.Forms.Button();
			this.button_view = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_list
			// 
			this.listView_list.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2,
																							this.columnHeader3,
																							this.columnHeader4,
																							this.columnHeader5,
																							this.columnHeader6});
			this.listView_list.FullRowSelect = true;
			this.listView_list.Location = new System.Drawing.Point(0, 24);
			this.listView_list.Name = "listView_list";
			this.listView_list.Size = new System.Drawing.Size(728, 352);
			this.listView_list.TabIndex = 0;
			this.listView_list.View = System.Windows.Forms.View.Details;
			this.listView_list.DoubleClick += new System.EventHandler(this.listView_list_DoubleClick);
			this.listView_list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_list_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 47;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Рег знак";
			this.columnHeader2.Width = 66;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Свидетельсво";
			this.columnHeader3.Width = 89;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Дата";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Автомобиль";
			this.columnHeader5.Width = 142;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Владелец";
			this.columnHeader6.Width = 213;
			// 
			// button_new_licence
			// 
			this.button_new_licence.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_licence.Image")));
			this.button_new_licence.Location = new System.Drawing.Point(24, 0);
			this.button_new_licence.Name = "button_new_licence";
			this.button_new_licence.Size = new System.Drawing.Size(24, 23);
			this.button_new_licence.TabIndex = 1;
			this.button_new_licence.Click += new System.EventHandler(this.button_new_licence_Click);
			// 
			// button_update_list
			// 
			this.button_update_list.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update_list.Image")));
			this.button_update_list.Name = "button_update_list";
			this.button_update_list.Size = new System.Drawing.Size(24, 23);
			this.button_update_list.TabIndex = 2;
			this.button_update_list.Click += new System.EventHandler(this.button_update_list_Click);
			// 
			// button_view
			// 
			this.button_view.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_view.Image")));
			this.button_view.Location = new System.Drawing.Point(48, 0);
			this.button_view.Name = "button_view";
			this.button_view.Size = new System.Drawing.Size(24, 23);
			this.button_view.TabIndex = 3;
			this.button_view.Click += new System.EventHandler(this.button_view_Click);
			// 
			// UIF_LicenceVehicleList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(848, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_view,
																		  this.button_update_list,
																		  this.button_new_licence,
																		  this.listView_list});
			this.Name = "UIF_LicenceVehicleList";
			this.Text = "Свидетельства о регистрации";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_licence_Click(object sender, System.EventArgs e)
		{
			// Заведение данных о новом свидетельсве о регистрации
			UserInterface.LicenceVehicle(0, null, 0, 0);
		}

		private void button_update_list_Click(object sender, System.EventArgs e)
		{
			listView_list.Items.Clear();
			ArrayList array = new ArrayList();
			DbSqlLicenceVehicle.SelectInArray(array);
			foreach(object o in array)
			{
				ListViewItem item = listView_list.Items.Add("");
				CS_LicenceVehicle licence = (CS_LicenceVehicle)o;
				licence.SetLVItem(item);
			}
		}

		private void listView_list_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			string mask = "";
			ArrayList array = new ArrayList();

			// Поиск по колонкам
			switch(e.Column)
			{
				case 1:
					mask = UserInterface.Selector_String("Регистрационный знак", "");
					if(mask == "") return;
					mask = "%" + mask + "%";
					DbSqlLicenceVehicle.SelectInArrayVehicleNumber(array, mask);
					listView_list.Items.Clear();
					foreach(object o in array)
					{
						ListViewItem item = listView_list.Items.Add("");
						CS_LicenceVehicle licence = (CS_LicenceVehicle)o;
						licence.SetLVItem(item);
					}
					break;
				case 2:
					mask = UserInterface.Selector_String("Номер свидетельства ТС", "");
					if(mask == "") return;
					mask = "%" + mask + "%";
					DbSqlLicenceVehicle.SelectInArrayLicenceNumber(array, mask);
					listView_list.Items.Clear();
					foreach(object o in array)
					{
						ListViewItem item = listView_list.Items.Add("");
						CS_LicenceVehicle licence = (CS_LicenceVehicle)o;
						licence.SetLVItem(item);
					}
					break;
				default:
					break;
			}
			
		}

		private void listView_list_DoubleClick(object sender, System.EventArgs e)
		{
			// Двойной счелчек - выбор элемента во внешний список
			ListViewItem item = Db.GetItemPosition(listView_list);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			CS_LicenceVehicle licence = DbSqlLicenceVehicle.Find(code);
			if(licence == null) return;
			licence_selected = licence;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_view_Click(object sender, System.EventArgs e)
		{
			// Просмотр свойств
			// Выбор изменяемого элемента
			ListViewItem item = Db.GetItemSelected(listView_list);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			// Изменение элемента
			CS_LicenceVehicle licence = DbSqlLicenceVehicle.Find(code);
			if(licence == null) return;
			UIF_LicenceVehicle dialog = new UIF_LicenceVehicle(licence, "");;
			dialog.ShowDialog();
		}
	}
}
