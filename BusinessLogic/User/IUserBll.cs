using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.User
{
    public interface IUserBll
    {
        IEnumerable<Entities.Entities.User> Get();

        Entities.Entities.User Get(string mail);

        IEnumerable<Entities.Entities.User> GetByRole(string role);

        Entities.Entities.User Post(Entities.Entities.User value);

        void Put(int id, string value);

        void Delete(string mail);
    }
}
