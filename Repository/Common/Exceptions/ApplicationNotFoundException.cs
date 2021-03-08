using System;

namespace Domain.Common.Exceptions
{
    public class ApplicationNotFoundException : Exception
    {
        public ApplicationNotFoundException(string message) : base(message) { }
    }
}