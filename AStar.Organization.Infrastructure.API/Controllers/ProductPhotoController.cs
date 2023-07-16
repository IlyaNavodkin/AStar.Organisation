using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductPhotoController : Controller, ICrudableController<ProductPhotoDto>
    {
        private readonly IProductPhotoService _productPhotoService;

        public ProductPhotoController(IProductPhotoService productPhotoService)
        {
            _productPhotoService = productPhotoService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dtos =  await _productPhotoService.GetAll();
            
            return Ok(dtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var dto =  await _productPhotoService.GetById(id);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductPhotoDto dto)
        {
            await _productPhotoService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Фото продукта {dto.Id} обновлен",
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductPhotoDto dto)
        {
            await _productPhotoService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Фото продукта {dto.Id} создан.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productPhotoService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Фото продукта с id[{id}] удален.",
                StatusCode = 200
            };
        }
    }
}