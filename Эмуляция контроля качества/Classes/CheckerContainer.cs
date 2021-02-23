using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckerContainer
    {
        ConcurrentDictionary<Type, IDetailChecker> checker = new ConcurrentDictionary<Type, IDetailChecker>();

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
            checker.TryAdd(type, detailChecker);
        }

        public void UnRegister(Type type, IDetailChecker detailChecker)
        {
            checker.TryRemove(type, out detailChecker);
        }
    }
}
