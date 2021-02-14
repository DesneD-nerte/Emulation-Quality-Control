using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Эмуляция_контроля_качества.Classes
{
    class Work
    {
        ICheckMachine checkMachine;
        IMachine machine;
        IDisplay display;
        int indexOfDetail;

        public Work(IDisplay display)
        {
            this.display = display;
        }

        public void StartWork()
        {
            Ititialize();
            Stopwatch stopWatch = new Stopwatch();

            while (true)
            {
                indexOfDetail++;
                
                IDetail detail = machine.GetDetail();

                bool CheckedDetail = checkMachine.CheckDetail(detail);

                if (CheckedDetail == true)
                {
                    display.WriteLine($"{detail} №{indexOfDetail} is fine");
                }
                else
                {
                    display.WriteLine($"{detail} №{indexOfDetail} is trash");
                }

                Thread.Sleep(1000);
            }
        }

        private void Ititialize()
        {
            machine = new Machine(1);

            checkMachine = new CheckMachine();

            indexOfDetail = 0;
        }
    }
}
