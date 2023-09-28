using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql.Print
{

    public enum BORDER:short { ALL = 0, NONE = 1};
    class PrintableTable
    {
        private readonly int _rowsCount;         // Количество строк
        private readonly int _columnsCount;      // Количество колонок
        private readonly float[] _columnsWidths; // Массив ширин колонок таблицы
        private float _rowsHeight;      // Минимальная высота строки данной таблицы
        private readonly PrintableTableRow[] _rows;       // Массив строк таблицы
        private bool _measured = false; // Произошло ли измерение таблицы
        private float _height;          // Измеренная высота таблицы
        private BORDER _border = BORDER.ALL;

        public PrintableTable(int rowsCount, int columnsCount)
        {
            _columnsCount = columnsCount;
            _rowsCount = rowsCount;
            _columnsWidths = new float[_columnsCount];
            _rows = new PrintableTableRow[_rowsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                _rows[i] = new PrintableTableRow(i + 1, _columnsCount, _rowsHeight);
            }
        }

        public void SetColumnWidth(int column, float width)
        {
            if (column <= _columnsWidths.Count())
            {
                _columnsWidths[column - 1] = width;
            }
        }
        public float RowsHeight
        {
            set
            {
                _rowsHeight = value;
                for (int i = 1; i <= _rowsCount; i++)
                {
                    GetRow(i).Height = _rowsHeight;
                }
            }
            get { return _rowsHeight; }
        }

        public void MesureTable(PrintDrawingBase tools)
        {
            if (_measured) return;
            float y = 0;
            float rowHeight;
            foreach (PrintableTableRow row in _rows)
            {
                rowHeight = row.MeasureSetRowHeight(y, tools);
                y += rowHeight;
                _height += rowHeight;
            }
            _measured = true;
        }

        public PrintableTableRow GetRow(int row)
        {
            return _rows[row - 1];
        }
        public float GetColumnWidth(int column)
        {
            return _columnsWidths[column - 1];
        }

        public float GetColumnX(int column)
        {
            float x = 0;
            for (int i = 1; i < column; i++)
            {
                x += GetColumnWidth(i);
            }
            return x;
        }
        public float GetMergeColumnWidth(int column, int mergeCount)
        {
            float width = 0;
            for (int i = column; i < mergeCount + column; i++)
            {
                width += GetColumnWidth(i);
            }
            return width;
        }

        #region добавление данных в таблицу
        public void AddCell(int row, int column, string data)
        {
            PrintableTableRow tableRow = GetRow(row);
            tableRow.AddCell(data, GetColumnWidth(column), column, GetColumnX(column));
        }
        public void AddMergedCell(int row, int column, string data, int mergeCount)
        {
            PrintableTableRow tableRow = GetRow(row);
            tableRow.AddCell(data, GetMergeColumnWidth(column, mergeCount), column, GetColumnX(column));
        }
        #endregion

        public float Print(float x, float y, PrintDrawingBase tools)
        {
            if (tools.TestFlag()) return _height;
            foreach (PrintableTableRow row in _rows)
            {
                row.Print(x, y, tools, _border);
            }
            return _height;
        }

        public void SetReminderRowHeight(int rowNumber, float commonHeight)
        {
            float reminderHeight = commonHeight;
            foreach (PrintableTableRow row in _rows)
            {
                if (row.Row != rowNumber)
                    reminderHeight -= row.Height;
            }
            GetRow(rowNumber).SetRowHeight(reminderHeight);
        }

        public void SetBorder(BORDER border)
        {
            _border = border;
        }
    }
}
