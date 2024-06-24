using BusinessLogic.Group;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
//AUTHOR: José Ramón Gallego
//PROJECT: KingOfTheTower.WebAPI
namespace kot_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        #region Fields
        private readonly IGroupBll _groupsBll;
        #endregion

        #region Builder
        public GroupsController(IGroupBll groupsBll)
        {
            _groupsBll = groupsBll;
        }
        #endregion

        /// <summary>
        /// Get all groups information.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            try
            {
                var groupsList = _groupsBll.Get();

                return Ok(groupsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get group information by name.
        /// </summary>
        [HttpGet("{name}")]
        public IActionResult GetGroupByName(string name)
        {
            try
            {
                var group = _groupsBll.Get(name);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Add new group.
        /// </summary>
        [HttpPost]
        public IActionResult Register([FromBody] Group value)
        {
            try
            {
                if (value == null)
                    return BadRequest();

                var result = _groupsBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
