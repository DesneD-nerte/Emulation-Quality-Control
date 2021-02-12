using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckMachine : ICheckMachine
    {
        //IDetail boltExample = new Bolt(0.5, 0.5, 3);

        //IDetail nailExample = new Nail(0.2, 0.3, 4);

        //IDetail screwExample = new Screw(0.2, 0.3, 4);

        //IDetail wheelExample = new Wheel(10, 4, 10);

        public bool Check(IDetail detail)
        {
            IDetail exampleDetail = null;

            if(detail is Bolt)
            {
                exampleDetail = new Bolt(0.5, 0.5, 3);
            }
            if (detail is Nail)
            {
                exampleDetail = new Nail(0.2, 0.3, 4);
            }
            if (detail is Screw)
            {
                exampleDetail = new Screw(0.2, 0.3, 4);
            }
            if (detail is Wheel)
            {
                exampleDetail = new Wheel(10, 4, 10);
            }

            if(detail.Height == exampleDetail.Height 
                || detail.Width == exampleDetail.Width
                || detail.Length == exampleDetail.Length)
            {
                return true;
            }
            return false;
        }

        public void Transit()
        {
            throw new NotImplementedException();
        }

        public void Utilize()
        {
            throw new NotImplementedException();
        }
    }
}
