using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface IMachine
    {
        IDetail GetDetail();           // метод для запуска
        int Performance { get; } // производительность станка
        bool DoesMachineWork();   // работает ли машина
        void Stop();           // метод для остановки
        void TurnOn();
        void TurnOff();
    }
}
