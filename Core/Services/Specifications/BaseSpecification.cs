using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specifications
{
    public class BaseSpecification<TEntity, Tkey> : ISpecifications<TEntity, Tkey> 
        where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get ; set ; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>> ();
        public Expression<Func<TEntity, object>>? OrderBy { get; set ; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get ; set ; }
        public int Take { get ; set; }
        public int Skip { get ; set; }
        public bool IsPAgination { get ; set ; }

        public BaseSpecification(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria= expression;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }
        protected void AddOrederBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy= expression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending= expression;
        }

        protected void ApplyPagination(int pageindex,int pagesize)
        {
            IsPAgination = true;
            Take= pagesize;
            Skip=(pageindex-1)*pagesize ;
        }
    }
}
