using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public ValidationException(List<string> errors) : base(string.Join("; ", errors))
        {
            Errors = errors ?? new List<string>();
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            Errors = new List<string> { message };
        }
    }
}
