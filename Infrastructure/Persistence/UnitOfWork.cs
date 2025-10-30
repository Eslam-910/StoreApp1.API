using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly ConcurrentDictionary<string, object> _Repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _Repositories = new ConcurrentDictionary<string, object>(); 
        }
        //public  IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        //{
        //    var type=typeof(TEntity).Name;
        //    if (!_Repositories.ContainsKey(type))
        //    {
        //      var repository= new GenericRepository<TEntity, Tkey>(_context);
        //        _Repositories.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, Tkey>) _Repositories[type];
        //}

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
           return (IGenericRepository<TEntity, Tkey>)_Repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_context));
        }



        public async Task<int> SavesChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
