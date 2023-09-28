using System;
using System.Windows.Forms;
using System.IO;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for TxtReadIncom.
	/// </summary>
	public class TxtReadIncom
	{
		public struct FileLine
		{
			public string	document_number;
			public DateTime	document_date;
			public long		nomenclature_position;
			public long		nomenclature_code;
			public string	nomenclature_name;
			public string	nomenclature_number;
			public string	nomenclature_unit;
			public float	nomenclature_quontity;
			public float	nomenclature_price;
			public float	nomenclature_input;
			public float	nomenclature_input_price;
		}

		public TxtReadIncom()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static StreamReader GetReader()
		{
			StreamReader reader = null;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect		= false;
			dlg.Filter = "��������� ����� (*.txt)|*.txt";
			dlg.ShowDialog();
			if(dlg.FileName.Length == 0) return null;
			try
			{
				reader = new StreamReader(dlg.FileName, System.Text.Encoding.GetEncoding(1251));
			}
			catch(Exception e)
			{
				return null;
			}
			if(reader == null) return null;
			return reader;
		}
		public static void SkipHeader(StreamReader reader)
		{
			// ������ � ���������� ������ N ����� �����
			string s;
			for(int i = 0; i < 4; i++)
			{
				s = reader.ReadLine();
			}
		}
		public static FileLine GetLine(StreamReader reader)
		{
			// ���������� ����� ���������, ���������� ��������� ������
			string s;
			FileLine line = new FileLine();
			line.document_number	= "";
			line.nomenclature_code	= -1;

			// ������ ����� �� �����
			s = reader.ReadLine();
			if(s == null) return line;
			// ����� ������ �� ��������� ������
			char[] separators = new Char[] {'\t'};
			string[] ssplit = s.Split(separators, 12);
			// ����������� ��������� � ������
			// �������� �������
			line.document_date			= DateTime.Now;
			line.document_number		= ssplit[1];
			if(ssplit[2].Length > 0)
				line.document_date			= (DateTime)Convert.ToDateTime(ssplit[2]);
			// ������������
			line.nomenclature_position	= 0;
			if(ssplit[3].Length > 0)
				line.nomenclature_position	= (long)Convert.ToInt64(ssplit[3]);
			if(ssplit[4].Length > 0)
				line.nomenclature_code		= (long)Convert.ToInt64(ssplit[4]);
			else
				line.nomenclature_code		= 0;
			line.nomenclature_name		= ssplit[5];
			line.nomenclature_number	= ssplit[6];
			line.nomenclature_unit		= ssplit[7];
			if(ssplit[8].Length > 0)
				line.nomenclature_quontity	= (float)Convert.ToDecimal(ssplit[8]);
			else
				line.nomenclature_quontity	= 0.0F;
			if(ssplit[9].Length > 0)
			{
				string[] ssplit2 = ssplit[9].Split(new Char[] {' '}, 2);
				try
				{
					line.nomenclature_price		= (float)Convert.ToDecimal(ssplit2[0]);
				}
				catch(Exception e)
				{
					line.nomenclature_price		= 0.0F;
				}
			}
			else
				line.nomenclature_price		= 0.0F;
			if(ssplit[10].Length > 0)
				line.nomenclature_input	= (float)Convert.ToDecimal(ssplit[10]);
			else
				line.nomenclature_input	= 0.0F;
			if(ssplit[11].Length > 0)
				line.nomenclature_input_price	= (float)Convert.ToDecimal(ssplit[11]);
			else
				line.nomenclature_input_price	= 0.0F;
			return line;
			
		}
		public static void ReadFile()
		{
			long doc_code = 0;
			StreamReader reader = null;
			reader	= GetReader();
			if(reader == null)
			{
				MessageBox.Show(null, "�� ������� ���������", "��������������");
				return;
			}
			SkipHeader(reader);
			FileLine line;
			line.nomenclature_code = 0;
			while(line.nomenclature_code != -1)
			{
				line = GetLine(reader);
				// ��������� � ���������� ��������
				if(line.document_number != "")
				{
					DtStorageIncomDoc doc = new DtStorageIncomDoc();
					doc.SetData("�����", line.document_number);
					doc.SetData("����", line.document_date);
					doc = DbSqlStorageIncomDoc.Insert(doc);
					if(doc != null)
						doc_code = (long)doc.GetData("���");
					else
					{
						// ������� �����, ��� ��� �������� ��� ����������
						doc = DbSqlStorageIncomDoc.FindNumber(line.document_number, line.document_date);
						if(doc != null)
							doc_code = (long)doc.GetData("���");
						else
							doc_code = 0;
					}
				}

				if(line.nomenclature_code > 0)
				{
					// ������� �������������� ������� � ������
					DtStorageDetail detail = DbSqlStorageDetail.Find1C(line.nomenclature_code);
					if(detail != null) 
					{
						// ���������� ������ ������ �� �����
						DtStorageIncom incom = new DtStorageIncom();
						incom.SetData("���_��������", doc_code);
						incom.SetData("�������", line.nomenclature_position);
						incom.SetData("���_�����_������", (long)detail.GetData("���_�����_������"));
						incom.SetData("����������", line.nomenclature_quontity);
						incom.SetData("����", line.nomenclature_input_price);
						incom = DbSqlStorageIncom.Insert(incom);
					}
				}
			}
			reader.Close();
			MessageBox.Show(null, "���������!!!", "��");
			return;
		}
	}
}
