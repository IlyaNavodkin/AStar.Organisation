using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Domain.Poco;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OrganizationContext _context;

        public CartRepository(OrganizationContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<Cart>> SkipAndTake(int startIndex, int andIndex)
        {
            var result = await _context.Cart.Skip(startIndex).Take(andIndex).ToListAsync();

            return result;
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
        
        public async Task<IEnumerable<CartRowProductDto>> GetCartRowProductsById(int cartId)
        {
            var query = @"
            SELECT c.Id AS CartId, p.Name AS ProductName, p.Description AS ProductDescription, p.Price AS ProductPrice
            FROM cart c
            INNER JOIN cartproduct cp ON c.Id = cp.cartid
            INNER JOIN product p ON cp.productid = p.Id
            WHERE c.Id = @CartId";

            var parameters = new { CartId = cartId };
            var dbConnection = _context.Database.GetDbConnection();
            
            return await dbConnection.QueryAsync<CartRowProductDto>(query, parameters);
        }
    }
}