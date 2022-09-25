using System;
using System.Collections.Generic;
using System.Text;

namespace MontyHall.Core.Common.Response
{
    public class PayloadListResponse<T>
        where T : class
    {
        public int Count { get; set; }

        public IEnumerable<T> Payload { get; set; }
    }
}
