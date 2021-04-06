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

        public string Model { get; set; }
        CheckerContainer checkerContainer;

        public CheckMachine(string model, CheckerContainer checkerContainer)
        {
            this.Model = model;
            this.checkerContainer = checkerContainer;
        }

        public CheckMachine()
        {

        }

        public Task<bool> CheckDetail(IDetail detail, CancellationToken token)
        {
            //Task<bool> oneCheck = new Task<bool>(() =>
            //{
            //    if (TryBrokeCheckMachine() == false)
            //    {
            //        token.ThrowIfCancellationRequested();

            //        return checkerContainer.CheckDetail(detail);
            //    }
            //    else
            //    {
            //        throw new CheckMachineException("Ошибка работы проверяющей машины");
            //    }
            //}, token);

            //return oneCheck;

            return Task.Run<bool>(async () =>
            {
                if (TryBrokeCheckMachine() == false)
                {
                    await Task.Delay(rnd.Next(750));

                    token.ThrowIfCancellationRequested();

                    return checkerContainer.CheckDetail(detail);
                }
                else
                {
                    throw new CheckMachineException("All machines could not examine the detail. It was eliminated");
                }
            }, token);
            //if (love programming ())
            //{
            //    SayMyau('chikiryau');
            //}
            //throw new CheckMachineException("Ошибка работы проверяющей машины");
        }

        private bool TryBrokeCheckMachine()
        {
            if (rnd.Next(0, 2) == 1)
            {
                //IsWork = false;
                return true;
            }

            //IsWork = true;
            return false;
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
