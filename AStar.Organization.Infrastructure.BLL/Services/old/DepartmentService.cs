// using AStar.Organisation.Core.Application.Dtos;
// using AStar.Organisation.Core.Application.Services;
// using AStar.Organisation.Core.Domain.Entities;
// using AStar.Organisation.Core.DomainServices.Exceptions;
// using AStar.Organisation.Core.DomainServices.Repositories;
// using AStar.Organization.Infrastructure.BLL.Validators;
// using FluentValidation;
//
// namespace AStar.Organization.Infrastructure.BLL.Services
// {
//     public class DepartmentService : IDepartmentService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly DepartmentValidator _departmentValidator;
//
//         public DepartmentService(IUnitOfWork unitOfWork, DepartmentValidator departmentValidator)
//         {
//             _unitOfWork = unitOfWork;
//             _departmentValidator = departmentValidator;
//         }
//
//         public async Task<IEnumerable<DepartmentDto>> GetAll()
//         {
//             var entities = await _unitOfWork.DepartmentRepository.GetAll();
//             var dtos = entities.Select(e => new DepartmentDto
//             {
//                 Id = e.Id,
//                 Name = e.Name,
//             });
//
//             return dtos;
//         }
//
//         public async Task<DepartmentDto> GetById(int id)
//         {
//             var entity = await _unitOfWork.DepartmentRepository.GetById(id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Department));
//
//             var dto = new DepartmentDto
//             {
//                 Id = id,
//                 Name = entity.Name,
//             };
//             
//             return dto;
//         }
//
//         public async Task Create(DepartmentDto dto)
//         {
//             var entity = new Department
//             {
//                 Name = dto.Name
//             };
//
//             var result = await _departmentValidator.ValidateAsync(entity);
//             if (!result.IsValid) throw new ValidationException(result.Errors);
//         
//
//             await _unitOfWork.DepartmentRepository.Create(entity);
//         
//             _unitOfWork.Commit();
//         }
//
//         public async Task Update(DepartmentDto dto)
//         {
//             var entity = await _unitOfWork.DepartmentRepository.GetById(dto.Id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Department));
//
//             entity.Name = dto.Name;
//             
//             var result = await _departmentValidator.ValidateAsync(entity);
//
//             if (!result.IsValid) throw new ValidationException(result.Errors);
//
//             await _unitOfWork.DepartmentRepository.Update(entity);
//         
//             _unitOfWork.Commit();
//         }
//
//         public async Task Delete(int id)
//         {
//             var entity = await _unitOfWork.DepartmentRepository.GetById(id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Department));
//             
//             await _unitOfWork.DepartmentRepository.Delete(entity.Id);
//             
//             _unitOfWork.Commit();
//         }
//     }
// }