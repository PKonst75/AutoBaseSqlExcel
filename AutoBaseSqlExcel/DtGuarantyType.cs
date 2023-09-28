using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Виды гарантии
	/// </summary>
	public class DtGuarantyType:Dt
	{
		long		code;			// Код вида гарантии в БД
		string		name;			// Наименование вида гарантии

		bool		is_responsible;	// Флаг наличия ответсвенного

		public DtGuarantyType()
		{
			code		= 0;
			name		= "";

			is_responsible	= false;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_ГАРАНТИЯ":
					return (object)(long)code;
				case "ОПИСАНИЕ_ГАРАНТИЯ":
					return (object)(string)name;
				case "ОТВЕТСТВЕННЫЙ":
					return (object)(bool)is_responsible;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_ГАРАНТИЯ":
					code = (long)val;
					break;
				case "ОПИСАНИЕ_ГАРАНТИЯ":
					name = (string)val;
					break;
				case "ОТВЕТСТВЕННЫЙ":
					is_responsible = (bool)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= this.code;
			item.Text				= this.name;
		}

		public override string Title()
		{
			return name;
		}

		public override long Code()
		{
			return code;
		}
	}
}
