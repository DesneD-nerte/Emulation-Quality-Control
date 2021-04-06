using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Emulation_Quality_Control.Classes
{
    class CheckMachineException : Exception
    {
        public CheckMachineException()
        {
        }

        public CheckMachineException(string message) : base(message)
        {
        }

        public CheckMachineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CheckMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
