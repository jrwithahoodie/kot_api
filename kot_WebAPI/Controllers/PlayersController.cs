using AutoMapper;
using BusinessLogic.DTO;
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
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public PlayersController(IPlayerBll playersBll, IMapper mapper)
        {
            _playersBll = playersBll;
            _mapper = mapper;
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
        [HttpGet("doc/{nif}")]
        public IActionResult GetPlayerByNif(string nif)
        {
            try
            {
                var player = _playersBll.GetByNif(nif);
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
        /// Get all user information by category.
        /// </summary>
        /// <param name="category">Players category.</param>
        [HttpGet("category")]
        public IActionResult GetPlayerByCategory(string category)
        {
            try
            {
                var player = _playersBll.GetByCategory(category);
                
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get all user information by edition.
        /// </summary>
        /// <param name="edition">Players edition.</param>
        [HttpGet("edition")]
        public IActionResult GetPlayerByEdition(string edition)
        {
            try
            {
                var player = _playersBll.GetByEdition(edition);
                
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get all user information by team.
        /// </summary>
        /// <param name="teamName">Players team.</param>
        [HttpGet("team")]
        public IActionResult GetPlayerByTeam(string teamName)
        {
            try
            {
                var player = _playersBll.GetByTeam(teamName);
                
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
        [HttpPost("new")]
        public IActionResult AddPlayer([FromBody] PlayerRequestInputDTO value)
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
        [HttpPost("new/several")]
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
