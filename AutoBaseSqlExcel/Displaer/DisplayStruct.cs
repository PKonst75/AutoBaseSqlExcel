using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql.Displaer
{
    class DisplayStruct
    {
        string _text = "";                  // Текст для отображения
        Color _textColor = Color.Black;     // Цвет текста при отображении
        private bool _hidden = false;       // Флаг скрытия элемента фори=мы

        public DisplayStruct(string text)
        {
            _text = text;
            _textColor = Color.Black;
        }

        #region геттеры и сеттеры для получения дянных
        public string Text // Получение/Запись поля Текст
        {
            get { return _text; }
            set { _text = value; }
        }
        public Color TextColor // Получение/запись поля Цвет
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        #endregion
    
        #region Система показа / скрытия элемента формы
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
        #endregion

        #region Система установки текста и его цвета
        private void SetColoredText(string text, Color color) // Установка текст и цвета
        {
            Text = text;
            TextColor = color;
        }
        public void SetTextColor(Color color)
        {
            TextColor = color;
        }
        public void SetTextColorRed()
        {
            SetTextColor(Color.Red);
        }
        public void SetTextColorBlack()
        {
            SetTextColor(Color.Black);
        }
        public void SetText(string text) // Установка текста. Цвет черный
        {
            SetColoredText(text, Color.Black);
        }
        public void SetRedText(string text) // Установка текста. Цвет красный
        {
            SetColoredText(text, Color.Red);
        }
        public void SetGreenText(string text) // Установка текста. Цвет зеленый
        {
            SetColoredText(text, Color.Green);
        }
        public void SetBlueText(string text) // Установка текста. Цвет синий
        {
            SetColoredText(text, Color.Blue);
        }
        public void SetYellowText(string text) // Установка текста. Цвет желтый
        {
            SetColoredText(text, Color.Yellow);
        }
        public void SetOrangeText(string text) // Установка текста. Цвет оранежевый
        {
            SetColoredText(text, Color.Orange);
        }
        #endregion
    }
}
