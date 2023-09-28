using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Вызов определенных частей пользовательского интерфейса
	/// Версия - будет позволять переходить к более старшим версиям без переделок в старых частях программы
	/// </summary>
	public class UserInterface
	{
		public enum ClickType: short {Select = 1, Modify = 2};
		public enum Buttons: short {Yes = 1, No = 2};

		public UserInterface()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static object PartnerList(long version, object data, int type, UserInterface.ClickType click_type)
		{
			// Осуществляем выбор контрагента
			FormListPartner dialog = new FormListPartner(type, data);
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			long code = dialog.SelectedCode;
			if(code == 0) return null;
			DtPartner partner = DbSqlPartner.Find(code);
			return partner;
		}
		public static object AutoList(long version, object data, int type, UserInterface.ClickType click_type)
		{
			// Осуществляем выбор автомобиля
			FormListAuto dialog = new FormListAuto(type, data);
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			long code = (long)dialog.Auto.GetData("КОД_АВТОМОБИЛЬ");
			if(code == 0) return null;
			DtAuto auto = DbSqlAuto.Find(code);
			return auto;
		}
		public static string InquiryPrompt(string prompt, string prompt_add)
		{
			UIInquiryPrompt dialog = new UIInquiryPrompt(prompt, prompt_add);
			if(dialog.ShowDialog() != DialogResult.OK) return "";
			return dialog.Inquiry;
		}
		public static UserInterface.Buttons YesNoPrompt(string prompt, string prompt_add)
		{
			UIYesNoPrompt dialog = new UIYesNoPrompt(prompt, prompt_add);
			if(dialog.ShowDialog() != DialogResult.OK) return UserInterface.Buttons.No;
			return dialog.Result;
		}
		public static object LicenceVehicle(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_LicenceVehicle dialog = new UIF_LicenceVehicle(null, "");
			dialog.Show();
			return null;
		}
		public static object LicenceVehicleList(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_LicenceVehicleList dialog = new UIF_LicenceVehicleList(null);
			dialog.Show();
			return null;
		}

		public static string Selector_String(string title, string init_value)
		{
			FormSelectString dialog = new FormSelectString(title, init_value);
			if (dialog.ShowDialog() !=DialogResult.OK) return "";
			return dialog.SelectedText;
		}

		public static int Selector_Int(string title, string init_value)
		{
			FormSelectString dialog = new FormSelectString(title, init_value);
			if (dialog.ShowDialog() != DialogResult.OK) return (int)0;
			string txt = dialog.SelectedText;
			int res = 0;
			try{
				res = Convert.ToInt32(txt);
			}
			catch (Exception e) { return 0; }
			return res;
		}

		public static float Selector_Float(string title, string init_value)
		{
			FormSelectString dialog = new FormSelectString(title, init_value);
			if (dialog.ShowDialog() != DialogResult.OK) return (int)0;
			string txt = dialog.SelectedText;
			float res = 0;
			try
			{
				res = (float)Convert.ToDecimal(txt);
			}
			catch (Exception e) { return 0; }
			return res;
		}


		public static object CardWorkCommentList(long version, object data, int type, UserInterface.ClickType click_type)
		{
			DtCardWork card_work = (DtCardWork)data;
			UIF_CardWorkCommentList dialog = new UIF_CardWorkCommentList(card_work);
			dialog.Show();
			return null;
		}

		public static object AutoAlarm(long version, object data, int type, UserInterface.ClickType click_type)
		{
			long code_auto = (long)data;
			UIF_AutoAlarm dialog = new UIF_AutoAlarm(code_auto);
			dialog.Show();
			return null;
		}

		public static object ListAlarm(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_Alarm_List dialog = new UIF_Alarm_List();
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			DtListAlarm alarm = (DtListAlarm) dialog.selected_alarm;
			return alarm;
		}

		public static object ListAutoComment(long version, object data, int type, UserInterface.ClickType click_type)
		{
			long code_auto = (long)data;
			UIF_AutoComment dialog = new UIF_AutoComment(code_auto);
			dialog.ShowDialog();
			return null;
		}

		public static object ListServiceOuter(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_ServiceOuter_List dialog = new UIF_ServiceOuter_List();
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			DtServiceOuter service = (DtServiceOuter) dialog.service_selected;
			return service;
		}

		public static object ListCommonReason(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_CommonReason_List dialog = new UIF_CommonReason_List();
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			DtCommonReason reason = (DtCommonReason) dialog.selected_reason;
			return reason;
		}

		public static object PTS(long version, object data, int type, UserInterface.ClickType click_type)
		{
			UIF_PTS dialog = new UIF_PTS();
			if(dialog.ShowDialog() != DialogResult.OK) return null;
			CS_PTS pts = (CS_PTS) dialog.selected_pts;
			return pts;
		}

		#region Заполнение листа
		public delegate void DelegateSetListViewItem(object o, ListViewItem lvItem);

		public static void FillList(ListView listView, ArrayList array, DelegateSetListViewItem func)
		{
			foreach (object obj in array)
			{ 
				ListViewItem itm = listView.Items.Add("Error");
				func(obj, itm);
			}
		}

		public static void SetListIndexes(ListView list) // Создание списка номеров в произвольном листе Позиция = индек + 1
        {
			foreach (ListViewItem item in list.Items)
			{
				item.Text = (item.Index + 1).ToString();
			}
		}
		#endregion

		public static object SelectAuto()	// Вызов диалога выбора автомобиля
		{
			FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return null;
			DbAuto dbAuto = dialog.Auto;
			DtAuto dtAuto = DbSqlAuto.Find(dbAuto.Code);
			return (object)dtAuto;
		}

		public static object SelectPartner() // Вызов диалога выбора контрагента возвращает DtPartner 
        {
			// Выбор клиента по данной карточке
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return null;
			if (dialog.Partner == null) return null;
			long code = dialog.Partner.Code;
			if (code == 0L) return null;
			DtPartner dtPartner = DbSqlPartner.Find(code);
			if(dtPartner != null)
            {
				dtPartner.DataComplicityCheck();
				dtPartner.PreselectInfoShow();
            }
			return dtPartner;
		}

		public static object SelectAutoType() // Вызов диалога выбора типа автомобиля DbAutoType 
		{
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return null;
			if (dialog.SelectedAutoType == null) return null;
			long code = dialog.SelectedAutoType.Code;
			if (code == 0L) return null;
			if (Db.ShowFaults()) return null;
			return dialog.SelectedAutoType;
		}

		public static object SelectWorkshop() // Вызов диалога выбора подраздления DtWorkshop 
		{
			ArrayList array = new ArrayList();
			DbSqlWorkshop.SelectInArray(array);
			UIF_Selector_Array dialog = new UIF_Selector_Array(array);
			if (dialog.ShowDialog() != DialogResult.OK) return null;
			DtWorkshop workshop = (DtWorkshop)dialog.SelectedObject;
			return workshop;
		}

		public static void PartnerEditForm(DtPartner partner) // Вызов диалога изменений свойств контрагента типа DtPartner
        {
			if (partner == null) return;
			long partner_code = partner.Code;
			if (partner_code == 0) return;
			FormUpdatePartner form = new FormUpdatePartner(partner_code);
			form.ShowDialog();
		}

		public static object SelectStaff() // Вызов диалога выбора персонала возвращает DtStaff
		{
			FormListStaff dialog = new FormListStaff(2, 0);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return null;
			return dialog.SelectedStaff;
		}

        #region Правильные функции выбоора значений различных типов
		public static object SelectFloat(string message = "", float initialValue = 0.0F)
        {
			FormSelectString dialog = new FormSelectString("Новая стоимость работы", initialValue.ToString());
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return null;
			return dialog.SelectedFloat;
        }
        #endregion
    }
}
