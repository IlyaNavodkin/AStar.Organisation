using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.UnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrganizationContext _context;

    public CustomerValidator(IUnitOfWork unitOfWork, OrganizationContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;

        RuleFor(e => e.Name)
            .MinimumLength(2)
            .WithMessage("Имя покупателя не должно быть меньше двух символов.");
        
        RuleFor(e => e.Name)
            .MaximumLength(255)
            .WithMessage("Имя покупателя не должно быть больше 255 символов.");
        
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email покупателя не может быть пустым.");

        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage("Неверный формат электронной почты.");
        
        RuleFor(e => e.Phone)
            .NotEmpty()
            .WithMessage("Номер телефона не должен быть пустым.");
    }
}