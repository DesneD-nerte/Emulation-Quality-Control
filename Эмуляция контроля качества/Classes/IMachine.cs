using System;
using System.Collections.Generic;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    interface IMachine
    {
        void Start();           // метод для запуска
        void Stop();            // метод для остановки
        //IDetail CreateDetail(); // создание детали
        int Performance { get; }// производительность станка
    }
}
