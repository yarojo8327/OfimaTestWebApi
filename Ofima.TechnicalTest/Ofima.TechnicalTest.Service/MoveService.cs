using AutoMapper;

using Ofima.TechnicalTest.Common;
using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;
using Ofima.TechnicalTest.Infraestructure.Interfaces;
using Ofima.TechnicalTest.Infraestructure.Models;
using Ofima.TechnicalTest.Service.Interfaces;

using System.Net;

namespace Ofima.TechnicalTest.Service
{
    public class MoveService : IMoveService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoveService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BodyResponse<object> Add(RegisterMove request)
        {
            GameMove move = _mapper.Map<GameMove>(request);
            _unitOfWork.GameMove.Add(move);
            bool saved = _unitOfWork.Save() > 0;

            return new BodyResponse<object>
            {
                Code = saved ? (int)HttpStatusCode.Created : (int)HttpStatusCode.BadRequest,
                IsSuccess = saved,
                Message = saved ? GeneralMessages.SaveDataSuccess : GeneralMessages.SaveDataError,
            };

        }

        public IEnumerable<MoveDto> GetAll()
        {
            return _mapper.Map<List<MoveDto>>(_unitOfWork.Move.GetAll().ToList());
        }
    }
}
