using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using FluentValidation;

namespace AStar.Organisation.Core.DomainServices.Validators;

public class PositionValidator : AbstractValidator<Position>
{
    private readonly IPositionRepository _repository;

    public PositionValidator(IPositionRepository repository)
    {
        _repository = repository;
        
        RuleFor(e => e.Name)
            .MinimumLength(2)
            .WithMessage("Название позиции не должно быть меньше двух символов.");
        
        RuleFor(e => e.Name)
            .MaximumLength(20)
            .WithMessage("Название позиции не должно быть больше 20 символов.");

        RuleFor(item => item)
            .MustAsync(HaveUniqueChange)
            .WithMessage("Позиция с похожим именем и отделом уже есть в базе данных.");
    }
    
    private async Task<bool> HaveUniqueChange(Position item, CancellationToken cancellationToken)
    {
        var positions = await _repository.GetPositionsByDepartmentIdAndName(item.DepartmentId,item.Name);

        var duplicatePosition = positions.FirstOrDefault(p =>
            p.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase) &&
            p.DepartmentId == item.DepartmentId);

        return duplicatePosition == null;
    }
}