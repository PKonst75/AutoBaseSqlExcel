using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtClaim.
	/// </summary>
	public class DtClaim
	{
		private long _code; // ���������� ��� �������/���������� ������
		private string _name; // �������� �������/���������� ������

		public DtClaim()
		{
			_code = 0;
			_name = "";
		}

		public DtClaim(string srcName):this()
		{
			_name = srcName;
		}

		#region ������� � �������
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}
		public long Code
		{
			set { _code = value; }
			get { return _code; }
		}
        #endregion

        public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= Code;
			item.Text				= Name;
		}
	}

	public class DtTxtClaim
    {
		private readonly DtClaim _claim;

		public DtTxtClaim(DtClaim srcClaim)
        {
			_claim = srcClaim;
        }

		public string Name
        {
            get { return _claim.Name; }
        }
    }
}
