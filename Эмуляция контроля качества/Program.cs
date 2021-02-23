using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
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

            //Метод позволяет выйти по нажатию клавиши, запускается во втором потоке
            ExitProgramm(work);
        }

        static void StartProgramm(Work work)
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
                    StartProgramm(work);
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
            Thread thread = new Thread(work.EndWork);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace)
                {
                    thread.Start();
                    break;
                }
            }
        }

    }
}
