using System.Collections.Generic;

namespace MontyHall.Core.Common.Commands
{
    public abstract class CommandResult<T>
    {
        public abstract ResultType ResultType { get; }

        public abstract List<string> Errors { get; }

        public abstract T Data { get; }
    }
}
