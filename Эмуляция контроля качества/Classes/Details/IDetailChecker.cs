using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes.Details
{
    interface IDetailChecker
    {
        bool CheckDetail(IDetail detail);
        IDetail CreateDetail(IDetail detail);
    }
}
