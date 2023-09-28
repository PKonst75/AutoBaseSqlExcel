using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Интерфейс рабочего места кассира
	/// Произведение оплат, печать отчетов
	/// </summary>
	public class UIF_Casher : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_pay_order;
		private System.Windows.Forms.Button button_department_1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIF_Casher()
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
			this.button_pay_order = new System.Windows.Forms.Button();
			this.button_department_1 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_pay_order
			// 
			this.button_pay_order.Location = new System.Drawing.Point(8, 16);
			this.button_pay_order.Name = "button_pay_order";
			this.button_pay_order.Size = new System.Drawing.Size(96, 96);
			this.button_pay_order.TabIndex = 0;
			this.button_pay_order.Text = "ЗАКАЗ-НАРЯД";
			this.button_pay_order.Click += new System.EventHandler(this.button_pay_order_Click);
			// 
			// button_department_1
			// 
			this.button_department_1.Location = new System.Drawing.Point(136, 16);
			this.button_department_1.Name = "button_department_1";
			this.button_department_1.Size = new System.Drawing.Size(96, 96);
			this.button_department_1.TabIndex = 1;
			this.button_department_1.Text = "ТО, РЕМОНТ, МАСЛА";
			this.button_department_1.Click += new System.EventHandler(this.button_department_1_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(264, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 96);
			this.button1.TabIndex = 2;
			this.button1.Text = "ЗАПЧАСТИ";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// UIF_Casher
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(560, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.button_department_1,
																		  this.button_pay_order});
			this.Name = "UIF_Casher";
			this.Text = "Интерфейс кассира";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_pay_order_Click(object sender, System.EventArgs e)
		{
			// Внесение оплаты по заказ-наряду
			// Запрашиваем номер заказ-наряда
			FormSelectString selector = new FormSelectString("Введите номер заказ-наряда", "");
			if(selector.ShowDialog() != DialogResult.OK) return;
			long warrant = selector.SelectedLong;
			if(warrant == 0L)
			{
				MessageBox.Show("Неправильный формат номера");
				return;
			}
			// Ищем карточку, сопоставленную заказ-наряду
			ArrayList array = new ArrayList();
			DbSqlCard.SelectWarrantNumber(array, warrant);
			if(array.Count == 0)
			{
				MessageBox.Show("Нет карточки соответствующей заказ-наряду");
				return;
			}
			DtCard card = (DtCard)array[array.Count - 1];
			card	= DbSqlCard.Find((long)card.GetData("КОД_КАРТОЧКА"));

			// Плятеж за работы
			CS_Payment payment	= new CS_Payment(1);
			payment.Card		= card;
			if(payment.CheckError() == false) return;
			if(payment.summ != 0.0F)
			{
				// Есть платеж
				UIF_Payment dialog	= new UIF_Payment(payment);
				dialog.ShowDialog();
			}
			// Плятеж за детали
			payment	= new CS_Payment(2);
			payment.Card		= card;
			if(payment.CheckError() == false) return;
			if(payment.summ != 0.0F)
			{
				// Есть платеж
				UIF_Payment dialog	= new UIF_Payment(payment);
				dialog.ShowDialog();
			}
		}

		private void button_department_1_Click(object sender, System.EventArgs e)
		{
			// Оплата по первому отделу
			CS_Payment payment	= new CS_Payment(1);
			// Запрос подразделения
			FormWorkshopList selector_1 = new FormWorkshopList();
			if(selector_1.ShowDialog() != DialogResult.OK) return;
			DbWorkshop ws = selector_1.SelectedWorkshop;
			DtWorkshop workshop = DbSqlWorkshop.Find(ws.Code);
			if(workshop == null) return;
			payment.Workshop = workshop;
			if(payment.CheckError() == false) return;
			// Запрос автомобиля
			string mask = "";
			FormSelectString selector_2 = new FormSelectString("Введите кузов/VIN автомобиля", "");
			if(selector_2.ShowDialog() == DialogResult.OK)
			{
				mask = selector_2.SelectedTextMask;
				FormListAuto selector_3 = new FormListAuto(2, mask);
				if(selector_3.ShowDialog() != DialogResult.OK) return;
				DtAuto auto = selector_3.Auto;
				if(auto == null) return;
				payment.Auto = auto;
				if(payment.CheckError() == false) return;
			}

			UIF_Payment dialog	= new UIF_Payment(payment);
			dialog.ShowDialog();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Оплата по второму отделу
			CS_Payment payment	= new CS_Payment(2);
			// Запрос подразделения
			FormWorkshopList selector_1 = new FormWorkshopList();
			if(selector_1.ShowDialog() != DialogResult.OK) return;
			DbWorkshop ws = selector_1.SelectedWorkshop;
			DtWorkshop workshop = DbSqlWorkshop.Find(ws.Code);
			if(workshop == null) return;
			payment.Workshop = workshop;
			if(payment.CheckError() == false) return;
			// Запрос автомобиля
			string mask = "";
			FormSelectString selector_2 = new FormSelectString("Введите кузов/VIN автомобиля", "");
			if(selector_2.ShowDialog() == DialogResult.OK)
			{
				mask = selector_2.SelectedTextMask;
				FormListAuto selector_3 = new FormListAuto(2, mask);
				if(selector_3.ShowDialog() != DialogResult.OK) return;
				DtAuto auto = selector_3.Auto;
				if(auto == null) return;
				payment.Auto = auto;
				if(payment.CheckError() == false) return;
			}

			UIF_Payment dialog	= new UIF_Payment(payment);
			dialog.ShowDialog();
		}
	}
}
