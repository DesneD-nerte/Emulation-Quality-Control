using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emulation_Quality_Control.Classes;
using Emulation_Quality_Control.Classes.Details;

namespace Emulation_Quality_Control
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new ScreenDisplay();

            CheckerContainer checkerContainer = FullCheckerContainer();

            CooperateMachines work = new CooperateMachines(display, checkerContainer);

            StartProgramm(work);
        }


        /// <summary>
        /// Начало работы метода по выходу из программы происходит до начала прямой работы конвеера
        /// сделано для того, чтобы он не успел перехватить управление главным потоком
        /// </summary>
        static void StartProgramm(CooperateMachines work)
        {
            Action<object> action = (object obj) =>
            {
                ExitProgramm(work);
            };
            Task stoppingTask = new Task(action, "Stopping");
            stoppingTask.Start();

            //Action<object> action1 = (object obj) =>
            //{
            //    IndicateProgrammWork();
            //};
            //Task showTask = new Task(action1, "Nothing");
            //showTask.Start();

            StartWorking(work);
        }


        static void StartWorking(CooperateMachines work)
        {
            try
            {
                work.StartWork();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unnexpected error:\n" + ex.Message);

                Environment.Exit(0);
            }
        }

        static void ExitProgramm(CooperateMachines work)
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace)
                {
                    work.EndWork();
                    Environment.Exit(0);
                }
            }
        }

        static async void IndicateProgrammWork()
        {
            while (true)
            {
                string s = "Programm is working .";
                Console.Write("\r{0}", s);
                await Task.Delay(100);

                string s1 = "Programm is working ..";
                Console.Write("\r{0}", s1);
                await Task.Delay(100);

                string s2 = "Programm is working ...";
                Console.Write("\r{0}", s2);
                await Task.Delay(100);
            }
        }

        static void ClearLine(int line)
        {
            
            Console.MoveBufferArea(0, line, Console.BufferWidth, 1, Console.BufferWidth, line, ' ', Console.ForegroundColor, Console.BackgroundColor);
        }

        static CheckerContainer FullCheckerContainer()
        {
            CheckerContainer checkerContainer = new CheckerContainer();

            checkerContainer.Register(new Bolt().GetType(), new BoltChecker());
            checkerContainer.Register(new Nail().GetType(), new NailChecker());
            checkerContainer.Register(new Screw().GetType(), new ScrewChecker());
            checkerContainer.Register(new Wheel().GetType(), new WheelChecker());

            return checkerContainer;
        }
    }
}
