using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailStoragePrice.
	/// </summary>
	public class FormDetailStoragePrice : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonRecomendPrice;
		private System.Windows.Forms.Button buttonDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.TextBox textBox = null;
		private ListViewItem textItem = null;

		public FormDetailStoragePrice(ListView list)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			foreach(ListViewItem item in list.Items)
			{
				DbDetailIncom element = (DbDetailIncom)item.Tag;
				if(element != null)
				{
					if((element.Adding || element.Changed) && (element.Del == false))
					{
						DrDetailStoragePrice elem = new DrDetailStoragePrice(element);
						listView1.Items.Add(elem.LVItem);
					}
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailStoragePrice));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonRecomendPrice = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(696, 216);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Складская позиция";
			this.columnHeader1.Width = 275;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Количество";
			this.columnHeader2.Width = 93;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Цена";
			this.columnHeader3.Width = 104;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена вход";
			this.columnHeader4.Width = 84;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цена рекоменд.";
			this.columnHeader5.Width = 97;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.Location = new System.Drawing.Point(312, 248);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "ОК";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonRecomendPrice
			// 
			this.buttonRecomendPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonRecomendPrice.Image")));
			this.buttonRecomendPrice.Location = new System.Drawing.Point(8, 0);
			this.buttonRecomendPrice.Name = "buttonRecomendPrice";
			this.buttonRecomendPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonRecomendPrice.TabIndex = 2;
			this.buttonRecomendPrice.Click += new System.EventHandler(this.buttonRecomendPrice_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(32, 0);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// FormDetailStoragePrice
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(712, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDelete,
																		  this.buttonRecomendPrice,
																		  this.buttonOK,
																		  this.listView1});
			this.Name = "FormDetailStoragePrice";
			this.Text = "Цены на складские позиции";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			bool flag = true;
			// Применяем сделанные изменения
			foreach(ListViewItem item in listView1.Items)
			{
				DrDetailStoragePrice element = (DrDetailStoragePrice)item.Tag;
				if(element != null)
				{
					DbDetailStorage elem = element.DetailStorage;
					if(elem != null)
					{
						if(elem.WritePrice() != true) flag = false;
					}
				}
				if(flag == true)
				{
					this.Close();
				}
			}
		}

		private void buttonRecomendPrice_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем для выбранных позиций рекомендованные цены
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DrDetailStoragePrice elem	= (DrDetailStoragePrice)item.Tag;
				elem.StoragePrice			= elem.RecomendPrice;
				elem.SetLVItem(item);
			}
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Удалить элементы из списка
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				item.Remove();
			}
		}

		private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			ListViewItem item		= Db.GetItemSelected(listView1);
			DrDetailStoragePrice	element = (DrDetailStoragePrice)item.Tag;
			if(element == null)
				return;
			DbDetailStorage elem	= element.DetailStorage;
			if(elem == null)
				return;

			// Работает только при  нажатии ENTER
			if(e.KeyCode == Keys.Enter)
			{
				textBox = Db.MakeBox(this, item, 2);
				TextBoxInit(elem.PriceTxt);
				textItem = item;
			}
		}

		public void TextBoxInit(string text)
		{
			textBox.MouseDown += new MouseEventHandler(this.textBox_MouseDown);
			textBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
			textBox.Text = text;
			textBox.SelectAll();
		}

		public void TextBoxDispose()
		{
			this.Controls.Remove(textBox);
			textBox.Dispose();
			textBox = null;
			listView1.Focus();
			textItem = null;
			return;
		}

		protected void textBox_MouseDown(object sender, MouseEventArgs e)
		{
			TextBoxDispose();
			return;
		}

		protected void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			
			string text				= textBox.Text;
			text = text.Trim();
			ListViewItem item		= textItem;
			DrDetailStoragePrice	element = (DrDetailStoragePrice)textItem.Tag;
			if(element == null)
				TextBoxDispose();
			DbDetailStorage elem	= element.DetailStorage;
			if(elem == null)
				TextBoxDispose();
			
			if(e.KeyCode == Keys.Enter)
			{
				elem.PriceTxt = text;
				if(Db.ShowFaults())
				{
					TextBoxDisposeNotFocus();
					return;
				}
				element.SetLVItem(textItem);
				TextBoxDispose();
				return;
			}
			if(e.KeyCode == Keys.Escape)
			{
				TextBoxDispose();
				return;
			}
		}

		public void TextBoxDisposeNotFocus()
		{
			this.Controls.Remove(textBox);
			textBox.Dispose();
			textBox = null;
			textItem = null;
			return;
		}
	}
}
