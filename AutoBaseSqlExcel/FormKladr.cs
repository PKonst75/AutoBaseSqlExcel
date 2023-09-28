using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormKladr.
	/// </summary>
	public class FormKladr : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxRegion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxArea;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxCity;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxSettlement;
		private System.Windows.Forms.TextBox textBoxStreet;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxHouse;
		private System.Windows.Forms.TextBox textBoxFlat;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ArrayList arrayRegion		= null;
		int	regionTextIndex			= 0;
		DbKladr region				= null;
		ArrayList arrayArea			= null;
		int	areaTextIndex			= 0;
		DbKladr area				= null;
		ArrayList arrayCity			= null;
		int	cityTextIndex			= 0;
		DbKladr city				= null;
		ArrayList arraySettlement	= null;
		int	settlementTextIndex		= 0;
		DbKladr settlement			= null;
		ArrayList arrayStreet		= null;
		int	streetTextIndex			= 0;
		DbKladr street				= null;

		bool ourChange				= false;
		bool ourPress				= false;
		
		string address				= "";
		

		// По базе данных
		SqlConnection kladrConnection = null;

		public FormKladr()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Попытка открыть базу данных КЛАДР
			try
			{
				
				string kladrConnectionString = ""; 
				kladrConnectionString = "initial catalog=kladr;data source=programm;Connect Timeout=5;user id=кладр;password=кладр";
				kladrConnection = new SqlConnection(kladrConnectionString);
				kladrConnection.Open();
				DbKladr.Init(kladrConnection);
			}
			catch(Exception e)
			{
				if (kladrConnection != null) kladrConnection.Close();
				kladrConnection = null;
				Db.SetException(e);
				Db.ShowFaults();
			}
			// Заполнение нужных массивов
			if(kladrConnection == null)
			{
				MessageBox.Show("База данных не открыта");
				return;
			}
			FormInfoTable dialog = new FormInfoTable("Загрузка регионов...");
			dialog.Show();
			arrayRegion = new ArrayList();
			DbKladr.FillArrayRegion(arrayRegion);
			dialog.Close();
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
			this.textBoxRegion = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxArea = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxCity = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxSettlement = new System.Windows.Forms.TextBox();
			this.textBoxStreet = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxHouse = new System.Windows.Forms.TextBox();
			this.textBoxFlat = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxRegion
			// 
			this.textBoxRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxRegion.Location = new System.Drawing.Point(120, 0);
			this.textBoxRegion.Name = "textBoxRegion";
			this.textBoxRegion.Size = new System.Drawing.Size(368, 26);
			this.textBoxRegion.TabIndex = 0;
			this.textBoxRegion.Text = "";
			this.textBoxRegion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegion_KeyDown);
			this.textBoxRegion.GotFocus += new System.EventHandler(this.textBoxRegion_GotFocus);
			this.textBoxRegion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRegion_KeyPress);
			this.textBoxRegion.TextChanged += new System.EventHandler(this.textBoxRegion_TextChanged);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(208, 200);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 7;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "РЕГИОН";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBoxArea
			// 
			this.textBoxArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxArea.Location = new System.Drawing.Point(120, 32);
			this.textBoxArea.Name = "textBoxArea";
			this.textBoxArea.Size = new System.Drawing.Size(368, 26);
			this.textBoxArea.TabIndex = 1;
			this.textBoxArea.Text = "";
			this.textBoxArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxArea_KeyDown);
			this.textBoxArea.GotFocus += new System.EventHandler(this.textBoxArea_GotFocus);
			this.textBoxArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxArea_KeyPress);
			this.textBoxArea.TextChanged += new System.EventHandler(this.textBoxArea_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "РАЙОН";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "ГОРОД";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBoxCity
			// 
			this.textBoxCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxCity.Location = new System.Drawing.Point(120, 64);
			this.textBoxCity.Name = "textBoxCity";
			this.textBoxCity.Size = new System.Drawing.Size(368, 26);
			this.textBoxCity.TabIndex = 2;
			this.textBoxCity.Text = "";
			this.textBoxCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCity_KeyDown);
			this.textBoxCity.GotFocus += new System.EventHandler(this.textBoxCity_GotFocus);
			this.textBoxCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCity_KeyPress);
			this.textBoxCity.TextChanged += new System.EventHandler(this.textBoxCity_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "НАС. ПУНКТ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBoxSettlement
			// 
			this.textBoxSettlement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxSettlement.Location = new System.Drawing.Point(120, 96);
			this.textBoxSettlement.Name = "textBoxSettlement";
			this.textBoxSettlement.Size = new System.Drawing.Size(368, 26);
			this.textBoxSettlement.TabIndex = 3;
			this.textBoxSettlement.Text = "";
			this.textBoxSettlement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSettlement_KeyDown);
			this.textBoxSettlement.GotFocus += new System.EventHandler(this.textBoxSettlement_GotFocus);
			this.textBoxSettlement.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSettlement_KeyPress);
			this.textBoxSettlement.TextChanged += new System.EventHandler(this.textBoxSettlement_TextChanged);
			// 
			// textBoxStreet
			// 
			this.textBoxStreet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxStreet.Location = new System.Drawing.Point(120, 128);
			this.textBoxStreet.Name = "textBoxStreet";
			this.textBoxStreet.Size = new System.Drawing.Size(368, 26);
			this.textBoxStreet.TabIndex = 4;
			this.textBoxStreet.Text = "";
			this.textBoxStreet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxStreet_KeyDown);
			this.textBoxStreet.GotFocus += new System.EventHandler(this.textBoxStreet_GotFocus);
			this.textBoxStreet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxStreet_KeyPress);
			this.textBoxStreet.TextChanged += new System.EventHandler(this.textBoxStreet_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 128);
			this.label5.Name = "label5";
			this.label5.TabIndex = 10;
			this.label5.Text = "УЛИЦА";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBoxHouse
			// 
			this.textBoxHouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxHouse.Location = new System.Drawing.Point(120, 160);
			this.textBoxHouse.Name = "textBoxHouse";
			this.textBoxHouse.TabIndex = 5;
			this.textBoxHouse.Text = "";
			this.textBoxHouse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHouse_KeyDown);
			// 
			// textBoxFlat
			// 
			this.textBoxFlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxFlat.Location = new System.Drawing.Point(384, 160);
			this.textBoxFlat.Name = "textBoxFlat";
			this.textBoxFlat.Size = new System.Drawing.Size(104, 26);
			this.textBoxFlat.TabIndex = 6;
			this.textBoxFlat.Text = "";
			this.textBoxFlat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFlat_KeyDown);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 160);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 23);
			this.label6.TabIndex = 13;
			this.label6.Text = "ДОМ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(280, 160);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 23);
			this.label7.TabIndex = 14;
			this.label7.Text = "КВАРТИРА";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormKladr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(496, 227);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label7,
																		  this.label6,
																		  this.textBoxFlat,
																		  this.textBoxHouse,
																		  this.label5,
																		  this.textBoxStreet,
																		  this.textBoxSettlement,
																		  this.label4,
																		  this.textBoxCity,
																		  this.label3,
																		  this.label2,
																		  this.textBoxArea,
																		  this.label1,
																		  this.buttonOk,
																		  this.textBoxRegion});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FormKladr";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Адрес";
			this.ResumeLayout(false);
		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Построение готового адреса
			string txt  = "";
			txt += region.NameTxt;
			if(area != null) txt += ", " + area.NameTxt;
			if(city != null) txt += ", " + city.NameTxt;
			if(settlement != null) txt += ", " + settlement.NameTxt;
			if(street != null) txt += ", " + street.NameTxt;
			if(textBoxHouse.Text.Length != 0) txt += ", д." + textBoxHouse.Text;
			if(textBoxFlat.Text.Length != 0) txt += ", кв." + textBoxFlat.Text;

			address	= txt;
			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		#region РЕГИОН
		private void textBoxRegion_GotFocus(object sender, EventArgs e)
		{
			// Если прошли по кругу и ткнули снова в регион
			regionTextIndex		= 0;
			textBoxRegion.SelectAll();
		}
		private void textBoxRegion_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(ourPress == true)
			{
				e.Handled	= true;
				ourPress	= false;
			}
		}
		private void textBoxRegion_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов региона
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayArea = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка районов....");
				dialog.Show();
				DbKladr.FillArrayArea(arrayArea, region);
				dialog.Close();
				// Конец загрузка
				textBoxArea.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов региона
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayArea = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка районов....");
				dialog.Show();
				DbKladr.FillArrayArea(arrayArea, region);
				dialog.Close();
				// Конец загрузка
				textBoxArea.Focus();
			}
			if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				e.Handled	= true;
			}
			if(e.KeyCode == Keys.Up)
			{
				e.Handled	= true;
				region = MoveDirection(textBoxRegion, region, arrayRegion, -1);
				regionTextIndex = 0;
			}
			if(e.KeyCode == Keys.Down)
			{
				e.Handled	= true;
				region = MoveDirection(textBoxRegion, region, arrayRegion, +1);
				regionTextIndex = 0;
			}
			if(e.KeyCode == Keys.Delete)
			{
				e.Handled	= true;
				regionTextIndex = 0;
				region		= null;
				ourChange	= true;
				textBoxRegion.Text	= "";
			}
			if(e.KeyCode == Keys.Back)
			{
				e.Handled	= true;
				ourPress	= true;
				// Удяляем последнюю букву
				regionTextIndex		= RemoveLastTest(textBoxRegion, regionTextIndex);
				region				= ClearTextTest(textBoxRegion, regionTextIndex, region);
				if(region == null) return;
				DbKladr element		= FindElementRemove(arrayRegion, textBoxRegion, regionTextIndex);
				regionTextIndex		= TextChangeRemove(textBoxRegion, element, regionTextIndex);
				region				= element;
			}
		}
		private void textBoxRegion_TextChanged(object sender, EventArgs e)
		{
			if(ourChange == true) {ourChange = false; return;}					// Если изменение вызвано нами - по второму разу не отрабатываем
			DbKladr element		= ElementFind(arrayRegion, textBoxRegion.Text);	// Ищем в массиве наиболее подходящий для нас элемент
			ourChange			= true;											// Отмечаем, что изменение вызванно нами
			regionTextIndex		= TextChange(textBoxRegion, element, region, regionTextIndex);
			if(element != null)	region = element;
		}
		#endregion

		#region РАЙОН
		private void textBoxArea_GotFocus(object sender, EventArgs e)
		{
			textBoxArea.SelectAll();
			areaTextIndex	= 0;
		}
		private void textBoxArea_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(ourPress == true)
			{
				e.Handled	= true;
				ourPress	= false;
			}
		}
		private void textBoxArea_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayCity = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка городов....");
				dialog.Show();
				DbKladr.FillArrayCity(arrayCity, region, area);
				dialog.Close();
				// Конец загрузка
				textBoxCity.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayCity = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка городов....");
				dialog.Show();
				DbKladr.FillArrayCity(arrayCity, region, area);
				dialog.Close();
				// Конец загрузка
				textBoxCity.Focus();
			}
			if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				e.Handled	= true;
			}
			if(e.KeyCode == Keys.Up)
			{
				e.Handled	= true;
				area = MoveDirection(textBoxArea, area, arrayArea, -1);
				areaTextIndex = 0;
			}
			if(e.KeyCode == Keys.Down)
			{
				e.Handled	= true;
				area = MoveDirection(textBoxArea, area, arrayArea, +1);
				areaTextIndex = 0;
			}
			if(e.KeyCode == Keys.Delete)
			{
				e.Handled	= true;
				areaTextIndex = 0;
				area		= null;
				ourChange	= true;
				textBoxArea.Text	= "";
			}
			if(e.KeyCode == Keys.Back)
			{
				e.Handled	= true;
				ourPress	= true;
				// Удяляем последнюю букву
				areaTextIndex		= RemoveLastTest(textBoxArea, areaTextIndex);
				area				= ClearTextTest(textBoxArea, areaTextIndex, area);
				if(area == null) return;
				DbKladr element		= FindElementRemove(arrayArea, textBoxArea, areaTextIndex);
				areaTextIndex		= TextChangeRemove(textBoxArea, element, areaTextIndex);
				area				= element;
			}
		}
		private void textBoxArea_TextChanged(object sender, EventArgs e)
		{
			if(ourChange == true) {ourChange = false; return;}					// Если изменение вызвано нами - по второму разу не отрабатываем
			DbKladr element		= ElementFind(arrayArea, textBoxArea.Text);	// Ищем в массиве наиболее подходящий для нас элемент
			ourChange			= true;											// Отмечаем, что изменение вызванно нами
			areaTextIndex		= TextChange(textBoxArea, element, area, areaTextIndex);
			if(element != null)	area = element;
		}
		#endregion

		#region ГОРОД
		private void textBoxCity_GotFocus(object sender, EventArgs e)
		{
			// При получении фокуса - зачитать соответствующий массив заново, выбрать весь текст
			textBoxCity.SelectAll();
			cityTextIndex	= 0;
		}
		private void textBoxCity_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(ourPress == true)
			{
				e.Handled	= true;
				ourPress	= false;
			}
		}
		private void textBoxCity_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arraySettlement = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка населенных пунктов....");
				dialog.Show();
				DbKladr.FillArraySettlement(arraySettlement, region, area, city);
				dialog.Close();
				// Конец загрузка
				textBoxSettlement.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arraySettlement = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка населенных пунктов....");
				dialog.Show();
				DbKladr.FillArraySettlement(arraySettlement, region, area, city);
				dialog.Close();
				// Конец загрузка
				textBoxSettlement.Focus();
			}
			if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				e.Handled	= true;
			}
			if(e.KeyCode == Keys.Up)
			{
				e.Handled	= true;
				city = MoveDirection(textBoxCity, city, arrayCity, -1);
				cityTextIndex = 0;
			}
			if(e.KeyCode == Keys.Down)
			{
				e.Handled	= true;
				city = MoveDirection(textBoxCity, city, arrayCity, +1);
				cityTextIndex = 0;
			}
			if(e.KeyCode == Keys.Delete)
			{
				e.Handled	= true;
				cityTextIndex = 0;
				city		= null;
				ourChange	= true;
				textBoxCity.Text	= "";
			}
			if(e.KeyCode == Keys.Back)
			{
				e.Handled	= true;
				ourPress	= true;
				// Удяляем последнюю букву
				cityTextIndex		= RemoveLastTest(textBoxCity, cityTextIndex);
				city				= ClearTextTest(textBoxCity, cityTextIndex, city);
				if(city == null) return;
				DbKladr element		= FindElementRemove(arrayCity, textBoxCity, cityTextIndex);
				cityTextIndex		= TextChangeRemove(textBoxCity, element, cityTextIndex);
				city				= element;
			}
		}
		private void textBoxCity_TextChanged(object sender, EventArgs e)
		{
			if(ourChange == true) {ourChange = false; return;}					// Если изменение вызвано нами - по второму разу не отрабатываем
			DbKladr element		= ElementFind(arrayCity, textBoxCity.Text);		// Ищем в массиве наиболее подходящий для нас элемент
			ourChange			= true;											// Отмечаем, что изменение вызванно нами
			cityTextIndex		= TextChange(textBoxCity, element, city, cityTextIndex);
			if(element != null)	city = element;
		}
		#endregion

		#region НАСЕЛЕННЫЙ ПУНКТ
		private void textBoxSettlement_GotFocus(object sender, EventArgs e)
		{
			// При получении фокуса - зачитать соответствующий массив заново, выбрать весь текст
			textBoxSettlement.SelectAll();
			settlementTextIndex	= 0;
		}
		private void textBoxSettlement_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(ourPress == true)
			{
				e.Handled	= true;
				ourPress	= false;
			}
		}
		private void textBoxSettlement_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayStreet = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка улиц....");
				dialog.Show();
				DbKladr.FillArrayStreet(arrayStreet, region, area, city, settlement);
				dialog.Close();
				// Конец загрузка
				textBoxStreet.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				ourPress	= true;
				// Загрузка районов данного региона, переход к ним
				// Попытка открыть базу данных КЛАДР
				if(CheckConnection() == false) return;
				if(region == null)
				{
					textBoxRegion.Focus();
					MessageBox.Show("Не выбран регион");
					return;
				}
				arrayStreet = new ArrayList();
				FormInfoTable dialog = new FormInfoTable("Загрузка улиц....");
				dialog.Show();
				DbKladr.FillArrayStreet(arrayStreet, region, area, city, settlement);
				dialog.Close();
				// Конец загрузка
				textBoxStreet.Focus();
			}
			if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				e.Handled	= true;
			}
			if(e.KeyCode == Keys.Up)
			{
				e.Handled	= true;
				settlement = MoveDirection(textBoxSettlement, settlement, arraySettlement, -1);
				settlementTextIndex = 0;
			}
			if(e.KeyCode == Keys.Down)
			{
				e.Handled	= true;
				settlement = MoveDirection(textBoxSettlement, settlement, arraySettlement, +1);
				settlementTextIndex = 0;
			}
			if(e.KeyCode == Keys.Delete)
			{
				e.Handled	= true;
				settlementTextIndex = 0;
				settlement	= null;
				ourChange	= true;
				textBoxSettlement.Text	= "";
			}
			if(e.KeyCode == Keys.Back)
			{
				e.Handled	= true;
				ourPress	= true;
				// Удяляем последнюю букву
				settlementTextIndex		= RemoveLastTest(textBoxSettlement, settlementTextIndex);
				settlement				= ClearTextTest(textBoxSettlement, settlementTextIndex, settlement);
				if(settlement == null) return;
				DbKladr element			= FindElementRemove(arraySettlement, textBoxSettlement, settlementTextIndex);
				settlementTextIndex		= TextChangeRemove(textBoxSettlement, element, settlementTextIndex);
				settlement				= element;
			}
		}
		private void textBoxSettlement_TextChanged(object sender, EventArgs e)
		{
			if(ourChange == true) {ourChange = false; return;}							// Если изменение вызвано нами - по второму разу не отрабатываем
			DbKladr element		= ElementFind(arraySettlement, textBoxSettlement.Text);	// Ищем в массиве наиболее подходящий для нас элемент
			ourChange			= true;													// Отмечаем, что изменение вызванно нами
			settlementTextIndex	= TextChange(textBoxSettlement, element, settlement, settlementTextIndex);
			if(element != null)	settlement = element;
		}
		#endregion

		#region УЛИЦА
		private void textBoxStreet_GotFocus(object sender, EventArgs e)
		{
			// При получении фокуса - зачитать соответствующий массив заново, выбрать весь текст
			textBoxStreet.SelectAll();
			streetTextIndex	= 0;
		}
		private void textBoxStreet_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(ourPress == true)
			{
				e.Handled	= true;
				ourPress	= false;
			}
		}
		private void textBoxStreet_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				ourPress	= true;
				textBoxHouse.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				ourPress	= true;
				textBoxHouse.Focus();
			}
			if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				e.Handled	= true;
			}
			if(e.KeyCode == Keys.Up)
			{
				e.Handled	= true;
				street = MoveDirection(textBoxStreet, street, arrayStreet, -1);
				streetTextIndex = 0;
			}
			if(e.KeyCode == Keys.Down)
			{
				e.Handled	= true;
				street = MoveDirection(textBoxStreet, street, arrayStreet, +1);
				streetTextIndex = 0;
			}
			if(e.KeyCode == Keys.Delete)
			{
				e.Handled	= true;
				streetTextIndex = 0;
				street		= null;
				ourChange	= true;
				textBoxStreet.Text	= "";
			}
			if(e.KeyCode == Keys.Back)
			{
				e.Handled	= true;
				ourPress	= true;
				// Удяляем последнюю букву
				streetTextIndex		= RemoveLastTest(textBoxStreet, streetTextIndex);
				street				= ClearTextTest(textBoxStreet, streetTextIndex, street);
				if(street == null) return;
				DbKladr element		= FindElementRemove(arrayStreet, textBoxStreet, streetTextIndex);
				streetTextIndex		= TextChangeRemove(textBoxStreet, element, streetTextIndex);
				street				= element;
			}
		}
		private void textBoxStreet_TextChanged(object sender, EventArgs e)
		{
			if(ourChange == true) {ourChange = false; return;}					// Если изменение вызвано нами - по второму разу не отрабатываем
			DbKladr element		= ElementFind(arrayStreet, textBoxStreet.Text);	// Ищем в массиве наиболее подходящий для нас элемент
			ourChange			= true;											// Отмечаем, что изменение вызванно нами
			streetTextIndex		= TextChange(textBoxStreet, element, street, streetTextIndex);
			if(element != null)	street = element;
		}
		#endregion

		#region ДОМ
		private void textBoxHouse_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				textBoxFlat.Focus();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				textBoxFlat.Focus();
			}
		}
		#endregion

		#region Квартира
		private void textBoxFlat_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.Handled	= true;
				buttonOk.Select();
			}
			if(e.KeyCode == Keys.Tab)
			{
				e.Handled	= true;
				buttonOk.Select();
			}
		}
		#endregion

		#region Служебные методы
		private int IndexFind(ArrayList array, string txt)
		{
			if(array == null) return -1;
			txt				= txt.ToUpper();
			int	index		= -1;
			foreach(object o in array)
			{
				DbKladr element = (DbKladr)o;
				if(element.Title.ToUpper().StartsWith(txt))
				{
					if(index < 0)
						index		= array.IndexOf(o);
				}
			}
			return index;
		}
		private DbKladr ElementFind(ArrayList array, string txt)
		{
			int	index		= IndexFind(array, txt);
			if(index < 0) return null;
			DbKladr element		= (DbKladr)array[index];
			return element;
		}
		private int TextChange(TextBox textBox, DbKladr elementNew, DbKladr elementOld, int textIndex)
		{
			if(elementNew != null)
			{
				textBox.Text	= elementNew.Title;
				textIndex		+= 1;
				textBox.Select(textIndex, textBox.Text.Length);
			}
			else
			{
				if(elementOld == null)
				{
					textBox.Text	= "";
				}
				else
				{
					textBox.Text	= elementOld.Title;
					textBox.Select(textIndex, textBox.Text.Length);
				}
			}
			return textIndex;
		}
		private bool CheckConnection()
		{
			if(kladrConnection == null)
			{
				MessageBox.Show("База данных не открыта");
				return false;
			}
			return true;
		}
		private int RemoveLastTest(TextBox textBox, int textIndex)
		{
			if(textIndex == 0) return textIndex;
			if(textIndex == 1)
			{
				textIndex		= 0;
				textBox.Text	= "";
				return textIndex;
			}
			return textIndex;
		}
		private DbKladr ClearTextTest(TextBox textBox, int textIndex, DbKladr element)
		{
			if(textIndex != 0) return element;
			ourChange = true;
			textBox.Text		= "";
			return null;
		}
		private DbKladr FindElementRemove(ArrayList array, TextBox textBox, int textIndex)
		{
			string txt			= textBox.Text;
			txt					= txt.ToUpper();
			txt					= txt.Substring(0, textIndex);
			DbKladr element		= ElementFind(array, txt);
			return element;
		}
		private int TextChangeRemove(TextBox textBox, DbKladr element, int textIndex)
		{
			if(element == null) return textIndex;	
			ourChange			= true;
			textBox.Text		= element.Title;
			textIndex			-= 1;
			textBox.Select(textIndex, textBox.Text.Length);
			return textIndex;
		}
		private DbKladr MoveDirection(TextBox textBox, DbKladr element, ArrayList array, int direction)
		{
			if (array.Count == 0) return null;
			int index;
			DbKladr	elementMoved;
			if(element == null)
			{
				if(direction > 0) 
					index = 0;
				else
					index = array.Count - 1;
				elementMoved	= (DbKladr)array[index];
			}
			else
			{
				index	= array.IndexOf(element);
				index		+= direction;
				if((index >= array.Count)||(index < 0))
					elementMoved	= element;
				else
					elementMoved	= (DbKladr)array[index];
			}
			ourChange				= true;
			textBox.Text			= elementMoved.Title;
			textBox.SelectAll();
			return elementMoved;
		}
		#endregion

		public string Address
		{
			get
			{
				return address;
			}
		}
	}
}
