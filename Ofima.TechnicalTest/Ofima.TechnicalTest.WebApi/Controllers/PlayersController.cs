using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Service.Interfaces;
using Ofima.TechnicalTest.WebApi.Models;

using System.Net;

namespace Ofima.TechnicalTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IValidator<RegisterPlayer> _validator;
        private readonly IPlayerService _playerService;

        public PlayersController(IValidator<RegisterPlayer> validator, IPlayerService playerService)
        {
            _validator = validator;
            _playerService = playerService;
        }

        [HttpPost("RegisterPlayers")]
        public async Task<IActionResult> RegisterPlayersAsync(RegisterPlayer request)
        {
            ValidationResult validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(FailValidation(validation));

            BodyResponse<object> response = _playerService.RegisterPlayers(request.PlayerOne, request.PlayerTwo);
            return Ok(response);
        }

        private static BodyResponse<object>? FailValidation(ValidationResult validation)
        {
            return new BodyResponse<object>
            {
                IsSuccess = false,
                Code = (int)HttpStatusCode.BadRequest,
                Message = "Error de validacion de campos",
                Data = validation.Errors.Select(x => x.ErrorMessage).ToList()
            };
        }
    }
}
