using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdatePartnerContact.
	/// </summary>
	public class FormUpdatePartnerContact : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox_type;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox_sort;
		private System.Windows.Forms.TextBox textBox_contact;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Button button_ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_cancel;

		DtPartnerContact contact;

		public FormUpdatePartnerContact(long code_partner, long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Стандартное заполнение
			comboBox_type.Items.Add("ТЕЛЕФОН");
			comboBox_type.Items.Add("E-MAIL");

			comboBox_sort.Items.Add("");
			comboBox_sort.Items.Add("ДОМАШНИЙ");
			comboBox_sort.Items.Add("КОНТАКТНЫЙ");
			comboBox_sort.Items.Add("МОБИЛЬНЫЙ");
			comboBox_sort.Items.Add("РАБОЧИЙ");

			if(code != 0)
			{
				contact = DbSqlPartnerContact.Find(code_partner, code);
			}
			else
			{
				contact = new DtPartnerContact();
				contact.SetData("ТИП_КОНТАКТ", "ТЕЛЕФОН");
				contact.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ_КОНТАКТ", code_partner);
			}
			
			// Заполняем имеющиеся данные
			int index = comboBox_type.FindStringExact((string)contact.GetData("ТИП_КОНТАКТ"));
			if(index != -1)
				comboBox_type.SelectedIndex = index;
			else
				comboBox_type.SelectedIndex = -1;

			index = comboBox_sort.FindStringExact((string)contact.GetData("ВИД_КОНТАКТ"));
			if(index != -1)
				comboBox_sort.SelectedIndex = index;
			else
				comboBox_sort.SelectedIndex = -1;

			textBox_contact.Text = (string)contact.GetData("КОНТАКТ");
			textBox_comment.Text = (string)contact.GetData("ПРИМЕЧАНИЕ_КОНТАКТ");
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
			this.comboBox_type = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox_sort = new System.Windows.Forms.ComboBox();
			this.textBox_contact = new System.Windows.Forms.TextBox();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBox_type
			// 
			this.comboBox_type.Location = new System.Drawing.Point(128, 8);
			this.comboBox_type.Name = "comboBox_type";
			this.comboBox_type.Size = new System.Drawing.Size(200, 24);
			this.comboBox_type.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Тип Контакта";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Вид Контакта";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.TabIndex = 3;
			this.label3.Text = "Контакт";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 96);
			this.label4.Name = "label4";
			this.label4.TabIndex = 4;
			this.label4.Text = "Примечание";
			// 
			// comboBox_sort
			// 
			this.comboBox_sort.Location = new System.Drawing.Point(128, 40);
			this.comboBox_sort.Name = "comboBox_sort";
			this.comboBox_sort.Size = new System.Drawing.Size(200, 24);
			this.comboBox_sort.TabIndex = 5;
			// 
			// textBox_contact
			// 
			this.textBox_contact.Location = new System.Drawing.Point(128, 72);
			this.textBox_contact.Name = "textBox_contact";
			this.textBox_contact.Size = new System.Drawing.Size(200, 23);
			this.textBox_contact.TabIndex = 6;
			this.textBox_contact.Text = "";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(8, 120);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(512, 23);
			this.textBox_comment.TabIndex = 7;
			this.textBox_comment.Text = "";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(448, 8);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 8;
			this.button_ok.Text = "OK";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(448, 32);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.TabIndex = 9;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// FormUpdatePartnerContact
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(528, 149);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_cancel,
																		  this.button_ok,
																		  this.textBox_comment,
																		  this.textBox_contact,
																		  this.comboBox_sort,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.comboBox_type});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormUpdatePartnerContact";
			this.Text = "Контакт";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			DtPartnerContact new_contact;
			// Сохраняем изменение / Добавляем новый 
			// Получаем данные
			contact.SetData("КОНТАКТ", textBox_contact.Text);

            // Тип контакта только из списка
            string txt0 = comboBox_type.Text;
            if (comboBox_type.FindStringExact(txt0) == -1)
            {
                comboBox_type.SelectedIndex = 0;
                return;
            }

			// Вид контакта только из списка
			string txt = comboBox_sort.Text;
			if(comboBox_sort.FindStringExact(txt) == -1)
			{
				comboBox_sort.SelectedIndex = 0;
				return;
			}
            contact.SetData("ТИП_КОНТАКТ", txt0);
			contact.SetData("ВИД_КОНТАКТ", txt);
			contact.SetData("ПРИМЕЧАНИЕ_КОНТАКТ", textBox_comment.Text);
			if((long)contact.GetData("КОД_КОНТАКТ") == 0)
			{
				new_contact = DbSqlPartnerContact.Insert(contact);
				if(new_contact == null) return;
				contact = new_contact;
			}
			else
			{
				if(DbSqlPartnerContact.Update(contact)!= true) return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			// Отмена всех изменений
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		public DtPartnerContact Contact
		{
			get
			{
				return contact;
			}
		}
	}
}
