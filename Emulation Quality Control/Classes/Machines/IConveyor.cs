using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    interface IConveyor
    {
        string Model { get; }
        public bool CheckPlacesOfConveyor();
        public void PutDetailOn(IDetail detail);
        public void MoveDetails();
        public IDetail GetCurrentDetail();
        public bool DoesConveyorWork();
        public void TurnOn();
        public void TurnOff();
    }
}
