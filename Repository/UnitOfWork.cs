using Domain.Data;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
        }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
