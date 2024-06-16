using Entities.AppContext;
using Microsoft.EntityFrameworkCore;
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

        #endregion

        #region Builder

        public TeamBLL()
        {
            _context = new Context();
        }

        #endregion

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Entities.Team> Get()
        {
            var teamList = _context.Teams
                .Include(t => t.Group)
                .OrderBy(t => t.Group.Id)
                .OrderByDescending(c => c.Classification_points)
                .OrderByDescending(ba => ba.Points_diff)
                .ToList();

            return teamList;
        }

        public IEnumerable<Entities.Entities.Team> GetClassif(string groupName)
        {
            var teamList = _context.Teams.Where(t => t.Group.Name == groupName).OrderByDescending(c => c.Classification_points).ToList();

            return teamList;
        }

        public Entities.Entities.Team Get(string name)
        {
            var team = _context.Teams.Where(t => t.Name == name).FirstOrDefault();

            return team;
        }

        public IEnumerable<Entities.Entities.Team> GetByGroup(string groupName)
        {
            var teams = _context.Teams.Where(g => g.Group.Name == groupName).AsNoTracking();

            return teams;
        }

        public Entities.Entities.Team Post(Entities.Entities.Team value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Name))
                    throw new Exception("El nombre del equipo no puede ser nulo/vacío");

                var team = _context.Teams
                    .Where(t => t.Name == value.Name)
                    .AsNoTracking()
                    .ToList().FirstOrDefault();

                if (team == null)
                {
                    var result = _context.Teams.Add(value);
                    _context.SaveChanges();
                    return result.Entity;
                }
                else
                {
                    return team;
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }

        }

        public void Put(int id, string value)
        {
            throw new NotImplementedException();
        }
    }
}
