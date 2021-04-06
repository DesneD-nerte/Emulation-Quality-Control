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

        List<ICheckMachine> listCheckMachines;

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

            while (DoMachineWork() == true)
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
                RestartOrCloseAfterError();
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

        private (List<Task<bool>>, List<CheckMachine>) StartFourCheckMachines(IDetail detail)
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

            List<CheckMachine> listCurrentMachines = new List<CheckMachine>()
            {
                (CheckMachine)checkMachine,
                (CheckMachine)checkMachine1,
                (CheckMachine)checkMachine2,
                (CheckMachine)checkMachine3,
            };

            (List<Task<bool>>, List<CheckMachine>) result = (list, listCurrentMachines);

            return result;
        }

        private async void CheckDetail(IDetail detail)
        {
            try
            {
                var tasksAndTheirMachines = StartFourCheckMachines(detail);

                var listTasks = tasksAndTheirMachines.Item1;
                var listCurrentMachines = tasksAndTheirMachines.Item2;

                bool taskResult = await CheckAndCancelTasks(listTasks, listCurrentMachines);

                DisplayCheckedDetail(detail, taskResult, listCurrentMachines);
            }
            catch (AggregateException ex)
            {
                foreach (var oneException in ex.InnerExceptions)
                {
                    display.WriteLine(oneException.Message);
                }
            }
        }

        private async Task<bool> CheckAndCancelTasks(List<Task<bool>> listTasks, List<CheckMachine> listCurrentMachines)
        {
            Task<bool> resultTask = await Task.WhenAny(listTasks);

            while (resultTask.Status == TaskStatus.Faulted)
            {
                if (listTasks.Count != 1)
                {
                    listCurrentMachines.RemoveAt(listTasks.FindIndex((x) => x == resultTask));
                    listTasks.Remove(resultTask);

                    resultTask = await Task.WhenAny(listTasks);
                }
                else
                {
                    return resultTask.Result;
                }
            }

            cancellationTokenSource.Cancel();

            return resultTask.Result;
        }

        private void DisplayCheckedDetail(IDetail detail, bool taskResult, List<CheckMachine> lastCheckMachine)
        {
            if (taskResult == true)
            {
                display.WriteLine($"The CheckMachine \"{lastCheckMachine.FirstOrDefault().Model}\" checked {detail.GetType().Name} №{detail.NumberOfDetail} - fine");
            }
            else
            {
                display.WriteLine($"The CheckMachine \"{lastCheckMachine.FirstOrDefault().Model}\" checked {detail.GetType().Name} №{detail.NumberOfDetail} - trash");
            }
        }

        private bool DoMachineWork()
        {
            if (machine.DoesMachineWork()
               && listCheckMachines.All((x) => x.DoesCheckMachineWork())
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
            display.WriteLine("1 - Restart\n" +
                                  "2 - Close");

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
            machine = new Machine("LifeLand", checkerContainer);
            machine.TurnOn();

            checkMachine = new CheckMachine("Raven", checkerContainer);
            checkMachine1 = new CheckMachine("Cycle", checkerContainer);
            checkMachine2 = new CheckMachine("Eagle", checkerContainer);
            checkMachine3 = new CheckMachine("ZLD", checkerContainer);
            checkMachine.TurnOn();
            checkMachine1.TurnOn();
            checkMachine2.TurnOn();
            checkMachine3.TurnOn();

            listCheckMachines = new List<ICheckMachine>() { checkMachine, checkMachine1, checkMachine2, checkMachine3 };

            conveyor = new Conveyor("StarField");
            conveyor.TurnOn();
        }
    }
}
