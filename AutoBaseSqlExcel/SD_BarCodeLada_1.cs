using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public class SD_BarCodeLada_1
    {
        // КЛАСС описывает и реализует обработку штрих-кода LADA с гарантийного талона
        public bool valid;  // Контролька правильного заполнения штрих-кода
        public string vin;  // Полученный VIN автомобиля
        public string model;  // Строка наименования модели
        public string complect;  // Строка наименования комплектации
        public string variant;  // Строка наименования варианта
        public string engine_model;  // Строка наименования модели двигателя
        public string engine_number;  // Строка номера двигателя
        public string partnumber;  // Строка номера для запчастей
        public string color_code;  // Cтрока кода цвета
        public string pts_series;  // Серия ПТС
        public string pts_number;  // Cтрока номера ПТС
        public SD_BarCodeLada_1(ArrayList data)
		{
			// Анализируем массив данных со штрих-кода
            valid = false;
            if (data.Count < 21) { MessageBox.Show("Неверный формат штрих-кода", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            // 0 - пустой символ
            vin = (string)data[1];  // 1 - вин автомобиля
            model = (string)data[2];  // 2 - наименование модели
            complect = (string)data[3];  // 3 - комплектация (000, 021 и т.д.)
            variant = (string)data[4];  // 4 - вариант (40, 41, 42)
            engine_model = (string)data[5];  // 5 - модель двигателя
            engine_number = (string)data[6];  // 6 - номер двигателя
            partnumber = (string)data[7];  // 7 - номер для запчастей
            color_code = (string)data[8];  // 8 - код цвета
            pts_series = (string)data[9];  // 9 - серия ПТС
            pts_number = (string)data[10];  // 10 - Номер ПТС
            // 11 - пустой символ
            // 12 - пустой символ
            // 13 - пустой символ
            // 14 - пустой символ
            // 15 - пустой символ
            // 16 - пустой символ
            // 17 - пустой символ
            // 18 - пустой символ
            // 19 - пустой символ
            // 20 - пустой символ
            // 21 - пустой символ
    
            valid = true;
		}
    }
}
