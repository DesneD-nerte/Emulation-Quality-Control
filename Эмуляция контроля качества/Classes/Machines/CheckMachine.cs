using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckMachine : ICheckMachine
    {
        public bool IsWork { get; private set; } = false;
        CheckerContainer checkerContainer;
        Conveyor conveyor = new Conveyor();

        public CheckMachine(CheckerContainer checkerContainer)
        {
            this.checkerContainer = checkerContainer;
        }

        public CheckMachine()
        {
            
        }

        public bool CheckDetail(IDetail detail)
        {
            conveyor.PutDetailOn(detail);

            do
            {
                conveyor.MoveDetails();
            }
            while (conveyor.CheckConveyor() == false);

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
