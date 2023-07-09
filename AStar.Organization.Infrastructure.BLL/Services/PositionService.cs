using AStar.Application.Dtos;
using AStar.Application.Services;
using AStar.Domain.Entities;
using AStar.Organization.Core.DomainServices.Exceptions;
using AStar.Organization.Core.DomainServices.Repositories;
using AStar.Organization.Core.DomainServices.Validators;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IPositionDapperRepository _positionDapperRepository;
        private readonly PositionValidator _positionValidator;

        public PositionService(IPositionRepository positionRepository, 
            IPositionDapperRepository positionDapperRepository, 
            PositionValidator positionValidator)
        {
            _positionRepository = positionRepository;
            _positionDapperRepository = positionDapperRepository;
            _positionValidator = positionValidator;
        }
        public async Task<IEnumerable<PositionDto>> GetAll()
        {
            var entities = await _positionDapperRepository.GetAll();
            var dtos = entities.Select(e => new PositionDto
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
            });

            return dtos;
        }

        public async Task<PositionDto> GetById(int id)
        {
            var entity = await _positionDapperRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));

            var dto = new PositionDto
            {
                Name = entity.Name,
                DepartmentId = entity.DepartmentId
            };
            
            return dto;
        }

        public async Task Create(PositionDto dto)
        {
            var entity = new Position
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId
            };

            var result = await _positionValidator.ValidateAsync(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            await _positionDapperRepository.Create(entity);
        }

        public async Task Update(PositionDto dto)
        {
            var entity = await _positionDapperRepository.GetById(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));

            entity.Name = dto.Name;
            entity.DepartmentId = dto.DepartmentId;
            
            var result = await _positionValidator.ValidateAsync(entity);

            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            await _positionDapperRepository.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _positionDapperRepository.GetById(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));
            
            await _positionDapperRepository.Delete(entity.Id);
        }
    }
}