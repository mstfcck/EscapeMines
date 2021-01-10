using System;

namespace EscapeMines.Exceptions
{
    public class LocationValidationException : InvalidOperationException
    {
        public LocationValidationException(string message) : base(message)
        {
        }
    }
}