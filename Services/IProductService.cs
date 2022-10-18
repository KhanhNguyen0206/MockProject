using Domain.Models;

namespace Services
{
    public interface IProductService
    {

        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetByIdAsync(int id);

        Task<ServiceResponse<Product>> GetByNameAsync(string name);
     
        Task<ServiceResponse<Product>> AddProductAsync(Product product);

        Task<ServiceResponse<Product>> UpdateProductAsync(Product product);

        Task<ServiceResponse<string>> DeleteProductAsync(int id);
    }
}
