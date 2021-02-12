﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Screw : IDetail
    {
        public double Height { get; }
        public double Width { get; }
        public double Length { get; }

        public Screw(double height, double width, double length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }

        public Screw()
        {

        }
    }
}
