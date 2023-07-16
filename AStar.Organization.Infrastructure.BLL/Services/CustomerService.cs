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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerValidator _customerValidator;
        private readonly OrganizationContext _context;

        public CustomerService(IUnitOfWork unitOfWork, CustomerValidator customerValidator, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _customerValidator = customerValidator;
            _context = context;
        }
        
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var entities = await _context.Customer.ToListAsync();

            var dtos = entities.Select(e => new CustomerDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone
            });

            return dtos;
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var entity = await _context.Customer.FirstOrDefaultAsync(e => e.Id == id);
            
            if (entity is null) throw new NotFoundEntityException(nameof(Customer));

            var dto = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };

            return dto;
        }

        public async Task Create(CustomerDto dto)
        {
            var entity = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var result = await _customerValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _context.Customer.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CustomerDto dto)
        {
            var entity = await _context.Customer.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Customer));

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            
            var result = await _customerValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _context.Customer.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Customer.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null) throw new NotFoundEntityException(nameof(Customer));
            
            _context.Customer.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}