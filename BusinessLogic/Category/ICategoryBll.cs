using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Category
{
    public interface ICategoryBll
    {
        IEnumerable<Entities.Entities.Category> Get();
        Entities.Entities.Category Post(Entities.Entities.Category newCategoryData);
    }
}