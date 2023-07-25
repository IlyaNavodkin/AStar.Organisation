using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators
{
    public class ProductPhotoValidator : AbstractValidator<ProductPhoto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrganizationContext _context;

        public ProductPhotoValidator(IUnitOfWork unitOfWork, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        
            RuleFor(e => e.Url)
                .MinimumLength(2)
                .WithMessage("Ссылка на фотографию продукта не должна быть меньше двух символов.");
        }
    }
}