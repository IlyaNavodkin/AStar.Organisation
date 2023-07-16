using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using FluentValidation;

namespace AStar.Organization.Infrastructure.BLL.Validators
{
    public class CartValidator : AbstractValidator<Cart>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrganizationContext _context;

        public CartValidator(IUnitOfWork unitOfWork, OrganizationContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}