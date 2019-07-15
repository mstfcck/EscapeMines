using System;

namespace EscapeMines.Exceptions
{
    public class CommandValidationException : InvalidOperationException
    {
        public CommandValidationException(string message) : base(message)
        {
        }
    }
}