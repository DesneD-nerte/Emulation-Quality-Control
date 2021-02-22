using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes.Details
{
    class BoltChecker : IDetailChecker
    {
        Random rnd = new Random();

        private readonly IDetail boltExample = new Bolt(0.5, 0.5, 3);

        public bool CheckDetail(IDetail detail)
        {
            if (detail.Height == boltExample.Height
                || detail.Width == boltExample.Width
                || detail.Length == boltExample.Length)
            {
                return true;
            }

            return false;
        }

        public IDetail CreateDetail(IDetail detail)
        {
            if (rnd.Next(10) == 5)//10% - процент брака
            {
                return new Bolt(1, 1, 2);
            }

            return new Bolt(0.5, 0.5, 3);
        }
    }
}
