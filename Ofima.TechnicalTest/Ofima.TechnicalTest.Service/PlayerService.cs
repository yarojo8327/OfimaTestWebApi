using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Infraestructure.Interfaces;
using Ofima.TechnicalTest.Infraestructure.Models;
using Ofima.TechnicalTest.Common;

using System.Net;
using Ofima.TechnicalTest.Service.Interfaces;

namespace Ofima.TechnicalTest.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BodyResponse<object> RegisterPlayers(string playerOne, string playerTwo)
        {           
            List<Player> playerList = new();
            Player player = new() { Names = playerOne };
            playerList.Add(AddPlayer(player));

            player = new Player { Names = playerTwo };
            playerList.Add(AddPlayer(player));

            return new BodyResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                IsSuccess = true,
                Message = GeneralMessages.SaveDataSuccess,
                Data = playerList
            };
        }

        private Player AddPlayer(Player player)
        {
            Player currentPlayer = _unitOfWork.Player.FirstOrDefault(x => x.Names == player.Names);

            if (currentPlayer == null)
            {
                _unitOfWork.Player.Add(player);
                _unitOfWork.Save();
                return player;
            }

            return currentPlayer;
        }
    }
}
