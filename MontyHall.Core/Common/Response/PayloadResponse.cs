namespace MontyHall.Core.Common.Extensions.Response
{
    public class PayloadResponse<T> : IPayloadResponse<T>
        where T : class
    {
        public T Payload { get; set; }
    }
}
