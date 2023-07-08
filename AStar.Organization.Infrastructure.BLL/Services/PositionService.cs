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
        public IEnumerable<PositionDto> GetAll()
        {
            var entities = _positionDapperRepository.GetAll();
            var dtos = entities.Select(e => new PositionDto
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
            });

            return dtos;
        }

        public PositionDto GetById(int id)
        {
            var entity = _positionRepository.GetByid(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));

            var dto = new PositionDto
            {
                Name = entity.Name,
                DepartmentId = entity.DepartmentId
            };
            
            return dto;
        }

        public void Create(PositionDto dto)
        {
            var entity = new Position
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId
            };

            var result = _positionValidator.Validate(entity);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _positionRepository.Create(entity);
            _positionRepository.Save();
        }

        public PositionDto Update(PositionDto dto)
        {
            var entity = _positionRepository.GetByid(dto.Id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));

            entity.Name = dto.Name;
            entity.DepartmentId = dto.DepartmentId;
            var result = _positionValidator.Validate(entity);

            if (!result.IsValid) throw new ValidationException(result.Errors);
            
            _positionRepository.Save();
            
            return dto;
        }

        public void Delete(int id)
        {
            var entity = _positionRepository.GetByid(id);
            if (entity is null) throw new NotFoundEntityException(nameof(Position));
            
            _positionRepository.Delete(entity.Id);
            _positionRepository.Save();
        }
    }
}