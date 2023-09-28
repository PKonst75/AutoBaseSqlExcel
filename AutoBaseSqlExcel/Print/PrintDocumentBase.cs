using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql.Print
{
    public class PrintDocumentBase
    {

        public PrintDrawingBase _printTools;
        public PrintDocumentBase()
        {

        }
        public virtual DocumentPrintSchema CreatePrintingSchema() { return null; }

        public virtual float PrintDocumentTitle(float y)
        {
            return 0;
        }

        public PrintDrawingBase GetPrintTool() 
        {
            return _printTools;
        }
        public void SetPrintToolGraphics(System.Drawing.Graphics graphics)
        {
            _printTools.SelectGraph(graphics);
        }
		public string LogoFileName(DtBrand.DIALER srcDialer) // Возвращает логитип для печати на карточке в зависимости от бренда
		{

			switch (srcDialer)
			{
				case DtBrand.DIALER.lada:
					return "logo_lada.bmp";
				case DtBrand.DIALER.chevrolet:
					return "logo_chevrolet.bmp";
				case DtBrand.DIALER.unknown:
				default:
					return "logo_avto.bmp";
			}
		}
		public bool OfficialMark(DtBrand.DIALER srcDialer) // Возвращает логитип для печати на карточке в зависимости от бренда
		{
			switch (srcDialer)
			{
				case DtBrand.DIALER.lada:
				case DtBrand.DIALER.chevrolet:
					return true;
				case DtBrand.DIALER.unknown:
				default:
					return false;
			}
		}
	}
}
