using System;
using System.Collections.Generic;

namespace MontyHall.Core.Common.Commands
{
    public class InvalidCommandResult<T> : CommandResult<T>
    {
        private readonly string _error;

        public InvalidCommandResult(string error)
        {
            _error = error;
        }

        public InvalidCommandResult()
        {
        }

        public override ResultType ResultType => ResultType.Invalid;

        public override List<string> Errors => new List<string> { _error ?? "The input was invalid." };

        public override T Data => throw new NotImplementedException();
    }
}
