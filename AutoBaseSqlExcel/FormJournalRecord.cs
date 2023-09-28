using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormJournalRecord.
	/// </summary>
	public class FormJournalRecord : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.NumericUpDown numericUpDownHours;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownMinut;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.TextBox textBoxCard;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSelectCard;

		private int startIndex;
		private DbWorkPlaceJournal wpJournal;
		private DbCard card;

		public FormJournalRecord(DbWorkPlaceJournal sourceJournal, int sourceStartIndex, DbCard srcCard)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			wpJournal	= sourceJournal;
			startIndex	= sourceStartIndex;

			// Проверяем, свободно ли место
			DbJournal journal = wpJournal.GetJournalAt(startIndex);
			if(journal != null)
			{
				buttonOk.Enabled = false;
				buttonRemove.Enabled = true;
				buttonUpdate.Enabled = true;
				textBoxCard.Text	= journal.CardTxt;
			}
			else
			{
				card						= srcCard;
				if(card != null)
				{
					textBoxCard.Text			= card.CardTxt;
					buttonSelectCard.Enabled	= false;
					// В карточке есть тип автомобиля и есть сумма - по времени, выбранных работ
					// Определяем часы и минуты
					int hours = (int)card.Time;
					int minut = (int)((card.Time - (float)hours) * 60.0F);
					if ((minut > 0) &&(minut <= 15)) minut = 15;
					if ((minut > 15) &&(minut <= 30)) minut = 30;
					if ((minut > 30) &&(minut <= 45)) minut = 45;
					if ((minut > 45) &&(minut <= 60))
					{
						minut = 0;
						hours++;
					}
					numericUpDownHours.Value = hours;
					numericUpDownMinut.Value = minut;
				}
				else
					textBoxCard.Text		= "Карточка не выбрана";
				buttonRemove.Enabled	= false;
				buttonOk.Enabled		= true;
				buttonUpdate.Enabled	= false;
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
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownMinut = new System.Windows.Forms.NumericUpDown();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.textBoxCard = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSelectCard = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinut)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(8, 128);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(488, 23);
			this.textBoxComment.TabIndex = 0;
			this.textBoxComment.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 104);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Примечание";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(200, 160);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// numericUpDownHours
			// 
			this.numericUpDownHours.Location = new System.Drawing.Point(8, 32);
			this.numericUpDownHours.Maximum = new System.Decimal(new int[] {
																			   24,
																			   0,
																			   0,
																			   0});
			this.numericUpDownHours.Name = "numericUpDownHours";
			this.numericUpDownHours.Size = new System.Drawing.Size(64, 23);
			this.numericUpDownHours.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Часов";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(96, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Минут";
			// 
			// numericUpDownMinut
			// 
			this.numericUpDownMinut.Increment = new System.Decimal(new int[] {
																				 15,
																				 0,
																				 0,
																				 0});
			this.numericUpDownMinut.Location = new System.Drawing.Point(96, 32);
			this.numericUpDownMinut.Maximum = new System.Decimal(new int[] {
																			   45,
																			   0,
																			   0,
																			   0});
			this.numericUpDownMinut.Name = "numericUpDownMinut";
			this.numericUpDownMinut.Size = new System.Drawing.Size(64, 23);
			this.numericUpDownMinut.TabIndex = 6;
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(416, 160);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.TabIndex = 7;
			this.buttonRemove.Text = "Удалить";
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(8, 160);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.TabIndex = 8;
			this.buttonUpdate.Text = "Обновить";
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// textBoxCard
			// 
			this.textBoxCard.Location = new System.Drawing.Point(8, 80);
			this.textBoxCard.Name = "textBoxCard";
			this.textBoxCard.ReadOnly = true;
			this.textBoxCard.Size = new System.Drawing.Size(464, 23);
			this.textBoxCard.TabIndex = 9;
			this.textBoxCard.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 24);
			this.label4.TabIndex = 10;
			this.label4.Text = "Карточка";
			// 
			// buttonSelectCard
			// 
			this.buttonSelectCard.Location = new System.Drawing.Point(472, 80);
			this.buttonSelectCard.Name = "buttonSelectCard";
			this.buttonSelectCard.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectCard.TabIndex = 11;
			this.buttonSelectCard.Text = "...";
			this.buttonSelectCard.Click += new System.EventHandler(this.buttonSelectCard_Click);
			// 
			// FormJournalRecord
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(504, 189);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSelectCard,
																		  this.label4,
																		  this.textBoxCard,
																		  this.buttonUpdate,
																		  this.buttonRemove,
																		  this.numericUpDownMinut,
																		  this.label3,
																		  this.label2,
																		  this.numericUpDownHours,
																		  this.buttonOk,
																		  this.label1,
																		  this.textBoxComment});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormJournalRecord";
			this.Text = "Запись в журнале";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinut)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			string text = textBoxComment.Text;

			// Проводим все проверки
			// Создаем записи в журнале, сразу с добавлением в базу!!
			int indexCount = (int)numericUpDownHours.Value * 4 + (int)numericUpDownMinut.Value / 15;
			if(indexCount == 0)
			{
				MessageBox.Show("Выберете время");
				return;
			}
			if((card == null)&&(text == ""))
			{
				MessageBox.Show("Выберете карточку или введите примечание");
				return;
			}
			// Проверка на возможность записи в указанное время
			int endIndex = startIndex + indexCount - 1;
			if(endIndex > 95) endIndex = 95;
			for(int index = startIndex; index <= endIndex; index++)
			{
				if(wpJournal.IsAvaliableAt(index) != true)
				{
					MessageBox.Show("Время не рабочее");
					return;
				}
				if(wpJournal.GetJournalAt(index) != null)
				{
					MessageBox.Show("Время занято");
					return;
				}
			}
			// Создаем лист журналов для записи в базу
			DbJournal journal = new DbJournal(wpJournal.WorkPlace, wpJournal.Date, startIndex);
			journal.SetCard(card);
			journal.SetComment(text);
			wpJournal.SetJournalAt(startIndex, journal);
			for(int index = startIndex + 1; index <= endIndex; index++)
			{
				DbJournal journalNext = new DbJournal(journal, index);
				journalNext.SetCard(card);
				journalNext.SetComment(text);
				wpJournal.SetJournalAt(index, journalNext);
			}

			// Процедура записи
			if(wpJournal.Add(startIndex, endIndex) == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			// Удалить существующую запись
			DbJournal journal = wpJournal.GetJournalAt(startIndex);
			if(journal != null)
			{
				if(wpJournal.Remove(journal) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновить существующую запись
			DbJournal journal = wpJournal.GetJournalAt(startIndex);
			if(journal != null)
			{
				if(wpJournal.Update(journal) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void buttonSelectCard_Click(object sender, System.EventArgs e)
		{
			// Выбор карточки для записи в журнал
			FormCardList dialog = new FormCardList(Db.ClickType.Select, 0, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card				= dialog.SelectedCard;
			textBoxCard.Text	= card.CardTxt;

			// Определяем часы и минуты
			int hours = (int)card.Time;
			int minut = (int)((card.Time - (float)hours) * 60.0F);
			if ((minut > 0) &&(minut <= 15)) minut = 15;
			if ((minut > 15) &&(minut <= 30)) minut = 30;
			if ((minut > 30) &&(minut <= 45)) minut = 45;
			if ((minut > 45) &&(minut <= 60))
			{
				minut = 0;
				hours++;
			}
			numericUpDownHours.Value = hours;
			numericUpDownMinut.Value = minut;
		}
	}
}
