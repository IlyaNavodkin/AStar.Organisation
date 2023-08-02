using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private readonly OrganizationContext _context;

        public ProductPhotoRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<ProductPhoto>> GetAll()
        {
            return await _context.ProductPhoto.ToListAsync();
        }
     
        public async Task<ProductPhoto> GetById(int id)
        {
            return await _context.ProductPhoto.FindAsync(id);
        }
     
        public void Create(ProductPhoto entity)
        {
            _context.ProductPhoto.Add(entity);
        }
        
        public void Update(ProductPhoto entity)
        {
            _context.ProductPhoto.Update(entity);
        }
     
        public void Delete(int id)
        {
            var entity = _context.ProductPhoto.Find(id);
            
            if(entity != null) 
                _context.ProductPhoto.Remove(entity);
        }
    }
}