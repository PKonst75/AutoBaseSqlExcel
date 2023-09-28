using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ����� �������� ���������� 
	/// </summary>
	public class DtStaff:Dt
	{
		// ������ ��������� ���� ������
		private long _code; // ���������� ���
		private string _surname; // �������
		private string _name; // ���
		private string _patronymic; // ��������
		private string _login; // ����� ��� ������� � �������
		private bool _avaliable; // �������� �� � ������ ������ ���������
		private long _sign; // ����������� ������� ������ � �������
		private long _codeJob; // ��� ���������
		private long _codeWorkshop; // ��� �������������
		private float _coefSalary; // ������������ ��� ������� ��������
		private float _salary; // ����� ���������� �����

		public DtStaff()
		{
			// ������ ��������� ���� ������
			_code = _sign = _codeJob = _codeWorkshop = 0L; 
			_surname = _name = _patronymic = _login = "";
			_avaliable	= false;
			_coefSalary	= 0.0F;
		}
		#region ������� ������� � �������
		public long Code
		{
			get { return _code; }
			set { _code = value; }
		}
		public string Surname
        {
            get { return _surname; }
			set { _surname = value; }
        }
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public string Patronymic
		{
			get { return _patronymic; }
			set { _patronymic = value; }
		}
		public string Login
		{
			get { return _login; }
			set { _login = value; }
		}
		public bool Avaliable
		{
			get { return _avaliable; }
			set { _avaliable = value; }
		}
		public long Sign
		{
			get { return _sign; }
			set { _sign = value; }
		}
		public long CodeJob
		{
			get { return _codeJob; }
			set { _codeJob = value; }
		}
		public long CodeWorksjop
		{
			get { return _codeWorkshop; }
			set { _codeWorkshop = value; }
		}
		public float CoefSelary
        {
            get { return _coefSalary; }
			set { _coefSalary = value; }
        }
		public float Salary
        {
			get { return _salary; }
			set { _salary = value; }
		}
		#endregion

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_��������":
					return (object)(long)Code;
				case "�������_��������":
					return (object)(string)Surname;
				case "���_��������":
					return (object)(string)Name;
				case "��������_��������":
					return (object)(string)Patronymic;
				case "�����":
					return (object)(string)Login;
				case "��������":
					return (object)(bool)Avaliable;
				case "�����������_�������_��������":
					return (object)(long)Sign;
				case "������_���_���������":
					return (object)(long)CodeJob;
				case "������_���_�������������":
					return (object)(long)CodeWorksjop;
				case "������_�����������":
					return (object)(float)CoefSelary;
				case "�����":
					return (object)(float)Salary;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_��������":
					Code = (long)val;
					break;
				case "�������_��������":
					Surname = (string)val;
					break;
				case "���_��������":
					Name = (string)val;
					break;
				case "��������_��������":
					Patronymic = (string)val;
					break;
				case "�����":
					Login = (string)val;
					break;
				case "��������":
					Avaliable = (bool)val;
					break;
				case "�����������_�������_��������":
					Sign = (long)val;
					break;
				case "������_���_���������":
					CodeJob = (long)val;
					break;
				case "������_���_�������������":
					CodeWorksjop = (long)val;
					break;
				case "������_�����������":
					CoefSelary = (float)val;
					break;
				case "�����":
					Salary = (float)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.Code;
			item.Text				= this.Surname;
			item.SubItems.Add(this.Name + " " + this.Patronymic);
		}

		public string Title
		{
			get
			{
				DtTxtStaff txtStaff = new DtTxtStaff(this);
				return txtStaff.TitleShort;
				/*
				string txt1 = surname + " ";
				string txt2 = "";
				if (name.Length > 2)
					txt2 = name.Substring(0, 1).ToUpper() + ".";
				else
					txt2 = name;
				string txt3 = "";
				if (patronymic.Length > 2)
				{
					txt3 = patronymic.Substring(0, 1).ToUpper() + ".";
				}
				return txt1 + txt2 + txt3;
				*/
			}
		}
	}
    #region ����� ����������� ���������� � �����
    public class DtTxtStaff
    {
		private readonly DtStaff _staff;
		private readonly string _titleShort;
		private readonly string _surname;
		private readonly string _name;
		private readonly string _patronymic;

		public DtTxtStaff(DtStaff srcStaff)
        {
			string txt;
			_staff = srcStaff;
			txt = _staff.Surname;
			if (_staff.Name.Length >= 1) txt += " " + _staff.Name.Substring(0, 1).ToUpper() +".";
			if (_staff.Patronymic.Length >= 1) txt += _staff.Patronymic.Substring(0, 1).ToUpper() +".";
			_titleShort = txt;
			_surname = _staff.Surname;
			_name = _staff.Name;
			_patronymic = _staff.Patronymic;
		}
		public DtStaff Staff
        {
			get { return _staff; }
        }
		public string TitleShort
        {
			get { return _titleShort; }
        }
		public string Surname
		{
			get { return _surname; }
		}
		public string Name
		{
			get { return _name; }
		}
		public string Pantronymic
		{
			get { return _patronymic; }
		}
	}
	#endregion

	#region ����������� � WindowsForm � ListView ������ �����
	public class WfListViewFormStaffT01 : DtTxtStaff, ListViewItemSetting
	{
		public WfListViewFormStaffT01(DtStaff srcStaff) : base(srcStaff) { }
		public void SetListViewItem(ListViewItem srcItem) // ������������� �������� ��� ����������� � ���� ������ ����
		{
			string txt = "";
			srcItem.SubItems.Clear();

			srcItem.Tag = Staff.Code;
			srcItem.Text = Surname;

			if (Name.Length != 0) txt = Name;
			if (txt.Length != 0 && Pantronymic.Length != 0) txt += " " + Pantronymic;
			srcItem.SubItems.Add(txt);
		}
	}
	#endregion

	#region ����� ������������� ��������� �����������
	public class DtStaffCollection
	{
		private ArrayList _collection; // ������ ������ ��������
		public DtStaffCollection(DtCardWork srcCardWork)
		{
			_collection = DbSqlStaff.SelectExecutors(srcCardWork);
		}
		public void AddStaff(DtStaff srcStaff)
		{
			if (_collection == null) _collection = new ArrayList();
			_collection.Add(srcStaff);
		}
		public ArrayList Collection
		{
			get { return _collection; }
		}

		public bool ExistElements() // ���� �� �������� � ���������
        {
			if (Count() == 0) return false;
			return true;
        }
		public int Count()
        {
			if (Collection == null) return 0;
			return Collection.Count;
		}
	}
	#endregion

}
