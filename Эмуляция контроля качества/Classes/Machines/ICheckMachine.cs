﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
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
