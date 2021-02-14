using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface IDisplay
    {
        public void WriteLine(string message);
        public string ReadLine();
    }
}
