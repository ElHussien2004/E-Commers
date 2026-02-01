using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories=[];
        public IGenericRepository<TEntity, TKey> GetReository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var Type= typeof(TEntity).Name;

          //  if(_repositories.ContainsKey(Type) ) return (IGenericRepository<TEntity,TKey>) _repositories[Type];
            if(_repositories.TryGetValue(Type ,out object value)) return (IGenericRepository<TEntity, TKey>) value;
            else
            {
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories.Add(Type, repo);
                return repo;
            }


        }

        public async Task<int> SaveChangeAsync()=>await _dbContext.SaveChangesAsync();
    }
}
