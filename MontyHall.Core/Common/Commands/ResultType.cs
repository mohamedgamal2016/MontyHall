using System;
using System.Collections.Generic;
using System.Text;

namespace MontyHall.Core.Common.Commands
{
    public enum ResultType
    {
        Ok,
        Partial,
        Invalid,
        NotFound,
        Unexpected,
        Conflict,
        Cancelled
    }
}
