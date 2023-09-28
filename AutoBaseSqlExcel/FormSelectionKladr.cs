using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectionKladr.
	/// </summary>
	public class FormSelectionKladr : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_region;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_department;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_city;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TreeView treeView_kladr;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_point;
		private System.Windows.Forms.Button button_clear_department;
		private System.Windows.Forms.Button button_clear_city;
		private System.Windows.Forms.Button button_clear_point;
		private System.Windows.Forms.TextBox textBox_find;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button_find;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_street;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_house;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_flat;
		private System.Windows.Forms.Button button_getaddress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string selected_address;

		public FormSelectionKladr()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DbDbfKladr.Init();
			DbDbfKladr.MakeRegions();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormSelectionKladr));
			this.textBox_region = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_department = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_city = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.treeView_kladr = new System.Windows.Forms.TreeView();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_point = new System.Windows.Forms.TextBox();
			this.button_clear_department = new System.Windows.Forms.Button();
			this.button_clear_city = new System.Windows.Forms.Button();
			this.button_clear_point = new System.Windows.Forms.Button();
			this.textBox_find = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button_find = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_street = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_house = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_flat = new System.Windows.Forms.TextBox();
			this.button_getaddress = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_region
			// 
			this.textBox_region.Location = new System.Drawing.Point(176, 8);
			this.textBox_region.Name = "textBox_region";
			this.textBox_region.Size = new System.Drawing.Size(360, 26);
			this.textBox_region.TabIndex = 0;
			this.textBox_region.Text = "";
			this.textBox_region.DoubleClick += new System.EventHandler(this.textBox_region_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Регион";
			// 
			// textBox_department
			// 
			this.textBox_department.Location = new System.Drawing.Point(176, 40);
			this.textBox_department.Name = "textBox_department";
			this.textBox_department.Size = new System.Drawing.Size(360, 26);
			this.textBox_department.TabIndex = 2;
			this.textBox_department.Text = "";
			this.textBox_department.DoubleClick += new System.EventHandler(this.textBox_department_DoubleClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Район";
			// 
			// textBox_city
			// 
			this.textBox_city.Location = new System.Drawing.Point(176, 72);
			this.textBox_city.Name = "textBox_city";
			this.textBox_city.Size = new System.Drawing.Size(360, 26);
			this.textBox_city.TabIndex = 4;
			this.textBox_city.Text = "";
			this.textBox_city.DoubleClick += new System.EventHandler(this.textBox_city_DoubleClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Город";
			// 
			// treeView_kladr
			// 
			this.treeView_kladr.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.treeView_kladr.ImageIndex = -1;
			this.treeView_kladr.Location = new System.Drawing.Point(24, 272);
			this.treeView_kladr.Name = "treeView_kladr";
			this.treeView_kladr.SelectedImageIndex = -1;
			this.treeView_kladr.Size = new System.Drawing.Size(576, 168);
			this.treeView_kladr.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(160, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Населенный пункт";
			// 
			// textBox_point
			// 
			this.textBox_point.Location = new System.Drawing.Point(176, 104);
			this.textBox_point.Name = "textBox_point";
			this.textBox_point.Size = new System.Drawing.Size(360, 26);
			this.textBox_point.TabIndex = 8;
			this.textBox_point.Text = "";
			this.textBox_point.DoubleClick += new System.EventHandler(this.textBox_point_DoubleClick);
			// 
			// button_clear_department
			// 
			this.button_clear_department.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_clear_department.Image")));
			this.button_clear_department.Location = new System.Drawing.Point(536, 40);
			this.button_clear_department.Name = "button_clear_department";
			this.button_clear_department.Size = new System.Drawing.Size(24, 23);
			this.button_clear_department.TabIndex = 9;
			this.button_clear_department.Click += new System.EventHandler(this.button_clear_department_Click);
			// 
			// button_clear_city
			// 
			this.button_clear_city.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_clear_city.Image")));
			this.button_clear_city.Location = new System.Drawing.Point(536, 72);
			this.button_clear_city.Name = "button_clear_city";
			this.button_clear_city.Size = new System.Drawing.Size(24, 23);
			this.button_clear_city.TabIndex = 10;
			this.button_clear_city.Click += new System.EventHandler(this.button_clear_city_Click);
			// 
			// button_clear_point
			// 
			this.button_clear_point.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_clear_point.Image")));
			this.button_clear_point.Location = new System.Drawing.Point(536, 104);
			this.button_clear_point.Name = "button_clear_point";
			this.button_clear_point.Size = new System.Drawing.Size(24, 23);
			this.button_clear_point.TabIndex = 11;
			this.button_clear_point.Click += new System.EventHandler(this.button_clear_point_Click);
			// 
			// textBox_find
			// 
			this.textBox_find.Location = new System.Drawing.Point(136, 240);
			this.textBox_find.Name = "textBox_find";
			this.textBox_find.Size = new System.Drawing.Size(248, 26);
			this.textBox_find.TabIndex = 12;
			this.textBox_find.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 248);
			this.label5.Name = "label5";
			this.label5.TabIndex = 13;
			this.label5.Text = "Поиск";
			// 
			// button_find
			// 
			this.button_find.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_find.Image")));
			this.button_find.Location = new System.Drawing.Point(384, 240);
			this.button_find.Name = "button_find";
			this.button_find.Size = new System.Drawing.Size(24, 23);
			this.button_find.TabIndex = 14;
			this.button_find.Click += new System.EventHandler(this.button_find_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 136);
			this.label6.Name = "label6";
			this.label6.TabIndex = 15;
			this.label6.Text = "Улица";
			// 
			// textBox_street
			// 
			this.textBox_street.Location = new System.Drawing.Point(176, 136);
			this.textBox_street.Name = "textBox_street";
			this.textBox_street.Size = new System.Drawing.Size(360, 26);
			this.textBox_street.TabIndex = 16;
			this.textBox_street.Text = "";
			this.textBox_street.DoubleClick += new System.EventHandler(this.textBox_street_DoubleClick);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 168);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(176, 23);
			this.label7.TabIndex = 17;
			this.label7.Text = "Дом/корпус/строение";
			// 
			// textBox_house
			// 
			this.textBox_house.Location = new System.Drawing.Point(184, 168);
			this.textBox_house.Name = "textBox_house";
			this.textBox_house.Size = new System.Drawing.Size(120, 26);
			this.textBox_house.TabIndex = 18;
			this.textBox_house.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(312, 168);
			this.label8.Name = "label8";
			this.label8.TabIndex = 19;
			this.label8.Text = "Квартира";
			// 
			// textBox_flat
			// 
			this.textBox_flat.Location = new System.Drawing.Point(416, 168);
			this.textBox_flat.Name = "textBox_flat";
			this.textBox_flat.Size = new System.Drawing.Size(120, 26);
			this.textBox_flat.TabIndex = 20;
			this.textBox_flat.Text = "";
			// 
			// button_getaddress
			// 
			this.button_getaddress.Location = new System.Drawing.Point(472, 240);
			this.button_getaddress.Name = "button_getaddress";
			this.button_getaddress.Size = new System.Drawing.Size(128, 23);
			this.button_getaddress.TabIndex = 21;
			this.button_getaddress.Text = "Получить";
			this.button_getaddress.Click += new System.EventHandler(this.button_getaddress_Click);
			// 
			// FormSelectionKladr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(616, 453);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_getaddress,
																		  this.textBox_flat,
																		  this.label8,
																		  this.textBox_house,
																		  this.label7,
																		  this.textBox_street,
																		  this.label6,
																		  this.button_find,
																		  this.label5,
																		  this.textBox_find,
																		  this.button_clear_point,
																		  this.button_clear_city,
																		  this.button_clear_department,
																		  this.textBox_point,
																		  this.label4,
																		  this.treeView_kladr,
																		  this.label3,
																		  this.textBox_city,
																		  this.label2,
																		  this.textBox_department,
																		  this.label1,
																		  this.textBox_region});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormSelectionKladr";
			this.Text = "Классификатор адресов";
			this.ResumeLayout(false);

		}
		#endregion

		private void textBox_region_DoubleClick(object sender, System.EventArgs e)
		{
			// Процедура выбора региона
			ListView list = DbDbfKladr.FillRegions();
			FormSelectionList dialog = new FormSelectionList(list, false);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedCodeObject == null) return;
			textBox_region.Text	= dialog.SelectedText;
			textBox_region.Tag	= dialog.SelectedCodeObject;

			// Очистка всего предыдущего
			textBox_department.Text = "";
			textBox_department.Tag = null;
			textBox_city.Text = "";
			textBox_city.Tag = null;
			textBox_point.Text = "";
			textBox_point.Tag = null;
			treeView_kladr.Nodes.Clear();
			textBox_street.Text = "";
			textBox_street.Tag = null;

			// После выбора региона заполняем все остальное
			DbDbfKladr.KladrData data = (DbDbfKladr.KladrData)dialog.SelectedCodeObject;
			string code = data.code;
			DbDbfKladr.MakeAll(code);
			DbDbfKladr.FillKladr(treeView_kladr);
			DbDbfKladr.MakeStreets(code);
		}

		private void textBox_department_DoubleClick(object sender, System.EventArgs e)
		{
			// Процедура выбора района
			if(textBox_region.Tag == null) return;
			DbDbfKladr.KladrData data = (DbDbfKladr.KladrData)textBox_region.Tag;
			ListView list = DbDbfKladr.FillDepartments(data.code);
			FormSelectionList dialog = new FormSelectionList(list, false);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedCodeObject == null) return;
			textBox_department.Text	= dialog.SelectedText;
			textBox_department.Tag	= dialog.SelectedCodeObject;

			// Очистка всего предыдущего
			textBox_city.Text = "";
			textBox_city.Tag = null;
			textBox_point.Text = "";
			textBox_point.Tag = null;
			textBox_street.Text = "";
			textBox_street.Tag = null;
		}

		private void textBox_city_DoubleClick(object sender, System.EventArgs e)
		{
			DbDbfKladr.KladrData data;
			// Процедура выбора города
			if(textBox_region.Tag == null) return;
			data = (DbDbfKladr.KladrData)textBox_region.Tag;
			if(textBox_department.Tag != null)
				data = (DbDbfKladr.KladrData)textBox_department.Tag;
			ListView list = DbDbfKladr.FillCity(data.code);
			FormSelectionList dialog = new FormSelectionList(list, false);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedCodeObject == null) return;
			textBox_city.Text	= dialog.SelectedText;
			textBox_city.Tag	= dialog.SelectedCodeObject;

			// Очистка всего предыдущего
			textBox_point.Text = "";
			textBox_point.Tag = null;
			textBox_street.Text = "";
			textBox_street.Tag = null;
		}

		private void textBox_point_DoubleClick(object sender, System.EventArgs e)
		{
			DbDbfKladr.KladrData data;
			// Процедура выбора города
			if(textBox_region.Tag == null) return;
			data = (DbDbfKladr.KladrData)textBox_region.Tag;
			if(textBox_department.Tag != null)
				data = (DbDbfKladr.KladrData)textBox_department.Tag;
			if(textBox_city.Tag != null)
				data = (DbDbfKladr.KladrData)textBox_city.Tag;
			ListView list = DbDbfKladr.FillPoints(data.code);
			FormSelectionList dialog = new FormSelectionList(list, false);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedCodeObject == null) return;
			textBox_point.Text	= dialog.SelectedText;
			textBox_point.Tag	= dialog.SelectedCodeObject;
		}

		private void button_clear_department_Click(object sender, System.EventArgs e)
		{
			// Процедура отмены района
			textBox_department.Text	= "";
			textBox_department.Tag	= null;

			// Очистка всего предыдущего
			textBox_city.Text = "";
			textBox_city.Tag = null;
			textBox_point.Text = "";
			textBox_point.Tag = null;
			textBox_street.Text = "";
			textBox_street.Tag = null;
		}

		private void button_clear_city_Click(object sender, System.EventArgs e)
		{
			textBox_city.Text	= "";
			textBox_city.Tag	= null;

			// Очистка всего предыдущего
			textBox_point.Text = "";
			textBox_point.Tag = null;
		}

		private void button_clear_point_Click(object sender, System.EventArgs e)
		{
			textBox_point.Text	= "";
			textBox_point.Tag	= null;
		}

		private void button_find_Click(object sender, System.EventArgs e)
		{
			// Поиск в заданном регионе
			if(textBox_region.Tag == null) return;
			object o;
			DbDbfKladr.KladrData data;
			string name = textBox_find.Text;
			// Начинаем с населенных улиц
			o = DbDbfKladr.FindStreet(name);
			if(o == null)
			{
				o = DbDbfKladr.FindPoint(name);
				if(o == null)
				{
					// Поиск дальше
					o = DbDbfKladr.FindCity(name);
					if(o == null)
					{
						// Поиск дальше
						o = DbDbfKladr.FindDepartment(name);
					}
				}
			}
			else
			{
				ArrayList list = (ArrayList)o;
				if(list.Count == 1)
					o = (DbDbfKladr.KladrData)list[0];
				else
				{
					// Запрос, что именно выбираем
					ListView list_view = DbDbfKladr.FillAdresses(list);
					FormSelectionList dialog = new FormSelectionList(list_view, false);
					if(dialog.ShowDialog() != DialogResult.OK) return;
					if(dialog.SelectedCodeObject == null) o = null;
					o = dialog.SelectedCodeObject;
				}
			}
			//
			if(o == null)
			{
				// Так и не нашли
				// Очистим
				textBox_department.Text	= "";
				textBox_department.Tag	= null;
				textBox_city.Text = "";
				textBox_city.Tag = null;
				textBox_point.Text = "";
				textBox_point.Tag = null;
				textBox_street.Text = "";
				textBox_street.Tag = null;
				return;	
			}
			data = (DbDbfKladr.KladrData)o;
			string code = data.code;
			o = DbDbfKladr.SelectDepartment(code);
			if(o != null)
			{
				data = (DbDbfKladr.KladrData)o;
				textBox_department.Text	= data.name;
				textBox_department.Tag	= data;
			}
			else
			{
				textBox_department.Text	= "";
				textBox_department.Tag	= null;
			}
			o = DbDbfKladr.SelectPoint(code);
			if(o != null)
			{
				data = (DbDbfKladr.KladrData)o;
				textBox_point.Text	= data.name;
				textBox_point.Tag	= data;
			}
			else
			{
				textBox_point.Text	= "";
				textBox_point.Tag	= null;
			}
			o = DbDbfKladr.SelectCity(code);
			if(o != null)
			{
				data = (DbDbfKladr.KladrData)o;
				textBox_city.Text	= data.name;
				textBox_city.Tag	= data;
			}
			else
			{
				textBox_city.Text	= "";
				textBox_city.Tag	= null;
			}
			o = DbDbfKladr.SelectDepartment(code);
			if(o != null)
			{
				data = (DbDbfKladr.KladrData)o;
				textBox_department.Text	= data.name;
				textBox_department.Tag	= data;
			}
			else
			{
				textBox_department.Text	= "";
				textBox_department.Tag	= null;
			}
			o = DbDbfKladr.SelectStreet(code);
			if(o != null)
			{
				data = (DbDbfKladr.KladrData)o;
				textBox_street.Text	= data.name;
				textBox_street.Tag	= data;
			}
			else
			{
				textBox_street.Text	= "";
				textBox_street.Tag	= null;
			}
		}

		private void textBox_street_DoubleClick(object sender, System.EventArgs e)
		{
			DbDbfKladr.KladrData data;
			object o = null;
			// Поиск в заданном регионе
			if(textBox_region.Tag == null) return;
			if(textBox_point.Tag != null)
			{
				o = textBox_point.Tag;
			}
			else
			{
				if(textBox_city.Tag != null)
					o = textBox_city.Tag;
			}
			if(o == null) return;
			data = (DbDbfKladr.KladrData)o;
			
			// Запуск выбора улицы
			ListView list = DbDbfKladr.FillStreets(data.code);
			FormSelectionList dialog = new FormSelectionList(list, false);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(dialog.SelectedCodeObject == null) return;
			textBox_street.Text	= dialog.SelectedText;
			textBox_street.Tag	= dialog.SelectedCodeObject;
		}

		private void button_getaddress_Click(object sender, System.EventArgs e)
		{
			// Получить итоговый адрес, преобразованный в строку
			DbDbfKladr.KladrData data;
			string address = "";
			string part = "";
			string index = "";
			// Регион, обязательно должен быть выбран...
			if(textBox_region.Tag == null) return;
			data = (DbDbfKladr.KladrData)textBox_region.Tag;
			address = DbDbfKladr.MakeTitle(data.name, data.socr);
			index	= data.index; 
			// Район, если есть
			if(textBox_department.Tag != null)
			{
				data = (DbDbfKladr.KladrData)textBox_department.Tag;
				part = DbDbfKladr.MakeTitle(data.name, data.socr);
				address += ", " + part;
				if(data.index.Length != 0) index = data.index; 
			}
			// Город, если есть
			if(textBox_city.Tag != null)
			{
				data = (DbDbfKladr.KladrData)textBox_city.Tag;
				part = DbDbfKladr.MakeTitle(data.name, data.socr);
				address += ", " + part;
				if(data.index.Length != 0) index = data.index; 
			}
			// Населенный пункт, если есть
			if(textBox_point.Tag != null)
			{
				data = (DbDbfKladr.KladrData)textBox_point.Tag;
				part = DbDbfKladr.MakeTitle(data.name, data.socr);
				address += ", " + part;
				if(data.index.Length != 0) index = data.index; 
			}
			// Улица, если есть
			if(textBox_street.Tag != null)
			{
				data = (DbDbfKladr.KladrData)textBox_street.Tag;
				part = DbDbfKladr.MakeTitle(data.name, data.socr);
				address += ", " + part;
				index = data.index; 
			}
			// Дом, если есть
			part = textBox_house.Text;
			part = part.ToUpper();
			part = part.Trim();
			if(part.Length != 0)
			{
				address += ", " + part;
			}
			// Квартира, если есть
			part = textBox_flat.Text;
			part = part.Trim();
			if(part.Length != 0)
			{
				address += "-" + part;
			}
			if(index.Length == 0)
				selected_address = address;
			else
				selected_address = index + ", " + address;
			this.Close();
			this.DialogResult = DialogResult.OK;
		}

		public string SelectedAddress
		{
			get
			{
				return selected_address;
			}
		}
	}
}
