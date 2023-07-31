using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organization.Infrastructure.BLL.Exceptions;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductPhotoValidator _productPhotoValidator;

        public ProductPhotoService(IUnitOfWork unitOfWork, ProductPhotoValidator productPhotoValidator)
        {
            _unitOfWork = unitOfWork;
            _productPhotoValidator = productPhotoValidator;
        }

        public async Task<IEnumerable<ProductPhotoDto>> GetAll()
        {
            var entities = await _unitOfWork.ProductPhotoRepository.GetAll();

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
            var entity = await _unitOfWork.ProductPhotoRepository.GetById(id);
            
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

            _unitOfWork.ProductPhotoRepository.Create(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(ProductPhotoDto dto)
        {
            var entity = await _unitOfWork.ProductPhotoRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(ProductPhoto));

            entity.ProductId = dto.ProductId;
            entity.Url = dto.Url;
            
            var result = await _productPhotoValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _unitOfWork.ProductPhotoRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = _unitOfWork.ProductPhotoRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(ProductPhoto));
            
            _unitOfWork.ProductPhotoRepository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}