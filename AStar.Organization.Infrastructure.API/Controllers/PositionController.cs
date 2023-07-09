using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionServiceService)
        {
            _positionService = positionServiceService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _positionService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _positionService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PositionDto dto)
        {
            await _positionService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Позиция {dto.Name} обновлена",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionDto dto)
        {
            await _positionService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Позиция {dto.Name} создана.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Позиция с id[{id}] удалена.",
                StatusCode = 200
            };
        }
    }
}