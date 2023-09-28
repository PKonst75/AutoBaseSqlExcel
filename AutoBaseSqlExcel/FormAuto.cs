using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Список автомобилей - клиентов автосервиса
	/// </summary>
	public class FormAuto : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBoxVin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonAdd;

		private System.Windows.Forms.TextBox textBoxYear;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxEngine;
		private System.Windows.Forms.TextBox textBoxFrame;
		private System.Windows.Forms.TextBox textBoxBody;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxSign;
		private System.Windows.Forms.TextBox textBoxModel;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Button buttonSelectModel;
		private System.Windows.Forms.Button buttonAvtoBody;
		private System.Windows.Forms.CheckBox checkBoxIsSellDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerSellDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonGuarantyTypeSelect;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox textBoxGuarantyType;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxSubModel;
		private System.Windows.Forms.Button buttonSelectSubModel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxAutoComplect;
		private System.Windows.Forms.Button buttonSelectAutoComplect;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxAutoColor;
		private System.Windows.Forms.Button buttonSelectAutoColor;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonNewOption;
		private System.Windows.Forms.Button buttonDelOption;
		private System.Windows.Forms.Button buttonPrice;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonComment;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonInstall;
		private System.Windows.Forms.TextBox textBox_spare_part_number;
		private System.Windows.Forms.CheckBox checkBox_spare_part_number;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBox_factory;
		private System.Windows.Forms.Button button_select_factory;

		DbAuto	auto;
		// Дополнительные данные - завод изготовитель

		public FormAuto(DbAuto source)
		{
			InitializeComponent();

			if(source != null)
			{
				auto = new DbAuto(source);

				// Список доп оборудования
				DbOptionAuto.FillList(listView1, auto);

				// догрузка
				auto.Load();
			}
			else
			{
				auto = new DbAuto();
			}
			textBoxModel.Text			= auto.ModelTxt;
			textBoxYear.Text			= auto.YearTxt;
			textBoxVin.Text				= auto.Vin;
			textBoxEngine.Text			= auto.EngineNo;
			textBoxFrame.Text			= auto.FrameNo;
			textBoxBody.Text			= auto.BodyNo;
			textBoxSign.Text			= auto.SignNo;
			textBoxComment.Text			= auto.Comment;
			textBoxGuarantyType.Text	= auto.GuarantyTypeTxt;
			// Дата продажи
			if(auto.IsSellDate)
			{
				// Дата продажи установлена - менять нельзя
				checkBoxIsSellDate.Checked		= true;
				checkBoxIsSellDate.Enabled		= false;
				dateTimePickerSellDate.Value	= auto.SellDate;
				dateTimePickerSellDate.Visible	= true;
				dateTimePickerSellDate.Enabled	= false;	
			}
			else
			{
				// Дата продажи неустановлена - можно установить
				dateTimePickerSellDate.Value	= DateTime.Today;
				dateTimePickerSellDate.Visible	= false;
				dateTimePickerSellDate.Enabled	= true;
				checkBoxIsSellDate.Checked		= false;
				checkBoxIsSellDate.Enabled		= true;
			}

			// Номер для запчастей
			if(auto.NoSparePartNumber == true)
			{
				checkBox_spare_part_number.Checked = false;
				textBox_spare_part_number.Enabled	= false;
			}
			else
			{
				checkBox_spare_part_number.Checked = true;
				textBox_spare_part_number.Enabled	= true;
			}
			textBox_spare_part_number.Text	= auto.SparePartNumberTxt;

			// Что касается дополнительных параметров
			if (auto.AutoColors != null)
				textBoxAutoColor.Text = auto.AutoColors.DbTitle();
			if (auto.AutoSubModel != null)
				textBoxSubModel.Lines = auto.AutoSubModel.Inform(0);	
			if (auto.AutoComplect != null)
				textBoxAutoComplect.Text = auto.AutoComplect.DbTitle();

			// Завод-производитель
			textBox_factory.Text	= auto.FactoryTxt;


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAuto));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBox_spare_part_number = new System.Windows.Forms.TextBox();
			this.checkBox_spare_part_number = new System.Windows.Forms.CheckBox();
			this.buttonGuarantyTypeSelect = new System.Windows.Forms.Button();
			this.textBoxGuarantyType = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerSellDate = new System.Windows.Forms.DateTimePicker();
			this.checkBoxIsSellDate = new System.Windows.Forms.CheckBox();
			this.buttonAvtoBody = new System.Windows.Forms.Button();
			this.buttonSelectModel = new System.Windows.Forms.Button();
			this.textBoxSign = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxBody = new System.Windows.Forms.TextBox();
			this.textBoxFrame = new System.Windows.Forms.TextBox();
			this.textBoxEngine = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxYear = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxModel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxVin = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.buttonSelectAutoColor = new System.Windows.Forms.Button();
			this.textBoxAutoColor = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.buttonSelectAutoComplect = new System.Windows.Forms.Button();
			this.textBoxAutoComplect = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonSelectSubModel = new System.Windows.Forms.Button();
			this.textBoxSubModel = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.buttonInstall = new System.Windows.Forms.Button();
			this.buttonComment = new System.Windows.Forms.Button();
			this.buttonPrice = new System.Windows.Forms.Button();
			this.buttonDelOption = new System.Windows.Forms.Button();
			this.buttonNewOption = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label12 = new System.Windows.Forms.Label();
			this.textBox_factory = new System.Windows.Forms.TextBox();
			this.button_select_factory = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage3,
																					  this.tabPage2,
																					  this.tabPage4});
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(520, 432);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.button_select_factory,
																				   this.textBox_factory,
																				   this.label12,
																				   this.textBox_spare_part_number,
																				   this.checkBox_spare_part_number,
																				   this.buttonGuarantyTypeSelect,
																				   this.textBoxGuarantyType,
																				   this.label3,
																				   this.dateTimePickerSellDate,
																				   this.checkBoxIsSellDate,
																				   this.buttonAvtoBody,
																				   this.buttonSelectModel,
																				   this.textBoxSign,
																				   this.label10,
																				   this.textBoxBody,
																				   this.textBoxFrame,
																				   this.textBoxEngine,
																				   this.label9,
																				   this.label8,
																				   this.label7,
																				   this.label6,
																				   this.textBoxYear,
																				   this.label2,
																				   this.textBoxModel,
																				   this.label1,
																				   this.textBoxVin});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(512, 403);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Основные";
			// 
			// textBox_spare_part_number
			// 
			this.textBox_spare_part_number.Location = new System.Drawing.Point(184, 184);
			this.textBox_spare_part_number.Name = "textBox_spare_part_number";
			this.textBox_spare_part_number.Size = new System.Drawing.Size(144, 23);
			this.textBox_spare_part_number.TabIndex = 108;
			this.textBox_spare_part_number.Text = "";
			// 
			// checkBox_spare_part_number
			// 
			this.checkBox_spare_part_number.Location = new System.Drawing.Point(8, 184);
			this.checkBox_spare_part_number.Name = "checkBox_spare_part_number";
			this.checkBox_spare_part_number.Size = new System.Drawing.Size(176, 24);
			this.checkBox_spare_part_number.TabIndex = 107;
			this.checkBox_spare_part_number.Text = "Номер для запчастей";
			this.checkBox_spare_part_number.CheckedChanged += new System.EventHandler(this.checkBox_spare_part_number_CheckedChanged);
			// 
			// buttonGuarantyTypeSelect
			// 
			this.buttonGuarantyTypeSelect.Location = new System.Drawing.Point(440, 40);
			this.buttonGuarantyTypeSelect.Name = "buttonGuarantyTypeSelect";
			this.buttonGuarantyTypeSelect.Size = new System.Drawing.Size(24, 24);
			this.buttonGuarantyTypeSelect.TabIndex = 106;
			this.buttonGuarantyTypeSelect.Text = "...";
			this.buttonGuarantyTypeSelect.Click += new System.EventHandler(this.buttonGuarantyTypeSelect_Click);
			// 
			// textBoxGuarantyType
			// 
			this.textBoxGuarantyType.Location = new System.Drawing.Point(112, 40);
			this.textBoxGuarantyType.Name = "textBoxGuarantyType";
			this.textBoxGuarantyType.ReadOnly = true;
			this.textBoxGuarantyType.Size = new System.Drawing.Size(328, 23);
			this.textBoxGuarantyType.TabIndex = 105;
			this.textBoxGuarantyType.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 24);
			this.label3.TabIndex = 104;
			this.label3.Text = "Вид гарантии";
			// 
			// dateTimePickerSellDate
			// 
			this.dateTimePickerSellDate.Location = new System.Drawing.Point(296, 152);
			this.dateTimePickerSellDate.Name = "dateTimePickerSellDate";
			this.dateTimePickerSellDate.TabIndex = 103;
			// 
			// checkBoxIsSellDate
			// 
			this.checkBoxIsSellDate.Location = new System.Drawing.Point(216, 152);
			this.checkBoxIsSellDate.Name = "checkBoxIsSellDate";
			this.checkBoxIsSellDate.TabIndex = 102;
			this.checkBoxIsSellDate.Text = "Продано";
			this.checkBoxIsSellDate.CheckedChanged += new System.EventHandler(this.checkBoxIsSellDate_CheckedChanged);
			// 
			// buttonAvtoBody
			// 
			this.buttonAvtoBody.Location = new System.Drawing.Point(328, 320);
			this.buttonAvtoBody.Name = "buttonAvtoBody";
			this.buttonAvtoBody.Size = new System.Drawing.Size(56, 23);
			this.buttonAvtoBody.TabIndex = 101;
			this.buttonAvtoBody.Text = "авто";
			this.buttonAvtoBody.Click += new System.EventHandler(this.buttonAvtoBody_Click);
			// 
			// buttonSelectModel
			// 
			this.buttonSelectModel.Location = new System.Drawing.Point(440, 8);
			this.buttonSelectModel.Name = "buttonSelectModel";
			this.buttonSelectModel.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectModel.TabIndex = 19;
			this.buttonSelectModel.Text = "...";
			this.buttonSelectModel.Click += new System.EventHandler(this.buttonSelectModel_Click);
			// 
			// textBoxSign
			// 
			this.textBoxSign.Location = new System.Drawing.Point(152, 360);
			this.textBoxSign.Name = "textBoxSign";
			this.textBoxSign.TabIndex = 11;
			this.textBoxSign.Text = "textBox1";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 360);
			this.label10.Name = "label10";
			this.label10.TabIndex = 18;
			this.label10.Text = "Гос. номер";
			// 
			// textBoxBody
			// 
			this.textBoxBody.Location = new System.Drawing.Point(152, 320);
			this.textBoxBody.Name = "textBoxBody";
			this.textBoxBody.Size = new System.Drawing.Size(168, 23);
			this.textBoxBody.TabIndex = 7;
			this.textBoxBody.Text = "textBox1";
			// 
			// textBoxFrame
			// 
			this.textBoxFrame.Location = new System.Drawing.Point(152, 288);
			this.textBoxFrame.Name = "textBoxFrame";
			this.textBoxFrame.Size = new System.Drawing.Size(168, 23);
			this.textBoxFrame.TabIndex = 6;
			this.textBoxFrame.Text = "textBox1";
			// 
			// textBoxEngine
			// 
			this.textBoxEngine.Location = new System.Drawing.Point(152, 256);
			this.textBoxEngine.Name = "textBoxEngine";
			this.textBoxEngine.Size = new System.Drawing.Size(168, 23);
			this.textBoxEngine.TabIndex = 5;
			this.textBoxEngine.Text = "textBox1";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 320);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(128, 23);
			this.label9.TabIndex = 14;
			this.label9.Text = "Кузов (коляска) №";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 288);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 23);
			this.label8.TabIndex = 13;
			this.label8.Text = "Шасси (рама) №";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 264);
			this.label7.Name = "label7";
			this.label7.TabIndex = 12;
			this.label7.Text = "Двигатель №";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 152);
			this.label6.Name = "label6";
			this.label6.TabIndex = 11;
			this.label6.Text = "Год выпуска";
			// 
			// textBoxYear
			// 
			this.textBoxYear.Location = new System.Drawing.Point(112, 152);
			this.textBoxYear.Name = "textBoxYear";
			this.textBoxYear.TabIndex = 3;
			this.textBoxYear.Text = "textBox1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.TabIndex = 100;
			this.label2.Text = "Марка/Модель";
			// 
			// textBoxModel
			// 
			this.textBoxModel.Location = new System.Drawing.Point(112, 8);
			this.textBoxModel.Name = "textBoxModel";
			this.textBoxModel.ReadOnly = true;
			this.textBoxModel.Size = new System.Drawing.Size(328, 23);
			this.textBoxModel.TabIndex = 100;
			this.textBoxModel.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 240);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "VIN";
			// 
			// textBoxVin
			// 
			this.textBoxVin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxVin.Location = new System.Drawing.Point(152, 232);
			this.textBoxVin.Name = "textBoxVin";
			this.textBoxVin.Size = new System.Drawing.Size(168, 23);
			this.textBoxVin.TabIndex = 4;
			this.textBoxVin.Text = "textBox1";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.buttonSelectAutoColor,
																				   this.textBoxAutoColor,
																				   this.label11,
																				   this.buttonSelectAutoComplect,
																				   this.textBoxAutoComplect,
																				   this.label5,
																				   this.buttonSelectSubModel,
																				   this.textBoxSubModel,
																				   this.label4});
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(512, 323);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Модель";
			// 
			// buttonSelectAutoColor
			// 
			this.buttonSelectAutoColor.Location = new System.Drawing.Point(464, 248);
			this.buttonSelectAutoColor.Name = "buttonSelectAutoColor";
			this.buttonSelectAutoColor.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectAutoColor.TabIndex = 8;
			this.buttonSelectAutoColor.Text = "...";
			this.buttonSelectAutoColor.Click += new System.EventHandler(this.buttonSelectAutoColor_Click);
			// 
			// textBoxAutoColor
			// 
			this.textBoxAutoColor.Location = new System.Drawing.Point(16, 248);
			this.textBoxAutoColor.Name = "textBoxAutoColor";
			this.textBoxAutoColor.ReadOnly = true;
			this.textBoxAutoColor.Size = new System.Drawing.Size(448, 23);
			this.textBoxAutoColor.TabIndex = 7;
			this.textBoxAutoColor.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(16, 224);
			this.label11.Name = "label11";
			this.label11.TabIndex = 6;
			this.label11.Text = "Цвет";
			// 
			// buttonSelectAutoComplect
			// 
			this.buttonSelectAutoComplect.Location = new System.Drawing.Point(464, 192);
			this.buttonSelectAutoComplect.Name = "buttonSelectAutoComplect";
			this.buttonSelectAutoComplect.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectAutoComplect.TabIndex = 5;
			this.buttonSelectAutoComplect.Text = "...";
			this.buttonSelectAutoComplect.Click += new System.EventHandler(this.buttonSelectAutoComplect_Click);
			// 
			// textBoxAutoComplect
			// 
			this.textBoxAutoComplect.Location = new System.Drawing.Point(16, 192);
			this.textBoxAutoComplect.Name = "textBoxAutoComplect";
			this.textBoxAutoComplect.ReadOnly = true;
			this.textBoxAutoComplect.Size = new System.Drawing.Size(448, 23);
			this.textBoxAutoComplect.TabIndex = 4;
			this.textBoxAutoComplect.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 168);
			this.label5.Name = "label5";
			this.label5.TabIndex = 3;
			this.label5.Text = "Комплектация";
			// 
			// buttonSelectSubModel
			// 
			this.buttonSelectSubModel.Location = new System.Drawing.Point(464, 32);
			this.buttonSelectSubModel.Name = "buttonSelectSubModel";
			this.buttonSelectSubModel.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectSubModel.TabIndex = 2;
			this.buttonSelectSubModel.Text = "...";
			this.buttonSelectSubModel.Click += new System.EventHandler(this.buttonSelectSubModel_Click);
			// 
			// textBoxSubModel
			// 
			this.textBoxSubModel.Location = new System.Drawing.Point(16, 32);
			this.textBoxSubModel.Multiline = true;
			this.textBoxSubModel.Name = "textBoxSubModel";
			this.textBoxSubModel.ReadOnly = true;
			this.textBoxSubModel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSubModel.Size = new System.Drawing.Size(448, 128);
			this.textBoxSubModel.TabIndex = 1;
			this.textBoxSubModel.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 8);
			this.label4.Name = "label4";
			this.label4.TabIndex = 0;
			this.label4.Text = "Подмодель";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.textBoxComment,
																				   this.label15});
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(512, 323);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Дополнительные";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(8, 40);
			this.textBoxComment.Multiline = true;
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxComment.Size = new System.Drawing.Size(448, 88);
			this.textBoxComment.TabIndex = 5;
			this.textBoxComment.Text = "textBox1";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 16);
			this.label15.Name = "label15";
			this.label15.TabIndex = 2;
			this.label15.Text = "Примечания";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.buttonInstall,
																				   this.buttonComment,
																				   this.buttonPrice,
																				   this.buttonDelOption,
																				   this.buttonNewOption,
																				   this.listView1});
			this.tabPage4.Location = new System.Drawing.Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(512, 323);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Доп. оборудование";
			// 
			// buttonInstall
			// 
			this.buttonInstall.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonInstall.Image")));
			this.buttonInstall.Location = new System.Drawing.Point(104, 8);
			this.buttonInstall.Name = "buttonInstall";
			this.buttonInstall.Size = new System.Drawing.Size(24, 23);
			this.buttonInstall.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonInstall, "Отметить установку оборудования");
			this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
			// 
			// buttonComment
			// 
			this.buttonComment.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonComment.Image")));
			this.buttonComment.Location = new System.Drawing.Point(80, 8);
			this.buttonComment.Name = "buttonComment";
			this.buttonComment.Size = new System.Drawing.Size(24, 23);
			this.buttonComment.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonComment, "Примечание к установленному оборудованию");
			this.buttonComment.Click += new System.EventHandler(this.buttonComment_Click);
			// 
			// buttonPrice
			// 
			this.buttonPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPrice.Image")));
			this.buttonPrice.Location = new System.Drawing.Point(56, 8);
			this.buttonPrice.Name = "buttonPrice";
			this.buttonPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonPrice.TabIndex = 3;
			this.toolTip1.SetToolTip(this.buttonPrice, "Новая цена доп. Оборудования");
			this.buttonPrice.Click += new System.EventHandler(this.buttonPrice_Click);
			// 
			// buttonDelOption
			// 
			this.buttonDelOption.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelOption.Image")));
			this.buttonDelOption.Location = new System.Drawing.Point(32, 8);
			this.buttonDelOption.Name = "buttonDelOption";
			this.buttonDelOption.Size = new System.Drawing.Size(24, 23);
			this.buttonDelOption.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonDelOption, "Отменить/Снять доп. оборудование");
			this.buttonDelOption.Click += new System.EventHandler(this.buttonDelOption_Click);
			// 
			// buttonNewOption
			// 
			this.buttonNewOption.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewOption.Image")));
			this.buttonNewOption.Location = new System.Drawing.Point(8, 8);
			this.buttonNewOption.Name = "buttonNewOption";
			this.buttonNewOption.Size = new System.Drawing.Size(24, 23);
			this.buttonNewOption.TabIndex = 1;
			this.toolTip1.SetToolTip(this.buttonNewOption, "Новое доп оборудование");
			this.buttonNewOption.Click += new System.EventHandler(this.buttonNewOption_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Оборудование";
			this.columnHeader1.Width = 280;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Цена";
			this.columnHeader2.Width = 85;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Примечание";
			this.columnHeader3.Width = 100;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(208, 448);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(96, 23);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "Добавить";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 72);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(160, 23);
			this.label12.TabIndex = 109;
			this.label12.Text = "Завод-изготовитель";
			// 
			// textBox_factory
			// 
			this.textBox_factory.Location = new System.Drawing.Point(152, 72);
			this.textBox_factory.Name = "textBox_factory";
			this.textBox_factory.ReadOnly = true;
			this.textBox_factory.Size = new System.Drawing.Size(288, 23);
			this.textBox_factory.TabIndex = 110;
			this.textBox_factory.Text = "";
			// 
			// button_select_factory
			// 
			this.button_select_factory.Location = new System.Drawing.Point(440, 72);
			this.button_select_factory.Name = "button_select_factory";
			this.button_select_factory.Size = new System.Drawing.Size(24, 24);
			this.button_select_factory.TabIndex = 111;
			this.button_select_factory.Text = "...";
			this.button_select_factory.Click += new System.EventHandler(this.button_select_factory_Click);
			// 
			// FormAuto
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(562, 487);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonAdd,
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAuto";
			this.Text = "Автомобиль";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			// Добавляем новый автомобиль
			auto.YearTxt	= textBoxYear.Text;
			auto.Vin		= textBoxVin.Text;
			auto.EngineNo	= textBoxEngine.Text;
			auto.FrameNo	= textBoxFrame.Text;
			auto.BodyNo		= textBoxBody.Text;
			auto.SignNo		= textBoxSign.Text;
			auto.Comment	= textBoxComment.Text;
			auto.IsSellDate	= checkBoxIsSellDate.Checked;
			auto.SellDate	= dateTimePickerSellDate.Value;

			auto.NoSparePartNumber = !checkBox_spare_part_number.Checked;
			if(auto.NoSparePartNumber == true)
				auto.SparePartNumber = 0;
			else
				auto.SparePartNumberTxt = textBox_spare_part_number.Text;

			if(Db.ShowFaults()) return;
			if(auto.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		private void buttonSelectModel_Click(object sender, System.EventArgs e)
		{
			// Выбор модели автомобиля
			FormModelList dialog = new FormModelList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.AutoModel = dialog.AutoModel;
			textBoxModel.Text = auto.ModelTxt;

			// Ставим вид гарантии по умолчанию
			auto.GuarantyType = dialog.AutoModel.GuarantyType;
			textBoxGuarantyType.Text = auto.GuarantyTypeTxt;
		}

		private void buttonAvtoBody_Click(object sender, System.EventArgs e)
		{
			//Автоматическая установка номера кузова по VIN
			String body_tmp = textBoxVin.Text;
			body_tmp = body_tmp.Trim();
			body_tmp = body_tmp.Substring(body_tmp.Length - 7);
			textBoxBody.Text = body_tmp;
		}

		private void checkBoxIsSellDate_CheckedChanged(object sender, System.EventArgs e)
		{
			// Включаем/Выключаем режим установления даты продажи
			if (checkBoxIsSellDate.Checked)
			{
				// Включаем режим
				dateTimePickerSellDate.Value	= DateTime.Today;
				dateTimePickerSellDate.Visible	= true;
			}
			else
			{
				// Выключаем режим
				dateTimePickerSellDate.Visible	= false;
			}
		}

		private void buttonGuarantyTypeSelect_Click(object sender, System.EventArgs e)
		{
			// Выбираем вид гарантии по данному автомобилю
			FormGuarantyTypeList dialog = new FormGuarantyTypeList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.GuarantyType = dialog.GuarantyType;
			textBoxGuarantyType.Text = auto.GuarantyTypeTxt;
		}

		private void buttonSelectSubModel_Click(object sender, System.EventArgs e)
		{
			// Выбор подмодели
			if((auto.AutoModel == null)||(auto.CodeModel == 0)) return; // Не выбрана модель
			FormSubmodelList dialog = new FormSubmodelList(Db.ClickType.Select, auto.AutoModel);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.AutoSubModel = dialog.AutoSubModel;

			//textBoxSubModel.Text = auto.AutoSubModel.DbTitle();
			textBoxSubModel.Lines = auto.AutoSubModel.Inform(0);
		}

		private void buttonSelectAutoComplect_Click(object sender, System.EventArgs e)
		{
			// Выбор комплектации
			if((auto.AutoModel == null)||(auto.CodeModel == 0)) return; // Не выбрана модель
			FormAutoComplectList dialog = new FormAutoComplectList(Db.ClickType.Select, auto.AutoModel);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.AutoComplect = dialog.AutoComplect;

			textBoxAutoComplect.Text = auto.AutoComplect.DbTitle();
		}

		private void buttonSelectAutoColor_Click(object sender, System.EventArgs e)
		{
			// Выбор цвета
			if((auto.AutoModel == null)||(auto.CodeModel == 0)) return; // Не выбрана модель
			FormAutoColorsList dialog = new FormAutoColorsList(Db.ClickType.Select, auto.AutoModel);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.AutoColors = dialog.AutoColor;

			textBoxAutoColor.Text = auto.AutoColors.DbTitle();
		}

		public DbAuto Auto
		{
			get
			{
				return auto;
			}
		}

		public void TransferToForm(ListViewItem item)
		{
			if(auto == null) return;
			if(auto.Code == 0) return;
			if(item == null) return;
			DbOption option		= (DbOption)item.Tag;
			if(option== null) return;

			DbOptionAuto optionAuto	= new DbOptionAuto(auto, option);
			if(optionAuto.Write() != true) return;
			ListViewItem itm = listView1.Items.Add(optionAuto.LVItem);
		}

		private void buttonNewOption_Click(object sender, System.EventArgs e)
		{
			FormManageOptions dialog = new FormManageOptions(new FormManageOptions.DelegateTransferToForm(TransferToForm));
			dialog.Show();
		}

		private void buttonDelOption_Click(object sender, System.EventArgs e)
		{
			if(auto == null) return;
			// Удалить выбранные позиции доп. оборудования
			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				DbOptionAuto element = (DbOptionAuto)itm.Tag;
				if(element != null)
				{
					if(element.WriteRemove() == true)
					{
						if(element.CardNumber == 0) itm.Remove();
						else element.SetLVItem(itm);
					}
				}
			}
		}

		private void buttonInstall_Click(object sender, System.EventArgs e)
		{
			if(auto == null) return;
			// Установка выбранного оборудования по карточке
			FormCardList dialog = new FormCardList(Db.ClickType.Select, 2, auto);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			DbCard card = dialog.SelectedCard;

			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				DbOptionAuto element = (DbOptionAuto)itm.Tag;
				if(element != null)
				{
					if(element != null)
					{
						if(element.WriteCard(card) == true) element.SetLVItem(itm);
					}
				}
			}
		}

		private void buttonPrice_Click(object sender, System.EventArgs e)
		{
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbOptionAuto element = (DbOptionAuto)item.Tag;
			if(element == null) return;
			// Смена цены по прайсу
			FormSelectString dialog = new FormSelectString("Введите новую цену", element.Price.ToString());
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			float price = dialog.SelectedFloat;
			DbOptionAuto elementNew = new DbOptionAuto(element);
			elementNew.Price = price;
			if(elementNew.Write() != true) return;
			elementNew.SetLVItem(item);
		}

		private void buttonComment_Click(object sender, System.EventArgs e)
		{
			// Ставим примечание
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbOptionAuto element = (DbOptionAuto)item.Tag;
			if(element == null) return;
			// Смена цены по прайсу
			FormSelectString dialog = new FormSelectString("Установите комментарий", element.Comment);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			string comment = dialog.SelectedText;
			DbOptionAuto elementNew = new DbOptionAuto(element);
			elementNew.Comment = comment;
			if(elementNew.Write() != true) return;
			elementNew.SetLVItem(item);
		}

		private void checkBox_spare_part_number_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBox_spare_part_number.Checked == false)
			{
				textBox_spare_part_number.Enabled = false;
				textBox_spare_part_number.Text = "0";
			}
			else
			{
				textBox_spare_part_number.Enabled = true;
			}
		}

		private void button_select_factory_Click(object sender, System.EventArgs e)
		{
			// Выбор завода изготовителя
			FormFirmList dialog = new FormFirmList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			auto.CodeFactory = dialog.SelectedCode;
			textBox_factory.Text	= auto.FactoryTxt;
		}
	}
}
