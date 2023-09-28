using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql.Print
{
    class PrintableTableCell
    {
        private readonly float _width;   // Ширина ячейки
        private readonly string _data;   // данные в ячейке в виде строки
        private readonly float _x;       // Левый верхний угол X (относительно таблицы)
        private float _height;   // Высота ячейки  
        private float _y;       // Левый верхний угол Y (относительно таблицы)

        public PrintableTableCell(float x, float width, float height, string data)
        {
            _x = x;
            _width = width;
            _height = height;
            _data = data;
            _y = 0;
        }

        public string Data
        {
            get { return _data; }
        }
        public float Width
        {
            get { return _width; }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                if (value > _height)
                    _height = value;
            }
        }
        public float Y
        {
            set { _y = value; }
            get { return _y; }
        }
        public float X
        {
//            set { _x = value; }
            get { return _x; }
        }

        // Измеряем и устанавливаем высоту ячейки 
        public float MeasureSetHeight(PrintDrawingBase tools)
        {
            PrintDrawingBase.TEST_LVL testLevel =  tools.TestOn();
            Height = tools.PrintTextBox(Data, 0, 0, Width, Height);
            tools.SetTestLvl(testLevel);
            return Height;
        }

        public float Print(float x, float y, PrintDrawingBase tools, BORDER border)
        {
            if (tools.TestFlag()) return Height;
            switch (border)
            {
                case BORDER.ALL:
                    return tools.PrintTextBox(Data, x + X, y + Y, Width, Height);
                case BORDER.NONE:
                    return tools.PrintText(Data, x + X, y + Y, Width, Height);
                default:
                    return tools.PrintTextBox(Data, x + X, y + Y, Width, Height);
            }
        
        }

    }
}
