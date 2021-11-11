using BlazingShop.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingShop.ServiceContract
{
   public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        Category GetByid(int id);
        Category Create(Category category);
        Category Edit(int id, Category category);
        Category Delete(int id);
    }
}
