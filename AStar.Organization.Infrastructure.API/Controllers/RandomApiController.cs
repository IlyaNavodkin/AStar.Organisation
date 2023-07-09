using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RandomApiController : Controller
    {
        private readonly IGetRandomApiService _getRandomApiService;

        public RandomApiController(IGetRandomApiService getRandomApiService)
        {
            _getRandomApiService = getRandomApiService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GenerateRandomApi()
        {
            var cardInfoDto = await _getRandomApiService.GetRandomName();
            
            return Ok(cardInfoDto);
        }
    }
}