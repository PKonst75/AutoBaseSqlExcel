using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardControl.
	/// </summary>
	public class FormCardControl : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_juridical;
		private System.Windows.Forms.TextBox textBox_cashless;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.TextBox textBox_contact;
		private System.Windows.Forms.TextBox textBox_represent;
		private System.Windows.Forms.TextBox textBox_represent_document;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.TextBox textBox_vin;
		private System.Windows.Forms.TextBox textBox_sign;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox_selldate;
		private System.Windows.Forms.TextBox textBox_run;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBox_to;
		private System.Windows.Forms.TextBox textBox_90;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.TextBox textBox_date;
		private System.Windows.Forms.TextBox textBox_warrant_number;
		private System.Windows.Forms.TextBox textBox_warrant_open;
		private System.Windows.Forms.TextBox textBox_warrant_close;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textBox_state;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox textBox_workshop;
		private System.Windows.Forms.TextBox textBox_guaranty_type;
		private System.Windows.Forms.TextBox textBox_auto_type;
		private System.Windows.Forms.CheckBox checkBox_cashless;
		private System.Windows.Forms.CheckBox checkBox_inner;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox checkBox_to;
		private System.Windows.Forms.CheckBox checkBox_ppp;
		private System.Windows.Forms.CheckBox checkBox_wash;
		private System.Windows.Forms.CheckBox checkBox_work;
		private System.Windows.Forms.CheckBox checkBox_norm;
		private System.Windows.Forms.TextBox textBox_r_to;
		private System.Windows.Forms.TextBox textBox_r_ppp;
		private System.Windows.Forms.TextBox textBox_r_wash;
		private System.Windows.Forms.TextBox textBox_r_work;
		private System.Windows.Forms.TextBox textBox_r_norm;
		private System.Windows.Forms.TextBox textBox_r_normtime;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox textBox_r_summ;
		private System.Windows.Forms.TextBox textBox_r_whole;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TextBox textBox_rg_whole;
		private System.Windows.Forms.TextBox textBox_rg_summ;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox textBox_rg_normtime;
		private System.Windows.Forms.TextBox textBox_rg_norm;
		private System.Windows.Forms.TextBox textBox_rg_work;
		private System.Windows.Forms.TextBox textBox_rg_wash;
		private System.Windows.Forms.TextBox textBox_rg_ppp;
		private System.Windows.Forms.TextBox textBox_rg_to;
		private System.Windows.Forms.CheckBox checkBox_rg_norm;
		private System.Windows.Forms.CheckBox checkBox_rg_work;
		private System.Windows.Forms.CheckBox checkBox_rg_wash;
		private System.Windows.Forms.CheckBox checkBox_rg_ppp;
		private System.Windows.Forms.CheckBox checkBox_rg_to;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.TextBox textBox_card_summ_work;
		private System.Windows.Forms.CheckBox checkBox_r_liquid;
		private System.Windows.Forms.CheckBox checkBox_r_liquidbad;
		private System.Windows.Forms.CheckBox checkBox_r_detail;
		private System.Windows.Forms.CheckBox checkBox_r_detailbad;
		private System.Windows.Forms.TextBox textBox_r_liquid;
		private System.Windows.Forms.TextBox textBox_r_liquid_input;
		private System.Windows.Forms.TextBox textBox_r_detail;
		private System.Windows.Forms.TextBox textBox_r_detail_input;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox textBox_card_summ_detail;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.TextBox textBox_rg_detail_summ;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textBox_rg_detail_input;
		private System.Windows.Forms.TextBox textBox_rg_detail;
		private System.Windows.Forms.TextBox textBox_rg_liquid_input;
		private System.Windows.Forms.TextBox textBox_rg_liquid;
		private System.Windows.Forms.CheckBox checkBox_rg_detailbad;
		private System.Windows.Forms.CheckBox checkBox_rg_detail;
		private System.Windows.Forms.CheckBox checkBox_rg_liquidbad;
		private System.Windows.Forms.CheckBox checkBox_rg_liquid;
		private System.Windows.Forms.TextBox textBox_rg_input_summ;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListView listView_work;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;


		DbCardReport1		report1;
		DbAutoType			auto_type;
		DbCard.REP			rep;
		DtPartnerProperty	owner_property;
		DtCard				card;
		DtTxtCard txtCard; // Текстовое отображение карточки
		DbPartner			owner;
		DbPartner			represent;
		DbAuto				auto;
		DbGuarantyType		guaranty_type;
		DbWorkshop			workshop;
		DbStaff				master;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox textBox_master;
		ListViewItem		lvitem_detail	= null;

		public FormCardControl(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Загрузка листа с изображениями
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl000.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl001.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl002.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl003.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl004.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl005.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl006.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl007.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl008.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\dtl009.bmp"));


			// Загрузка карточки
			card = DbSqlCard.Find(code);
			if(card == null)
			{
				this.Text	= "Ошибка загрузки";
				return;
			}
			txtCard = new DtTxtCard(card);
			// Загрузка всех дополнительных данных
			// Загрузка владельца по карточке
			owner			= DbPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
			represent		= DbPartner.Find((long)card.GetData("ПРЕДСТАВИТЕЛЬ_КАРТОЧКА"));
			auto			= DbAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
			owner_property	= DbSqlPartnerProperty.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
			rep				= DbCard.Report1((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
			guaranty_type	= DbGuarantyType.Find((long)card.GetData("ВИД_ГАРАНТИЯ_КАРТОЧКА"));
			workshop		= DbWorkshop.Find((long)card.GetData("ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА"));
			auto_type		= DbAutoType.Find((long)card.GetData("ВИД_ТРУДОЕМКОСТЬ_КАРТОЧКА"));
			master			= DbStaff.Find((long)card.GetData("МАСТЕР_КОНТРОЛЕР_КАРТОЧКА"));
			// Отчеты
			report1			= new DbCardReport1((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));

			// Отображение в форме
			SetOwner();
			SetAuto();
			SetCard();
			SetReportWork();
			SetReportWorkGuaranty();
			SetReportDetail();
			SetReportDetailGuaranty();

			// Детали
			DbSqlCardDetail.SelectInList(card, listView1);
			// Работы
			DbSqlCardWork.SelectInList(card, listView_work);
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkBox_inner = new System.Windows.Forms.CheckBox();
			this.checkBox_cashless = new System.Windows.Forms.CheckBox();
			this.textBox_auto_type = new System.Windows.Forms.TextBox();
			this.textBox_guaranty_type = new System.Windows.Forms.TextBox();
			this.textBox_workshop = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.textBox_state = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textBox_warrant_close = new System.Windows.Forms.TextBox();
			this.textBox_warrant_open = new System.Windows.Forms.TextBox();
			this.textBox_date = new System.Windows.Forms.TextBox();
			this.textBox_warrant_number = new System.Windows.Forms.TextBox();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox_90 = new System.Windows.Forms.TextBox();
			this.textBox_to = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textBox_run = new System.Windows.Forms.TextBox();
			this.textBox_selldate = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_sign = new System.Windows.Forms.TextBox();
			this.textBox_vin = new System.Windows.Forms.TextBox();
			this.textBox_model = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox_represent_document = new System.Windows.Forms.TextBox();
			this.textBox_represent = new System.Windows.Forms.TextBox();
			this.textBox_contact = new System.Windows.Forms.TextBox();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.textBox_cashless = new System.Windows.Forms.TextBox();
			this.textBox_juridical = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.textBox_rg_input_summ = new System.Windows.Forms.TextBox();
			this.textBox_rg_detail_summ = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.textBox_rg_detail_input = new System.Windows.Forms.TextBox();
			this.textBox_rg_detail = new System.Windows.Forms.TextBox();
			this.textBox_rg_liquid_input = new System.Windows.Forms.TextBox();
			this.textBox_rg_liquid = new System.Windows.Forms.TextBox();
			this.checkBox_rg_detailbad = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_detail = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_liquidbad = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_liquid = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.textBox_card_summ_detail = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.textBox_r_detail_input = new System.Windows.Forms.TextBox();
			this.textBox_r_detail = new System.Windows.Forms.TextBox();
			this.textBox_r_liquid_input = new System.Windows.Forms.TextBox();
			this.textBox_r_liquid = new System.Windows.Forms.TextBox();
			this.checkBox_r_detailbad = new System.Windows.Forms.CheckBox();
			this.checkBox_r_detail = new System.Windows.Forms.CheckBox();
			this.checkBox_r_liquidbad = new System.Windows.Forms.CheckBox();
			this.checkBox_r_liquid = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textBox_rg_whole = new System.Windows.Forms.TextBox();
			this.textBox_rg_summ = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.textBox_rg_normtime = new System.Windows.Forms.TextBox();
			this.textBox_rg_norm = new System.Windows.Forms.TextBox();
			this.textBox_rg_work = new System.Windows.Forms.TextBox();
			this.textBox_rg_wash = new System.Windows.Forms.TextBox();
			this.textBox_rg_ppp = new System.Windows.Forms.TextBox();
			this.textBox_rg_to = new System.Windows.Forms.TextBox();
			this.checkBox_rg_norm = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_work = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_wash = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_ppp = new System.Windows.Forms.CheckBox();
			this.checkBox_rg_to = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBox_card_summ_work = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.textBox_r_whole = new System.Windows.Forms.TextBox();
			this.textBox_r_summ = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.textBox_r_normtime = new System.Windows.Forms.TextBox();
			this.textBox_r_norm = new System.Windows.Forms.TextBox();
			this.textBox_r_work = new System.Windows.Forms.TextBox();
			this.textBox_r_wash = new System.Windows.Forms.TextBox();
			this.textBox_r_ppp = new System.Windows.Forms.TextBox();
			this.textBox_r_to = new System.Windows.Forms.TextBox();
			this.checkBox_norm = new System.Windows.Forms.CheckBox();
			this.checkBox_work = new System.Windows.Forms.CheckBox();
			this.checkBox_wash = new System.Windows.Forms.CheckBox();
			this.checkBox_ppp = new System.Windows.Forms.CheckBox();
			this.checkBox_to = new System.Windows.Forms.CheckBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.listView_work = new System.Windows.Forms.ListView();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label28 = new System.Windows.Forms.Label();
			this.textBox_master = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2,
																					  this.tabPage3});
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1012, 552);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox3,
																				   this.groupBox2,
																				   this.groupBox1});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(1004, 523);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Шапка";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_master,
																					this.label28,
																					this.checkBox_inner,
																					this.checkBox_cashless,
																					this.textBox_auto_type,
																					this.textBox_guaranty_type,
																					this.textBox_workshop,
																					this.label22,
																					this.label21,
																					this.label20,
																					this.textBox_state,
																					this.label19,
																					this.textBox_warrant_close,
																					this.textBox_warrant_open,
																					this.textBox_date,
																					this.textBox_warrant_number,
																					this.textBox_number,
																					this.label18,
																					this.label17,
																					this.label16,
																					this.label15,
																					this.label14});
			this.groupBox3.Location = new System.Drawing.Point(8, 312);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(632, 200);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Карточка";
			// 
			// checkBox_inner
			// 
			this.checkBox_inner.Enabled = false;
			this.checkBox_inner.Location = new System.Drawing.Point(352, 104);
			this.checkBox_inner.Name = "checkBox_inner";
			this.checkBox_inner.TabIndex = 19;
			this.checkBox_inner.Text = "Внутренний";
			// 
			// checkBox_cashless
			// 
			this.checkBox_cashless.Enabled = false;
			this.checkBox_cashless.Location = new System.Drawing.Point(352, 80);
			this.checkBox_cashless.Name = "checkBox_cashless";
			this.checkBox_cashless.Size = new System.Drawing.Size(136, 24);
			this.checkBox_cashless.TabIndex = 18;
			this.checkBox_cashless.Text = "Безналичный";
			// 
			// textBox_auto_type
			// 
			this.textBox_auto_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_auto_type.Enabled = false;
			this.textBox_auto_type.Location = new System.Drawing.Point(128, 128);
			this.textBox_auto_type.Name = "textBox_auto_type";
			this.textBox_auto_type.Size = new System.Drawing.Size(200, 23);
			this.textBox_auto_type.TabIndex = 17;
			this.textBox_auto_type.Text = "";
			// 
			// textBox_guaranty_type
			// 
			this.textBox_guaranty_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_guaranty_type.Enabled = false;
			this.textBox_guaranty_type.Location = new System.Drawing.Point(128, 104);
			this.textBox_guaranty_type.Name = "textBox_guaranty_type";
			this.textBox_guaranty_type.Size = new System.Drawing.Size(200, 23);
			this.textBox_guaranty_type.TabIndex = 16;
			this.textBox_guaranty_type.Text = "";
			// 
			// textBox_workshop
			// 
			this.textBox_workshop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_workshop.Enabled = false;
			this.textBox_workshop.Location = new System.Drawing.Point(128, 80);
			this.textBox_workshop.Name = "textBox_workshop";
			this.textBox_workshop.Size = new System.Drawing.Size(200, 23);
			this.textBox_workshop.TabIndex = 15;
			this.textBox_workshop.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 128);
			this.label22.Name = "label22";
			this.label22.TabIndex = 14;
			this.label22.Text = "Трудоемкости";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 104);
			this.label21.Name = "label21";
			this.label21.TabIndex = 13;
			this.label21.Text = "Вид гарантии";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 80);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(136, 23);
			this.label20.TabIndex = 12;
			this.label20.Text = "Подразделение";
			// 
			// textBox_state
			// 
			this.textBox_state.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_state.Enabled = false;
			this.textBox_state.Location = new System.Drawing.Point(488, 24);
			this.textBox_state.Name = "textBox_state";
			this.textBox_state.Size = new System.Drawing.Size(128, 23);
			this.textBox_state.TabIndex = 11;
			this.textBox_state.Text = "";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(432, 24);
			this.label19.Name = "label19";
			this.label19.TabIndex = 10;
			this.label19.Text = "Статус";
			// 
			// textBox_warrant_close
			// 
			this.textBox_warrant_close.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_warrant_close.Enabled = false;
			this.textBox_warrant_close.Location = new System.Drawing.Point(488, 48);
			this.textBox_warrant_close.Name = "textBox_warrant_close";
			this.textBox_warrant_close.Size = new System.Drawing.Size(128, 23);
			this.textBox_warrant_close.TabIndex = 9;
			this.textBox_warrant_close.Text = "";
			// 
			// textBox_warrant_open
			// 
			this.textBox_warrant_open.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_warrant_open.Enabled = false;
			this.textBox_warrant_open.Location = new System.Drawing.Point(288, 48);
			this.textBox_warrant_open.Name = "textBox_warrant_open";
			this.textBox_warrant_open.Size = new System.Drawing.Size(128, 23);
			this.textBox_warrant_open.TabIndex = 8;
			this.textBox_warrant_open.Text = "";
			// 
			// textBox_date
			// 
			this.textBox_date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_date.Enabled = false;
			this.textBox_date.Location = new System.Drawing.Point(288, 24);
			this.textBox_date.Name = "textBox_date";
			this.textBox_date.Size = new System.Drawing.Size(128, 23);
			this.textBox_date.TabIndex = 7;
			this.textBox_date.Text = "";
			// 
			// textBox_warrant_number
			// 
			this.textBox_warrant_number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_warrant_number.Enabled = false;
			this.textBox_warrant_number.Location = new System.Drawing.Point(120, 48);
			this.textBox_warrant_number.Name = "textBox_warrant_number";
			this.textBox_warrant_number.Size = new System.Drawing.Size(64, 23);
			this.textBox_warrant_number.TabIndex = 6;
			this.textBox_warrant_number.Text = "";
			// 
			// textBox_number
			// 
			this.textBox_number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_number.Enabled = false;
			this.textBox_number.Location = new System.Drawing.Point(120, 24);
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.Size = new System.Drawing.Size(64, 23);
			this.textBox_number.TabIndex = 5;
			this.textBox_number.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(432, 48);
			this.label18.Name = "label18";
			this.label18.TabIndex = 4;
			this.label18.Text = "Закрыт";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(192, 48);
			this.label17.Name = "label17";
			this.label17.TabIndex = 3;
			this.label17.Text = "Открыт";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 48);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(136, 23);
			this.label16.TabIndex = 2;
			this.label16.Text = "Заказ-наряд №";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(192, 24);
			this.label15.Name = "label15";
			this.label15.TabIndex = 1;
			this.label15.Text = "Дата и время";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 24);
			this.label14.Name = "label14";
			this.label14.TabIndex = 0;
			this.label14.Text = "Карточка №";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_90,
																					this.textBox_to,
																					this.label13,
																					this.label12,
																					this.textBox_run,
																					this.textBox_selldate,
																					this.label11,
																					this.label10,
																					this.textBox_sign,
																					this.textBox_vin,
																					this.textBox_model,
																					this.label9,
																					this.label8,
																					this.label7});
			this.groupBox2.Location = new System.Drawing.Point(8, 168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(632, 136);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Автомобиль";
			// 
			// textBox_90
			// 
			this.textBox_90.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_90.Enabled = false;
			this.textBox_90.Location = new System.Drawing.Point(408, 96);
			this.textBox_90.Name = "textBox_90";
			this.textBox_90.Size = new System.Drawing.Size(216, 23);
			this.textBox_90.TabIndex = 13;
			this.textBox_90.Text = "";
			// 
			// textBox_to
			// 
			this.textBox_to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_to.Enabled = false;
			this.textBox_to.Location = new System.Drawing.Point(408, 72);
			this.textBox_to.Name = "textBox_to";
			this.textBox_to.Size = new System.Drawing.Size(216, 23);
			this.textBox_to.TabIndex = 12;
			this.textBox_to.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(304, 96);
			this.label13.Name = "label13";
			this.label13.TabIndex = 11;
			this.label13.Text = "За 90 дней";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(304, 72);
			this.label12.Name = "label12";
			this.label12.TabIndex = 10;
			this.label12.Text = "ТО";
			// 
			// textBox_run
			// 
			this.textBox_run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_run.Enabled = false;
			this.textBox_run.Location = new System.Drawing.Point(408, 48);
			this.textBox_run.Name = "textBox_run";
			this.textBox_run.Size = new System.Drawing.Size(216, 23);
			this.textBox_run.TabIndex = 9;
			this.textBox_run.Text = "";
			// 
			// textBox_selldate
			// 
			this.textBox_selldate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_selldate.Enabled = false;
			this.textBox_selldate.Location = new System.Drawing.Point(408, 24);
			this.textBox_selldate.Name = "textBox_selldate";
			this.textBox_selldate.Size = new System.Drawing.Size(216, 23);
			this.textBox_selldate.TabIndex = 8;
			this.textBox_selldate.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(304, 48);
			this.label11.Name = "label11";
			this.label11.TabIndex = 7;
			this.label11.Text = "Пробег";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(304, 24);
			this.label10.Name = "label10";
			this.label10.TabIndex = 6;
			this.label10.Text = "Дата продажи";
			// 
			// textBox_sign
			// 
			this.textBox_sign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_sign.Enabled = false;
			this.textBox_sign.Location = new System.Drawing.Point(112, 72);
			this.textBox_sign.Name = "textBox_sign";
			this.textBox_sign.Size = new System.Drawing.Size(184, 23);
			this.textBox_sign.TabIndex = 5;
			this.textBox_sign.Text = "";
			// 
			// textBox_vin
			// 
			this.textBox_vin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_vin.Enabled = false;
			this.textBox_vin.Location = new System.Drawing.Point(112, 48);
			this.textBox_vin.Name = "textBox_vin";
			this.textBox_vin.Size = new System.Drawing.Size(184, 23);
			this.textBox_vin.TabIndex = 4;
			this.textBox_vin.Text = "";
			// 
			// textBox_model
			// 
			this.textBox_model.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_model.Enabled = false;
			this.textBox_model.Location = new System.Drawing.Point(112, 24);
			this.textBox_model.Name = "textBox_model";
			this.textBox_model.Size = new System.Drawing.Size(184, 23);
			this.textBox_model.TabIndex = 3;
			this.textBox_model.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 72);
			this.label9.Name = "label9";
			this.label9.TabIndex = 2;
			this.label9.Text = "Рег.Знак";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 48);
			this.label8.Name = "label8";
			this.label8.TabIndex = 1;
			this.label8.Text = "VIN";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 24);
			this.label7.Name = "label7";
			this.label7.TabIndex = 0;
			this.label7.Text = "Модель";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_represent_document,
																					this.textBox_represent,
																					this.textBox_contact,
																					this.textBox_name,
																					this.textBox_cashless,
																					this.textBox_juridical,
																					this.label6,
																					this.label5,
																					this.label4,
																					this.label3,
																					this.label2,
																					this.label1});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(632, 152);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Владелец/Представитель";
			// 
			// textBox_represent_document
			// 
			this.textBox_represent_document.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_represent_document.Enabled = false;
			this.textBox_represent_document.Location = new System.Drawing.Point(152, 120);
			this.textBox_represent_document.Name = "textBox_represent_document";
			this.textBox_represent_document.Size = new System.Drawing.Size(472, 23);
			this.textBox_represent_document.TabIndex = 11;
			this.textBox_represent_document.Text = "";
			// 
			// textBox_represent
			// 
			this.textBox_represent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_represent.Enabled = false;
			this.textBox_represent.Location = new System.Drawing.Point(152, 96);
			this.textBox_represent.Name = "textBox_represent";
			this.textBox_represent.Size = new System.Drawing.Size(472, 23);
			this.textBox_represent.TabIndex = 10;
			this.textBox_represent.Text = "";
			// 
			// textBox_contact
			// 
			this.textBox_contact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_contact.Enabled = false;
			this.textBox_contact.Location = new System.Drawing.Point(152, 72);
			this.textBox_contact.Name = "textBox_contact";
			this.textBox_contact.Size = new System.Drawing.Size(472, 23);
			this.textBox_contact.TabIndex = 9;
			this.textBox_contact.Text = "";
			// 
			// textBox_name
			// 
			this.textBox_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_name.Enabled = false;
			this.textBox_name.Location = new System.Drawing.Point(152, 48);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(472, 23);
			this.textBox_name.TabIndex = 8;
			this.textBox_name.Text = "";
			// 
			// textBox_cashless
			// 
			this.textBox_cashless.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_cashless.Enabled = false;
			this.textBox_cashless.Location = new System.Drawing.Point(456, 24);
			this.textBox_cashless.Name = "textBox_cashless";
			this.textBox_cashless.Size = new System.Drawing.Size(168, 23);
			this.textBox_cashless.TabIndex = 7;
			this.textBox_cashless.Text = "";
			// 
			// textBox_juridical
			// 
			this.textBox_juridical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_juridical.Enabled = false;
			this.textBox_juridical.Location = new System.Drawing.Point(112, 24);
			this.textBox_juridical.Name = "textBox_juridical";
			this.textBox_juridical.Size = new System.Drawing.Size(184, 23);
			this.textBox_juridical.TabIndex = 6;
			this.textBox_juridical.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 120);
			this.label6.Name = "label6";
			this.label6.TabIndex = 5;
			this.label6.Text = "По документу";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "Представитель";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(304, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 23);
			this.label4.TabIndex = 3;
			this.label4.Text = "Основной вид расчета";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 2;
			this.label3.Text = "Вид";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Контакты";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "ФИО/Наименование";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox7,
																				   this.groupBox6,
																				   this.groupBox5,
																				   this.groupBox4});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(1004, 526);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Отчет";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_rg_input_summ,
																					this.textBox_rg_detail_summ,
																					this.label27,
																					this.textBox_rg_detail_input,
																					this.textBox_rg_detail,
																					this.textBox_rg_liquid_input,
																					this.textBox_rg_liquid,
																					this.checkBox_rg_detailbad,
																					this.checkBox_rg_detail,
																					this.checkBox_rg_liquidbad,
																					this.checkBox_rg_liquid});
			this.groupBox7.Location = new System.Drawing.Point(344, 232);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(328, 168);
			this.groupBox7.TabIndex = 3;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Детали гарантия";
			// 
			// textBox_rg_input_summ
			// 
			this.textBox_rg_input_summ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_input_summ.Enabled = false;
			this.textBox_rg_input_summ.Location = new System.Drawing.Point(240, 136);
			this.textBox_rg_input_summ.Name = "textBox_rg_input_summ";
			this.textBox_rg_input_summ.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_input_summ.TabIndex = 20;
			this.textBox_rg_input_summ.Text = "";
			// 
			// textBox_rg_detail_summ
			// 
			this.textBox_rg_detail_summ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_detail_summ.Enabled = false;
			this.textBox_rg_detail_summ.Location = new System.Drawing.Point(160, 136);
			this.textBox_rg_detail_summ.Name = "textBox_rg_detail_summ";
			this.textBox_rg_detail_summ.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_detail_summ.TabIndex = 19;
			this.textBox_rg_detail_summ.Text = "";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(16, 136);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(120, 23);
			this.label27.TabIndex = 18;
			this.label27.Text = "Сумма";
			// 
			// textBox_rg_detail_input
			// 
			this.textBox_rg_detail_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_detail_input.Enabled = false;
			this.textBox_rg_detail_input.Location = new System.Drawing.Point(240, 65);
			this.textBox_rg_detail_input.Name = "textBox_rg_detail_input";
			this.textBox_rg_detail_input.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_detail_input.TabIndex = 17;
			this.textBox_rg_detail_input.Text = "";
			// 
			// textBox_rg_detail
			// 
			this.textBox_rg_detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_detail.Enabled = false;
			this.textBox_rg_detail.Location = new System.Drawing.Point(160, 65);
			this.textBox_rg_detail.Name = "textBox_rg_detail";
			this.textBox_rg_detail.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_detail.TabIndex = 16;
			this.textBox_rg_detail.Text = "";
			// 
			// textBox_rg_liquid_input
			// 
			this.textBox_rg_liquid_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_liquid_input.Enabled = false;
			this.textBox_rg_liquid_input.Location = new System.Drawing.Point(240, 17);
			this.textBox_rg_liquid_input.Name = "textBox_rg_liquid_input";
			this.textBox_rg_liquid_input.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_liquid_input.TabIndex = 15;
			this.textBox_rg_liquid_input.Text = "";
			// 
			// textBox_rg_liquid
			// 
			this.textBox_rg_liquid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_liquid.Enabled = false;
			this.textBox_rg_liquid.Location = new System.Drawing.Point(160, 17);
			this.textBox_rg_liquid.Name = "textBox_rg_liquid";
			this.textBox_rg_liquid.Size = new System.Drawing.Size(72, 23);
			this.textBox_rg_liquid.TabIndex = 14;
			this.textBox_rg_liquid.Text = "";
			// 
			// checkBox_rg_detailbad
			// 
			this.checkBox_rg_detailbad.Enabled = false;
			this.checkBox_rg_detailbad.Location = new System.Drawing.Point(16, 89);
			this.checkBox_rg_detailbad.Name = "checkBox_rg_detailbad";
			this.checkBox_rg_detailbad.Size = new System.Drawing.Size(128, 24);
			this.checkBox_rg_detailbad.TabIndex = 13;
			this.checkBox_rg_detailbad.Text = "Нерасцененные";
			// 
			// checkBox_rg_detail
			// 
			this.checkBox_rg_detail.Enabled = false;
			this.checkBox_rg_detail.Location = new System.Drawing.Point(16, 65);
			this.checkBox_rg_detail.Name = "checkBox_rg_detail";
			this.checkBox_rg_detail.TabIndex = 12;
			this.checkBox_rg_detail.Text = "Детали";
			// 
			// checkBox_rg_liquidbad
			// 
			this.checkBox_rg_liquidbad.Enabled = false;
			this.checkBox_rg_liquidbad.Location = new System.Drawing.Point(16, 41);
			this.checkBox_rg_liquidbad.Name = "checkBox_rg_liquidbad";
			this.checkBox_rg_liquidbad.Size = new System.Drawing.Size(144, 24);
			this.checkBox_rg_liquidbad.TabIndex = 11;
			this.checkBox_rg_liquidbad.Text = "Нерасцененные";
			// 
			// checkBox_rg_liquid
			// 
			this.checkBox_rg_liquid.Enabled = false;
			this.checkBox_rg_liquid.Location = new System.Drawing.Point(16, 17);
			this.checkBox_rg_liquid.Name = "checkBox_rg_liquid";
			this.checkBox_rg_liquid.TabIndex = 10;
			this.checkBox_rg_liquid.Text = "Жидкости";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_card_summ_detail,
																					this.label26,
																					this.textBox_r_detail_input,
																					this.textBox_r_detail,
																					this.textBox_r_liquid_input,
																					this.textBox_r_liquid,
																					this.checkBox_r_detailbad,
																					this.checkBox_r_detail,
																					this.checkBox_r_liquidbad,
																					this.checkBox_r_liquid});
			this.groupBox6.Location = new System.Drawing.Point(8, 232);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(328, 168);
			this.groupBox6.TabIndex = 2;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Детали";
			// 
			// textBox_card_summ_detail
			// 
			this.textBox_card_summ_detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_card_summ_detail.Enabled = false;
			this.textBox_card_summ_detail.Location = new System.Drawing.Point(136, 136);
			this.textBox_card_summ_detail.Name = "textBox_card_summ_detail";
			this.textBox_card_summ_detail.TabIndex = 9;
			this.textBox_card_summ_detail.Text = "";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(8, 136);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(120, 23);
			this.label26.TabIndex = 8;
			this.label26.Text = "Сумма ДЕТАЛИ";
			// 
			// textBox_r_detail_input
			// 
			this.textBox_r_detail_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_detail_input.Enabled = false;
			this.textBox_r_detail_input.Location = new System.Drawing.Point(232, 72);
			this.textBox_r_detail_input.Name = "textBox_r_detail_input";
			this.textBox_r_detail_input.Size = new System.Drawing.Size(72, 23);
			this.textBox_r_detail_input.TabIndex = 7;
			this.textBox_r_detail_input.Text = "";
			// 
			// textBox_r_detail
			// 
			this.textBox_r_detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_detail.Enabled = false;
			this.textBox_r_detail.Location = new System.Drawing.Point(152, 72);
			this.textBox_r_detail.Name = "textBox_r_detail";
			this.textBox_r_detail.Size = new System.Drawing.Size(72, 23);
			this.textBox_r_detail.TabIndex = 6;
			this.textBox_r_detail.Text = "";
			// 
			// textBox_r_liquid_input
			// 
			this.textBox_r_liquid_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_liquid_input.Enabled = false;
			this.textBox_r_liquid_input.Location = new System.Drawing.Point(232, 24);
			this.textBox_r_liquid_input.Name = "textBox_r_liquid_input";
			this.textBox_r_liquid_input.Size = new System.Drawing.Size(72, 23);
			this.textBox_r_liquid_input.TabIndex = 5;
			this.textBox_r_liquid_input.Text = "";
			// 
			// textBox_r_liquid
			// 
			this.textBox_r_liquid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_liquid.Enabled = false;
			this.textBox_r_liquid.Location = new System.Drawing.Point(152, 24);
			this.textBox_r_liquid.Name = "textBox_r_liquid";
			this.textBox_r_liquid.Size = new System.Drawing.Size(72, 23);
			this.textBox_r_liquid.TabIndex = 4;
			this.textBox_r_liquid.Text = "";
			// 
			// checkBox_r_detailbad
			// 
			this.checkBox_r_detailbad.Enabled = false;
			this.checkBox_r_detailbad.Location = new System.Drawing.Point(8, 96);
			this.checkBox_r_detailbad.Name = "checkBox_r_detailbad";
			this.checkBox_r_detailbad.Size = new System.Drawing.Size(128, 24);
			this.checkBox_r_detailbad.TabIndex = 3;
			this.checkBox_r_detailbad.Text = "Нерасцененные";
			// 
			// checkBox_r_detail
			// 
			this.checkBox_r_detail.Enabled = false;
			this.checkBox_r_detail.Location = new System.Drawing.Point(8, 72);
			this.checkBox_r_detail.Name = "checkBox_r_detail";
			this.checkBox_r_detail.TabIndex = 2;
			this.checkBox_r_detail.Text = "Детали";
			// 
			// checkBox_r_liquidbad
			// 
			this.checkBox_r_liquidbad.Enabled = false;
			this.checkBox_r_liquidbad.Location = new System.Drawing.Point(8, 48);
			this.checkBox_r_liquidbad.Name = "checkBox_r_liquidbad";
			this.checkBox_r_liquidbad.Size = new System.Drawing.Size(144, 24);
			this.checkBox_r_liquidbad.TabIndex = 1;
			this.checkBox_r_liquidbad.Text = "Нерасцененные";
			// 
			// checkBox_r_liquid
			// 
			this.checkBox_r_liquid.Enabled = false;
			this.checkBox_r_liquid.Location = new System.Drawing.Point(8, 24);
			this.checkBox_r_liquid.Name = "checkBox_r_liquid";
			this.checkBox_r_liquid.TabIndex = 0;
			this.checkBox_r_liquid.Text = "Жидкости";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_rg_whole,
																					this.textBox_rg_summ,
																					this.label24,
																					this.textBox_rg_normtime,
																					this.textBox_rg_norm,
																					this.textBox_rg_work,
																					this.textBox_rg_wash,
																					this.textBox_rg_ppp,
																					this.textBox_rg_to,
																					this.checkBox_rg_norm,
																					this.checkBox_rg_work,
																					this.checkBox_rg_wash,
																					this.checkBox_rg_ppp,
																					this.checkBox_rg_to});
			this.groupBox5.Location = new System.Drawing.Point(344, 8);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(336, 216);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Гарантийные работы";
			// 
			// textBox_rg_whole
			// 
			this.textBox_rg_whole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_whole.Enabled = false;
			this.textBox_rg_whole.Location = new System.Drawing.Point(224, 152);
			this.textBox_rg_whole.Name = "textBox_rg_whole";
			this.textBox_rg_whole.TabIndex = 27;
			this.textBox_rg_whole.Text = "";
			// 
			// textBox_rg_summ
			// 
			this.textBox_rg_summ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_summ.Enabled = false;
			this.textBox_rg_summ.Location = new System.Drawing.Point(120, 152);
			this.textBox_rg_summ.Name = "textBox_rg_summ";
			this.textBox_rg_summ.TabIndex = 26;
			this.textBox_rg_summ.Text = "";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(16, 152);
			this.label24.Name = "label24";
			this.label24.TabIndex = 25;
			this.label24.Text = "Сумма";
			// 
			// textBox_rg_normtime
			// 
			this.textBox_rg_normtime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_normtime.Enabled = false;
			this.textBox_rg_normtime.Location = new System.Drawing.Point(224, 120);
			this.textBox_rg_normtime.Name = "textBox_rg_normtime";
			this.textBox_rg_normtime.TabIndex = 24;
			this.textBox_rg_normtime.Text = "";
			// 
			// textBox_rg_norm
			// 
			this.textBox_rg_norm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_norm.Enabled = false;
			this.textBox_rg_norm.Location = new System.Drawing.Point(120, 120);
			this.textBox_rg_norm.Name = "textBox_rg_norm";
			this.textBox_rg_norm.TabIndex = 23;
			this.textBox_rg_norm.Text = "";
			// 
			// textBox_rg_work
			// 
			this.textBox_rg_work.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_work.Enabled = false;
			this.textBox_rg_work.Location = new System.Drawing.Point(120, 96);
			this.textBox_rg_work.Name = "textBox_rg_work";
			this.textBox_rg_work.TabIndex = 22;
			this.textBox_rg_work.Text = "";
			// 
			// textBox_rg_wash
			// 
			this.textBox_rg_wash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_wash.Enabled = false;
			this.textBox_rg_wash.Location = new System.Drawing.Point(120, 72);
			this.textBox_rg_wash.Name = "textBox_rg_wash";
			this.textBox_rg_wash.TabIndex = 21;
			this.textBox_rg_wash.Text = "";
			// 
			// textBox_rg_ppp
			// 
			this.textBox_rg_ppp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_ppp.Enabled = false;
			this.textBox_rg_ppp.Location = new System.Drawing.Point(120, 48);
			this.textBox_rg_ppp.Name = "textBox_rg_ppp";
			this.textBox_rg_ppp.TabIndex = 20;
			this.textBox_rg_ppp.Text = "";
			// 
			// textBox_rg_to
			// 
			this.textBox_rg_to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_rg_to.Enabled = false;
			this.textBox_rg_to.Location = new System.Drawing.Point(120, 24);
			this.textBox_rg_to.Name = "textBox_rg_to";
			this.textBox_rg_to.TabIndex = 19;
			this.textBox_rg_to.Text = "";
			// 
			// checkBox_rg_norm
			// 
			this.checkBox_rg_norm.Enabled = false;
			this.checkBox_rg_norm.Location = new System.Drawing.Point(16, 120);
			this.checkBox_rg_norm.Name = "checkBox_rg_norm";
			this.checkBox_rg_norm.TabIndex = 18;
			this.checkBox_rg_norm.Text = "Нормачас";
			// 
			// checkBox_rg_work
			// 
			this.checkBox_rg_work.Enabled = false;
			this.checkBox_rg_work.Location = new System.Drawing.Point(16, 96);
			this.checkBox_rg_work.Name = "checkBox_rg_work";
			this.checkBox_rg_work.TabIndex = 17;
			this.checkBox_rg_work.Text = "Договорные";
			// 
			// checkBox_rg_wash
			// 
			this.checkBox_rg_wash.Enabled = false;
			this.checkBox_rg_wash.Location = new System.Drawing.Point(16, 72);
			this.checkBox_rg_wash.Name = "checkBox_rg_wash";
			this.checkBox_rg_wash.TabIndex = 16;
			this.checkBox_rg_wash.Text = "Мойка";
			// 
			// checkBox_rg_ppp
			// 
			this.checkBox_rg_ppp.Enabled = false;
			this.checkBox_rg_ppp.Location = new System.Drawing.Point(16, 48);
			this.checkBox_rg_ppp.Name = "checkBox_rg_ppp";
			this.checkBox_rg_ppp.TabIndex = 15;
			this.checkBox_rg_ppp.Text = "ППП";
			// 
			// checkBox_rg_to
			// 
			this.checkBox_rg_to.Enabled = false;
			this.checkBox_rg_to.Location = new System.Drawing.Point(16, 24);
			this.checkBox_rg_to.Name = "checkBox_rg_to";
			this.checkBox_rg_to.TabIndex = 14;
			this.checkBox_rg_to.Text = "ТО";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_card_summ_work,
																					this.label25,
																					this.textBox_r_whole,
																					this.textBox_r_summ,
																					this.label23,
																					this.textBox_r_normtime,
																					this.textBox_r_norm,
																					this.textBox_r_work,
																					this.textBox_r_wash,
																					this.textBox_r_ppp,
																					this.textBox_r_to,
																					this.checkBox_norm,
																					this.checkBox_work,
																					this.checkBox_wash,
																					this.checkBox_ppp,
																					this.checkBox_to});
			this.groupBox4.Location = new System.Drawing.Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(328, 216);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Работы";
			// 
			// textBox_card_summ_work
			// 
			this.textBox_card_summ_work.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_card_summ_work.Enabled = false;
			this.textBox_card_summ_work.Location = new System.Drawing.Point(128, 184);
			this.textBox_card_summ_work.Name = "textBox_card_summ_work";
			this.textBox_card_summ_work.TabIndex = 15;
			this.textBox_card_summ_work.Text = "";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8, 184);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(120, 23);
			this.label25.TabIndex = 14;
			this.label25.Text = "Сумма РАБОТЫ";
			// 
			// textBox_r_whole
			// 
			this.textBox_r_whole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_whole.Enabled = false;
			this.textBox_r_whole.Location = new System.Drawing.Point(216, 152);
			this.textBox_r_whole.Name = "textBox_r_whole";
			this.textBox_r_whole.TabIndex = 13;
			this.textBox_r_whole.Text = "";
			// 
			// textBox_r_summ
			// 
			this.textBox_r_summ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_summ.Enabled = false;
			this.textBox_r_summ.Location = new System.Drawing.Point(112, 152);
			this.textBox_r_summ.Name = "textBox_r_summ";
			this.textBox_r_summ.TabIndex = 12;
			this.textBox_r_summ.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(8, 152);
			this.label23.Name = "label23";
			this.label23.TabIndex = 11;
			this.label23.Text = "Сумма";
			// 
			// textBox_r_normtime
			// 
			this.textBox_r_normtime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_normtime.Enabled = false;
			this.textBox_r_normtime.Location = new System.Drawing.Point(216, 120);
			this.textBox_r_normtime.Name = "textBox_r_normtime";
			this.textBox_r_normtime.TabIndex = 10;
			this.textBox_r_normtime.Text = "";
			// 
			// textBox_r_norm
			// 
			this.textBox_r_norm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_norm.Enabled = false;
			this.textBox_r_norm.Location = new System.Drawing.Point(112, 120);
			this.textBox_r_norm.Name = "textBox_r_norm";
			this.textBox_r_norm.TabIndex = 9;
			this.textBox_r_norm.Text = "";
			// 
			// textBox_r_work
			// 
			this.textBox_r_work.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_work.Enabled = false;
			this.textBox_r_work.Location = new System.Drawing.Point(112, 96);
			this.textBox_r_work.Name = "textBox_r_work";
			this.textBox_r_work.TabIndex = 8;
			this.textBox_r_work.Text = "";
			// 
			// textBox_r_wash
			// 
			this.textBox_r_wash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_wash.Enabled = false;
			this.textBox_r_wash.Location = new System.Drawing.Point(112, 72);
			this.textBox_r_wash.Name = "textBox_r_wash";
			this.textBox_r_wash.TabIndex = 7;
			this.textBox_r_wash.Text = "";
			// 
			// textBox_r_ppp
			// 
			this.textBox_r_ppp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_ppp.Enabled = false;
			this.textBox_r_ppp.Location = new System.Drawing.Point(112, 48);
			this.textBox_r_ppp.Name = "textBox_r_ppp";
			this.textBox_r_ppp.TabIndex = 6;
			this.textBox_r_ppp.Text = "";
			// 
			// textBox_r_to
			// 
			this.textBox_r_to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_r_to.Enabled = false;
			this.textBox_r_to.Location = new System.Drawing.Point(112, 24);
			this.textBox_r_to.Name = "textBox_r_to";
			this.textBox_r_to.TabIndex = 5;
			this.textBox_r_to.Text = "";
			// 
			// checkBox_norm
			// 
			this.checkBox_norm.Enabled = false;
			this.checkBox_norm.Location = new System.Drawing.Point(8, 120);
			this.checkBox_norm.Name = "checkBox_norm";
			this.checkBox_norm.TabIndex = 4;
			this.checkBox_norm.Text = "Нормачас";
			// 
			// checkBox_work
			// 
			this.checkBox_work.Enabled = false;
			this.checkBox_work.Location = new System.Drawing.Point(8, 96);
			this.checkBox_work.Name = "checkBox_work";
			this.checkBox_work.TabIndex = 3;
			this.checkBox_work.Text = "Договорные";
			// 
			// checkBox_wash
			// 
			this.checkBox_wash.Enabled = false;
			this.checkBox_wash.Location = new System.Drawing.Point(8, 72);
			this.checkBox_wash.Name = "checkBox_wash";
			this.checkBox_wash.TabIndex = 2;
			this.checkBox_wash.Text = "Мойка";
			// 
			// checkBox_ppp
			// 
			this.checkBox_ppp.Enabled = false;
			this.checkBox_ppp.Location = new System.Drawing.Point(8, 48);
			this.checkBox_ppp.Name = "checkBox_ppp";
			this.checkBox_ppp.TabIndex = 1;
			this.checkBox_ppp.Text = "ППП";
			// 
			// checkBox_to
			// 
			this.checkBox_to.Enabled = false;
			this.checkBox_to.Location = new System.Drawing.Point(8, 24);
			this.checkBox_to.Name = "checkBox_to";
			this.checkBox_to.TabIndex = 0;
			this.checkBox_to.Text = "ТО";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.listView_work,
																				   this.listView1});
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1004, 526);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Работы/Детали";
			// 
			// listView_work
			// 
			this.listView_work.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader10,
																							this.columnHeader11,
																							this.columnHeader12,
																							this.columnHeader13,
																							this.columnHeader14,
																							this.columnHeader15,
																							this.columnHeader16,
																							this.columnHeader17,
																							this.columnHeader18});
			this.listView_work.FullRowSelect = true;
			this.listView_work.Location = new System.Drawing.Point(16, 48);
			this.listView_work.Name = "listView_work";
			this.listView_work.Size = new System.Drawing.Size(976, 184);
			this.listView_work.StateImageList = this.imageList1;
			this.listView_work.TabIndex = 1;
			this.listView_work.View = System.Windows.Forms.View.Details;
			this.listView_work.DoubleClick += new System.EventHandler(this.listView_work_DoubleClick);
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Наименование";
			this.columnHeader10.Width = 250;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Коли-во";
			this.columnHeader11.Width = 77;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Нормачас";
			this.columnHeader12.Width = 98;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Цена";
			this.columnHeader13.Width = 89;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Сумма";
			this.columnHeader14.Width = 80;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Скидка";
			this.columnHeader15.Width = 80;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Гарантия";
			this.columnHeader16.Width = 106;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Виновник";
			this.columnHeader17.Width = 90;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Косяк";
			this.columnHeader18.Width = 90;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8,
																						this.columnHeader9});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 304);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(988, 179);
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			this.toolTip1.SetToolTip(this.listView1, "Тест");
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 256;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Кол-во";
			this.columnHeader3.Width = 71;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена";
			this.columnHeader4.Width = 76;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Сумма";
			this.columnHeader5.Width = 72;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Вход";
			this.columnHeader6.Width = 76;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Гарантия";
			this.columnHeader7.Width = 110;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Виновник";
			this.columnHeader8.Width = 120;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Причина";
			this.columnHeader9.Width = 80;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(8, 160);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(128, 23);
			this.label28.TabIndex = 20;
			this.label28.Text = "Мастер-контролер";
			// 
			// textBox_master
			// 
			this.textBox_master.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_master.Enabled = false;
			this.textBox_master.Location = new System.Drawing.Point(144, 160);
			this.textBox_master.Name = "textBox_master";
			this.textBox_master.Size = new System.Drawing.Size(312, 23);
			this.textBox_master.TabIndex = 21;
			this.textBox_master.Text = "";
			// 
			// FormCardControl
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(1028, 597);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCardControl";
			this.Text = "Контроль карточки заказа";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected void SetOwner()
		{
			if(owner == null) return;
			if(owner.Juridical == true)
			{
				textBox_juridical.Text	= "Юридическое лицо";
				textBox_name.Text		= owner.NameJuridical;
				textBox_contact.Text	= owner.ContactPhone;
			}
			else
			{
				textBox_juridical.Text	= "Физическое лицо";
				textBox_name.Text		= owner.FirstName + " " + owner.Name + " " + owner.SecondName;
				textBox_contact.Text	= owner.Phone;
			}
			if(owner_property == null)
			{
				textBox_cashless.Text	= "Наличный";
			}
			else
			{
				if(owner_property.Cashless == true)
					textBox_cashless.Text	= "Безналичный";
				else
					textBox_cashless.Text	= "Наличный";
			}

			if(represent == null)
			{
				textBox_represent.Text		= "Не выбран";
				return;
			}
			textBox_represent.Text			= represent.Title;
			textBox_represent_document.Text	= (string)card.GetData("ДОКУМЕНТ_ПРЕДСТАВИТЕЛЬ_КАРТОЧКА");
		}

		protected void SetAuto()
		{
			if(auto == null) return;
			textBox_model.Text		= auto.ModelTxt;
			textBox_vin.Text		= auto.Vin;
			textBox_sign.Text		= auto.SignNo;
			textBox_selldate.Text	= auto.SellDateTxt;
			textBox_run.Text		= (string)((int)card.GetData("ПРОБЕГ_КАРТОЧКА")).ToString();

			string txt_to;
			string txt_90;
			if(rep.error == true)
			{
				txt_to = "*";
				txt_90 = "*";
			}
			else
			{
				if (rep.to == 0)
					txt_to	= "ТО НЕТ";
				else
					txt_to	= "ТО №" + rep.to.ToString() + "(" + rep.run.ToString() + " ," + rep.date.ToShortDateString() + ")";

				txt_90 = "Заездов : " + rep.count + " ср. пробег " + rep.evrg_run ;
			}
			textBox_to.Text			= txt_to;
			textBox_90.Text			= txt_90;
		}

		protected void SetCard()
		{
			textBox_number.Text = txtCard.Number;//card.GetDataTxt("НОМЕР_КАРТОЧКА");
			textBox_date.Text = txtCard.Date;// card.GetDataTxt("ДАТА_КАРТОЧКА");
			textBox_state.Text = txtCard.CardState;// card.GetDataTxt("СТАТУС_КАРТОЧКА");
			if((short)card.GetData("СТАТУС_КАРТОЧКА") > 0 && (short)card.GetData("СТАТУС_КАРТОЧКА") != 5)
			{
				textBox_warrant_number.Text = txtCard.WarrantNumber;//card.GetDataTxt("НОМЕР_НАРЯД_КАРТОЧКА");
				textBox_warrant_open.Text = txtCard.OpenDateTime;// card.GetDataTxt("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
			}
			if((short)card.GetData("СТАТУС_КАРТОЧКА") == 2)
			{
				textBox_warrant_close.Text = txtCard.CloseDateTime;// card.GetDataTxt("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");
			}
			if(guaranty_type != null)
				textBox_guaranty_type.Text		= guaranty_type.Description;
			else
				textBox_guaranty_type.Text		= "Неустановленно";
			if(auto_type != null)
				textBox_auto_type.Text		= auto_type.DbTitle();
			else
				textBox_auto_type.Text		= "Неустановленно";
			if(workshop != null)
				textBox_workshop.Text		= workshop.DbTitle();
			else
				textBox_workshop.Text		= "Неустановленно";
			if(master != null)
				textBox_master.Text		= master.Title;
			else
				textBox_master.Text		= "Неустановлен";

			checkBox_cashless.Checked		= (bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА");
			checkBox_inner.Checked		= (bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА");
		}

		public void SetReportWork()
		{
			checkBox_to.Checked		= report1.to_is;
			checkBox_ppp.Checked	= report1.ppp_is;
			checkBox_wash.Checked	= report1.wash_is;
			checkBox_work.Checked	= report1.work_is;
			checkBox_norm.Checked	= report1.norm_is;

			textBox_r_to.Text			= report1.to_summ.ToString();
			textBox_r_ppp.Text			= report1.ppp_summ.ToString();
			textBox_r_wash.Text			= report1.wash_summ.ToString();
			textBox_r_work.Text			= report1.work_summ.ToString();
			textBox_r_norm.Text			= report1.norm_summ.ToString();
			textBox_r_normtime.Text		= report1.norm_time.ToString();
			textBox_r_whole.Text		= report1.work_whole.ToString();
			textBox_r_summ.Text			= (report1.to_summ + report1.ppp_summ + report1.wash_summ + report1.work_summ + report1.norm_summ).ToString();
			textBox_card_summ_work.Text	= (report1.liquid_summ + report1.work_whole).ToString();
		}

		public void SetReportWorkGuaranty()
		{
			checkBox_rg_to.Checked		= report1.g_to_is;
			checkBox_rg_ppp.Checked		= report1.g_ppp_is;
			checkBox_rg_wash.Checked	= report1.g_wash_is;
			checkBox_rg_work.Checked	= report1.g_work_is;
			checkBox_rg_norm.Checked	= report1.g_norm_is;

			textBox_rg_to.Text			= report1.g_to_summ.ToString();
			textBox_rg_ppp.Text			= report1.g_ppp_summ.ToString();
			textBox_rg_wash.Text		= report1.g_wash_summ.ToString();
			textBox_rg_work.Text		= report1.g_work_summ.ToString();
			textBox_rg_norm.Text		= report1.g_norm_summ.ToString();
			textBox_rg_normtime.Text	= report1.g_norm_time.ToString();
			textBox_rg_whole.Text		= report1.g_work_whole.ToString();
			textBox_rg_summ.Text		= (report1.g_to_summ + report1.g_ppp_summ + report1.g_wash_summ + report1.g_work_summ + report1.g_norm_summ).ToString();
		}

		public void SetReportDetail()
		{
			checkBox_r_liquid.Checked		= report1.liquid_is;
			checkBox_r_liquidbad.Checked	= report1.liquid_bad_is;
			checkBox_r_detail.Checked		= report1.detail_is;
			checkBox_r_detailbad.Checked	= report1.detail_bad_is;

			textBox_r_liquid.Text			= report1.liquid_summ.ToString();
			textBox_r_liquid_input.Text		= report1.liquid_input.ToString();
			textBox_r_detail.Text			= report1.detail_summ.ToString();
			textBox_r_detail_input.Text		= report1.detail_input.ToString();
			textBox_card_summ_detail.Text	= (report1.detail_summ).ToString();
		}

		public void SetReportDetailGuaranty()
		{
			checkBox_rg_liquid.Checked		= report1.g_liquid_is;
			checkBox_rg_liquidbad.Checked	= report1.g_liquid_bad_is;
			checkBox_rg_detail.Checked		= report1.g_detail_is;
			checkBox_rg_detailbad.Checked	= report1.g_detail_bad_is;

			textBox_rg_liquid.Text			= report1.g_liquid_summ.ToString();
			textBox_rg_liquid_input.Text	= report1.g_liquid_input.ToString();
			textBox_rg_detail.Text			= report1.g_detail_summ.ToString();
			textBox_rg_detail_input.Text	= report1.g_detail_input.ToString();
			textBox_rg_detail_summ.Text		= (report1.g_liquid_summ + report1.g_detail_summ).ToString();
			textBox_rg_input_summ.Text		= (report1.g_liquid_input + report1.g_detail_input).ToString();
		}

		private void listView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Работа с подсказкой
			// Определяем элемент, над которым сейчас находиться мышка
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null)
			{
				lvitem_detail = null;
				toolTip1.SetToolTip(listView1, "");
				return;
			}
			if(lvitem_detail != item)
			{
				lvitem_detail = item;
				string txt = "";
				if(item.Text.Length != 0) txt += item.Text + "      ";
				txt += item.SubItems[1].Text;
				if(item.SubItems[6].Text.Length != 0) txt += "\n" + item.SubItems[6].Text;
				if(item.SubItems[7].Text.Length != 0) txt += "\n" + item.SubItems[7].Text;
				if(item.SubItems[8].Text.Length != 0) txt += "\n" + item.SubItems[8].Text;
				toolTip1.SetToolTip(listView1, txt);
			}
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Проверяем элемент на котором щелкнули два раза
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			int column = Db.GetColumnPosition(listView1);
			switch(column)
			{
				case 3:
					// Изменение цены
					// ТОЛЬКО ДЛЯ ГАРАНТИИ!
					DtCardDetail detail = DbSqlCardDetail.Find(card, (long)item.Tag);
					if((bool)detail.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") != true) return;
					FormSelectString dialog2 = new FormSelectString("Новая входная цена", "");
					if(dialog2.ShowDialog() != DialogResult.OK) return;
					float price = dialog2.SelectedFloat;
					if(DbSqlCardDetail.UpdatePrice(card, (long)item.Tag, price) == false) return;
					item.SubItems[3].Text	= price.ToString();
					return;
				case 5:
					// Изменение входной цены
					FormSelectString dialog = new FormSelectString("Новая входная цена", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					float input = dialog.SelectedFloat;
					if(DbSqlCardDetail.UpdateInput(card, (long)item.Tag, input) == false) return;
					item.SubItems[5].Text	= input.ToString();
					if(input > 0)
						item.SubItems[5].BackColor = Color.White;
					else
						item.SubItems[5].BackColor = Color.Red;
					return;
				case 6:
					// Изменение вида гарантии
					FormSetMistake dialog1 = new FormSetMistake(card, (long)item.Tag, 0);
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					dialog1.CardDetail.SetLVItem(item);
					return;
				default:
					return;
			}
		}

		private void listView_work_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор вида гарантии
			DtCardWork card_work;
			// Проверяем элемент на котором щелкнули два раза
			ListViewItem item = Db.GetItemPosition(listView_work);
			if(item == null) return;
			int column = Db.GetColumnPosition(listView_work);
			long position = 0;
			switch(column)
			{
				case 6:
					// Изменение вида гарантии
					position = (int)item.Tag;
					FormSetMistake dialog = new FormSetMistake(card, position, 1);
					if(dialog.ShowDialog() != DialogResult.OK) return;
					dialog.CardWork.SetLVItem(item);
					return;
				case 3:
					// Изменение стоимости гарантийного нормачаса
					position = (int)item.Tag;
					card_work = DbSqlCardWork.Find(card, (int)position);
					if((bool)card_work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") != true)
					{
						MessageBox.Show("Работа не гарантийная");
						return;
					}
					FormSelectString dialog1 = new FormSelectString("Новая стоимость нормачаса", "");
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					float cost = dialog1.SelectedFloat;
					if(DbSqlCardWork.UpdateCost(card_work, cost) == false) return;
					card_work.SetData("НОРМАЧАС_КАРТОЧКА_РАБОТА", cost);
					card_work.SetLVItem(item);
					return;
				case 2:
					// Изменение количество часов работы
					position = (int)item.Tag;
					card_work = DbSqlCardWork.Find(card, (int)position);
					if((bool)card_work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") != true)
					{
						MessageBox.Show("Работа не гарантийная");
						return;
					}
					FormSelectString dialog2 = new FormSelectString("Новая трудоемкость", "");
					if(dialog2.ShowDialog() != DialogResult.OK) return;
					float val = dialog2.SelectedFloat;
					if(DbSqlCardWork.UpdateVal(card_work, val) == false) return;
					card_work.SetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА", val);
					card_work.SetLVItem(item);
					return;
				default:
					return;
			}
		}
	}
}
