using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStorageGroupTree.
	/// </summary>
	public class FormStorageGroupTree : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Button buttonUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbStorageGroup selectedStorageGroup = null;

		public FormStorageGroupTree()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStorageGroupTree));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 24);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(400, 240);
			this.treeView1.TabIndex = 0;
			this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			this.treeView1.DoubleClick += new EventHandler(this.treeView1_DoubleClick);
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(32, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 2;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(56, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 3;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// FormStorageGroupTree
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonUpdate,
																		  this.buttonChange,
																		  this.buttonNew,
																		  this.treeView1});
			this.Name = "FormStorageGroupTree";
			this.Text = "Складские группы";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление новой товарной группы
			TreeNode node = Db.GetItemSelected(treeView1);
			DbStorageGroup element = null;
			if(node != null) element = (DbStorageGroup)node.Tag;
			FormStorageGroup dialog = new FormStorageGroup(null, element);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if(node == null)
				treeView1.Nodes.Add(dialog.StorageGroup.TVItem);
			else
				node.Nodes.Add(dialog.StorageGroup.TVItem);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение существующего элемента
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			DbStorageGroup element = null;
			element = (DbStorageGroup)node.Tag;
			if(element == null) return;

			FormStorageGroup dialog = new FormStorageGroup(element, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.StorageGroup.SetTVItem(node);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка
			treeView1.Nodes.Clear();
			DbStorageGroup.FillTree(treeView1);
		}

		protected void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			DbStorageGroup element = (DbStorageGroup)e.Node.Tag;
			if(element.Expand) return;
			foreach(TreeNode node in e.Node.Nodes)
			{
				DbStorageGroup.FillTree(node);
			}
			element.Expand = true;
			element.SetTVItem(e.Node);
		}

		protected void treeView1_DoubleClick(object sender, EventArgs e)
		{
			// Изменение существующего элемента
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			DbStorageGroup element = null;
			element = (DbStorageGroup)node.Tag;
			if(element == null) return;

			selectedStorageGroup = element;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbStorageGroup SelectedStorageGroup
		{
			get
			{
				return selectedStorageGroup;
			}
		}
	}
}
