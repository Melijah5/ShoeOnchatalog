using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.viewModel
{
    //TEntity- Type of Entity -- Generic
    public class PaginatedItemsViewModel<TEntity>
        where TEntity: class

    {
        // Property
        //private set - i can control
        public int PageSize { get; private set; }
        public int PageIndex { get; set; }

        public long Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        // constractor 
        public PaginatedItemsViewModel(int pageIndex, int pageSize,
            long count, IEnumerable <TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
