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

        private (List<Task<bool>>, List<ICheckMachine>) StartFourCheckMachines(IDetail detail)
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

            List<ICheckMachine> listCurrentCheckMachines = new List<ICheckMachine>()
            {
                checkMachine,
                checkMachine1,
                checkMachine2,
                checkMachine3,
            };

            (List<Task<bool>>, List<ICheckMachine>) result = (list, listCurrentCheckMachines);

            return result;
        }

        private async void CheckDetail(IDetail detail)
        {
            try
            {
                var tasksAndMachines = StartFourCheckMachines(detail);

                var listTasks = tasksAndMachines.Item1;
                var listCurrentCheckMachines = tasksAndMachines.Item2;

                Task<bool> resultTask = await CheckAndCancelTasks(listTasks, listCurrentCheckMachines);

                ICheckMachine currentCheckMachine = GetLastCurrentCheckMachine(resultTask, listTasks, listCurrentCheckMachines);

                DisplayCheckedDetail(detail, resultTask, currentCheckMachine);
            }
            catch (AggregateException ex)
            {
                foreach (var oneException in ex.InnerExceptions)
                {
                    display.WriteLine(oneException.Message);
                }
            }
        }

        private async Task<Task<bool>> CheckAndCancelTasks(List<Task<bool>> listTasks, List<ICheckMachine> listCurrentCheckMachines)
        {
            Task<bool> resultTask = await Task.WhenAny(listTasks);

            resultTask = await CheckFaultedListTasks(resultTask, listTasks, listCurrentCheckMachines);

            cancellationTokenSource.Cancel();

            return resultTask;
        }

        ///По достижению последнего элемента (задачи) в списке, забираем его,
        ///если он по итогу выполнится то будет выброс ошибки, потому что никто не смог проверить 
        private async Task<Task<bool>> CheckFaultedListTasks(Task<bool> resultTask, List<Task<bool>> listTasks, List<ICheckMachine> listCurrentCheckMachines)
        {
            while (resultTask.Status == TaskStatus.Faulted)
            {
                if (listTasks.Count != 1)
                {
                    listCurrentCheckMachines.RemoveAt(listTasks.FindIndex((x) => x == resultTask));
                    listTasks.Remove(resultTask);

                    resultTask = await Task.WhenAny(listTasks);
                }
                else
                {
                    return resultTask;
                }
            }

            return resultTask;
        }

        //Оставляет один экземпляр проверочный машины, которая осуществила задачу по проверке детали быстрее всех
        private ICheckMachine GetLastCurrentCheckMachine(Task<bool> resultTask, List<Task<bool>> listTasks, List<ICheckMachine> listCurrentCheckMachines)
        {
            for (int i = 0; i < listTasks.Count; i++)
            {
                if (listTasks[i] == resultTask)
                {
                    return listCurrentCheckMachines[i];
                }
            }

            throw new ArgumentException("List Check Machines is empty");
        }

        private void DisplayCheckedDetail(IDetail detail, Task<bool> taskResult, ICheckMachine currentCheckMachines)
        {
            if (taskResult.Result == true)
            {
                display.WriteLine($"The CheckMachine \"{currentCheckMachines.Model}\" checked {detail.GetType().Name} №{detail.NumberOfDetail} - fine");
            }
            else
            {
                display.WriteLine($"The CheckMachine \"{currentCheckMachines.Model}\" checked {detail.GetType().Name} №{detail.NumberOfDetail} - trash");
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
