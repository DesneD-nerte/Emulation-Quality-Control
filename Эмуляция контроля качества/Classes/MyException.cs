using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    public class MyException : Exception
    {
        public MyException()
        {
        }

        public MyException(string message) : base(message)
        {

        }

        public MyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
