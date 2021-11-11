using BlazingShop.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingShop.ServiceContract
{
   public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }

        Product GetProductById(int productId);

        void CreateProduct(Product product);

        void EditProduct(int id, Product product);

        void DeleteProduct(int id);
    }
}
