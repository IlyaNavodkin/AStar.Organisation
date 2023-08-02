using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CartController : Controller, ICrudableController<CartDto>
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _cartService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _cartService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CartDto dto)
        {
            await _cartService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Корзина {dto.Id} обновлена",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartDto dto)
        {
            await _cartService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Корзина {dto.Id} создана.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Корзина с id[{id}] удалена.",
                StatusCode = 200
            };
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCartRowProductsById(int cartId)
        {
            var dtos =  await _cartService.GetCartRowProductsById(cartId);
            
            return Ok(dtos);
        }
    }
}