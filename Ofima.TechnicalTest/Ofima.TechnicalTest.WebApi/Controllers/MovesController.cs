using Microsoft.AspNetCore.Mvc;

using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;
using Ofima.TechnicalTest.Service;
using Ofima.TechnicalTest.Service.Interfaces;

using System.Net;

namespace Ofima.TechnicalTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private readonly IMoveService _moveService;

        public MovesController(IMoveService moveService)
        {
            _moveService = moveService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _moveService.GetAll();
            BodyResponse<IEnumerable<MoveDto>> response = new()
            {
                Code = result.Any() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent,
                Data = result,
                IsSuccess = result.Any(),
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post(RegisterMove request)
        {
            BodyResponse<object> result = _moveService.Add(request);
            return !result.IsSuccess ? BadRequest(result) : Ok(result);
        }

    }
}
