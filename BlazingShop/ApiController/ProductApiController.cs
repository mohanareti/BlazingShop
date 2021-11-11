using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingShop.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductApiController(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

        }

        public IActionResult Get()
        {
            var products = _productRepository.AllProducts.ToList();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productRepository.GetProductById(id);
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }
            else
            {

                _productRepository.CreateProduct(product);

                return Ok();
            }
        }
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] Product product)
        {
            var result = _productRepository.GetProductById(id);
            if (result == null)
            {
                return BadRequest($"Product with id {id.ToString()} not found");
            }
            else
            {
                _productRepository.EditProduct(id, product);

                return Ok();
            }
        }
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var result = _productRepository.GetProductById(id);
            if (result == null)
            {
                return BadRequest($"Product with id {id.ToString()} not found");
            }
            else
            {
                _productRepository.DeleteProduct(id);
                return Ok();
            }
        }
    }
}
