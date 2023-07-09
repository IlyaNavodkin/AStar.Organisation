using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _departmentService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _departmentService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDto dto)
        {
            await _departmentService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Отдел {dto.Name} обновлен",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            await _departmentService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Отдел {dto.Name} создан.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Отдел с id[{id}] удален.",
                StatusCode = 200
            };
        }
    }
}