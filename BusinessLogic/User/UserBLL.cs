using Entities.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.User
{
    public class UserBLL : IUserBll
    {
        #region Fields

        private readonly Context _context;

        #endregion

        #region Builder

        public UserBLL()
        {
            _context = new Context();
        }

        #endregion

        public void Delete(string mail)
        {
            var userSelected = _context.Users.Where(u => u.Mail == mail).ToList().FirstOrDefault();

            _context.Users.Remove(userSelected);

            _context.SaveChanges();
        }

        public IEnumerable<Entities.Entities.User> Get()
        {
            var userList = _context.Users.ToList();

            return userList;
        }

        public Entities.Entities.User Get(string mail)
        {
            var user = _context.Users.Where(u => u.Mail == mail).ToList().FirstOrDefault();

            return user;
        }

        public IEnumerable<Entities.Entities.User> GetByRole(string role)
        {
            var userList = _context.Users.Where(r => r.Role == role).ToList();

            return userList;
        }

        public Entities.Entities.User Post(Entities.Entities.User value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Mail))
                    throw new Exception("El email del usuario no puede ser nulo/vacio");

                // Primero compruebo si existe en la base de datos
                var user = _context.Users
                    .Where(u => u.Mail == value.Mail)
                    .AsNoTracking()
                    .ToList().FirstOrDefault();

                if (null == user)
                {
                    // Usuario nuevo
                    var result = _context.Users.Add(value);
                    _context.SaveChanges();
                    return result.Entity;
                }
                else
                {
                    // Ya existe
                    return user; 
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
