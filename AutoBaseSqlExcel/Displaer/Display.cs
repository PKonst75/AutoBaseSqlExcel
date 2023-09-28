using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql.Displaer
{
    class Display
    {
        public static void DisplayComponent(Control formComponent, DisplayStruct displayStruct) // Общий метод отображения компонента WindowsForms
        {
            /* Может отображать следующие элементы WindowsForm
             *  LinkLabel
             *  ComboBox - требуется началльное создание
             *  CheckBox
             *  Label
             *  Заголовок формы
             *  
             *  При неизвестной для себя компоненте не делает ничего
             */
            if (displayStruct.Hidden) // Если флаг сокрытия, скрываем компонент
            {
                formComponent.Hide();
                return;
            }
            formComponent.Show();
            switch (formComponent.GetType().Name)
            {
                case "LinkLabel":
                    LinkLabelColoredText((LinkLabel)formComponent, displayStruct);
                    return;
                case "Label":
                    LabelColoredText((Label)formComponent, displayStruct);
                    return;
                //           case "FormCard": // Костыль
                //               FormTitleText((Form)formComponent, displayStruct);
                //               return;
                //           case "ComboBox": // Комбобоксы
                //              ComboboxSelection((ComboBox)formComponent, displayStruct);
                //               return;
                //          case "CheckBox": // Чекбоксы
                //               CheckboxCheck((CheckBox)formComponent, displayStruct);
                //               return;
                //           case "Label": // Лейблы
                //               LabelBackColoredText((Label)formComponent, displayStruct);
                //              return;
                default:
                    return;
            }
        }

        public static void LinkLabelColoredText(LinkLabel linkLabel, DisplayStruct displayStruct) // Отображение структуры в LinkLabel
        {
            linkLabel.Text = displayStruct.Text;
            linkLabel.LinkColor = displayStruct.TextColor;
        }
        public static void LabelColoredText(Label label, DisplayStruct displayStruct) // Отображение структуры в LinkLabel
        {
            label.Text = displayStruct.Text;
            label.ForeColor = displayStruct.TextColor;
        }
        //public static void LabelBackColoredText(Label label, DisplayStruct displayStruct) // Отображение структуры в Label
        //{
        //    label.Text = displayStruct.Text;
        //    if (displayStruct.TextColor != Color.Black)
        //        label.BackColor = displayStruct.TextColor;
        //}

    }
}
