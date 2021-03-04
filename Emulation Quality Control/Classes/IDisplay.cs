using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    interface IDisplay
    {
        public void WriteLine(string message);
        public string ReadLine();
    }
}
