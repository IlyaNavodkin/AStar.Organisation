using AStar.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts;
using AStar.Organization.Core.DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly OrganizationContext _context;
 
        public PositionRepository(OrganizationContext context)
        {
            _context = context;
        }
 
        public IEnumerable<Position> GetAll()
        {
            return _context.Positions.ToList();
        }
 
        public Position GetById(int id)
        {
            return _context.Positions.Find(id);
        }
 
        public void Create(Position position)
        {
            _context.Positions.Add(position);
        }
 
        public void Update(Position position)
        {
            _context.Entry(position).State = EntityState.Modified;
        }
 
        public void Delete(int id)
        {
            var entity = _context.Positions.Find(id);
            if (entity != null)
                _context.Positions.Remove(entity);
        }
 
        public void Save()
        {
            _context.SaveChanges();
        }
 
        private bool disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}