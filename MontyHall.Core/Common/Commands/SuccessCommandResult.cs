using System.Collections.Generic;

namespace MontyHall.Core.Common.Commands
{
    public class SuccessCommandResult<T> : CommandResult<T>
    {
        public SuccessCommandResult(T data)
        {
            Data = data;
        }

        public override ResultType ResultType => ResultType.Ok;

        public override List<string> Errors => new List<string>();

        public override T Data { get; }
    }
}
