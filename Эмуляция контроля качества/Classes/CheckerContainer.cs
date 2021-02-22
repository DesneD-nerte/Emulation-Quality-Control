using System;
using System.Collections.Generic;
using System.Text;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckerContainer
    {
        Dictionary<Type, IDetailChecker> checker = new Dictionary<Type, IDetailChecker>();

        public bool CheckDetail(IDetail detail)
        {
            IDetailChecker detailchecker = checker[detail.GetType()];

            return detailchecker.CheckDetail(detail);
        }

        public IDetail CreateDetail(IDetail detail)
        {
            IDetailChecker detailchecker = checker[detail.GetType()];

            return detailchecker.CreateDetail(detail);
        }

        public void Register(Type type, IDetailChecker detailChecker)
        {
            checker.Add(type, detailChecker);
        }

        public void UnRegister(Type type, IDetailChecker detailChecker)
        {
            checker.Remove(type);
        }
    }
}
