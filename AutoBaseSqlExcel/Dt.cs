using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	public interface IDt
    {
		bool DataComplicityCheck();
		void PreselectInfoShow();
    }

	public class DateValuePair
    {
		public enum OBJECTTYPE: ushort {OTYPE_UNSINED=0, OTYPE_SHORT=1, OTYPE_INT=2}
		private DateTime _dateOfValue;
		private object _value;
		private OBJECTTYPE _objectType;

		public DateValuePair(OBJECTTYPE srcOBJECTTYPE)
		{
			_objectType = srcOBJECTTYPE;
			_dateOfValue = DateTime.Now;
			switch (_objectType)
			{
				case OBJECTTYPE.OTYPE_INT:
					_value = (int)0;
					break;
				default:
					_value = null;
					break;
			}
		}
		public void SetInt(DateTime srcDate, int srcInt)
        {
			if (_objectType != OBJECTTYPE.OTYPE_INT) return;
			_dateOfValue = srcDate;
			_value = (object)srcInt;
        }
		public DateTime GetDate()
        {
			return _dateOfValue;
        }
		public int GetInt()
        {
			if (_objectType != OBJECTTYPE.OTYPE_INT) return 0;
			return (int)_value;
        }
    }
	/// <summary>
	/// Общие методы для упрощения
	/// </summary>
	public class Dt
	{
		public delegate void DisplayMemberChange(string memberName = "");
		public event DisplayMemberChange DisplayMemberChanges;

		public void ChangeMember(string name = "")
        {
			IsChg = true;
			DisplayMemberChanges?.Invoke(name);
        }
		private class DtScheme
		{
			public enum FieldType : short { Error = 0, BigInt = 1, Varchar = 2 };
			public enum ParamType : short { Error = 0, Long = 1, String = 2 }

			private class DtSchemeElement
			{
				string field_name;          // Имя поля в базе данных
				string variable_name;       // Имя переменой в процедурах обмена с базой
				FieldType field_type;       // Тип поля в базе данных
				int field_length;           // Длина поля (для varchar)
				ParamType param_type;       // Тип параметра в программе

				public DtSchemeElement(string fn, string vn, FieldType ft, int fl, ParamType pt)
				{
					field_name = fn;
					variable_name = vn;
					field_type = ft;
					field_length = fl;
					param_type = pt;
				}

				public static DtSchemeElement MakeLong(string fn, string vn)
				{
					return new DtSchemeElement(fn, vn, FieldType.BigInt, 0, ParamType.Long);
				}
			}

			ArrayList scheme_elements;

			public DtScheme()
			{
				ArrayList scheme_elements = new ArrayList();
			}

			private void AddLong(string fn, string vn)
			{
				DtSchemeElement obj = DtSchemeElement.MakeLong(fn, vn);
				scheme_elements.Add(obj);
			}

		}
		DtScheme scheme;    // Опсание схемы класса

		public class Pair
        {
			object o;	// Объект
			long code;	// Код
			public Pair(Dt source)
            {
				code = source.Code();
				o = (object)source;
				
            }
			public long Code
            {
                get { return code; }
            }
			public object Obj
            {
                get { return o; }
            }
        }

		private bool isNew = false; // Это новый элеимент - требуется создание
		private bool isDel = false; // Это удаленный элемент - требйется удаление
		private bool isChg = false; // Это измененный элемент - требуется запись
		public Dt()
		{
			// Все флаги ЛОЖЬ в изначальном конструкторе
			//isNew = false;
			//isDel = false;
			//isChg = false;
		}

		public bool IsNew
		{
			get { return isNew; }
            set { isNew = value; }
		}

		public bool IsChg
		{
			get { return isChg; }
			set { isChg = value; }
		}
		public bool IsDel
		{
			get { return isDel; }
			set { isDel = value; }
		}

		virtual public string Title()
		{
			return "";
		}

		virtual public long Code()
		{
			return 0;
		}

		//public delegate void DelegateSetListViewItem(ListViewItem iten, short viewType);
	}
}
