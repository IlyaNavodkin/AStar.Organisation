using AStar.Organisation.Core.Domain.Entities;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
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
}