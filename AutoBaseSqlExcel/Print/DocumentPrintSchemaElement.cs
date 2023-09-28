using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AutoBaseSql._Print;

namespace AutoBaseSql.Print
{
    class DocumentPrintSchemaElement
    {
        private readonly DelegatePrintBlock _blockDelegate = null;
        private readonly object _element = null;
        private readonly DelegatePrintBlock _blockHeadDelegate = null;
        private readonly bool _firstFlag = false;
        private readonly int _count = 0;

        public DocumentPrintSchemaElement(DelegatePrintBlock blockDelegate, object element = null, DelegatePrintBlock blockHeadDelegate = null, bool firstFlag = false, int count = 0)
        {
            _blockDelegate = blockDelegate;
            _element = element;
            _blockHeadDelegate = blockHeadDelegate;
            _firstFlag = firstFlag;
            _count = count;
        }
        public DelegatePrintBlock BlockDelegate { get { return _blockDelegate; } }
        public DelegatePrintBlock BlockHeadDelegate { get { return _blockHeadDelegate; }  }
        public object Element { get { return _element; } }
        public bool FirstElementFlag { get{ return _firstFlag; } }
        public int Count { get { return _count; } }
    }
}
