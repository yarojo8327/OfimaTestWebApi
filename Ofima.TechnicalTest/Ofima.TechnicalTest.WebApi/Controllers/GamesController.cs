using Microsoft.AspNetCore.Mvc;

using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;
using Ofima.TechnicalTest.Service.Interfaces;

namespace Ofima.TechnicalTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public IActionResult Post(RegisterGame request)
        {
            BodyResponse<object> result = _gameService.Add(request);
            return !result.IsSuccess ? BadRequest(result) : Ok(result);
        }

        [HttpPut]
        public IActionResult Put(RegisterGame request)
        {
            BodyResponse<object> result = _gameService.Put(request);
            return !result.IsSuccess ? BadRequest(result) : Ok(result);
        }

        [HttpGet("Winner/{gameId}/Round/{roundNumber}")]
        public IActionResult GetWinner(int gameId, int roundNumber)
        {
            BodyResponse<RoundDto> result = _gameService.GetWinner(gameId, roundNumber);
            return !result.IsSuccess ? BadRequest(result) : Ok(result);
        }
    }
}
