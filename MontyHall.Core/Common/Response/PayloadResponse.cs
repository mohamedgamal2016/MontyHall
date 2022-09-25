namespace MontyHall.Core.Common.Response
{
    public class PayloadResponse<T> : IPayloadResponse<T>
        where T : class
    {
        public T Payload { get; set; }
    }
}
