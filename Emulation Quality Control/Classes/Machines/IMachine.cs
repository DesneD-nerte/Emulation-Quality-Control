using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    interface IMachine
    {
        string Model { get; }
        IDetail GetDetail();           // метод для запуска
        bool DoesMachineWork();   // работает ли машина
        void Stop();           
        void TurnOn();
        void TurnOff();
    }
}
