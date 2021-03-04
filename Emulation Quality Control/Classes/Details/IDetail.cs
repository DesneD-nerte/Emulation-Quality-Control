using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    interface IDetail
    {
        double Height { get; }
        double Width { get; }
        double Length { get; }

        int NumberOfDetail { get; set; }
    }
}
