using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoBaseSql
{
    class Error
    {
        public static bool ErroMessageFalse(string srcMessage)
        {
            MessageBox.Show(srcMessage);
            return false;
        }
        public static void ErroMessageVoid(string srcMessage)
        {
            MessageBox.Show(srcMessage);
        }
        public static void InfoMessageVoid(string srcMessage)
        {
            MessageBox.Show(srcMessage);
        }
    }
}
