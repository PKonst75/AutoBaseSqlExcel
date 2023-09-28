using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoBaseSql.TxtClasses;

namespace AutoBaseSql.Displaer
{
    class DisplayCard
    {
        private readonly DtCard _card;
        private readonly TxtCard _txt;
        public DisplayCard (DtCard card)
        {
            _card = card;
            _txt = new TxtCard(card);
        }
        public DtCard Card
        {
            get { return _card; }

        }
        public DisplayStruct AutoVIN
        {
            get
            {
                DisplayStruct  ds = new DisplayStruct(_txt.AutoVIN);
                if (ds.Text == "")
                {
                    ds.SetRedText("Автомобиль не выбран");
                }
                return ds;
            }
        }
    }
}
