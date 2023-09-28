using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailManage.
	/// </summary>
	public class FormDetailManage : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView listViewNode;
		private System.Windows.Forms.Button buttonNewNode;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ListView listViewSubNode;
		private System.Windows.Forms.Button buttonNewSubNode;
		private System.Windows.Forms.ListView listViewSubNodeDetails;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonAddDetails;
		private System.Windows.Forms.Button buttonDelSybNodeDetail;
		private System.Windows.Forms.ListView listViewSubNodeSubModel;
		private System.Windows.Forms.ListView listViewSubModel;
		private System.Windows.Forms.ListView listViewModel;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button buttonSelectSubmodel;
		private System.Windows.Forms.Button buttonNewSubModelSubNode;
		private System.Windows.Forms.Button buttonDelSubNodeSubModel;

		DbAutoSubmodel selectedAutoSubModel = null;

		public FormDetailManage()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем listView2 иконками
		//	listView2.Items.Add("Система улавливания паров бензина", 0);
		//	listView2.Items.Add("Ля ля ля", 1);

			// Список узлов
		//	Bitmap image;
		//	image = new Bitmap(".\\Nodes\\двигатель.bmp");
		//	imageList2.Images.Add(image);
		//	image = new Bitmap(".\\Nodes\\система питания.bmp");
		//	imageList2.Images.Add(image);

			Bitmap image;
			image = new Bitmap(".\\Nodes\\Неизвестно.bmp");
			imageList2.Images.Add(image);
			DbNode.FillList(listViewNode);

			image = new Bitmap(".\\SubNodes\\Неизвестно.bmp");
			imageList1.Images.Add(image);

			// Список моделей автомобилей
			DbAutoModel.FillList(listViewModel);

			this.Text = "Управление деталями / " + "Подмодель не выбрана";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailManage));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.listViewSubNode = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listViewSubNodeSubModel = new System.Windows.Forms.ListView();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.listViewNode = new System.Windows.Forms.ListView();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.buttonNewNode = new System.Windows.Forms.Button();
			this.buttonNewSubNode = new System.Windows.Forms.Button();
			this.listViewSubNodeDetails = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.buttonAddDetails = new System.Windows.Forms.Button();
			this.buttonDelSybNodeDetail = new System.Windows.Forms.Button();
			this.listViewSubModel = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.listViewModel = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonSelectSubmodel = new System.Windows.Forms.Button();
			this.buttonNewSubModelSubNode = new System.Windows.Forms.Button();
			this.buttonDelSubNodeSubModel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.Location = new System.Drawing.Point(328, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(328, 160);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 90;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 200;
			// 
			// listViewSubNode
			// 
			this.listViewSubNode.HideSelection = false;
			this.listViewSubNode.LargeImageList = this.imageList1;
			this.listViewSubNode.Location = new System.Drawing.Point(0, 168);
			this.listViewSubNode.Name = "listViewSubNode";
			this.listViewSubNode.Size = new System.Drawing.Size(288, 200);
			this.listViewSubNode.TabIndex = 1;
			this.listViewSubNode.DoubleClick += new System.EventHandler(this.listViewSubNode_DoubleClick);
			this.listViewSubNode.SelectedIndexChanged += new System.EventHandler(this.listViewSubNode_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listViewSubNodeSubModel
			// 
			this.listViewSubNodeSubModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									  this.columnHeader13,
																									  this.columnHeader14,
																									  this.columnHeader15,
																									  this.columnHeader16,
																									  this.columnHeader17,
																									  this.columnHeader19,
																									  this.columnHeader20,
																									  this.columnHeader18});
			this.listViewSubNodeSubModel.FullRowSelect = true;
			this.listViewSubNodeSubModel.Location = new System.Drawing.Point(0, 384);
			this.listViewSubNodeSubModel.Name = "listViewSubNodeSubModel";
			this.listViewSubNodeSubModel.Size = new System.Drawing.Size(912, 112);
			this.listViewSubNodeSubModel.TabIndex = 2;
			this.listViewSubNodeSubModel.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Модель";
			this.columnHeader13.Width = 95;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Двигатель";
			this.columnHeader14.Width = 100;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Двигатель-описание";
			this.columnHeader15.Width = 172;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "КПП";
			this.columnHeader16.Width = 116;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "4х4";
			this.columnHeader17.Width = 32;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Кузов";
			this.columnHeader19.Width = 95;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Года выпуска";
			this.columnHeader20.Width = 92;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Марка";
			this.columnHeader18.Width = 120;
			// 
			// listViewNode
			// 
			this.listViewNode.HideSelection = false;
			this.listViewNode.LargeImageList = this.imageList2;
			this.listViewNode.Location = new System.Drawing.Point(0, 8);
			this.listViewNode.MultiSelect = false;
			this.listViewNode.Name = "listViewNode";
			this.listViewNode.Size = new System.Drawing.Size(280, 144);
			this.listViewNode.SmallImageList = this.imageList2;
			this.listViewNode.TabIndex = 3;
			this.listViewNode.SelectedIndexChanged += new System.EventHandler(this.listViewNode_SelectedIndexChanged);
			// 
			// imageList2
			// 
			this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList2.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// buttonNewNode
			// 
			this.buttonNewNode.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewNode.Image")));
			this.buttonNewNode.Location = new System.Drawing.Point(280, 48);
			this.buttonNewNode.Name = "buttonNewNode";
			this.buttonNewNode.Size = new System.Drawing.Size(24, 23);
			this.buttonNewNode.TabIndex = 4;
			this.buttonNewNode.Click += new System.EventHandler(this.buttonNewNode_Click);
			// 
			// buttonNewSubNode
			// 
			this.buttonNewSubNode.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewSubNode.Image")));
			this.buttonNewSubNode.Location = new System.Drawing.Point(288, 168);
			this.buttonNewSubNode.Name = "buttonNewSubNode";
			this.buttonNewSubNode.Size = new System.Drawing.Size(24, 23);
			this.buttonNewSubNode.TabIndex = 5;
			this.buttonNewSubNode.Click += new System.EventHandler(this.buttonNewSubNode_Click);
			// 
			// listViewSubNodeDetails
			// 
			this.listViewSubNodeDetails.AllowDrop = true;
			this.listViewSubNodeDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									 this.columnHeader3,
																									 this.columnHeader4});
			this.listViewSubNodeDetails.Location = new System.Drawing.Point(328, 200);
			this.listViewSubNodeDetails.Name = "listViewSubNodeDetails";
			this.listViewSubNodeDetails.Size = new System.Drawing.Size(328, 168);
			this.listViewSubNodeDetails.TabIndex = 6;
			this.listViewSubNodeDetails.View = System.Windows.Forms.View.Details;
			this.listViewSubNodeDetails.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewSubNodeDetails_DragDrop);
			this.listViewSubNodeDetails.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewSubNodeDetails_DragEnter);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Код";
			this.columnHeader3.Width = 128;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Наименование";
			this.columnHeader4.Width = 194;
			// 
			// buttonAddDetails
			// 
			this.buttonAddDetails.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAddDetails.Image")));
			this.buttonAddDetails.Location = new System.Drawing.Point(328, 168);
			this.buttonAddDetails.Name = "buttonAddDetails";
			this.buttonAddDetails.Size = new System.Drawing.Size(24, 23);
			this.buttonAddDetails.TabIndex = 7;
			this.buttonAddDetails.Click += new System.EventHandler(this.buttonAddDetails_Click);
			// 
			// buttonDelSybNodeDetail
			// 
			this.buttonDelSybNodeDetail.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelSybNodeDetail.Image")));
			this.buttonDelSybNodeDetail.Location = new System.Drawing.Point(632, 176);
			this.buttonDelSybNodeDetail.Name = "buttonDelSybNodeDetail";
			this.buttonDelSybNodeDetail.Size = new System.Drawing.Size(24, 23);
			this.buttonDelSybNodeDetail.TabIndex = 8;
			this.buttonDelSybNodeDetail.Click += new System.EventHandler(this.buttonDelSybNodeDetail_Click);
			// 
			// listViewSubModel
			// 
			this.listViewSubModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader6,
																							   this.columnHeader7,
																							   this.columnHeader8,
																							   this.columnHeader9,
																							   this.columnHeader10,
																							   this.columnHeader11,
																							   this.columnHeader12,
																							   this.columnHeader21});
			this.listViewSubModel.FullRowSelect = true;
			this.listViewSubModel.HideSelection = false;
			this.listViewSubModel.Location = new System.Drawing.Point(0, 504);
			this.listViewSubModel.Name = "listViewSubModel";
			this.listViewSubModel.Size = new System.Drawing.Size(912, 112);
			this.listViewSubModel.TabIndex = 9;
			this.listViewSubModel.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Модель";
			this.columnHeader6.Width = 95;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Двигатель";
			this.columnHeader7.Width = 96;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Двигатель-описание";
			this.columnHeader8.Width = 173;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "КПП";
			this.columnHeader9.Width = 124;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "4х4";
			this.columnHeader10.Width = 32;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Кузов";
			this.columnHeader11.Width = 89;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Года выпуска";
			this.columnHeader12.Width = 91;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Марка";
			this.columnHeader21.Width = 120;
			// 
			// listViewModel
			// 
			this.listViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader5});
			this.listViewModel.HideSelection = false;
			this.listViewModel.Location = new System.Drawing.Point(672, 8);
			this.listViewModel.MultiSelect = false;
			this.listViewModel.Name = "listViewModel";
			this.listViewModel.Size = new System.Drawing.Size(256, 360);
			this.listViewModel.TabIndex = 10;
			this.listViewModel.View = System.Windows.Forms.View.Details;
			this.listViewModel.SelectedIndexChanged += new System.EventHandler(this.listViewModel_SelectedIndexChanged);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Наименование";
			this.columnHeader5.Width = 245;
			// 
			// buttonSelectSubmodel
			// 
			this.buttonSelectSubmodel.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSelectSubmodel.Image")));
			this.buttonSelectSubmodel.Location = new System.Drawing.Point(304, 0);
			this.buttonSelectSubmodel.Name = "buttonSelectSubmodel";
			this.buttonSelectSubmodel.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectSubmodel.TabIndex = 11;
			this.buttonSelectSubmodel.Click += new System.EventHandler(this.buttonSelectSubmodel_Click);
			// 
			// buttonNewSubModelSubNode
			// 
			this.buttonNewSubModelSubNode.Location = new System.Drawing.Point(912, 504);
			this.buttonNewSubModelSubNode.Name = "buttonNewSubModelSubNode";
			this.buttonNewSubModelSubNode.Size = new System.Drawing.Size(24, 23);
			this.buttonNewSubModelSubNode.TabIndex = 12;
			this.buttonNewSubModelSubNode.Click += new System.EventHandler(this.buttonNewSubModelSubNode_Click);
			// 
			// buttonDelSubNodeSubModel
			// 
			this.buttonDelSubNodeSubModel.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelSubNodeSubModel.Image")));
			this.buttonDelSubNodeSubModel.Location = new System.Drawing.Point(912, 384);
			this.buttonDelSubNodeSubModel.Name = "buttonDelSubNodeSubModel";
			this.buttonDelSubNodeSubModel.Size = new System.Drawing.Size(24, 23);
			this.buttonDelSubNodeSubModel.TabIndex = 13;
			this.buttonDelSubNodeSubModel.Click += new System.EventHandler(this.buttonDelSubNodeSubModel_Click);
			// 
			// FormDetailManage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(944, 621);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDelSubNodeSubModel,
																		  this.buttonNewSubModelSubNode,
																		  this.buttonSelectSubmodel,
																		  this.listViewModel,
																		  this.listViewSubModel,
																		  this.buttonDelSybNodeDetail,
																		  this.buttonAddDetails,
																		  this.listViewSubNodeDetails,
																		  this.buttonNewSubNode,
																		  this.buttonNewNode,
																		  this.listViewNode,
																		  this.listViewSubNodeSubModel,
																		  this.listViewSubNode,
																		  this.listView1});
			this.Name = "FormDetailManage";
			this.Text = "Управление списком деталей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNewNode_Click(object sender, System.EventArgs e)
		{
			// Создание нового узла
			FormNode dialog = new FormNode(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			listViewNode.Items.Add(dialog.Node.LVItem);
		}

		private void buttonNewSubNode_Click(object sender, System.EventArgs e)
		{
			// Создание поднового узла
			// Проверяем активный подузел
			ListViewItem item;
			if(listViewNode.SelectedItems.Count > 0)
				item		= listViewNode.SelectedItems[0];
			else
				item		= null;
			if(item == null) return;
			DbNode node		= (DbNode)item.Tag;
			if(node == null) return;

			FormSubNode dialog = new FormSubNode(node, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			listViewSubNode.Items.Add(dialog.SubNode.LVItem);
		}

		protected void listViewNode_SelectedIndexChanged(object sender, EventArgs e)
		{
			listViewSubNode.Items.Clear();
			listViewSubNodeDetails.Items.Clear();
			if(listViewNode.SelectedItems.Count == 0) return;
			ListViewItem item = listViewNode.SelectedItems[0];
			if(item == null) return;
			DbNode	node	= (DbNode)item.Tag;
			if(node	== null) return;

			Bitmap image;
			image = new Bitmap(".\\SubNodes\\Неизвестно.bmp");
			imageList1.Images.Clear();
			imageList1.Images.Add(image);
			//DbSubNode.FillList(listViewSubNode, node);
			DbSubNode.FillList(listViewSubNode, node, selectedAutoSubModel);
		}

		protected void listViewSubNode_SelectedIndexChanged(object sender, EventArgs e)
		{
			listViewSubNodeDetails.Items.Clear();
			listViewSubNodeSubModel.Items.Clear();

			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem item = listViewSubNode.SelectedItems[0];
			if(item == null) return;
			DbSubNode	subNode	= (DbSubNode)item.Tag;
			if(subNode	== null) return;
			
			// Заполняем лист деталями записанными на подузел
			DbDetail.FillList(listViewSubNodeDetails, subNode);
			// Заполняем список подмоделей записанных на подузел
			DbAutoSubmodel.FillList(listViewSubNodeSubModel, subNode, 0);
		}

		protected void listViewSubNode_DoubleClick(object sender, EventArgs e)
		{
			// Определяем кликнутый элеиент
			ListViewItem item = Db.GetItemSelected(listViewSubNode);
			if(item == null) return;
			DbSubNode subNode = (DbSubNode)item.Tag;
			if(subNode == null) return;
			string text = ".\\SubNodePics\\" + subNode.Name + "_" + subNode.Code.ToString() + ".bmp";
			FormImage dialog = new FormImage(text, subNode.Name, subNode.Code);
			dialog.Show();
		}

		private void buttonAddDetails_Click(object sender, System.EventArgs e)
		{
			// Добавляем детали в список деталей для подузла
			// Выбор подузла куда добавляем
			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem itemSubNode	= listViewSubNode.SelectedItems[0];
			if(itemSubNode == null) return;
			DbSubNode subNode	= (DbSubNode)itemSubNode.Tag;
			if(subNode == null) return;

			// Выбор добавляемой детали
			foreach(ListViewItem itemDetail in listView1.SelectedItems)
			{
				DbDetail detail	= (DbDetail)itemDetail.Tag;
				if(detail != null)
				{
					if(subNode.WriteDetail(detail, true) == true)
					{
						listViewSubNodeDetails.Items.Add(detail.LVItem);
					}
				}
			}
			Db.ShowFaults();
		}

		protected void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			string mask = "";
			FormSelectString dialog = null;
			switch(e.Column)
			{
				case 0:
					// Щелчек на колонке с кодом детали
					dialog = new FormSelectString("Код детали", "Код детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					DbDetail.FillList(listView1, mask, DbDetail.SelectType.ByCode);
					return;
				case 1:
					dialog = new FormSelectString("Наименование детали", "Наименование детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					DbDetail.FillList(listView1, mask, DbDetail.SelectType.ByName);
					return;
			}
		}
		protected void listView1_MouseMove(object sender, MouseEventArgs e)
		{
			// Проверяем на наличие доплнительных нажатых клавиш
			if(e.Button != MouseButtons.Left) return;
			if(listView1.SelectedItems.Count == 0) return;
			// Начинаем операцию по перетаскиванию
			this.DoDragDrop("Из списка деталей", DragDropEffects.Move);
		}
		protected void listViewSubNodeDetails_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		protected void listViewSubNodeDetails_DragDrop(object sender, DragEventArgs e)
		{
			// Проверяем на правильность перетаскиваемого объекта
			if(e.Data.GetDataPresent(DataFormats.Text) != true) return;
			if((string)e.Data.GetData(DataFormats.Text) != "Из списка деталей") return;

			// Выбор подузла куда добавляем
			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem itemSubNode	= listViewSubNode.SelectedItems[0];
			if(itemSubNode == null) return;
			DbSubNode subNode	= (DbSubNode)itemSubNode.Tag;
			if(subNode == null) return;

			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetail detail = (DbDetail)item.Tag;
				if(detail != null)
				{
					if(subNode.WriteDetail(detail, true) == true)
					{
						listViewSubNodeDetails.Items.Add(detail.LVItem);
					}
				}
			}
		}

		private void buttonDelSybNodeDetail_Click(object sender, System.EventArgs e)
		{
			// Удаляем детали из списока деталей для подузла
			// Выбор подузла откуда удаляем
			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem itemSubNode	= listViewSubNode.SelectedItems[0];
			if(itemSubNode == null) return;
			DbSubNode subNode	= (DbSubNode)itemSubNode.Tag;
			if(subNode == null) return;

			// Выбор удяляемых деталей
			foreach(ListViewItem itemDetail in listViewSubNodeDetails.SelectedItems)
			{
				DbDetail detail	= (DbDetail)itemDetail.Tag;
				if(detail != null)
				{
					if(subNode.WriteDetail(detail, false) == true)
					{
						listViewSubNodeDetails.Items.Remove(itemDetail);
					}
				}
			}
			Db.ShowFaults();
		}

		protected void listViewModel_SelectedIndexChanged(object sender, EventArgs e)
		{
			listViewSubModel.Items.Clear();			// Очистка списка подмоделей

			if(listViewModel.SelectedItems.Count == 0) return;
			ListViewItem item = listViewModel.SelectedItems[0];
			if(item == null) return;
			DbAutoModel	model	= (DbAutoModel)item.Tag;
			if(model	== null) return;
			
			// Заполняем лист деталями записанными на подузел
			DbAutoSubmodel.FillList(listViewSubModel, model, 0);
		}

		private void buttonSelectSubmodel_Click(object sender, System.EventArgs e)
		{
			// Выбор активной подмодели, для которой все настраиваем
			long codeAutoSubModelOld = 0;
			if(selectedAutoSubModel != null)
			{
				codeAutoSubModelOld = selectedAutoSubModel.Code;
			}

			// Запуск выбора новой подмодели
			// Для начала выбираем модель для которой управлялем списком подмоделей
			FormModelList dialog = new FormModelList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK)
			{
				MakeAutoSubmodelSelection(codeAutoSubModelOld, null);
				selectedAutoSubModel	= null;
				return;
			}
			if(dialog.AutoModel == null)
			{
				MakeAutoSubmodelSelection(codeAutoSubModelOld, null);
				selectedAutoSubModel	= null;
				return;
			}
			FormSubmodelList dialog1 = new FormSubmodelList(Db.ClickType.Select, dialog.AutoModel);
			dialog1.ShowDialog();
			if(dialog1.DialogResult != DialogResult.OK)
			{
				MakeAutoSubmodelSelection(codeAutoSubModelOld, null);
				selectedAutoSubModel	= null;
				return;
			}
			selectedAutoSubModel = dialog1.AutoSubModel;
			MakeAutoSubmodelSelection(codeAutoSubModelOld, selectedAutoSubModel);
		}

		protected void MakeAutoSubmodelSelection(long oldCode, DbAutoSubmodel selection)
		{
			if(selection == null)
			{
				if(oldCode == 0) return;
				// Производим очистку всех выборов
				listViewSubNodeDetails.Items.Clear();
				listViewSubNode.Items.Clear();
				listViewNode.SelectedItems.Clear();
				// Установка нового наименования
				this.Text = "Управление деталями / " + "Подмодель не выбрана";
				return;
			}
			if(oldCode == selection.Code) return;
			// Производим очистку всех выборов
			listViewSubNodeDetails.Items.Clear();
			listViewSubNode.Items.Clear();
			listViewNode.SelectedItems.Clear();
			listViewSubNodeSubModel.Items.Clear();
			this.Text = "Управление деталями / " + selection.LongTitleTxt;
		}

		private void buttonNewSubModelSubNode_Click(object sender, System.EventArgs e)
		{
			// Добавление списка подмоделей в список подузлов
			// Выбор подузла куда добавляем
			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem itemSubNode	= listViewSubNode.SelectedItems[0];
			if(itemSubNode == null) return;
			DbSubNode subNode	= (DbSubNode)itemSubNode.Tag;
			if(subNode == null) return;

			// Выбор добавляемых подмоделей
			foreach(ListViewItem itemSubModel in listViewSubModel.SelectedItems)
			{
				DbAutoSubmodel subModel	= (DbAutoSubmodel)itemSubModel.Tag;
				if(subModel != null)
				{
					if(subNode.WriteSubModel(subModel, true) == true)
					{
						listViewSubNodeSubModel.Items.Add(subModel.LVItem);
					}
				}
			}
			Db.ShowFaults();
		}

		private void buttonDelSubNodeSubModel_Click(object sender, System.EventArgs e)
		{
			// Убираем выбранные элементы из списка подмоделей для подузла
			// Добавление списка подмоделей в список подузлов
			// Выбор подузла куда добавляем
			if(listViewSubNode.SelectedItems.Count == 0) return;
			ListViewItem itemSubNode	= listViewSubNode.SelectedItems[0];
			if(itemSubNode == null) return;
			DbSubNode subNode	= (DbSubNode)itemSubNode.Tag;
			if(subNode == null) return;

			// Выбор добавляемых подмоделей
			foreach(ListViewItem itemSubModel in listViewSubNodeSubModel.SelectedItems)
			{
				DbAutoSubmodel subModel	= (DbAutoSubmodel)itemSubModel.Tag;
				if(subModel != null)
				{
					if(subNode.WriteSubModel(subModel, false) == true)
					{
						listViewSubNodeSubModel.Items.Remove(itemSubModel);
					}
				}
			}
			Db.ShowFaults();
		}
	}
}
