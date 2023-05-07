using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;

namespace Ofima.TechnicalTest.Service.Interfaces
{
    public interface IMoveService
    {
        BodyResponse<object> Add(RegisterMove request);
        IEnumerable<MoveDto> GetAll();
    }
}