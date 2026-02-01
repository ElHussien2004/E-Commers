using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria=expression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        

        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }


        protected void AddOrderBy (Expression<Func<TEntity, object>> expression)
        {
            OrderBy=expression;
        }
        protected void AddOrderByDescending (Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending=expression;
        }
        #endregion

        #region Pagination
        public  int Skip { get; private set; }
        public  int Take { get; private set; }
        public  bool IsPagination { get; set; }

        protected void AddPagination(int PageSize, int PageIndex)
        {
            IsPagination=true;

            Take=PageSize;
            Skip= (PageIndex-1)*PageSize;
        }
        #endregion
    }
}
