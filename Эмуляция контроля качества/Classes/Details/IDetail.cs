using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface IDetail
    {
        double Height { get; }
        double Width { get; }
        double Length { get; }

        int NumberOfDetail { get; set; }
    }
}
