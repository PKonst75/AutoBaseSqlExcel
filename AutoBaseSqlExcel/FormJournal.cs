using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormJournal.
	/// </summary>
	public class FormJournal : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DateTimePicker dateTimePickerDate;
		private System.Windows.Forms.Button buttonShow;
		private int offsetX = 70;
		private int offsetY = 150;
		private int step = 150;

		private long currentCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		ArrayList listWorkPlaceJournal;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.ComponentModel.IContainer components;

		DbCard	activeCard;

		public FormJournal(DbCard srcCard, DateTime dateSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DateTime date;
			// Установка первичной даты
			date = dateSrc;
			dateTimePickerDate.Value = date;

			// Заполняем журнал рабочими листами
			activeCard	= srcCard;
			ArrayList list = DbWorkPlace.MakeList();
			int count = 0;
			if(list != null)
			{
				listWorkPlaceJournal = new ArrayList();
				foreach(object o in list)
				{
					count++;
					DbWorkPlaceJournal wpjournal = new DbWorkPlaceJournal((DbWorkPlace)o);
					wpjournal.GetDateJournal(date);
					listWorkPlaceJournal.Add(wpjournal);
				}
			}
			// Установки вертикальной полосы прокрутки
			vScrollBar1.Minimum = 0;
			vScrollBar1.Maximum = count - 1;
			vScrollBar1.SmallChange = 1;
			vScrollBar1.LargeChange = 1;
			vScrollBar1.Value = 0;
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
			this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			this.buttonShow = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// dateTimePickerDate
			// 
			this.dateTimePickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.dateTimePickerDate.Location = new System.Drawing.Point(320, 8);
			this.dateTimePickerDate.Name = "dateTimePickerDate";
			this.dateTimePickerDate.TabIndex = 0;
			this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
			// 
			// buttonShow
			// 
			this.buttonShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.buttonShow.Location = new System.Drawing.Point(528, 8);
			this.buttonShow.Name = "buttonShow";
			this.buttonShow.TabIndex = 1;
			this.buttonShow.Text = "Показать";
			this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.vScrollBar1.Location = new System.Drawing.Point(616, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(16, 296);
			this.vScrollBar1.TabIndex = 2;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// FormJournal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.vScrollBar1,
																		  this.buttonShow,
																		  this.dateTimePickerDate});
			this.Name = "FormJournal";
			this.Text = "Журнал";
			this.DoubleClick += new System.EventHandler(this.FormJournal_DoubleClick);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormJournal_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormJournal_MouseMove);
			this.Leave += new System.EventHandler(this.FormJournal_Leave);
			this.ResumeLayout(false);

		}
		#endregion

		protected void FormJournal_Paint(object sender, PaintEventArgs e)
		{
			int startI = vScrollBar1.Value;
			int i = 0;
			if(listWorkPlaceJournal == null) return;
			foreach(object o in listWorkPlaceJournal)
			{
				DbWorkPlaceJournal workPlaceJournal = (DbWorkPlaceJournal)o;
				if(i >= startI)
					workPlaceJournal.Draw(e.Graphics, offsetX, (i - startI) * step + offsetY);
				i++;
			}
		}

		protected void FormJournal_DoubleClick(object sender, EventArgs e)
		{
			int startI = vScrollBar1.Value;
			Point pnt = Cursor.Position;
			pnt = this.PointToClient(pnt);
			if(pnt.Y - offsetY < 0) return;
			int i = (pnt.Y - offsetY) / step;
			i = i + startI;
			if ((i < 0) || (i >= listWorkPlaceJournal.Count)) return;

			DbWorkPlaceJournal wpJournal = (DbWorkPlaceJournal)listWorkPlaceJournal[i];
			if(wpJournal.GetIndex(pnt, offsetX, (i - startI) * step + offsetY) == -1) return;

			FormJournalRecord dialog = new FormJournalRecord(wpJournal, wpJournal.GetIndex(pnt, offsetX, (i - startI) * step + offsetY), activeCard);
			dialog.ShowDialog(this);
			this.Invalidate();
		}

		protected void FormJournal_MouseMove(object sender, MouseEventArgs e)
		{
			int startI = vScrollBar1.Value;
			Point pnt = Cursor.Position;
			pnt = this.PointToClient(pnt);
			if(pnt.Y - offsetY < 0) return;
			int i = (pnt.Y - offsetY) / step;
			i = i + startI;
			if ((i < 0) || (i >= listWorkPlaceJournal.Count)) return;

			DbWorkPlaceJournal wpJournal = (DbWorkPlaceJournal)listWorkPlaceJournal[i];
			int index = wpJournal.GetIndex(pnt, offsetX, (i - startI) * step + offsetY);
			long groupCode = wpJournal.GetGroupCode(index);
			if(groupCode == currentCode)
			{
				return;
			}
			if(groupCode == 0)
			{
				toolTip1.Active = false;
				currentCode = 0;
				DbWorkPlace.SetSelectedGroup(currentCode);
				this.Invalidate();
				return;
			}
			// Активная смена подсказки!
			currentCode = groupCode;
			DbWorkPlace.SetSelectedGroup(currentCode);
			toolTip1.SetToolTip(this, wpJournal.GetToolTip(index));
			toolTip1.Active = true;
			this.Invalidate();
		}

		protected void FormJournal_Leave(object sender, EventArgs e)
		{
			toolTip1.Active = false;
			currentCode = 0;
			DbWorkPlace.SetSelectedGroup(currentCode);
			this.Invalidate();
			return;
		}

		private void buttonShow_Click(object sender, System.EventArgs e)
		{
			// Показать данные за определенный день.
			DateTime date = dateTimePickerDate.Value;
			date = new DateTime(date.Year, date.Month, date.Day);
			foreach(object o in listWorkPlaceJournal)
			{
				DbWorkPlaceJournal wpjournal = (DbWorkPlaceJournal)o;
				wpjournal.GetDateJournal(date);
			}
			this.Invalidate();
		}

		protected void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			Invalidate();
		}

		private void dateTimePicker_ValueChanged(object sender, System.EventArgs e)
		{
			// При изменении значение дня - перерисовываем
			// Показать данные за определенный день.
			DateTime date = dateTimePickerDate.Value;
			date = new DateTime(date.Year, date.Month, date.Day);
			if(listWorkPlaceJournal == null) return;
			foreach(object o in listWorkPlaceJournal)
			{
				DbWorkPlaceJournal wpjournal = (DbWorkPlaceJournal)o;
				wpjournal.GetDateJournal(date);
			}
			this.Invalidate();
		}
	}
}
