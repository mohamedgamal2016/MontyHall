namespace MontyHall.Core.Common.Extensions.Response
{
    public interface IPayloadResponse<T>
    {
        T Payload { get; set; }
    }
}
