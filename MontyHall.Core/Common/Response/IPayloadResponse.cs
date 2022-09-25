namespace MontyHall.Core.Common.Response
{
    public interface IPayloadResponse<T>
    {
        T Payload { get; set; }
    }
}
