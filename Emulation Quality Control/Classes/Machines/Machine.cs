using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    class Machine : IMachine
    {
        Random rnd = new Random();

        bool IsWork = false;

        int indexOfDetail;
        public string Model { get; set; }
        CheckerContainer checkerContainer;

        public Machine(string model, CheckerContainer checkerContainer)
        {
            this.Model = model;
            this.checkerContainer = checkerContainer;
        }

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
