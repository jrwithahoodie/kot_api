using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.AppContext;

namespace BusinessLogic.Category
{
    public class CategoryBLL : ICategoryBll
    {
        #region Fields
        private readonly Context _context;
        #endregion

        #region Builder
        public CategoryBLL()
        {
            _context = new Context();
        }
        #endregion

        public IEnumerable<Entities.Entities.Category> Get()
        {
            try
            {
                var categoryList = _context.Categories.ToList();

                return categoryList;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public Entities.Entities.Category Post(Entities.Entities.Category newCategoryData)
        {
            try
            {
                var existingCategory = _context.Categories
                    .Where(c => c.Name == newCategoryData.Name)
                    .ToList()
                    .FirstOrDefault();
                
                if (existingCategory != null)
                    throw new Exception("Esta categoría ya existe");
                
                var category = _context.Categories.Add(newCategoryData);
                _context.SaveChanges();

                return category.Entity;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}