using Entities.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Group
{
    public class GroupBLL : IGroupBll
    {

        #region Fields

        private readonly Context _context;

        #endregion

        #region Builder

        public GroupBLL()
        {
            _context = new Context();
        }

        #endregion

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Entities.Group> Get()
        {
            var groups = _context.Groups.ToList();
        
            return groups;
        }

        public Entities.Entities.Group Get(string name)
        {
            var groupSelected = _context.Groups.Where(g => g.Name == name).ToList().FirstOrDefault();

            return groupSelected;
        }

        public Entities.Entities.Group Post(Entities.Entities.Group value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Name))
                    throw new Exception("El nombre del grupo no puede ser nulo/vacío");

                var group = _context.Groups
                    .Where(g => g.Name == value.Name)
                    .AsNoTracking()
                    .ToList().FirstOrDefault();

                if (group == null)
                {
                    var result = _context.Groups.Add(value);

                    _context.SaveChanges();

                    return result.Entity;
                }
                else
                {
                    return group;
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
