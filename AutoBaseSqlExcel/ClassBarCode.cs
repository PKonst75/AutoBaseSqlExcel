using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ClassBarCode.
	/// </summary>
	public class ClassBarCode
	{
		protected string barCode;

		public ClassBarCode(string srcBarCode)
		{
			barCode = srcBarCode;
		}

		public string AutoModelMask
		{
			get
			{
				switch(Db.barcCodeType)
				{
					case Db.BarCodes.Vaz:
						// �������� ������ ������ ���������� �� ���������� ����
						return VazAutoModelMask();
						
					default:
						return "";
						
				}
			}
		}

		public string AutoBody
		{
			get
			{
				switch(Db.barcCodeType)
				{
					case Db.BarCodes.Vaz:
						// �������� ������ ������ ���������� �� ���������� ����
						return VazAutoBody();
						
					default:
						return "";
						
				}
			}
		}

		public bool ValidAuto
		{
			// ���������� �� �����-���
			get
			{
				switch(Db.barcCodeType)
				{
					case Db.BarCodes.Vaz:
						if(barCode.Length != 14) return false;
						break;
					default:
						return false;
				}
				return true;
			}
		}

		protected string VazAutoModelMask()
		{
			string shortModel;

			shortModel = barCode.Substring(2, 4);
			return "��� " + shortModel + "%";
		}

		protected string VazAutoBody()
		{
			string body;

			body		= barCode.Substring(6, 7);
			return body;
		}
	}
}
