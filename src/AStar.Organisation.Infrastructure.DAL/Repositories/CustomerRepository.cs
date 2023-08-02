using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrganizationContext _context;

        public CustomerRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
        }
     
        public async Task<Customer> GetById(int id)
        {
            return await _context.Customer.FindAsync(id);
        }
     
        public void Create(Customer entity)
        {
            _context.Customer.Add(entity);
        }
        
        public void Update(Customer entity)
        {
            _context.Customer.Update(entity);
        }
     
        public void Delete(int id)
        {
            var entity = _context.Customer.Find(id);
            
            if(entity != null) 
                _context.Customer.Remove(entity);
        }
    }
}