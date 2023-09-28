using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� ����������.
	/// </summary>
	public class DtAuto : Dt
	{

		// ��������� ����������� ����� ����������
		public class DtAutoTxt
		{
			// ������
			public readonly string vin;
			public readonly string year;
			public readonly string licence_plate;
			public readonly string sell_date;
			// ����������
			public readonly string model;
			public DtAutoTxt(DtAuto auto)
			{
				if (auto == null) return;
				// ������
				if (auto.is_id_vin == false)
					vin = auto.id_vin;
				else
					vin = "����������";
				year = auto.year.ToString();
				if (auto.licence_plate_number != "")
					licence_plate = auto.licence_plate_number + " " + auto.licence_plate_region;
				else
					licence_plate = auto.id_sign;

				if (auto.ext_is_sell_date)
					sell_date = auto.ext_sell_date.ToShortDateString();
				else
					sell_date = "-";
				// ����������
				DtModel m = DbSqlModel.Find(auto.code_model);
				if (m == null) model = "NO MODEL";
				else model = m.Txt();
			}
		}

		DtLicensePlate _licensePlate = null;
		DtModel _tmpModel = null;

		long code;          // ���������� ��� ���������� - �� ������
							// ����������������� ������
		private long code_model;        // ��� ������ ����������
		private string id_vin;          // VIN - ���������� VIN ����������
		private bool is_id_vin;     // ���� �� VIN � ����������
		private string id_vin_origin;   // �������� VIN ������������� ����������
		private string id_body;     // ����� ������ ����������
		private string id_frame;        // ����� ����� ����������
		private string id_engine;       // ����� ��������� ����������
		private long id_parts;      // ����� ��������� ����������, ���� ����
		private bool is_id_parts;   // ���� �� ����� ��������� ����������


		// �������������� ������
		int year;           // ��� �������
		long code_producer; // ��� ������������� ����������
		long code_color;        // ��� ����� ����������
		long code_variant;  // ��� ���������� ����������
							// ��������������� ������
		string id_sign;     // ��������������� ���� ����������
							// ��������������
		string comment;             // ����������, �������������� ������
		string licence_plate_number;    // ����� ���-�����
		string licence_plate_region;    // ������ �����
		bool is_pts;                    // ���� �� ���
		float color_price;          // ������� �� ����

		string tmp_model_name;      // ������������ ������
		string tmp_factory_name;    // ������������ ������������� ����������
		string tmp_color_name;      // ������������ �����
		string tmp_variant_name;    // ������������ ����������


		string ext_receive_comment; // ���������� ��� ���������, ����������
		DateTime ext_receive_date;      // ����������, ���� ���������
		bool ext_is_receive_date;   // ����������, ���� �� ���� ���������
		bool ext_is_sell_date;      // ����������, ���� �� ���� �������
		DateTime ext_sell_date;         // ����������, ���� �������
		string ext_sell_customer;       // ����������, ����������
		string ext_sell_comment;        // ����������, ������� ����������

		string ext_reserve_info;        // ���������� � ��������������
		float ext_summ_add;         // ����� �����
		float ext_price;              // ���� �� �����-�����

		// ���������� ������
		private DtBrand _autoBrand; // ����� ����������


		public DtAuto()
		{
			code = 0;
			code_model = 0;
			id_vin = "";
			is_id_vin = false;
			id_vin_origin = "";
			id_body = "";
			id_frame = "";
			id_engine = "";
			id_sign = "";
			year = 0;
			code_producer = 0;
			id_parts = 0;
			is_id_parts = false;
			code_color = 0;
			code_variant = 0;
			comment = "";
			is_pts = false;
			color_price = 0.0F;

			licence_plate_number = "";
			licence_plate_region = "";

			tmp_model_name = "";
			tmp_factory_name = "";

			ext_receive_comment = "";
			ext_receive_date = DateTime.Now;
			ext_is_receive_date = false;
			ext_sell_comment = "";
			ext_sell_customer = "";
			ext_sell_date = DateTime.Now;
			ext_is_sell_date = false;

			ext_reserve_info = "";
			ext_summ_add = 0.0F;
			ext_price = 0.0F;

			_autoBrand = null;
		}

		public DtBrand AutoBrand
		{
            get
            {
				if (_autoBrand != null) return _autoBrand;
				if (code_model == 0) return null;
				return _autoBrand = DbSqlBrand.FindModel(code_model);
			}
		}

		public DtLicensePlate LicensePlate
        {
            get
			{ 
				if(_licensePlate == null)
					_licensePlate = new DtLicensePlate(licence_plate_number, licence_plate_region);
				return _licensePlate;
			}
            set
            {
				licence_plate_number = value.Number;
				licence_plate_region = value.Region;
				_licensePlate = value;
            }
        }

		public DtModel AutoModel
        {
            get
            {
				if (code_model == 0) return null;
				if(_tmpModel == null)
					_tmpModel = DbSqlModel.Find(code_model);
				return _tmpModel;
            }
        }
		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������":
					return (object)(long)code;
				// ����������������� ������
				case "������_���_����������_������":
					return (object)(long)code_model;
				case "VIN":
					return (object)(string)id_vin;
				case "VIN_�����������":
					return (object)(bool)is_id_vin;
				case "VIN_�������������":
					return (object)(string)id_vin_origin;
				case "�����_�����":
					return (object)(string)id_body;
				case "�����_�����":
					return (object)(string)id_frame;
				case "�����_���������":
					return (object)(string)id_engine;
				case "�����_���������_����������":
					return (object)(long)id_parts;
				case "�����_���������_�����������_����������":
					return (object)(bool)is_id_parts;
				// �������� ����������
				case "���_������":
					return (object)(int)year;
				case "���_�������������_����������":
					return (object)(long)code_producer;
				case "������_���_����������_����":
					return (object)(long)code_color;
				case "������_���_����������_����������":
					return (object)(long)code_variant;
				// ���������������
				case "�����_����":
					return (object)(string)id_sign;
				// ��������������
				case "����������":
					return (object)(string)comment;
                case "���":
                    return (object)(bool)is_pts;
                case "�������_����":
                    return (object)(float)color_price;
				// ���������������
                case "�����_����_�����":
					return (object)(string)licence_plate_number;
				// ���������������
				case "�����_����_������":
					return (object)(string)licence_plate_region;
				// ���������������
				case "������":
					return (object)(string)tmp_model_name;
				case "�������������":
					return (object)(string)tmp_factory_name;
				case "����������_����":
					return (object)(string)tmp_color_name;
				case "����������_����������":
					return (object)(string)tmp_variant_name;

				// �����������
				case "���������_����������":
					return (object)(string)ext_receive_comment;
				case "���������_����":
					return (object)(DateTime)ext_receive_date;
				case "����_���������_����":
					return (object)(bool)ext_is_receive_date;
				case "�������_����������":
					return (object)(string)ext_sell_comment;
				case "�������_����������":
					return (object)(string)ext_sell_customer;
				case "�������_����":
					return (object)(DateTime)ext_sell_date;
				case "����_�������_����":
					return (object)(bool)ext_is_sell_date;

				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������":
					code = (long)val;
					break;
				// ����������������� ������
				case "������_���_����������_������":
					code_model = (long)val;
					break;
				case "VIN":
					id_vin = (string)val;
					break;
				case "VIN_�����������":
					is_id_vin = (bool)val;
					break;
				case "VIN_�������������":
					id_vin_origin = (string)val;
					break;
				case "�����_�����":
					id_body = (string)val;
					break;
				case "�����_�����":
					id_frame = (string)val;
					break;
				case "�����_���������":
					id_engine = (string)val;
					break;
				case "�����_���������_����������":
					id_parts = (long)val;
					break;
				case "�����_���������_�����������_����������":
					is_id_parts = (bool)val;
					break;
				// �������� ����������
				case "���_������":
					year = (int)val;
					break;
				case "���_�������������_����������":
					code_producer = (long)val;
					break;
				case "������_���_����������_����":
					code_color = (long)val;
					break;
				case "������_���_����������_����������":
					code_variant = (long)val;
					break;
				// ���������������
				case "�����_����":
					id_sign = (string)val;
					break;
					// ���������������
				case "����������":
					comment = (string)val;
					break;
                case "���":
                    is_pts = (bool)val;
                    break;
                case "�������_����":
                    color_price = (float)val;
                    break;
					// ���������������
				case "�����_����_�����":
					licence_plate_number = (string)val;
					break;
				case "�����_����_������":
					licence_plate_region = (string)val;
					break;
				// ���������������
				case "������":
					tmp_model_name = (string)val;
					break;
				case "�������������":
					tmp_factory_name = (string)val;
					break;
				case "����������_����":
					tmp_color_name = (string)val;
					break;
				case "����������_����������":
					tmp_variant_name = (string)val;
					break;
				// �����������
				case "���������_����������":
					ext_receive_comment = (string)val;
					break;
				case "���������_����":
					ext_receive_date = (DateTime)val;
					break;
				case "����_���������_����":
					ext_is_receive_date = (bool)val;
					break;
				case "�������_����������":
					ext_sell_comment = (string)val;
					break;
				case "�������_����������":
					ext_sell_customer = (string)val;
					break;
				case "�������_����":
					ext_sell_date = (DateTime)val;
					break;
				case "����_�������_����":
					ext_is_sell_date = (bool)val;
					break;
				case "������":
					ext_reserve_info = (string)val;
					break;
				case "�����_�����":
					ext_summ_add = (float)val;
					break;
				case "����_�����":
					ext_price = (float)val;
					break;
				default:
					break;
			}
		}

		public long CodeModel
        {
            get { return code_model; }
        }
		public long CodeAutoType
        {
            get
            {
				DtModel dtAutoModel = DbSqlModel.Find(CodeModel);
				return dtAutoModel.CodeAutoType;
            }
        }
		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= code;
			item.Text				= this.tmp_model_name;
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.id_body);
			item.SubItems.Add(this.tmp_factory_name);
			item.SubItems.Add(this.comment);
		}

		public void SetLVItemReceive(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= code;
			item.Text				= (item.Index + 1).ToString();
			item.SubItems.Add(this.tmp_model_name);
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.ext_receive_comment);
		}

		public void SetLVItemStorageV1(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag	= code;
			item.Text	= this.tmp_model_name;
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.id_body);
			if(this.ext_is_receive_date)
				item.SubItems.Add(this.ext_receive_date.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.ext_receive_comment);
			if(this.ext_is_sell_date)
				item.SubItems.Add(this.ext_sell_date.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.ext_sell_customer);
			item.SubItems.Add(this.ext_sell_comment);
		}

		public void SetLVItemStorageV2(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag	= code;
			if(this.ext_is_receive_date)
				item.Text = this.ext_receive_date.ToShortDateString();
			else
				item.Text = "������������";
			item.SubItems.Add(this.tmp_model_name);
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.ext_reserve_info);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.year.ToString());
			if(this.ext_price != 0.0F)
				item.SubItems.Add(Db.CachToTxt(this.ext_price));
			else
				item.SubItems.Add("");
            // �����! ������� �� ����
            if (this.color_price != 0.0F)
                item.SubItems.Add(Db.CachToTxt(this.color_price));
            else
                item.SubItems.Add("");

			string summ_add_txt = "";
			if(this.ext_summ_add == -1.0F)
				summ_add_txt = "������";
			if(this.ext_summ_add > 0.0F)
				summ_add_txt = ext_summ_add.ToString();
			item.SubItems.Add(summ_add_txt);

			string summ_whole_txt = "";
            if ((this.ext_price > 0.0F) && ((this.ext_summ_add > 0.0F) || (this.color_price > 0.0F)))
			{
				summ_whole_txt = (this.ext_price + this.ext_summ_add + this.color_price).ToString();
			}
			item.SubItems.Add(summ_whole_txt);

            string pts_txt = "";
            if (this.is_pts == true)
                pts_txt = "+";
            item.SubItems.Add(pts_txt);
		}

		public string Txt()
		{
			// ��������� ����������� ��������
			string txt;//		= "";
			DtModel model = DbSqlModel.Find(code_model);
			if(model == null) txt = "NO MODEL, ";
			else txt = model.Txt() + ", ";
			if(is_id_vin == false) txt += id_vin + " (" + id_body + ")";
			else txt += id_body;
			return txt;
		}

		public long Code
        {
            get { return code; }
        }
		public string TitleTxt
        {
            get
            {
				//string title = "";
				//title = tmp_model_name + " ";
				string title = tmp_model_name + " ";
				if (!is_id_vin) title += this.id_vin;
				else title +=  "VIN ����������";
				return title;
            }
        }

		public DateTime SellDate
        {
            get
            {
				if (ext_is_sell_date) return ext_sell_date;
				return new DateTime(1, 1, 1);
            }
        }
		public string SellDateTxt
		{
			get
			{
				if (SellDate.Year == 1) return "���";
				return SellDate.ToShortDateString();
			}
		}

		public static DateValuePair ReadDbLastRun(DtAuto auto)
        {
			DateValuePair pair = new DateValuePair(DateValuePair.OBJECTTYPE.OTYPE_INT);
			if (auto == null) return pair;
			DtCard card = DbSqlCard.FindCardAutoRun(auto);
			if (card == null) return pair;
			pair.SetInt(card.Date, card.CardRun);
			return pair;
        }

		public string VIN
        {
            get { return id_vin; }
			set
			{
				//string str = value;
				//str = str.Trim().ToUpper();
				//str = str.ToUpper();
				if (value == "") return;
				if (id_vin == value) return;
				id_vin = value;
				is_id_vin = true;
				IsChg = true;
			}
        }

		public string IdSign
		{
			get
			{
				return id_sign;
			}
		}

        #region ������� ������� � �������
		public int Year
        {
			get { return year; }
        }
		#endregion

		#region ��������� ������������� ����������

		public bool IsBrandLada()
        {
			if (id_vin.StartsWith("XTA")) return true;
			if (id_vin.StartsWith("Z9Z")) return true;
			if (id_vin.StartsWith("XWK")) return true;
			if (id_vin.StartsWith("X9L")) return true;
			if (id_vin.StartsWith("X6D")) return true;
			return false;
		}

		#endregion
	}
}
