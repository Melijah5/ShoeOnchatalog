using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    //instruction/definition - about the database 
    // - instruction primary key
    // -  forignkey
    public class CatalogContext: DbContext // EnitityFrameWorke.... DbContext  // inheritance
    {
        // constractor // no return type
        // what are option to constract the database : call base class pass the value
        // DbContextOptions - Dependencey injection
        //paramater DbcontextOption --- start up
        public CatalogContext(DbContextOptions options)
            : base(options)
        {

        }
        // using propduct catalog API ... table name catalogetypes is plurale
        // building a tabe  // Dbset is a table  //schema name is singular
        // schem what does on brand look like/ type
        public DbSet<CatalogType> catalogTypes { get; set; }
        public DbSet<CatalogBrand> catalogBrands { get; set; }
        public DbSet<CatalogItem> catalogItems { get; set; }

        // only applyed for inheritance which means when you hv a base classes
        // method that you want to change its behavior in the child class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // creation of model of the table and folow the configration
            // entity the name of the table
            // delegate
            modelBuilder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            modelBuilder.Entity<CatalogType>(ConfigureCatalogType);
            modelBuilder.Entity<CatalogItem>(ConfigureCatalogItem);
        }
        //method job to configer // metadata builder
        // builder help me to build my catalog/ entities
        private void ConfigureCatalogType(
            EntityTypeBuilder<CatalogType>builder)
        {
            // for every cataloge Id is required
            // builder go to catalogType and pick the property
            // Id auto generate Hilo
            //arror function is lambda
            builder.ToTable("CatalogType");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo");  // Auto generation
            builder.Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(100);
        }
        private void ConfigureCatalogBrand(
      EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_Brand_hilo");  // Auto generation
            builder.Property(c => c.Brand)
            .IsRequired()
            .HasMaxLength(100);
        }
        private void ConfigureCatalogItem(
  EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_hilo");  // Auto generation
            builder.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(50);
            builder.Property(c => c.Price)
                 .IsRequired();

            //Relationship 
            // one to many == one to one

            builder.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);

            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);
                
        }
    }
}
