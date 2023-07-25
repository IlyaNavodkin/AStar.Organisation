using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IRepositories;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OrganizationContext _context;

        public CartRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Cart.ToListAsync();
        }
     
        public async Task<Cart> GetById(int id)
        {
            return await _context.Cart.FindAsync(id);
        }
     
        public void Create(Cart entity)
        {
            _context.Cart.Add(entity);
        }
        
        public void Update(Cart entity)
        {
            _context.Cart.Update(entity);
        }
     
        public void Delete(int id)
        {
            var entity = _context.Cart.Find(id);
            
            if(entity != null) 
                _context.Cart.Remove(entity);
        }
    }
}