using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emulation_Quality_Control.Classes
{
    class CooperateMachines
    {
        Random rnd = new Random();

        ICheckMachine checkMachine;
        ICheckMachine checkMachine1;
        ICheckMachine checkMachine2;
        ICheckMachine checkMachine3;

        IMachine machine;
        IConveyor conveyor;

        CheckerContainer checkerContainer;
        IDisplay display;

        public CooperateMachines(IDisplay display, CheckerContainer checkerContainer)
        {
            this.display = display;
            this.checkerContainer = checkerContainer;
        }

        public void StartWork()
        {
            Ititialize();

            while (AreMachinesWork() == true)
            {
                OneStep();
            }
        }

        private void OneStep()
        {
            try
            {
                IDetail detail = machine.GetDetail();

                PutDetailOnConveyorAndMoveOneStep(detail);

                CheckConveyorAndDetail();

                Thread.Sleep(2000);
            }
            catch (ConveyorException ex)
            {
                display.WriteLine(ex.Message);
                RestartOrCloseAfterError();
            }
            catch (CheckMachineException ex)
            {
                display.WriteLine(ex.Message);
            }
        }

        private void PutDetailOnConveyorAndMoveOneStep(IDetail detail)
        {
            conveyor.PutDetailOn(detail);
            conveyor.MoveDetails();
        }

        private void CheckConveyorAndDetail()
        {
            if (conveyor.CheckPlacesOfConveyor() == true)
            {
                Task.Run(() =>
                {
                    CheckDetail(conveyor.GetCurrentDetail());
                }).Wait();
            }
        }


        CancellationTokenSource cancellationTokenSource;

        private List<Task<bool>> StartFourCheckMachines(IDetail detail)
        {
            cancellationTokenSource = new CancellationTokenSource();//Для нового набора задач нужен новый экземпляр, иначе 
                                                                   //все новые задачи при запуске сразу отменяются
            CancellationToken token = cancellationTokenSource.Token;

            Task<bool> task = checkMachine.CheckDetail(detail, token);

            Task<bool> task1 = checkMachine1.CheckDetail(detail, token);

            Task<bool> task2 = checkMachine2.CheckDetail(detail, token);

            Task<bool> task3 = checkMachine3.CheckDetail(detail, token);

            List<Task<bool>> list = new List<Task<bool>>()
            {
                task,
                task1,
                task2,
                task3
            };

            return list;
        }

        private async void CheckDetail(IDetail detail)
        {
            var listTasks = StartFourCheckMachines(detail);

            bool taskResult = await CheckAndCancelTasks(listTasks);

            DisplayCheckedDetail(detail, taskResult, listTasks);
        }

        private async Task<bool> CheckAndCancelTasks(List<Task<bool>> listTasks)
        {
            Task<bool> resultTask = await Task.WhenAny(listTasks);//Пока не будет проверена деталь какой - нибудь машиной

            //Остановка всех задач, так как деталь уже была проверена
            cancellationTokenSource.Cancel();

            return resultTask.Result;
        }

        private async void DisplayCheckedDetail(IDetail detail, bool taskResult, List<Task<bool>> listTasks)
        {
            //await Task.Delay(5000);//Без задержки не успевают выключиться задачи

            int indexOfMachine = GetIdOfMachineFromTasks(listTasks);

            if (taskResult == true)
            {
                display.WriteLine($"The Machine №{indexOfMachine} checked {detail.GetType().Name} №{detail.NumberOfDetail} - fine");
            }
            else
            {
                display.WriteLine($"The Machine №{indexOfMachine} checked {detail.GetType().Name} №{detail.NumberOfDetail} - trash");
            }
        }

        private int GetIdOfMachineFromTasks(List<Task<bool>> listTasks)
        {
            for (int i = 0; i < listTasks.Count; i++)
            {
                if (listTasks[i].Status == TaskStatus.RanToCompletion)
                {
                    return i + 1;
                }
            }

            throw new CheckMachineException("All Tasks Are Cancelled");

            //int idOfMachine = from x in listTasks
            //         where x.Status == TaskStatus.RanToCompletion
            //         select x;
            //return idOfMachine;
        }

        private bool AreMachinesWork()
        {
            if (machine.DoesMachineWork()
               && checkMachine.DoesCheckMachineWork() == true
               && conveyor.DoesConveyorWork() == true)
            {
                return true;
            }

            return false;
        }

        public void EndWork()
        {
            machine.TurnOff();
            checkMachine.TurnOff();
            conveyor.TurnOff();
        }

        private void RestartOrCloseAfterError()
        {
            display.WriteLine("1 - Перезапустить\n" +
                                  "2 - Закрыть");

            string check;
            do
            {
                check = display.ReadLine();
                if (check == "1")
                {
                    display.Clear();
                    StartWork();
                }
                if (check == "2")
                {
                    EndWork();
                    Environment.Exit(0);
                }
            }
            while (check != "1" || check != "2");
        }

        private void Ititialize()
        {
            machine = new Machine(checkerContainer);
            machine.TurnOn();

            checkMachine = new CheckMachine(checkerContainer);
            checkMachine1 = new CheckMachine(checkerContainer);
            checkMachine2 = new CheckMachine(checkerContainer);
            checkMachine3 = new CheckMachine(checkerContainer);
            checkMachine.TurnOn();

            conveyor = new Conveyor();
            conveyor.TurnOn();
        }
    }
}
