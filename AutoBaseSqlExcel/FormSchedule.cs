using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSchedule.
	/// </summary>
	public class FormSchedule : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ArrayList workPlaces;
		ArrayList days = new ArrayList();
		ArrayList posts = new ArrayList();
		Brush brushBlack;
		Brush brushGreen;
		Brush brushRed;
		Brush brushYellow;
		Font font;
		Pen pen;

		public FormSchedule()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Подготовка объектов рисования
			brushYellow = new SolidBrush(Color.Yellow);
			brushGreen = new SolidBrush(Color.LightGreen);
			brushRed = new SolidBrush(Color.Red);
			brushBlack = new SolidBrush(Color.Black);
			pen = new Pen(Color.Black, 1);
			font = new Font("Microsoft Sans Serif", 10);

			// Создание списка всех рабочих мест
			workPlaces		= DbWorkPlace.MakeList();
			posts = new ArrayList();
			// Для каждого элемента списка, составляем расписание, на каждую дату
			foreach(object o in workPlaces)
			{
				DbWorkPlace workPlace = (DbWorkPlace)o;
				days = new ArrayList(20);
				for(int day = 0; day < 20; day++)
				{
					DateTime date;
					date	= DateTime.Today.AddDays(day);
					days.Add((int)DbJournal.CountIntervals(workPlace.Code, date));
				}
				posts.Add(days);
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
			// 
			// FormSchedule
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 357);
			this.Name = "FormSchedule";
			this.Text = "Общее расписание";
			this.Paint += new PaintEventHandler(FormSchedule_Paint);
			this.DoubleClick += new EventHandler(FormSchedule_DoubleClick);
		}
		#endregion

		protected void FormSchedule_DoubleClick(object sender, EventArgs e)
		{
			// Определим дату, на которой кликнули
			Point pnt	= Cursor.Position;
			pnt			= this.PointToClient(pnt);

			int xoffset		= 25;
			int yoffset		= 50;
			int col1		= 100;
			int height		= 25;
			int intervalX	= 40;
			int interval	= 5;

			int count		= workPlaces.Count;
			if (pnt.Y < yoffset - height) return;
			if (pnt.Y - yoffset - (height * count + interval) > 0) return;
			if (pnt.X < xoffset + col1) return;
			if (pnt.X - xoffset - col1 - intervalX * 20 > 0) return;
			int	day			= (pnt.X - xoffset - col1) / intervalX;
			DateTime date	= DateTime.Today.AddDays(day);

			FormJournal dialog = new FormJournal(null, date);
			dialog.Show();
		}

		protected void FormSchedule_Paint(object sender, PaintEventArgs e)
		{
			StringFormat format		= new StringFormat();
			format.LineAlignment	= StringAlignment.Center;
			format.Alignment		= StringAlignment.Near;
			RectangleF rect			= new RectangleF(0, 0, 0, 0);

			int height		= 25;
			int xoffset		= 25;
			int yoffset		= 50;
			int col1		= 100;
			int interval	= 5;
			int intervalX	= 40;
			string text;

			format.Alignment		= StringAlignment.Center;
			format.LineAlignment	= StringAlignment.Center;
			// Числа
			rect.Width = 40;
			rect.Height	= height;
			rect.Y	= yoffset - rect.Height;
			rect.X = xoffset + col1;
			for(int day = 0; day < 20; day++)
			{
				DateTime date;
				date	= DateTime.Today.AddDays(day);
				text	= date.Day.ToString();
				if(text.Length == 1) text = "0" + text;
				text += ".";
				if (date.Month.ToString().Length == 1)
					text += "0" + date.Month.ToString();
				else
					text += date.Month.ToString();
				e.Graphics.DrawString(text, font, brushBlack, rect, format);
				rect.X += intervalX;
			}


			if(workPlaces == null) return;
			rect.Y	= yoffset;
			rect.Height	= height;
			rect.Width	= col1;
			int j = 0;
			foreach(object o in workPlaces)
			{
				rect.X	= xoffset;
				DbWorkPlace workPlace = (DbWorkPlace)o;
				e.Graphics.DrawString(workPlace.Name, font, brushBlack, rect);
				rect.X					= rect.X + rect.Width;
				for(int day = 0; day < 20; day++)
				{
					rect.Width = 40;
					int freeIntervals = workPlace.CountHours() - (int)((ArrayList)posts[j])[day];
					int hours = freeIntervals / 4;
					int minutes = (freeIntervals - (freeIntervals / 4) * 4) * 15;
					float freeOurs = (float)freeIntervals * 15.0F / 60.0F;
					if (freeOurs <= 0)
						e.Graphics.FillRectangle(brushRed, rect);
					else if (freeOurs <= 1)
						e.Graphics.FillRectangle(brushYellow, rect);
					else
						e.Graphics.FillRectangle(brushGreen, rect);
					
					e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
					text = hours.ToString();
					text += "-";
					if(minutes.ToString().Length == 1)
						text += "0" + minutes.ToString();
					else
						text += minutes.ToString();
					if (freeOurs > 0) e.Graphics.DrawString(text, font, brushBlack, rect, format);
					rect.X += intervalX;
				}
				rect.Width	= col1;
				rect.Y	= rect.Y + rect.Height + interval;
				format.LineAlignment	= StringAlignment.Center;
				format.Alignment		= StringAlignment.Near;
				j++;
			}
		}
	}
}
