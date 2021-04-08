using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emulation_Quality_Control.Classes
{
    interface ICheckMachine
    {
        string Model { get;}
        Task<bool> CheckDetail(IDetail detail, CancellationToken token);
        void TurnOn();
        void TurnOff();
        bool DoesCheckMachineWork();
    }
}
