using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey> (IQueryable<TEntity> InputQuery ,ISpecifications<TEntity,TKey > specifications)where TEntity:BaseEntity<TKey>
        {
            var Query = InputQuery;

            if(specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if(specifications.OrderBy is not null)
            {
                Query=Query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDescending);
            }

            if(specifications.IncludeExpressions != null && specifications.IncludeExpressions.Count>0)
            {
                foreach (var expression in specifications.IncludeExpressions)
                {
                    Query = Query.Include(expression);
                }

               // Query = specifications.IncludeExpressions.Aggregate(Query, (cur ,inc) =>  cur.Include(inc));
            }

            if(specifications.IsPagination)
            {
                Query=Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }

    }
}
