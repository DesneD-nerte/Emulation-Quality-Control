using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
            catch(ConveyorException ex)
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
               });
            }
        }

        private ConcurrentDictionary<Task<bool>, CancellationTokenSource> StartFourCheckMachines(IDetail detail)
        {
            #region Время
            //await Task.Run(() =>
            //{
            //    while (!task.IsCompleted)
            //    {
            //        if (task.IsCompleted)
            //        {
            //            stopwatch.Stop();
            //        }
            //    }
            //});
            #endregion

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task<bool> task = checkMachine.CheckDetail(detail, token);

            CancellationTokenSource cancellationTokenSource1 = new CancellationTokenSource();
            CancellationToken token1 = cancellationTokenSource1.Token;
            Task<bool> task1 = checkMachine1.CheckDetail(detail, token1);

            CancellationTokenSource cancellationTokenSource2 = new CancellationTokenSource();
            CancellationToken token2 = cancellationTokenSource2.Token;
            Task<bool> task2 = checkMachine2.CheckDetail(detail, token2);

            CancellationTokenSource cancellationTokenSource3 = new CancellationTokenSource();
            CancellationToken token3 = cancellationTokenSource3.Token;
            Task<bool> task3 = checkMachine3.CheckDetail(detail, token3);

            ConcurrentDictionary<Task<bool>, CancellationTokenSource> dictionary = new ConcurrentDictionary<Task<bool>, CancellationTokenSource>();
            dictionary.TryAdd(task, cancellationTokenSource);
            dictionary.TryAdd(task1, cancellationTokenSource1);
            dictionary.TryAdd(task2, cancellationTokenSource2);
            dictionary.TryAdd(task3, cancellationTokenSource3);

            return dictionary;
        }

        private void CheckDetail(IDetail detail)
        {
            ConcurrentDictionary<Task<bool>, CancellationTokenSource> dictionary =  StartFourCheckMachines(detail);

            CheckAndCancelTasks(dictionary);

            DisplayCheckedDetail(detail, dictionary);
        }

        private async void CheckAndCancelTasks(ConcurrentDictionary<Task<bool>, CancellationTokenSource> dictionary)
        {
            await Task.WhenAny(dictionary.Keys);//Пока не будет проверена деталь какой - нибудь машиной

            //Остановка всех задач, так как деталь уже была проверена
            foreach (KeyValuePair<Task<bool>, CancellationTokenSource> keyValue in dictionary)
            {
               keyValue.Value.Cancel();
            }
        }

        private async void DisplayCheckedDetail(IDetail detail, ConcurrentDictionary<Task<bool>, CancellationTokenSource> dictionary)
        {
            await Task.Delay(10000);//Без задержки не успевают выключиться задачи

            int index = 1;
            foreach (var oneTask in dictionary.Keys)
            {
                if (oneTask.IsCompleted == true && oneTask.IsCanceled == false && oneTask.IsFaulted == false)
                {
                    if (oneTask.Result == true)
                    {
                        display.WriteLine($"The Machine №{index} checked {detail.GetType().Name} №{detail.NumberOfDetail} - fine");
                    }
                    else
                    {
                        display.WriteLine($"The Machine №{index} checked {detail.GetType().Name} №{detail.NumberOfDetail} - trash");
                    }
                }

                index++;
            }
        }

        private bool AreMachinesWork()
        {
            if(machine.DoesMachineWork() 
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
