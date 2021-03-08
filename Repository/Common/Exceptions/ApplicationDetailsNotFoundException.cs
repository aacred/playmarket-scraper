using System;

namespace Domain.Common.Exceptions
{
    public class ApplicationDetailsNotFoundException : Exception
    {
        public ApplicationDetailsNotFoundException(string message) : base(message) { }
    }
}