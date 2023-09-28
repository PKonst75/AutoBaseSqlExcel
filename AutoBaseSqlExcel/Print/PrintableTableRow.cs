using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AutoBaseSql.Print
{
    class PrintableTableRow
    {
        private readonly int _row;             // Номер строки - задается при инициализации
        private readonly int _columnsCount;    // Количество колонок строки (задается при инициализации от таблицы)
        private readonly ArrayList[] _cells;             // Фиксированный по длине (количество колонок) массив ячеек строки  
        private readonly int[] _cellsMergeCount;           // Фиксированный по длине (количество колонок) массив счетчика объединений ячеек по высоте
        private float[] _cellsHeights;         // Высоты ячеек с учетом объединения
        private float _height;                  // Высота строки (изначально задается как минисальная высота одной строки

        public PrintableTableRow(int row, int columnsCount, float height)
        {
            _row = row;
            _columnsCount = columnsCount;
            _cells = new ArrayList[_columnsCount];
            _cellsMergeCount = new int[_columnsCount];
            _height = height;
            for (int i = 0; i < ColumnsCount; i++)
            {
                _cells[i] = new ArrayList();
            }
        }

        public float Height
        {
            get { return _height; }
            set
            {
                if (_height < value)
                    _height = value;
            }
        }

        public float Row
        {
            get { return _row; }
        }


        public int ColumnsCount
        {
            get { return _columnsCount; }
        }

        #region Добавление данных
        public void AddCell(string data, float width, int column, float x)
        {
            PrintableTableCell cell = new PrintableTableCell(x, width, _height, data);

            if (_cells[column - 1] == null)
                _cells[column - 1] = new ArrayList();
            _cells[column - 1].Add(cell);
            _cellsMergeCount[column - 1] += 1;
        }
        #endregion

        // Получение списка ячеек в заданной колонке ряда таблицы
        // Нумерация колонок с единицы
        public ArrayList Cells(int column)
        {
            if (column < 1 || column > _cells.Length) return null; // Выход за границы массива
            return _cells[column - 1];
        }

        #region Измерение и установка высот
        // Измеряем высоту ряда (и минимальные возможные высоту ячеек в колонках ряда)
        // Нумерация колонок у нас с единицы!!!!
        public float MeasureSetRowHeight(float y, PrintDrawingBase tools)
        {
            // Инициализируем массив максимальных высот объединенных ячеек
            int maxMerge = 0;
            for (int i = 1; i <= ColumnsCount; i++)
            {
                if(maxMerge < _cellsMergeCount[i - 1])
                    maxMerge = _cellsMergeCount[i - 1];       
            }
            _cellsHeights = new float[maxMerge];
            for (int i = 0; i < maxMerge; i++)
            {
                _cellsHeights[i] = Height;
            }

            for (int i = 1; i <= ColumnsCount; i++)
            {
                MeasureSetRowColumnHeight(i, tools);
            }
            for (int i = 1; i <= ColumnsCount; i++)
            {
                PositionMesuredRowCells(y);
            }
            return Height;
        }
        // Измеряем все ячейки в колонке ячеек ряда и при необходимости увеличиваем итоговую высоту колонки
        // Нумерация колонок с единицы!!!
        private float MeasureSetRowColumnHeight(int column, PrintDrawingBase tools)
        {
            // На всякий случай проверки
            ArrayList cells = Cells(column);
            if (cells == null) return 0;

            int merge = 0;
            float mergeHeight = 0;
            foreach (PrintableTableCell cell in cells)
            {
                if (cell != null)
                {
                    cell.Height = _cellsHeights[merge];
                    cell.Height = cell.MeasureSetHeight(tools);
                    mergeHeight += cell.Height;
                    if (_cellsHeights[merge] < cell.Height)
                        _cellsHeights[merge] = cell.Height;
                    merge++;
                }
            }
            Height = mergeHeight;
           // CellMergeMax = merge;
            return Height;
        }

        #endregion

        // Позиционируем ячейки в колонке ряда исходя из предидущих замеров
        private void PositionMesuredRowColumnCells(int column, float y)
        {
            // На всякий случай проверки
            ArrayList cells = Cells(column);
            if (cells == null) return;

            int merge = 0;
            foreach (PrintableTableCell cell in cells)
            {
                if (cell != null)
                {
                    merge++;
                }
            }
            float mergeHeight = Height / merge;
            if (merge != 1)
            {
                // Если нет объединения ячеек, экономим время
                //float mergeHeight = Height / merge;
                merge = 0;
                float noMergeHeight = 0;
                foreach (PrintableTableCell cell in cells)
                {
                    if (cell != null)
                    {
                        if (cell.Height <= mergeHeight)
                        {
                            merge++;
                        }
                        else
                        {
                            noMergeHeight += cell.Height;
                        }
                    }
                }
                mergeHeight = (Height - noMergeHeight) / merge;
            }
            
            foreach (PrintableTableCell cell in cells)
            {
                if (cell != null)
                {
                    cell.Y = y;
                  //  cell.X = x;
                    cell.Height = mergeHeight;
                    y += cell.Height;            
                }
            }   
        }
        // Позиционируем ячейки в ряду исходя из предидущих замеров
        public float PositionMesuredRowCells(float y)
        {
            for (int i = 1; i <= _columnsCount; i++)
            {
                PositionMesuredRowColumnCells(i, y);
            }
            return Height;
        }

        public void PrintRowColumn(float x, float y, int column, PrintDrawingBase tools, BORDER border)
        {
            foreach (PrintableTableCell cell in Cells(column))
            {
                if (cell != null)
                {
                    cell.Print(x, y, tools, border);
                }
            }
        }

        public float Print(float x, float y, PrintDrawingBase tools, BORDER border)
        {
            if (tools.TestFlag()) return Height;
            for (int i = 1; i <= _columnsCount; i++)
            {
                PrintRowColumn(x, y, i, tools, border);
            }
            return Height;
        }

        public float SetRowHeight(float height)
        {
            for (int i = 1; i <= _columnsCount; i++)
            {
                SetRowColumnHeight(i, height);
            }
            return Height;
        }

        public void SetRowColumnHeight(int column, float height)
        {
            foreach (PrintableTableCell cell in Cells(column))
            {
                if (cell != null)
                {
                    cell.Height = height;
                }
            }
        }
    }
}
