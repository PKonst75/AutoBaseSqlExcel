using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSetMistake.
	/// </summary>
	public class FormSetMistake : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_guaranty;
		private System.Windows.Forms.Button button_select_guaranty;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_mistake_initiator;
		private System.Windows.Forms.Button button_select_mistake_initiator;
		private System.Windows.Forms.TextBox textBox_mistake;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_remove_mistake_initiator;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtCardDetail	card_detail			= null;
		DtCardWork		card_work			= null;
		DbStaff			mistake_initiator	= null;
		DbGuarantyType	guaranty_type		= null;
		string			mistake_text		= "";
		int				the_type;

		public FormSetMistake(DtCard card, long position, int type)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			the_type	= type;
			switch(type)
			{
				case 0:
					InitDetail(card, position);
					break;
				case 1:
					InitWork(card, position);
					break;
				default:
					button_ok.Enabled = false;
					break;
			}
			// Îòîáğàæåíèå
			if(mistake_initiator != null)
				textBox_mistake_initiator.Text	= mistake_initiator.Title;
			else
				textBox_mistake_initiator.Text	= "Íå óñòàíîâëåí";

			if(guaranty_type != null)
				textBox_guaranty.Text	= guaranty_type.Description;
			else
				textBox_guaranty.Text	= "Íå óñòàíîâëåí";

			textBox_mistake.Text		= mistake_text;
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
			this.textBox_guaranty = new System.Windows.Forms.TextBox();
			this.button_select_guaranty = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_mistake_initiator = new System.Windows.Forms.TextBox();
			this.button_select_mistake_initiator = new System.Windows.Forms.Button();
			this.textBox_mistake = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_remove_mistake_initiator = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_guaranty
			// 
			this.textBox_guaranty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_guaranty.Enabled = false;
			this.textBox_guaranty.Location = new System.Drawing.Point(8, 32);
			this.textBox_guaranty.Name = "textBox_guaranty";
			this.textBox_guaranty.Size = new System.Drawing.Size(408, 23);
			this.textBox_guaranty.TabIndex = 0;
			this.textBox_guaranty.Text = "";
			// 
			// button_select_guaranty
			// 
			this.button_select_guaranty.Location = new System.Drawing.Point(416, 32);
			this.button_select_guaranty.Name = "button_select_guaranty";
			this.button_select_guaranty.Size = new System.Drawing.Size(24, 23);
			this.button_select_guaranty.TabIndex = 1;
			this.button_select_guaranty.Text = "...";
			this.button_select_guaranty.Click += new System.EventHandler(this.button_select_guaranty_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Âèä ãàğàíòèè";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Âèíîâíèê";
			// 
			// textBox_mistake_initiator
			// 
			this.textBox_mistake_initiator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_mistake_initiator.Enabled = false;
			this.textBox_mistake_initiator.Location = new System.Drawing.Point(8, 88);
			this.textBox_mistake_initiator.Name = "textBox_mistake_initiator";
			this.textBox_mistake_initiator.Size = new System.Drawing.Size(408, 23);
			this.textBox_mistake_initiator.TabIndex = 4;
			this.textBox_mistake_initiator.Text = "";
			// 
			// button_select_mistake_initiator
			// 
			this.button_select_mistake_initiator.Location = new System.Drawing.Point(416, 88);
			this.button_select_mistake_initiator.Name = "button_select_mistake_initiator";
			this.button_select_mistake_initiator.Size = new System.Drawing.Size(24, 23);
			this.button_select_mistake_initiator.TabIndex = 5;
			this.button_select_mistake_initiator.Text = "...";
			this.button_select_mistake_initiator.Click += new System.EventHandler(this.button_select_mistake_initiator_Click);
			// 
			// textBox_mistake
			// 
			this.textBox_mistake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_mistake.Location = new System.Drawing.Point(8, 128);
			this.textBox_mistake.MaxLength = 1024;
			this.textBox_mistake.Multiline = true;
			this.textBox_mistake.Name = "textBox_mistake";
			this.textBox_mistake.Size = new System.Drawing.Size(456, 112);
			this.textBox_mistake.TabIndex = 6;
			this.textBox_mistake.Text = "";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(184, 248);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 7;
			this.button_ok.Text = "ÎÊ";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_remove_mistake_initiator
			// 
			this.button_remove_mistake_initiator.Location = new System.Drawing.Point(440, 88);
			this.button_remove_mistake_initiator.Name = "button_remove_mistake_initiator";
			this.button_remove_mistake_initiator.Size = new System.Drawing.Size(24, 23);
			this.button_remove_mistake_initiator.TabIndex = 8;
			this.button_remove_mistake_initiator.Text = "Õ";
			this.button_remove_mistake_initiator.Click += new System.EventHandler(this.button_remove_mistake_initiator_Click);
			// 
			// FormSetMistake
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(474, 277);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_remove_mistake_initiator,
																		  this.button_ok,
																		  this.textBox_mistake,
																		  this.button_select_mistake_initiator,
																		  this.textBox_mistake_initiator,
																		  this.label2,
																		  this.label1,
																		  this.button_select_guaranty,
																		  this.textBox_guaranty});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormSetMistake";
			this.Text = "Ïåğåóñòàíîâêà âèäà ãàğàíòèè";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// Ïğîâåğêà óñòàíîâëåííûõ äàííûõ
			// Âûïîëíÿåì óñòàíîâêó
			mistake_text = textBox_mistake.Text;
			
			switch(the_type)
			{
				case 0:
					if(OkDetail() != true) return;
					break;
				case 1:
					if(OkWork() != true) return;
					break;
				default:
					break;
			}

			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		private void button_select_guaranty_Click(object sender, System.EventArgs e)
		{
			// Âûáèğàåì âèä ãàğàíòèè
			FormGuarantyTypeList dialog = new FormGuarantyTypeList(Db.ClickType.Select);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			guaranty_type	= dialog.GuarantyType;
			textBox_guaranty.Text	= guaranty_type.Description;
		}

		private void button_select_mistake_initiator_Click(object sender, System.EventArgs e)
		{
			// Âûáèğàåì âèíîâíèêà
			FormStaffList dialog = new FormStaffList();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			mistake_initiator		= dialog.SelectedStaff;
			textBox_mistake_initiator.Text	= mistake_initiator.Title;
		}

		private void button_remove_mistake_initiator_Click(object sender, System.EventArgs e)
		{
			// Îòìåíÿåì âèíîâíèêà
			mistake_initiator = null;
			textBox_mistake_initiator.Text = "Íå óñòàíîâëåí";
		}

		public DtCardDetail CardDetail
		{
			get
			{
				return card_detail;
			}
		}
		public DtCardWork CardWork
		{
			get
			{
				return card_work;
			}
		}

		// Ğàáîòà ñ äåòàëÿìè
		private void InitDetail(DtCard card, long position)
		{
			// Ïîëó÷àåì ıëåìåíò ñ êîòîğûì ğàáîòàåì
			card_detail = DbSqlCardDetail.Find(card, position);

			// Åñëè äåòàëü íå ÿâëÿåòñÿ ãàğàíòèéíîé, òî íè÷åãî äåëàòü íå ìîæåì
			if((bool)card_detail.GetData("ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ") == false)
				button_ok.Enabled = false;

			// Ïğîâåğÿåì êàêèå ïàğàìåòğû óñòàíîâëåíû êàêèå íåò
			if((long)card_detail.GetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ") != 0)
			{
				mistake_initiator = DbStaff.Find((long)card_detail.GetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			}
			if((long)card_detail.GetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ") != 0)
			{
				guaranty_type = DbGuarantyType.Find((long)card_detail.GetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ"));
			}

			mistake_text		= (string)card_detail.GetData("ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ");
		}

		private bool OkDetail()
		{
			if(DbSqlCardDetail.UpdateGuaranty(card_detail, guaranty_type, mistake_initiator, mistake_text) != true) return false;

			if(mistake_initiator != null)
			{
				card_detail.SetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(long)mistake_initiator.Code);
				card_detail.SetData("ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(string)(mistake_initiator.FirstName + " " + mistake_initiator.Name + " " + mistake_initiator.SecondName));
			}
			else
			{
				card_detail.SetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(long)0);
				card_detail.SetData("ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(string)"");
			}
			if(guaranty_type != null)
			{
				card_detail.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(long)guaranty_type.Code);
				card_detail.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(string)guaranty_type.Description);
			}
			else
			{
				card_detail.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(long)0);
				card_detail.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(string)"");
			}
			card_detail.SetData("ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ÄÅÒÀËÜ", (object)(string)mistake_text);
			return true;
		}

		// Ğàáîòà ñ ğàáîòàìè
		private void InitWork(DtCard card, long position)
		{
			// Ïîëó÷àåì ıëåìåíò ñ êîòîğûì ğàáîòàåì
			card_work = DbSqlCardWork.Find(card, (int)position);

			// Åñëè äåòàëü íå ÿâëÿåòñÿ ãàğàíòèéíîé, òî íè÷åãî äåëàòü íå ìîæåì
			if((bool)card_work.GetData("ÃÀĞÀÍÒÈß_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ") == false)
				button_ok.Enabled = false;

			// Ïğîâåğÿåì êàêèå ïàğàìåòğû óñòàíîâëåíû êàêèå íåò
			if((long)card_work.GetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ") != 0)
			{
				mistake_initiator = DbStaff.Find((long)card_work.GetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ"));
			}
			if((long)card_work.GetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ") != 0)
			{
				guaranty_type = DbGuarantyType.Find((long)card_work.GetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ"));
			}

			mistake_text		= (string)card_work.GetData("ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ");
		}

		private bool OkWork()
		{
			if(DbSqlCardWork.UpdateGuaranty(card_work, guaranty_type, mistake_initiator, mistake_text) != true) return false;

			if(mistake_initiator != null)
			{
				card_work.SetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(long)mistake_initiator.Code);
				card_work.SetData("ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(string)(mistake_initiator.FirstName + " " + mistake_initiator.Name + " " + mistake_initiator.SecondName));
			}
			else
			{
				card_work.SetData("ÍÀÊÎÑß×ÈË_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(long)0);
				card_work.SetData("ÍÀÊÎÑß×ÈË_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(string)"");
			}
			if(guaranty_type != null)
			{
				card_work.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(long)guaranty_type.Code);
				card_work.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(string)guaranty_type.Description);
			}
			else
			{
				card_work.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(long)0);
				card_work.SetData("ÃÀĞÀÍÒÈß_ÂÈÄ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(string)"");
			}
			card_work.SetData("ÊÎÑßÊ_ÊÀĞÒÎ×ÊÀ_ĞÀÁÎÒÀ", (object)(string)mistake_text);
			return true;
		}
	}
}
