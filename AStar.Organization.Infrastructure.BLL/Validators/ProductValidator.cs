using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.UnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrganizationContext _context;

    public ProductValidator(IUnitOfWork unitOfWork, OrganizationContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;

        RuleFor(e => e.Name)
            .MinimumLength(2)
            .WithMessage("Имя продукта не должно быть меньше двух символов.");
        
        RuleFor(e => e.Name)
            .MaximumLength(255)
            .WithMessage("Имя продукта не должно быть больше 255 символов.");

        RuleFor(e => e.Description)
            .MinimumLength(2)
            .WithMessage("Описание продукта не должно быть меньше двух символов.");
        
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Описание продукта не должно быть больше 255 символов.");
    }
}