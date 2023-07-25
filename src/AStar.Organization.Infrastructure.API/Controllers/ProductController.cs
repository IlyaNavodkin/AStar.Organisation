using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller, ICrudableController<ProductDto>
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _productService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _productService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto dto)
        {
            await _productService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Продукт {dto.Name} обновлен",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            await _productService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Продукт {dto.Name} создан.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Продукт с id[{id}] удален.",
                StatusCode = 200
            };
        }
    }
}