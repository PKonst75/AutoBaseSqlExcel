using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Бренды. Для удобства сортировки. Исключительно.
	/// </summary>
	public class DtBrand
	{

		public enum DIALER : long { unknown = 0, chevrolet = 1, lada = 2, kia = 3 }

		long		code;			// Уникальный код бренда в БД
		string		name;			// Наименование бренда

		public DtBrand()
		{
			code		= 0;
			name		= "";
		}

		public long Code
        {
            get { return code; }
        }
		public DIALER DialerOfficial
        {
            get
            {
                switch (name)
                {
					case "ШЕВРОЛЕ":
						return DtBrand.DIALER.chevrolet;
					case "LADA":
						return DtBrand.DIALER.lada;
					default:
						return DtBrand.DIALER.unknown;
				}
			}
        }

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_БРЕНД":
					return (object)(long)code;
				case "НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ_БРЕНД":
					code = (long)val;
					break;
				case "НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД":
					name = (string)val;
					name.Trim();
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД":
					if(name.Length == 0) return false;
					break;
				case "КОД_АВТОМОБИЛЬ_БРЕНД":
					if(code <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.name;
		}

		public void SetTNode(TreeNode node)
		{
			node.Text = "";			// Чтобы сделать однотипным добавление и изменение

			node.Tag				= this.code;
			node.Text				= this.name;
		}
	}
}
