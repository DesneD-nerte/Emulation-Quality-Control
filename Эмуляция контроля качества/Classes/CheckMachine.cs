using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckMachine : ICheckMachine
    {
        IDetail bold = new Bolt(0.5, 0.5, 3);

        IDetail nail = new Nail(0.2, 0.3, 4);

        IDetail screw = new Screw(0.2, 0.3, 4);

        IDetail wheel = new Wheel(10, 4, 10);

        public void Check(IDetail detail)
        {
            
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
