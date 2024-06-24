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
        #region Fields
        private readonly ITeamBll _teamsBll;
        #endregion

        #region Builder
        public TeamsController(ITeamBll teamsbll)
        {
            _teamsBll = teamsbll;
        }
        #endregion
        
        /// <summary>
        /// Get all teams information.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllTeams()
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

        /// <summary>
        /// Get teams information by name.
        /// </summary>
        [HttpGet("{name}")]
        public IActionResult GetTeamByName(string name)
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

        /// <summary>
        /// Get classification information by group.
        /// </summary>
        [HttpGet("group/{groupName}/classification")]
        public IActionResult GetTeamClassByGroup(string groupName)
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

        /// <summary>
        /// Get teams information by group.
        /// </summary>
        [HttpGet("group/{groupName}")]
        public IActionResult GetTeamsByGroup(string groupName)
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

        /// <summary>
        /// Add new team.
        /// </summary>
        [HttpPost]
        public IActionResult AddTeam([FromBody] Team value)
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
    }
}
