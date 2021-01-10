using System;

namespace EscapeMines.Exceptions
{
    public class InstructionValidationException : Exception
    {
        public InstructionValidationException(string message) : base(message)
        {
        }
    }
}