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
        Task <TEntity?> GetAsync(Tkey id);
        Task AddAsync(TEntity entity);

        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
