using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.viewModel;

namespace ProductCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : ControllerBase
    {
        // local Paramater
        private readonly CatalogContext _CatalogContext;
        private readonly IConfiguration _configuration;

        // constractor
        //injection
        public CatalogController(CatalogContext catalogContext,
            IConfiguration configuration)
        {
            _CatalogContext = catalogContext;
            _configuration = configuration;
        }

        //by defualt get
        [HttpGet]
        [Route("[action]")]

        // like dropdown button
        // always EveryApi have an IactionResult  
        // like dropdown button catalogTypes
        public async Task<IActionResult>CatalogTypes()
        {
            //toListAsync convest all rows into list and write into items 
            var items = await _CatalogContext.catalogTypes.ToListAsync();
            return Ok(items);
        }


        [HttpGet]
        [Route("[action]")]

        // like dropdown button catalogBrand
        public async Task<IActionResult> CatalogBrands()
        {
            //toListAsync convest all rows into list
            var items = await _CatalogContext.catalogBrands.ToListAsync();
            return Ok(items);
        }



        [HttpGet]
        [Route("[action]")]
        // ? means Query Parameter

        public async Task<IActionResult> Items(
           //give me six element total 18 satrt from 0
           // data record limite
           [FromQuery] int pageSize = 6,
           [FromQuery] int pageIndex = 0
            )
        {
            // goto catalog context
            var totalItems = await
                 _CatalogContext.catalogItems.LongCountAsync();

            //order by Name
            var itemsOnpage = await _CatalogContext.catalogItems
                 .OrderBy(c => c.Name)

                 //pagination
                 //for example skip forwared 3index 3X6=18 and the grabe the next
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            //Modefied items of my new url
            itemsOnpage = ChangeUrlPlaceholder(itemsOnpage);


            var model = new PaginatedItemsViewModel<CatalogItem>(
               pageIndex, pageSize, totalItems, itemsOnpage
                );
            return Ok(model);

 //search By Name
        }
        [HttpGet]
        [Route("[action]/withname/{Name:minlength(1)}")]
        public async Task<IActionResult> Items(
            string Name,
             [FromQuery] int pagesize = 6,
             [FromQuery]int pageIndex = 0
            )
        {
            var totalItems = await
                 _CatalogContext.catalogItems.LongCountAsync();

            var ItemsOnPage = await _CatalogContext.catalogItems
                .Where(c => c.Name.StartsWith(Name))
                 .OrderBy(c => c.Name)
                 .Skip(pageIndex * pagesize)
                 .Take(pagesize)
                 .ToListAsync();

            ItemsOnPage = ChangeUrlPlaceholder(ItemsOnPage);

            // Pagination
            var model = new PaginatedItemsViewModel<CatalogItem>(
              pageIndex, pagesize, totalItems, ItemsOnPage
               );
            return Ok(model);
        }

// Search By type / brand

        [HttpGet]
        //items/type/1/brand/3 --- example
        [Route("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> Items(
            // ? nullable type-- allowing null value
            //Example -- items/type//brand/3
            int? catalogeBrandId,
            int? catalogeTypeId,
            [FromQuery] int pagesize = 6,
            [FromQuery]int pageIndex = 0
          )
        {
            //Iqueryable is just converting to Query
            var root = (IQueryable<CatalogItem>)_CatalogContext.catalogItems;
            if (catalogeTypeId.HasValue)
            {
                root = root.Where(c => c.CatalogTypeId == catalogeTypeId);
            }
            if (catalogeBrandId.HasValue)
            {
                root = root.Where(c => c.CatalogBrandId == catalogeBrandId);
            }

            // root is a query
            var totalItems = await root.LongCountAsync();

            var ItemsOnPage = await root
                 .OrderBy(c => c.Name)
                 .Skip(pageIndex * pagesize)
                 .Take(pagesize)
                 .ToListAsync();

            ItemsOnPage = ChangeUrlPlaceholder(ItemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(
              pageIndex, pagesize, totalItems, ItemsOnPage
               );
            return Ok(model);
        }

// used for used to fill the form and the sumbit
        [HttpPost]
        [Route("items")]
        public async Task<IActionResult> CreateProduct (
           [FromBody] CatalogItem Product
            )
        {
            var item = new CatalogItem
            {
                CatalogBrandId = Product.CatalogBrandId,
                CatalogTypeId = Product.CatalogTypeId,
                Description = Product.Description,
                Name = Product.PictureUrl,
                Price = Product.Price
            };

            _CatalogContext.catalogItems.Add(item);
            await _CatalogContext.SaveChangesAsync();
            //GetItemsbyId (item.Id)
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id });
        }
 // Get Item by ID

        [HttpGet]

     //limited to only integer number
     // Bad request
     // Not Found
        [Route("items/{id:int}")]
    
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            // this method return one item
            var item = await _CatalogContext.catalogItems
                  .SingleOrDefaultAsync(c => c.Id == id);
            //if the item is not null replace by a picture
            if (item != null)
            {
                item.PictureUrl = item.PictureUrl
                    .Replace("http://externalcatalogbaseurltobereplaced",
                _configuration["ExternalCatalogBaseurl"]);
                return Ok(item);
            }
            return NotFound();
        }
        // give to me the right item filled with the right Url

        private List<CatalogItem> ChangeUrlPlaceholder(List<CatalogItem> items)
        {
            items.ForEach(
                x => x.PictureUrl =
                x.PictureUrl
                //replace on the picture Url from the seed
                .Replace("http://externalcatalogbaseurltobereplaced",
                _configuration["ExternalCatalogBaseurl"]));
            return items;

        }

    }
}