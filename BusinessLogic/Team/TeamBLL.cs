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

            return _mapper.Map<IEnumerable<TeamRequestResponseDTO>>(teamList);
        }

        public IEnumerable<TeamRequestResponseDTO> GetClassif(string groupName)
        {
            var teamList = _context.Teams.Where(t => t.Group.Name == groupName).OrderByDescending(c => c.Classification_points).ToList();

            return _mapper.Map<IEnumerable<TeamRequestResponseDTO>>(teamList);;
        }

        public TeamRequestResponseDTO Get(string name)
        {
            var team = _context.Teams.Where(t => t.Name == name).FirstOrDefault();

            return _mapper.Map<TeamRequestResponseDTO>(team);
        }

        public IEnumerable<TeamRequestResponseDTO> GetByGroup(string groupName)
        {
            var teamList = _context.Teams.Where(g => g.Group.Name == groupName).AsNoTracking();

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
