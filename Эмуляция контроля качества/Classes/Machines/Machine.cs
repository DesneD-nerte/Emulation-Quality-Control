using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Machine : IMachine
    {
        Random rnd = new Random();
        
        public int Performance { get; }
        bool IsWork = false;

        int indexOfDetail;

        CheckerContainer checkerContainer;

        public Machine (int performance, CheckerContainer checkerContainer)
        {
            this.Performance = performance;
            this.checkerContainer = checkerContainer;
        }

        //TODO:Cтоит как-то использовать "Performance", чтобы получать фигуру один раз в определенное время
        private IDetail CreateDetail()
        {
            IDetail bolt = new Bolt();
            IDetail nail = new Nail();
            IDetail screw = new Screw();
            IDetail wheel = new Wheel();

            IDetail[] details = new IDetail[] { bolt, nail, screw, wheel };

            int index = rnd.Next(4);

            return checkerContainer.CreateDetail(details[index]);
        }

        public IDetail GetDetail()
        {
            IDetail detail = CreateDetail();

            indexOfDetail++;
            detail.NumberOfDetail = indexOfDetail;

            return detail;
        }

        public void TurnOn()
        {
            IsWork = true;
        }

        public void TurnOff()
        {
            IsWork = false;
        }

        public bool DoesMachineWork()
        {
            if (IsWork == true)
            {
                return true;
            }

            return false;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
