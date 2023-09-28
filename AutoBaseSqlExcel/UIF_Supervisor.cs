using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Supervisor.
	/// </summary>
	public class UIF_Supervisor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Button button_select;
		private System.Windows.Forms.TreeView treeView_cards;
		private System.Windows.Forms.ImageList imageList_status;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Button button_show_payments;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.ComponentModel.IContainer components;

		public UIF_Supervisor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_Supervisor));
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.button_select = new System.Windows.Forms.Button();
			this.treeView_cards = new System.Windows.Forms.TreeView();
			this.imageList_status = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.button_show_payments = new System.Windows.Forms.Button();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(8, 16);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.Size = new System.Drawing.Size(128, 20);
			this.dateTimePicker_date.TabIndex = 0;
			// 
			// button_select
			// 
			this.button_select.Location = new System.Drawing.Point(144, 16);
			this.button_select.Name = "button_select";
			this.button_select.TabIndex = 1;
			this.button_select.Text = "Выбрать";
			this.button_select.Click += new System.EventHandler(this.button_select_Click);
			// 
			// treeView_cards
			// 
			this.treeView_cards.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.treeView_cards.ImageList = this.imageList_status;
			this.treeView_cards.Location = new System.Drawing.Point(16, 64);
			this.treeView_cards.Name = "treeView_cards";
			this.treeView_cards.Size = new System.Drawing.Size(496, 240);
			this.treeView_cards.TabIndex = 2;
			this.treeView_cards.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_cards_MouseUp);
			// 
			// imageList_status
			// 
			this.imageList_status.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList_status.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList_status.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_status.ImageStream")));
			this.imageList_status.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem5,
																					  this.menuItem6});
			this.menuItem1.Text = "Одобрение";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Гарантия";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "Оплата";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem4});
			this.menuItem3.Text = "Гарантия";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Установить вид";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// button_show_payments
			// 
			this.button_show_payments.Location = new System.Drawing.Point(520, 16);
			this.button_show_payments.Name = "button_show_payments";
			this.button_show_payments.Size = new System.Drawing.Size(88, 23);
			this.button_show_payments.TabIndex = 3;
			this.button_show_payments.Text = "Платежи";
			this.button_show_payments.Click += new System.EventHandler(this.button_show_payments_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "Полное";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// UIF_Supervisor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_show_payments,
																		  this.treeView_cards,
																		  this.button_select,
																		  this.dateTimePicker_date});
			this.Name = "UIF_Supervisor";
			this.Text = "Контролер";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_select_Click(object sender, System.EventArgs e)
		{
			// Выбираем заказ-наряды закрытые на дату
			treeView_cards.Nodes.Clear();
			DateTime date = dateTimePicker_date.Value;
			ArrayList cards = new ArrayList();
			DbSqlCard.SelectCardClosedNumber(cards, date, date);

			// Основные ветки
			TreeNode node_nal		= new TreeNode("НАЛИЧНЫЙ");
			TreeNode node_cashless	= new TreeNode("БЕЗНАЛИЧНЫЙ");
			TreeNode node_inner		= new TreeNode("ВНУТРЕННИЙ");
			treeView_cards.Nodes.Add(node_nal);
			treeView_cards.Nodes.Add(node_cashless);
			treeView_cards.Nodes.Add(node_inner);

			// Составляем список
			foreach(object o in cards)
			{
				DtCard card = (DtCard)o;
				TreeNode node = new TreeNode("ОШИБКА");
				card = DbSqlCard.Find(card.Number, card.Year);
				if(card != null)
				{
					bool cashless = card.Cashless;
					bool inner = card.Inner;
					card.SetTNode_Supervisor(node);
					if(cashless == false & inner == false)
						node_nal.Nodes.Add(node);
					if(cashless == true)
						node_cashless.Nodes.Add(node);
					if(inner == true)
						node_inner.Nodes.Add(node);
					//treeView_cards.Nodes.Add(node);
				}
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Одобрить гарантию по выбранному элементу
			TreeNode node = Db.GetItemSelected(treeView_cards);
			if(node == null) return;
			if(node.Tag == null) return;
			DtCard.Data	data	= (DtCard.Data)node.Tag;
			DtCard.Pair pair	= (DtCard.Pair)data.data;
			long number			= pair.number;
			int year			= pair.year;
			if(number == 0 || year == 0) return;
			// Запрашиваем электронную подпись одобряющего
			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего заказ-наряд", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(staff.SelectedLong == 0) return;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return;
			long code = (long)master.GetData("КОД_ПЕРСОНАЛ");
			if(code == 0) return;
			if(DbSqlCard.SetSupervisorGuaranty(number, year, code) != true) return;
			// Ищем подузел гарантии
			TreeNode mach = null;
			foreach(TreeNode nd in node.Nodes)
			{
				if(nd.Text == "ГАРАНТИЯ") mach = nd;
			}
			mach.Text					= "ГАРАНТИЯ (" + master.Title + ")";
			mach.ImageIndex				= 2;
			mach.SelectedImageIndex		= 2;
		}

		private void treeView_cards_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// Находим элемент на котором отпустили
				TreeNode node = Db.GetItemPosition(treeView_cards);
				treeView_cards.SelectedNode = node;

				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//menuItem6.Enabled = false;
				
				// Включаем по разрешению
				//string login = Form1.currentLogin.ToLower();
				//if (login == "админ")
				//{
				//	menuItem6.Enabled = true;
				//}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(treeView_cards, new Point(e.X, e.Y));
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем вид гарантии для выбранного элемента
			// Ищем выбранный элемент
			TreeNode node = Db.GetItemSelected(treeView_cards);
			if(node == null) return;
			if(node.Tag == null) return;
			// Ищем корневой родительский элемент
			TreeNode pnode	= node.Parent;
			TreeNode parent = node;
			while(pnode != null)
			{
				parent		= pnode;
				pnode		= pnode.Parent;
			}
			if(parent == null || parent == node) return;
			if(parent.Tag	== null) return;
			DtCard.Data	pdata	= (DtCard.Data)parent.Tag;
			if(pdata.group	!= DtCard.NodeGroup.ROOT) return;
			DtCard.Pair pair	= (DtCard.Pair)pdata.data;
			// Проверяем, относится ли элемент к группе ГАРАНТИЯ
			DtCard.Data	data	= (DtCard.Data)node.Tag;
			if(data.group != DtCard.NodeGroup.GUARANTY) return;	
			long number			= pair.number;
			int year			= pair.year;
			if(number == 0 || year == 0) return;
			if(data.type == DtCard.NodeType.DETAIL)
			{
				long pos = (long)data.data;
				// Ищем соответствующую деталь
				DtCardDetail detail = DbSqlCardDetail.Find(number, year, pos);
				if(detail == null) return;
				long gt_code		= (long)detail.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_ДЕТАЛЬ");
				ArrayList gt_array	= new ArrayList();
				DbSqlGuarantyType.SelectInArrayChild(gt_array, gt_code);
				if(gt_array.Count == 0)
				{
					DbSqlGuarantyType.SelectInArrayRoot(gt_array);
				}
				if(gt_array.Count == 0) return;
				UIF_Selector_Array form = new UIF_Selector_Array(gt_array);
				if(form.ShowDialog() != DialogResult.OK) return;
				gt_code = form.SelectedCode;
				DtGuarantyType	gt	= DbSqlGuarantyType.Find(gt_code);
				if(gt == null) return;
				// Проверяем, требуется ли виновник
				long code					= (long)gt.GetData("КОД_ГАРАНТИЯ");
				long code_responsible		= 0;
				string reason_responsible	= "";
				if((bool)gt.GetData("ОТВЕТСТВЕННЫЙ") == true)
				{
					// Запрашиваем виновника
					FormStaffList form1 = new FormStaffList();
					if(form1.ShowDialog() != DialogResult.OK) return;
					DbStaff staff = form1.SelectedStaff;
					if(staff == null) return;
					code_responsible	= staff.Code;
					FormSelectString form2 = new FormSelectString("Причина выбора ответсвенного", "");
					if(form2.ShowDialog() != DialogResult.OK) return;
					reason_responsible = form2.SelectedText;
				}
				// Записываем результат в базу данных
				if(DbSqlCardDetail.UpdateGuaranty(number, year, pos, code, code_responsible, reason_responsible) != true) return;
				// Отображаем на экране
				detail = DbSqlCardDetail.Find(number, year, pos);
				detail.SetTNode_Supervisor(node);
			}
			if(data.type == DtCard.NodeType.WORK)
			{
				int pos = (int)data.data;
				// Ищем соответствующую работу
				DtCardWork work = DbSqlCardWork.Find(number, year, pos);
				if(work == null) return;
				long gt_code		= (long)work.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА");
				ArrayList gt_array	= new ArrayList();
				DbSqlGuarantyType.SelectInArrayChild(gt_array, gt_code);
				if(gt_array.Count == 0)
				{
					DbSqlGuarantyType.SelectInArrayRoot(gt_array);
				}
				if(gt_array.Count == 0) return;
				UIF_Selector_Array form = new UIF_Selector_Array(gt_array);
				if(form.ShowDialog() != DialogResult.OK) return;
				gt_code = form.SelectedCode;
				DtGuarantyType	gt	= DbSqlGuarantyType.Find(gt_code);
				if(gt == null) return;
				// Проверяем, требуется ли виновник
				long code					= (long)gt.GetData("КОД_ГАРАНТИЯ");
				long code_responsible		= 0;
				string reason_responsible	= "";
				if((bool)gt.GetData("ОТВЕТСТВЕННЫЙ") == true)
				{
					// Запрашиваем виновника
					FormStaffList form1 = new FormStaffList();
					if(form1.ShowDialog() != DialogResult.OK) return;
					DbStaff staff = form1.SelectedStaff;
					if(staff == null) return;
					code_responsible	= staff.Code;
					FormSelectString form2 = new FormSelectString("Причина выбора ответсвенного", "");
					if(form2.ShowDialog() != DialogResult.OK) return;
					reason_responsible = form2.SelectedText;
				}
				// Записываем результат в базу данных
				if(DbSqlCardWork.UpdateGuaranty(number, year, pos, code, code_responsible, reason_responsible) != true) return;
				// Отображаем на экране
				work = DbSqlCardWork.Find(number, year, pos);
				work.SetTNode_Supervisor(node);
			}
			MessageBox.Show("OK");
		}

		private void button_show_payments_Click(object sender, System.EventArgs e)
		{
			// Показ списка платежей
			UIF_Payment_List form = new UIF_Payment_List();
			form.Show();												  
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Одобрить оплату карточки
			TreeNode node = Db.GetItemSelected(treeView_cards);
			if(node == null) return;
			if(node.Tag == null) return;
			DtCard.Data	data	= (DtCard.Data)node.Tag;
			DtCard.Pair pair	= (DtCard.Pair)data.data;
			long number			= pair.number;
			int year			= pair.year;
			if(number == 0 || year == 0) return;
			// Запрашиваем электронную подпись одобряющего
			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего заказ-наряд", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(staff.SelectedLong == 0) return;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return;
			long code = (long)master.GetData("КОД_ПЕРСОНАЛ");
			if(code == 0) return;
			if(DbSqlCard.SetSupervisorPayment(number, year, code) != true) return;
			// Ищем подузел оплаты
			TreeNode mach = null;
			foreach(TreeNode nd in node.Nodes)
			{
				DtCard.Data dt;
				if(nd.Tag != null)
				{
					dt= (DtCard.Data)nd.Tag;
					if(dt.group == DtCard.NodeGroup.PAY_ROOT) mach = nd;
				}
			}
			if(mach != null)
			{
				mach.Text					= "ОПЛАТА (" + master.Title + ")";
				mach.ImageIndex				= 2;
				mach.SelectedImageIndex		= 2;
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Полное одобрение карточки
			// Одобрить оплату карточки
			TreeNode node = Db.GetItemSelected(treeView_cards);
			if(node == null) return;
			if(node.Tag == null) return;
			DtCard.Data	data	= (DtCard.Data)node.Tag;
			DtCard.Pair pair	= (DtCard.Pair)data.data;
			long number			= pair.number;
			int year			= pair.year;
			if(number == 0 || year == 0) return;
			// Запрашиваем электронную подпись одобряющего
			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего заказ-наряд", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(staff.SelectedLong == 0) return;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return;
			long code = (long)master.GetData("КОД_ПЕРСОНАЛ");
			if(code == 0) return;
			if(DbSqlCard.SetSupervisorWhole(number, year, code) != true) return;

			// Перезачитываем карточку
			node.ImageIndex			= 2;
			node.SelectedImageIndex	= 2;
			string staff_txt			= "ОШИБКА";
			if(master != null) staff_txt = master.Title;
			node.Text	= node.Text + " " + staff_txt;
		}
	}
}
