using System;
using System.Collections.Generic;
using System.Text;
using Эмуляция_контроля_качества.Classes;

namespace Эмуляция_контроля_качества
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
