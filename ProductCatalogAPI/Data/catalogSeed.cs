using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public class catalogSeed
    {
        // multitreading who is win
        // seed my data
        // void - nothing to return
        //Async there is something to return
        public static async Task SeedAsync(CatalogContext context)
        {
            //this command will do ...find the changes b/n ur last and new
            //then update the changes
            //eg. after migrate i went add two columns in my catalog table
            //this means they should be now two columns in my table so this command
            // update the changes
            context.Database.Migrate();
            //keep adding duplication
            //Linq 
            //any checking rows in my table only if the table doesnt already have any rows
            if (!context.catalogBrands.Any())
            {
                //rule
                //if there is no row add some data
                //add ranges of rows
                    context.catalogBrands.AddRange
                    (GetPreConfigeredCatalogBrands());
                // this is important to save changes in database otherwise
                //only in temporary memory it will not commit in too the database
                await context.SaveChangesAsync();
            }
            if (!context.catalogTypes.Any())
            {
                //rule
                context.catalogTypes.AddRange
                    (GetPreConfigeredCatalogTypes());
                // this is important to save changes in database otherwise
                //only in temporary memory it will not commit in too the database
                context.SaveChanges();
            }
            if (!context.catalogItems.Any())
            {
                //rule
                context.catalogItems.AddRange
                    (GetPreConfigeredCatalogItems());
                // this is important to save changes in database otherwise
                //only in temporary memory it will not commit in too the database
                context.SaveChanges();
            }

        }

        //IEnumerable is a collection read only data
        //create a function that give as list of dummy brands
        private static IEnumerable<CatalogBrand> GetPreConfigeredCatalogBrands()
        {
            // return list of catlog brands
            return new List<CatalogBrand>()
            {
                //List look like
                // we hv two column in brand class ID and brand
                //ID is auto generate so only for brand'

                new CatalogBrand {Brand = "addidas"},
                new CatalogBrand {Brand = "Puma"},
                new CatalogBrand {Brand = "Slazenger"}
            };
        }
        private static IEnumerable<CatalogType> GetPreConfigeredCatalogTypes()
        {
            // return list of catlog brands
            return new List<CatalogType>()
            {
                //List look like
                // we hv two column in brand class ID and brand
                //ID is auto generate so only for brand'

                new CatalogType {Type = "Running"},
                new CatalogType {Type = "Basketball"},
                new CatalogType {Type = "Tennis"}
            };
        }

        private static IEnumerable<CatalogItem> GetPreConfigeredCatalogItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 3, Description = "Shoes for next century", Name = "World Star", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
               new CatalogItem() { CatalogTypeId = 1,CatalogBrandId = 2, Description = "will make you world champions", Name = "White Line", Price = 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 3, Description = "You have already won gold medal", Name = "Prism White Shoes", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },
                new CatalogItem() { CatalogTypeId = 1,CatalogBrandId = 1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new CatalogItem() { CatalogTypeId = 1,CatalogBrandId = 2, Description = "High Jumper", Name = "Addidas<White> Running", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new CatalogItem() { CatalogTypeId = 1,CatalogBrandId = 2, Description = "All round", Name = "Inredeible", Price = 248.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new CatalogItem() { CatalogTypeId = 2,CatalogBrandId = 1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new CatalogItem() { CatalogTypeId = 3,CatalogBrandId = 3, Description = "Tennis Star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new CatalogItem() { CatalogTypeId = 3,CatalogBrandId = 2, Description = "Wimbeldon", Name = "London Star", Price = 218.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new CatalogItem() { CatalogTypeId = 3,CatalogBrandId = 1, Description = "Rolan Garros", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

            };
        }


    }
}