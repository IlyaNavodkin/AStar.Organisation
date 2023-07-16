// using AStar.Organisation.Core.Domain.Entities;
// using FluentValidation;
//
// namespace AStar.Organization.Infrastructure.BLL.Validators;
//
// public class DepartmentValidator : AbstractValidator<Department>
// {
//     public DepartmentValidator()
//     {
//         RuleFor(e => e.Name)
//             .MinimumLength(3)
//             .WithMessage("Название отдела не должно быть меньше трех символов.");
//         
//         RuleFor(e => e.Name)
//             .MaximumLength(20)
//             .WithMessage("Название отдела не должно быть больше 30 символов.");
//     }
// }