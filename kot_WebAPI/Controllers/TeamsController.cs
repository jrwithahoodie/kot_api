using BusinessLogic.Team;
using BusinessLogic.User;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace kot_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        #region Fileds

        private readonly ITeamBll _teamsBll;

        #endregion

        #region Builder

        public TeamsController(ITeamBll teamsbll)
        {
            _teamsBll = teamsbll;
        }

        #endregion

        // GET: api/<UsersController>
        [HttpGet("getAllTeams")]
        [Description("Enpoint that return a list with all teams")]
        public IActionResult Get()
        {
            try
            {
                var teamList = _teamsBll.Get();

                return Ok(teamList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/TeamsController>/5
        [HttpGet("getTeamByName/{name}")]
        [Description("Enpoint that return a team by the name")]
        public IActionResult Get(string name)
        {
            try
            {
                var team = _teamsBll.Get(name);

                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("getTeamClassByGroup/{groupName}")]
        [Description("Enpoint that get the team classification by groups")]
        public IActionResult GetClassif(string groupName)
        {
            try
            {
                var teams = _teamsBll.GetClassif(groupName);

                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/TeamsController>/5
        [HttpGet("getTeamByGroup/{groupName}")]
        [Description("Enpoint that return a list of teams")]
        public IActionResult GetByGroup(string groupName)
        {
            try
            {
                var teams = _teamsBll.GetByGroup(groupName);

                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/<TeamsController>
        [HttpPost("register")]
        [Description("Enpoint that create a team")]
        public IActionResult Post([FromBody] Team value)
        {
            try
            {
                var result = _teamsBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        [Description("Enpoint that update team info")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        [Description("Enpoint that delete a team")]
        public void Delete(string name)
        {
        }
    }
}
