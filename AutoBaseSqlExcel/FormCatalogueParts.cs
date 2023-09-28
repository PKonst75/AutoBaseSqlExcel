using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCatalogueParts.
	/// </summary>
	public class FormCatalogueParts : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_new_group;
		private System.Windows.Forms.TreeView treeView_groups;
		private System.Windows.Forms.ContextMenu contextMenu_groups;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ListView listView_details;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.CheckBox checkBox_use_groups;

		TreeNode context_menu_node = null;
		Form outer_form				= null;
		bool outer_connection		= false;
		int form_type				= 0;

		public FormCatalogueParts(Form form, int type)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(type == 1 && form != null)
			{
				outer_form			= form; 
				outer_connection	= true;
				form_type			= type;
			}

			checkBox_use_groups.Checked = true;
			// ��������� ���������� ������ �����
			DbSqlCatalogueParts.SelectInTree(treeView_groups);
			// ��������� ���������� ����� �������
			DbSqlCatalogueParts.SelectInList(listView_details, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCatalogueParts));
			this.treeView_groups = new System.Windows.Forms.TreeView();
			this.button_new_group = new System.Windows.Forms.Button();
			this.contextMenu_groups = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.listView_details = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.checkBox_use_groups = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// treeView_groups
			// 
			this.treeView_groups.HideSelection = false;
			this.treeView_groups.ImageIndex = -1;
			this.treeView_groups.Location = new System.Drawing.Point(8, 40);
			this.treeView_groups.Name = "treeView_groups";
			this.treeView_groups.SelectedImageIndex = -1;
			this.treeView_groups.Size = new System.Drawing.Size(168, 272);
			this.treeView_groups.TabIndex = 0;
			this.treeView_groups.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_groups_MouseUp);
			this.treeView_groups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_groups_AfterSelect);
			// 
			// button_new_group
			// 
			this.button_new_group.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_group.Image")));
			this.button_new_group.Location = new System.Drawing.Point(8, 16);
			this.button_new_group.Name = "button_new_group";
			this.button_new_group.Size = new System.Drawing.Size(24, 23);
			this.button_new_group.TabIndex = 1;
			this.button_new_group.Click += new System.EventHandler(this.button_new_group_Click);
			// 
			// contextMenu_groups
			// 
			this.contextMenu_groups.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.menuItem1,
																							   this.menuItem2,
																							   this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "�������� ������";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "�������� ������������";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "�������� ������ � ������";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// listView_details
			// 
			this.listView_details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1});
			this.listView_details.Location = new System.Drawing.Point(192, 40);
			this.listView_details.Name = "listView_details";
			this.listView_details.Size = new System.Drawing.Size(424, 272);
			this.listView_details.TabIndex = 2;
			this.listView_details.View = System.Windows.Forms.View.Details;
			this.listView_details.DoubleClick += new System.EventHandler(this.listView_details_DoubleClick);
			this.listView_details.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_details_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "������������";
			this.columnHeader1.Width = 348;
			// 
			// checkBox_use_groups
			// 
			this.checkBox_use_groups.Location = new System.Drawing.Point(192, 0);
			this.checkBox_use_groups.Name = "checkBox_use_groups";
			this.checkBox_use_groups.Size = new System.Drawing.Size(144, 24);
			this.checkBox_use_groups.TabIndex = 3;
			this.checkBox_use_groups.Text = "���������� �� ������";
			this.checkBox_use_groups.CheckedChanged += new System.EventHandler(this.checkBox_use_groups_CheckedChanged);
			// 
			// FormCatalogueParts
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBox_use_groups,
																		  this.listView_details,
																		  this.button_new_group,
																		  this.treeView_groups});
			this.Name = "FormCatalogueParts";
			this.Text = "������� - �������� �����";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_group_Click(object sender, System.EventArgs e)
		{
			// ������� ����� ������ � �������� �������� ������
			// ���������� ������� ������, ������� ������ ������
			TreeNode node = Db.GetItemSelected(treeView_groups);
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// ����������� ������������ ������ ��������
			FormSelectString dialog = new FormSelectString("������������ ������", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// �������������� ����� �������
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("������������",(object)name);
			element.SetData("���_������",(object)code_group);
			element.SetData("����_������",(object)true);
			// �������� �������� ����� �������
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("�������� ����� ������");
			TreeNode new_node = null;
			if(node == null)
			{
				new_node = treeView_groups.Nodes.Add("����� �������");
			}
			else
			{
				new_node = node.Nodes.Add("����� �������");
			}
			if(new_node != null)
				element.SetTNode(new_node);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// ���������� ����� ������
			// ������� ����� ������ � �������� �������� ������
			// ���������� ������� ������, ������� ������ ������
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// ����������� ������������ ������ ��������
			FormSelectString dialog = new FormSelectString("������������ ������", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// �������������� ����� �������
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("������������",(object)name);
			element.SetData("���_������",(object)code_group);
			element.SetData("����_������",(object)true);
			// �������� �������� ����� �������
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("�������� ����� ������");
			TreeNode new_node = null;
			if(node == null)
			{
				new_node = treeView_groups.Nodes.Add("����� �������");
			}
			else
			{
				new_node = node.Nodes.Add("����� �������");
			}
			if(new_node != null)
				element.SetTNode(new_node);
		}

		private void treeView_groups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// �������� ���������� ������� �� ������� ��������� ����������� ����
				context_menu_node	= null;
				// �� ���������� ������ ������ ����� - ����������� ����
				// ��������� ����
				// ����� ������������
				// ��������� ��� �����������
				// �������� �� ����������
				string login = Form1.currentLogin.ToLower();
				if (login == "�������" || login == "�����" || login == "�����������"){}
				if (login == "�����"){}
				if (login == "�������"){}
				// ����������� ����, �� ������� ��������� ����������� ����
				context_menu_node	= Db.GetItemPosition(treeView_groups);
				// ����� ����
				contextMenu_groups.Show(treeView_groups, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// ���������� ����� ������
			// ������� ����� ������ � �������� �������� ������
			// ���������� ������� ������, ������� ������ ������
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code = (long)o;
				}
			}
			if(code == 0) return;
			DtCatalogueParts element = DbSqlCatalogueParts.Find(code);
			if(element == null) return;
			// ����������� ������������ ������ ��������
			FormSelectString dialog = new FormSelectString("������������ ������", (string)element.GetData("������������"));
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// �������������� ����� ������������
			element.SetData("������������",(object)name);
			// �������� �������� ����� �������
			if(DbSqlCatalogueParts.Update(element) == false) return;
			MessageBox.Show("�������� ������������ ������");
			element.SetTNode(node);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// ���������� � ��������� ������ ����� ������
			// ���������� ����� ������
			// ������� ����� ������ � �������� �������� ������
			// ���������� ������� ������, ������� ������ ������
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// ����������� ������������ ����� ������
			FormSelectString dialog = new FormSelectString("������������ ������", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// �������������� ����� �������
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("������������",(object)name);
			element.SetData("���_������",(object)code_group);
			element.SetData("����_������",(object)false);
			// �������� �������� ����� �������
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("�������� ����� ������");
			ListViewItem new_item = null;
			if(node != treeView_groups.SelectedNode)
			{
				return; // ������ � ������� �������� ������� ������ �� ������������
			}
			new_item = listView_details.Items.Add("����� �������");
			if(new_item != null)
				element.SetLVItem(new_item);
		}

		private void treeView_groups_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			listView_details.Items.Clear();
			// ���������� ������� ������, ������� ������ ������
			TreeNode node = Db.GetItemSelected(treeView_groups);
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// ����� ������ �������� ������ ��������� ���� �������������� ��� ����������
			DbSqlCatalogueParts.SelectInList(listView_details, code_group);
		}

		private void checkBox_use_groups_CheckedChanged(object sender, System.EventArgs e)
		{
			// ������������ ������ ������������/�������������� ������
			if(checkBox_use_groups.Checked)
			{
				// �������� ����� ������������� �����
				treeView_groups.CollapseAll();
				treeView_groups.Enabled		= true;
				button_new_group.Enabled	= true;
				// ��������� ��� ������ �������, ���������� �� �����
				listView_details.Items.Clear();
				DbSqlCatalogueParts.SelectInList(listView_details, 0);
			}
			else
			{
				// ��������� ����� ������������� �����
				treeView_groups.CollapseAll();
				treeView_groups.Enabled		= false;
				button_new_group.Enabled	= false;
				// ��������� ��� ������ �������, ���������� �� �����
				listView_details.Items.Clear();
				DbSqlCatalogueParts.SelectInList(listView_details);
			}
		}

		private void listView_details_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// ������ ������ �� �����, ���� �������� �� �������
			FormSelectString dialog;
			switch(e.Column)
			{
				case 0:
					// ����� �� �����
					dialog = new FormSelectString("������������ ������", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					string pattern = dialog.SelectedTextMask;
					if (pattern == "") return;
					listView_details.Items.Clear();
					DbSqlCatalogueParts.SelectInList(listView_details, pattern);
					return;
				default:
					return;
			}
		}

		private void listView_details_DoubleClick(object sender, System.EventArgs e)
		{
			// ���������� ������� �����, ������� ������ ������
			ListViewItem item = Db.GetItemSelected(listView_details);
			long code = 0L;
			if (item != null)
			{
				object o = item.Tag;
				if( o != null)
				{
					code = (long)o;
				}
			}
			if(code == 0) return;
			// ����� �������� � ���� ������
			DtCatalogueParts catalogue_parts = DbSqlCatalogueParts.Find(code);
			if(catalogue_parts == null) return;

			// ���������� ������� �� ���� �������� ����������
			if(outer_connection == false) return;
			if(outer_form == null) return;
			if(outer_form.IsDisposed == true) return;
			if(form_type == 1)
			{
				// ������ �� ������� � ��������
				FormCard form = (FormCard)outer_form;
				form.NewCatalogueParts(catalogue_parts);
				return;
			}
		}
	}
}
