﻿// using AStar.Organisation.Core.Application.Dtos;
// using AStar.Organisation.Core.Application.Services;
// using AStar.Organisation.Core.Domain.Entities;
// using AStar.Organisation.Core.DomainServices.Exceptions;
// using AStar.Organisation.Core.DomainServices.Repositories;
// using AStar.Organisation.Infrastructure.DAL.Repositories;
// using AStar.Organization.Infrastructure.BLL.Validators;
// using FluentValidation;
//
// namespace AStar.Organization.Infrastructure.BLL.Services
// {
//     public class PositionService : IPositionService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly CustomerValidator _customerValidator;
//
//         public PositionService(IUnitOfWork unitOfWork, 
//             CustomerValidator customerValidator)
//         {
//             _unitOfWork = unitOfWork;
//             _customerValidator = customerValidator;
//         }
//         public async Task<IEnumerable<PositionDto>> GetAll()
//         {
//             var entities = await _unitOfWork.PositionRepository.GetAll();
//             var dtos = entities.Select(e => new PositionDto
//             {
//                 Id = e.Id,
//                 Name = e.Name,
//                 DepartmentId = e.DepartmentId,
//             });
//
//             return dtos;
//         }
//
//         public async Task<PositionDto> GetById(int id)
//         {
//             var entity = await _unitOfWork.PositionRepository.GetById(id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Position));
//
//             var dto = new PositionDto
//             {
//                 Name = entity.Name,
//                 Id = entity.Id,
//                 DepartmentId = entity.DepartmentId
//             };
//             
//             return dto;
//         }
//
//         public async Task Create(PositionDto dto)
//         {
//             var entity = new Position
//             {
//                 Name = dto.Name,
//                 DepartmentId = dto.DepartmentId
//             };
//
//             var result = await _customerValidator.ValidateAsync(entity);
//             if (!result.IsValid) throw new ValidationException(result.Errors);
//         
//             await _unitOfWork.PositionRepository.Create(entity);
//         
//             _unitOfWork.Commit();
//         }
//
//         public async Task Update(PositionDto dto)
//         {
//             var entity = await _unitOfWork.PositionRepository.GetById(dto.Id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Position));
//
//             entity.Name = dto.Name;
//             entity.DepartmentId = dto.DepartmentId;
//         
//             var result = await _customerValidator.ValidateAsync(entity);
//
//             if (!result.IsValid) throw new ValidationException(result.Errors);
//         
//             await _unitOfWork.PositionRepository.Update(entity);
//
//             throw new Exception("Упси");
//             _unitOfWork.Commit();
//         }
//
//         public async Task Delete(int id)
//         {
//             var entity = await _unitOfWork.PositionRepository.GetById(id);
//             if (entity is null) throw new NotFoundEntityException(nameof(Position));
//         
//             await _unitOfWork.PositionRepository.Delete(entity.Id);
//         
//             _unitOfWork.Commit();
//         }
//     }
// }