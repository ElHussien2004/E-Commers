using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : ICacheRepository

    {
        readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
            var Cachevalue=await _database.StringGetAsync(CacheKey);
            return Cachevalue.IsNullOrEmpty ?null :Cachevalue.ToString();   
        }

        public async Task SetAsync(string CacheKey, string Cachevalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, Cachevalue, TimeToLive);
        }
    }
}
