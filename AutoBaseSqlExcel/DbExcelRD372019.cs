using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
    class DbExcelRD372019:DbExcel
    {
        DbExcelRD372019Act act = null;
        DbExcelRD372019CheckOutList checkOutList = null;
        DbExcelRD372019Claim claim = null;
        DbExcelRD372019ContractOrder contractOrder = null;
        DbExcelRD372019WorkOrder workOrder = null;
        DbExcelRD372019Storage storage = null;

        public DbExcelRD372019(DtCard theCard)
        {
            act = new DbExcelRD372019Act(theCard);
            checkOutList = new DbExcelRD372019CheckOutList(theCard);
            claim = new DbExcelRD372019Claim(theCard);
            contractOrder = new DbExcelRD372019ContractOrder(theCard);
            workOrder = new DbExcelRD372019WorkOrder(theCard);
            storage = new DbExcelRD372019Storage(theCard);
        }

        override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
        {
            if (sheet == 1)
            {
                act.FillData(ws);
                return;
            }
            if (sheet == 2)
            {
                claim.FillData(ws);
                return;
            }
            if (sheet == 3)
            {
                workOrder.FillData(ws);
                return;
            }
            if (sheet == 4)
            {
                contractOrder.FillData(ws);
                return;
            }
            if (sheet == 5)
            {
                checkOutList.FillData(ws);
                return;
            }
            if (sheet == 6)
            {
                storage.FillData(ws);
                return;
            }
        }
    }
}
