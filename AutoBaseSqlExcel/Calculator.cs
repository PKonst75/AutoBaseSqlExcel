using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AutoBaseSql
{
    // Класс описывающий результат вычислений с конкретными параметрами!
    // Например Работы по гарантии или работы оплачиваемые
    // НДС включен, НДС сверху или БЕЗ НДС
    public class CalculatorResult
    {
        private float _summDatabase; // Сумма всего - как в базе данных без применения скидок и НДС
        private float _summDiscount; // Сумма скидок всего (исключая бонусы, 100% скидки)
        private float _summBonus; // Сумма бонусов, скидок 100%
        private float _summTotal; // Сумма всего - как в базе данных с применением скидок и бонусов
        private float _priceUnit; // Цена за одну единицу
        private float _valueVAT; // Станвка НДС
        private float _summVAT; // Сумма НДС
        private float _summTotalVAT; // Сумма к оплате всего с НДС
        private float _summTotalNoVAT; // Сумма к оплате всего с НДС
        private float _expenses; // Сумма затрат на позицию

        private float _simpleAmount;
        private float _simpleAmountUnit;

        public float SummVAT
        {
            set{ _summVAT = value; }
            get { return _summVAT; }
        }
        public float SummDatabase
        {
            set { _summDatabase = value; }
            get { return _summDatabase; }
        }
        public float SummTotal
        {
            set { _summTotal = value; }
            get { return _summTotal; }
        }
        public float SummBonus
        {
            set { _summBonus = value; }
            get { return _summBonus; }
        }
        public float SummDiscount
        {
            set { _summDiscount = value; }
            get { return _summDiscount; }
        }
        public float SummTotalNoVAT
        {
            set { _summTotalNoVAT = value; }
            get { return _summTotalNoVAT; }
        }
        public float SummTotalVAT
        {
            set { _summTotalVAT = value; }
            get { return _summTotalVAT; }
        }
        public float SummTotalDiscountBonus
        {
            get { return _summDiscount + _summBonus; }
        }
        public float SimpleAmount
        {
            set { _simpleAmount = value; }
            get { return _simpleAmount; }
        }
        public float SimpleAmountUnit
        {
            set { _simpleAmountUnit = value; }
            get { return _simpleAmountUnit; }
        }
        public float Expenses
        {
            set { _expenses = value; }
            get { return _expenses; }
        }

        public static CalculatorResult operator +(CalculatorResult left, CalculatorResult right)
        {
            CalculatorResult result = new CalculatorResult();
            result._summDatabase = left._summDatabase + right._summDatabase;
            result._summDiscount = left._summDiscount + right._summDiscount;
            result._summBonus = left._summBonus + right._summBonus;
            result._summTotal = left._summTotal + right._summTotal;
            result._summVAT = left._summVAT + right._summVAT;
            result._summTotalVAT = left._summTotalVAT + right._summTotalVAT;
            result._summTotalNoVAT = left._summTotalNoVAT + right._summTotalNoVAT;
            result._simpleAmount = left._simpleAmount + right._simpleAmount;
            result._simpleAmountUnit = left._simpleAmountUnit + right._simpleAmountUnit;
            result._expenses = left._expenses + right._expenses;
            return result;
        }
    }
    public abstract class Calculator
    {
        private float _valueVAT; // Ставка НДС
        public float ValueVAT
        {
            get { return _valueVAT; }
        }
        public Calculator(float srcVAT)
        {
            _valueVAT = srcVAT;
        }
        abstract public CalculatorResult Calculate(Calculatable data);
        //public CalculatorResult CalculateSumm(Calculatable[] elements) // Суммирует вычисления по массиву вычисляемых елементов
        public CalculatorResult CalculateSumm(ArrayList elements) // Суммирует вычисления по массиву вычисляемых елементов
        {
            CalculatorResult result = new CalculatorResult();
            if (elements == null) return result;
            foreach(Calculatable calc in elements)
            {
                result += Calculate(calc);
            }
            return result;
        }
    }
    public interface Calculatable
    {
        float PriceUnit();  // Цена за единицу - длярассчитываемого параметра
        float TotalAmount(); // Количество всего - для расситываемого параметра
        float SummDatabase(); // Прямая сумма без учета модификаторов - для рассчитываемого параметра
        float SummDiscount(); // Прямая скидка - для рассчитываемого параметра
        float SummBonus(); // Сумма бонуса - для рассчитваемого параметра
        float SimpleAmount(); // Количество / количество операций
        float SimpleAmountUnit();  // Количество в единицах измерения
        bool GuaranteeFlag();
        WORK_TYPE WorkType();
        DETAIL_TYPE DetailType();
        float Expenses(); // Стоимость затрат
    }
}
