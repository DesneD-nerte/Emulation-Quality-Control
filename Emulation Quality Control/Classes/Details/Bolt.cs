﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    class Bolt : Detail
    {
        public Bolt(double height, double width, double length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }

        public Bolt()
        {

        }
    }
}
