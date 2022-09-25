using System;
using System.Collections.Generic;

namespace MontyHall.Core.Common.Commands
{
    public class UnexpectedCommandResult<T> : CommandResult<T>
    {
        private readonly string _error;

        public UnexpectedCommandResult()
        {
            _error = "There was an unexpected problem.";
        }

        public UnexpectedCommandResult(string error)
        {
            _error = error;
        }

        public UnexpectedCommandResult(Exception exception)
        {
            Exception = exception;
        }

        public override ResultType ResultType => ResultType.Unexpected;

        public override List<string> Errors => new List<string> { Exception?.Message ?? _error };

        public override T Data => throw new NotImplementedException();

        public Exception Exception { get; }
    }
}
