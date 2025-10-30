using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChange = false)
        {
            if (TrackChange)
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync(); 
        }

        public async Task<TEntity?> GetAsync(Tkey id)
        {
           return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }
        public void UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
        }
       
    }
}
