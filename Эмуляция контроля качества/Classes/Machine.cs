using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Machine : IMachine
    {
        Random rnd = new Random();
        public int Performance { get; }

        public Machine (int performance)
        {
            this.Performance = performance;
        }

        private IDetail CreateDetail()//Cтоит как-то использовать "Performance", чтобы получать фигуру один раз в определенное время
        {
            IDetail bolt = new Bolt();
            IDetail nail = new Nail();
            IDetail screw = new Screw();
            IDetail wheel = new Wheel();

            IDetail[] details = new IDetail[] { bolt, nail, screw, wheel };

            int Number = rnd.Next(4);

            return details[Number];
        }

        public void Start()
        {
            CreateDetail();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
