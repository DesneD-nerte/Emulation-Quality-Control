using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Эмуляция_контроля_качества.Classes;
using Эмуляция_контроля_качества.Classes.Details;

namespace Эмуляция_контроля_качества
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new ScreenDisplay();

            Work work = new Work(display);

            StartProgramm(work);
        }


        /// <summary>
        /// Начало работы метода по выходу из программы происходит до начала прямой работы конвеера
        /// сделано для того, чтобы он не успел перехватить управление главным потоком
        /// </summary>
        static void StartProgramm(Work work)
        {
            Action<object> action = (object obj) =>
            {
                ExitProgramm(work);
            };
            Task stoppingTask = new Task(action, "Stopping");
            stoppingTask.Start();

            //Запуск основного метода 
            StartWorking(work);
        }


        static void StartWorking(Work work)
        {
            try
            {
                work.StartWork();
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла непридвиденная ошибка, требуется перезапуск!");

                Console.WriteLine("1 - Перезапустить\n" +
                                  "2 - Закрыть");

                NextMoveAfterError(work);
            }
        }


        static void NextMoveAfterError(Work work)
        {
            string check;
            do
            {
                check = Console.ReadLine();
                if (check == "1")
                {
                    Console.Clear();
                    StartWorking(work);
                }
                if (check == "2")
                {
                    Environment.Exit(0);
                }
            }
            while (check != "1" || check != "2");
        }


        static void ExitProgramm(Work work)
        {

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace)
                {
                    work.EndWork();
                    break;
                }
            }
        }

    }
}
