using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organization.Infrastructure.BLL.Exceptions;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _productValidator;

        public ProductService(IUnitOfWork unitOfWork, ProductValidator productValidator)
        {
            _unitOfWork = unitOfWork;
            _productValidator = productValidator;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var entities = await _unitOfWork.ProductRepository.GetAll();

            var dtos = entities.Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                Description = e.Description
            });

            return dtos;
        }

        public async Task<ProductDto> GetById(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetById(id);
            
            if (entity is null) throw new NotFoundEntityException(nameof(Product));

            var dto = new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description
            };

            return dto;
        }

        public async Task Create(ProductDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description
            };

            var result = await _productValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _unitOfWork.ProductRepository.Create(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(ProductDto dto)
        {
            var entity = await _unitOfWork.ProductRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Product));

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
            
            var result = await _productValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _unitOfWork.ProductRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Product));
            
            _unitOfWork.ProductRepository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}