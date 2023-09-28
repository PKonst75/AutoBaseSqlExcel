using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Список моделей автомобилей
	/// </summary>
	public class DtModel
	{
		long code;                  // Уникальный код модели
		string _name;                    // Наименование модели
		long code_workgroup;            // Группа трудоемкостей по умолчанию
		long code_guarantytype;     // Вид гарантии по умолчанию
		bool in_sell;               // Активна ли модель по продаже
		string engine;                  // Тип двигателя модели
		string markmodel;               // Марка модель в ПТС
		string type;                    // Тип транспортного средства
		string trans;                   // Трансмиссия

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
				case "КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					return (object)(long)code;
				case "МОДЕЛЬ":
					return (object)(string)Name;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП":
					return (object)(long)code_workgroup;
				case "ССЫЛКА_КОД_ГАРАНТИЯ":
					return (object)(long)code_guarantytype;
				case "В_ПРОДАЖЕ":
					return (object)(bool)in_sell;
				case "ДВИГАТЕЛЬ":
					return (object)(string)engine;
				case "МАРКА_МОДЕЛЬ_ПТС":
					return (object)(string)markmodel;
				case "ТРАНСМИССИЯ":
					return (object)(string)trans;
				case "ТИП_ТС":
					return (object)(string)type;
				// Временные
				case "НАИМЕНОВАНИЕ":
					return (object)(string)tmp_workgroup_name;
				case "ОПИСАНИЕ_ГАРАНТИЯ":
					return (object)(string)tmp_guarantytype_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					code = (long)val;
					break;
				case "МОДЕЛЬ":
					_name = (string)val;
					_name = _name.Trim();
					break;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП":
					code_workgroup = (long)val;
					break;
				case "ССЫЛКА_КОД_ГАРАНТИЯ":
					code_guarantytype = (long)val;
					break;
				case "В_ПРОДАЖЕ":
					in_sell = (bool)val;
					break;
				case "ДВИГАТЕЛЬ":
					engine = (string)val;
					break;
				case "МАРКА_МОДЕЛЬ_ПТС":
					markmodel = (string)val;
					break;
				case "ТРАНСМИССИЯ":
					trans = (string)val;
					break;
				case "ТИП_ТС":
					type = (string)val;
					break;
				// Временные
				case "НАИМЕНОВАНИЕ":
					tmp_workgroup_name = (string)val;
					break;
				case "ОПИСАНИЕ_ГАРАНТИЯ":
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
				case "МОДЕЛЬ":
					if (_name.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();      // Чтобы сделать однотипным добавление и изменение

			item.Tag = this.code;
			item.Text = this._name;
		}

		public void SetLVItemWide(ListViewItem item)
		{
			item.SubItems.Clear();      // Чтобы сделать однотипным добавление и изменение

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
			node.Text = "";     // Чтобы сделать однотипным добавление и изменение

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
