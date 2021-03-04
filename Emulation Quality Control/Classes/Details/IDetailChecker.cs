using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes.Details
{
    interface IDetailChecker
    {
        bool CheckDetail(IDetail detail);
        IDetail CreateDetail(IDetail detail);
    }
}
