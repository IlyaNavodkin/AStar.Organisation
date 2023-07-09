using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using FluentValidation;

namespace AStar.Organisation.Core.DomainServices.Validators;

public class DepartmentValidator : AbstractValidator<Department>
{
    private readonly IRepository<Department> _departmentRepository;

    public DepartmentValidator(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;

        RuleFor(e => e.Name)
            .MinimumLength(3)
            .WithMessage("Название отдела не должно быть меньше трех символов.");
        
        RuleFor(e => e.Name)
            .MaximumLength(20)
            .WithMessage("Название отдела не должно быть больше 30 символов.");

        // RuleFor(item => item)
        //     .Must(HaveUniqueChange)
        //     .WithMessage("Позиция с похожим именем и отделом уже есть в базе данных.");
    }
    
    // private bool HaveUniqueChange(Position item)
    // {
    //     var change = _repository.Ge
    //         .FirstOrDefault(p => p.Description == item.Description && p.ChangeTime == item.ChangeTime );
    //     
    //     if (change is null) return true;
    //     if (item.Id == change.Id) return true;
    //     
    //     return false;
    // }
}