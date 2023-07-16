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
    public class CartProductService : ICartProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CartProductValidator _cartProductValidator;
        private readonly OrganizationContext _context;

        public CartProductService(IUnitOfWork unitOfWork, CartProductValidator cartProductValidator, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _cartProductValidator = cartProductValidator;
            _context = context;
        }

        public async Task<IEnumerable<CartProductDto>> GetAll()
        {
            var entities = await _context.CartProduct.ToListAsync();

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
            var entity = await _context.CartProduct.FirstOrDefaultAsync(e => e.Id == id);
            
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

            await _context.CartProduct.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CartProductDto dto)
        {
            var entity = await _context.CartProduct.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(CartProduct));

            entity.CartId = dto.CartId;
            entity.ProductId = dto.ProductId;
            
            var result = await _cartProductValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _context.CartProduct.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.CartProduct.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null) throw new NotFoundEntityException(nameof(CartProduct));
            
            _context.CartProduct.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}