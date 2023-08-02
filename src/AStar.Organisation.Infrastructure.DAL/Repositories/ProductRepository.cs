using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrganizationContext _context;

        public ProductRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product.ToListAsync();
        }
     
        public async Task<Product> GetById(int id)
        {
            return await _context.Product.FindAsync(id);
        }
     
        public void Create(Product entity)
        {
            _context.Product.Add(entity);
        }
        
        public void Update(Product entity)
        {
            _context.Product.Update(entity);
        }
     
        public void Delete(int id)
        {
            var entity = _context.Product.Find(id);
            
            if(entity != null) 
                _context.Product.Remove(entity);
        }
    }
}