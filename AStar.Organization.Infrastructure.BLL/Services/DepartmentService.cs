using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.Services;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Exceptions;
using AStar.Organisation.Core.DomainServices.Repositories;
using AStar.Organisation.Core.DomainServices.Validators;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentValidator _departmentValidator;

        public DepartmentService(IDepartmentRepository departmentRepository, DepartmentValidator departmentValidator)
        {
            _departmentRepository = departmentRepository;
            _departmentValidator = departmentValidator;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAll()
        {
            var entities = await _departmentRepository.GetAll();
            var dtos = entities.Select(e => new DepartmentDto
            {
                Id = e.Id,
                Name = e.Name,
            });

            return dtos;
        }

        public async Task<DepartmentDto> GetById(int id)
        {
            var entity = await _departmentRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Department));

            var dto = new DepartmentDto
            {
                Id = id,
                Name = entity.Name,
            };
            
            return dto;
        }

        public async Task Create(DepartmentDto dto)
        {
            var entity = new Department
            {
                Name = dto.Name
            };

            var result = await _departmentValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            await _departmentRepository.Create(entity);
        }

        public async Task Update(DepartmentDto dto)
        {
            var entity = await _departmentRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Department));

            entity.Name = dto.Name;
            
            var result = await _departmentValidator.ValidateAsync(entity);

            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            await _departmentRepository.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _departmentRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Department));
            
            await _departmentRepository.Delete(entity.Id);
        }
    }
}