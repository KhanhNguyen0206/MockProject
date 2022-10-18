using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class ProductRepository :  IProductRepository
    {

        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) 
        {
            _db = db;
        }

        public async Task<bool> ProductExistAsync(int Id)
        {
            return await _db.Products.AnyAsync(pro => pro.Id == Id);
        }

        public async Task<bool> ProductExistAsync(string Name)
        {
            return await _db.Products.AnyAsync(pro => pro.Name == Name);
        }


        public async Task<bool> CreateProductAsync(Product Product)
        {
            await _db.Products.AddAsync(Product);
            return await Save();
        }
        public async Task<bool> UpdateProductAsync(Product Product)
        {
            _db.Products.Update(Product);
            return await Save();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            return  _db.Products.AsEnumerable();
        }
        public async Task<Product> GetProductByIDAsync(int Id)
        {
            return _db.Products.FirstOrDefault(pro => pro.Id == Id);
        }

        public async Task<Product> GetProductByNameAsync(string ProductName)
        {
            return  _db.Products.FirstOrDefault(pro => pro.Name == ProductName);
        }

        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await GetProductByIDAsync(Id);

            if (product != null)
            {
                _db.Remove(product);
            }
           
            return await Save();
        }


    }
}
