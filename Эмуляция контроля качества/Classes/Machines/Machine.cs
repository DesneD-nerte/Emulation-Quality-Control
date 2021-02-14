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
            IDetail bolt = CreateBolt();
            IDetail nail = new Nail();
            IDetail screw = new Screw();
            IDetail wheel = new Wheel();

            IDetail[] details = new IDetail[] { bolt, nail, screw, wheel };

            int index = rnd.Next(4);

            return details[index];
        }

        public IDetail GetDetail()
        {
            IDetail detail = CreateDetail();

            return detail;
        }


        private Bolt CreateBolt()
        {
            Bolt bolt = new Bolt();


            return bolt;
        }

        private Bolt CreateNail()
        {
            Bolt nail = new Bolt();


            return bolt;
        }

        private Bolt CreateScrew()
        {
            Bolt screw = new Bolt();


            return bolt;
        }

        private Bolt CreateWheel()
        {
            Bolt wheel = new Bolt();


            return bolt;
        }



        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
