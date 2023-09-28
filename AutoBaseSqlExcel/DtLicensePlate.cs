using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBaseSql
{
    public class DtLicensePlate: Dt
    {
        private string _number;
        private string _region;
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }
        public string Region
        {
            get { return _region; }
            set { _region = value;  }
        }
        public DtLicensePlate(string srcNumber, string srcRegion)
        {
            _number = srcNumber;
            _region = srcRegion;
        }
        public string LicensePlateTxt
        {
            get { return Number + Region; }
        }
    }
}
