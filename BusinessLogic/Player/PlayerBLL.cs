using AutoMapper;
using Azure.Core;
using BusinessLogic.DTO;
using Entities.AppContext;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Player
{
    public class PlayerBLL : IPlayerBll
    {
        #region Fields

        private readonly Context _context;
        private readonly IMapper _mapper;

        #endregion

        #region Builder

        public PlayerBLL(IMapper mapper)
        {
            _context = new Context();
            _mapper = mapper;
        }

        #endregion

        public IEnumerable<PlayerRequestResponseDTO> Get()
        {
            var playerList = _context.Players
                .Include(p => p.Team)
                .Include(p => p.Team.Edition)
                .Include(p => p.Team.Category)
                .ToList();

            return _mapper.Map<IEnumerable<PlayerRequestResponseDTO>>(playerList);
        }

        public Entities.Entities.Player GetByNif(string nif)
        {
            var player = _context.Players.Where( p => p.NIF == nif ).ToList().FirstOrDefault();

            return player;
        }

        public IEnumerable<PlayerRequestResponseDTO> GetByCategory(string categoryName)
        {
            try
            {
                var existingCategory = _context.Categories
                    .Where(c => c.Name == categoryName)
                    .ToList()
                    .FirstOrDefault()
                    ?? throw new Exception($"No existe ninguna categoría '{categoryName}' en la base de datos.");

                var playerList = _context.Players
                    .Include(p => p.Team)
                    .Include(p => p.Team.Edition)
                    .Include(p => p.Team.Category)
                    .Where(p => p.Team.CategoryId == existingCategory.Id)
                    .ToList();

                return _mapper.Map<IEnumerable<PlayerRequestResponseDTO>>(playerList);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public IEnumerable<PlayerRequestResponseDTO> GetByEdition(string editonName)
        {
            try
            {
                var existingEdition = _context.Editions
                    .Where(e => e.Name == editonName)
                    .ToList()
                    .FirstOrDefault()
                    ?? throw new Exception($"No existe ninguna edición '{editonName}' en la base de datos.");

                var playerList = _context.Players
                    .Include(p => p.Team)
                    .Include(p => p.Team.Edition)
                    .Include(p => p.Team.Category)
                    .Where(p => p.Team.EditionId == existingEdition.Id)
                    .ToList();

                return _mapper.Map<IEnumerable<PlayerRequestResponseDTO>>(playerList);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public IEnumerable<PlayerRequestResponseDTO> GetByTeam(string teamName)
        {
            try
            {
                var existingTeam = _context.Teams
                    .Where(t => t.Name == teamName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún equipo '{teamName}' en la base de datos");
                
                var playerList = _context.Players
                    .Include(p => p.Team)
                    .Include(p => p.Team.Edition)
                    .Include(p => p.Team.Category)
                    .Where(p => p.Team.Id == existingTeam.Id)
                    .ToList();
            
                return _mapper.Map<IEnumerable<PlayerRequestResponseDTO>>(playerList);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public PlayerRequestResponseDTO Post(PlayerRequestInputDTO newPlayerData)
        {
            try
            {
                if (string.IsNullOrEmpty(newPlayerData.NIF))
                    throw new Exception("El DNI del jugador no puede ser nulo/vacio:");

                if (!Regex.IsMatch(newPlayerData.NIF, @"^[XYZ]?\d{7,8}[A-Z]$"))
                    throw new Exception("El formato del DNI no es correcto.");

                var newPlayer = _mapper.Map<Entities.Entities.Player>(newPlayerData);

                var newPlayerTeam = _context.Teams
                    .Where(t => t.Name == newPlayerData.TeamName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("Este equipo no existe");

                newPlayer.TeamId = newPlayerTeam.Id;
                newPlayer.Team = newPlayerTeam;

                var result = _context.Players.Add(newPlayer);
                _context.SaveChanges();

                return _mapper.Map<PlayerRequestResponseDTO>(result.Entity);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public IEnumerable<Entities.Entities.Player> PostSeveralPlayers(IEnumerable<Entities.Entities.Player> value)
        {
            try
            {
                // 1) Comprobamos que la información de los
                // jugadores es válida

                foreach (var player in value)
                {
                    if (string.IsNullOrEmpty(player.NIF))
                        throw new Exception($"El DNI del jugador {player.Name} " +
                            "no puede ser nulo/vacio:");

                    if (!Regex.IsMatch(player.NIF, @"^[XYZ]?\d{7,8}[A-Z]$"))
                        throw new Exception($"El formato del DNI del jugador {player.Name}" +
                            " no es correcto.");
                }

                // *)
                var group = _context.Groups.Where(g => g.Name == value.FirstOrDefault().Team.Group.Name).FirstOrDefault();

                var auxTeam = value.FirstOrDefault().Team;
                auxTeam.Group = group;
                
                var resultTeam = _context.Teams.Add(auxTeam);
                var team = resultTeam.Entity;
                
                value.ToList().ForEach(p => p.Team = team);


                // 2) Si la validación del DNI no hay saltado nnguna excepción
                // se pueden guardar los jugadores en la base de datos

                _context.Players.AddRange(value);
                _context.SaveChanges();

                // 3) Cuando se realiza un guardado "masivo" EF Core no nos
                // devuelve la infrmación actualizada de lo guardado. Si queremos
                // devolver las entidades guardadas con los valores de id que le 
                // ha asignado la bd tenemos que traernoslo

                // creamos una lista auxiliar con los DNIs donde guardaremos
                // los NIF que se han enviado y lo utilizaremos como filtro
                var auxNifList = new List<string>();

                value.ToList().ForEach(p => auxNifList.Add(p.NIF));

                var result = _context.Players
                    .Where(p => auxNifList.Contains(p.NIF))
                    .AsNoTracking()
                    .ToList();

                return result;

            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

    }
}
