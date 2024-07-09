using AutoMapper;
using BusinessLogic.DTO;
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
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public GamesController(IGameBll gamesBll, IMapper mapper)
        {
            _gamesBll = gamesBll;
            _mapper = mapper;

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
                var gamesList = _gamesBll.GetAllGames();

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
        [HttpGet("info/{id}")]
        public IActionResult GetGameById(int id)
        {
            try
            {
                var game = _gamesBll.GetGame(id);

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
        [HttpGet("staff/{staffId}")]
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
        [HttpGet("court/{court}")]
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
        public IActionResult Register([FromBody] NewGameRequestDTO newGameData)
        {
            try
            {
                var result = _gamesBll.Post(newGameData);

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
        [HttpPut("result")]
        public IActionResult Put([FromBody] AlterGameResultRequestDTO gameResultInfo)
        {
            try
            {
                var gameUpdated = _gamesBll.Put(gameResultInfo);

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
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _gamesBll.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
