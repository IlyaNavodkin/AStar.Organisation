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