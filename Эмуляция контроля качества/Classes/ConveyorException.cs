using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Эмуляция_контроля_качества.Classes
{
    public class ConveyorException : Exception
    {
        public ConveyorException()
        {
        }

        public ConveyorException(string message) : base(message)
        {

        }

        public ConveyorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConveyorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
