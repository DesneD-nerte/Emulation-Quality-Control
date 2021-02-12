using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface ICheckMachine
    {
        bool Check(IDetail detail);
        void Transit();
        void Utilize();

    }
}
