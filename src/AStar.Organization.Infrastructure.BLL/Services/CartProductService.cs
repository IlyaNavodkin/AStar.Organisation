using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.Application.IUnitOfWork;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organization.Infrastructure.BLL.Exceptions;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CartProductValidator _cartProductValidator;

        public CartProductService(IUnitOfWork unitOfWork, CartProductValidator cartProductValidator)
        {
            _unitOfWork = unitOfWork;
            _cartProductValidator = cartProductValidator;
        }

        public async Task<IEnumerable<CartProductDto>> GetAll()
        {
            var entities = await _unitOfWork.CartProductRepository.GetAll();

            var dtos = entities.Select(e => new CartProductDto
            {
                Id = e.Id,
                CartId = e.CartId,
                ProductId = e.ProductId,
            });

            return dtos;
        }

        public async Task<CartProductDto> GetById(int id)
        {
            var entity = await _unitOfWork.CartProductRepository.GetById(id);
            
            if (entity is null) throw new NotFoundEntityException(nameof(CartProduct));

            var dto = new CartProductDto
            {
                Id = entity.Id,
                CartId = entity.CartId,
                ProductId = entity.ProductId,
            };

            return dto;
        }

        public async Task Create(CartProductDto dto)
        {
            var entity = new CartProduct
            {
                CartId = dto.CartId,
                ProductId = dto.ProductId,
            };

            var result = await _cartProductValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _unitOfWork.CartProductRepository.Create(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(CartProductDto dto)
        {
            var entity = await _unitOfWork.CartProductRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(CartProduct));

            entity.CartId = dto.CartId;
            entity.ProductId = dto.ProductId;
            
            var result = await _cartProductValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _unitOfWork.CartProductRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CartProductRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(CartProduct));
            
            _unitOfWork.CartProductRepository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}