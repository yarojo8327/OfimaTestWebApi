using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;

namespace Ofima.TechnicalTest.Service.Interfaces
{
    public interface IGameService
    {
        BodyResponse<object> Add(RegisterGame request);
        BodyResponse<RoundDto> GetWinner(int gameId, int roundNumber);
        BodyResponse<object> Put(RegisterGame request);
    }
}