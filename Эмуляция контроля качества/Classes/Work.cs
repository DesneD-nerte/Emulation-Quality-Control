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
            while(true)
            {
                machine.Start();
            }
        }

        private void Ititialize()
        {
            machine = new Machine(1);

            checkMachine = new CheckMachine();
        }
    }
}
