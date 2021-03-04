using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    class Nail : Detail
    {
        public Nail(double height, double width, double length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }

        public Nail()
        {

        }
    }
}
