using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
    // Описания класса делегата - превращение объекта Dt в стандартную структуру отображения
    delegate FormDisplay.FormDisplayStruct DtDisplayMethod(Dt dt);

    // Отображение Данных  Dt в формах
    class FormDisplay
    {
        // Стандартизованныый класс структура для отображение в элементах формы
        public class FormDisplayStruct
        {
            public enum TYPE_YES_NO: short {YES = 1, NO = 0};
            string text = "";
            Color textColor = Color.Black;
            private FormDisplayStruct _nextElement = null; // Для возможности отображния комбобоксов и списков
            private Enum _enumData; // Для возможности сохранения данных энумератора
            private bool _hidden = false; // Если необходимо скрыть элемент

            public string Text // Получение/Запись поля Текст
            {
                get { return text; }
                set { text = value; }
            }
            public Color TextColor // Получение/запись поля Цвет
            {
                get { return textColor; } 
                set { textColor = value; } 
            }
            public FormDisplayStruct Next // Получение/запись поля Следующий элемент
            {
                get { return _nextElement; }
                set { _nextElement = value; }
            }
            public Enum EnumData // Получение/запись поля Данные энумератора
            {
                get { return _enumData; }
                set { _enumData = value; }
            }
            public override string ToString()
            {
                return Text;
            }
            public bool Hidden
            {
                get { return _hidden; }
                set { _hidden = value; }
            } // Геттер/ Сеттер для флага спрятанного поля

            public void Hide()
            {
                Hidden = true;
            }
            public void Show()
            {
                Hidden = false;
            }

            private void SetColorText(string srcText, Color srcColor) // Установка текст и цвета
            {
                Text = srcText;
                TextColor = srcColor;
            }
            public void SetText(string srcText) // Установка текста. Цвет черный
            {
                Text = srcText;
                TextColor = Color.Black;
            }
            public void SetRedText(string srcText) // Установка текста. Цвет красный
            {
                SetColorText(srcText, Color.Red);
            }
            public void SetGreenText(string srcText) // Установка текста. Цвет зеленый
            {
                SetColorText(srcText, Color.Green);
            }
            public void SetBlueText(string srcText) // Установка текста. Цвет синий
            {
                SetColorText(srcText, Color.Blue);
            }
            public void SetYellowText(string srcText) // Установка текста. Цвет желтый
            {
                SetColorText(srcText, Color.Yellow);
            }
            public void SetOrangeText(string srcText) // Установка текста. Цвет оранежевый
            {
                SetColorText(srcText, Color.Orange);
            }
            public void SetYesEnumData()
            {
                EnumData = TYPE_YES_NO.YES;
            }
            public void SetNoEnumData()
            {
                EnumData = TYPE_YES_NO.NO;
            }
        }

        public static FormDisplayStruct ErrorStruct(string srcText) // Возвращает структуру отображения ошибки (цвет текста красный)
        {
            FormDisplayStruct displayStruct = new FormDisplayStruct();
            displayStruct.SetRedText(srcText);
            return displayStruct;
        }
        public static void DisplayComponent(object srcComponent, FormDisplayStruct srcDisplayStruct) // Общий метод отображения компонента WindowsForms
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
            if (srcDisplayStruct.Hidden) // Если флаг сокрытия, скрываем компонент
            {
                ((Control)srcComponent).Hide();
                return;
            }
           ((Control)srcComponent).Show();
            switch (srcComponent.GetType().Name)
            {
                case "LinkLabel":
                    LinkLabelColoredText((LinkLabel)srcComponent, srcDisplayStruct);
                    return;
                case "FormCard": // Костыль
                    FormTitleText((Form)srcComponent, srcDisplayStruct);
                    return;
                case "ComboBox": // Комбобоксы
                    ComboboxSelection((ComboBox)srcComponent, srcDisplayStruct);
                    return;
                case "CheckBox": // Чекбоксы
                    CheckboxCheck((CheckBox)srcComponent, srcDisplayStruct);
                    return;
                case "Label": // Лейблы
                    LabelBackColoredText((Label)srcComponent, srcDisplayStruct);
                    return;
                default:
                    return;
            }
        }
        // Методы отображение в различные элементы Windows Forms
        public static void LinkLabelColoredText(LinkLabel label, FormDisplayStruct srcDisplayStruct) // Отображение структуры в LinkLabel
        {
            label.Text = srcDisplayStruct.Text;
            label.LinkColor = srcDisplayStruct.TextColor;
        }
        public static void LabelBackColoredText(Label label, FormDisplayStruct srcDisplayStruct) // Отображение структуры в Label
        {
            label.Text = srcDisplayStruct.Text;
            if(srcDisplayStruct.TextColor != Color.Black)
                label.BackColor = srcDisplayStruct.TextColor;
        }
        public static void FormTitleText(Form srcForm, FormDisplayStruct srcDisplayStruct) // Отображение структуры в заголовок Windows Form
        {
            srcForm.Text = srcDisplayStruct.Text;
        }
        public static void ComboboxSelection(ComboBox box, FormDisplayStruct srcDisplayStruct) // Выбор нужного элемента комбобокс
        {
            FormDisplayStruct displayStruct;
            foreach (object o in box.Items)
            {
                displayStruct = (FormDisplayStruct)o;
                if(displayStruct.EnumData.CompareTo(srcDisplayStruct.EnumData) == 0)
                    box.SelectedItem = o;
            }
        }
        public static void CheckboxCheck(CheckBox box, FormDisplayStruct srcDisplayStruct) // Выбор нужного элемента комбобокс
        {
            if ((FormDisplayStruct.TYPE_YES_NO)srcDisplayStruct.EnumData == FormDisplayStruct.TYPE_YES_NO.YES)
                box.Checked = true;
            else
                box.Checked = false;
        }
        public static void InitCombobox(ComboBox  box, FormDisplayStruct srcDisplayStruct) // Отображение структуры в LinkLabel
        {
            FormDisplayStruct next = srcDisplayStruct;
            while (next != null)
            {   
                box.Items.Add(next);
                next = next.Next;
            }
            if(box.Items.Count > 0)
                box.SelectedIndex = 0;
        }
    }
}
