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
        Conveyor conveyor = new Conveyor();

        public Work(IDisplay display, CheckerContainer checkerContainer)
        {
            this.display = display;
            this.checkerContainer = checkerContainer;
        }

        public void StartWork()
        {
            Ititialize();

            while (CheckWorkingIsTrue() == true)
            {
                IDetail detail = machine.GetDetail();
                conveyor.PutDetailOn(detail);//Установка детали на конвеер
                conveyor.MoveDetails();//Движение фигур на конвеере

                if (conveyor.CheckConveyor() == true)
                {
                    CheckDetail(conveyor.GetCurrentDetail());
                }

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
                    display.WriteLine($"{detail.GetType().Name} №{detail.NumberOfDetail} is fine");
                }
                else
                {
                    display.WriteLine($"{detail.GetType().Name} №{detail.NumberOfDetail} is trash");
                }
            }
            catch (NullReferenceException ex)
            {
                throw new MyException("Wrong detail", ex);
                //display.WriteLine("Detail is missing on the CheckMachine" + ex.Message);
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
        }
    }
}
