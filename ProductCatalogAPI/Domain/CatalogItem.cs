using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Domain
{
    public class CatalogItem
    {
        // creating table / schema
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        // Creating Foreign key
        // primary key - Uniqly identify the column

        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        // Virtual - Im related to this table -- virtually
        public virtual CatalogType CatalogType { get; set; }  // Foreign key
        public virtual CatalogBrand CatalogBrand{ get; set; }
    }
}
