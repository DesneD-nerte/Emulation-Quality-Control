using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes.Details
{
    class WheelChecker : IDetailChecker
    {
        Random rnd = new Random();
        private readonly IDetail wheelExample = new Wheel(10, 4, 10);

        public bool CheckDetail(IDetail detail)
        {
            if (detail.Height == wheelExample.Height
                || detail.Width == wheelExample.Width
                || detail.Length == wheelExample.Length)
            {
                return true;
            }

            return false;
        }

        public IDetail CreateDetail(IDetail detail)
        {
            if (rnd.Next(10) == 5)//10% - процент брака
            {
                return new Wheel(1, 1, 2);
            }

            return new Wheel(10, 4, 10);
        }
    }
}
