using Domain.Models;
using Repository;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponse<Product>> AddProductAsync(Product product)
        {
            ServiceResponse<Product> _response = new();
            try
            {


                //Check If Product exist
                if (await _unitOfWork.Product.ProductExistAsync(product.Name))
                {
                    _response.Message = "Exist";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;

                }

                Product _newProduct = new()
                {

                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                };

                //Add new record
                if (!await _unitOfWork.Product.CreateProductAsync(_newProduct))
                {
                    _response.Error = "RepoError";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Data = _newProduct;
                _response.Message = "Created";

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

            }
            return _response;
        }

        public async Task<ServiceResponse<Product>> GetByNameAsync(string name)
        {
            ServiceResponse<Product> _response = new();

            try
            {

                var _product = await _unitOfWork.Product.GetProductByNameAsync(name);

                if (_product == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _product;


            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<Product>> GetByIdAsync(int Id)
        {
            ServiceResponse<Product> _response = new();

            try
            {


                var _product = await _unitOfWork.Product.GetProductByIDAsync(Id);

                if (_product == null)
                {

                    _response.Success = false;
                    _response.Message = "Not Found";
                    return _response;
                }

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _product;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            ServiceResponse<List<Product>> _response = new();

            try
            {

                var ProductList = await _unitOfWork.Product.GetAllAsync();
                _response.Success = true;
                _response.Message = "Success";
                _response.Data = ProductList.ToList();

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<string>> DeleteProductAsync(int id)
        {
            ServiceResponse<string> _response = new();

            try
            {
                var _product = await _unitOfWork.Product.ProductExistAsync(id);

                if (_product == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;

                }

                if (!await _unitOfWork.Product.DeleteProductAsync(id))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    return _response;
                }



                _response.Success = true;
                _response.Message = "Deleted";

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product product)
        {
            ServiceResponse<Product> _response = new();

            try
            {
                //check if record exist
                var _product = await _unitOfWork.Product.GetProductByIDAsync(product.Id);

                if (_product == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;

                }

                //Update
                _product.Name = product.Name;
                _product.Description = product.Name;
                _product.Price = product.Price;
                _product.ImageUrl = product.ImageUrl;

                if (!await _unitOfWork.Product.UpdateProductAsync(_product))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _product;

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
    }
}