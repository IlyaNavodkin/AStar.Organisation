using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller, ICrudableController<CustomerDto>
    {
        private readonly ICustomerService _customerService;
        private readonly IPaginationService _paginationService;

        public CustomerController(ICustomerService customerService, IPaginationService paginationService)
        {
            _customerService = customerService;
            _paginationService = paginationService;
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

        [HttpGet]
        public async Task<IActionResult> GetPaginateItems(int pageNumber, int pageSize)
        {
            var items = await _customerService.GetAll();
            var dto = _paginationService.GetPaginationInfo<CustomerDto>(pageNumber, pageSize, items);
            
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerDto dto)
        {
            await _customerService.Update(dto);
            
            return new ContentResult
            {
                Content = $"Покупатель {dto.Name} обновлен",
                StatusCode = 200
            };
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            await _customerService.Create(dto);
            
            return new ContentResult
            {
                Content = $"Покупатель {dto.Name} создан.",
                StatusCode = 200
            };
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            
            return new ContentResult
            {
                Content = $"Покупатель с id[{id}] удален.",
                StatusCode = 200
            };
        }
    }
}