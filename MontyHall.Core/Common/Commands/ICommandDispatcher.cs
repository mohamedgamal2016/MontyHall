using MediatR;
using System.Threading.Tasks;

namespace MontyHall.Core.Common.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
