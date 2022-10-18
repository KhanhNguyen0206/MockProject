using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public virtual DbSet<Users>? Users { get; set; }
        public virtual DbSet<Product>? Products { get; set; }


    }
}
