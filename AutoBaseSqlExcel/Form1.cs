using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		// ����������� ����������
		private ArrayList info_window_workend	= null;

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private FormAutoTypeList autoTypeList = null;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.MenuItem menuItem41;

		public static System.Data.SqlClient.SqlConnection connection = null;
		private static DbStaff currentUser = null;
		public static string currentLogin = "";
		public static FormSelectionKladr kladr_form = null;

		public static System.Threading.Timer timer = null;
		public static System.Threading.TimerCallback timerDelegate = null;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem menuItem45;
		private System.Windows.Forms.MenuItem menuItem46;
		private System.Windows.Forms.MenuItem menuItem47;
		private System.Windows.Forms.MenuItem menuItem48;
		private System.Windows.Forms.MenuItem menuItem49;
		private System.Windows.Forms.MenuItem menuItem50;
		private System.Windows.Forms.MenuItem menuItem51;
		private System.Windows.Forms.MenuItem menuItem52;
		private System.Windows.Forms.MenuItem menuItem53;
		private System.Windows.Forms.MenuItem menuItem54;
		private System.Windows.Forms.MenuItem menuItem55;
		private System.Windows.Forms.MenuItem menuItem56;
		private System.Windows.Forms.MenuItem menuItem57;
		private System.Windows.Forms.MenuItem menuItem58;
		public static bool reconnect = false;
		private System.Windows.Forms.MenuItem menuItem59;
		private System.Windows.Forms.MenuItem menuItem60;
		private System.Windows.Forms.MenuItem menuItem61;
		private System.Windows.Forms.MenuItem menuItem62;
		private System.Windows.Forms.MenuItem menuItem64;
		private System.Windows.Forms.MenuItem menuItem65;
		private System.Windows.Forms.MenuItem menuItem63;
		private System.Windows.Forms.MenuItem menuItem66;
		private System.Windows.Forms.MenuItem menuItem67;
		private System.Windows.Forms.MenuItem menuItem68;
		private System.Windows.Forms.MenuItem menuItem69;
		private System.Windows.Forms.MenuItem menuItem70;
		private System.Windows.Forms.MenuItem menuItem71;
		private System.Windows.Forms.MenuItem menuItem72;
		private System.Windows.Forms.MenuItem menuItem73;
		private System.Windows.Forms.MenuItem menuItem74;
		private System.Windows.Forms.MenuItem menuItem75;
		private System.Windows.Forms.Button button_manage_card;
		private System.Windows.Forms.MenuItem menuItem76;
		private System.Windows.Forms.MenuItem menuItem77;
		private System.Windows.Forms.MenuItem menuItem78;
		private System.Windows.Forms.MenuItem menuItem79;
		private System.Windows.Forms.MenuItem menuItem80;
		private System.Windows.Forms.MenuItem menuItem81;
		private System.Windows.Forms.MenuItem menuItem82;
		private System.Windows.Forms.MenuItem menuItem83;
		private System.Windows.Forms.Button button_manage_partner;
		private System.Windows.Forms.MenuItem menuItem84;
		private System.Windows.Forms.MenuItem menuItem85;
		private System.Windows.Forms.MenuItem menuItem86;
		private System.Windows.Forms.MenuItem menuItem87;
		private System.Windows.Forms.MenuItem menuItem88;
		private System.Windows.Forms.Button button_auto_sell;
		private System.Windows.Forms.Button button_works;
		private System.Windows.Forms.MenuItem menuItem89;
		private System.Windows.Forms.MenuItem menuItem90;
		private System.Windows.Forms.MenuItem menuItem91;
		private System.Windows.Forms.MenuItem menuItem92;
		private System.Windows.Forms.MenuItem menuItem93;
		private System.Windows.Forms.MenuItem menuItem94;
		private System.Windows.Forms.MenuItem menuItem95;
		private System.Windows.Forms.MenuItem menuItem96;
		private System.Windows.Forms.Button button_timer_workend;
		private System.Windows.Forms.Timer timer_workend;
		private System.Windows.Forms.MenuItem menuItem97;
		private System.Windows.Forms.MenuItem menuItem98;
		private System.Windows.Forms.MenuItem menuItem99;
		private System.Windows.Forms.MenuItem menuItem100;
		private System.Windows.Forms.MenuItem menuItem101;
		private System.Windows.Forms.MenuItem menuItem102;
		private System.Windows.Forms.MenuItem menuItem103;
		private System.Windows.Forms.MenuItem menuItem104;
		private System.Windows.Forms.MenuItem menuItem105;
		private System.Windows.Forms.MenuItem menuItem106;
		private System.Windows.Forms.Button button_casher;
		private System.Windows.Forms.Button button_guaranty;
		private System.Windows.Forms.Button button_supervisor;
		private System.Windows.Forms.MenuItem menuItem107;
		private System.Windows.Forms.MenuItem menuItem108;
		private System.Windows.Forms.MenuItem menuItem109;
		private System.Windows.Forms.MenuItem menuItem110;
		private System.Windows.Forms.Button button_pos_timer;
		private System.Windows.Forms.Button button_exit;
		private System.Windows.Forms.Button button_auto_list;
		private System.Windows.Forms.Button button_cashless;
		private System.Windows.Forms.Button client_calls;
		private System.Windows.Forms.MenuItem menuItem111;
		private System.Windows.Forms.MenuItem menuItem112;
		private System.Windows.Forms.Button button_incom_calls;
		private System.Windows.Forms.MenuItem menuItem113;
		private System.Windows.Forms.MenuItem menuItem114;
		private System.Windows.Forms.Button button_manage_cardrate;
		private System.Windows.Forms.MenuItem menuItem115;
		private System.Windows.Forms.MenuItem menuItem116;
		private System.Windows.Forms.MenuItem menuItem117;
        private MenuItem menuItem118;
        private MenuItem menuItem119;
        private MenuItem menuItem120;
        private MenuItem menuItem121;
        private MenuItem menuItem122;
        private MenuItem menuItem123;
        private MenuItem menuItem124;
        private MenuItem menuItem125;
        private MenuItem menuItem126;
        private MenuItem menuItem127;
        private MenuItem menuItem128;
        public static bool reportPassChecked = false;

		public Form1()
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
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem67 = new System.Windows.Forms.MenuItem();
            this.menuItem68 = new System.Windows.Forms.MenuItem();
            this.menuItem69 = new System.Windows.Forms.MenuItem();
            this.menuItem80 = new System.Windows.Forms.MenuItem();
            this.menuItem81 = new System.Windows.Forms.MenuItem();
            this.menuItem72 = new System.Windows.Forms.MenuItem();
            this.menuItem73 = new System.Windows.Forms.MenuItem();
            this.menuItem74 = new System.Windows.Forms.MenuItem();
            this.menuItem75 = new System.Windows.Forms.MenuItem();
            this.menuItem89 = new System.Windows.Forms.MenuItem();
            this.menuItem90 = new System.Windows.Forms.MenuItem();
            this.menuItem101 = new System.Windows.Forms.MenuItem();
            this.menuItem76 = new System.Windows.Forms.MenuItem();
            this.menuItem77 = new System.Windows.Forms.MenuItem();
            this.menuItem79 = new System.Windows.Forms.MenuItem();
            this.menuItem92 = new System.Windows.Forms.MenuItem();
            this.menuItem82 = new System.Windows.Forms.MenuItem();
            this.menuItem83 = new System.Windows.Forms.MenuItem();
            this.menuItem95 = new System.Windows.Forms.MenuItem();
            this.menuItem104 = new System.Windows.Forms.MenuItem();
            this.menuItem117 = new System.Windows.Forms.MenuItem();
            this.menuItem126 = new System.Windows.Forms.MenuItem();
            this.menuItem85 = new System.Windows.Forms.MenuItem();
            this.menuItem86 = new System.Windows.Forms.MenuItem();
            this.menuItem96 = new System.Windows.Forms.MenuItem();
            this.menuItem97 = new System.Windows.Forms.MenuItem();
            this.menuItem87 = new System.Windows.Forms.MenuItem();
            this.menuItem93 = new System.Windows.Forms.MenuItem();
            this.menuItem98 = new System.Windows.Forms.MenuItem();
            this.menuItem99 = new System.Windows.Forms.MenuItem();
            this.menuItem107 = new System.Windows.Forms.MenuItem();
            this.menuItem111 = new System.Windows.Forms.MenuItem();
            this.menuItem112 = new System.Windows.Forms.MenuItem();
            this.menuItem115 = new System.Windows.Forms.MenuItem();
            this.menuItem116 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem63 = new System.Windows.Forms.MenuItem();
            this.menuItem71 = new System.Windows.Forms.MenuItem();
            this.menuItem78 = new System.Windows.Forms.MenuItem();
            this.menuItem84 = new System.Windows.Forms.MenuItem();
            this.menuItem88 = new System.Windows.Forms.MenuItem();
            this.menuItem91 = new System.Windows.Forms.MenuItem();
            this.menuItem94 = new System.Windows.Forms.MenuItem();
            this.menuItem105 = new System.Windows.Forms.MenuItem();
            this.menuItem106 = new System.Windows.Forms.MenuItem();
            this.menuItem108 = new System.Windows.Forms.MenuItem();
            this.menuItem109 = new System.Windows.Forms.MenuItem();
            this.menuItem110 = new System.Windows.Forms.MenuItem();
            this.menuItem113 = new System.Windows.Forms.MenuItem();
            this.menuItem119 = new System.Windows.Forms.MenuItem();
            this.menuItem120 = new System.Windows.Forms.MenuItem();
            this.menuItem121 = new System.Windows.Forms.MenuItem();
            this.menuItem122 = new System.Windows.Forms.MenuItem();
            this.menuItem124 = new System.Windows.Forms.MenuItem();
            this.menuItem125 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem123 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem66 = new System.Windows.Forms.MenuItem();
            this.menuItem70 = new System.Windows.Forms.MenuItem();
            this.menuItem127 = new System.Windows.Forms.MenuItem();
            this.menuItem64 = new System.Windows.Forms.MenuItem();
            this.menuItem65 = new System.Windows.Forms.MenuItem();
            this.menuItem100 = new System.Windows.Forms.MenuItem();
            this.menuItem102 = new System.Windows.Forms.MenuItem();
            this.menuItem103 = new System.Windows.Forms.MenuItem();
            this.menuItem114 = new System.Windows.Forms.MenuItem();
            this.menuItem118 = new System.Windows.Forms.MenuItem();
            this.button_manage_card = new System.Windows.Forms.Button();
            this.button_manage_partner = new System.Windows.Forms.Button();
            this.button_auto_sell = new System.Windows.Forms.Button();
            this.button_works = new System.Windows.Forms.Button();
            this.button_timer_workend = new System.Windows.Forms.Button();
            this.timer_workend = new System.Windows.Forms.Timer(this.components);
            this.button_casher = new System.Windows.Forms.Button();
            this.button_guaranty = new System.Windows.Forms.Button();
            this.button_supervisor = new System.Windows.Forms.Button();
            this.button_pos_timer = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_auto_list = new System.Windows.Forms.Button();
            this.button_cashless = new System.Windows.Forms.Button();
            this.client_calls = new System.Windows.Forms.Button();
            this.button_incom_calls = new System.Windows.Forms.Button();
            this.button_manage_cardrate = new System.Windows.Forms.Button();
            this.menuItem128 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem4,
            this.menuItem28,
            this.menuItem32,
            this.menuItem37,
            this.menuItem40,
            this.menuItem42,
            this.menuItem51,
            this.menuItem53,
            this.menuItem57,
            this.menuItem64,
            this.menuItem102});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            this.menuItem17,
            this.menuItem18,
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem23,
            this.menuItem24,
            this.menuItem25,
            this.menuItem26,
            this.menuItem27,
            this.menuItem30,
            this.menuItem45,
            this.menuItem59});
            this.menuItem1.Text = "�����������";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "����� �����������";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "������������";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.Text = "����������";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "������� �����";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 4;
            this.menuItem8.Text = "�����������";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 5;
            this.menuItem12.Text = "������";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 6;
            this.menuItem13.Text = "�������������";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 7;
            this.menuItem14.Text = "�����";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 8;
            this.menuItem17.Text = "��������";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 9;
            this.menuItem18.Text = "������-������������� �����������";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 10;
            this.menuItem20.Text = "��������� ������";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 11;
            this.menuItem21.Text = "�������";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 12;
            this.menuItem22.Text = "������ �����������";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 13;
            this.menuItem23.Text = "���� ��������";
            this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 14;
            this.menuItem24.Text = "���� ���������";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 15;
            this.menuItem25.Text = "���� ������� �������";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 16;
            this.menuItem26.Text = "���������";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 17;
            this.menuItem27.Text = "������������";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 18;
            this.menuItem30.Text = "�����";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 19;
            this.menuItem45.Text = "����";
            this.menuItem45.Click += new System.EventHandler(this.menuItem45_Click);
            // 
            // menuItem59
            // 
            this.menuItem59.Index = 20;
            this.menuItem59.Text = "��������� �����";
            this.menuItem59.Click += new System.EventHandler(this.menuItem59_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5,
            this.menuItem9,
            this.menuItem11,
            this.menuItem15,
            this.menuItem16,
            this.menuItem19});
            this.menuItem4.Text = "�������";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "�������� �������";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10});
            this.menuItem9.Text = "����� �������";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "����������";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.Text = "������";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 3;
            this.menuItem15.Text = "��������� ������";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 4;
            this.menuItem16.Text = "����������";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 5;
            this.menuItem19.Text = "����� (������������)";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 2;
            this.menuItem28.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem29,
            this.menuItem31,
            this.menuItem67,
            this.menuItem68,
            this.menuItem72,
            this.menuItem73,
            this.menuItem74,
            this.menuItem76,
            this.menuItem82,
            this.menuItem85,
            this.menuItem87,
            this.menuItem93,
            this.menuItem98,
            this.menuItem107,
            this.menuItem111,
            this.menuItem115,
            this.menuItem116});
            this.menuItem28.Text = "����������";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 0;
            this.menuItem29.Text = "������";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 1;
            this.menuItem31.Text = "������";
            this.menuItem31.Click += new System.EventHandler(this.menuItem31_Click);
            // 
            // menuItem67
            // 
            this.menuItem67.Index = 2;
            this.menuItem67.Text = "�����-������";
            this.menuItem67.Click += new System.EventHandler(this.menuItem67_Click);
            // 
            // menuItem68
            // 
            this.menuItem68.Index = 3;
            this.menuItem68.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem69,
            this.menuItem80,
            this.menuItem81});
            this.menuItem68.Text = "�����������";
            // 
            // menuItem69
            // 
            this.menuItem69.Index = 0;
            this.menuItem69.Text = "��������";
            this.menuItem69.Click += new System.EventHandler(this.menuItem69_Click);
            // 
            // menuItem80
            // 
            this.menuItem80.Index = 1;
            this.menuItem80.Text = "������";
            this.menuItem80.Click += new System.EventHandler(this.menuItem80_Click);
            // 
            // menuItem81
            // 
            this.menuItem81.Index = 2;
            this.menuItem81.Text = "�����";
            this.menuItem81.Click += new System.EventHandler(this.menuItem81_Click);
            // 
            // menuItem72
            // 
            this.menuItem72.Index = 4;
            this.menuItem72.Text = "�����������";
            this.menuItem72.Click += new System.EventHandler(this.menuItem72_Click);
            // 
            // menuItem73
            // 
            this.menuItem73.Index = 5;
            this.menuItem73.Text = "���������� �����";
            this.menuItem73.Click += new System.EventHandler(this.menuItem73_Click);
            // 
            // menuItem74
            // 
            this.menuItem74.Index = 6;
            this.menuItem74.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem75,
            this.menuItem89,
            this.menuItem101});
            this.menuItem74.Text = "�����";
            // 
            // menuItem75
            // 
            this.menuItem75.Index = 0;
            this.menuItem75.Text = "������";
            this.menuItem75.Click += new System.EventHandler(this.menuItem75_Click);
            // 
            // menuItem89
            // 
            this.menuItem89.Index = 1;
            this.menuItem89.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem90});
            this.menuItem89.Text = "������";
            // 
            // menuItem90
            // 
            this.menuItem90.Index = 0;
            this.menuItem90.Text = "����� � �������";
            this.menuItem90.Click += new System.EventHandler(this.menuItem90_Click);
            // 
            // menuItem101
            // 
            this.menuItem101.Index = 2;
            this.menuItem101.Text = "������";
            this.menuItem101.Click += new System.EventHandler(this.menuItem101_Click);
            // 
            // menuItem76
            // 
            this.menuItem76.Index = 7;
            this.menuItem76.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem77,
            this.menuItem79,
            this.menuItem92});
            this.menuItem76.Text = "����������";
            // 
            // menuItem77
            // 
            this.menuItem77.Index = 0;
            this.menuItem77.Text = "������";
            this.menuItem77.Click += new System.EventHandler(this.menuItem77_Click);
            // 
            // menuItem79
            // 
            this.menuItem79.Index = 1;
            this.menuItem79.Text = "��������� �����������";
            this.menuItem79.Click += new System.EventHandler(this.menuItem79_Click);
            // 
            // menuItem92
            // 
            this.menuItem92.Index = 2;
            this.menuItem92.Text = "�����";
            this.menuItem92.Click += new System.EventHandler(this.menuItem92_Click);
            // 
            // menuItem82
            // 
            this.menuItem82.Index = 8;
            this.menuItem82.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem83,
            this.menuItem95,
            this.menuItem104,
            this.menuItem117,
            this.menuItem126});
            this.menuItem82.Text = "������";
            // 
            // menuItem83
            // 
            this.menuItem83.Index = 0;
            this.menuItem83.Text = "��������� �� ������";
            this.menuItem83.Click += new System.EventHandler(this.menuItem83_Click);
            // 
            // menuItem95
            // 
            this.menuItem95.Index = 1;
            this.menuItem95.Text = "��������� �� ���������";
            this.menuItem95.Click += new System.EventHandler(this.menuItem95_Click);
            // 
            // menuItem104
            // 
            this.menuItem104.Index = 2;
            this.menuItem104.Text = "������ �� �������������";
            this.menuItem104.Click += new System.EventHandler(this.menuItem104_Click);
            // 
            // menuItem117
            // 
            this.menuItem117.Index = 3;
            this.menuItem117.Text = "������� ��������";
            this.menuItem117.Click += new System.EventHandler(this.menuItem117_Click);
            // 
            // menuItem126
            // 
            this.menuItem126.Index = 4;
            this.menuItem126.Text = "�������� �������";
            this.menuItem126.Click += new System.EventHandler(this.menuItem126_Click);
            // 
            // menuItem85
            // 
            this.menuItem85.Index = 9;
            this.menuItem85.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem86,
            this.menuItem96,
            this.menuItem97});
            this.menuItem85.Text = "Excel";
            // 
            // menuItem86
            // 
            this.menuItem86.Index = 0;
            this.menuItem86.Text = "�/�";
            this.menuItem86.Click += new System.EventHandler(this.menuItem86_Click);
            // 
            // menuItem96
            // 
            this.menuItem96.Index = 1;
            this.menuItem96.Text = "��������� �� ������";
            this.menuItem96.Click += new System.EventHandler(this.menuItem96_Click);
            // 
            // menuItem97
            // 
            this.menuItem97.Index = 2;
            this.menuItem97.Text = "��������� ������";
            this.menuItem97.Click += new System.EventHandler(this.menuItem97_Click);
            // 
            // menuItem87
            // 
            this.menuItem87.Index = 10;
            this.menuItem87.Text = "��������";
            this.menuItem87.Click += new System.EventHandler(this.menuItem87_Click);
            // 
            // menuItem93
            // 
            this.menuItem93.Index = 11;
            this.menuItem93.Text = "������";
            this.menuItem93.Click += new System.EventHandler(this.menuItem93_Click);
            // 
            // menuItem98
            // 
            this.menuItem98.Index = 12;
            this.menuItem98.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem99});
            this.menuItem98.Text = "��������";
            // 
            // menuItem99
            // 
            this.menuItem99.Index = 0;
            this.menuItem99.Text = "��������";
            this.menuItem99.Click += new System.EventHandler(this.menuItem99_Click);
            // 
            // menuItem107
            // 
            this.menuItem107.Index = 13;
            this.menuItem107.Text = "������������� � �����������";
            this.menuItem107.Click += new System.EventHandler(this.menuItem107_Click);
            // 
            // menuItem111
            // 
            this.menuItem111.Index = 14;
            this.menuItem111.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem112});
            this.menuItem111.Text = "������ ������� ���������";
            // 
            // menuItem112
            // 
            this.menuItem112.Index = 0;
            this.menuItem112.Text = "������� ����� ������";
            this.menuItem112.Click += new System.EventHandler(this.menuItem112_Click);
            // 
            // menuItem115
            // 
            this.menuItem115.Index = 15;
            this.menuItem115.Text = "������������";
            this.menuItem115.Click += new System.EventHandler(this.menuItem115_Click);
            // 
            // menuItem116
            // 
            this.menuItem116.Index = 16;
            this.menuItem116.Text = "������ �� �� �� ������";
            this.menuItem116.Click += new System.EventHandler(this.menuItem116_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 3;
            this.menuItem32.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem33,
            this.menuItem34,
            this.menuItem35,
            this.menuItem36,
            this.menuItem46,
            this.menuItem47,
            this.menuItem48,
            this.menuItem49,
            this.menuItem50,
            this.menuItem60,
            this.menuItem61,
            this.menuItem63,
            this.menuItem71,
            this.menuItem78,
            this.menuItem84,
            this.menuItem88,
            this.menuItem91,
            this.menuItem94,
            this.menuItem105,
            this.menuItem106,
            this.menuItem108,
            this.menuItem109,
            this.menuItem110,
            this.menuItem113,
            this.menuItem119,
            this.menuItem120,
            this.menuItem122,
            this.menuItem124,
            this.menuItem125});
            this.menuItem32.Text = "Debug";
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 0;
            this.menuItem33.Text = "Debug1";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 1;
            this.menuItem34.Text = "Debug2";
            this.menuItem34.Click += new System.EventHandler(this.menuItem34_Click);
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 2;
            this.menuItem35.Text = "Debug3";
            this.menuItem35.Click += new System.EventHandler(this.menuItem35_Click);
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 3;
            this.menuItem36.Text = "Debug4";
            this.menuItem36.Click += new System.EventHandler(this.menuItem36_Click);
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 4;
            this.menuItem46.Text = "��������� �������";
            this.menuItem46.Click += new System.EventHandler(this.menuItem46_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 5;
            this.menuItem47.Text = "��������� ������";
            this.menuItem47.Click += new System.EventHandler(this.menuItem47_Click);
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 6;
            this.menuItem48.Text = "�����";
            this.menuItem48.Click += new System.EventHandler(this.menuItem48_Click);
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 7;
            this.menuItem49.Text = "��� ������������";
            this.menuItem49.Click += new System.EventHandler(this.menuItem49_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 8;
            this.menuItem50.Text = "�������";
            this.menuItem50.Click += new System.EventHandler(this.menuItem50_Click);
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 9;
            this.menuItem60.Text = "�����";
            this.menuItem60.Click += new System.EventHandler(this.menuItem60_Click);
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 10;
            this.menuItem61.Text = "�����-�����";
            this.menuItem61.Click += new System.EventHandler(this.menuItem61_Click);
            // 
            // menuItem63
            // 
            this.menuItem63.Index = 11;
            this.menuItem63.Text = "����������� ����������";
            this.menuItem63.Click += new System.EventHandler(this.menuItem63_Click);
            // 
            // menuItem71
            // 
            this.menuItem71.Index = 12;
            this.menuItem71.Text = "Test";
            this.menuItem71.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // menuItem78
            // 
            this.menuItem78.Index = 13;
            this.menuItem78.Text = "������";
            this.menuItem78.Click += new System.EventHandler(this.menuItem78_Click);
            // 
            // menuItem84
            // 
            this.menuItem84.Index = 14;
            this.menuItem84.Text = "View Test";
            this.menuItem84.Click += new System.EventHandler(this.menuItem84_Click);
            // 
            // menuItem88
            // 
            this.menuItem88.Index = 15;
            this.menuItem88.Text = "Debug 5";
            this.menuItem88.Click += new System.EventHandler(this.menuItem88_Click);
            // 
            // menuItem91
            // 
            this.menuItem91.Index = 16;
            this.menuItem91.Text = "Serial";
            this.menuItem91.Click += new System.EventHandler(this.menuItem91_Click);
            // 
            // menuItem94
            // 
            this.menuItem94.Index = 17;
            this.menuItem94.Text = "������������ ��������";
            this.menuItem94.Click += new System.EventHandler(this.menuItem94_Click);
            // 
            // menuItem105
            // 
            this.menuItem105.Index = 18;
            this.menuItem105.Text = "��������� ���������� ����������";
            this.menuItem105.Click += new System.EventHandler(this.menuItem105_Click);
            // 
            // menuItem106
            // 
            this.menuItem106.Index = 19;
            this.menuItem106.Text = "�������� �������";
            this.menuItem106.Click += new System.EventHandler(this.menuItem106_Click);
            // 
            // menuItem108
            // 
            this.menuItem108.Index = 20;
            this.menuItem108.Text = "��� �������";
            this.menuItem108.Click += new System.EventHandler(this.menuItem108_Click);
            // 
            // menuItem109
            // 
            this.menuItem109.Index = 21;
            this.menuItem109.Text = "���� 555";
            this.menuItem109.Click += new System.EventHandler(this.menuItem109_Click);
            // 
            // menuItem110
            // 
            this.menuItem110.Index = 22;
            this.menuItem110.Text = "������ �� ������";
            this.menuItem110.Click += new System.EventHandler(this.menuItem110_Click);
            // 
            // menuItem113
            // 
            this.menuItem113.Index = 23;
            this.menuItem113.Text = "COM ����";
            this.menuItem113.Click += new System.EventHandler(this.menuItem113_Click);
            // 
            // menuItem119
            // 
            this.menuItem119.Index = 24;
            this.menuItem119.Text = "������� �����-�������";
            this.menuItem119.Click += new System.EventHandler(this.menuItem119_Click);
            // 
            // menuItem120
            // 
            this.menuItem120.Index = 25;
            this.menuItem120.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem121});
            this.menuItem120.Text = "���� ���������� �� ��";
            // 
            // menuItem121
            // 
            this.menuItem121.Index = 0;
            this.menuItem121.Text = "����";
            this.menuItem121.Click += new System.EventHandler(this.menuItem121_Click);
            // 
            // menuItem122
            // 
            this.menuItem122.Index = 26;
            this.menuItem122.Text = "�������������� ����";
            this.menuItem122.Click += new System.EventHandler(this.menuItem122_Click);
            // 
            // menuItem124
            // 
            this.menuItem124.Index = 27;
            this.menuItem124.Text = "DataGrid";
            this.menuItem124.Click += new System.EventHandler(this.menuItem124_Click);
            // 
            // menuItem125
            // 
            this.menuItem125.Index = 28;
            this.menuItem125.Text = "��������������������";
            this.menuItem125.Click += new System.EventHandler(this.menuItem125_Click);
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 4;
            this.menuItem37.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem38,
            this.menuItem39,
            this.menuItem44});
            this.menuItem37.Text = "������������";
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 0;
            this.menuItem38.Text = "����������� �����";
            this.menuItem38.Click += new System.EventHandler(this.menuItem38_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 1;
            this.menuItem39.Text = "���������� ��������������";
            this.menuItem39.Click += new System.EventHandler(this.menuItem39_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 2;
            this.menuItem44.Text = "������������";
            this.menuItem44.Click += new System.EventHandler(this.menuItem44_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 5;
            this.menuItem40.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem41,
            this.menuItem123});
            this.menuItem40.Text = "�����������";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 0;
            this.menuItem41.Text = "�����������";
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem123
            // 
            this.menuItem123.Index = 1;
            this.menuItem123.Text = "������ ����������� ����";
            this.menuItem123.Click += new System.EventHandler(this.menuItem123_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 6;
            this.menuItem42.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem43});
            this.menuItem42.Text = "��������";
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 0;
            this.menuItem43.Text = "�������";
            this.menuItem43.Click += new System.EventHandler(this.menuItem43_Click);
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 7;
            this.menuItem51.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem52});
            this.menuItem51.Text = "�����-����";
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 0;
            this.menuItem52.Text = "����� ���� ������";
            this.menuItem52.Click += new System.EventHandler(this.menuItem52_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 8;
            this.menuItem53.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem54,
            this.menuItem55,
            this.menuItem56});
            this.menuItem53.Text = "����� �����������";
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 0;
            this.menuItem54.Text = "��������� ���������";
            this.menuItem54.Click += new System.EventHandler(this.menuItem54_Click);
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 1;
            this.menuItem55.Text = "�����";
            this.menuItem55.Click += new System.EventHandler(this.menuItem55_Click);
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 2;
            this.menuItem56.Text = "�������";
            this.menuItem56.Click += new System.EventHandler(this.menuItem56_Click);
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 9;
            this.menuItem57.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem58,
            this.menuItem62,
            this.menuItem66,
            this.menuItem70,
            this.menuItem127});
            this.menuItem57.Text = "������";
            this.menuItem57.Popup += new System.EventHandler(this.menuItem57_Popup);
            // 
            // menuItem58
            // 
            this.menuItem58.Index = 0;
            this.menuItem58.Text = "��������� ������";
            this.menuItem58.Click += new System.EventHandler(this.menuItem58_Click);
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 1;
            this.menuItem62.Text = "������������";
            this.menuItem62.Click += new System.EventHandler(this.menuItem62_Click);
            // 
            // menuItem66
            // 
            this.menuItem66.Index = 2;
            this.menuItem66.Text = "������ �� ������";
            this.menuItem66.Click += new System.EventHandler(this.menuItem66_Click);
            // 
            // menuItem70
            // 
            this.menuItem70.Index = 3;
            this.menuItem70.Text = "�� - �� ������";
            this.menuItem70.Click += new System.EventHandler(this.menuItem70_Click);
            // 
            // menuItem127
            // 
            this.menuItem127.Index = 4;
            this.menuItem127.Text = "������� �����";
            this.menuItem127.Click += new System.EventHandler(this.menuItem127_Click);
            // 
            // menuItem64
            // 
            this.menuItem64.Index = 10;
            this.menuItem64.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem65,
            this.menuItem100,
            this.menuItem128});
            this.menuItem64.Text = "1C";
            // 
            // menuItem65
            // 
            this.menuItem65.Index = 0;
            this.menuItem65.Text = "�����/�����";
            this.menuItem65.Click += new System.EventHandler(this.menuItem65_Click);
            // 
            // menuItem100
            // 
            this.menuItem100.Index = 1;
            this.menuItem100.Text = "�����/������";
            this.menuItem100.Click += new System.EventHandler(this.menuItem100_Click);
            // 
            // menuItem102
            // 
            this.menuItem102.Index = 11;
            this.menuItem102.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem103,
            this.menuItem114,
            this.menuItem118});
            this.menuItem102.Text = "������";
            // 
            // menuItem103
            // 
            this.menuItem103.Index = 0;
            this.menuItem103.Text = "�������� �����";
            this.menuItem103.Click += new System.EventHandler(this.menuItem103_Click);
            // 
            // menuItem114
            // 
            this.menuItem114.Index = 1;
            this.menuItem114.Text = "���� �������";
            this.menuItem114.Click += new System.EventHandler(this.menuItem114_Click);
            // 
            // menuItem118
            // 
            this.menuItem118.Index = 2;
            this.menuItem118.Text = "������ ������ �������� �����";
            this.menuItem118.Click += new System.EventHandler(this.menuItem118_Click);
            // 
            // button_manage_card
            // 
            this.button_manage_card.Image = ((System.Drawing.Image)(resources.GetObject("button_manage_card.Image")));
            this.button_manage_card.Location = new System.Drawing.Point(29, 18);
            this.button_manage_card.Name = "button_manage_card";
            this.button_manage_card.Size = new System.Drawing.Size(77, 74);
            this.button_manage_card.TabIndex = 1;
            this.button_manage_card.Click += new System.EventHandler(this.button_manage_card_Click);
            // 
            // button_manage_partner
            // 
            this.button_manage_partner.Location = new System.Drawing.Point(125, 18);
            this.button_manage_partner.Name = "button_manage_partner";
            this.button_manage_partner.Size = new System.Drawing.Size(90, 74);
            this.button_manage_partner.TabIndex = 3;
            this.button_manage_partner.Text = "����������";
            this.button_manage_partner.Click += new System.EventHandler(this.button_manage_partner_Click);
            // 
            // button_auto_sell
            // 
            this.button_auto_sell.Location = new System.Drawing.Point(29, 157);
            this.button_auto_sell.Name = "button_auto_sell";
            this.button_auto_sell.Size = new System.Drawing.Size(77, 74);
            this.button_auto_sell.TabIndex = 5;
            this.button_auto_sell.Text = "�������";
            this.button_auto_sell.Click += new System.EventHandler(this.button_auto_sell_Click);
            // 
            // button_works
            // 
            this.button_works.Location = new System.Drawing.Point(403, 18);
            this.button_works.Name = "button_works";
            this.button_works.Size = new System.Drawing.Size(90, 84);
            this.button_works.TabIndex = 7;
            this.button_works.Text = "������";
            this.button_works.Click += new System.EventHandler(this.button_works_Click);
            // 
            // button_timer_workend
            // 
            this.button_timer_workend.Location = new System.Drawing.Point(835, 18);
            this.button_timer_workend.Name = "button_timer_workend";
            this.button_timer_workend.Size = new System.Drawing.Size(77, 74);
            this.button_timer_workend.TabIndex = 9;
            this.button_timer_workend.Text = "������";
            this.button_timer_workend.Click += new System.EventHandler(this.button_timer_workend_Click);
            // 
            // timer_workend
            // 
            this.timer_workend.Interval = 300000;
            this.timer_workend.Tick += new System.EventHandler(this.timer_workend_Tick);
            // 
            // button_casher
            // 
            this.button_casher.Location = new System.Drawing.Point(29, 249);
            this.button_casher.Name = "button_casher";
            this.button_casher.Size = new System.Drawing.Size(77, 74);
            this.button_casher.TabIndex = 11;
            this.button_casher.Text = "������";
            this.button_casher.Click += new System.EventHandler(this.button_casher_Click);
            // 
            // button_guaranty
            // 
            this.button_guaranty.Location = new System.Drawing.Point(125, 157);
            this.button_guaranty.Name = "button_guaranty";
            this.button_guaranty.Size = new System.Drawing.Size(77, 74);
            this.button_guaranty.TabIndex = 13;
            this.button_guaranty.Text = "��������";
            this.button_guaranty.Click += new System.EventHandler(this.button_guaranty_Click);
            // 
            // button_supervisor
            // 
            this.button_supervisor.Location = new System.Drawing.Point(221, 157);
            this.button_supervisor.Name = "button_supervisor";
            this.button_supervisor.Size = new System.Drawing.Size(77, 74);
            this.button_supervisor.TabIndex = 14;
            this.button_supervisor.Text = "��������";
            this.button_supervisor.Click += new System.EventHandler(this.button_supervisor_Click);
            // 
            // button_pos_timer
            // 
            this.button_pos_timer.Location = new System.Drawing.Point(586, 18);
            this.button_pos_timer.Name = "button_pos_timer";
            this.button_pos_timer.Size = new System.Drawing.Size(90, 84);
            this.button_pos_timer.TabIndex = 16;
            this.button_pos_timer.Text = "�����-����";
            this.button_pos_timer.Click += new System.EventHandler(this.button_pos_timer_Click);
            // 
            // button_exit
            // 
            this.button_exit.Location = new System.Drawing.Point(595, 240);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(90, 74);
            this.button_exit.TabIndex = 18;
            this.button_exit.Text = "�����";
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_auto_list
            // 
            this.button_auto_list.Location = new System.Drawing.Point(317, 157);
            this.button_auto_list.Name = "button_auto_list";
            this.button_auto_list.Size = new System.Drawing.Size(77, 74);
            this.button_auto_list.TabIndex = 20;
            this.button_auto_list.Text = "����������";
            this.button_auto_list.Click += new System.EventHandler(this.button_auto_list_Click);
            // 
            // button_cashless
            // 
            this.button_cashless.Location = new System.Drawing.Point(125, 249);
            this.button_cashless.Name = "button_cashless";
            this.button_cashless.Size = new System.Drawing.Size(77, 74);
            this.button_cashless.TabIndex = 22;
            this.button_cashless.Text = "������";
            this.button_cashless.Click += new System.EventHandler(this.button_cashless_Click);
            // 
            // client_calls
            // 
            this.client_calls.Location = new System.Drawing.Point(845, 231);
            this.client_calls.Name = "client_calls";
            this.client_calls.Size = new System.Drawing.Size(90, 74);
            this.client_calls.TabIndex = 24;
            this.client_calls.Text = "������ ��������";
            this.client_calls.Click += new System.EventHandler(this.client_calls_Click);
            // 
            // button_incom_calls
            // 
            this.button_incom_calls.Location = new System.Drawing.Point(691, 240);
            this.button_incom_calls.Name = "button_incom_calls";
            this.button_incom_calls.Size = new System.Drawing.Size(90, 74);
            this.button_incom_calls.TabIndex = 26;
            this.button_incom_calls.Text = "�������� ��������";
            this.button_incom_calls.Click += new System.EventHandler(this.button_incom_calls_Click);
            // 
            // button_manage_cardrate
            // 
            this.button_manage_cardrate.Location = new System.Drawing.Point(835, 129);
            this.button_manage_cardrate.Name = "button_manage_cardrate";
            this.button_manage_cardrate.Size = new System.Drawing.Size(90, 65);
            this.button_manage_cardrate.TabIndex = 28;
            this.button_manage_cardrate.Text = "�������� ��������";
            this.button_manage_cardrate.Click += new System.EventHandler(this.button_manage_cardrate_Click);
            // 
            // menuItem128
            // 
            this.menuItem128.Index = 2;
            this.menuItem128.Text = "������ ����� / �������";
            this.menuItem128.Click += new System.EventHandler(this.menuItem128_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(1282, 418);
            this.Controls.Add(this.button_manage_cardrate);
            this.Controls.Add(this.button_incom_calls);
            this.Controls.Add(this.client_calls);
            this.Controls.Add(this.button_cashless);
            this.Controls.Add(this.button_auto_list);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_pos_timer);
            this.Controls.Add(this.button_supervisor);
            this.Controls.Add(this.button_guaranty);
            this.Controls.Add(this.button_casher);
            this.Controls.Add(this.button_timer_workend);
            this.Controls.Add(this.button_works);
            this.Controls.Add(this.button_auto_sell);
            this.Controls.Add(this.button_manage_partner);
            this.Controls.Add(this.button_manage_card);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "���� ������";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.Idle += new EventHandler(application_Idle);
			Application.Run(new Form1());
		}

		protected static void application_Idle(object sender, EventArgs e)
		{
			if(timerDelegate == null)
			{
				timerDelegate = new TimerCallback(CheckStatus);
			}
			if(timer == null)
			{
				if(reconnect == false)
					timer = new System.Threading.Timer(timerDelegate, null, 10000, -1);
			}
			else
			{
				timer.Dispose();
				timer = null;
				if(reconnect == false)
					timer = new System.Threading.Timer(timerDelegate, null, 10000, -1);
			}
		}
		static void CheckStatus(Object state)
		{
			if(timer != null) timer.Dispose();
			timer = null;
			// ������������� ���� ���������� �� ������
			reconnect = true;
		}

		protected  override void OnCreateControl()
		{
			Authorization();
			return;
			// ��� �������� ���������� - ������� �������� � ���� ������
			// ������ ����� ������������, ������ � ���� ������
			FormConnection dialog = new FormConnection();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK)
			{
				// ����� �� �����������, ��������� ����������
				MessageBox.Show("�� ������� �������������� � ����");
				this.Close();
				return;
			}

			// ������� ������� ����������
			string connString = dialog.ConnString;
			try
			{
				connection = new SqlConnection(connString);
				connection.Open();
			}
			catch(Exception E)
			{
				MessageBox.Show(E.Message);
				this.Close();
				return;
			}

			// ������������� ����� � ���������
			DbReportCardWorkDelete.Init(connection);
			// ������������
			DbAutoType.Init(connection);
			DbWorkLink.Init(connection);
			DbWorkPlace.Init(connection);
			DbPartner.Init(connection);
			DbCardAction.Init(connection);
			DbDetail.Init(connection);
			DbFirm.Init(connection);
			DbStorageGroup.Init(connection);
			DbStaff.Init(connection);
			DbAutoFactory.Init(connection);
			DbTimeWork.Init(connection);
			DbGuarantyType.Init(connection);
			DbEngineType.Init(connection);
			DbTransmissionType.Init(connection);
			DbCardRecomend.Init(connection);
			DbAutoComplect.Init(connection);
			DbAutoColors.Init(connection);
			DbNode.Init(connection);
			DbSubNode.Init(connection);
			DbBrand.Init(connection);
			DbDirectoryWork.Init(connection);
			DbWorkGroup.Init(connection);
			DbWorkGroupDirectoryWork.Init(connection);
			DbCategorySearch.Init(connection);
			DbCategoryPrice.Init(connection);
			DbCategoryCost.Init(connection);
			DbWorkshop.Init(connection);
			DbTakeDocument.Init(connection);
			DbOption.Init(connection);
			DbOptionAuto.Init(connection);
			DbCreditBank.Init(connection);

			//���������
			// ��������
			DbWork.Init(connection);
			DbAutoModel.Init(connection);
			DbAutoSubmodel.Init(connection);
			DbAutoFactura.Init(connection);
			DbAuto.Init(connection);
			DbQuestionnaire.Init(connection);
			DbAutoIncom.Init(connection);
			DbAutoStorage.Init(connection);
			DbAutoSell.Init(connection);
			// ������������
			DbCardWork.Init(connection);
			DbCard.Init(connection);
			DbJournal.Init(connection);
			DbDetailStorage.Init(connection);
			DbDetailIncomDoc.Init(connection);
			DbDetailIncom.Init(connection);
			DbDetailIncomDetailed.Init(connection);
			DbDetailOutcomDoc.Init(connection);
			DbDetailOutcom.Init(connection);
			DbAccountDetail.Init(connection);
			DbAccountDetailItem.Init(connection);
			DbCardDetail.Init(connection);
			
			

			// �� ���������������
			DbCardWorkPersonal.Init(connection);

			// �� ������������
			DbReserve.Init(connection);
			
			// ������
			DrStorageMove.Init(connection);
			DrDetailStoragePrice.Init(connection);


			// ����� ����������
			DbSqlCard.Init(connection);
			DbSqlPartnerProperty.Init(connection);
			DbSqlDiscount.Init(connection);
			DbSqlAuto.Init(connection);
			DbSqlCardAction.Init(connection);
			DbSqlModel.Init(connection);
			DbSqlColor.Init(connection);
			DbSqlBrand.Init(connection);
			DbSqlWorkGroup.Init(connection);
			DbSqlGuarantyType.Init(connection);
			DbSqlVariant.Init(connection);
			DbSqlAutoReceive.Init(connection);
			DbSqlPartner.Init(connection);
			DbSqlPartnerContact.Init(connection);
			DbSqlPartnerConnection.Init(connection);
			DbSqlStaff.Init(connection);
			DbSqlWorkshop.Init(connection);
			DbSqlAutoSell.Init(connection);
			DbSqlStorageDetail.Init(connection);
			DbSqlWorkCollection.Init(connection);
			DbSqlWorkCollectionItem.Init(connection);
			DbSqlWork.Init(connection);
			DbSqlClaim.Init(connection);
			DbSqlCardClaim.Init(connection);
			DbSqlCardRecomendation.Init(connection);
			//DbSqlCardWorkend.Init(connection);
			DbSqlCatalogueParts.Init(connection);
			DbSqlCardDetailOrder.Init(connection);
			DbSqlStorageIncomDoc.Init(connection);
			DbSqlStorageIncom.Init(connection);
			DbSqlCardMarkEndWork.Init(connection);
			DbSqlPayment.Init(connection);
			DbSqlCardWorkComment.Init(connection);
			DbSqlSellInfo.Init(connection);
			DbSqlLicenceVehicle.Init(connection);

			DbSqlCommonReason.Init(connection);
			DbSqlServiceOuter.Init(connection);
			DbSqlListAlarm.Init(connection);
			DbSqlAutoAlarm.Init(connection);
			DbSqlCardTime.Init(connection);
			DbSqlAutoComment.Init(connection);
			DbSqlInvoice.Init(connection);
			DbSqlInvoicePay.Init(connection);

			V1_DbSqlCardRate.Init(connection);
			DbSqlAutoOptions.Init(connection);
			DbSqlAutoSellServ.Init(connection);
            DbSqlPassport.Init(connection);
            DbSqlAutoPts.Init(connection);


			// ������� ����� ������������, ������������������� ���
			// ��������� �������
			currentUser = DbStaff.GetByLogin(dialog.Login);
			if(currentUser == null)
			{
				MessageBox.Show("������������ �� ���������������!");
				connection.Close();
				this.Close();
				return;
			}
			MessageBox.Show(currentUser.FirstName + ", ����� ���������� � ����", "����������");
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// ����� ����������� ����� �����������
			autoTypeList = new FormAutoTypeList();
			autoTypeList.ShowDialog(this);
			autoTypeList.Dispose();
			autoTypeList = null;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// ����� ����������� �������������
			FormWorkList workList = new FormWorkList(null, null);
			workList.Show();
		//	workList.ShowDialog(this);
		//	workList.Dispose();
		//	workList = null;
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// ����� ������� �������� �������
			FormCardList cardList = new FormCardList(Db.ClickType.Properties, 0, null);
			cardList.Show();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ������ �����������
			FormAutoList dialog = new FormAutoList(Db.ClickType.Properties, null);
			dialog.ShowDialog(this);
			dialog.Dispose();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			// ������ ���������� ������� ������� ����
			FormWorkPlaceList dialog = new FormWorkPlaceList();
			dialog.ShowDialog(this);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// ������ ���������� ������� ������������
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ������ ������������� ������ ������
			//FormAutoFacturaList dialog = new FormAutoFacturaList();
			//dialog.ShowDialog(this);
			MessageBox.Show("����������");
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			// ����� ���� �������
			FormJournal dialog = new FormJournal(null, DateTime.Today);
			dialog.Show();
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			FormDetailList dialog = new FormDetailList();
			dialog.Show();
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			FormFirmList dialog = new FormFirmList();
			dialog.Show();
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			FormDetailStorageList dialog = new FormDetailStorageList(null, 0, null, null);
			dialog.Show();
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			// ����� ������ ��������� ������
			FormDetailIncomList dialog = new FormDetailIncomList();
			dialog.Show();
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			// ����� ������ ����������
			FormDetailOutcomList dialog = new FormDetailOutcomList();
			dialog.Show();
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			// ������ ���������
			FormStaffList dialog = new FormStaffList();
			dialog.Show();
		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			// ������ ������� �������������� �����������
			FormAutoFactoryList dialog = new FormAutoFactoryList();
			dialog.Show();
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			// ������ ������ �� ������� ���������
			FormAccountDetailList dialog = new FormAccountDetailList();
			dialog.Show();
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			// ���������� ���������� ��������
			FormStorageGroupTree dialog = new FormStorageGroupTree();
			dialog.Show();
		}

		private void menuItem21_Click(object sender, System.EventArgs e)
		{
			// ���������� ������������ ��������
			FormDefectList dialog = new FormDefectList();
			dialog.Show();
		}

		private void menuItem22_Click(object sender, System.EventArgs e)
		{
			// ���������� ������������ ������� �����������
			FormModelList dialog = new FormModelList(Db.ClickType.Properties);
			dialog.Show();
		}

		private void menuItem23_Click(object sender, System.EventArgs e)
		{
			// ���������� ������������ ����� �������
			FormGuarantyTypeList dialog = new FormGuarantyTypeList(Db.ClickType.Properties);
			dialog.Show();
		}

		private void menuItem24_Click(object sender, System.EventArgs e)
		{
			// ���������� ������������ ����� ����������
			FormEngineTypeList dialog = new FormEngineTypeList(Db.ClickType.Properties);
			dialog.Show();
		}

		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			// ���������� ������������ ����� ������� �������
			FormTransmissionTypeList dialog = new FormTransmissionTypeList(Db.ClickType.Properties);
			dialog.Show();
		}

		private void menuItem26_Click(object sender, System.EventArgs e)
		{
			// ���������� ������� ���������� ��� �����������
			// ��� ������ �������� ������ ��� ������� ���������� ������� ����������
			FormModelList dialog = new FormModelList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if(dialog.AutoModel == null) return;
			FormSubmodelList dialog1 = new FormSubmodelList(Db.ClickType.Select, dialog.AutoModel);
			dialog1.Show();
		}

		private void menuItem27_Click(object sender, System.EventArgs e)
		{
			// ���������� ������� ������������ ��� �����������
			// ��� ������ �������� ������ ��� ������� ���������� ������� ����������
			FormModelList dialog = new FormModelList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if(dialog.AutoModel == null) return;
			FormAutoComplectList dialog1 = new FormAutoComplectList(Db.ClickType.Select, dialog.AutoModel);
			dialog1.Show();
		}

		private void menuItem29_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ���������� ��������
			FormDetailManage dialog = new FormDetailManage();
			dialog.ShowDialog(this);
		}

		private void menuItem30_Click(object sender, System.EventArgs e)
		{
			// ���������� ������� ������ ��� �����������
			// ��� ������ �������� ������ ��� ������� ���������� ������� ����������
			FormModelList dialog = new FormModelList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if(dialog.AutoModel == null) return;
			FormAutoColorsList dialog1 = new FormAutoColorsList(Db.ClickType.Select, dialog.AutoModel);
			dialog1.Show();
		}

		private void menuItem31_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ���������� ��������
			FormManageBrand dialog = new FormManageBrand();
			dialog.Show();
		}

		private void menuItem33_Click(object sender, System.EventArgs e)
		{
			// ����� ������ ���������� �����-�������
			// ��� ������
			//FormCardd1 dialog = new FormCard1();
			//dialog.ShowDialog(this);
			FormManageAutoDetail dialog = new FormManageAutoDetail();
			dialog.ShowDialog();
		}

		private void menuItem34_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ����� ������������ �������������
			FormWorkSelect dialog = new FormWorkSelect(null, null);
			dialog.Show();
		}

		private void menuItem35_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� �������������� �� ����� ����������
			FormManageAutoWork dialog = new FormManageAutoWork();
			dialog.ShowDialog(this);
		}

		private void menuItem36_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ����������
			// ����� ���� ���������� �������������� �� ����� ����������
			FormManageCategoryCost dialog = new FormManageCategoryCost();
			dialog.ShowDialog(this);
		}

		private void menuItem38_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ����� ������������ �������������
			FormManageDirectoryWork dialog = new FormManageDirectoryWork();
			dialog.Show();
		}

		private void menuItem39_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� �������������� �� ����� ����������
			FormManageAutoWork dialog = new FormManageAutoWork();
			dialog.ShowDialog(this);
		}

		#region �����������
		protected static void Initialization(SqlConnection conn)
		{
			// ������������� ����� � ���������
			DbReportCardWorkDelete.Init(conn);
			// ������������
			DbAutoType.Init(conn);
			DbWorkLink.Init(conn);
			DbWorkPlace.Init(conn);
			DbPartner.Init(conn);
			DbCardAction.Init(conn);
			DbDetail.Init(conn);
			DbFirm.Init(connection);
			DbStorageGroup.Init(conn);
			DbStaff.Init(conn);
			DbAutoFactory.Init(conn);
			DbTimeWork.Init(conn);
			DbGuarantyType.Init(conn);
			DbEngineType.Init(conn);
			DbTransmissionType.Init(conn);
			DbCardRecomend.Init(conn);
			DbAutoComplect.Init(conn);
			DbAutoColors.Init(conn);
			DbNode.Init(conn);
			DbSubNode.Init(conn);
			DbBrand.Init(conn);
			DbDirectoryWork.Init(conn);
			DbWorkGroup.Init(conn);
			DbWorkGroupDirectoryWork.Init(conn);
			DbCategorySearch.Init(conn);
			DbCategoryPrice.Init(conn);
			DbCategoryCost.Init(conn);
			DbWorkshop.Init(conn);
			DbTakeDocument.Init(conn);
			DbOption.Init(conn);
			DbOptionAuto.Init(conn);
			DbCreditBank.Init(conn);

			//���������
			// ��������
			DbWork.Init(conn);
			DbAutoModel.Init(conn);
			DbAutoSubmodel.Init(conn);
			DbAutoFactura.Init(conn);
			DbAuto.Init(conn);
			DbQuestionnaire.Init(conn);
			DbAutoIncom.Init(conn);
			DbAutoStorage.Init(conn);
			DbAutoSell.Init(conn);
			// ������������
			DbCardWork.Init(conn);
			DbCard.Init(conn);
			DbJournal.Init(conn);
			DbDetailStorage.Init(conn);
			DbDetailIncomDoc.Init(conn);
			DbDetailIncom.Init(conn);
			DbDetailIncomDetailed.Init(conn);
			DbDetailOutcomDoc.Init(conn);
			DbDetailOutcom.Init(conn);
			DbAccountDetail.Init(conn);
			DbAccountDetailItem.Init(conn);
			DbCardDetail.Init(conn);
			

			// �� ���������������
			DbCardWorkPersonal.Init(conn);

			// �� ������������
			DbReserve.Init(conn);
			
			// ������
			DrStorageMove.Init(conn);
			DrDetailStoragePrice.Init(conn);
			DbHistory.Init(conn);

			// ����� ����������
			DbSqlCard.Init(connection);
			DbSqlPartnerProperty.Init(connection);
			DbSqlCardDetail.Init(connection);
			DbSqlCardWork.Init(connection);
			DbSqlFactory.Init(connection);
			DbSqlDirection.Init(connection);
			DbSqlDiscount.Init(connection);
			DbSqlStorageRequest.Init(connection);
			DbSqlAuto.Init(connection);
			DbSqlCardAction.Init(connection);
			DbSqlModel.Init(connection);
			DbSqlColor.Init(connection);
			DbSqlBrand.Init(connection);
			DbSqlWorkGroup.Init(connection);
			DbSqlGuarantyType.Init(connection);
			DbSqlVariant.Init(connection);
			DbSqlAutoReceive.Init(connection);
			DbSqlPartner.Init(connection);
			DbSqlPartnerContact.Init(connection);
			DbSqlPartnerConnection.Init(connection);
			DbSqlStaff.Init(connection);
			DbSqlWorkshop.Init(connection);
			DbSqlAutoSell.Init(connection);
			DbSqlStorageDetail.Init(connection);
			DbSqlWorkCollection.Init(connection);
			DbSqlWorkCollectionItem.Init(connection);
			DbSqlWork.Init(connection);
			DbSqlClaim.Init(connection);
			DbSqlCardClaim.Init(connection);
			DbSqlCardRecomendation.Init(connection);
			//DbSqlCardWorkend.Init(connection);
			DbSqlCatalogueParts.Init(connection);
			DbSqlCardDetailOrder.Init(connection);
			DbSqlStorageIncomDoc.Init(connection);
			DbSqlStorageIncom.Init(connection);
			DbSqlCardMarkEndWork.Init(connection);
			DbSqlPayment.Init(connection);
			DbSqlCardWorkComment.Init(connection);
			DbSqlSellInfo.Init(connection);
			DbSqlLicenceVehicle.Init(connection);

			DbSqlCommonReason.Init(connection);
			DbSqlServiceOuter.Init(connection);
			DbSqlListAlarm.Init(connection);
			DbSqlAutoAlarm.Init(connection);
			DbSqlCardTime.Init(connection);
			DbSqlAutoComment.Init(connection);
			DbSqlInvoice.Init(connection);
			DbSqlInvoicePay.Init(connection);

			V1_DbSqlCardRate.Init(connection);
			DbSqlAutoOptions.Init(connection);
			DbSqlAutoSellServ.Init(connection);
            DbSqlPassport.Init(connection);
            DbSqlAutoPts.Init(connection);
		}
		protected static void Authorization()
		{
			// ��� �������� ���������� - ������� �������� � ���� ������
			// ������ ����� ������������, ������ � ���� ������
			DbStaff user = currentUser;
			Disconnect();
			FormConnection dialog = new FormConnection();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK)
			{
				// ����� �� �����������, ��������� ����������
				Disconnect();
				MessageBox.Show("�� ������� �������������� � ����");
				//this.Close();
				return;
			}

			// ������� ������� ����������
			string connString = dialog.ConnString;
			//connString = "Server=DESKTOP-DIJ4F23\\SQLEXPRESS;Database=AutoBaseNew;Trusted_Connection=False;uid=sa;pwd=rdfrf3hekbn;TrustServerCertificate=True";
			try
			{
				connection = new SqlConnection(connString);
				connection.Open();
			}
			catch(Exception E)
			{
				Disconnect();
				MessageBox.Show(E.Message);
				//this.Close();
				return;
			}
			Initialization(connection);
			
			// ������� ����� ������������, ������������������� ���
			// ��������� �������
			currentUser		= DbStaff.GetByLogin(dialog.Login);
			currentLogin	= dialog.Login;
			if(currentUser == null)
			{
				Disconnect();
				MessageBox.Show("������������ �� ���������������!");
				//connection.Close();
				
				//this.Close();
				return;
			}
			MessageBox.Show(currentUser.FirstName + ", ����� ���������� � ����", "����������");
			reconnect = false;
		}

        protected static void AuthorizationTEST()
        {

            FormConnection dialog = new FormConnection();
            dialog.ShowDialog();
            // ������� ������� ����������
            string connString = "";
            FormSelectString dlg = new FormSelectString();
            dlg.ShowDialog();
            connString = dlg.SelectedText;
            try
            {
                connection = new SqlConnection(connString);
                connection.Open();
            }
            catch (Exception E)
            {
                Disconnect();
                MessageBox.Show(E.Message);
                //this.Close();
                return;
            }
            Initialization(connection);

            // ������� ����� ������������, ������������������� ���
            // ��������� �������
            currentUser = DbStaff.GetByLogin(dialog.Login);
            currentLogin = dialog.Login;
            if (currentUser == null)
            {
                Disconnect();
                MessageBox.Show("������������ �� ���������������!");
                //connection.Close();

                //this.Close();
                return;
            }
            MessageBox.Show(currentUser.FirstName + ", ����� ���������� � ����", "����������");
            reconnect = false;
        }

		protected static bool Authorization(string user_login, string user_password)
		{
			// ��� �������� ���������� - ������� �������� � ���� ������
			// ������ ����� ������������, ������ � ���� ������
			string server	= FileIni.GetParameter("base.ini", "#DATA_SERVER");
			string database	= FileIni.GetParameter("base.ini", "#DATA_BASE");

			string connString = "";
			connString = "user id=" + user_login + ";";
			connString += "password=" + user_password + ";";
			connString += "initial catalog=" + database + ";";
			connString += "data source=" + server + ";";
			connString += "Connect Timeout=180";

			//connString = "Server=DESKTOP-DIJ4F23\\SQLEXPRESS;Database=AutoBaseNew;Trusted_Connection=False;uid=sa;pwd=rdfrf3hekbn;TrustServerCertificate=True";

			// ������� ������� ����������
			try
			{
				connection = new SqlConnection(connString);
				connection.Open();
			}
			catch(Exception E)
			{
				Disconnect();
				MessageBox.Show(E.Message);
				//this.Close();
				return false;
			}
			Initialization(connection);
			return true;
		}
		protected static bool Disconnect()
		{
			try
			{
				if(connection != null)
					connection.Close();
			}
			catch(Exception E)
			{
				//return false;
			}
			currentUser	= null;
			connection = null;
			Initialization(null);
			return true;
		}
		#endregion

		private void menuItem41_Click(object sender, System.EventArgs e)
		{
			// ���������� ��������� �����������
			//Disconnect();
			Authorization();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			Disconnect();
		}

		private void menuItem43_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ������������ ������
			FormHistory dialog = new FormHistory();
			dialog.Show();
		}

		private void menuItem44_Click(object sender, System.EventArgs e)
		{
			// ����� ���������-������ �������������
			FormWorkSelect dialog = new FormWorkSelect(null, null);
			dialog.Show();
		}

		private void menuItem45_Click(object sender, System.EventArgs e)
		{
			// ���� ����������/�������������� �����
			FormWorkshopList dialog = new FormWorkshopList();
			dialog.ShowDialog(this);
		}

		private void menuItem46_Click(object sender, System.EventArgs e)
		{
			// ���� ������ ��������� �������� �����������
			// ��� ��� �� �����!
			return;
			FormTakeDocumentList dialog = new FormTakeDocumentList();
			dialog.ShowDialog();
		}

		private void menuItem47_Click(object sender, System.EventArgs e)
		{
			// ���� ������ � �������� �����������
			FormAutoIncom dialog = new FormAutoIncom();
			dialog.ShowDialog();
		}

		private void menuItem48_Click(object sender, System.EventArgs e)
		{
			// ������������� �����
			FormAutoStorageList dialog = new FormAutoStorageList();
			dialog.ShowDialog();
		}

		private void menuItem49_Click(object sender, System.EventArgs e)
		{
			// ���������� ���. �������������
			FormManageOptions dialog = new FormManageOptions(null);
			dialog.ShowDialog();
		}

		private void menuItem50_Click(object sender, System.EventArgs e)
		{
			// ������ ������
			FormAutoSellList dialog = new FormAutoSellList();
			dialog.ShowDialog();
		}

		private void menuItem52_Click(object sender, System.EventArgs e)
		{
			// ����� �������� ���� ������
			FormBarCodeType dialog = new FormBarCodeType();
			dialog.ShowDialog();
		}

		private void menuItem54_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ������ ������������� ������ ������
			FormAutoFacturaList dialog = new FormAutoFacturaList(Db.ClickType.Properties);
			dialog.ShowDialog(this);
		}

		private void menuItem55_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ������� ������
			FormAutoStoragePure dialog = new FormAutoStoragePure();
			dialog.Show();
		}

		private void menuItem56_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem58_Click(object sender, System.EventArgs e)
		{
			// ������ �������� � Excel ������ ��������� �����
			FormSelectDate dialog = new FormSelectDate();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;

			ExcelCardWorkDelete report = new ExcelCardWorkDelete(dialog.SelectedDate, null);
			report.Download();
		}

		private void menuItem57_Popup(object sender, System.EventArgs e)
		{
			// ������ ������
			if(reportPassChecked != true)
			{
				if(Db.CheckReportPass() != true) return;
				reportPassChecked = true;
			}
		}

		private void menuItem59_Click(object sender, System.EventArgs e)
		{
			// ������ ��������� ������
			FormCreditBankList dialog = new FormCreditBankList();
			dialog.ShowDialog();
		}

		private void menuItem60_Click(object sender, System.EventArgs e)
		{
			// ������������ ����������� �����
			//FormKladr dialog = new FormKladr();
			//dialog.ShowDialog();

			FormSelectionKladr dialog = new FormSelectionKladr();
			dialog.ShowDialog();
		}

		private void menuItem61_Click(object sender, System.EventArgs e)
		{
			// ����� ����� �����-������
			FormCardSimple dialog = new FormCardSimple();
			dialog.Show();
		}

		private void menuItem62_Click(object sender, System.EventArgs e)
		{
			// ����� ������� �������������
		}

		private void menuItem65_Click(object sender, System.EventArgs e)
		{
			// �������� � ��������� ������/������
			string login = Form1.currentLogin.ToLower();
			if (login != "�������" && login != "�����")
			{
				MessageBox.Show(this, "�� �� ������������� ��� ���� ��������");
				return;
			}
			TxtReadPrice rdr = new TxtReadPrice();
			TxtReadPrice.ReadFile();
		}

		private void menuItem63_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ������������ ����������

		}

		private void menuItem66_Click(object sender, System.EventArgs e)
		{
			// ������ ������� �� ������
			// ������ ���� ������ �������
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			// ������ ���� ����� �������
			dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			// ������ ���� ������
			FormSelectString dlg1 = new FormSelectString("��� ������", "");
			dlg1.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			long code_brand = dlg1.SelectedLong;
			
			ArrayList array = new ArrayList();
			DbCard.FillArrayPeriodBrand(array, date_start, date_end, code_brand);
			ExcelCardReport1.DownloadList(array);
			Db.ShowFaults();
		}

		private void menuItem67_Click(object sender, System.EventArgs e)
		{
			// ����� ���������� �����-��������
			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 0, null);
			dialog.Show();
		}

		private void menuItem69_Click(object sender, System.EventArgs e)
		{
			// ����� ���������� ���������� ������������
			if(Db.CheckSysPass() != true) return;
			FormManagePartnerProperty dialog = new FormManagePartnerProperty();
			dialog.Show();
		}

		private void menuItem70_Click(object sender, System.EventArgs e)
		{
			// ������ �� �� ������
			// ������ ���� ������ �������
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			// ������ ���� ����� �������
			dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			ArrayList array = new ArrayList();
			DbCard.FillArrayPeriod(array, date_start, date_end);
			ExcelCardReport2.DownloadList(array);
			Db.ShowFaults();
		}

		private void menuItem71_Click(object sender, System.EventArgs e)
		{
			// ����
			FormTest form = new FormTest();
			form.ShowDialog();
		}

		private void menuItem72_Click(object sender, System.EventArgs e)
		{
			// ���������� �������������
			FormManageDirection dialog = new FormManageDirection();
			dialog.ShowDialog();
		}

		private void menuItem73_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ����������� �������
			if(Db.CheckSysPass() != true) return;
			FormDiscountList dialog = new FormDiscountList();
			dialog.ShowDialog();
		}

		private void menuItem75_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ������ �� �����
			FormListStorageRequest dialog = new FormListStorageRequest();
			dialog.Show();
		}

		private void button_manage_card_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� �����-��������
			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 0, null);
			dialog.Show();
		}

		private void menuItem77_Click(object sender, System.EventArgs e)
		{
			// ����� ������ �����������
			FormListAuto dialog = new FormListAuto(0, null);
			dialog.Show();
		}

		private void menuItem78_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ��������
			FormManageModel dialog = new FormManageModel();
			dialog.Show();
		}

		private void menuItem79_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ��������� �����������
			FormListAutoReceive dialog = new FormListAutoReceive();
			dialog.Show();
		}

		private void menuItem80_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ������� ������������
			FormListPartner dialog = new FormListPartner(0, null);
			dialog.Show();
		}

		private void menuItem81_Click(object sender, System.EventArgs e)
		{
			// ����� ������� ������ � �������������
			FormListPartnerConnection dialog = new FormListPartnerConnection();
			dialog.Show();
		}

		private void menuItem83_Click(object sender, System.EventArgs e)
		{
			// ��������� ��������� �� ��������� ������
			// �������� �� ����������
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}

			FormSelectDateInterval dialog = new FormSelectDateInterval();
			if(dialog.ShowDialog() != DialogResult.OK) return;

			DbPrintProduction prn = new DbPrintProduction(dialog.StartDate, dialog.EndDate, 1);
			prn.Print();
		}

		private void button_manage_partner_Click(object sender, System.EventArgs e)
		{
			// ���������� ������� ������������
			FormListPartner dialog = new FormListPartner(0, null);
			dialog.Show();
		}

		private void menuItem84_Click(object sender, System.EventArgs e)
		{
			// ���������� � �������
			FormViewTest dialog = new FormViewTest();
			dialog.Show();
		}

		private void menuItem86_Click(object sender, System.EventArgs e)
		{
			// �������� � EXCEL ���������� �����
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			DbExcelSalary report = new DbExcelSalary();
			report.DownloadData(false, 1);
		}

		private void menuItem87_Click(object sender, System.EventArgs e)
		{
			FormListStaff dialog = new FormListStaff(0, 0);
			dialog.Show();
		}

		private void menuItem88_Click(object sender, System.EventArgs e)
		{
			FormCard_v01 dialog = new FormCard_v01(0, 0);
			dialog.ShowDialog(this);
		}

		private void button_auto_sell_Click(object sender, System.EventArgs e)
		{
			// �������� ������������
			string login = Form1.currentLogin.ToLower();
			if (login != "��������" && login != "�����")
			{
				return;
			}
			// ����� ���� ������ � ���������
			FormListSell dialog = new FormListSell();
			dialog.Show();
		}

		private void button_works_Click(object sender, System.EventArgs e)
		{
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;
			}
			FormManageWork dialog = new FormManageWork();
			dialog.ShowDialog();
		}

		private void menuItem90_Click(object sender, System.EventArgs e)
		{
			// ������ ������� ��������� �������
			FormStorageDetailBalance form = new FormStorageDetailBalance();
			form.Show();
		}

		private void menuItem91_Click(object sender, System.EventArgs e)
		{
			FormTestSerial dialog = new FormTestSerial();
			dialog.ShowDialog();
		}

		private void menuItem92_Click(object sender, System.EventArgs e)
		{
			FormStorageV1 dialog = new FormStorageV1();
			dialog.Show();
		}

		private void menuItem93_Click(object sender, System.EventArgs e)
		{
			FormWorkManagment dialog = new FormWorkManagment(null);
			dialog.Show();
		}

		private void menuItem94_Click(object sender, System.EventArgs e)
		{
			// ������������ ������ ��������
			DbPrintTamplate test = new DbPrintTamplate("D:\\������\\���������\\��.txt");
			test.Print();
		}

		private void menuItem95_Click(object sender, System.EventArgs e)
		{
			// �������� EXCEL ��������� �� ��������� ���������� ������ �����������
			// �������� � EXCEL ���������� �����
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			DbExcelStuffProduction report = new DbExcelStuffProduction();
			report.DownloadDataMult(false, report.staffs.Count + 1);
		}

		private void menuItem96_Click(object sender, System.EventArgs e)
		{
			// �������� � EXCEL ��������� �� ������ �������
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			DbExcelProduction report = new DbExcelProduction();
			report.DownloadDataMult(false, 3);
		}

		private void button_timer_workend_Click(object sender, System.EventArgs e)
		{
			// ������ � ��������� ������� ��� �������� ������� ������ ���������� ��������
			if(timer_workend.Enabled == true)
			{
				// ������ ��� �������
				timer_workend.Stop();
				button_timer_workend.FlatStyle = FlatStyle.Standard;
			}
			else
			{
				timer_workend.Start();
				button_timer_workend.FlatStyle = FlatStyle.Flat;
			}
		}

		private void timer_workend_Tick(object sender, System.EventArgs e)
		{
			// ���������� ������� ������ �����-������� ������� ������ ����� ���������
			ArrayList card_workends = new ArrayList();

			//DbSqlCardWorkend.SelectArrayInterval(card_workends);

			// ���� ������ ���� - ������ � �� ������
			if(card_workends.Count == 0) return;

			ArrayList text_lines = new ArrayList();
			foreach(object o in card_workends)
			{
				DtCardWorkend workend = (DtCardWorkend)o;
				string text = "";
				text = "�������� �" + workend.GetData("������_��������_�����").ToString();
				text += "; ����� ��������� ������� - " + workend.GetData("����_���������_�������").ToString();
				text_lines.Add(text);
			}
			card_workends.Clear();
			// ��������� ������� ������ ����������
			FormInfoTransparent dialog = new FormInfoTransparent("����� �������� �����-�������", "", text_lines);
			dialog.Show();
			dialog.TopLevel	= true;
			dialog.TopMost	= true;
			dialog.BringToFront();

			// �������� ���������� ��� �������� ����
			if(info_window_workend == null)
			{
				info_window_workend = new ArrayList();
			}
			info_window_workend.Add((object)dialog);
			// ��������� ���� �� �������������
			ArrayList indexies = new ArrayList();
			foreach(object o in info_window_workend)
			{
				Form form = (Form)o;
				if(form.IsDisposed == true)
				{
					indexies.Add(o);
				}
			}
			foreach(object o in indexies)
			{
				info_window_workend.Remove((object)o);
			}
			indexies.Clear();
			// ���� ���������� ���� ������� �������, ������������� ������
			if(info_window_workend.Count > 5)
			{
				timer_workend.Stop();
				button_timer_workend.FlatStyle = FlatStyle.Standard;
			}
		}

		private void menuItem97_Click(object sender, System.EventArgs e)
		{
			// �������� ���������� ������� ������ �� �����, �����-�������, ������-�������������
			// �������� � EXCEL ��������� �� ������ �������
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			DbExcelAnaliz report = new DbExcelAnaliz();
			report.DownloadDataMult(false, 1);
		}

		private void menuItem99_Click(object sender, System.EventArgs e)
		{
			// ����� ����� ���������� ��������� ���������
			FormCatalogueParts dialog = new FormCatalogueParts(null, 0);
			dialog.Show();
		}

		private void menuItem100_Click(object sender, System.EventArgs e)
		{
			// �������� ����� � ���������
			// �������� � ��������� ������/������
			string login = Form1.currentLogin.ToLower();
			if (login != "�������" && login != "�����")
			{
				MessageBox.Show(this, "�� �� ������������� ��� ���� ��������");
				return;
			}
			TxtReadIncom rdr = new TxtReadIncom();
			TxtReadIncom.ReadFile();
		}

		private void menuItem101_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ���������� ��������
			FormManageStorageIncomDoc dialog = new FormManageStorageIncomDoc();
			dialog.Show();
		}

		private void menuItem103_Click(object sender, System.EventArgs e)
		{
			// ������ ���� ������
			FormSelectDate dialog = new FormSelectDate();
			if (dialog.ShowDialog() != DialogResult.OK) return;
			DateTime date = dialog.SelectedDate;

			DbPrintReportCash print = new DbPrintReportCash(date);
			print.Print();
		}

		private void menuItem104_Click(object sender, System.EventArgs e)
		{
			// �������� ������ ����� �� ������������� � ���������
			// �������� � EXCEL ��������� �� ������ �������
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DateTime start = dialog.SelectedDate;
			dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DateTime stop = dialog.SelectedDate;

			DbExcelWorkshopWorks report = new DbExcelWorkshopWorks(start, stop, 6);
			//report.DownloadData(false, 1);
			report.DownloadDataMult(false, 2);
		}

		private void menuItem105_Click(object sender, System.EventArgs e)
		{
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return;	// ����������� �������
			}
			FormCardExtend dialog = new FormCardExtend(0, 0);
			dialog.Show();
		}

		private void menuItem106_Click(object sender, System.EventArgs e)
		{
			// ��������� ������ �� ��������
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DateTime date = dialog.SelectedDate;

			DbExcelAntirust report = new DbExcelAntirust(date, 6);
			report.DownloadDataMult(true, 2);
		}

		private void button_casher_Click(object sender, System.EventArgs e)
		{
			// ������ ������ �������
			FormSelectString selector = new FormSelectString("������� ��� ������","",true);
			if(selector.ShowDialog() != DialogResult.OK) return;
			string password = selector.SelectedText;
			// �������� ������
			if(password != "321") return;
			// ����� ���� �������
			UIF_Casher dialog = new UIF_Casher();
			dialog.Show();
		}

		private void button_guaranty_Click(object sender, System.EventArgs e)
		{
			// ������� ����� �������� �� ��������
		}

		private void button_supervisor_Click(object sender, System.EventArgs e)
		{
			// ������ ������ �������
			FormSelectString selector = new FormSelectString("������� ��� ������","",true);
			if(selector.ShowDialog() != DialogResult.OK) return;
			string password = selector.SelectedText;
			// �������� ������
			if(password != "564897231") return;

			// ������� ����� ����������
			UIF_Supervisor form = new UIF_Supervisor();
			form.Show();
		}

		private void menuItem107_Click(object sender, System.EventArgs e)
		{
			UserInterface.LicenceVehicleList(0, null, 0, UserInterface.ClickType.Modify);
		}

		private void menuItem108_Click(object sender, System.EventArgs e)
		{
			UIF_Inspection_V1 dialog = new UIF_Inspection_V1();
			dialog.Show();
		}

		private void menuItem109_Click(object sender, System.EventArgs e)
		{
			CS_PTS pts = new CS_PTS();
			pts.code = 5;
			MessageBox.Show(pts.code.ToString());
			Data_SQL_PTS data = new Data_SQL_PTS(pts.SaveStruct());
			data.Test();
			pts.LoadStruct(data.Return());
			MessageBox.Show(pts.code.ToString());
		}

		private void menuItem110_Click(object sender, System.EventArgs e)
		{
			UIF_Claim dialog = new UIF_Claim();
			dialog.Show();
		}

		private void button_pos_timer_Click(object sender, System.EventArgs e)
		{
			// ���������� � �������������� ��������� �����������
			// � ��������� ����������� ����� - ���������� ��������� �����-��������
			if(Authorization("��������", "1") == false) return;

			UIF_POS_AutoTiming dialog = new UIF_POS_AutoTiming();
			dialog.ShowDialog();

			Disconnect();
		}

		private void button_exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button_auto_list_Click(object sender, System.EventArgs e)
		{
			// ������ �����������
			// ����� ������ �����������
			FormListAuto dialog = new FormListAuto(0, null);
			dialog.Show();
		}

		private void button_cashless_Click(object sender, System.EventArgs e)
		{
			// ���������� �������/��������
			UI_InvoiceList form = new UI_InvoiceList(Db.ClickType.Properties);
			form.Show();
		}

		private void client_calls_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ������ � ��������������� ��������
			UI_CallToClient form = new UI_CallToClient();
			form.Show();
		}

		private void menuItem112_Click(object sender, System.EventArgs e)
		{
			// ����� �� �������� �� ������ ��
			DbExcelReport report = new DbExcelReport();
			//report.DownloadData(false, 1);
			report.DownloadDataMult(false, 5);
		}

		private void button_incom_calls_Click(object sender, System.EventArgs e)
		{
			// ����� ���� ������ �� ��������� ����������
			UIFormListCalls form = new UIFormListCalls();
			form.Show();
		}

		private void menuItem113_Click(object sender, System.EventArgs e)
		{
			COMPortReader form = new COMPortReader();
			form.ShowDialog();
		}

		private void menuItem114_Click(object sender, System.EventArgs e)
		{
			// ������ ������ ��� ������� ��������
			// ������ ���� ������
			FormSelectDate dialog = new FormSelectDate();
			if (dialog.ShowDialog() != DialogResult.OK) return;
			DateTime date = dialog.SelectedDate;
			// ������ ������ ������������
			FormStaffList dialog1 = new FormStaffList();
			if (dialog1.ShowDialog() != DialogResult.OK) return;
			DbStaff staff = dialog1.SelectedStaff;
			if (staff == null) return;
			long staff_code = staff.Code;

			DBPrintReportCallService print = new DBPrintReportCallService(date, staff_code);
			print.Print();
		}

		private void button_manage_cardrate_Click(object sender, System.EventArgs e)
		{
			// ����� ���� �������� �������� �������
			V1_UIF_CardRateManager form = new V1_UIF_CardRateManager();
			form.Show();
		}

		private void menuItem115_Click(object sender, System.EventArgs e)
		{
			UIF_Options dialog = new UIF_Options();
			dialog.Show();
		}

		private void menuItem116_Click(object sender, System.EventArgs e)
		{
			// ������ ������������ ������ �� �������� �� �� ������
			DbExcelReportTo report_to = new DbExcelReportTo();
			//report_to.DownloadData(false, 1);
			report_to.DownloadDataMult(false, 1);
		}

		private void menuItem117_Click(object sender, System.EventArgs e)
		{
			// ������ ���������� ������ �� �������� �� ������
			DbExcelReportSell report_sell = new DbExcelReportSell();
			//report_to.DownloadData(false, 1);
			report_sell.DownloadDataMult(false, 1);
		}

        private void menuItem118_Click(object sender, EventArgs e)
        {
            // �������� � EXCEL ����� ������� ������ �������� �����
            DbExcelReportFBS report = new DbExcelReportFBS();
            report.DownloadDataMult(true, 2);
        }

        private void menuItem119_Click(object sender, EventArgs e)
        {
            // ������������ �������� �����-�������
           // DbWord.OpenFile("E:\\test.doc");
        }

        private void menuItem121_Click(object sender, EventArgs e)
        {
            // ���� ���������� ���� �� �����-���� � ��� ������
            FormBarCodeLada1 form = new FormBarCodeLada1();
            form.ShowDialog();
        }

        private void menuItem122_Click(object sender, EventArgs e)
        {
            FormSelectString dlg = new FormSelectString();
            bool flag = true;
            while (flag)
            {
                if (dlg.ShowDialog() != DialogResult.OK) flag = false;
                else
                {
                    double digit = dlg.SelectedFloat;
                    UI_Digit2Text.Convert(digit);
                }
            }
        }

        private void menuItem123_Click(object sender, EventArgs e)
        {
            // ����������� ����� �������� �������� ������
            AuthorizationTEST();
        }

        private void menuItem124_Click(object sender, EventArgs e)
        {
			// ������������ �������� DataGrid
			TestDataGrid dlg = new TestDataGrid();
			dlg.ShowDialog();
        }

        private void menuItem125_Click(object sender, EventArgs e)
        {
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;


			// ����� ������ ��������� ������ �������� �������� �� ��������� ����
			// �������, ��� �������� � ����� ����
			ArrayList number_array = new ArrayList();
			DbSqlCard.SelectCardClosedNumber(number_array, date_start, date_end);
			ArrayList array = new ArrayList();
			FormInfoTable info = new FormInfoTable("������ �������");
			info.Show();
			foreach (DtCard element in number_array)
			{

				DbCard card = DbCard.Find((long)element.GetData("�����_��������"), (int)element.GetData("���_��������"));
				array.Add(card);
				info.SetText(card.NumberTxt);
			}
			info.SetText("����� �������");
			info.Close();


			//DbCard.FillArrayClosedInterval(array, date_start, date_end);
			ExelCardReportStaffSellary.DownloadList(array);
			Db.ShowFaults();
		}

        private void menuItem126_Click(object sender, EventArgs e)
        {
			// �������� EXCEL ��������� �� ��������� ���������� ������ �����������
			// �������� � EXCEL ���������� �����
			string login = Form1.currentLogin.ToLower();
			if (login != "�����")
			{
				return; // ����������� �������
			}
			DbExcelStuffProduction report = new DbExcelStuffProduction(3);
			report.DownloadDataMult(false, report.staffs.Count + 1);
		}

        private void menuItem127_Click(object sender, EventArgs e)
        {
			// ������� ������� ����� - ������ �������������� ������
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;


			// ����� ������ ��������� ������ �������� �������� �� ��������� ����
			// �������, ��� �������� � ����� ����
			ArrayList number_array = new ArrayList();
			DbSqlCard.SelectCardClosedNumber(number_array, date_start, date_end);
			ArrayList array = new ArrayList();
			FormInfoTable info = new FormInfoTable("������ �������");
			info.Show();
			foreach (DtCard element in number_array)
			{

				DbCard card = DbCard.Find((long)element.GetData("�����_��������"), (int)element.GetData("���_��������"));
				array.Add(card);
				info.SetText(card.NumberTxt);
			}
			info.SetText("����� �������");
			info.Close();


			//DbCard.FillArrayClosedInterval(array, date_start, date_end);
			ExcelReports.ComplexReport.Downloadreport(array);
			Db.ShowFaults();
		}

        private void menuItem128_Click(object sender, EventArgs e)
        {
			// �������� � ��������� ������/������
			string login = Form1.currentLogin.ToLower();
			if (login != "�������" && login != "�����")
			{
				MessageBox.Show(this, "�� �� ������������� ��� ���� ��������");
				return;
			}
			//ReadTextStorage rdr = new ReadTextStorage();
			ReadTextStorage.ReadFile();
		}
    }
}
