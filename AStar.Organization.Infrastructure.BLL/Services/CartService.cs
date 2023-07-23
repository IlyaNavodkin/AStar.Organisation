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
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CartValidator _cartValidator;

        public CartService(IUnitOfWork unitOfWork, CartValidator cartValidator)
        {
            _unitOfWork = unitOfWork;
            _cartValidator = cartValidator;
        }

        public async Task<IEnumerable<CartDto>> GetAll()
        {
            var entities = await _unitOfWork.CartRepository.GetAll();

            var dtos = entities.Select(e => new CartDto
            {
                Id = e.Id,
                CustomerId = e.CustomerId,
            });

            return dtos;
        }

        public async Task<CartDto> GetById(int id)
        {
            var entity = await _unitOfWork.CartRepository.GetById(id);
            
            if (entity is null) throw new NotFoundEntityException(nameof(Cart));

            var dto = new CartDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
            };

            return dto;
        }

        public async Task Create(CartDto dto)
        {
            var entity = new Cart
            {
                CustomerId = dto.CustomerId,
            };

            var result = await _cartValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _unitOfWork.CartRepository.Create(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(CartDto dto)
        {
            var entity = await _unitOfWork.CartRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Cart));

            entity.CustomerId = dto.CustomerId;
            
            var result = await _cartValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _unitOfWork.CartRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CartRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Cart));
            
            _unitOfWork.CartRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}