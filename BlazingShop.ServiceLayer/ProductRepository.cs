using BlazingShop.DataLayer;
using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlazingShop.ServiceLayer
{
   public class ProductRepository : IProductRepository
    {
        private readonly BlazingDbContext _blazingDb;
        public ProductRepository(BlazingDbContext blazingDb)
        {
            _blazingDb = blazingDb;
        }
        public IEnumerable<Product> AllProducts => _blazingDb.Products.Include(m => m.Category);

        public void CreateProduct(Product product)
        {
            _blazingDb.Add(product);
            _blazingDb.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var existingProduct = _blazingDb.Products.FirstOrDefault(p => p.PId == id);
            var currentImage = Path.Combine(Directory.GetCurrentDirectory(), "~\\wwwroot\\Uploads", existingProduct.Image);
            _blazingDb.Products.Remove(existingProduct);
            //_blazingDbContext.SaveChanges();
            if (_blazingDb.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImage))
                {
                    System.IO.File.Delete(currentImage);
                }
            }
        }

        public void EditProduct(int id, Product product)
        {
            var updateProduct = _blazingDb.Products.FirstOrDefault(p => p.PId == id);
            updateProduct.PName = product.PName;
            updateProduct.Price = product.Price;
            updateProduct.ShadeColor = product.ShadeColor;
            updateProduct.CategoryId = product.CategoryId;
            updateProduct.Image = product.Image;
            _blazingDb.SaveChanges();
        }

        public Product GetProductById(int productId)
        {
            return _blazingDb.Products.Include(p=>p.Category).FirstOrDefault(p => p.PId == productId);
        }
    }
}
