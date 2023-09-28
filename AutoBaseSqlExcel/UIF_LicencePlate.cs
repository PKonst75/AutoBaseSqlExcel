using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_LicencePlate.
	/// </summary>
	public class UIF_LicencePlate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RadioButton radioButton_licence;
		private System.Windows.Forms.RadioButton radioButton_tranzit;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.TextBox textBox_region;
		private System.Windows.Forms.Button button_save;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RadioButton radioButton_free;

		long code_auto;
		private DtLicensePlate _licencePlate = null;

		public UIF_LicencePlate(long auto, string old_plate)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			code_auto = auto;
			radioButton_free.Checked = true;
			// Анализ старого регистрационного знака
			string txt = old_plate.ToUpper();
			txt = txt.Replace(" ", "");
			txt = txt.Trim();
			if (txt.Length > 9) txt = txt.Substring(0, 9);
			if (txt.Length < 6)
			{
				// Совсем неправильный формат
				textBox_region.Text = "54";
				return;
			}
			string number = txt.Substring(0, 6);
			string region = "";
			if (txt.Length == 9)
				region = txt.Substring(6, 3);
			if (txt.Length == 8)
				region = txt.Substring(6, 2);
			if (txt.Length == 7 || txt.Length == 6)
			{
				region = "54";
			}
			textBox_region.Text = region;

			string part1 = number.Substring(0, 2);
			string part2 = number.Substring(2, 4);
			string part3 = "";
			if (IsDigits(part2) == true)
			{
				// Это транзит
				if (IsRussian(part1) == false) return;  // Неверный формат
				textBox_number.Text = number;
				radioButton_tranzit.Checked = true;
				radioButton_licence.Checked = false;
			}
			else
			{
				// Это регистрационный знак
				if (number.Length < 6) return; // неверный формат
				part1 = number.Substring(0, 1);
				part2 = number.Substring(1, 3);
				part3 = number.Substring(4, 2);
				if (IsRussian(part1) == false) return;  // Неверный формат
				if (IsDigits(part2) == false) return;
				if (IsRussian(part3) == false) return;  // Неверный формат
				textBox_number.Text = number;
				radioButton_tranzit.Checked = false;
				radioButton_licence.Checked = true;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.radioButton_licence = new System.Windows.Forms.RadioButton();
			this.radioButton_tranzit = new System.Windows.Forms.RadioButton();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.textBox_region = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.radioButton_free = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// radioButton_licence
			// 
			this.radioButton_licence.Location = new System.Drawing.Point(8, 8);
			this.radioButton_licence.Name = "radioButton_licence";
			this.radioButton_licence.Size = new System.Drawing.Size(152, 24);
			this.radioButton_licence.TabIndex = 0;
			this.radioButton_licence.Text = "Регистрационный знак";
			// 
			// radioButton_tranzit
			// 
			this.radioButton_tranzit.Location = new System.Drawing.Point(8, 32);
			this.radioButton_tranzit.Name = "radioButton_tranzit";
			this.radioButton_tranzit.Size = new System.Drawing.Size(136, 24);
			this.radioButton_tranzit.TabIndex = 1;
			this.radioButton_tranzit.Text = "Транзитный номер";
			// 
			// textBox_number
			// 
			this.textBox_number.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_number.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_number.Location = new System.Drawing.Point(8, 88);
			this.textBox_number.MaxLength = 6;
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.TabIndex = 2;
			this.textBox_number.Text = "";
			this.textBox_number.TextChanged += new System.EventHandler(this.textBox_number_TextChanged);
			// 
			// textBox_region
			// 
			this.textBox_region.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_region.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_region.Location = new System.Drawing.Point(128, 88);
			this.textBox_region.MaxLength = 3;
			this.textBox_region.Name = "textBox_region";
			this.textBox_region.Size = new System.Drawing.Size(48, 19);
			this.textBox_region.TabIndex = 3;
			this.textBox_region.Text = "";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(56, 120);
			this.button_save.Name = "button_save";
			this.button_save.TabIndex = 4;
			this.button_save.Text = "Сохранить";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// radioButton_free
			// 
			this.radioButton_free.Location = new System.Drawing.Point(8, 56);
			this.radioButton_free.Name = "radioButton_free";
			this.radioButton_free.Size = new System.Drawing.Size(128, 24);
			this.radioButton_free.TabIndex = 5;
			this.radioButton_free.Text = "Свободный формат";
			// 
			// UIF_LicencePlate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(192, 149);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.radioButton_free,
																		  this.button_save,
																		  this.textBox_region,
																		  this.textBox_number,
																		  this.radioButton_tranzit,
																		  this.radioButton_licence});
			this.Name = "UIF_LicencePlate";
			this.Text = "Регистрационный знак автомобиля";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Проверяем полученные данные
			string number = textBox_number.Text;
			string region = textBox_region.Text;

			// стандартные действия
			number = number.ToUpper();
			region = region.ToUpper();
			number = number.Trim();
			region = region.Trim();
			// Общие проверки
			if (number.Length != 6) return;
			if (region.Length < 2 && region.Length > 3) return;
			if (IsRussianDigits(number) == false) return;
			if (IsDigits(region) == false) return;
			// Проверяем формат
			if (radioButton_licence.Checked)
			{
				// Это регистрационный знак
				string part1 = number.Substring(0, 1);
				string part2 = number.Substring(1, 3);
				string part3 = number.Substring(4, 2);

				if (IsRussian(part1) == false) return;
				if (IsDigits(part2) == false) return;
				if (IsRussian(part3) == false) return;
			}

			if (radioButton_tranzit.Checked)
			{
				// Это транзитный номер
				string part1 = number.Substring(0, 2);
				string part2 = number.Substring(2, 4);
				if (IsRussian(part1) == false) return;
				if (IsDigits(part2) == false) return;
			}
			if (radioButton_free.Checked)
			{
				// Это свободный формат
				if (IsRussianDigits(number) == false) return;
			}
			// Все проверенно, номер верный, можно присвоить номер автомобилю.
			string plate = number + region;
			if (DbSqlAuto.UpdateLicencePlate(code_auto, number, region) == false) return;
			DialogResult = DialogResult.OK;
			this.Close();
			// Сохраняем данные о регистрациооном знаке в рамках ООП АТД
			LicensePlate = MakeLicensePlate(number, region);
		}

		public DtLicensePlate LicensePlate
		{
            get { return _licencePlate; }
			set { _licencePlate = value;  }
		}
		private DtLicensePlate MakeLicensePlate(string srcNumber, string srcRegion)
        {
			DtLicensePlate licencePlate = new DtLicensePlate(srcNumber, srcRegion);
			return licencePlate;
        }

		public bool IsRussianDigits(string txt)
		{
			string pattern = "ЦУКЕНГШХФВАПРОЛДЖЭЯЧСМИТБЮ1234567890";
			char[] pattern_chr = pattern.ToCharArray();
			int index = txt.LastIndexOfAny(pattern_chr);
			while (index != -1)
			{
				txt = txt.Remove(index, 1);
				index = txt.LastIndexOfAny(pattern_chr);
			}
			if (txt.Length != 0) return false;
			return true;
		}

		public bool IsRussian(string txt)
		{
			string pattern = "ЦУКЕНГШХФВАПРОЛДЖЭЯЧСМИТБЮ";
			char[] pattern_chr = pattern.ToCharArray();
			int index = txt.LastIndexOfAny(pattern_chr);
			while (index != -1)
			{
				txt = txt.Remove(index, 1);
				index = txt.LastIndexOfAny(pattern_chr);
			}
			if (txt.Length != 0) return false;
			return true;
		}

		public bool IsDigits(string txt)
		{
			string pattern = "1234567890";
			char[] pattern_chr = pattern.ToCharArray();
			int index = txt.LastIndexOfAny(pattern_chr);
			while (index != -1)
			{
				txt = txt.Remove(index, 1);
				index = txt.LastIndexOfAny(pattern_chr);
			}
			if (txt.Length != 0) return false;
			return true;
		}

		private void textBox_number_TextChanged(object sender, System.EventArgs e)
		{
			string txt	= textBox_number.Text;
			txt = txt.ToUpper();
			if(txt != textBox_number.Text)
			{
				textBox_number.Text = txt;
				textBox_number.Select(txt.Length, 0);
			}
			if(txt.Length == 6)
			{
				textBox_region.Focus();
			}
		}
	}
}
