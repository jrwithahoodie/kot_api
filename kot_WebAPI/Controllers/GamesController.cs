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

        /// <summary>
        /// Get all games information.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllGames()
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

        /// <summary>
        /// Get game information by id.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetGameById(int id)
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

        /// <summary>
        /// Get all games information by staff.
        /// </summary>
        [HttpGet("{staffId}")]
        public IActionResult GetGameByStaffId(int staffId)
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

        /// <summary>
        /// Get all games information by court.
        /// </summary>
        [HttpGet("{court}")]
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

        /// <summary>
        /// Add new game.
        /// </summary>
        [HttpPost]
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

        /// <summary>
        /// Update game scoring result.
        /// </summary>
        [HttpPut("{id}/{score1}/{score2}")]
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

        /// <summary>
        /// Delete game.
        /// </summary>
        [HttpDelete("{id}")]
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
