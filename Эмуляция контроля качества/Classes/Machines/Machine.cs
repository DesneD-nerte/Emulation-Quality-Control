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
            IDetail bolt = CreateOneDetail(new Bolt());
            IDetail nail = CreateOneDetail(new Nail());
            IDetail screw = CreateOneDetail(new Screw());
            IDetail wheel = CreateOneDetail(new Wheel());

            IDetail[] details = new IDetail[] { bolt, nail, screw, wheel };

            int index = rnd.Next(4);

            return details[index];
        }

        public IDetail GetDetail()
        {
            IDetail detail = CreateDetail();

            return detail;
        }


        private Detail CreateOneDetail(IDetail detail)
        {
            Detail createdDetail = null;


            if(detail is Bolt)
            {
                if (rnd.Next(10) == 5)//10% - процент брака
                {
                    return createdDetail = new Bolt(1, 1, 2);
                }

                return createdDetail = new Bolt(0.5, 0.5, 3);
            }

            if (detail is Nail)
            {
                if (rnd.Next(10) == 5)//10% - процент брака
                {
                    return createdDetail = new Nail(1, 1, 2);
                }

                return createdDetail = new Nail(0.2, 0.3, 4);
            }

            if (detail is Screw)
            {
                if (rnd.Next(10) == 5)//10% - процент брака
                {
                    return createdDetail = new Screw(1, 1, 2);
                }

                return createdDetail = new Screw(0.2, 0.3, 4);
            }

            if (detail is Wheel)
            {
                if (rnd.Next(10) == 5)//10% - процент брака
                {
                    return createdDetail = new Wheel(1, 1, 2);
                }

                return createdDetail = new Wheel(10, 4, 10);
            }

            return createdDetail;
        }


        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
