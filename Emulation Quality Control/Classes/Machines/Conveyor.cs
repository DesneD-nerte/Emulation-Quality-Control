using System;
using System.Collections.Generic;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    class Conveyor : IConveyor
    {
        Random rnd = new Random();
        public IDetail[] MassivOfDetails { get; set; } = new IDetail[10];

        public int Length => MassivOfDetails.Length; 

        bool IsWork = false;

        public Conveyor()
        {

        }

        public bool CheckPlacesOfConveyor()
        {
            if(MassivOfDetails[0] == null && MassivOfDetails[1] != null && MassivOfDetails[2] == null)
            {
                return true;
            }
            if(MassivOfDetails[0] != null || MassivOfDetails[2] != null)
            {
                throw new ConveyorException("Вокруг требуемой позиции для проверки находятся препятствующие детали");
            }

            return false;
        }

        public void PutDetailOn(IDetail detail)
        {
            MassivOfDetails[9] = detail;
        }

        public void MoveDetails()
        {
            if (TryBrokeConveyor() == false)
            {
                for (int i = 0; i < MassivOfDetails.Length - 1; i++)
                {
                    if (i % 2 == 1)//Перемещение по нечетным позициям
                    {
                        MassivOfDetails[i] = MassivOfDetails[i + 2];
                    }
                }

                MassivOfDetails[9] = null;
            }
            else
            {
                throw new ConveyorException("Неполадки с механизмом передвижения конвеера");
            }
        }

        public IDetail GetCurrentDetail()
        {
            return MassivOfDetails[1];
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
