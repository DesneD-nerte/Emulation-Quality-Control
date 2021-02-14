using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    public class MyException : Exception
    {
        public MyException (string message) : base(message)
        {

        }
    }
}
