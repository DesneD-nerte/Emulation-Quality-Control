using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Work
    {
        ICheckMachine checkMachine;
        IMachine machine;

        public void StartWork()
        {
            Ititialize();

            while (true)
            {
                IDetail detail = machine.GetDetail();

                bool CheckedDetail = checkMachine.CheckDetail(detail);

                if(CheckedDetail == true)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("Trash");
                }
            }
        }

        private void Ititialize()
        {
            machine = new Machine(1);

            checkMachine = new CheckMachine();
        }
    }
}
