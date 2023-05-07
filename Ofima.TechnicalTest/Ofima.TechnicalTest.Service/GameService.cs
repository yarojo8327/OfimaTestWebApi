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
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BodyResponse<object> Add(RegisterGame request)
        {
            Game game = _mapper.Map<Game>(request);
            _unitOfWork.Game.Add(game);
            bool saved = _unitOfWork.Save() > 0;

            return new BodyResponse<object>
            {
                Code = saved ? (int)HttpStatusCode.Created : (int)HttpStatusCode.BadRequest,
                IsSuccess = saved,
                Message = saved ? GeneralMessages.SaveDataSuccess : GeneralMessages.SaveDataError,
                Data = new { GameId = game.Id }
            };
        }

        public BodyResponse<object> Put(RegisterGame request)
        {
            Game game = _unitOfWork.Game.FirstOrDefault(x => x.Id == request.Id);
            bool saved = false;
            if (game != null)
            {
                game.WinnerId = request.WinnerId;
                _unitOfWork.Game.Update(game);
                saved = _unitOfWork.Save() > 0;
            }

            return new BodyResponse<object>
            {
                Code = saved ? (int)HttpStatusCode.Created : (int)HttpStatusCode.BadRequest,
                IsSuccess = saved,
                Message = saved ? GeneralMessages.SaveDataSuccess : GeneralMessages.SaveDataError,
                Data = new { GameId = game.Id }
            };
        }

        public BodyResponse<RoundDto> GetWinner(int gameId, int roundNumber)
        {
            Game? game = _unitOfWork.Game.FirstOrDefault(x => x.Id == gameId, x => x.Player, x => x.PlayerTwo, x => x.GameMoves);
            List<Move> listMoves = _unitOfWork.Move.GetAll().ToList();

            Player? winner = null;
            RoundDto round = new();
            string message = string.Empty;


            if (game != null)
            {
                List<GameMove> moves = game.GameMoves.Where(x => x.RoundNumber == roundNumber).OrderBy(x => x.Id).ToList();
                GameRule gameRule = ValidateWinner(moves);

                switch (gameRule.Winner)
                {
                    case "PlayerOne":
                        winner = game.Player;
                        message = $"El ganador es {winner.Names}";
                        break;
                    case "PlayerTwo":
                        winner = game.PlayerTwo;
                        message = $"El ganador es {winner.Names}";
                        break;
                    case "Tie":
                        message = $"Se ha presentado un empate";
                        break;

                    default:
                        throw new BusinessException(GeneralMessages.WinnerException);

                }

                round = new()
                {
                    GameId = gameId,
                    PlayerOneId = game.Player.Id,
                    PlayerTwoId = game.PlayerTwo.Id,
                    PlayerTwo = game.PlayerTwo.Names,
                    PlayerOne = game.Player.Names,
                    MovePlayerOne = listMoves.First(x => x.Id == gameRule.MovePlayerOneId).MoveName,
                    MovePlayerTwo = listMoves.First(x => x.Id == gameRule.MovePlayerTwoId).MoveName,
                    WinnerId = winner != null ? winner.Id : null,
                    Winner = winner != null ? winner.Names : "Empate"
                };


            }

            return new BodyResponse<RoundDto>
            {
                Code = (int)HttpStatusCode.OK,
                IsSuccess = true,
                Message = message,
                Data = round

            };
        }

        private GameRule ValidateWinner(List<GameMove> moves)
        {
            if ((moves == null || moves.Count == 0) || moves.Count < 2)
                throw new BusinessException(GeneralMessages.WinnerExceptionNotComplete);
            var t = _unitOfWork.GameRule.Where(x => x.MovePlayerOneId == moves[0].MoveId && x.MovePlayerTwoId == moves[1].MoveId).ToList();

            GameRule gameRule = _unitOfWork.GameRule.FirstOrDefault(x => x.MovePlayerOneId == moves[0].MoveId && x.MovePlayerTwoId == moves[1].MoveId);
            return gameRule;
        }
    }
}
