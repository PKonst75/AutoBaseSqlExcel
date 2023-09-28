using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoBaseSql.TxtClasses
{
    class TxtCard:INotifyPropertyChanged
    {
        private readonly DtCard _card;
        public event PropertyChangedEventHandler PropertyChanged;

        public TxtCard(DtCard card)
        {
            _card = card;
        }

        public string AutoVIN
        {
            get
            {
                if (_card.CodeAuto == 0) return "";
                if (_card.Auto == null) return "Ошибка загрузки автомобиля";
                return _card.Auto.VIN;
            }
        }
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
    }
}
