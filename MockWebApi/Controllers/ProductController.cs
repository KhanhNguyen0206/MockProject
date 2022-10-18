using Domain.Models;
using Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Numerics;

namespace MockWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("GetByProductID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> GetProductID(int productID)
        {
            if (productID <= 0)
            {
                return BadRequest(productID);
            }
            var Product = await _productService.GetByIdAsync(productID);

            if (Product.Data == null)
            {
                return NotFound();
            }

            return Ok(Product);

        }


        [HttpGet("GetByProductName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {

            if (name == null)
            {
                return BadRequest(name);
            }

            var product = await _productService.GetByNameAsync(name);

            if (product.Data == null)
            {

                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(ModelState);
            }
            ProductsValidator rules = new();
            var validator = rules.Validate(product);
            if (!validator.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validator.Errors);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _product = await _productService.AddProductAsync(product);

            if (_product.Success == false && _product.Message == "Exist")
            {
                ModelState.AddModelError("", $"Product Exist {_product}");
                return StatusCode(400, ModelState);
            }


            if (_product.Success == false && _product.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding product {_product}");
                return StatusCode(500, ModelState);
            }

            if (_product.Success == false && _product.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding product {_product}");
                return StatusCode(500, ModelState);
            }

            return Ok(_product);

        }


        [HttpPatch("Update", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(ModelState);
            }
            ProductsValidator rules = new();
            var validator = rules.Validate(product);
            if (!validator.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validator.Errors);
            }
            var _updateProduct = await _productService.UpdateProductAsync(product);

            if (_updateProduct.Success == false && _updateProduct.Message == "NotFound")
            {
                ModelState.AddModelError("", $"{product} Not Found");
                return StatusCode(404, ModelState);
            }

            if (_updateProduct.Success == false && _updateProduct.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating product {product}");
                return StatusCode(400, ModelState);
            }

            if (_updateProduct.Success == false && _updateProduct.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when updating product {product}");
                return StatusCode(500, ModelState);
            }


            return Ok(_updateProduct);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int Id)
        {

            var _deleteProduct = await _productService.DeleteProductAsync(Id);

            if(_deleteProduct.Success == false && _deleteProduct.Data == "NotFound")
            {
                ModelState.AddModelError("", "Product Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteProduct.Success == false && _deleteProduct.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting product");
                return StatusCode(500, ModelState);
            }

            if (_deleteProduct.Success == false && _deleteProduct.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting product");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}