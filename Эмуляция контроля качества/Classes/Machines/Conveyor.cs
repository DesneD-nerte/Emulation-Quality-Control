using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    class Conveyor : IConveyor
    {
        Random rnd = new Random();
        IDetail[] massiv = new IDetail[10];
        bool IsWork = false;

        public Conveyor()
        {

        }

        public bool CheckPlacesOfConveyor()
        {
            if(massiv[0] == null && massiv[1] != null && massiv[2] == null)
            {
                return true;
            }
            if(massiv[0] != null || massiv[2] != null)
            {
                throw new ConveyorException("Вокруг требуемой позиции для проверки находятся препятствующие детали");
            }

            return false;
        }

        public void PutDetailOn(IDetail detail)
        {
            massiv[9] = detail;
        }

        public void MoveDetails()
        {
            if (TryBrokeConveyor() == false)
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
            else
            {
                throw new ConveyorException("Неполадки с механизмом передвижения конвеера");
            }
        }

        public IDetail GetCurrentDetail()
        {
            return massiv[1];
        }

        //Шанс поломки 0.01
        private bool TryBrokeConveyor()
        {
            if(rnd.Next(0,99) == 50)
            {
                IsWork = false;
                return true;
            }

            IsWork = true;
            return false;
        }

        public bool DoesConveyorWork()
        {
            if (IsWork == true)
            {
                return true;
            }

            return false;
        }

        public void TurnOn()
        {
            IsWork = true;
        }

        public void TurnOff()
        {
            IsWork = false;
        }
    }
}
