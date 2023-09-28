using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoBaseSql
{
    // Класс отвечает за отображение пареметров объектов типа DtPartner в разные типы текста
    class DisplayDtPartner
    { 
          private DtPartner _dtPartner;
        public DisplayDtPartner(DtPartner srcDtPartner)
        {
            _dtPartner = srcDtPartner;
        }
        public DtPartner Partner
        {
            get { return _dtPartner; }
        }
    
        public  FormDisplay.FormDisplayStruct Contacts()
        {
            FormDisplay.FormDisplayStruct displayStruct = new FormDisplay.FormDisplayStruct();  
            if(Partner != null)
                displayStruct.Text = "Телефоны: " + Partner.GetPhone() + " / " + "E-MAIL: " + Partner.GetMail();
            displayStruct.TextColor = Color.Green;
            return displayStruct;
        }

        public static string TitleAddress(DtPartner srcPartner)
        {
            string str = "Не выбран";
            if (srcPartner != null)
                str = srcPartner.GetTitle() + " / " + srcPartner.GetAddress();        
            return str;
        }
    }
}
