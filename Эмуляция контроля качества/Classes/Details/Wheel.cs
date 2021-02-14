using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Wheel : Detail
    {
        public Wheel(double height, double width, double length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }

        public Wheel()
        {

        }
    }
}
