using BusinessLogic.Player;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        /// <summary>
        /// Get all players information.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllPlayers()
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

        /// <summary>
        /// Get specific user information by NIF.
        /// </summary>
        /// <param name="nif">Player's NIF.</param>
        [HttpGet("{nif}")]
        public IActionResult GetPlayerByNif(string nif)
        {
            try
            {
                var player = _playersBll.Get(nif);
                if (player == null)
                    return NotFound($"No se encontró jugador con NIF: {nif}");
                
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Add new player.
        /// </summary>
        /// <param name="value">New player data.</param>
        [HttpPost]
        public IActionResult AddPlayer([FromBody] Player value)
        {
            try
            {
                if (value == null)
                    return BadRequest("Datos del jugador no proporcionados.");

                var result = _playersBll.Post(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Adding several players.
        /// </summary>
        /// <param name="value">List of players to add.</param>
        [HttpPost("several")]
        public IActionResult AddSeveralPlayers([FromBody] IEnumerable<Player> value)
        {
            try
            {
                if (value == null || !value.Any())
                    return BadRequest("Datos de jugadores no proporcionados o vacíos.");

                var result = _playersBll.PostSeveralPlayers(value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
