using System;
using System.Threading;
using Эмуляция_контроля_качества.Classes;

namespace Эмуляция_контроля_качества
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new ScreenDisplay();

            Work work = new Work(display);
            
            Thread thread = new Thread(work.StartWork);

            try
            {
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(new MyException(ex.Message));
            }

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace)
                {
                    work.EndWork();
                    break;
                }
            }

            //MyException myException = new MyException("wqe");
            //throw new MyException("LOL");
        }

    }
}
