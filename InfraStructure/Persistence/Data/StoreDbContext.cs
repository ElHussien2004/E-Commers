using DomainLayer.Models.ProductModules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class StoreDbContext:DbContext 
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> Options):base(Options)
        {
            
        }
        public DbSet<Product>Products {  get; set; }
        public DbSet<ProductBrand>ProductBrands { get; set; }
        public DbSet<ProductType>ProductTypes {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemplyRefrance).Assembly);
        }


    }
}