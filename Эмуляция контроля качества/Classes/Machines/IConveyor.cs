using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface IConveyor
    {
        public bool CheckPlacesOfConveyor();
        public void PutDetailOn(IDetail detail);
        public void MoveDetails();
        public IDetail GetCurrentDetail();
        public bool DoesConveyorWork();
        public void TurnOn();
        public void TurnOff();
    }
}
