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
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
                     await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync() as IEnumerable<TEntity>
                   : await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return trackChanges ?                                                                                                          
                await _context.Set<TEntity>().ToListAsync()
             : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }
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

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec, bool TrackChange = false)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity,Tkey>spec)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), spec);
        }

       
    }
}
