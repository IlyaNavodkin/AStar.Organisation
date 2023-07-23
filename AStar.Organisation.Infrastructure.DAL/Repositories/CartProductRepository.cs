using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IRepositories;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CartProductRepository : ICartProductRepository
    {
        private readonly OrganizationContext _context;

        public CartProductRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<CartProduct>> GetAll()
        {
            return await _context.CartProduct.ToListAsync();
        }
     
        public async Task<CartProduct> GetById(int id)
        {
            return await _context.CartProduct.FindAsync(id);
        }
     
        public void Create(CartProduct entity)
        {
            _context.CartProduct.Add(entity);
        }
        
        public void Update(CartProduct entity)
        {
            _context.CartProduct.Update(entity);
        }
     
        public void Delete(int id)
        {
            var entity = _context.CartProduct.Find(id);
            
            if(entity != null) 
                _context.CartProduct.Remove(entity);
        }
    }
}