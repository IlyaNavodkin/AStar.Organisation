using AStar.Application.Dtos;
using AStar.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PositionController : Controller
    {
        readonly IPositionService _positionService;
        
        public PositionController(IPositionService positionServiceService)
        {
            _positionService = positionServiceService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PositionDto>> GetAll()
        {
            var dtos =  _positionService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public ActionResult<PositionDto> GetById(int id)
        {
            var dto =  _positionService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public IActionResult Update(PositionDto dto)
        {
            _positionService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Позиция {dto.Name} обновлена",
                StatusCode = 200
            };
        }

        [HttpPost]
        public IActionResult Create(PositionDto dto)
        {
            _positionService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Позиция {dto.Name} создана.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _positionService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Позиция с id[{id}] удалена.",
                StatusCode = 200
            };
        }
    }
}