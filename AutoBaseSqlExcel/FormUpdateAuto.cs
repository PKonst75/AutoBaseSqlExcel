using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateAuto.
	/// </summary>
	public class FormUpdateAuto : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_vin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_factory;
		private System.Windows.Forms.TextBox textBox_body;
		private System.Windows.Forms.TextBox textBox_year;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox_novin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_engine;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_frame;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_variant;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_color;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBox_sign;
		private System.Windows.Forms.Label label10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox_partsno;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkBox_partsno;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Button button_factory_cancel;
		private System.Windows.Forms.Button button_manage_model;
		private System.Windows.Forms.TextBox textBox_vin_origin;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;

		DtAuto		auto	= null;

        public FormUpdateAuto(SD_BarCodeCN_1 cn)
        {
            // Автоматическое создание автомобиля из кода первого типа Шевроле-Нива
            InitializeComponent();

            auto = new DtAuto();
            auto.SetData("VIN", cn.vin);
            // Установка модели автомобиля
            DtModel model = DbSqlModel.Find(1); // Код Модели Шевроле-Нива 1
            if (model != null)
            {
                // Нашли ровно одну модел - устанавливаем ее
                long model_code = (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
                string model_name = (string)model.GetData("МОДЕЛЬ");
                auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", model_code);
                auto.SetData("МОДЕЛЬ", model_name);

            }
            textBox_model.Text = (string)auto.GetData("МОДЕЛЬ");
            // Дозаполняем данные по автомобилю
            auto.SetData("НОМЕР_КУЗОВ", (string)cn.vin);
            auto.SetData("НОМЕР_ДВИГАТЕЛЬ", (string)cn.engine_number);
           
           
            auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ", (long)0);
            auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ", (bool)true);
           
            auto.SetData("КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ", (long)2);
            auto.SetData("ПРОИЗВОДИТЕЛЬ", (string)"GM-AVTOVAZ");

            // Поиск модели и иcполнения
            long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
            if (code_model != 0)
            {
                // Поиск цвета
                string opt3 = cn.color_code;
                string opt3_pattern = "%" + opt3 + "%";
                ArrayList arr3 = new ArrayList();
                DbSqlColor.SelectInArrayAll(arr3, code_model, opt3_pattern);
                if (arr3.Count == 1)
                {
                    DtColor var3 = (DtColor)arr3[0];
                    if ((long)var3.GetData("КОД_АВТОМОБИЛЬ_ЦВЕТ") != 0)
                    {
                        auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ", (long)var3.GetData("КОД_АВТОМОБИЛЬ_ЦВЕТ"));
                        auto.SetData("АВТОМОБИЛЬ_ЦВЕТ", (string)var3.GetData("ЦВЕТ_НАИМЕНОВАНИЕ") + "(" + (string)var3.GetData("ЦВЕТ_КОД") + ")");
                    }
                }
            }


            // Заполнение данных
            textBox_vin.Text = (string)auto.GetData("VIN");
            checkBox_novin.Checked = (bool)auto.GetData("VIN_ОТСУТСТВУЕТ");
            textBox_vin_origin.Text = (string)auto.GetData("VIN_ПРОИЗВОДИТЕЛЬ");


            textBox_body.Text = (string)auto.GetData("НОМЕР_КУЗОВ");
            textBox_engine.Text = (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
            textBox_frame.Text = (string)auto.GetData("НОМЕР_ШАССИ");
            textBox_model.Text = (string)auto.GetData("МОДЕЛЬ");
            textBox_partsno.Text = auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ").ToString();
            checkBox_partsno.Checked = (bool)auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ");

            textBox_factory.Text = (string)auto.GetData("ПРОИЗВОДИТЕЛЬ");
            textBox_year.Text = auto.GetData("ГОД_ВЫПУСК").ToString();
            textBox_variant.Text = (string)auto.GetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
            textBox_color.Text = (string)auto.GetData("АВТОМОБИЛЬ_ЦВЕТ");

            textBox_sign.Text = (string)auto.GetData("НОМЕР_ЗНАК");

            textBox_comment.Text = (string)auto.GetData("ПРИМЕЧАНИЕ");

        }

        public FormUpdateAuto(SD_BarCodeLada_1 lada)
        {
            // Автоматическое создание автомобиля из кода первого типа LADA
            InitializeComponent();

            auto = new DtAuto();
            auto.SetData("VIN", lada.vin);
            // Установка модели автомобиля
            string model_pattern = lada.model;
            model_pattern = "%" + model_pattern + "%";
            ArrayList arr = new ArrayList();
            DbSqlModel.SelectInArray(arr, model_pattern);
            DtModel model = null;
            if (arr.Count == 1)
            {
                // Нашли ровно одну модел - устанавливаем ее
                DtModel m = (DtModel)arr[0];
                long code = (long)m.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
                model = DbSqlModel.Find(code);
                if (model != null)
                {
                    long model_code = (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
                    string model_name = (string)model.GetData("МОДЕЛЬ");
                    auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", model_code);
                    auto.SetData("МОДЕЛЬ", model_name);
                }
            }
            if ((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ") == 0)
            {
            
                FormSelectModel dialog = new FormSelectModel();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", dialog.SelectedCode);
                    auto.SetData("МОДЕЛЬ", dialog.SelectedText);
                }
            }
            textBox_model.Text = (string)auto.GetData("МОДЕЛЬ");
            // Дозаполняем данные по автомобилю
            auto.SetData("НОМЕР_КУЗОВ", (string)lada.vin);
            auto.SetData("НОМЕР_ДВИГАТЕЛЬ", (string)lada.engine_number);
            long pn = 0;
            try
            {
                pn = Convert.ToInt64(lada.partnumber);
            }
            catch (Exception e)
            {
            }
            if (pn != 0)
            {
                auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ", (long)pn);
                auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ", (bool)false);
            }
            else
            {
                auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ", (long)0);
                auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ", (bool)true);
            }
            auto.SetData("КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ", (long)1);
            auto.SetData("ПРОИЗВОДИТЕЛЬ", (string)"АВТОВАЗ");

            // Поиск модели и иcполнения
            long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
            if (code_model != 0)
            {
                string opt = lada.variant + "-" + lada.complect;
                string opt_pattern = "%" + opt + "%";
                ArrayList arr2 = new ArrayList();
                DbSqlVariant.SelectInArrayAll(arr2, code_model, opt_pattern);
                if (arr2.Count == 1)
                {
                    DtVariant var = (DtVariant)arr2[0];
                    if ((long)var.GetData("КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ") != 0)
                    {
                        auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", (long)var.GetData("КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ"));
                        auto.SetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", (string)var.GetData("ИСПОЛНЕНИЕ_НАИМЕНОВАНИЕ"));
                    }
                }

                // Поиск цвета
                string opt3 = lada.color_code;
                string opt3_pattern = "%" + opt3 + "%";
                ArrayList arr3 = new ArrayList();
                DbSqlColor.SelectInArrayAll(arr3, code_model, opt3_pattern);
                if (arr3.Count == 1)
                {
                    DtColor var3 = (DtColor)arr3[0];
                    if ((long)var3.GetData("КОД_АВТОМОБИЛЬ_ЦВЕТ") != 0)
                    {
                        auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ", (long)var3.GetData("КОД_АВТОМОБИЛЬ_ЦВЕТ"));
                        auto.SetData("АВТОМОБИЛЬ_ЦВЕТ", (string)var3.GetData("ЦВЕТ_НАИМЕНОВАНИЕ") + "(" + (string)var3.GetData("ЦВЕТ_КОД") + ")");
                    }
                }
            }


            // Заполнение данных
            textBox_vin.Text = (string)auto.GetData("VIN");
            checkBox_novin.Checked = (bool)auto.GetData("VIN_ОТСУТСТВУЕТ");
            textBox_vin_origin.Text = (string)auto.GetData("VIN_ПРОИЗВОДИТЕЛЬ");

            
            textBox_body.Text = (string)auto.GetData("НОМЕР_КУЗОВ");
            textBox_engine.Text = (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
            textBox_frame.Text = (string)auto.GetData("НОМЕР_ШАССИ");
            textBox_model.Text = (string)auto.GetData("МОДЕЛЬ");
            textBox_partsno.Text = auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ").ToString();
            checkBox_partsno.Checked = (bool)auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ");

            textBox_factory.Text = (string)auto.GetData("ПРОИЗВОДИТЕЛЬ");
            textBox_year.Text = auto.GetData("ГОД_ВЫПУСК").ToString();
            textBox_variant.Text = (string)auto.GetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
            textBox_color.Text = (string)auto.GetData("АВТОМОБИЛЬ_ЦВЕТ");

            textBox_sign.Text = (string)auto.GetData("НОМЕР_ЗНАК");

            textBox_comment.Text = (string)auto.GetData("ПРИМЕЧАНИЕ");

        }
		public FormUpdateAuto(long code_auto, string vin)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code_auto == 0)
			{
				auto = new DtAuto();
				auto.SetData("VIN", vin);
			}
			else
			{
				auto = DbSqlAuto.Find(code_auto);
				if(auto == null)
					auto = new DtAuto();
			}

			// Заполнение данных
			textBox_vin.Text			= (string)auto.GetData("VIN");
			checkBox_novin.Checked		= (bool)auto.GetData("VIN_ОТСУТСТВУЕТ");
			textBox_vin_origin.Text		= (string)auto.GetData("VIN_ПРОИЗВОДИТЕЛЬ");
			textBox_body.Text			= (string)auto.GetData("НОМЕР_КУЗОВ");
			textBox_engine.Text			= (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
			textBox_frame.Text			= (string)auto.GetData("НОМЕР_ШАССИ");
			textBox_model.Text			= (string)auto.GetData("МОДЕЛЬ");
			textBox_partsno.Text		= auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ").ToString();
			checkBox_partsno.Checked	= (bool)auto.GetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ");

			textBox_factory.Text		= (string)auto.GetData("ПРОИЗВОДИТЕЛЬ");
			textBox_year.Text			= auto.GetData("ГОД_ВЫПУСК").ToString();
			textBox_variant.Text		= (string)auto.GetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			textBox_color.Text			= (string)auto.GetData("АВТОМОБИЛЬ_ЦВЕТ");
			
			textBox_sign.Text			= (string)auto.GetData("НОМЕР_ЗНАК");

			textBox_comment.Text		= (string)auto.GetData("ПРИМЕЧАНИЕ");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateAuto));
            this.textBox_vin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_factory = new System.Windows.Forms.TextBox();
            this.textBox_body = new System.Windows.Forms.TextBox();
            this.textBox_year = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_vin_origin = new System.Windows.Forms.TextBox();
            this.button_manage_model = new System.Windows.Forms.Button();
            this.checkBox_partsno = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_partsno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_model = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_frame = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_engine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_novin = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_factory_cancel = new System.Windows.Forms.Button();
            this.textBox_color = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_variant = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_sign = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_vin
            // 
            this.textBox_vin.Location = new System.Drawing.Point(75, 28);
            this.textBox_vin.Name = "textBox_vin";
            this.textBox_vin.Size = new System.Drawing.Size(256, 26);
            this.textBox_vin.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "VIN";
            // 
            // textBox_factory
            // 
            this.textBox_factory.Location = new System.Drawing.Point(160, 28);
            this.textBox_factory.Name = "textBox_factory";
            this.textBox_factory.Size = new System.Drawing.Size(331, 26);
            this.textBox_factory.TabIndex = 5;
            this.textBox_factory.DoubleClick += new System.EventHandler(this.textBox_factory_DoubleClick);
            this.textBox_factory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_factory_KeyDown);
            // 
            // textBox_body
            // 
            this.textBox_body.Location = new System.Drawing.Point(331, 133);
            this.textBox_body.Name = "textBox_body";
            this.textBox_body.Size = new System.Drawing.Size(266, 26);
            this.textBox_body.TabIndex = 2;
            // 
            // textBox_year
            // 
            this.textBox_year.Location = new System.Drawing.Point(160, 57);
            this.textBox_year.Name = "textBox_year";
            this.textBox_year.Size = new System.Drawing.Size(331, 26);
            this.textBox_year.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_vin_origin);
            this.groupBox1.Controls.Add(this.button_manage_model);
            this.groupBox1.Controls.Add(this.checkBox_partsno);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_partsno);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_model);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_frame);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_engine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox_novin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_vin);
            this.groupBox1.Controls.Add(this.textBox_body);
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 294);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Идинтификационные данные";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(341, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(299, 28);
            this.label13.TabIndex = 19;
            this.label13.Text = "(VIN Производителя, если есть)";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(21, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 28);
            this.label12.TabIndex = 18;
            this.label12.Text = "VIN";
            // 
            // textBox_vin_origin
            // 
            this.textBox_vin_origin.Location = new System.Drawing.Point(75, 66);
            this.textBox_vin_origin.Name = "textBox_vin_origin";
            this.textBox_vin_origin.Size = new System.Drawing.Size(256, 26);
            this.textBox_vin_origin.TabIndex = 17;
            // 
            // button_manage_model
            // 
            this.button_manage_model.Image = ((System.Drawing.Image)(resources.GetObject("button_manage_model.Image")));
            this.button_manage_model.Location = new System.Drawing.Point(629, 104);
            this.button_manage_model.Name = "button_manage_model";
            this.button_manage_model.Size = new System.Drawing.Size(32, 28);
            this.button_manage_model.TabIndex = 16;
            this.button_manage_model.TabStop = false;
            this.button_manage_model.Click += new System.EventHandler(this.button_manage_model_Click);
            // 
            // checkBox_partsno
            // 
            this.checkBox_partsno.Location = new System.Drawing.Point(405, 238);
            this.checkBox_partsno.Name = "checkBox_partsno";
            this.checkBox_partsno.Size = new System.Drawing.Size(235, 28);
            this.checkBox_partsno.TabIndex = 15;
            this.checkBox_partsno.Text = "Отсутсвует";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 27);
            this.label11.TabIndex = 14;
            this.label11.Text = "№ запчастей";
            // 
            // textBox_partsno
            // 
            this.textBox_partsno.Location = new System.Drawing.Point(160, 238);
            this.textBox_partsno.Name = "textBox_partsno";
            this.textBox_partsno.Size = new System.Drawing.Size(224, 26);
            this.textBox_partsno.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(245, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 28);
            this.label5.TabIndex = 12;
            this.label5.Text = "Модель";
            // 
            // textBox_model
            // 
            this.textBox_model.Location = new System.Drawing.Point(331, 104);
            this.textBox_model.Name = "textBox_model";
            this.textBox_model.Size = new System.Drawing.Size(266, 26);
            this.textBox_model.TabIndex = 1;
            this.textBox_model.TextChanged += new System.EventHandler(this.textBox_model_TextChanged);
            this.textBox_model.DoubleClick += new System.EventHandler(this.textBox_model_DoubleClick);
            this.textBox_model.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_model_KeyDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(181, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 27);
            this.label4.TabIndex = 10;
            this.label4.Text = "Номер шасси";
            // 
            // textBox_frame
            // 
            this.textBox_frame.Location = new System.Drawing.Point(331, 190);
            this.textBox_frame.Name = "textBox_frame";
            this.textBox_frame.Size = new System.Drawing.Size(266, 26);
            this.textBox_frame.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(160, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 27);
            this.label3.TabIndex = 8;
            this.label3.Text = "Номер двигателя";
            // 
            // textBox_engine
            // 
            this.textBox_engine.Location = new System.Drawing.Point(331, 162);
            this.textBox_engine.Name = "textBox_engine";
            this.textBox_engine.Size = new System.Drawing.Size(266, 26);
            this.textBox_engine.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(181, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 27);
            this.label2.TabIndex = 6;
            this.label2.Text = "Номер кузова";
            // 
            // checkBox_novin
            // 
            this.checkBox_novin.Location = new System.Drawing.Point(341, 28);
            this.checkBox_novin.Name = "checkBox_novin";
            this.checkBox_novin.Size = new System.Drawing.Size(182, 29);
            this.checkBox_novin.TabIndex = 5;
            this.checkBox_novin.TabStop = false;
            this.checkBox_novin.Text = "VIN Отсутсвует";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_factory_cancel);
            this.groupBox2.Controls.Add(this.textBox_color);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox_variant);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox_factory);
            this.groupBox2.Controls.Add(this.textBox_year);
            this.groupBox2.Location = new System.Drawing.Point(11, 304);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 162);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Описание автомобиля";
            // 
            // button_factory_cancel
            // 
            this.button_factory_cancel.Image = ((System.Drawing.Image)(resources.GetObject("button_factory_cancel.Image")));
            this.button_factory_cancel.Location = new System.Drawing.Point(491, 28);
            this.button_factory_cancel.Name = "button_factory_cancel";
            this.button_factory_cancel.Size = new System.Drawing.Size(32, 28);
            this.button_factory_cancel.TabIndex = 10;
            this.button_factory_cancel.TabStop = false;
            this.button_factory_cancel.Click += new System.EventHandler(this.button_factory_cancel_Click);
            // 
            // textBox_color
            // 
            this.textBox_color.Location = new System.Drawing.Point(160, 114);
            this.textBox_color.Name = "textBox_color";
            this.textBox_color.Size = new System.Drawing.Size(331, 26);
            this.textBox_color.TabIndex = 8;
            this.textBox_color.DoubleClick += new System.EventHandler(this.textBox_color_DoubleClick);
            this.textBox_color.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_color_KeyDown);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 27);
            this.label9.TabIndex = 9;
            this.label9.Text = "Цвет";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 27);
            this.label8.TabIndex = 8;
            this.label8.Text = "Исполнение";
            // 
            // textBox_variant
            // 
            this.textBox_variant.Location = new System.Drawing.Point(160, 86);
            this.textBox_variant.Name = "textBox_variant";
            this.textBox_variant.Size = new System.Drawing.Size(331, 26);
            this.textBox_variant.TabIndex = 7;
            this.textBox_variant.DoubleClick += new System.EventHandler(this.textBox_variant_DoubleClick);
            this.textBox_variant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_variant_KeyDown);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 27);
            this.label7.TabIndex = 6;
            this.label7.Text = "Год выпуска";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 28);
            this.label6.TabIndex = 5;
            this.label6.Text = "Производитель";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox_sign);
            this.groupBox3.Location = new System.Drawing.Point(11, 466);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(672, 76);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Регистрационные данные";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 28);
            this.label10.TabIndex = 1;
            this.label10.Text = "Рег. Знак";
            // 
            // textBox_sign
            // 
            this.textBox_sign.Location = new System.Drawing.Point(160, 28);
            this.textBox_sign.Name = "textBox_sign";
            this.textBox_sign.Size = new System.Drawing.Size(331, 26);
            this.textBox_sign.TabIndex = 9;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Location = new System.Drawing.Point(592, 720);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(117, 28);
            this.button_save.TabIndex = 8;
            this.button_save.Text = "Сохранить";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_comment);
            this.groupBox4.Location = new System.Drawing.Point(11, 551);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(672, 105);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Дополнительно";
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(11, 19);
            this.textBox_comment.Multiline = true;
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(650, 76);
            this.textBox_comment.TabIndex = 0;
            // 
            // FormUpdateAuto
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(730, 755);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormUpdateAuto";
            this.Text = "Автомобиль";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		protected override void OnCreateControl()
		{
			/*
			// Сначала необходимо запросить VIN автомобиля
			FormSelectString dialog = new FormSelectString("Введите VIN номер автомобиля", "");
			dialog.ShowDialog();
			DvVinCode vin = new DvVinCode(dialog.SelectedText);
			if(vin.CheckStepOne() == false)
			{
				MessageBox.Show("Введенный VIN код содержит запрещенные символы");
				return;
			}
			if(vin.Analize() == true)
			{
				if(vin.Iso == true)
				{
					// Европейский анализируемый вариант WIN
					// Загрузка по коду модели
					DtFactory factory = DbSqlFactory.FindPrefix(vin.IsoWmi);
					if(factory != null) textBox_factory.Text = factory.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ").ToString();
					textBox_year.Text	= vin.Year.ToString();
					textBox_body.Text	= vin.Body;
					// Поиск модели автомобиля
					string s = vin.Model;
					s = "%" + s + "%";		// Образец для поиска
					ListView list = new ListView();
					DbSqlModel.SelectInList(list, s);
					FormSelectionList dialog1 = new FormSelectionList(list);
					if(dialog1.ShowDialog() == DialogResult.OK)
					{
						auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", dialog1.SelectedCode);
						textBox_model.Text = dialog1.SelectedText;
					}

				}
			}
			textBox_vin.Text = vin.Vin;
			*/
		}

		private void textBox_color_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// При нажатии ENTER вызов диалога выбора цвета для модели

			if(e.KeyCode == Keys.Enter)
			{
				long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
				if(code_model == 0) return;
				ListView list = new ListView();
				DbSqlColor.SelectInList(list, code_model);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				textBox_color.Text = dialog.SelectedText;
				auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ", dialog.SelectedCode);
				auto.SetData("АВТОМОБИЛЬ_ЦВЕТ", dialog.SelectedText);
			}
		}

		private void textBox_variant_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// При нажатии ENTER вызов диалога выбора исполнения для модели

			if(e.KeyCode == Keys.Enter)
			{
				long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
				if(code_model == 0) return;
				ListView list = new ListView();
				DbSqlVariant.SelectInList(list, code_model);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				textBox_variant.Text = dialog.SelectedText;
				auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", dialog.SelectedCode);
				auto.SetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", dialog.SelectedText);
			}
		}

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Получаем данные
			auto.SetData("VIN", textBox_vin.Text);
			auto.SetData("VIN_ОТСУТСТВУЕТ", checkBox_novin.Checked);
			auto.SetData("VIN_ПРОИЗВОДИТЕЛЬ", textBox_vin_origin.Text);
			auto.SetData("НОМЕР_КУЗОВ", textBox_body.Text);
			auto.SetData("НОМЕР_ДВИГАТЕЛЬ", textBox_engine.Text);
			auto.SetData("НОМЕР_ШАССИ", textBox_frame.Text);
			auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ", DtService.ToLong(textBox_partsno.Text, "НОМЕР ДЛЯ ЗАПЧЕСТЕЙ"));
			auto.SetData("НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ", checkBox_partsno.Checked);
			auto.SetData("ГОД_ВЫПУСК", DtService.ToInt(textBox_year.Text, "ГОД"));
			auto.SetData("НОМЕР_ЗНАК", textBox_sign.Text);
			auto.SetData("ПРИМЕЧАНИЕ", textBox_comment.Text);

			//Проверяем на коректность введенные данные
			if(Db.ShowFaults() == true) return;

			// Записываем данные автомобиля
			if((long)auto.GetData("КОД_АВТОМОБИЛЬ") == 0)
			{
				DtAuto new_auto = DbSqlAuto.Insert(auto);
				if(new_auto == null) return;
				auto = new_auto;
				DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{	
				if(DbSqlAuto.Update(auto) == false) return;
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void textBox_model_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// При нажатии ENTER вызов диалога выбора модели

			if(e.KeyCode == Keys.Enter)
			{
				FormSelectModel dialog = new FormSelectModel();
				if(dialog.ShowDialog() != DialogResult.OK) return;
				textBox_model.Text = dialog.SelectedText;
				auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", dialog.SelectedCode);
				auto.SetData("МОДЕЛЬ", dialog.SelectedText);
			}
		}

		private void textBox_factory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Выбор производителя
			if(e.KeyCode == Keys.Enter)
			{
				ListView list = new ListView();
				DbSqlFactory.SelectInList(list);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				textBox_factory.Text = dialog.SelectedText;
				auto.SetData("КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ", dialog.SelectedCode);
				auto.SetData("ПРОИЗВОДИТЕЛЬ", dialog.SelectedText);
			}
		}

		private void button_factory_cancel_Click(object sender, System.EventArgs e)
		{
			// Обнулить производителя
			textBox_factory.Text = "";
			auto.SetData("КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ", (long)0);
			auto.SetData("ПРОИЗВОДИТЕЛЬ", "");
		}

		private void button_manage_model_Click(object sender, System.EventArgs e)
		{
			// Вызов окна управления моделями
			FormManageModel dialog = new FormManageModel();
			dialog.ShowDialog();
		}

		private void textBox_factory_DoubleClick(object sender, System.EventArgs e)
		{
			ListView list = new ListView();
			DbSqlFactory.SelectInList(list);
			FormSelectionList dialog = new FormSelectionList(list);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			textBox_factory.Text = dialog.SelectedText;
			auto.SetData("КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ", dialog.SelectedCode);
			auto.SetData("ПРОИЗВОДИТЕЛЬ", dialog.SelectedText);
		}

		private void textBox_variant_DoubleClick(object sender, System.EventArgs e)
		{
			long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			if(code_model == 0) return;
			ListView list = new ListView();
			DbSqlVariant.SelectInList(list, code_model);
			FormSelectionList dialog = new FormSelectionList(list);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			textBox_variant.Text = dialog.SelectedText;
			auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", dialog.SelectedCode);
			auto.SetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ", dialog.SelectedText);
		}

		private void textBox_color_DoubleClick(object sender, System.EventArgs e)
		{
			long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			if(code_model == 0) return;
			ListView list = new ListView();
			DbSqlColor.SelectInList(list, code_model);
			FormSelectionList dialog = new FormSelectionList(list);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			textBox_color.Text = dialog.SelectedText;
			auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ", dialog.SelectedCode);
			auto.SetData("АВТОМОБИЛЬ_ЦВЕТ", dialog.SelectedText);
		}

		private void textBox_model_DoubleClick(object sender, System.EventArgs e)
		{
			FormSelectModel dialog = new FormSelectModel();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			textBox_model.Text = dialog.SelectedText;
			auto.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ", dialog.SelectedCode);
			auto.SetData("МОДЕЛЬ", dialog.SelectedText);
		}

		public DtAuto Auto
		{
			get
			{
				return auto;
			}
		}

        private void textBox_model_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
