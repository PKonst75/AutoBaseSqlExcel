using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateStorageRequest.
	/// </summary>
	public class FormUpdateStorageRequest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_write;
		private System.Windows.Forms.Button button_close;

		DtStorageRequest request;

		public FormUpdateStorageRequest(DtStorageRequest source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ListViewItem item;
			// Äîáàâëÿåì ñòğî÷êè ñ ïàğàìåòğàìè
			// ÊÎÄ
			item = new ListViewItem();
			item.Text = "ÊÎÄ";
			item.Tag = "ÊÎÄ_ÇÀßÂÊÀ";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ÃÎÄ
			item = new ListViewItem();
			item.Text = "ÃÎÄ";
			item.Tag = "ÃÎÄ_ÇÀßÂÊÀ";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ÄÀÒÀ È ÂĞÅÌß
			item = new ListViewItem();
			item.Text = "ÄÀÒÀ È ÂĞÅÌß";
			item.Tag = "ÄÀÒÀ_ÇÀßÂÊÀ";
			item.BackColor = Color.LightGray;
			listView1.Items.Add(item);
			// ÑÊËÀÄÑÊÀß ÏÎÇÈÖÈß
			item = new ListViewItem();
			item.Text = "ÑÊËÀÄÑÊÀß ÏÎÇÈÖÈß";
			item.Tag = "ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ";
			listView1.Items.Add(item);
			// ÊÎËÈ×ÅÑÒÂÎ
			item = new ListViewItem();
			item.Text = "ÊÎËÈ×ÅÑÒÂÎ";
			item.Tag = "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ";
			listView1.Items.Add(item);
			// ÃÀĞÀÍÒÈß
			item = new ListViewItem();
			item.Text = "ÃÀĞÀÍÒÈß";
			item.Tag = "ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ";
			listView1.Items.Add(item);
			// ÒĞÅÁÓÅÌÀß ÄÀÒÀ
			item = new ListViewItem();
			item.Text = "ÒĞÅÁÓÅÌÀß ÄÀÒÀ";
			item.Tag = "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß";
			listView1.Items.Add(item);
			// ÊÀĞÒÎ×ÊÀ
			item = new ListViewItem();
			item.Text = "ÊÀĞÒÎ×ÊÀ";
			item.Tag = "ÊÀĞÒÎ×ÊÀ";
			listView1.Items.Add(item);
			// ÊÎÄ ÊÎÍÒĞÀÃÅÍÒÀ
			item = new ListViewItem();
			item.Text = "ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒÀ";
			item.Tag = "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ";
			listView1.Items.Add(item);
			// ÊÎÄ ÏÎÄÏÈÑÀÂØÅÃÎ ÇÀÊÀÇ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÊÎÄ ÏÎÄÏÈÑÀÂØÅÃÎ ÇÀßÂÊÓ";
			item.Tag = "ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ";
			listView1.Items.Add(item);
			// ÄÀÒÀ ÏÎÄÀ×È ÇÀßÂÊÀ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÄÀÒÀ È ÂĞÅÌß ÏÎÄÀ×È ÇÀßÂÊÈ";
			item.Tag = "ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À";
			listView1.Items.Add(item);
			// ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÏÎÄÏÈÑÀË ÏÎÄÀ×Ó ÇÀßÂÊÈ";
			item.Tag = "ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ";
			listView1.Items.Add(item);
			// ÄÀÒÀ ÂÛÏÎËÍÅÍÈß ÇÀßÂÊÀ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÄÀÒÀ È ÂĞÅÌß ÂÛÏÎËÍÅÍÈß ÇÀßÂÊÈ";
			item.Tag = "ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ";
			listView1.Items.Add(item);
			// ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÏÎÄÏÈÑÀË ÂÛÏÎËÍÅÍÈÅ ÇÀßÂÊÈ";
			item.Tag = "ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ";
			listView1.Items.Add(item);
			// ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ
			item = new ListViewItem();
			item.BackColor = Color.LightGray;
			item.Text = "ÏĞÅÄÏÎËÀÃÀÅÌÀß ÄÀÒÀ ÏÎÑÒÀÂÊÈ";
			item.Tag = "ÄÀÒÀ_ÏÎÑÒÀÂÊÈ";
			listView1.Items.Add(item);

			// Ïîëó÷àåì âõîäíûå äàííûå
			DbPartner partner		= null;
			DbStaff staff_give		= null;
			DbStaff staff_execute	= null;
			DtCard	card			= null;
			if(source == null)
			{
				request = new DtStorageRequest();
			}
			else
			{
				request = new DtStorageRequest(source);
				// Äîãğóçêà íåîáõîäèìûõ äëÿ ïîëíîãî îòîáğàæåíèÿ äàííûõ
				partner			= DbPartner.Find((long)request.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
				staff_give		= DbStaff.Find((long)request.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÏÎÄÀ×À_ÇÀßÂÊÀ"));
				staff_execute	= DbStaff.Find((long)request.GetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÂÛÏÎËÍÅÍÈÅ_ÇÀßÂÊÀ"));
				// Çàïğåò çàïèñè ıëåìåíòà, åñëè ıëåìåíò íå íîâûé
				if((long)request.GetData("ÊÎÄ_ÇÀßÂÊÀ") != 0)
					button_write.Enabled = false;
			}

			// Çàïîëíÿåì âõîäíûå äàííûå
			listView1.Items[0].SubItems.Add(request.GetData("ÊÎÄ_ÇÀßÂÊÀ").ToString());
			listView1.Items[1].SubItems.Add(request.GetData("ÃÎÄ_ÇÀßÂÊÀ").ToString());
			listView1.Items[2].SubItems.Add(request.GetData("ÄÀÒÀ_ÇÀßÂÊÀ").ToString());
			listView1.Items[3].SubItems.Add(request.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÑÊËÀÄ_ÄÅÒÀËÜ").ToString());
			listView1.Items[4].SubItems.Add(request.GetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ").ToString());
			// Ãàğàíòèÿ çàÿâêà
			if((bool)request.GetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ") == true)
				listView1.Items[5].SubItems.Add("ÄÀ");
			else
				listView1.Items[5].SubItems.Add("ÍÅÒ");

			if((bool)request.GetData("ÅÑÒÜ_ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß") == false)
				listView1.Items[6].SubItems.Add("");
			else
				listView1.Items[6].SubItems.Add(request.GetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß").ToString());
			// Äàííûå î êàğòî÷êå çàêàçà
			card	= DbSqlCard.Find((long)request.GetData("ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"), (int)request.GetData("ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
			if(card == null)
				listView1.Items[7].SubItems.Add("");
			else
			{
				listView1.Items[7].SubItems.Add(card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ").ToString() + " îò " + card.GetData("ÄÀÒÀ").ToString() + " / " + card.GetData("ÍÎÌÅĞ_ÍÀĞßÄ_ÊÀĞÒÎ×ÊÀ").ToString());
			}
			// Êîíåö äàííûõ î êàğòî÷êå çàêàçà
			if(partner == null)
				listView1.Items[8].SubItems.Add("");
			else
				listView1.Items[8].SubItems.Add(partner.Title);
			listView1.Items[9].SubItems.Add(request.GetData("ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ").ToString());
			if((bool)request.GetData("ÅÑÒÜ_ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À") == false)
				listView1.Items[10].SubItems.Add("");
			else
				listView1.Items[10].SubItems.Add(request.GetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÏÎÄÀ×À").ToString());
			if(staff_give == null)
				listView1.Items[11].SubItems.Add("");
			else
				listView1.Items[11].SubItems.Add(staff_give.Title);

			if((bool)request.GetData("ÅÑÒÜ_ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ") == false)
				listView1.Items[12].SubItems.Add("");
			else
				listView1.Items[12].SubItems.Add(request.GetData("ÄÀÒÀ_ÇÀßÂÊÀ_ÂÛÏÎËÍÅÍÈÅ").ToString());
			if(staff_execute == null)
				listView1.Items[13].SubItems.Add("");
			else
				listView1.Items[13].SubItems.Add(staff_execute.Title);
			if((bool)request.GetData("ÅÑÒÜ_ÄÀÒÀ_ÏÎÑÒÀÂÊÈ") == false)
				listView1.Items[14].SubItems.Add("");
			else
				listView1.Items[14].SubItems.Add(request.GetData("ÄÀÒÀ_ÏÎÑÒÀÂÊÈ").ToString());
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button_write = new System.Windows.Forms.Button();
			this.button_close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(424, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Ïàğàìåòğ";
			this.columnHeader1.Width = 142;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Çíà÷åíèå";
			this.columnHeader2.Width = 256;
			// 
			// button_write
			// 
			this.button_write.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_write.Location = new System.Drawing.Point(352, 248);
			this.button_write.Name = "button_write";
			this.button_write.TabIndex = 1;
			this.button_write.Text = "Çàïèñàòü";
			this.button_write.Click += new System.EventHandler(this.button_write_Click);
			// 
			// button_close
			// 
			this.button_close.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_close.Location = new System.Drawing.Point(280, 248);
			this.button_close.Name = "button_close";
			this.button_close.TabIndex = 2;
			this.button_close.Text = "Çàêğûòü";
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// FormUpdateStorageRequest
			// 
			this.AcceptButton = this.button_write;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button_close;
			this.ClientSize = new System.Drawing.Size(456, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_close,
																		  this.button_write,
																		  this.listView1});
			this.Name = "FormUpdateStorageRequest";
			this.Text = "Çàÿâêà íà ñêëàä";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Äâîéíîé ùåë÷åê íà êàêîì ëèáî ıëåìåíòå èíèöèèğóåò åãî ââîä èëè èçìåíåíèå
			DbPartner	partner;
			ListViewItem item = Db.GetItemSelected(listView1);
			string tag = (string)item.Tag;
			switch(tag)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					// Âûáîğ çàêàçûâàåìîé ïîçèöèè
					FormDetailStorageList dialog2 = new FormDetailStorageList(null, 1, null, null);
					if(dialog2.ShowDialog() != DialogResult.OK) return;
					DbDetailStorage detail = dialog2.SelectedDetailStorage;
					request.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÑÊËÀÄ_ÄÅÒÀËÜ", (object)detail.Code);
					item.SubItems[1].Text = detail.DetailCodeTxt + " / " + detail.DetailName;
					break;
				case "ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ":
					// Âûáîğ çàêàçûâàåìîãî êîëè÷åñòâà
					FormSelectString dialog1 = new FormSelectString("Êîëè÷åñòâî çàêàçà", "0");
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					request.SetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ", (object)dialog1.SelectedFloat);
					item.SubItems[1].Text = request.GetData("ÊÎËÈ×ÅÑÒÂÎ_ÑÊËÀÄ_ÄÅÒÀËÜ").ToString();
					break;
				case "ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ":
					// Îòìåòêà ãàğàíòèéíîñòè
					if(MessageBox.Show("Îòìåòèòü çàêàçûâàåìûå äåòàëè êàê ãàğàíòèéíûå?", "Çàïğîñ", MessageBoxButtons.YesNo) != DialogResult.Yes)
					{
						request.SetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ", (object)false);
						item.SubItems[1].Text = "ÍÅÒ";
					}
					else
					{
						request.SetData("ÃÀĞÀÍÒÈß_ÇÀßÂÊÀ", (object)true);
						item.SubItems[1].Text = "ÄÀ";
					}
					break;
				case "ÊÀĞÒÎ×ÊÀ":
					// Âûáîğ êàğòî÷êè
					FormManageCard dialog3 = new FormManageCard(Db.ClickType.Select, 0, null);
					if(dialog3.ShowDialog() != DialogResult.OK) return;
					DtCard card = dialog3.card_selected;
					request.SetData("ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ", (object)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"));
					request.SetData("ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ", (object)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
					request.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", (object)card.GetData("ÂËÀÄÅËÅÖ_ÊÀĞÒÎ×ÊÀ"));
					item.SubItems[1].Text = card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ").ToString() + " îò " + card.GetData("ÄÀÒÀ").ToString() + " / " + card.GetData("ÍÎÌÅĞ_ÍÀĞßÄ_ÊÀĞÒÎ×ÊÀ").ToString();
					partner	= DbPartner.Find((long)request.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ"));
					if(partner == null)
						listView1.Items[8].SubItems[1].Text = "";
					else
						listView1.Items[8].SubItems[1].Text = partner.Title;
					break;
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ":
					// Âûáîğ êîíòğàãåíòà
					FormPartnerList dialog4 = new FormPartnerList();
					if(dialog4.ShowDialog() != DialogResult.OK) return;
					partner = dialog4.Partner;
					request.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ", (object)partner.Code);
					if(partner == null)
						item.SubItems[1].Text = "";
					else
						item.SubItems[1].Text = partner.Title;
					break;
				case "ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß":
					// Âûáîğ æåëàåìîé äàòû èñïîëíåíèÿ
					FormSelectDate dialog5 = new FormSelectDate();
					if(dialog5.ShowDialog() != DialogResult.OK) return;
					request.SetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß", (object)dialog5.SelectedDate);
					item.SubItems[1].Text = request.GetData("ÒĞÅÁÓÅÌÀß_ÄÀÒÀ_ÈÑÏÎËÍÅÍÈß").ToString();
					break;
				default:
					break;
			}
		}

		private void button_write_Click(object sender, System.EventArgs e)
		{
			// Çàïèñûâàåì ââåäåííûå äàííûå
			// Çàïğîñ êîäà ïîäïèñàâøåãî çàÿâêó
			DbStaff staff = DbStaff.GetByESign("İëåêòğîííàÿ ïîäïèñü");
			if(staff == null) return;
			request.SetData("ÊÎÄ_ÏÎÄÏÈÑÀË_ÇÀßÂÊÀ", staff.Code);
			// Çàïèñü äàííûõ
			DtStorageRequest result = DbSqlStorageRequest.Insert(request);
			if(result == null) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_close_Click(object sender, System.EventArgs e)
		{
			// Çàêğûâàåì îêíî, áåç ïğèìåíåíèÿ èçìåíåíèé
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
