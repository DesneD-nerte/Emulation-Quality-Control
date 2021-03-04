using System;
using System.Collections.Generic;
using System.Text;
using Emulation_Quality_Control.Classes;

namespace Emulation_Quality_Control
{
    class ScreenDisplay : IDisplay
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
