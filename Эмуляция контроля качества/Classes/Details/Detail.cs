using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Detail : IDetail
    {
        public double Height { get; internal set; }
        public double Width { get; internal set; }
        public double Length { get; internal set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
