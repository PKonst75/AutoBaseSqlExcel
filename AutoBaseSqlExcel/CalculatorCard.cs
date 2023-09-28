using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
    public enum VAT_TYPE : ushort {VAT_INCLUDED = 1, VAT_EXCLUDED=2, VAT_NON = 0}
    public enum CALCULATOR_TYPE : ushort { CALCULATOR_DATABASE = 0, CALCULATOR_PAY = 1, CALCULATOR_GUARANTY = 2, CALCULATOR_PAYTO=3, CALCULATOR_PAYWASH = 4, CALCULATOR_PAYOIL=5 }
    public class CalculatorCard
    {
        private VAT_TYPE _typrOfVAT;
        private CALCULATOR_TYPE _typeOfCalculator;
        private float _valueOfVAT;
        private CalculatorResult _worksCalculate = new CalculatorResult(); // Суммы по работам
        private CalculatorResult _detailsCalculate = new CalculatorResult(); // Суммы по деталям
        private CalculatorResult _totalCalculate = new CalculatorResult(); // Общая сумма
        private Calculator _workCalculator = null;
        private Calculator _detailCalculator = null;

        public Calculator WorkCalculator
        {
            get { return _workCalculator; }
        }
        public Calculator DetailCalculator
        {
            get { return _detailCalculator; }
        }

        public CalculatorCard(CALCULATOR_TYPE srcTypeOfCalculator, VAT_TYPE srcTypeOfVAT, float srcValueOfVAT)
        {
            _typrOfVAT = srcTypeOfVAT;
            _valueOfVAT = srcValueOfVAT;

            switch (srcTypeOfCalculator)
            {
                case CALCULATOR_TYPE.CALCULATOR_PAY:
                    if (srcTypeOfVAT == VAT_TYPE.VAT_INCLUDED)
                    {
                        _workCalculator = new CalculatorWorkToPayVATIncluded(_valueOfVAT);
                        _detailCalculator = new CalculatorDetailToPayVATIncluded(_valueOfVAT);
                    }
                    else if(srcTypeOfVAT == VAT_TYPE.VAT_NON)
                    {
                        _workCalculator = new CalculatorWorkToPayNoVAT();
                        _detailCalculator = new CalculatorDetailToPayNoVAT();
                    }
                    break;
                case CALCULATOR_TYPE.CALCULATOR_PAYTO:
                    if (srcTypeOfVAT == VAT_TYPE.VAT_INCLUDED)
                    {
                        //_workCalculator = new CalculatorWorkToPayVATIncluded(_valueOfVAT);
                    }
                    else if (srcTypeOfVAT == VAT_TYPE.VAT_NON)
                    {
                        _workCalculator = new CalculatorWorkToPayNoVATTO();
                    }
                    _detailCalculator = new CalculatorDummy();
                    break;
                case CALCULATOR_TYPE.CALCULATOR_PAYWASH:
                    if (srcTypeOfVAT == VAT_TYPE.VAT_INCLUDED)
                    {
                        //_workCalculator = new CalculatorWorkToPayVATIncluded(_valueOfVAT);
                    }
                    else if (srcTypeOfVAT == VAT_TYPE.VAT_NON)
                    {
                        _workCalculator = new CalculatorWorkToPayNoVATWash();
                    }
                    _detailCalculator = new CalculatorDummy();
                    break;
                case CALCULATOR_TYPE.CALCULATOR_PAYOIL:
                    if (srcTypeOfVAT == VAT_TYPE.VAT_INCLUDED)
                    {
                        //_workCalculator = new CalculatorWorkToPayVATIncluded(_valueOfVAT);
                    }
                    else if (srcTypeOfVAT == VAT_TYPE.VAT_NON)
                    {
                        _detailCalculator = new CalculatorDetailToPayNoVATOil();
                    }
                    _workCalculator = new CalculatorDummy();
                    break;
                default:
                    _workCalculator = new CalculatorDatabase();
                    _detailCalculator = new CalculatorDatabase();
                    break;
            }
            if(_workCalculator == null ) _workCalculator = new CalculatorDummy();
            if (_detailCalculator == null) _detailCalculator = new CalculatorDummy();
        }
        public CalculatorResult CalculateWorkGuaranty(ArrayList srcWorksArray, WORK_TYPE srcTypeOfWork, long srcTypeOfGuaranty)
        {
            ArrayList resultWorks = new ArrayList();
            foreach(DtCardWork work in srcWorksArray)            
                if (work.GuaranteeFlag() && (work.WorkType() == srcTypeOfWork || srcTypeOfWork == WORK_TYPE.NONE) && work.CodeGuarantyType == srcTypeOfGuaranty)
                    resultWorks.Add(work);     
            return WorkCalculator.CalculateSumm(resultWorks);
        }
        public void Calculate(DtCard srcCard)
        {
            // Работы
            srcCard.LoadCardWorksForce();
            srcCard.LoadCardDetailsForce();
            ArrayList worksArray = srcCard.CardWorks;
            _worksCalculate = WorkCalculator.CalculateSumm(worksArray);

            // Детали
            ArrayList detailsArray = srcCard.CardDetails;
            _detailsCalculate = _detailCalculator.CalculateSumm(detailsArray);
        }
        public void Calculate(ListView srcWorkList)
        {
            int count = 0;
            ArrayList worksArray = new ArrayList();
            DbCardWork dbCardWork;
            DtCardWork dtCardWork;
            foreach (ListViewItem lvi in srcWorkList.Items)
            {
                dbCardWork = (DbCardWork)lvi.Tag;
                if (dbCardWork != null)
                {
                    if(dbCardWork.Adding == true)
                    {
                        dtCardWork = new DtCardWork(dbCardWork);
                        worksArray.Add(dtCardWork);
                        count++;
                    }
                    else if (dbCardWork.Deleted == false)
                    {
                        dtCardWork = (DbSqlCardWork.Find(dbCardWork));
                        worksArray.Add(dtCardWork);
                        count++;
                    }
                }
            }
            _worksCalculate = _workCalculator.CalculateSumm(worksArray);
        }

        public float SummWork()
        {
            return _worksCalculate.SummDatabase;
        }
        public float SummDetail()
        {
            return _detailsCalculate.SummDatabase;
        }
        public float SummDetailExpences()
        {
            return _detailsCalculate.Expenses;
        }
        public float SummDetailDiscount()
        {
            return _detailsCalculate.SummTotalDiscountBonus;
        }
        public float SummWorkDiscount()
        {
            return _worksCalculate.SummTotalDiscountBonus;
        }
        public float SummPayDetail()
        {
            return _detailsCalculate.SummTotal;
        }
        public float SummPayWork()
        {
            return _worksCalculate.SummTotal;
        }
        public CalculatorResult WorksTotal
        {
            get { return _worksCalculate; }
        }
        public CalculatorResult DetailsTotal
        {
            get { return _detailsCalculate; }
        }
    }
}
