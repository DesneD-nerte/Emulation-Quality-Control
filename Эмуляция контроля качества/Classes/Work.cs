using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Work
    {
        ICheckMachine checkMachine;
        IMachine machine;
        IDisplay display;

        public Work(IDisplay display)
        {
            this.display = display;
        }

        public void StartWork()
        {
            Ititialize();

            while (true)
            {
                IDetail detail = machine.GetDetail();

                bool CheckedDetail = checkMachine.CheckDetail(detail);

                if(CheckedDetail == true)
                {
                    display.WriteLine("OK");
                }
                else
                {
                    display.WriteLine("Trash");
                }

                //display.WriteLine(CheckedDetail == true ? "Ok" : "Trash");
            }
        }

        private void Ititialize()
        {
            machine = new Machine(1);

            checkMachine = new CheckMachine();
        }
    }
}
