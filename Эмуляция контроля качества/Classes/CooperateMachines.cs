using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Эмуляция_контроля_качества.Classes
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
                IDetail detail = machine.GetDetail();

                PutDetailOnConveyorAndMove1Step(detail);

                CheckConveyorAndDetail();

                Thread.Sleep(1000);
            }
        }

        private void PutDetailOnConveyorAndMove1Step(IDetail detail)
        {
            try
            {
                conveyor.PutDetailOn(detail);
                conveyor.MoveDetails();

                if (conveyor.CheckPlacesOfConveyor() == true)
                {
                    CheckDetail(conveyor.GetCurrentDetail());
                }
            }
            catch(ConveyorException ex)
            {
                display.WriteLine(ex.Message);
                RestartOrCloseAfterError();
            }
        }

        private void CheckConveyorAndDetail()
        {
            try
            {
                if (conveyor.CheckPlacesOfConveyor() == true)
                {
                    CheckDetail(conveyor.GetCurrentDetail());
                }
            }
            catch (ConveyorException ex)
            {
                display.WriteLine(ex.Message);
                RestartOrCloseAfterError();
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
                check = Console.ReadLine();
                if (check == "1")
                {
                    Console.Clear();
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
            machine = new Machine(1, checkerContainer);
            machine.TurnOn();

            checkMachine = new CheckMachine(checkerContainer);
            checkMachine.TurnOn();

            conveyor = new Conveyor();
            conveyor.TurnOn();
        }
    }
}
