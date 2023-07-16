using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Exceptions;
using AStar.Organisation.Core.DomainServices.UnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _productValidator;
        private readonly OrganizationContext _context;

        public ProductService(IUnitOfWork unitOfWork, ProductValidator productValidator, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _productValidator = productValidator;
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var entities = await _context.Product.ToListAsync();

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
            var entity = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            
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

            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductDto dto)
        {
            var entity = await _context.Product.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Product));

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
            
            var result = await _productValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _context.Product.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null) throw new NotFoundEntityException(nameof(Product));
            
            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}