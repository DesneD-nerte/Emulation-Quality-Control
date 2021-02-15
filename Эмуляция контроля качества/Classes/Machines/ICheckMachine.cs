using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface ICheckMachine
    {
        bool CheckDetail(IDetail detail);
        bool IsWork { get; }
        void Transit();
        void Utilize();

        void TurnOn();
        void TurnOff();
    }
}
