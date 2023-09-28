using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ ������� �����������
	/// </summary>
	public class DtModel
	{
		long code;                  // ���������� ��� ������
		string _name;                    // ������������ ������
		long code_workgroup;            // ������ ������������� �� ���������
		long code_guarantytype;     // ��� �������� �� ���������
		bool in_sell;               // ������� �� ������ �� �������
		string engine;                  // ��� ��������� ������
		string markmodel;               // ����� ������ � ���
		string type;                    // ��� ������������� ��������
		string trans;                   // �����������

		string tmp_workgroup_name;
		string tmp_guarantytype_name;

		public DtModel()
		{
			code = 0;
			_name = "";
			code_guarantytype = 0;
			code_workgroup = 0;
			in_sell = false;


			engine = "";
			markmodel = "";
			trans = "";
			type = "";

			tmp_workgroup_name = "";
			tmp_guarantytype_name = "";
		}

		public string Name
        {
			get { return _name; }

        }

		public object GetData(string data)
		{
			switch (data)
			{
				case "���_����������_������":
					return (object)(long)code;
				case "������":
					return (object)(string)Name;
				case "������_���_����������_���":
					return (object)(long)code_workgroup;
				case "������_���_��������":
					return (object)(long)code_guarantytype;
				case "�_�������":
					return (object)(bool)in_sell;
				case "���������":
					return (object)(string)engine;
				case "�����_������_���":
					return (object)(string)markmodel;
				case "�����������":
					return (object)(string)trans;
				case "���_��":
					return (object)(string)type;
				// ���������
				case "������������":
					return (object)(string)tmp_workgroup_name;
				case "��������_��������":
					return (object)(string)tmp_guarantytype_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "���_����������_������":
					code = (long)val;
					break;
				case "������":
					_name = (string)val;
					_name = _name.Trim();
					break;
				case "������_���_����������_���":
					code_workgroup = (long)val;
					break;
				case "������_���_��������":
					code_guarantytype = (long)val;
					break;
				case "�_�������":
					in_sell = (bool)val;
					break;
				case "���������":
					engine = (string)val;
					break;
				case "�����_������_���":
					markmodel = (string)val;
					break;
				case "�����������":
					trans = (string)val;
					break;
				case "���_��":
					type = (string)val;
					break;
				// ���������
				case "������������":
					tmp_workgroup_name = (string)val;
					break;
				case "��������_��������":
					tmp_guarantytype_name = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch (data)
			{
				case "������":
					if (_name.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();      // ����� ������� ���������� ���������� � ���������

			item.Tag = this.code;
			item.Text = this._name;
		}

		public void SetLVItemWide(ListViewItem item)
		{
			item.SubItems.Clear();      // ����� ������� ���������� ���������� � ���������

			item.Tag = this.code;
			item.Text = this._name;
			item.SubItems.Add(this.tmp_guarantytype_name);
			if (this.in_sell)
				item.SubItems.Add("+");
			else
				item.SubItems.Add("");
		}

		public void SetTNode(TreeNode node)
		{
			node.Text = "";     // ����� ������� ���������� ���������� � ���������

			node.Tag = this.code;
			node.Text = this._name;
			if (this.in_sell)
				node.BackColor = Color.LightGreen;
		}
		public string Txt()
		{
			return Name;
		}

		public override string ToString()
		{
			return Name;
		}

		public long CodeAutoType
		{
			get { return code_workgroup; }
		}
	}
}
