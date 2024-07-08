using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Group
{
    public interface IGroupBll
    {
        IEnumerable<Entities.Entities.Group> Get();

        Entities.Entities.Group Get(string name);

        Entities.Entities.Group Post(Entities.Entities.Group value);

        void Put(int id, string value);

        Entities.Entities.Group Delete(string name);
    }
}
