using BusinessLogic.DTO;
using Entities.AppContext;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BusinessLogic.Team
{
    public class TeamBLL : ITeamBll
    {
        #region Fields

        private readonly Context _context;
        private readonly IMapper _mapper;

        #endregion

        #region Builder

        public TeamBLL(IMapper mapper)
        {
            _context = new Context();
            _mapper = mapper;
        }

        #endregion

        public IEnumerable<TeamRequestResponseDTO> Get()
        {
            var teamList = _context.Teams
                .Include(t => t.Group)
                .Include(e => e.Edition)
                .Include(c => c.Category)
                .OrderBy(t => t.Group.Id)
                .OrderByDescending(c => c.Classification_points)
                .OrderByDescending(ba => ba.Points_diff)
                .ToList();
            
            var playersByTeam = _context.Players
                .GroupBy(p => p.TeamId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var teamListMapped = teamList.Select(t =>{
                var teamDto = _mapper.Map<TeamRequestResponseDTO>(t);
                teamDto.TeamPlayers = playersByTeam.ContainsKey(t.Id)
                    ? _mapper.Map<List<PlayerRequestResponseDTO>>(playersByTeam[t.Id])
                    : new List<PlayerRequestResponseDTO>();

                return teamDto;
            });

            return teamListMapped;
        }
        
        public IEnumerable<TeamRequestResponseDTO> GetClassif(string groupName)
        {
            var teamList = _context.Teams
                .Include(t => t.Edition)
                .Include(t => t.Category)
                .Where(t => t.Group.Name == groupName)
                .OrderByDescending(c => c.Classification_points)
                .ToList();

            var playersByTeam = _context.Players
                .GroupBy(p => p.TeamId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var teamListMapped = teamList.Select(t =>{
                var teamDto = _mapper.Map<TeamRequestResponseDTO>(t);
                teamDto.TeamPlayers = playersByTeam.ContainsKey(t.Id)
                    ? _mapper.Map<List<PlayerRequestResponseDTO>>(playersByTeam[t.Id])
                    : new List<PlayerRequestResponseDTO>();

                return teamDto;
            });

            return teamListMapped;
        }
        
        public TeamRequestResponseDTO GetByName(string name)
        {
            try
            {
                if(string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("El nombre no puede ser nulo/vacío");

                var team = _context.Teams
                    .Include(t => t.Category)
                    .Include(t => t.Edition)
                    .Where(t => t.Name == name)
                    .FirstOrDefault();
                
                var teamPlayers = _context.Players
                    .Where(p => p.TeamId == team.Id)
                    .ToList();

                var teamDto = _mapper.Map<TeamRequestResponseDTO>(team);
                teamDto.TeamPlayers = _mapper.Map<List<PlayerRequestResponseDTO>>(teamPlayers);

                return teamDto;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
        
        public IEnumerable<TeamRequestResponseDTO> GetByGroup(string groupName)
        {
            var teamList = _context.Teams
                .Include(t => t.Edition)
                .Include(t => t.Category)
                .Where(g => g.Group.Name == groupName)
                .AsNoTracking();

            return _mapper.Map<IEnumerable<TeamRequestResponseDTO>>(teamList);
        }

        public IEnumerable<TeamRequestResponseDTO> GetByEdition(string edition)
        {
            try
            {
                if(string.IsNullOrEmpty(edition))
                    throw new Exception("El nombre de la edición no puede ser nulo/vacío");
                
                var teamList = _context.Teams
                    .Include(t => t.Edition)
                    .Include(t => t.Category)
                    .Where(t => t.Edition.Name == edition)
                    .ToList();
                
                return _mapper.Map<IEnumerable<TeamRequestResponseDTO>>(teamList);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
        
        public IEnumerable<TeamRequestResponseDTO> GetByCategory(string category)
        {
            try
            {
                if(string.IsNullOrEmpty(category))
                    throw new Exception("El nombre de la categoría no puede ser nulo/vacío");
                
                var teamList = _context.Teams
                    .Include(t => t.Edition)
                    .Include(t => t.Category)
                    .Where(t => t.Category.Name == category)
                    .ToList();
                
                return _mapper.Map<IEnumerable<TeamRequestResponseDTO>>(teamList);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
        
        public TeamRequestResponseDTO Post(TeamRequestInputDTO newTeamData)
        {
            try
            {
                if (string.IsNullOrEmpty(newTeamData.Name))
                    throw new Exception("El nombre del equipo no puede ser nulo/vacío");

                var existingTeam = _context.Teams
                    .Where(t => t.Name == newTeamData.Name)
                    .ToList()
                    .FirstOrDefault();
                
                if(existingTeam == null)
                {
                    var newTeam = _mapper.Map<Entities.Entities.Team>(newTeamData);

                    newTeam.Wins = 0;
                    newTeam.Defeats = 0;
                    newTeam.Points_diff = 0;
                    newTeam.Classification_points = 0;
                    
                    newTeam.GroupId = 1;

                    var category = _context.Categories
                        .Where(c => c.Name == newTeamData.CategoryName)
                        .ToList()
                        .FirstOrDefault()
                            ?? throw new Exception("La categoría especificada no existe");

                    newTeam.CategoryId = category.Id;

                    var edition = _context.Editions
                        .Where(e => e.Name == newTeamData.EditionName && e.IsActive == true)
                        .ToList()
                        .FirstOrDefault();
                    if(edition == null)
                    {
                        newTeam.EditionId = _context.Editions
                            .Where(e => e.IsActive == true)
                            .ToList()
                            .FirstOrDefault().Id;
                    }
                    else
                    {
                        newTeam.EditionId = edition.Id;
                    }

                    var result = _context.Teams.Add(newTeam);
                    _context.SaveChanges();

                    return _mapper.Map<TeamRequestResponseDTO>(result.Entity);
                }
                else
                {
                    return _mapper.Map<TeamRequestResponseDTO>(existingTeam);
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public TeamRequestResponseDTO PostWithPlayers(TeamWithPlayersRequestInputDTO newTeamWithPlayersData)
        {
            try
            {
                if(string.IsNullOrEmpty(newTeamWithPlayersData.Name))
                    throw new Exception("El nombre del equipo no puede ser nulo/vacío");
                
                var existingTeam = _context.Teams
                    .Where(t => t.Name == newTeamWithPlayersData.Name)
                    .ToList()
                    .FirstOrDefault();
                
                if(existingTeam == null)
                {
                    var newTeam = _mapper.Map<Entities.Entities.Team>(newTeamWithPlayersData);

                    newTeam.Wins = 0;
                    newTeam.Defeats = 0;
                    newTeam.Points_diff = 0;
                    newTeam.Classification_points = 0;

                    var category = _context.Categories
                        .Where(c => c.Name == newTeamWithPlayersData.CategoryName)
                        .ToList()
                        .FirstOrDefault();
                    
                    if(category == null)
                        throw new Exception("La categoría seleccionada no existe");
                    
                    newTeam.CategoryId = category.Id;
                    //FUEGO
                    newTeam.GroupId = 1;
                    //FUEGO
                    newTeam.EditionId = 4;

                    var newTeamAux = _context.Teams.Add(newTeam);
                    _context.SaveChanges();
                    var newTeamResult = newTeamAux.Entity;

                    if(newTeamWithPlayersData.PlayerList == null || newTeamWithPlayersData.PlayerList.Count == 0)
                        throw new Exception("No se han introducido los datos de los jugadores");
                    
                    if(newTeamWithPlayersData.PlayerList.Count < 3 || newTeamWithPlayersData.PlayerList.Count > 4)
                        throw new Exception ("El número de jugadores es incorrecto");
                    
                    var players = _mapper.Map<List<Entities.Entities.Player>>(newTeamWithPlayersData.PlayerList);
                    players.ForEach(p => p.TeamId = newTeamResult.Id);
                    _context.Players.AddRange(players);
                    _context.SaveChanges();

                    return _mapper.Map<TeamRequestResponseDTO>(newTeam);
                }
                else
                {
                    return _mapper.Map<TeamRequestResponseDTO>(existingTeam);
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
    
        public TeamRequestResponseDTO UpdateTeamInfo(TeamUpdateRequestInputDTO updateTeamInfo)
        {
            try
            {
                if(updateTeamInfo == null)
                    throw new Exception("No se ha pasado ningún dato.");

                if(string.IsNullOrEmpty(updateTeamInfo.Name))
                    throw new Exception("El nombre del equipo no puede ser nulo/vacío");

                var existingTeam = _context.Teams
                    .Include(t => t.Category)
                    .Include(t => t.Edition)
                    .Where(t => t.Name == updateTeamInfo.Name)
                    .ToList()
                    .FirstOrDefault();

                if(existingTeam == null)
                    throw new Exception("El equipo proporcionado no existe en la base de datos.");
                    
                var existingCategory = _context.Categories
                    .Where(c => c.Name == updateTeamInfo.CategoryName)
                    .ToList()
                    .FirstOrDefault();

                if (existingCategory == null)
                    throw new Exception("La nueva categoría especificada no existe en la base de datos.");

                var existingEdition = _context.Editions
                    .Where(e => e.Name == updateTeamInfo.EditionName)
                    .ToList()
                    .FirstOrDefault();
                
                if(existingEdition == null)
                    throw new Exception("La nueva edición especificada no existe en la base de datos.");

                existingTeam.Pay = updateTeamInfo.Pay;
                existingTeam.Wins = updateTeamInfo.Wins;
                existingTeam.Defeats = updateTeamInfo.Defeats;
                existingTeam.Classification_points = updateTeamInfo.Classification_points;
                existingTeam.CategoryId = existingCategory.Id;
                existingTeam.EditionId = existingEdition.Id;

                var updateResult = _context.Update(existingTeam);
                _context.SaveChanges();

                var response = _mapper.Map<TeamRequestResponseDTO>(updateResult.Entity);

                return response;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public TeamRequestResponseDTO AsignTeamGroup (TeamGroupRequestInputDTO newTeamGroupData){
            try 
            {
                var existingTeam = _context.Teams
                    .Where(t => t.Name == newTeamGroupData.TeamName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("No existe ningún equipo con este nombre");

                var existingGroup = _context.Groups
                    .Where(g => g.Name == newTeamGroupData.GroupName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("No existe ningún grupo con este nombre");

                existingTeam.GroupId = existingGroup.Id;

                var updateResult = _context.Update(existingTeam);
                _context.SaveChanges();

                var response  = _mapper.Map<TeamRequestResponseDTO>(updateResult.Entity);

                return response;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public TeamRequestResponseDTO DeleteTeam(string teamName, string editionName){
            try
            {
                var existingEdition = _context.Editions
                    .Where(e => e.Name == editionName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("No existe ninguna edicion con este nombre");
                
                var existingTeam = _context.Teams
                    .Where(t => t.Name.Equals(teamName) && t.EditionId == existingEdition.Id)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún equipo {teamName} en la edición {editionName}");
                
                _context.Teams.Remove(existingTeam);
                _context.SaveChanges();

                var response = _mapper.Map<TeamRequestResponseDTO>(existingTeam);

                return response;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
    }
}
