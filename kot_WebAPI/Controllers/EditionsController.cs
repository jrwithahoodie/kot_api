using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Edition;
using Microsoft.AspNetCore.Mvc;

namespace kot_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditionsController : ControllerBase
    {
        #region Fields
        private readonly IEditionBll _editionBll;
        #endregion

        #region Builder
        public EditionsController(IEditionBll editionBll)
        {
            _editionBll = editionBll;
        }
        #endregion

        /// <summary>
        /// Get all editions information
        /// </summary>
        [HttpGet]
        public IActionResult GetAllEditions()
        {
            try
            {
                var teamList = _editionBll.Get();

                return Ok(teamList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Add new edition
        /// </summary>
        [HttpPost]
        public IActionResult AddEdition([FromBody] Entities.Entities.Edition newEditionData)
        {
            try
            {
                var result = _editionBll.Post(newEditionData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}