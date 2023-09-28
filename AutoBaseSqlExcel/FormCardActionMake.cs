using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardActionMake.
	/// </summary>
	public class FormCardActionMake : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_open;
		private System.Windows.Forms.Button button_close;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_stop;
		private System.Windows.Forms.Button button_start;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		short action_code			= 0;
		DtCard card					= null;

		ListViewItem connected_item	= null;

		public FormCardActionMake(long card_number, int card_year)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Загрузка карточки
			card = DbSqlCard.Find(card_number, card_year);
			if (card == null) return;

			// Необходимо определить текущий статус заказ-наряда.
			DtCardAction action	= DbSqlCardAction.SelectLast(card_number, card_year);
			action_code = 0;
			if(action != null) 
			{
				action_code = (short)action.ActionCode;
			}
			// Натройка внешнего вида
			button_open.Enabled		= false;
			button_close.Enabled	= false;
			button_cancel.Enabled	= false;
			button_start.Enabled	= false;
			button_stop.Enabled		= false;
			button_open.Visible		= false;
			button_close.Visible	= false;
			button_cancel.Visible	= false;
			button_start.Visible	= false;
			button_stop.Visible		= false;
			
			switch(action_code)
			{
				case 0:
					button_cancel.Enabled	= true;
					button_open.Enabled		= true;
					button_cancel.Visible	= true;
					button_open.Visible		= true;
					break;
				case 1:
					button_close.Enabled	= true;
					button_stop.Enabled		= true;
					button_close.Visible	= true;
					button_stop.Visible		= true;
					break;
				case 2:
					textBox_comment.Enabled	= false;
					textBox_comment.Visible	= false;
					break;
				case 3:
					button_start.Enabled	= true;
					button_start.Visible	= true;
					break;
				case 4:
					button_close.Enabled	= true;
					button_stop.Enabled		= true;
					button_close.Visible	= true;
					button_stop.Visible		= true;
					break;
				default:
					break;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCardActionMake));
			this.button_open = new System.Windows.Forms.Button();
			this.button_close = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_stop = new System.Windows.Forms.Button();
			this.button_start = new System.Windows.Forms.Button();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// button_open
			// 
			this.button_open.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_open.Image")));
			this.button_open.Location = new System.Drawing.Point(8, 24);
			this.button_open.Name = "button_open";
			this.button_open.Size = new System.Drawing.Size(96, 96);
			this.button_open.TabIndex = 0;
			this.toolTip1.SetToolTip(this.button_open, "Открыть заказ-наряд");
			this.button_open.Click += new System.EventHandler(this.button_open_Click);
			// 
			// button_close
			// 
			this.button_close.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_close.Image")));
			this.button_close.Location = new System.Drawing.Point(8, 24);
			this.button_close.Name = "button_close";
			this.button_close.Size = new System.Drawing.Size(96, 96);
			this.button_close.TabIndex = 1;
			this.toolTip1.SetToolTip(this.button_close, "Закрыть заказ-наряд");
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_cancel.Image")));
			this.button_cancel.Location = new System.Drawing.Point(176, 24);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(96, 96);
			this.button_cancel.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_cancel, "Анулировать заказ-наряд");
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_stop
			// 
			this.button_stop.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_stop.Image")));
			this.button_stop.Location = new System.Drawing.Point(176, 24);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(96, 96);
			this.button_stop.TabIndex = 3;
			this.toolTip1.SetToolTip(this.button_stop, "Приостановить заказ-наряд");
			this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
			// 
			// button_start
			// 
			this.button_start.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_start.Image")));
			this.button_start.Location = new System.Drawing.Point(176, 24);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(96, 96);
			this.button_start.TabIndex = 4;
			this.toolTip1.SetToolTip(this.button_start, "Возобновить заказ-наряд");
			this.button_start.Click += new System.EventHandler(this.button_start_Click);
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(8, 136);
			this.textBox_comment.Multiline = true;
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(272, 96);
			this.textBox_comment.TabIndex = 5;
			this.textBox_comment.Text = "";
			this.toolTip1.SetToolTip(this.textBox_comment, "Комментарий к действию");
			// 
			// FormCardActionMake
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.BackColor = System.Drawing.Color.PaleGreen;
			this.ClientSize = new System.Drawing.Size(288, 245);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_comment,
																		  this.button_start,
																		  this.button_stop,
																		  this.button_cancel,
																		  this.button_close,
																		  this.button_open});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "FormCardActionMake";
			this.Opacity = 0.800000011920929;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Действие с карточкой";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormCardActionMake_KeyUp);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_open_Click(object sender, System.EventArgs e)
		{
			// Открываем заказ наряд
			if(CardOpen() == false) return;
			Refresh();
			this.Close();
		}

		private void button_close_Click(object sender, System.EventArgs e)
		{
			// Закрываем заказ-наряд
			if(CardClose() == false) return;
			Refresh();
			this.Close();
		}

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			// Отменяем карточку
			if(CardCancel() == false) return;
			Refresh();
			this.Close();
		}

		private void button_stop_Click(object sender, System.EventArgs e)
		{
			// Приостанавливаем заказ-наряд
			if(CardStop() == false) return;
			Refresh();
			this.Close();
		}

		private void button_start_Click(object sender, System.EventArgs e)
		{
			// Возобновляем заказ-наряд
			if(CardStart() == false) return;
			Refresh();
			this.Close();
		}

		protected override void OnCreateControl()
		{
			if(card == null)
			{
				this.Close();
				return;
			}
			switch(action_code)
			{
				case 0:
					button_open.Select();
					break;
				case 1:
					button_stop.Select();
					break;
				case 2:
					this.Close();
					break;
				case 3:
					button_start.Select();
					break;
				case 4:
					button_stop.Select();
					break;
				default:
					this.Close();
					break;
			}
		}

		private void FormCardActionMake_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Escape:
					this.Close();
					break;
				default:
					break;
			}
		}

		private bool CardClose()
		{
			long code_workshop		= (long)card.GetData("ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА");
			long code_partner		= (long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА");
			DtPartner partner		= (DtPartner)DbSqlPartner.Find(code_partner);
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			// Закрыть заказ-наряд
			// Запрос об установке скидки
			if(code_workshop == 1)
			{
				// Скидка только для сервиса
				if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == true)
				{
					// Для юридических лиц скидка по списку
					// Загружаем свойтсва контрагента
					DtPartnerProperty property = DbSqlPartnerProperty.Find(code_partner);
					if(property != null)
					{
						if(property.Discount > 0)
						{
							// Запрос на предоставление скидки
							if(MessageBox.Show("Предоставить корпоративную скидку " + property.Discount.ToString() + "%", "Запрос",  MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								// Предоставляем скидку корпоративную
								DbSqlCard.SetDiscount(card_number, card_year, property.Discount, 0);
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
							if(discount == null) return false;
							// Предоставляем скидку дисконтную
							DbSqlCard.SetDiscount(card_number, card_year, (float)discount.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ"), (long)discount.GetData("КОД_ДИСКОНТ"));
						}
					}
				}
			}

			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего заказ-наряд", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return false;
			if(staff.SelectedLong == 0) return false;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return false;
			if(DbSqlCard.SetMaster(card_number, card_year, master) != true) return false;
			
			return DtCardAction.Action(card_number, card_year, DbCardAction.ActionCodes.Close, textBox_comment.Text);
		}

		private bool CardOpen()
		{
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			return DtCardAction.Action(card_number, card_year, DbCardAction.ActionCodes.Open, textBox_comment.Text);
		}
		private bool CardStart()
		{
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			return DtCardAction.Action(card_number, card_year, DbCardAction.ActionCodes.Start, textBox_comment.Text);
		}
		private bool CardStop()
		{
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			return DtCardAction.Action(card_number, card_year, DbCardAction.ActionCodes.Stop, textBox_comment.Text);
		}
		private bool CardCancel()
		{
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			return DtCardAction.Action(card_number, card_year, DbCardAction.ActionCodes.Cancel, textBox_comment.Text);
		}

		public void SetConnection(ListViewItem item)
		{
			connected_item	= item;
		}
		private void RefreshConnection()
		{
			long card_number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year			= (int)card.GetData("ГОД_КАРТОЧКА");

			DtCard refresh = DbSqlCard.Find(card_number, card_year);
			if (refresh == null) return;

			if(connected_item != null && connected_item.ListView.IsDisposed == false)
			{
				refresh.SetLVItem(connected_item);
			}
		}
	}
}
