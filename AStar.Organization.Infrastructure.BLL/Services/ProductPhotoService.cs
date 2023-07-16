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
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductPhotoValidator _productPhotoValidator;
        private readonly OrganizationContext _context;

        public ProductPhotoService(IUnitOfWork unitOfWork, ProductPhotoValidator productPhotoValidator, 
            OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _productPhotoValidator = productPhotoValidator;
            _context = context;
        }

        public async Task<IEnumerable<ProductPhotoDto>> GetAll()
        {
            var entities = await _context.ProductPhoto.ToListAsync();

            var dtos = entities.Select(e => new ProductPhotoDto
            {
                Id = e.Id,
                Url = e.Url,
                ProductId = e.ProductId,
            });

            return dtos;
        }

        public async Task<ProductPhotoDto> GetById(int id)
        {
            var entity = await _context.ProductPhoto.FirstOrDefaultAsync(e => e.Id == id);
            
            if (entity is null) throw new NotFoundEntityException(nameof(ProductPhoto));

            var dto = new ProductPhotoDto
            {
                Id = entity.Id,
                Url = entity.Url,
                ProductId = entity.ProductId,
            };

            return dto;
        }

        public async Task Create(ProductPhotoDto dto)
        {
            var entity = new ProductPhoto()
            {
                ProductId = dto.ProductId,
                Url = dto.Url,
            };

            var result = await _productPhotoValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _context.ProductPhoto.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductPhotoDto dto)
        {
            var entity = await _context.ProductPhoto.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(ProductPhoto));

            entity.ProductId = dto.ProductId;
            entity.Url = dto.Url;
            
            var result = await _productPhotoValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _context.ProductPhoto.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.ProductPhoto.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null) throw new NotFoundEntityException(nameof(ProductPhoto));
            
            _context.ProductPhoto.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}