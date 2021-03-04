using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Emulation_Quality_Control.Classes.Details;

namespace Emulation_Quality_Control.Classes
{
    class CheckMachine : ICheckMachine
    {
        bool IsWork = false;
        CheckerContainer checkerContainer;

        public CheckMachine(CheckerContainer checkerContainer)
        {
            this.checkerContainer = checkerContainer;
        }

        public CheckMachine()
        {
            
        }

        public bool CheckDetail(IDetail detail)
        {
            return checkerContainer.CheckDetail(detail);
        }

        public void TurnOn()
        {
            IsWork = true;
        }

        public void TurnOff()
        {
            IsWork = false;
        }

        public bool DoesCheckMachineWork()
        {
            if (IsWork == true)
            {
                return true;
            }

            return false;
        }

        public void Transit()
        {
            throw new NotImplementedException();
        }

        public void Utilize()
        {
            throw new NotImplementedException();
        }
    }
}
