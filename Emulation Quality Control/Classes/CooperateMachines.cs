using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Emulation_Quality_Control.Classes
{
    class CooperateMachines
    {
        ICheckMachine checkMachine;
        IMachine machine;
        IConveyor conveyor;

        CheckerContainer checkerContainer;
        IDisplay display;

        public CooperateMachines(IDisplay display, CheckerContainer checkerContainer)
        {
            this.display = display;
            this.checkerContainer = checkerContainer;
        }

        public void StartWork()
        {
            Ititialize();

            while (AreMachinesWork() == true)
            {
                OneStep();
            }
        }

        private void OneStep()
        {
            try
            {
                IDetail detail = machine.GetDetail();

                PutDetailOnConveyorAndMoveOneStep(detail);

                CheckConveyorAndDetail();

                Thread.Sleep(1000);
            }
            catch(ConveyorException ex)
            {
                display.WriteLine(ex.Message);
                RestartOrCloseAfterError();
            }
        }

        private void PutDetailOnConveyorAndMoveOneStep(IDetail detail)
        {
            conveyor.PutDetailOn(detail);
            conveyor.MoveDetails();
        }

        private void CheckConveyorAndDetail()
        {
            if (conveyor.CheckPlacesOfConveyor() == true)
            {
                CheckDetail(conveyor.GetCurrentDetail());
            }
        }

        private void CheckDetail(IDetail detail)
        {
            bool checkedDetail = checkMachine.CheckDetail(detail);

            if (checkedDetail == true)
            {
                display.WriteLine($"{detail.GetType().Name} №{detail.NumberOfDetail} is fine");
            }
            else
            {
                display.WriteLine($"{detail.GetType().Name} №{detail.NumberOfDetail} is trash");
            }
        }

        private bool AreMachinesWork()
        {
            if(machine.DoesMachineWork() 
               && checkMachine.DoesCheckMachineWork() == true
               && conveyor.DoesConveyorWork() == true)
            {
                return true;
            }

            return false;
        }

        public void EndWork()
        {
            machine.TurnOff();
            checkMachine.TurnOff();
            conveyor.TurnOff();
        }

        private void RestartOrCloseAfterError()
        {
            display.WriteLine("1 - Перезапустить\n" +
                                  "2 - Закрыть");

            string check;
            do
            {
                check = display.ReadLine();
                if (check == "1")
                {
                    display.Clear();
                    StartWork();
                }
                if (check == "2")
                {
                    EndWork();
                    Environment.Exit(0);
                }
            }
            while (check != "1" || check != "2");
        }

        private void Ititialize()
        {
            machine = new Machine(checkerContainer);
            machine.TurnOn();

            checkMachine = new CheckMachine(checkerContainer);
            checkMachine.TurnOn();

            conveyor = new Conveyor();
            conveyor.TurnOn();
        }
    }
}
