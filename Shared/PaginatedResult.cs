using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult( int pageIndex, int pageSize, int totalCount ,IEnumerable<TEntity> data)
        {
            PageSize = pageSize;
            Count = totalCount;
            PageIndex = pageIndex;
            Data = data;
        }

        public int PageSize { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }

        public IEnumerable<TEntity> Data { get; set; }
    }
}
