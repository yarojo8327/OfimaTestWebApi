using Ofima.TechnicalTest.Common.Dto;

namespace Ofima.TechnicalTest.Service.Interfaces
{
    public interface IPlayerService
    {
        BodyResponse<object> RegisterPlayers(string playerOne, string playerTwo);
    }
}