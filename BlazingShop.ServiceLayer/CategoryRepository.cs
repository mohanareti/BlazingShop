using BlazingShop.DataLayer;
using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazingShop.ServiceLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlazingDbContext _blazingDb;
        public CategoryRepository(BlazingDbContext blazingDb)
        {
            _blazingDb = blazingDb;
        }
        public IEnumerable<Category> AllCategories => _blazingDb.Categories;

        public Category Create(Category category)
        {
            _blazingDb.Categories.Add(category);
            _blazingDb.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            var Deler = _blazingDb.Categories.FirstOrDefault(m => m.CId == id);
            if (Deler == null)
                return Deler;
            else
            {
                _blazingDb.Categories.Remove(Deler);
                _blazingDb.SaveChanges();
                return Deler;
            }
        }

        public Category Edit(int id, Category category)
        {
            var res = _blazingDb.Categories.FirstOrDefault(m => m.CId == id);
            if (res == null)
                return res;
            else
            {
                res.CName = category.CName;
                _blazingDb.SaveChanges();
                return res;
            }
        }

        public Category GetByid(int id)
        {
            return _blazingDb.Categories.FirstOrDefault(m => m.CId == id);
        }
    }
}
