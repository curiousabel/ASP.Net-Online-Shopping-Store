using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
namespace Casestudy.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}