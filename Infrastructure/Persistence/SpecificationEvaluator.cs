using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity>GetQuery<TEntity,Tkey>(IQueryable<TEntity>inputquery
            ,ISpecifications<TEntity,Tkey> spec)
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputquery;
            if (spec?.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy is not null)
                query= query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDescending is not null)
                query= query.OrderByDescending(spec.OrderByDescending);
            if (spec.IsPAgination)
                query=query.Skip(spec.Skip).Take(spec.Take);
                query = spec.IncludeExpressions.Aggregate(query, (currentQuery, includeexpression) => currentQuery.Include(includeexpression));
            return query;
        }
    }
}
