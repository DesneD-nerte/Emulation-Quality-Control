using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества.Classes
{
    class Work
    {
        ICheckMachine checkMachine;
        IMachine machine;
        IDisplay display;
        CheckerContainer checkerContainer;
        int indexOfDetail;

        public Work(IDisplay display)
        {
            this.display = display;
            FullCheckerContainer();
        }

        public void StartWork()
        {
            Ititialize();

            while (CheckWorkingIsTrue() == true)
            {
                indexOfDetail++;

                //IDetail detail = machine.GetDetail();
                IDetail detail = null;
                CheckDetail(detail);

                //IDetail detail1 = null;

                //CheckDetail(detail1);

                Thread.Sleep(1000);
            }
        }

        private void CheckDetail(IDetail detail)
        {
            try
            {
                bool checkedDetail = checkMachine.CheckDetail(detail);

                if (checkedDetail == true)
                {
                    display.WriteLine($"{detail.GetType().Name} №{indexOfDetail} is fine");
                }
                else
                {
                    display.WriteLine($"{detail.GetType().Name} №{indexOfDetail} is trash");
                }
            }
            catch(NullReferenceException)
            {
                display.WriteLine("Detail is missing on the CheckMachine");
            }
        }

        private bool CheckWorkingIsTrue()
        {
            if(machine.IsWork == true && checkMachine.IsWork == true)
            {
                return true;
            }

            return false;
        }

        public void EndWork()
        {
            machine.TurnOff();
            checkMachine.TurnOff();
        }

        private void Ititialize()
        {
            machine = new Machine(1, checkerContainer);
            machine.TurnOn();

            checkMachine = new CheckMachine(checkerContainer);
            checkMachine.TurnOn();

            indexOfDetail = 0;
        }

        private void FullCheckerContainer()
        {
            checkerContainer = new CheckerContainer();

            checkerContainer.Register(new Bolt().GetType(), new BoltChecker());
            checkerContainer.Register(new Nail().GetType(), new NailChecker());
            checkerContainer.Register(new Screw().GetType(), new ScrewChecker());
            checkerContainer.Register(new Wheel().GetType(), new WheelChecker());
        }
    }
}
