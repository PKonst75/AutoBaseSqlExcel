using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс описывающий каталог запасных частей.
	/// </summary>
	public class DtCatalogueParts
	{
		long	code;					// Уникальный код в каталоге
		long	code_group;				// Код группы которой пренадлежит запчасть
		bool	flag_group;				// Флаг того, что данный элемент является группой
		string	name;					// Наименование запчасти / группы
		
		public DtCatalogueParts()
		{
			code		= 0;
			code_group	= 0;
			flag_group	= false;
			name		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_КАТАЛОГ_ДЕТАЛЬ":
					return (object)(long)code;
				case "КОД_ГРУППА":
					return (object)(long)code_group;
				case "ФЛАГ_ГРУППА":
					return (object)(bool)flag_group;
				case "НАИМЕНОВАНИЕ":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_КАТАЛОГ_ДЕТАЛЬ":
					code = (long)val;
					break;
				case "КОД_ГРУППА":
					code_group = (long)val;
					break;
				case "ФЛАГ_ГРУППА":
					flag_group = (bool)val;
					break;
				case "НАИМЕНОВАНИЕ":
					name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetTNode(TreeNode node)
		{
			node.Text	= "";		// Чтобы сделать однотипным добавление и изменение

			node.Tag				= this.code;
			node.Text				= this.name;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.Text	= "";		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.name;
		}
	}
}
