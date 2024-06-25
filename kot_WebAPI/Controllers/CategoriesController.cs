using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Category;
using Microsoft.AspNetCore.Mvc;

namespace kot_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        #region Fields
        private readonly ICategoryBll _categoryBll;
        #endregion

        #region Builder
        public CategoriesController(ICategoryBll categoryBll)
        {
            _categoryBll = categoryBll;
        }
        #endregion

        /// <summary>
        /// Get all categories information
        /// </summary>
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categoriesList = _categoryBll.Get();

                return Ok(categoriesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Add new category
        /// </summary>
        [HttpPost]
        public IActionResult AddCategory([FromBody] Entities.Entities.Category newCategoryData)
        {
            try
            {
                var result = _categoryBll.Post(newCategoryData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}