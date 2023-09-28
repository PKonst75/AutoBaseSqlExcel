using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static AutoBaseSql._Print;

namespace AutoBaseSql.Print
{
    public class DocumentPrintSchema
    {
        public delegate float DelegatePrint(float startY, object headerData);

        private ArrayList _schemaElements = null;
        private DelegatePrint _printHeaderFunction = null;
        private readonly Graphics _graphics;


        public DocumentPrintSchema(Graphics graphics)
        {
            _graphics = graphics;
        }
       
        public DelegatePrint PrintPageHeader
        {
            get { return _printHeaderFunction; }
            set { _printHeaderFunction = value; }
        }
        public ArrayList SchemaElements
        {
            get { return _schemaElements; }
            set { _schemaElements = value; }
        }

    }
}
