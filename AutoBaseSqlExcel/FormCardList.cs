using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardList.
	/// </summary>
	public class FormCardList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonWarrantOpen;
		private System.Windows.Forms.Button buttonWarrantClose;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ToolTip toolTip1;

		private System.ComponentModel.IContainer components;

		private Db.ClickType clickType;
		private System.Windows.Forms.Button buttonSearchInterval;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.CheckBox checkBoxSetSelectPartner;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button buttonPrintBuhg;
		private System.Windows.Forms.CheckBox checkBoxSearchAuto;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button buttonPrintDocument;
		private System.Windows.Forms.Button buttonCancelClose;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonToExcel;
		private System.Windows.Forms.Button buttonPrintRequest;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button_check;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private DbCard selectedCard;

		public FormCardList(Db.ClickType type, int typeSearch, DbAuto auto)
		{
			InitializeComponent();

			clickType = type;
			selectedCard = null;

			this.Text = "Карточки заказов " + DbCard.searchCondition.DateIntervalTxt;

			if(typeSearch == 1 && auto != null)
			{
				// Поиск предпрадажной подготовки
				this.Text = "Карточки заказов предпрадажной подготовки автомобиля " + auto.ModelTxt + " VIN " + auto.Vin;
				DbCard.FillListPreparation(listView1, auto);
			}
			if(typeSearch == 2 && auto != null)
			{
				// Все закрытые карточки по автомобилю
				this.Text = "Закрытые карточки заказов автомобиля " + auto.ModelTxt + " VIN " + auto.Vin;
				DbCard.FillListAutoClosed(listView1, auto);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCardList));
            this.buttonNew = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonWarrantOpen = new System.Windows.Forms.Button();
            this.buttonWarrantClose = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSearchInterval = new System.Windows.Forms.Button();
            this.checkBoxSetSelectPartner = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonPrintBuhg = new System.Windows.Forms.Button();
            this.checkBoxSearchAuto = new System.Windows.Forms.CheckBox();
            this.buttonPrintDocument = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonPrintRequest = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonCancelClose = new System.Windows.Forms.Button();
            this.buttonToExcel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_check = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNew
            // 
            this.buttonNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonNew.Image")));
            this.buttonNew.Location = new System.Drawing.Point(0, 0);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(29, 27);
            this.buttonNew.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonNew, "Новая карточка");
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1313, 468);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Дата";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Заказ-наряд";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Клиент";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Модель/Кузов/Рег.Знак";
            this.columnHeader5.Width = 180;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Примечание";
            this.columnHeader6.Width = 140;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpdate.Image")));
            this.buttonUpdate.Location = new System.Drawing.Point(29, 0);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(29, 27);
            this.buttonUpdate.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonUpdate, "Обновить список");
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Image = ((System.Drawing.Image)(resources.GetObject("buttonChange.Image")));
            this.buttonChange.Location = new System.Drawing.Point(58, 0);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(28, 27);
            this.buttonChange.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonChange, "Свойтсва карточки");
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
            this.buttonPrint.Location = new System.Drawing.Point(86, 0);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(29, 27);
            this.buttonPrint.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonPrint, "Печать карточки");
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // buttonWarrantOpen
            // 
            this.buttonWarrantOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonWarrantOpen.Image")));
            this.buttonWarrantOpen.Location = new System.Drawing.Point(221, 0);
            this.buttonWarrantOpen.Name = "buttonWarrantOpen";
            this.buttonWarrantOpen.Size = new System.Drawing.Size(29, 27);
            this.buttonWarrantOpen.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonWarrantOpen, "Открыть заказ-наряд");
            this.buttonWarrantOpen.Click += new System.EventHandler(this.buttonWarrantOpen_Click);
            // 
            // buttonWarrantClose
            // 
            this.buttonWarrantClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonWarrantClose.Image")));
            this.buttonWarrantClose.Location = new System.Drawing.Point(250, 0);
            this.buttonWarrantClose.Name = "buttonWarrantClose";
            this.buttonWarrantClose.Size = new System.Drawing.Size(28, 27);
            this.buttonWarrantClose.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonWarrantClose, "Закрыть заказ-наряд");
            this.buttonWarrantClose.Click += new System.EventHandler(this.buttonWarrantClose_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            this.buttonStop.Location = new System.Drawing.Point(278, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(29, 27);
            this.buttonStop.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonStop, "Приостановить заказ-наряд");
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
            this.buttonStart.Location = new System.Drawing.Point(307, 0);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(29, 27);
            this.buttonStart.TabIndex = 8;
            this.toolTip1.SetToolTip(this.buttonStart, "Возобновить заказ-наряд");
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonSearchInterval
            // 
            this.buttonSearchInterval.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearchInterval.Image")));
            this.buttonSearchInterval.Location = new System.Drawing.Point(394, 0);
            this.buttonSearchInterval.Name = "buttonSearchInterval";
            this.buttonSearchInterval.Size = new System.Drawing.Size(28, 28);
            this.buttonSearchInterval.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonSearchInterval, "Интервал дат поиска");
            this.buttonSearchInterval.Click += new System.EventHandler(this.buttonSearchInterval_Click);
            // 
            // checkBoxSetSelectPartner
            // 
            this.checkBoxSetSelectPartner.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxSetSelectPartner.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxSetSelectPartner.Image")));
            this.checkBoxSetSelectPartner.Location = new System.Drawing.Point(422, 0);
            this.checkBoxSetSelectPartner.Name = "checkBoxSetSelectPartner";
            this.checkBoxSetSelectPartner.Size = new System.Drawing.Size(29, 28);
            this.checkBoxSetSelectPartner.TabIndex = 12;
            this.toolTip1.SetToolTip(this.checkBoxSetSelectPartner, "Фильтр по клиенту");
            this.checkBoxSetSelectPartner.CheckedChanged += new System.EventHandler(this.checkBoxSetSelectPartner_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(336, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(29, 27);
            this.buttonCancel.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonCancel, "Анулировать карточку");
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(499, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 27);
            this.button1.TabIndex = 13;
            this.toolTip1.SetToolTip(this.button1, "Запись выбранной карточки на время");
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(528, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 27);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.toolTip1.SetToolTip(this.button2, "Расписание");
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonPrintBuhg
            // 
            this.buttonPrintBuhg.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrintBuhg.Image")));
            this.buttonPrintBuhg.Location = new System.Drawing.Point(115, 0);
            this.buttonPrintBuhg.Name = "buttonPrintBuhg";
            this.buttonPrintBuhg.Size = new System.Drawing.Size(29, 27);
            this.buttonPrintBuhg.TabIndex = 15;
            this.toolTip1.SetToolTip(this.buttonPrintBuhg, "Бухгалтерская печать");
            this.buttonPrintBuhg.Click += new System.EventHandler(this.buttonPrintBuhg_Click);
            // 
            // checkBoxSearchAuto
            // 
            this.checkBoxSearchAuto.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxSearchAuto.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxSearchAuto.Image")));
            this.checkBoxSearchAuto.Location = new System.Drawing.Point(451, 0);
            this.checkBoxSearchAuto.Name = "checkBoxSearchAuto";
            this.checkBoxSearchAuto.Size = new System.Drawing.Size(29, 28);
            this.checkBoxSearchAuto.TabIndex = 16;
            this.toolTip1.SetToolTip(this.checkBoxSearchAuto, "Фильтр по автомобилю");
            this.checkBoxSearchAuto.CheckedChanged += new System.EventHandler(this.checkBoxSearchAuto_CheckedChanged);
            // 
            // buttonPrintDocument
            // 
            this.buttonPrintDocument.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrintDocument.Image")));
            this.buttonPrintDocument.Location = new System.Drawing.Point(144, 0);
            this.buttonPrintDocument.Name = "buttonPrintDocument";
            this.buttonPrintDocument.Size = new System.Drawing.Size(29, 27);
            this.buttonPrintDocument.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonPrintDocument, "Печать документа на въезд");
            this.buttonPrintDocument.Click += new System.EventHandler(this.buttonPrintDocument_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Image = ((System.Drawing.Image)(resources.GetObject("buttonReplace.Image")));
            this.buttonReplace.Location = new System.Drawing.Point(816, 0);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(29, 27);
            this.buttonReplace.TabIndex = 19;
            this.toolTip1.SetToolTip(this.buttonReplace, "Смена контрагента карточки");
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonPrintRequest
            // 
            this.buttonPrintRequest.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrintRequest.Image")));
            this.buttonPrintRequest.Location = new System.Drawing.Point(173, 0);
            this.buttonPrintRequest.Name = "buttonPrintRequest";
            this.buttonPrintRequest.Size = new System.Drawing.Size(29, 27);
            this.buttonPrintRequest.TabIndex = 21;
            this.toolTip1.SetToolTip(this.buttonPrintRequest, "Печать заголовка");
            this.buttonPrintRequest.Click += new System.EventHandler(this.buttonPrintRequest_Click);
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(672, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(29, 27);
            this.button4.TabIndex = 23;
            this.toolTip1.SetToolTip(this.button4, "Получение деталей по карточке");
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(701, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(29, 27);
            this.button5.TabIndex = 24;
            this.toolTip1.SetToolTip(this.button5, "Возврат деталей по картчке");
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonCancelClose
            // 
            this.buttonCancelClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancelClose.Image")));
            this.buttonCancelClose.Location = new System.Drawing.Point(787, 0);
            this.buttonCancelClose.Name = "buttonCancelClose";
            this.buttonCancelClose.Size = new System.Drawing.Size(29, 27);
            this.buttonCancelClose.TabIndex = 18;
            this.buttonCancelClose.Click += new System.EventHandler(this.buttonCancelClose_Click);
            // 
            // buttonToExcel
            // 
            this.buttonToExcel.Location = new System.Drawing.Point(576, 0);
            this.buttonToExcel.Name = "buttonToExcel";
            this.buttonToExcel.Size = new System.Drawing.Size(29, 27);
            this.buttonToExcel.TabIndex = 20;
            this.buttonToExcel.Text = "E";
            this.buttonToExcel.Click += new System.EventHandler(this.buttonToExcel_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(605, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 27);
            this.button3.TabIndex = 22;
            this.button3.Text = "ETO";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_check
            // 
            this.button_check.Location = new System.Drawing.Point(883, 0);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(48, 27);
            this.button_check.TabIndex = 25;
            this.button_check.Text = "CHK";
            this.button_check.Click += new System.EventHandler(this.button_check_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(931, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(48, 27);
            this.button6.TabIndex = 26;
            this.button6.Text = "ВАЗ";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(979, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(29, 27);
            this.button7.TabIndex = 27;
            this.button7.Text = "p";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // FormCardList
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(1333, 502);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button_check);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonPrintRequest);
            this.Controls.Add(this.buttonToExcel);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonCancelClose);
            this.Controls.Add(this.buttonPrintDocument);
            this.Controls.Add(this.checkBoxSearchAuto);
            this.Controls.Add(this.buttonPrintBuhg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxSetSelectPartner);
            this.Controls.Add(this.buttonSearchInterval);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonWarrantClose);
            this.Controls.Add(this.buttonWarrantOpen);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonNew);
            this.Name = "FormCardList";
            this.Text = "Карточки заказов";
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога заполнения и добавления нового элемента
			FormCard dialog = new FormCard(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Card.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем лист
			listView1.Items.Clear();
			DbCard.FillList(listView1);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Попытка исправить выбранный документ
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;
			FormCard dialog = new FormCard(card);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Card.SetLVItem(item);
		}

		protected void ListView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = null;
			DbCard card = null;
			switch(clickType)
			{
				case Db.ClickType.Properties:
					item = Db.GetItemPosition(listView1);
					if(item == null) return;
					card = (DbCard)item.Tag;
					if(card == null) return;
					FormCard dialog = new FormCard(card);
					dialog.ShowDialog();
					if(dialog.DialogResult != DialogResult.OK) return;
					dialog.Card.SetLVItem(item);
					break;
				case Db.ClickType.Select:
					item = Db.GetItemPosition(listView1);
					if(item == null) return;
					card = (DbCard)item.Tag;
					if(card == null) return;
					selectedCard = card;
					this.DialogResult = DialogResult.OK;
					this.Close();
					break;
			}
		}

		private void buttonPrint_Click(object sender, System.EventArgs e)
		{
			// Печать выбранной карточки заказа
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;
			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.Print();
		}

		private void buttonWarrantOpen_Click(object sender, System.EventArgs e)
		{
			// Пытаемся открыть выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			card.Action(DbCardAction.ActionCodes.Open);
			card.SetLVItem(item);
		}

		private void buttonWarrantClose_Click(object sender, System.EventArgs e)
		{
			// Пытаемся закрыть выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			// Запрос об установке скидки
			if(card.CodeWorkshop == 1)
			{
				// Скидка только для сервиса
				if(card.Partner.Juridical == true)
				{
					// Для юридических лиц скидка по списку
					// Загружаем свойтсва контрагента
					DtPartnerProperty property = DbSqlPartnerProperty.Find(card.Partner.Code);
					if(property != null)
					{
						if(property.Discount > 0)
						{
							// Запрос на предоставление скидки
							if(MessageBox.Show("Предоставить корпоративную скидку " + property.Discount.ToString() + "%", "Запрос",  MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								// Предоставляем скидку корпоративную
								if(DbSqlCard.SetDiscount(card, property.Discount, 0) == true)
								{
									card.DiscountWork = (float)property.Discount;
								}
							}
						}
					}
				}
				else
				{
					// Запрос кода карточки
					if(MessageBox.Show("Есть ли дисконтная карта?", "Запрос",  MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						FormSelectString dialog = new FormSelectString("Введите код карточки", "");
						if(dialog.ShowDialog() == DialogResult.OK)
						{
							// Поиск дисконтной карточки
							DtDiscount discount = DbSqlDiscount.Find(dialog.SelectedLong);
							if(discount == null) return;
							// Предоставляем скидку дисконтную
							if(DbSqlCard.SetDiscount(card, (float)discount.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ"), (long)discount.GetData("КОД_ДИСКОНТ"))== true)
							{
								card.DiscountWork = (float)discount.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ");
							}
						}
					}
				}
			}

			//Запрашиваем мастера-контролера подписавшего закрытие заказ-наряда
			MessageBox.Show("Выберете мастера-контролера подписавшего закрытие заказ-наряда");
			FormStaffList staff = new FormStaffList();
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(DbSqlCard.SetMaster(card, staff.SelectedStaff) != true) return;
			
			card.Action(DbCardAction.ActionCodes.Close);
			card.SetLVItem(item);
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			// Пытаемся приостановить выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			card.Action(DbCardAction.ActionCodes.Stop);
			card.SetLVItem(item);
		}

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			// Пытаемся возобновить выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			card.Action(DbCardAction.ActionCodes.Start);
			card.SetLVItem(item);
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			// Пытаемся анулировать выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			card.Action(DbCardAction.ActionCodes.Cancel);
			card.SetLVItem(item);
		}

		private void buttonSearchInterval_Click(object sender, System.EventArgs e)
		{
			// Изменение интервала поиска
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DbCard.searchCondition.SetDateInterval(dialog.StartDate, dialog.EndDate);
			this.Text = "Карточки заказов " + DbCard.searchCondition.DateIntervalTxt;
		}

		private void checkBoxSetSelectPartner_CheckedChanged(object sender, System.EventArgs e)
		{
			// При нажатой карточке производим выбор клиента для поиска
			// Выставляем подсказку
			// Устанавливаем условие
			if(checkBoxSetSelectPartner.Checked)
			{
				FormPartnerList dialog = new FormPartnerList();
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK)
				{
					checkBoxSetSelectPartner.Checked = false;
					return;
				}
				DbCard.searchCondition.SetCodePartner(dialog.Partner.Code);
				toolTip1.SetToolTip(checkBoxSetSelectPartner, dialog.Partner.Title);
			}
			else
			{
				DbCard.searchCondition.SetCodePartner(0);
				toolTip1.SetToolTip(checkBoxSetSelectPartner, "Фильтр по клиенту");
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Пытаемся анулировать выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			FormJournal dialog = new FormJournal(card, DateTime.Today);
			dialog.Show();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			// Показ окна с расписанием
			FormSchedule dialog = new FormSchedule();
			dialog.Show();
		}

		private void buttonPrintBuhg_Click(object sender, System.EventArgs e)
		{
			// Печать выбранной карточки заказа
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			// Проверяем статус карточки
			if(card.IsWarrantOpened == false)
			{
				MessageBox.Show(this, "Нельзя печатать справку для неоткрытого заказ-наряда");
				return;
			}
			if(card.IsWarrantClosed == true)
			{
				string login = Form1.currentLogin.ToLower();
				if (login != "заякинм" && login != "админ")
				{
					MessageBox.Show(this, "Вам запрещено печатать бухгалтерскую справку закрытого заказ-наряда");
					return;
				}
			}
			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintBuhg();
		}

		private void checkBoxSearchAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			// При нажатой карточке производим выбор автомобиля для поиска
			// Выставляем подсказку
			// Устанавливаем условие
			if(checkBoxSearchAuto.Checked)
			{
				FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK)
				{
					checkBoxSearchAuto.Checked = false;
					return;
				}
				DbCard.searchCondition.SetCodeAuto(dialog.Auto.Code);
				toolTip1.SetToolTip(checkBoxSearchAuto, dialog.Auto.Title);
			}
			else
			{
				DbCard.searchCondition.SetCodeAuto(0);
				toolTip1.SetToolTip(checkBoxSearchAuto, "Фильтр по автомобилю");
			}
		}

		private void buttonPrintDocument_Click(object sender, System.EventArgs e)
		{
			// Печать выбранной карточки заказа
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;
			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintDocument();
		}

		private void buttonCancelClose_Click(object sender, System.EventArgs e)
		{
			// Пытаемся анулировать выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			DbCardAction act = new DbCardAction(card);
			if(act.WriteCancelClose() == false)
			{
				Db.ShowFaults();
				return;
			}
			listView1.Items.Remove(item);
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			// Смена контрагента карточки
			// Админовская привилегия
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;

			// Запрос нового контрагента
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if(card.Replace(dialog.Partner) == true)
			{
				card.Partner = dialog.Partner;
				card.SetLVItem(item);
			}
		}

		private void buttonToExcel_Click(object sender, System.EventArgs e)
		{
			// Преобразование выбранного заказ-наряда в EXCEL
			//ListViewItem item = Db.GetItemSelected(listView1);
			//if(item == null) return;
			//DbCard card = (DbCard)item.Tag;
			//if(card == null) return;

			//ExcelCard	excelCard = new ExcelCard(card, null);
			//excelCard.StartDownLoad();

			ExcelCard.DownloadList(listView1);
			Db.ShowFaults();
		}

		private void buttonPrintRequest_Click(object sender, System.EventArgs e)
		{
			// Печать выбранной карточки заказа
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCard card = (DbCard)item.Tag;
			if(card == null) return;
			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintRequest();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			// Выгрузка отчета по ТО
			// Запрос даты закрытия заказ-наряда
		//	FormSelectDate dlg = new FormSelectDate();
		//	dlg.ShowDialog();
		//	if(dlg.DialogResult != DialogResult.OK) return;
		//	DateTime date = dlg.SelectedDate;
		//	
		//	ArrayList array = new ArrayList();
		//	DbCard.FillArrayClosedDate(array, date);
		//	ExcelCardReport.DownloadList(array);
		//	Db.ShowFaults();

			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;
			

			// Новый способ получения списка карточке закрытых на указанную дату
			// Считаем, что работаем в одном году
			ArrayList	number_array	= new ArrayList();
			DbSqlCard.SelectCardClosedNumber(number_array, date_start, date_end);
			ArrayList array = new ArrayList();
			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach(DtCard element in number_array)
			{
				
				DbCard card = DbCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				array.Add(card);
				info.SetText(card.NumberTxt);
			}
			info.SetText("Конец отсчета");
			info.Close();

			
			//DbCard.FillArrayClosedInterval(array, date_start, date_end);
			ExcelCardReport.DownloadList(array);
			Db.ShowFaults();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			// Получение деталей по выбранной карточке
			ListViewItem itm = Db.GetItemSelected(listView1);
			if(itm == null) return;
			DbCard card = (DbCard)itm.Tag;
			if(card == null) return;
			FormCardDetailExchange dialog = new FormCardDetailExchange(true, card);
			dialog.ShowDialog();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			// Возврат деталей по выбранной карточке
			ListViewItem itm = Db.GetItemSelected(listView1);
			if(itm == null) return;
			DbCard card = (DbCard)itm.Tag;
			if(card == null) return;
			FormCardDetailExchange dialog = new FormCardDetailExchange(false, card);
			dialog.ShowDialog();
		}

		private void button_check_Click(object sender, System.EventArgs e)
		{
			// Проверить автомобиль выбранной карточки на предписания
			// Возврат деталей по выбранной карточке
			ListViewItem itm = Db.GetItemSelected(listView1);
			if(itm == null) return;
			DbCard card = (DbCard)itm.Tag;
			if(card == null) return;
			
			// И опять контроль свойств автомобиля
			// Проверяем наличие номера для запчастей
			if(card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			// Выгрузка гарантии по АВТОВАЗ на выбранную дату
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			bool works;
			if(MessageBox.Show("Выгружать работы?", "Запрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
				works = true;
			else
				works = false;
			
			
			// Старый вариант
			// ArrayList array = new ArrayList();
			// DbCard.FillArrayAutovaz(array, date_start, date_end);
			// Новый вариант
			ArrayList	number_array	= new ArrayList();
			DbSqlCard.SelectCardClosedNumberAvtovaz(number_array, date_start, date_end);
			ArrayList array = new ArrayList();
			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach (DtCard element in number_array)
			{
				int count = 0;
				DbCard card = null;
				while (count < 5 && card == null)
				{
					card = DbCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
					count++;
				}
				if (card != null)
				{
					array.Add(card);
					info.SetText(card.NumberTxt);
				}
			}
			info.SetText("Конец отсчета");
			info.Close();

			ExcelCardReport3.DownloadList(array, works);
			Db.ShowFaults();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			// Тест печати
			DbPrintCard1 prn = new DbPrintCard1();
			prn.Print();
		}

		public DbCard SelectedCard
		{
			get
			{
				return selectedCard;
			}
		}
	}
}
