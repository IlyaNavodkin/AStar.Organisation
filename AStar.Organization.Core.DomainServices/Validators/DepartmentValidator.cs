using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using FluentValidation;

namespace AStar.Organisation.Core.DomainServices.Validators;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(e => e.Name)
            .MinimumLength(3)
            .WithMessage("Название отдела не должно быть меньше трех символов.");
        
        RuleFor(e => e.Name)
            .MaximumLength(20)
            .WithMessage("Название отдела не должно быть больше 30 символов.");
    }
}