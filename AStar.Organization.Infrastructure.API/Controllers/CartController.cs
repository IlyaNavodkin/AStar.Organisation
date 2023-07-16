using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CartController : Controller, ICrudableController<CartDto>
    {
        private readonly ICartService _customerService;

        public CartController(ICartService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _customerService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _customerService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CartDto dto)
        {
            await _customerService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Корзина {dto.Id} обновлена",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartDto dto)
        {
            await _customerService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Корзина {dto.Id} создана.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Корзина с id[{id}] удалена.",
                StatusCode = 200
            };
        }
    }
}