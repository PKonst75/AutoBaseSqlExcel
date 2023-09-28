using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageBrand.
	/// </summary>
	public class FormManageBrand : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listViewBrand;
		private System.Windows.Forms.ListView listViewModel;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button buttonNewBrand;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button buttonAddModel;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonDelModel;
		private System.ComponentModel.IContainer components;

		public FormManageBrand()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Bitmap image;
			image = new Bitmap(".\\Brand\\Неизвестно.bmp");
			imageList1.Images.Add(image);
			DbBrand.FillList(listViewBrand);

			// Заполняем окно общим списком автомобилей
			DbAutoModel.FillList(listView1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageBrand));
			this.listViewBrand = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listViewModel = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonNewBrand = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.buttonAddModel = new System.Windows.Forms.Button();
			this.buttonDelModel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listViewBrand
			// 
			this.listViewBrand.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.listViewBrand.HideSelection = false;
			this.listViewBrand.LargeImageList = this.imageList1;
			this.listViewBrand.Location = new System.Drawing.Point(8, 8);
			this.listViewBrand.MultiSelect = false;
			this.listViewBrand.Name = "listViewBrand";
			this.listViewBrand.Size = new System.Drawing.Size(96, 288);
			this.listViewBrand.TabIndex = 0;
			this.listViewBrand.SelectedIndexChanged += new System.EventHandler(this.listViewBrand_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listViewModel
			// 
			this.listViewModel.AllowDrop = true;
			this.listViewModel.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1});
			this.listViewModel.Location = new System.Drawing.Point(136, 8);
			this.listViewModel.Name = "listViewModel";
			this.listViewModel.Size = new System.Drawing.Size(336, 144);
			this.listViewModel.TabIndex = 1;
			this.listViewModel.View = System.Windows.Forms.View.Details;
			this.listViewModel.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewModel_DragDrop);
			this.listViewModel.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewModel_DragEnter);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 285;
			// 
			// buttonNewBrand
			// 
			this.buttonNewBrand.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewBrand.Image")));
			this.buttonNewBrand.Location = new System.Drawing.Point(104, 8);
			this.buttonNewBrand.Name = "buttonNewBrand";
			this.buttonNewBrand.Size = new System.Drawing.Size(24, 23);
			this.buttonNewBrand.TabIndex = 2;
			this.buttonNewBrand.Click += new System.EventHandler(this.buttonNewBrand_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader2});
			this.listView1.Location = new System.Drawing.Point(136, 184);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(336, 112);
			this.listView1.TabIndex = 3;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Модель";
			this.columnHeader2.Width = 294;
			// 
			// buttonAddModel
			// 
			this.buttonAddModel.Location = new System.Drawing.Point(136, 160);
			this.buttonAddModel.Name = "buttonAddModel";
			this.buttonAddModel.Size = new System.Drawing.Size(24, 23);
			this.buttonAddModel.TabIndex = 4;
			this.buttonAddModel.Click += new System.EventHandler(this.buttonAddModel_Click);
			// 
			// buttonDelModel
			// 
			this.buttonDelModel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonDelModel.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelModel.Image")));
			this.buttonDelModel.Location = new System.Drawing.Point(448, 152);
			this.buttonDelModel.Name = "buttonDelModel";
			this.buttonDelModel.Size = new System.Drawing.Size(24, 23);
			this.buttonDelModel.TabIndex = 5;
			this.buttonDelModel.Click += new System.EventHandler(this.buttonDelModel_Click);
			// 
			// FormManageBrand
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDelModel,
																		  this.buttonAddModel,
																		  this.listView1,
																		  this.buttonNewBrand,
																		  this.listViewModel,
																		  this.listViewBrand});
			this.Name = "FormManageBrand";
			this.Text = "Управление брендами, моделями";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNewBrand_Click(object sender, System.EventArgs e)
		{
			// Добавление нового бренда
			FormBrand dialog = new FormBrand(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			listViewBrand.Items.Add(dialog.Brand.LVItem);
		}

		private void buttonAddModel_Click(object sender, System.EventArgs e)
		{
			// Добавляем выбранные модели в список моделей бренда

			// Определяем выбранный бренд
			if(listViewBrand.SelectedItems.Count == 0) return;
			ListViewItem item = listViewBrand.SelectedItems[0];
			if(item == null) return;
			DbBrand brand = (DbBrand)item.Tag;
			if(brand == null) return;

			// Добавляем в список
			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				DbAutoModel model = (DbAutoModel)itm.Tag;
				if(model != null)
				{
					if(brand.WriteModel(model, true) == true)
					{
						listViewModel.Items.Add(model.LVItem);
					}
				}
			}
			Db.ShowFaults();
		}

		protected void listViewBrand_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Очищаем предыдущий выбор
			listViewModel.Items.Clear();

			// Определяем выбранный бренд
			if(listViewBrand.SelectedItems.Count == 0) return;
			ListViewItem item = listViewBrand.SelectedItems[0];
			if(item == null) return;
			DbBrand brand = (DbBrand)item.Tag;
			if(brand == null) return;

			DbAutoModel.FillList(listViewModel, brand);
		}

		private void buttonDelModel_Click(object sender, System.EventArgs e)
		{
			// Удаляем модель из списка

			// Определяем выбранный бренд
			if(listViewBrand.SelectedItems.Count == 0) return;
			ListViewItem item = listViewBrand.SelectedItems[0];
			if(item == null) return;
			DbBrand brand = (DbBrand)item.Tag;
			if(brand == null) return;

			// Удаляем из списока
			foreach(ListViewItem itm in listViewModel.SelectedItems)
			{
				DbAutoModel model = (DbAutoModel)itm.Tag;
				if(model != null)
				{
					if(brand.WriteModel(model, false) == true)
					{
						listViewModel.Items.Remove(itm);
					}
				}
			}
			Db.ShowFaults();
		}

		protected void listView1_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button != MouseButtons.Left) return;
			if(listView1.SelectedItems.Count == 0) return;
			// Начинаем операцию по перетаскиванию
			this.DoDragDrop("Из списка моделей", DragDropEffects.Move);
		}
		protected void listViewModel_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		protected void listViewModel_DragDrop(object sender, DragEventArgs e)
		{
			// Проверяем на правильность перетаскиваемого объекта
			if(e.Data.GetDataPresent(DataFormats.Text) != true) return;
			if((string)e.Data.GetData(DataFormats.Text) != "Из списка моделей") return;

			// Определяем выбранный бренд
			if(listViewBrand.SelectedItems.Count == 0) return;
			ListViewItem item = listViewBrand.SelectedItems[0];
			if(item == null) return;
			DbBrand brand = (DbBrand)item.Tag;
			if(brand == null) return;

			// Добавляем в список
			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				DbAutoModel model = (DbAutoModel)itm.Tag;
				if(model != null)
				{
					if(brand.WriteModel(model, true) == true)
					{
						listViewModel.Items.Add(model.LVItem);
					}
				}
			}
			Db.ShowFaults();
		}
	}
}
