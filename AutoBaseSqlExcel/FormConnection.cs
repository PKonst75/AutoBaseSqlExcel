using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormConnection.
	/// </summary>
	public class FormConnection : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxLogin;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxDatabase;
		private System.Windows.Forms.Button buttonOK;

		// Идинтификационные данные
		private string login = "";			// логин 		
		private string password = "";		// пароль
		private string database = "";		// база данных на SQL сервере
		private string server = "";			// SQL сервер

		private string conn_string = "";
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxServer;	// Строка присоединения к базе данных

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormConnection()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Значения по умолчанию
		//	server = "programm";
		//	database = "autonew";
		//	login = "Админ";
		//	password = "123";

			server = "";
			database = "";
			// Читаем из инициализационного файла
			server		= FileIni.GetParameter("base.ini", "#DATA_SERVER");
			database	= FileIni.GetParameter("base.ini", "#DATA_BASE");

			password	= "";
			login		= Form1.currentLogin;

			textBoxLogin.Text = login;
			textBoxPassword.Text = password;
			textBoxDatabase.Text = database;
			textBoxServer.Text = server;
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
			this.textBoxLogin = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxDatabase = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxServer = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxLogin
			// 
			this.textBoxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxLogin.Location = new System.Drawing.Point(168, 16);
			this.textBoxLogin.Name = "textBoxLogin";
			this.textBoxLogin.Size = new System.Drawing.Size(208, 23);
			this.textBoxLogin.TabIndex = 1;
			this.textBoxLogin.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(56, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Логин";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Пароль";
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(168, 48);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(208, 23);
			this.textBoxPassword.TabIndex = 2;
			this.textBoxPassword.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(56, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "База данных";
			// 
			// textBoxDatabase
			// 
			this.textBoxDatabase.Location = new System.Drawing.Point(168, 80);
			this.textBoxDatabase.Name = "textBoxDatabase";
			this.textBoxDatabase.Size = new System.Drawing.Size(208, 23);
			this.textBoxDatabase.TabIndex = 3;
			this.textBoxDatabase.Text = "";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(176, 144);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "Вход";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(56, 112);
			this.label4.Name = "label4";
			this.label4.TabIndex = 0;
			this.label4.Text = "Сервер";
			// 
			// textBoxServer
			// 
			this.textBoxServer.Location = new System.Drawing.Point(168, 112);
			this.textBoxServer.Name = "textBoxServer";
			this.textBoxServer.Size = new System.Drawing.Size(208, 23);
			this.textBoxServer.TabIndex = 4;
			this.textBoxServer.Text = "";
			// 
			// FormConnection
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(458, 175);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxServer,
																		  this.label4,
																		  this.buttonOK,
																		  this.textBoxDatabase,
																		  this.label3,
																		  this.textBoxPassword,
																		  this.label2,
																		  this.label1,
																		  this.textBoxLogin});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormConnection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Идентификационная информация прользователя базы данных";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			// Получение логина, пароля, имени базы данных
			login = textBoxLogin.Text;
			password = textBoxPassword.Text;
			database = textBoxDatabase.Text;
			server = textBoxServer.Text;

			// Проверка их целостности
			login.Trim();
			password.Trim();
			database.Trim();
			server.Trim();
			if(login.Length == 0) return;
			if(password.Length == 0) return;
			if(database.Length == 0) return;
			if(server.Length == 0) return;

			conn_string = "user id=" + login + ";";
			conn_string += "password=" + password + ";";
			conn_string += "initial catalog=" + database + ";";
			conn_string += "data source=" + server + ";";
			conn_string += "Connect Timeout=180;";
            conn_string += "Trusted_connection=false;";
            conn_string += "net=dbnmpntw;";

         //   MessageBox.Show(conn_string);

			// Успешно закрываем диалог
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public string ConnString
		{
			get
			{
				return conn_string;
			}
		}

		public string Login
		{
			get
			{
				return login;
			}
		}
	}
}
