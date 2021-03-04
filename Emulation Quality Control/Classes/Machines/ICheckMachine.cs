using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    interface ICheckMachine
    {
        bool CheckDetail(IDetail detail);
        void TurnOn();
        void TurnOff();
        bool DoesCheckMachineWork();
        void Transit();
        void Utilize();
    }
}
