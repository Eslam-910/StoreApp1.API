using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChange=false);
        Task<int> CountAsync(ISpecifications<TEntity,Tkey> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,Tkey>spec,bool TrackChange = false);
        Task <TEntity?> GetByIdAsync(Tkey id);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,Tkey> spec);
        Task AddAsync(TEntity entity);

        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
