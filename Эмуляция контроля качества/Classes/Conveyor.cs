using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Conveyor
    {
        IDetail[] massiv = new IDetail[10];

        public bool CheckConveyor()
        {
            if(massiv[0] == null && massiv[1] != null && massiv[2] == null)
            {
                return true;
            }

            return false;
        }

        public void PutDetailOn(IDetail detail)
        {
            massiv[9] = detail;
        }

        public void MoveDetails()
        {
            for (int i = 0; i < massiv.Length - 1; i++)
            {
                if (i % 2 == 1)//Перемещение по нечетным позициям
                {
                    massiv[i] = massiv[i + 2];
                }
            }

            massiv[9] = null;
        }

        public IDetail GetCurrentDetail()
        {
            return massiv[1];
        }
    }
}
