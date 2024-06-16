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

        //GET: api/<GroupsController>
        [HttpGet("getAllGroups")]
        [Description("Enpoint that return a list with all groups")]
        public IActionResult Get()
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

        // GET api/<GroupsController>/5
        [HttpGet("getGroupByName/{name}")]
        [Description("Enpoint that return a list with all groups by name")]
        public IActionResult Get(string name)
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

        // POST api/<GroupsController>
        [HttpPost("register")]
        [Description("Enpoint that creates a new group")]
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
