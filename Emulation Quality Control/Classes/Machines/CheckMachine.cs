using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Emulation_Quality_Control.Classes.Details;
using System.Threading.Tasks;
using System.Threading;

namespace Emulation_Quality_Control.Classes
{
    class CheckMachine : ICheckMachine
    {
        Random rnd = new Random();

        bool IsWork = false;
        CheckerContainer checkerContainer;

        public CheckMachine(CheckerContainer checkerContainer)
        {
            this.checkerContainer = checkerContainer;
        }

        public CheckMachine()
        {

        }

        public Task<bool> CheckDetail(IDetail detail, CancellationToken token)
        {
            return Task.Run<bool>(async () =>
            {
                await Task.Delay(rnd.Next(750));

                token.ThrowIfCancellationRequested();

                return checkerContainer.CheckDetail(detail);

            }, token);
        }

        public void TurnOn()
        {
            IsWork = true;
        }

        public void TurnOff()
        {
            IsWork = false;
        }

        public bool DoesCheckMachineWork()
        {
            if (IsWork == true)
            {
                return true;
            }

            return false;
        }
    }
}
