using Domain.Models;

namespace Repository
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetProductByIDAsync(int Id);

        Task<Product> GetProductByNameAsync(string ProductName);

        Task<bool> ProductExistAsync(string ProductName);

        Task<bool> ProductExistAsync(int Id);
        Task<bool> CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(int Id);

    }
}
