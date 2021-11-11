using BlazingShop.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingShop.DataLayer
{
   public class BlazingDbContext:DbContext
    {
        public BlazingDbContext(DbContextOptions<BlazingDbContext>options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
