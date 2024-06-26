using BusinessLogic.DTO;
using Entities.AppContext;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TeamRequestResponseDTO Get(string name)
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

                    newTeam.EditionId = 2;
                    newTeam.GroupId = 1;

                    var category = _context.Categories
                        .Where(c => c.Name == newTeamData.CategoryName)
                        .ToList()
                        .FirstOrDefault();
                    if (category == null)
                    {
                        throw new Exception("La categoría especificada no existe");
                    }
                    else
                    {
                        newTeam.CategoryId = category.Id;
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
    }
}
