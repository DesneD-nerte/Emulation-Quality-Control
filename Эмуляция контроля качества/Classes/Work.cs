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
                indexOfDetail++;

                IDetail detail = machine.GetDetail();
                CheckDetail(detail);

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

            indexOfDetail = 0;
        }

    }
}
