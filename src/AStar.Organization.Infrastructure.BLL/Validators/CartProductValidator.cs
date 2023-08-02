using AStar.Organisation.Core.Application.IUnitOfWork;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators
{
    public class CartProductValidator : AbstractValidator<CartProduct>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrganizationContext _context;

        public CartProductValidator(IUnitOfWork unitOfWork, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}