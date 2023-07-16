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
        private readonly OrganizationContext _context;

        public CartService(IUnitOfWork unitOfWork, CartValidator cartValidator, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _cartValidator = cartValidator;
            _context = context;
        }

        public async Task<IEnumerable<CartDto>> GetAll()
        {
            var entities = await _context.Cart.ToListAsync();

            var dtos = entities.Select(e => new CartDto
            {
                Id = e.Id,
                CustomerId = e.CustomerId,
            });

            return dtos;
        }

        public async Task<CartDto> GetById(int id)
        {
            var entity = await _context.Cart.FirstOrDefaultAsync(e => e.Id == id);
            
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

            await _context.Cart.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CartDto dto)
        {
            var entity = await _context.Cart.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Cart));

            entity.CustomerId = dto.CustomerId;
            
            var result = await _cartValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _context.Cart.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Cart.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null) throw new NotFoundEntityException(nameof(Cart));
            
            _context.Cart.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}