using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества.Classes
{
    class CheckMachine : ICheckMachine
    {
        public bool IsWork { get; private set; } = false;
        IDisplay display;
        CheckerContainer checkerContainer;

        public CheckMachine(IDisplay display, CheckerContainer checkerContainer)
        {
            this.display = display;
            this.checkerContainer = checkerContainer;
        }

        public CheckMachine()
        {
            
        }


        public bool CheckDetail(IDetail detail)
        {
            return checkerContainer.CheckDetail(detail);
        }

        private bool DetailIsFine(IDetail exampleDetail, IDetail detail)
        {
            try
            {
                return CompareDetails(exampleDetail, detail);
            }
            catch (Exception ex)
            {
                display.WriteLine("Ошибка в ходе измерения детали:\n " + ex.Message);

                return false;
            }
        }


        private bool CompareDetails(IDetail exampleDetail, IDetail detail)
        {
            if (detail.Height == exampleDetail.Height
                || detail.Width == exampleDetail.Width
                || detail.Length == exampleDetail.Length)
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

        public void Transit()
        {
            throw new NotImplementedException();
        }

        public void Utilize()
        {
            throw new NotImplementedException();
        }
    }
}
