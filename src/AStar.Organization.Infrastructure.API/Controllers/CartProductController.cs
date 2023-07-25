using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CartProductController : Controller, ICrudableController<CartProductDto>
    {
        private readonly ICartProductService _cartProductService;

        public CartProductController(ICartProductService cartProductService)
        {
            _cartProductService = cartProductService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _cartProductService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _cartProductService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CartProductDto dto)
        {
            await _cartProductService.Update(dto);
            
            return new ContentResult
            {
                Content = $"КорзинаПродукт {dto.Id} обновлена",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartProductDto dto)
        {
            await _cartProductService.Create(dto);
            
            return new ContentResult
            {
                Content = $"КорзинаПродукт {dto.Id} создана.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartProductService.Delete(id);
            
            return new ContentResult
            {
                Content = $"КорзинаПродукт с id[{id}] удалена.",
                StatusCode = 200
            };
        }
    }
}