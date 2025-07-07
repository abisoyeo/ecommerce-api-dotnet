using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EcommerceApiSnapnetTestApp.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
