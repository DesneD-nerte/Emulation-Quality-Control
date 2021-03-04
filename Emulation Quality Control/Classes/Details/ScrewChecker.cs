using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes.Details
{
    class ScrewChecker : IDetailChecker
    {
        Random rnd = new Random();
        private readonly IDetail screwExample = new Screw(0.2, 0.3, 4);

        public bool CheckDetail(IDetail detail)
        {
            if (detail.Height == screwExample.Height
                || detail.Width == screwExample.Width
                || detail.Length == screwExample.Length)
            {
                return true;
            }

            return false;
        }

        public IDetail CreateDetail(IDetail detail)
        {
            if (rnd.Next(10) == 5)//10% - процент брака
            {
                return new Screw(1, 1, 2);
            }

            return new Screw(0.2, 0.3, 4);
        }
    }
}
