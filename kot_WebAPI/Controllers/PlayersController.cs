using BusinessLogic.Player;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
//AUTHOR: José Ramón Gallego
//PROJECT: KingOfTheTower.WebAPI
namespace kot_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        #region Fields

        private readonly IPlayerBll _playersBll;

        #endregion

        #region Builder

        public PlayersController(IPlayerBll playersBll)
        {
            _playersBll = playersBll;
        }

        #endregion

        // GET: api/<PlayersController>
        [HttpGet("getAllPlayers")]
        [Description("Enpoint that return a list with all players")]
        public IActionResult Get()
        {
            try
            {
                var playerList = _playersBll.Get();

                return Ok(playerList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<PlayersController>/5
        [HttpGet("getPlayerByNif/{nif}")]
        [Description("Enpoint that return player by NIF")]
        public IActionResult Get(string nif)
        {
            try
            {
                var team = _playersBll.Get(nif);

                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/<PlayersController>
        [HttpPost("register")]
        [Description("Enpoint that create a new player")]
        public IActionResult Post([FromBody] Player value)
        {
            try
            {
                if (null == value)
                    return BadRequest();

                var result = _playersBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<PlayersController>
        [HttpPost("registerSeveral")]
        [Description("Enpoint that create multiple players")]
        public IActionResult PostSeveral([FromBody] IEnumerable<Player> value)
        {
            try
            {
                if (null == value)
                    return BadRequest();

                var result = _playersBll.PostSeveralPlayers(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        [Description("Enpoint that update player info")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayersController>/5
        [HttpDelete("{id}")]
        [Description("Enpoint that delete a player")]
        public void Delete(string name)
        {
        }
    }
}
