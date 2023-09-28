using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ ��������� �� �����.
	/// </summary>
	public class DtStorageIncom
	{
		struct Pair
		{
			public long code;
			public long poition;
		}

		long code_doc;			// ��� ���������
		long position;			// ������� � ���������
		long code_detail;		// ��� ��������� �������
		float quontity;			// ����������
		float price;			// ���� �� �������

		string tmp_name;		// ������������ ������
		string tmp_articul;		// ����� ������ / �������
		string tmp_number;		// ����� ���������
		DateTime tmp_date;		// ���� ���������

		public DtStorageIncom()
		{
			code_doc		= 0;
			position		= 0;
			code_detail		= 0;
			quontity		= 0.0F;
			price			= 0.0F;

			tmp_name		= "";
			tmp_articul		= "";
			tmp_number		= "";
			tmp_date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_��������":
					return (object)(long)code_doc;
				case "�������":
					return (object)(long)position;
				case "���_�����_������":
					return (object)(long)code_detail;
				case "����������":
					return (object)(float)quontity;
				case "����":
					return (object)(float)price;
				case "������������":
					return (object)(string)tmp_name;
				case "�������":
					return (object)(string)tmp_articul;
				case "�����":
					return (object)(string)tmp_number;
				case "����":
					return (object)(DateTime)tmp_date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_��������":
					code_doc = (long)val;
					break;
				case "�������":
					position = (long)val;
					break;
				case "���_�����_������":
					code_detail = (long)val;
					break;
				case "����������":
					quontity = (float)val;
					break;
				case "����":
					price = (float)val;
					break;
				case "������������":
					tmp_name = (string)val;
					break;
				case "�������":
					tmp_articul = (string)val;
					break;
				case "�����":
					tmp_number = (string)val;
					break;
				case "����":
					tmp_date = (DateTime)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			Pair pair;
			pair.code				= this.code_doc;
			pair.poition			= this.position;
			item.Tag				= pair;

			item.Text				= this.position.ToString();
			item.SubItems.Add(this.tmp_articul);
			item.SubItems.Add(this.tmp_name);
			item.SubItems.Add(this.quontity.ToString());
			item.SubItems.Add(this.price.ToString());
		}

		public void SetLVItemMove(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			Pair pair;
			pair.code				= this.code_doc;
			pair.poition			= this.position;
			item.Tag				= pair;

			item.Text				= this.tmp_articul;
			item.SubItems.Add(this.tmp_name);
			item.SubItems.Add(this.tmp_date.ToString());
			item.SubItems.Add(this.quontity.ToString());
			item.SubItems.Add(this.tmp_number);
		}
	}
}
