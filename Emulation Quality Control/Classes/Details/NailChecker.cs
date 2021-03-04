using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes.Details
{
    class NailChecker : IDetailChecker
    {
        Random rnd = new Random();
        private readonly IDetail nailExample = new Nail(0.2, 0.3, 4);

        public bool CheckDetail(IDetail detail)
        {
            if (detail.Height == nailExample.Height
                || detail.Width == nailExample.Width
                || detail.Length == nailExample.Length)
            {
                return true;
            }

            return false;
        }

        public IDetail CreateDetail(IDetail detail)
        {
            if (rnd.Next(10) == 5)//10% - процент брака
            {
                return new Nail(1, 1, 2);
            }

            return new Nail(0.2, 0.3, 4);
        }
    }
}
