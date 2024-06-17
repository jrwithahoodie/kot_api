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
        private readonly IPlayerBll _playersBll;

        public PlayersController(IPlayerBll playersBll)
        {
            _playersBll = playersBll;
        }

        /// <summary>
        /// Obtiene todos los jugadores.
        /// </summary>
        /// <remarks>
        /// Ejemplo de uso:
        /// GET /api/players/getAllPlayers
        /// </remarks>
        [HttpGet("getAllPlayers")]
        [Description("Endpoint que devuelve una lista con todos los jugadores.")]
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

        /// <summary>
        /// Obtiene un jugador por su NIF.
        /// </summary>
        /// <param name="nif">NIF del jugador.</param>
        /// <remarks>
        /// Ejemplo de uso:
        /// GET /api/players/getPlayerByNif/{nif}
        /// </remarks>
        [HttpGet("getPlayerByNif/{nif}")]
        [Description("Endpoint que devuelve un jugador por su NIF.")]
        public IActionResult Get(string nif)
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
        /// Registra un nuevo jugador.
        /// </summary>
        /// <param name="value">Datos del jugador a registrar.</param>
        /// <remarks>
        /// Ejemplo de uso:
        /// POST /api/players/register
        /// </remarks>
        [HttpPost("register")]
        [Description("Endpoint que crea un nuevo jugador.")]
        public IActionResult Post([FromBody] Player value)
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
        /// Registra varios jugadores simultáneamente.
        /// </summary>
        /// <param name="value">Lista de jugadores a registrar.</param>
        /// <remarks>
        /// Ejemplo de uso:
        /// POST /api/players/registerSeveral
        /// </remarks>
        [HttpPost("registerSeveral")]
        [Description("Endpoint que crea varios jugadores simultáneamente.")]
        public IActionResult PostSeveral([FromBody] IEnumerable<Player> value)
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

        /// <summary>
        /// Actualiza la información de un jugador.
        /// </summary>
        /// <param name="id">ID del jugador a actualizar.</param>
        /// <param name="value">Nuevos datos del jugador.</param>
        /// <remarks>
        /// Ejemplo de uso:
        /// PUT /api/players/{id}
        /// </remarks>
        [HttpPut("{id}")]
        [Description("Endpoint que actualiza la información de un jugador.")]
        public IActionResult Put(int id, [FromBody] Player value)
        {
            try
            {
                // Implementa la lógica de actualización según tus necesidades
                return Ok($"Jugador con ID {id} actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un jugador por su nombre.
        /// </summary>
        /// <param name="name">Nombre del jugador a eliminar.</param>
        /// <remarks>
        /// Ejemplo de uso:
        /// DELETE /api/players/{name}
        /// </remarks>
        [HttpDelete("{name}")]
        [Description("Endpoint que elimina un jugador por su nombre.")]
        public IActionResult Delete(string name)
        {
            try
            {
                // Implementa la lógica de eliminación según tus necesidades
                return Ok($"Jugador '{name}' eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
