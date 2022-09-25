using System;
using System.Collections.Generic;

namespace MontyHall.Core.Common.Commands
{
    public class NotFoundCommandResult<T> : CommandResult<T>
    {
        public override ResultType ResultType => ResultType.NotFound;

        public override List<string> Errors => new List<string> { "The requested resource was not found." };

        public override T Data => throw new NotImplementedException();
    }
}
