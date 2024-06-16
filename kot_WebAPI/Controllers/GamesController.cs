using BusinessLogic.Game;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
//AUTHOR: José Ramón Gallego
//PROJECT: KingOfTheTower.WebAPI
namespace kot_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        #region Fields

        private readonly IGameBll _gamesBll;

        #endregion

        #region Builder

        public GamesController(IGameBll gamesBll)
        {
            _gamesBll = gamesBll;
        }

        #endregion


        // GET: api/<UsersController>
        [HttpGet("getAllGames")]
        [Description("Enpoint that return a list with all games")]
        public IActionResult Get()
        {
            try
            {
                var gamesList = _gamesBll.Get();

                return Ok(gamesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("getGameById/{id}")]
        [Description("Enpoint that return a list with all games and filter it by the staff id")]
        public IActionResult Get(int id)
        {
            try
            {
                var game = _gamesBll.Get(id);

                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("getGameByStaff/{staffId}")]
        [Description("Enpoint that return a list with all games and filter it by the staff id")]
        public IActionResult GetGetByStaff(int staffId)
        {
            try
            {
                var gameList = _gamesBll.GetByStaff(staffId);

                return Ok(gameList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("getGameByCourt/{court}")]
        [Description("Enpoint that return a list with all games and filter it by the court")]
        public IActionResult GetGamesByCourt(int court)
        {
            try
            {
                var game = _gamesBll.GetByCourt(court);

                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/<UsersController>
        [HttpPost("register")]
        [Description("Enpoint that receive a Game object and insert into our db")]
        public IActionResult Register([FromBody] Game value)
        {
            try
            {
                var result = _gamesBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}/{score1}/{score2}")]
        [Description("Enpoint that update a game with the id given and scores given on the request")]
        public IActionResult Put(int id, int score1, int score2)
        {
            try
            {
                var gameUpdated = _gamesBll.Put(id, score1, score2);

                return Ok(gameUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Description("Enpoint that delete the game with an id")]
        public IActionResult Delete(int id)
        {
            try
            {
                _gamesBll.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
