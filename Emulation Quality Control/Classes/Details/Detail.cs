using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    abstract class Detail : IDetail
    {
        public double Height { get; internal set; }
        public double Width { get; internal set; }
        public double Length { get; internal set; }
        public int NumberOfDetail { get; set; }
    }
}
