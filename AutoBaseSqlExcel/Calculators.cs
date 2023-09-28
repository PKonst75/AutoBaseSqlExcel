namespace AutoBaseSql
{
    class CalculatorWorkVATIncluded : Calculator // Читсый калькулятор параметров работы - НДС Включен
    {
        public CalculatorWorkVATIncluded(float srcVAT) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            result.SummDatabase = data.SummDatabase();
            result.SummDiscount = data.SummDiscount();
            result.SummBonus = data.SummBonus();
            result.SummTotal = result.SummDatabase - result.SummTotalDiscountBonus;
            result.SummTotalVAT = result.SummTotal;
            result.SummVAT = result.SummTotal * ValueVAT / 100.0F;
            result.SummTotalNoVAT = result.SummTotal - result.SummVAT;
            result.SimpleAmount = data.SimpleAmount();
            result.SimpleAmountUnit = data.SimpleAmountUnit();
            result.Expenses = data.Expenses();
            return result;
        }
    }
    class CalculatorWorkToPayVATIncluded : CalculatorWorkVATIncluded
    {
        public CalculatorWorkToPayVATIncluded(float srcVAT) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            if (data.GuaranteeFlag() == false) return base.Calculate(data);
            else return new CalculatorResult();
        }
    }
    class CalculatorWorkNoVAT : Calculator // Чиствй калькулятор работы без НДС
    {
        public CalculatorWorkNoVAT(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            result.SummDatabase = data.SummDatabase();
            result.SummDiscount = data.SummDiscount();
            result.SummBonus = data.SummBonus();
            result.SummTotal = result.SummDatabase - result.SummTotalDiscountBonus;
            result.SummTotalVAT = result.SummTotal;
            result.SummVAT = 0;
            result.SummTotalNoVAT = result.SummTotal;
            result.SimpleAmount = data.SimpleAmount();
            result.SimpleAmountUnit = data.SimpleAmountUnit();
            result.Expenses = data.Expenses();
            return result;
        }
    }
    class CalculatorWorkToPayNoVAT : CalculatorWorkNoVAT
    {
        public CalculatorWorkToPayNoVAT(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            if (data.GuaranteeFlag() == false) return base.Calculate(data);
            return new CalculatorResult();
        }
    }
    class CalculatorWorkToPayNoVATTO : CalculatorWorkToPayNoVAT
    {
        public CalculatorWorkToPayNoVATTO(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            if (data.GuaranteeFlag() == false && data.WorkType() == WORK_TYPE.TO)
                return base.Calculate(data);
            else
                return new CalculatorResult();
        }
    }
    class CalculatorWorkToPayNoVATWash : CalculatorWorkToPayNoVAT
    {
        public CalculatorWorkToPayNoVATWash(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            if (data.GuaranteeFlag() == false && data.WorkType() == WORK_TYPE.WASH)
                return base.Calculate(data);
            else
                return new CalculatorResult();
        }
    }
    class CalculatorDetailToPayVATIncluded : Calculator
    {
        public CalculatorDetailToPayVATIncluded(float srcVAT) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            if (data.GuaranteeFlag() == false)
            {
                result.SummDatabase = data.SummDatabase();
                result.SummDiscount = data.SummDiscount();
                result.SummBonus = data.SummBonus();
                result.SummTotal = result.SummDatabase - result.SummTotalDiscountBonus;
                result.SummTotalVAT = result.SummTotal;
                result.SummVAT = result.SummTotal * ValueVAT / 100.0F;
                result.SummTotalNoVAT = result.SummTotal - result.SummVAT;
                result.SimpleAmount = data.SimpleAmount();
                result.SimpleAmountUnit = data.SimpleAmountUnit();
                result.Expenses = data.Expenses();
            }
            return result;
        }
    }
    class CalculatorDetailToPayVATIncludedOIL : CalculatorDetailToPayVATIncluded
    {
        public CalculatorDetailToPayVATIncludedOIL(float srcVAT) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            if (data.GuaranteeFlag() == false && data.DetailType() == DETAIL_TYPE.OIL)
                return base.Calculate(data);
            else
                return new CalculatorResult();
        }
    }
    class CalculatorDetailToPayNoVAT : Calculator
    {
        public CalculatorDetailToPayNoVAT(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            if (data.GuaranteeFlag() == false)
            {
                result.SummDatabase = data.SummDatabase();
                result.SummDiscount = data.SummDiscount();
                result.SummBonus = data.SummBonus();
                result.SummTotal = result.SummDatabase - result.SummTotalDiscountBonus;
                result.SummTotalVAT = result.SummTotal;
                result.SummVAT = 0;
                result.SummTotalNoVAT = result.SummTotal;
                result.SimpleAmount = data.SimpleAmount();
                result.SimpleAmountUnit = data.SimpleAmountUnit();
                result.Expenses = data.Expenses();
            }
            return result;
        }
    }
    class CalculatorDetailToPayNoVATOil : CalculatorDetailToPayNoVAT
    {
        public CalculatorDetailToPayNoVATOil(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            if (data.GuaranteeFlag() == false && data.DetailType() == DETAIL_TYPE.OIL)
                return base.Calculate(data);
            else
                return new CalculatorResult();
        }
    }
    class CalculatorDummy : Calculator // Пустой калькулятор - возвращает нули
    {
        public CalculatorDummy(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            return new CalculatorResult();
        }
    }
    class CalculatorDatabase : Calculator // Калькулятор возвращает знгачения как в базе данных (все поотдельности, объемы, скидки и ТД. НДС в расчет не принимается
    {
        public CalculatorDatabase(float srcVAT = 0) : base(srcVAT) { }
        public override CalculatorResult Calculate(Calculatable data)
        {
            CalculatorResult result = new CalculatorResult();
            result.SummDatabase = data.SummDatabase();
            result.SummDiscount = data.SummDiscount();
            result.SummBonus = data.SummBonus();
            result.SummTotal = result.SummDatabase;
            result.SummTotalVAT = 0;
            result.SummVAT = 0;
            result.SummTotalNoVAT = 0;
            result.SimpleAmount = data.SimpleAmount();
            result.SimpleAmountUnit = data.SimpleAmountUnit();
            result.Expenses = data.Expenses();
            return result;
        }
    }


}