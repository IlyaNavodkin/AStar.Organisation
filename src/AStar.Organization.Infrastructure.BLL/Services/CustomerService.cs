using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organization.Infrastructure.BLL.Exceptions;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerValidator _customerValidator;

        public CustomerService(IUnitOfWork unitOfWork, CustomerValidator customerValidator)
        {
            _unitOfWork = unitOfWork;
            _customerValidator = customerValidator;
        }
        
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var entities = await _unitOfWork.CustomerRepository.GetAll();

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
            var entity = await _unitOfWork.CustomerRepository.GetById(id);
            
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

            _unitOfWork.CustomerRepository.Create(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(CustomerDto dto)
        {
            var entity = await _unitOfWork.CustomerRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Customer));

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            
            var result = await _customerValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _unitOfWork.CustomerRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CustomerRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Customer));
            
            _unitOfWork.CustomerRepository.Delete(entity.Id);
            _unitOfWork.SaveChanges();
        }
    }
}